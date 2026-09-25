# Phase 0 技術自審與技術債清單（Claude 預審版）V1.0

> 狀態：**Claude 自審初稿，供 HANDOFF-008 §17「Phase 0 全面 Code Review」使用**，不是最終結論。
> 進 Phase 1 前仍需 ChatGPT 審查、咖哩裁定優先順序。
> 日期：2026-09-25　範圍：`unity/JiuyaoTianxu/Assets/_Project/` 全部程式碼（Phase 0-A～0-E）
> 方法：逐檔閱讀；離線編譯（Roslyn＋UnityEngine 參考組件＋Fusion DLL）；Unity-free 單元測試。
> **限制：** 雲端環境沒有 Unity，本文件的判斷都來自讀程式碼，**沒有實跑數據**（尤其效能與網路手感）。

---

## 1. 架構現況（一句話版）

```
輸入層  Core/  PlayerInputData ← LocalInputProvider(鍵盤+觸控) / AutoTestInputProvider
          │ (Fusion NetworkInput，Client 只送意圖)
網路層  Net/NetworkGameLauncher — Dedicated Server，Server 決定出生點/生怪/套用資料覆寫
          │
戰鬥層  Combat/  PlayerMovement(移動+面向) → CombatController(唯一戰鬥腳本, 資料驅動)
          │      HitDetectionService(唯一 Physics 查詢) → DamageService(唯一寫 Health)
          │      SpiritSealSystem(3 個通用 Hook)   TargetLock(Server 權威鎖定)
          │      CombatEvents.TargetKilled（HP >0→0 那一擊發一次）
玩法層  Gameplay/  Events(路由) → Quests(純C#狀態機 + QuestTracker) ; World(Spawner/怪物生命週期)
資料層  Config/Tables/*.csv → ConfigTableImporter → ScriptableObject（+ Server 執行期覆寫）
表現層  UI/  IMGUI 原型（虛擬搖桿、血條、傷害數字），只讀同步狀態
```

**做得好的地方（建議 Phase 1 保留）**
- 傷害單一入口（DamageService）、物理查詢單一入口（HitDetectionService），Phase 0-C／0-D 都沒另建管線。
- 網路上只傳 int id（SealId／QuestNumId／NetworkId），定義資料各端查表——這是可擴充的做法。
- 任務、靈印都沒有 `if (id == ...)` 寫死；任務狀態機是純 C# 且有完整轉移表測試。
- 所有 Phase 0 數值已集中在 CSV，並有驗證器檢查與資產一致。

---

## 2. 本次已修正（隨本 PR）

| # | 問題 | 修正 |
|---|---|---|
| F1 | Phase 0-E 虛擬搖桿用 `Input.touchSupported` 判斷顯示：有觸控螢幕的 Windows 筆電會跳出搖桿，而且滑鼠左鍵攻擊會被關掉 | 改用 `Application.isMobilePlatform`（桌機仍可用 `-touchui` 測試） |
| F3（=D2） | 蓄力／施法流程寫死在武器類型上 | 見下方 D2：已改為 `AttackDefinition.InputMode`（Tap／HoldRelease／Cast），attacks.csv 新增一欄、13 筆資產同步填值，行為不變 |
| F4（=D6） | 怪物數值寫死在 Editor 腳本 | 見下方 D6：新增 `monsters.csv`（MonsterId／DisplayName／MaxHp／DespawnDelay），匯入器寫進 `EnemyIdentity.TargetId` 相符的 prefab；validate_tables 另外檢查任務 TargetId 必須是表內怪物 |
| F5（=D1） | 赤炎燃燒對怪物無效、冷卻卻照樣消耗 | 咖哩裁定方案 A：新增 `Combat/Framework/StatusEffects/BurnStatus`（只有燃燒的最小元件），玩家與怪物 prefab 都掛；`SpiritSealSystem` 找不到 BurnStatus 時不觸發、不消耗冷卻；燃燒傷害仍走 DamageService。需重跑 0-C 靈印 regression |
| F6（=D7） | 物理查詢緩衝 16 格，人多漏打 | 加大到 64，滿格時每幀最多一次警告 |
| F7（=D8） | Projectile 每 tick 配置記憶體且自己查 Physics | 改走 `HitDetectionService.TryFindFirstHealth`（NonAlloc、不配置 List），命中規則不變 |
| F8（=D10） | 每次命中/觸發都 `Debug.Log` | 新增 `Core/GameLog.Info`（`[Conditional]`：Editor／Development build／`JIUYAO_GAMELOG` 才編進去），戰鬥、靈印、任務、鎖定、怪物的逐事件 log 改用它；Phase 0 驗收 build 都是 Development，log 與 autotest 比對不受影響；警告/錯誤與啟動訊息維持 `Debug.*` |
| F9（=D13） | 有人離開再加入時出生點重疊 | 改為分配「目前沒人佔用的最小出生點編號」，離開時釋放 |
| F10（=D15） | `CombatState.Spawned` 在所有端寫同步屬性 | 只有 state authority 寫 |
| F11（=D3 測試版＋D11） | 沒有死亡／重生；死掉的目標還會吃掉攻擊 | 咖哩同意先做**測試版**：`Health.IsDead`（HP 0）；新增 `Gameplay/World/PlayerLifecycle`，倒地 5 秒（可調整）後在自己第一次出生的位置滿血復活，無懲罰；倒地時戰鬥/移動/鎖定/閃避測試都不接受輸入、鎖定解除、燃燒停止；`HitDetectionService` 不再把死亡目標當命中對象。**正式死亡規則（懲罰、復活點、副本規則）仍需 HANDOFF** |
| F2 | Server 直接使用 Client 送來的搖桿值：改過的 Client 送 NaN 會讓角色座標變 NaN 並同步給所有人；送超長向量＝加速外掛 | 新增 `Core/InputSanitizer`，`PlayerMovement` 在碰 transform 前先清洗（NaN/Infinity→0、長度夾到 1）；單元測試 4 項 |

---

## 3. 問題與技術債清單

嚴重度：**P0 = 進 Phase 1 前應處理**／**P1 = Phase 1 期間處理**／**P2 = 之後再說**。
依 HANDOFF-008 §16「不要直接大改，先提方案」，以下**都還沒改**，等審查裁定。

### P0

**D1. 赤炎燃燒對怪物無效（功能缺陷）——✅ 已修正（F5，咖哩 2026-09-25 裁定方案 A）**
- 位置：`Combat/Framework/SpiritSeals/SpiritSealSystem.cs:100`
- 現象：燃燒狀態存在「被打者」的 `SpiritSealSystem` 裡；Phase 0-D 的怪物沒有這個元件，所以
  `targetSeals?.ApplyBurn(...)` 靜默跳過——**但冷卻照樣被消耗、log 照樣寫「triggered」**。
  0-C 測試全是玩家互打，所以沒被發現；Phase 1 打怪時赤炎等於沒用。
- 方案 A（建議）：把燃燒抽成獨立的最小元件 `BurnStatus : NetworkBehaviour`（只有燃燒，不是
  完整 Status Effect Framework），掛在所有有 Health 的 prefab；`SpiritSealSystem` 改呼叫
  `target.GetComponent<BurnStatus>()`，找不到時**不消耗冷卻**。傷害仍走 DamageService。
- 方案 B（最快、偏 hack）：怪物 prefab 也掛空的 `SpiritSealLoadout`＋`SpiritSealSystem`，零程式修改。
- 影響：改動 0-C 程式碼，需重跑 0-C 靈印 regression。

**D2. 蓄力／施法流程寫死在武器類型上（違反資料驅動原則）——✅ 已於本 PR 修正（F3）**
- 位置：`Combat/Framework/CombatController.cs:111-112`（`WeaponType == Bow` → Charging、`== Staff` → Casting）
- 影響：Phase 1 若出現「會蓄力的刀」或「施法型的槍技」，就得改 CombatController。
- 方案：`AttackDefinition` 新增 `InputMode { Tap, HoldRelease, Cast }`，attacks.csv 加一欄，
  CombatController 改讀這欄；現有 13 筆資產同步填值，行為不變。

**D3. 沒有死亡／重生——🟡 測試版已做（F11），正式規則待 HANDOFF**
- 位置：`Combat/Health.cs:40`（HP 夾在 0，玩家 HP 0 仍可移動攻擊）
- 影響：Phase 1 的「副本通關」「主線→世界事件」都需要倒地與復活；0-C 的玄甲在 HP=0 時會反覆觸發
  （0-C README 已記錄）。
- 方案：Health 加 `IsDead`（HP 由 >0 變 0 時設定，已有 CombatEvents 可沿用）；CombatController／
  PlayerMovement 在 IsDead 時不接受輸入；Server 在 N 秒後把玩家移回出生點並補滿 HP。需要 HANDOFF 定規則
  （懲罰、復活點、副本內規則）。

**D4. 沒有延遲補償，命中判定用 Server 當下位置**
- 位置：`HitDetectionService`（全部查詢）；`NetworkProjectConfig.fusion` 的 `LagCompensation.Enabled: false`
- 影響：`03_COMBAT_SYSTEM` §4 明訂短窗口判定要用 Rewind／Lag Compensation；手機網路延遲 100ms 以上時，
  近戰「明明砍到卻沒中」。這是閃避系統的前置條件。
- 方案：啟用 Fusion Lag Compensation，角色／怪物掛 `HitboxRoot`＋`Hitbox`，HitDetectionService 改用
  `Runner.LagCompensation.OverlapSphere` 等 API（仍集中在同一個類別，呼叫端不變）。需實機驗證。

**D5. Client 沒有預測，操作延遲＝網路來回時間**
- 位置：`CombatController`／`PlayerMovement`／`TargetLock` 都是 `if (!Object.HasStateAuthority) return;`
- 影響：Client 按下攻擊或移動，要等 Server 回傳才看到——在區域網路測不出來，手機 4G 會很明顯。
  「戰鬥雙搖桿手感」是 Roadmap 列為絕對不能砍的項目。
- 方案：先讓移動在 Client 預測（Fusion 的 `NetworkCharacterController` 或自寫 predicted movement），
  攻擊動畫可先本地播放、傷害仍由 Server 決定。需要 ChatGPT 決定預測範圍。

**D18. `[Rpc]` 在 Mono 打包版整個不能用（2026-09-26 本機實跑發現）——⚠️ 已繞過，待裁定專案標準**
- 位置：Fusion 2.1.2 weaver（`Assets/Photon/Fusion/CodeGen/Fusion.CodeGen.cs:1201`）
- 現象：weaver 在每個 `[Rpc]` 方法插入對 `Fusion.Runtime` internal 方法的呼叫，Mono 執行時丟
  `MethodAccessException`。手機正式包用 IL2CPP 可能不受影響，但 Dedicated Server 若用 Mono 就會壞。
- 目前做法：一次性 Client→Server 命令走 `Core/ClientCommands`（`SendReliableDataToServer`），接任務已改用。
- 待裁定：是否定為專案標準並寫進 01_ARCHITECTURE_DECISIONS；Server 打包方式（Mono／IL2CPP）；
  或等 Photon 修正後恢復 `[Rpc]`。細節見 CLAUDE-REPLY-008。

### P1

| # | 問題 | 位置 | 方案 |
|---|---|---|---|
| D6 | ✅ 已修正（F4）：怪物數值原本寫死在 Editor 腳本 | `Editor/Phase0DSetup.cs`（原 `MonsterMaxHp = 30`） | 已改為 `monsters.csv` → prefab。**限制**：怪物不在 Server 執行期覆寫範圍（prefab 值不是共用 ScriptableObject），之後若需要可改成 MonsterDefinition 資產 |
| D7 | ✅ 已修正（F6）：物理查詢緩衝只有 16 格，人多時會漏打目標 | `HitDetectionService.cs:17` | 20~50 人副本前加大（例如 64）並在滿格時 log 警告 |
| D8 | ✅ 已修正（F7）：Projectile 每 tick 用會配置記憶體的 `Physics.OverlapSphere`，且不經過 HitDetectionService | `Projectile.cs:45` | 改用 NonAlloc，或移進 HitDetectionService |
| D9 | 擊退直接改目標 transform，沒有碰撞檢查，可能把角色推進牆裡 | `CombatController.cs:243` | 改成經 PlayerMovement／CharacterController 的位移 |
| D10 | ✅ 已修正（F8）：每次命中、每次靈印觸發都 `Debug.Log`；Server 在多人時會被 log 拖慢 | `CombatController`、`SpiritSealSystem`、`QuestTracker` 等 | 包一層 `[Conditional]` 的 GameLog，Release／正式 Server 關閉，驗收用 build 保留 |
| D11 | ✅ 已修正（F11）：死掉的目標（HP 0，消失前 0.5 秒）仍會吃掉近戰命中與箭矢 | `HitDetectionService`、`Projectile` | 與 D3 一起做：IsDead 目標不列入命中 |
| D12 | `PlayerCount: 10`（每房上限） | `NetworkProjectConfig.fusion` | Phase 2 的 20~50 人副本前調整並做負載測試 |
| D13 | ✅ 已修正（F9）：出生點索引用「目前人數」，有人離開再加入時兩人會疊在同一點 | `NetworkGameLauncher.cs:142` | 改成找目前沒人站的出生點，或用 PlayerRef 分配 |

### P2

- **D14** 三個元件各自存一份 `[Networked] PreviousButtons`（CombatController／SpiritSealSystem／TargetLock），
  可以收斂成一個輸入元件統一算「這個 tick 剛按下」。
- **D15** ✅ 已修正（F10）：`CombatState.Spawned` 在所有端都寫 Networked 屬性。
- **D16** IMGUI 原型每幀有少量 GC；Phase 1 換正式 UI 時一併淘汰，不值得現在優化。
- **D17** 靜態事件（CombatEvents／GameplayEvents／QuestEvents）已用 runner 過濾；若之後改成同一個
  process 開多個 runner（Fusion 多 peer 測試模式），需再確認每個訂閱者都有過濾。
- **D19**（2026-09-26 實跑發現）Unity 6000.5 編譯有 CS0618 過時 API 警告：`FindObjectsSortMode`
  （`MonsterSpawner.cs:45`、`NetworkGameLauncher.cs:154`）、`SimulationMessagePtr`（`NetworkGameLauncher.cs:199`）。
  目前不影響執行，升級 Unity 前換成新 API。
- **D20**（2026-09-26 實跑發現）`Projectile` 命中沒有 log，regression 無法從 log 確認弓有打中目標
  （刀／劍／槍／重刃／靈杖都有命中 log）。之後加一行命中 log（走 D10 的 GameLog）。

---

## 4. 手機效能初步觀察（只看程式碼，無實測）

- 每 tick 的配置：CommandLineFlags 已改為只解析一次（0-E）；HitDetection 與箭矢都用 NonAlloc（F7）；
  逐事件 log 在 release build 連字串組合一起剝除（F8）。
- `FindHealthInRadius` 只在按鎖定鍵時配置一個 List，可接受。
- `ConfigOverrideLoader` 只在 Server 啟動時跑一次。
- **真正的效能數據需要實機**：建議 Phase 1 前用中階 Android 手機跑一次 Phase0D 場景，記錄 FPS、
  GC、網路頻寬（Fusion Statistics 面板已在專案內）。

---

## 5. 建議的 Phase 1 前順序

1. ~~本機跑完 Phase 0-D／0-E 驗收~~ 2026-09-26 完成：0-D COMPLETE；0-E 只差手機觸控（CLAUDE-REPLY-008、CLAUDE-NOTE-006）。
2. ChatGPT 審這份清單，裁定 P0 範圍 → 發 HANDOFF。
3. D1、D2、D6 已完成（待本機 regression）。
4. D3（死亡／重生）需要規則 → HANDOFF 定規格後做。
5. D4、D5（延遲補償、Client 預測）是 Phase 1 手感的關鍵，建議獨立一個 Phase 0-F／1-0 技術驗證，
   並在手機 4G 環境實測。

## 6. 待決問題（給 ChatGPT／咖哩）

1. ~~D1 選方案 A 還是 B？~~ 咖哩裁定 A，已實作。
2. D5 Client 預測要做到哪裡：只有移動？還是連攻擊起手也預測？
3. ~~D3 要不要先做測試版？~~ 咖哩同意，已做（5 秒後回出生點滿血，無懲罰）；正式規則仍待定。
4. D18：一次性 Client→Server 命令是否以 `ClientCommands` 為專案標準？Dedicated Server 用 Mono 還是 IL2CPP 打包？
