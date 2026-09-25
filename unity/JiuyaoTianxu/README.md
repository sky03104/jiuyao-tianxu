# 《九曜：天墟》Unity 專案 — Phase 0-A / 0-B / 0-C / 0-D / 0-E

## 版本資訊
- Unity Editor：**6000.5.5f1**
- URP：**17.5.0**（隨此 Editor 版本內建）
- Photon Fusion：**2.1.2 (stable, build 2279)**，透過 Unity Asset Store「My Assets」
  取得 `.unitypackage` 後以批次模式匯入
- 額外相依套件：`com.unity.ugui` 2.5.0（Fusion Statistics面板需要，新專案未內建，
  已補進manifest.json）、`com.unity.nuget.mono-cecil` 1.10.2（Fusion IL Weaver需要，
  匯入時自動加入manifest.json）

## 專案結構

```
Assets/_Project/
  Core/      — 輸入抽象層（PlayerInputData/KeyboardInputProvider/AutoTestInputProvider）
  Net/       — NetworkGameLauncher（Fusion啟動/回呼）
               Net/Prefabs/（Player, NetworkRunner, Projectile）
  Combat/    — PlayerMovement（移動＋Phase0-E雙搖桿面向）、Health（Server權威狀態持有者）
    Targeting/ — Phase 0-E：TargetingMath（純C#面向/鎖定規則）、TargetLock（Server權威目標鎖定）
    Framework/ — 共用Combat Framework（HANDOFF-006 0-B-01~05），六種武器共用同一套：
      WeaponType.cs / CombatPhase.cs / HitShapeType.cs — 列舉
      AttackDefinition.cs / WeaponDefinition.cs — 資料驅動攻擊/武器定義（ScriptableObject）
      CombatState.cs — 純狀態持有NetworkBehaviour（Phase/ComboStep/計時器）
      CombatController.cs — 唯一的戰鬥腳本，六武器差異全部來自資料而非程式碼
      HitDetectionService.cs — 唯一呼叫Physics.Overlap*的地方
      DamageService.cs — 唯一寫入Health的地方
      DamageTypes.cs — DamageRequest/DamageResult
      Projectile.cs — 弓/杖用簡易彈道
  Combat/Weapons/ — 六把武器的資料資產（Phase0BWeaponDataSetup產生）：
    Blade/ Sword/ Spear/ Bow/ HeavyBlade/ Staff/ 各含WeaponDefinition+AttackDefinition
    Framework/SpiritSeals/ — 靈印Trigger/Modifier框架（HANDOFF-007 0-C-01起）：
      SpiritSealTriggerType.cs — 觸發時機列舉（OnAttackHit/OnFatalDamage/OnDodgeEvent）
      SpiritSealDefinition.cs — 資料驅動靈印定義（ScriptableObject）
      SpiritSealRegistry.cs — 網路int SealId → 資產的查表（Fusion不能直接網路化ScriptableObject參照）
      SpiritSealIds.cs — 三個Prototype靈印的穩定id常數（執行期／編輯器共用）
      SpiritSealLoadout.cs — 固定8槽NetworkBehaviour（純資料，不含邏輯）
      SpiritSealSystem.cs — 唯一的Trigger/Modifier邏輯，DamageService透過三個
        通用Hook呼叫它，新增第4個靈印不需要修改CombatController或DamageService
  Combat/SpiritSeals/ — 赤炎/玄甲/影遁三個Prototype資料資產+Registry
    （Phase0CSpiritSealDataSetup產生）
  Gameplay/  — Phase 0-D（HANDOFF-008）地圖/Spawn/怪物/任務骨架：
    Events/   — GameplayEvents（EnemyKilled）、CombatToGameplayEventRouter
                （把CombatEvents.TargetKilled轉成EnemyKilled，只認有EnemyIdentity的目標）
    World/    — PlayerSpawnPoint/MonsterSpawnPoint（場景標記）、MonsterSpawner（Server-only）、
                MonsterLifecycle（死亡→Despawn）、EnemyIdentity（TargetId資料）
                World/Prefabs/Phase0D_TestMonster.prefab（Phase0DSetup產生）
    Quests/   — QuestDefinition/QuestRegistry（資料）、QuestStateMachine（純C#轉移表）、
                PlayerQuestLog（網路狀態）、QuestTracker（唯一任務邏輯）、QuestEvents、QuestIds
                Quests/Data/（Q_PHASE0D_001/002 + QuestRegistry，Phase0DSetup產生）
    Testing/  — Phase0DTestRunner（Server端觀察者，印SUMMARY/PASS）
  UI/        — Phase 0-E 原型介面（IMGUI，非正式版）：
    TouchControls/VirtualControlsOverlay — 虛擬搖桿＋攻擊鍵兼瞄準搖桿＋閃/鎖/換/任務鍵
    Hud/ — CombatHudOverlay（血條/傷害數字/鎖定標記/debug任務清單）、HealthFeedback（受擊閃紅）、
           LocalPlayerCameraFollow
  Economy/   — 經濟資料展示層（尚未使用）
  Guild/     — 公會資料展示層（尚未使用）
  Config/    — Phase 0-E 資料表：Tables/*.csv（attacks/weapons/spirit_seals/quests，所有Phase0數值的來源）、
               Core/（CsvTable/TableBinder，純C#）、ConfigOverrideLoader（Server執行期覆寫）
  Art/       — 美術資源（尚未使用）
  Scenes/    — 場景（Phase0A_NetworkTest.unity；Phase0D_TestScene.unity 由Phase0DSetup產生）
  Editor/    — 一次性設定工具（Phase0ASetup/Phase0ANetworkSetup/Phase0BWeaponDataSetup/
    Phase0CSpiritSealDataSetup/Phase0ABuild/Phase0DSetup/Phase0DBuild）
  Settings/  — URP Pipeline Asset 等專案設定資產
Tools/       — Unity不會匯入的外部工具（資料夾在Assets外）：
  run_all_checks.sh — 不需Unity的全部檢查一次跑完（離線編譯＋三組單元測試＋資料表驗證），CI 也用它
  CompileCheck/ — 離線編譯檢查（Roslyn on mono＋UnityEngine/UnityEditor參考組件＋專案Fusion DLL，含 --self-test）
  QuestLogicTests/ — 任務狀態機單元測試（不需Unity，run.ps1 / run.sh）
  ConfigTableTests/ — CSV解析/綁定單元測試＋validate_tables.py（欄名/型別/交叉參照/與資產一致）
  ControlsTests/ — 面向/鎖定/搖桿曲線單元測試
  Phase0D/run_autotest.ps1 — Phase 0-D 1 Server+2 Client 自動驗收
```

## 如何開啟專案
1. 用 Unity Hub 開啟 `unity/JiuyaoTianxu` 資料夾（需要 Unity 6000.5.5f1，或相容版本）。
2. 補上本機的Photon App ID（見下方「Photon App ID設定」）。
3. 開啟 `Assets/_Project/Scenes/Phase0A_NetworkTest.unity`，按Play：Editor內預設以
   GameMode.Host啟動（本機測試用，正式build應以`-netmode server`啟動才是01已定案的
   Dedicated Server架構）。

## 如何跑 2-Client 連線測試（本次Phase 0-A驗收採用的方式）

因為要驗證的是Dedicated Server拓樸（不是Editor內Host模式），實際驗收是打包成
Windows Standalone後，啟動3個獨立進程：

```
Unity.exe -batchmode -executeMethod Phase0ABuild.Build -quit   # 打包
JiuyaoTianxu.exe -batchmode -nographics -netmode server -autotest -logFile server.log
JiuyaoTianxu.exe -batchmode -nographics -netmode client -autotest -logFile client1.log
JiuyaoTianxu.exe -batchmode -nographics -netmode client -autotest -logFile client2.log
```

`-autotest`旗標會讓玩家用固定節奏自動按攻擊鍵（`AutoTestInputProvider`），因為
`-nographics`的無視窗進程收不到真實鍵盤輸入，這是Phase0-A headless驗收專用的
測試替身，不是正式輸入方案。

## Phase 0-A 進度（依 `docs/HANDOFF-005_PHASE0A.md` 執行順序）

| 步驟 | 狀態 |
|---|---|
| 1. Unity + URP 專案建立 | ✅ 完成 |
| 2. Photon Fusion 套件／版本確認 | ✅ 完成（2.1.2 stable，編譯通過無錯誤） |
| 3. Network Runner 基礎連線 | ✅ 完成（1 Server + 2 Client，互相看見） |
| 4. 玩家移動 | ✅ 完成（Server權威，WASD／AutoTest輸入抽象層已就緒） |
| 5. 玩家可見性與同步 | ✅ 完成（NetworkTransform同步位置） |
| 6. 基礎戰鬥 | ✅ 完成（攻擊→命中判定→傷害→HP） |
| 7. Server Authority | ✅ 完成（所有邏輯以`Object.HasStateAuthority`／`runner.IsServer`把關） |
| 8. 穩定性測試 | ✅ 完成（見下方「驗收結果」，324次命中循環，無錯誤/例外） |

## 驗收結果（2026-09-16，Windows Standalone Build，1 Server + 2 Client）

- **連線**：Server(PID 76) + 2個Client進程各自`StartGame succeeded`、
  `Connected to server`；Server log記錄兩名玩家加入`Player joined: [Player:2]`
  `[Player:3]`；兩個Client互相看見對方（Client1看到自己是Player:2、對方Player:3；
  Client2反之）。
- **戰鬥循環**：324次`Attack→Hit→Damage→HP Sync`完整循環（遠超過驗收標準的10次），
  HP從100正確遞減至0並在0處clamp（`Mathf.Max(0, ...)`生效，未見負值）。
- **Server Authority**：`PlayerCombat`/`PlayerMovement`/`Health`的邏輯修改全部
  guard在`Object.HasStateAuthority`（Server topology下即Server本身），Client
  端無法直接改對方HP；`OnPlayerJoined`的生成邏輯guard在`runner.IsServer`。
- **加入／離開**：手動終止其中一個Client進程，Server正確觸發
  `Player left: [Player:3]`，未發生崩潰或卡死。
- **錯誤/例外**：Server log全程無`Exception`/`NullReference`/`Unhandled`關鍵字
  命中（排除已知的授權握手警告，屬本機Editor授權訊息，與Fusion連線無關）。
- **已知問題**：Dedicated Server模式下Server本身不生成本地玩家（符合預期，
  Dedicated Server不該有本地玩家）；因此本測試用1 Server + 2 Client（共2名玩家）
  驗證，而非最初嘗試的1 Server + 1 Client（該組合下只有1名玩家、打不到任何目標，
  已排查並記錄於`docs/00_AI_HANDOFF_BRIDGE.md`）。
- **除錯過程中修正的一個Editor腳本陷阱**：`PrefabUtility.SaveAsPrefabAsset`
  回傳的物件參照，會在後續`EditorSceneManager.OpenScene`切換場景後失效（即使
  是新鮮取得的參照也一樣）。已改為「先開場景，再用`AssetDatabase.LoadAssetAtPath`
  依路徑重新讀取」的寫法，避免這個Unity Editor腳本的已知坑。

---

## Phase 0-B：Combat Framework + 六大武器 MVP（依`docs/HANDOFF-006_PHASE0B.md`）

### 進度

| 子階段 | 狀態 |
|---|---|
| 0-B-01 Combat Framework骨架 | ✅ 完成 |
| 0-B-02 Attack Definition資料驅動 | ✅ 完成（ScriptableObject） |
| 0-B-03 Combo/State Machine | ✅ 完成 |
| 0-B-04 Hit Detection抽離 | ✅ 完成（HitDetectionService） |
| 0-B-05 Damage Service | ✅ 完成 |
| 0-B-06 刀修 | ✅ 完成 |
| 0-B-07 劍修 | ✅ 完成 |
| 0-B-08 槍修 | ✅ 完成 |
| 0-B-09 弓修 | ✅ 完成 |
| 0-B-10 重刃 | ✅ 完成 |
| 0-B-11 靈杖 | ✅ 完成 |
| 0-B-12 六流派對照測試 | ✅ 完成 |
| 0-B-13 Server Authority/Network Regression | ✅ 完成 |
| 0-B-14 文件與測試報告 | ✅ 完成（本節） |

### Framework 分層

`CombatController`是**唯一**的戰鬥腳本（取代Phase 0-A的`PlayerCombat`），六種武器的
差異完全來自`WeaponDefinition`/`AttackDefinition`資料資產，不是六份程式碼：

```
輸入(PlayerInputData) → CombatController → CombatState(狀態機)
                              ↓
                     AttackDefinition(資料驅動)
                        ↓            ↓
              HitDetectionService  Projectile(弓/杖)
                        ↓
                  DamageService → Health
```

- **CombatState**：只持有`Phase`/`ComboStep`/`PhaseTimer`/`ComboWindowTimer`等網路化
  狀態，不含任何邏輯，供未來動畫/UI連段計數器等系統獨立讀取。
- **HitDetectionService**：唯一呼叫`Physics.Overlap*`的地方，支援
  Sphere/Box/Capsule/Area四種即時查詢；Projectile是唯一例外（用獨立NetworkObject
  逐tick飛行判定，不是瞬時查詢）。
- **DamageService**：唯一寫入`Health`的地方，`DamageRequest→DamageResult`的責任邊界
  已建立，未來Crit/Element/Armor/Resistance/Shield/StatusEffect都加在`DamageResult`
  這個既有型別上，不需要重新設計整條管線。
- **Server Authority延續**：`CombatController`/`Health`所有狀態修改仍guard在
  `Object.HasStateAuthority`，`DamageService.Resolve`額外再檢查一次
  `target.Object.HasStateAuthority`才寫入，防止未來有人在guard外的地方誤呼叫。

### 六大武器辨識度設計（非僅數值差異）

| 武器 | 辨識機制 | 關鍵數據 |
|---|---|---|
| 刀 Blade | 3段近戰連擊，第3段轉重擊+擊退 | Step3 Damage 22（Step1/2為8/10），觸發Knockback |
| 劍 Sword | 5段快速連擊，可邊移動 | 每段Startup/Active/Recovery僅0.05~0.08s，`CanMoveDuringAttack=true` |
| 槍 Spear | 中距離Capsule突刺，破甲+擊退 | Range 3.5（刀劍僅1.3~1.5），`AppliesArmorBreak=true` |
| 弓 Bow | Hold→Charge→Release→Projectile | `CombatPhase.Charging`（按住不放，鬆開才發射），非計時觸發 |
| 重刃 HeavyBlade | 慢速大AOE，簡單霸體 | Startup 0.45s+Recovery 0.55s（全武器最慢），`GrantsSuperArmor=true` |
| 靈杖 Staff | Cast延遲→定點AOE | `CombatPhase.Casting`，ChargeOrCastTime 0.6s後於施法點造成範圍傷害 |

### 測試方式與結果（0-B-12/0-B-13，實跑證據）

沿用Phase 0-A的1 Server+2 Client headless驗收模式，`-autotest`旗標驅動的
`AutoTestInputProvider`除了原本的攻擊按鍵節奏，新增「每個武器測試窗口內，攻擊一段
時間→完全靜置一段時間（保證回到Idle）→按一次切換武器鍵」的循環，讓兩個玩家在單次
執行中自動輪過全部六把武器。

**測試結果**（單次~35秒執行）：
- 六種武器全部至少切換到並攻擊過：Blade×17次組合啟動、Sword×31、Spear×18、
  Bow×12、HeavyBlade×6、Staff×6次施法。
- 27次命中紀錄，傷害數值隨武器不同（8/10/12/14/16/22等），證實資料驅動生效，
  不是同一套寫死數字。
- 12次Projectile成功發射（Bow charge-release、Staff cast都會經過這條路徑）。
- HP正確遞減並在0 clamp（觀察到多筆`HP now 0`紀錄，無負值）。
- 手動終止一個Client，Server正確觸發`Player left`，未崩潰。
- 全程搜尋`Exception`/`NullReference`/`Unhandled`：**0命中**。
- Phase 0-A已驗證的連線/移動/同步能力未被破壞（兩Client互相看見、`StartGame
  succeeded`、`Connected to server`皆正常）。

### 已知問題（除錯過程誠實記錄）

1. **測試分身的節奏設計缺陷（已修正）**：`AutoTestInputProvider`第一版用
   `Time.frameCount`（畫面更新幀數）安排攻擊/切換武器節奏，實測發現在
   `-nographics`無視窗headless進程中，Unity的Update幀率跟Fusion的固定模擬tick
   完全脫鉤——導致同一份輸入被套用到大量網路tick上，劍（Sword）的快速連段節奏
   剛好跟原本的攻擊按鍵週期共振，使戰鬥狀態永遠回不到Idle，讓「僅限Idle才能切換
   武器」的規則卡死，測試進程曾因此卡住超過30分鐘沒有進展。**修法**：改用
   `NetworkRunner.Tick.Raw`（Fusion真正的模擬tick計數）取代`Time.frameCount`
   安排測試輸入節奏，與`CombatController`本身用來計時的`TickTimer`處於同一個
   時鐘基準，問題排除。
2. **測試佈局缺陷（已修正）**：兩個測試玩家原本生成時面朝同一方向（Phase 0-A
   遺留的`Quaternion.identity`），而近戰命中判定是往「面朝方向」偏移的Sphere/
   Capsule，導致兩個玩家實際上打不到彼此。已修正`NetworkGameLauncher`讓生成的
   兩名玩家面對面。
3. **Staff初始Range設定過大（已修正）**：靈杖第一版Range=6、AreaRadius=2.5，
   相對於測試用的1.5單位生成間距，施法點會直接飛越目標。已依測試佈局調整為
   Range=2.5、AreaRadius=2，正式數值仍待Phase 1實際地圖/距離手感調整。

以上三項都是**Phase 0-B測試工具本身的瑕疵**，不是Combat Framework或六武器邏輯的
錯誤——三次除錯都是靠實際跑測試、看log找證據排查出來的，過程中沒有跳過任何一次
真的失敗就直接宣稱成功。

---

## Phase 0-C：靈印 Data-driven Prototype（依`docs/HANDOFF-007_PHASE0C.md`）

### 完成條件對照

| 項目 | 狀態 |
|---|---|
| A. `SpiritSealDefinition`建立、固定8槽、三靈印為資料資產、改參數不需改CombatController | ✅ |
| B. Trigger管線建立、Modifier/Effect Hook建立、沿用既有DamageService、無第二套權威入口 | ✅ |
| C. 赤炎可觸發／玄甲可保命一次／影遁可Armed→消耗／Cooldown生效 | ✅ |
| D. 1 Server+2 Client、Server Authority、必要狀態同步、Client無法直接改結果 | ✅ |
| E. 每個Prototype≥10次、總數≥30、Join/Leave regression、Data-driven修改測試 | ✅ |
| F. 文件（本節）+ CLAUDE-REPLY-007 + CHANGELOG | ✅ |

### 架構：CombatController／DamageService 完全未修改

這是本階段最重要的驗證結果——**`CombatController.cs`本次零修改**。三個靈印的
Hook全部集中在`DamageService.Resolve()`裡呼叫的三個通用方法：

```
DamageService.Resolve(request)
  ├─ sourceSeals.ModifyOutgoingDamage(rawDamage)       ← 影遁：消耗Armed加成
  ├─ targetSeals.TryPreventFatalDamage(final, HP)      ← 玄甲：致命傷害封頂保命
  ├─ Health.ApplyDamage(final)                          ← 既有Phase0B管線，未改
  └─ sourceSeals.OnAttackHitDealt(target, final)        ← 赤炎：對目標施加燃燒
```

`SpiritSealSystem`內部用資料迴圈（`for slot in 8槽`+`registry.GetById()`）決定
要不要觸發，沒有任何`if (sealId == Blaze)`這類寫死判斷。新增第4個靈印只需要：
建立一個新的`SpiritSealDefinition`資產＋（如果是全新的觸發時機）在
`SpiritSealTriggerType`加一個列舉值，兩者都不涉及修改`CombatController`或
`DamageService`的既有程式碼。

赤炎的燃燒效果（DoT）沒有另外建立獨立的Status Effect元件，而是讓
`SpiritSealSystem`自己持有燃燒計時狀態，每次tick透過**同一條**
`DamageService.Resolve()`管線（用`FlatDamageOverride`+`IsStatusDamage=true`
旗標）造成傷害——刻意不建第二套傷害系統，符合HANDOFF-007第9節要求。

### 三個Prototype的最小驗證行為

- **赤炎（OnAttackHit）**：攻擊命中後，若冷卻已好，對目標施加3段燃燒（每段3點，
  間隔1秒），燃燒傷害走同一條DamageService管線，但不會讓燃燒本身再次觸發赤炎
  （`IsStatusDamage`旗標擋下）。
- **玄甲（OnFatalDamage）**：偵測到即將致命的傷害時，若冷卻已好，把傷害封頂讓
  HP剛好停在1（而非0），驗證「靈印可以介入Health/Damage流程」。
- **影遁（OnDodgeEvent）**：測試用Dodge事件（真正的Dodge系統尚未建立，
  HANDOFF-007§7.3明確允許用測試事件代替）觸發後進入Armed狀態，下一次攻擊會
  自動消耗Armed並疊加+5傷害，驗證「靈印可以介入攻擊輸出」。

### Cooldown 機制

每個靈印槽位獨立持有一個`TickTimer`（`SpiritSealLoadout.Cooldowns`，Fusion
`NetworkArray<TickTimer>`），以`Runner`的權威模擬時間為準（`TickTimer.
CreateFromSeconds(Runner, ...)`），不使用Client本地時間。冷卻中的觸發會被
直接跳過（不排隊、不緩衝），符合HANDOFF-007第8節「Trigger→Cooldown Start→
Active→再次Trigger被拒絕→結束→可再次Trigger」的最低需求。

### 測試方式與結果（實跑證據）

沿用Phase0-A/B的1 Server+2 Client headless模式，`AutoTestInputProvider`
新增「每90 tick（約1.5秒）按一次測試用Dodge鍵」，獨立於原本的攻擊/切換武器
節奏（`SpiritSealSystem`自己讀取這個按鍵，`CombatController`完全不知道它
的存在）。角色出生時自動裝備赤炎/玄甲/影遁到8槽中的前3槽（無背包UI，
HANDOFF-007允許此簡化）。

**主測試結果**（單次約45秒）：
- 赤炎觸發 **20次**、玄甲觸發 **10次**、影遁武裝 **36次**／消耗 **26次**——
  三個Prototype各自都超過≥10次的要求，總數（20+10+36=66，或以「武裝+消耗」
  合計對應影遁完整循環26次計，20+10+26=56）遠超≥30的要求。
- 全程搜尋`Exception`/`NullReference`/`Unhandled`：**0命中**。
- 兩個Client皆正常連線並取得靈印裝備確認（`equipped test loadout: 1, 2, 3`）。
- 手動終止一個Client，Server正確觸發`Player left`，未崩潰。

**Data-driven驗收**（HANDOFF-007第10節要求的「改資料不改程式」測試）：
1. 基準：赤炎`Cooldown=3`（原型值），同一時間窗口（~20秒）內觸發 **14次**。
2. 只修改`赤炎.asset`的`Cooldown`欄位為`12`（純資料編輯，**未觸碰任何.cs檔案**），
   重新打包。
3. 同樣~20秒時間窗口內，觸發次數降為 **5次**——變化方向與量級皆符合預期
   （冷卻拉長4倍，觸發頻率明顯下降）。
4. 驗證完畢後已改回`Cooldown=3`（原型正式數值），並重新打包確認建置正常。

此結果直接證明：調整靈印參數只需要編輯ScriptableObject資產，`CombatController`/
`DamageService`/`SpiritSealSystem`程式碼完全不用碰，行為就會照預期改變。

### 已知問題與限制（誠實記錄，非隱藏）

1. **HP無回復機制**：Phase0-A/B從未實作HP回復，玩家HP降到0後永久停在0
   （`Mathf.Max(0, HP-amount)`）。這代表玄甲在角色第一次被「保命」之後，
   若冷卻期間又受到攻擊，HP會真的觸底停在0；冷卻結束後再次被攻擊時，
   `TryPreventFatalDamage`仍會判定為「致命」並再次觸發（因為`0-傷害>0`恆為
   假），但此時只是把已經是0的HP再次夾到`FatalSaveMinHp`附近，屬於Prototype
   簡化下的合理副作用，不是程式錯誤——正式版本需要搭配HP回復或重生機制
   才有完整意義，目前故意不做（HANDOFF-007明確禁止完整生命/重生系統）。
2. **測試裝備方式是暫時性的**：靈印在玩家出生時直接寫死裝備到固定3槽
   （`EquipTestLoadout`），沒有背包/UI/取得流程，這是HANDOFF-007第92行
   明確允許的簡化（「本階段可只開放測試用裝備／卸下功能，不需要正式背包」）。
3. **影遁的Armed狀態沒有時間限制**：目前設計是「武裝後永久有效，直到消耗或
   角色重新裝備」，沒有「武裝後N秒內必須用掉否則過期」的機制。HANDOFF-007
   沒有明確要求這點，Prototype階段先不加，正式設計時需要決定。

---

## Phase 0-D：Map / Spawn / Quest State Machine Skeleton（依`docs/HANDOFF-008_PHASE0D.md`）

> **狀態：COMPLETE（2026-09-26 本機 Unity 6000.5.5f1 實跑驗收通過）。** 結果見下方「本機實跑結果」。
> 實跑時發現 Fusion 2.1.2 的 `[Rpc]` 在 Mono 打包版不能用，接任務已改走 NetworkInput（見「已知問題」）。

### 事件鏈（Combat 與 Quest 分離）

```
CombatController / Projectile / 赤炎燃燒
        ↓
DamageService.Resolve()  ── 只在 HP 由 >0 變 0 的那一擊 ──→ CombatEvents.TargetKilled(source, target)
                                                                  ↓
                                        CombatToGameplayEventRouter（target 有 EnemyIdentity 才轉）
                                                                  ↓
                                        GameplayEvents.EnemyKilled(runner, targetId, killer, victim)
                                                                  ↓
                                        QuestTracker（擊殺者自己的那一份，TargetId 比對資料）
                                                                  ↓
                                        PlayerQuestLog（NetworkArray，同步給所有 Client）
```

- `DamageService` 只多了「發事件」這一步，傷害計算沒動；Quest 不能碰 Health／DamageService，
  怪物也不會自己改任務進度。
- 鞭屍（打 HP 已經是 0 的目標）不會重複發事件；玩家互打（沒有 EnemyIdentity）不算任務擊殺。
- 擊殺歸屬：最後一擊的玩家（組隊共享不在本階段範圍）。

### 任務狀態機

`Locked →(前置完成) Available →(Accept) Accepted →(下一個 tick) InProgress →(達成) Completed`

- 轉移表在 `QuestStateMachine`（純 C#），任務差異全部來自 `QuestDefinition` 資料，
  沒有任何 `if (questId == ...)`。
- Accept：Client 呼叫 `QuestTracker.RequestAccept` → 把 QuestNumId 放進 `PlayerInputData.QuestAcceptId`
  隨每個 tick 的輸入送出（直到同步狀態離開 Available 或 2 秒逾時）→ Server 在 `FixedUpdateNetwork`
  邊緣偵測、只處理一次 → 驗證狀態才轉移；非法請求會印 `accept REJECTED`。**不用 `[Rpc]`**，原因見「已知問題」。
- 完成時發測試獎勵（`DebugRewardPoints`），並把「前置任務 = 本任務」的 Locked 任務解鎖。
- 網路只同步 `QuestNumId / State / Progress` 三個 int，定義資料各端從 `QuestRegistry` 查。

| 測試任務 | 目標 | 前置 | 用途 |
|---|---|---|---|
| Q_PHASE0D_001 清理測試區 | Phase0D_TestMonster × 3 | 無（開局 Available） | HANDOFF 指定的主驗收任務 |
| Q_PHASE0D_002 清理測試區（續） | Phase0D_TestMonster × 5 | Q_PHASE0D_001 | 驗證 Locked → Available 解鎖 |

### 測試場景 Phase0D_TestScene（數值皆可調整）

- 兩個玩家出生點 (-1.5,1,0) 面向 +X、(1.5,1,0) 面向 -X，三個怪物出生點在中線
  (0,1,0)/(0,1,±1.2)，讓 `-autotest` 一直往前攻擊的輸入能用六種武器打到怪。
- 怪物 HP 30、死亡 0.5 秒後消失、消失 2 秒後原地重生。
- 手動遊玩（Editor Play = Host）：WASD 移動、Space/左鍵攻擊、Tab 換武器、E 測試閃避、**Q 接任務**。
- `-autotest` 時 Client 每秒自動對第一個 Available 任務送出 Accept 請求。

### 本機驗收步驟（Unity 6000.5.5f1，從 `unity/JiuyaoTianxu` 執行）

```
Unity.exe -batchmode -projectPath . -executeMethod Phase0ANetworkSetup.Run -quit   # 只有 prefab 不存在時需要
Unity.exe -batchmode -projectPath . -executeMethod Phase0DSetup.Run -quit
Unity.exe -batchmode -projectPath . -executeMethod Phase0DBuild.Build -quit
pwsh Tools/Phase0D/run_autotest.ps1 -Seconds 90
```

`run_autotest.ps1` 會開 1 Server + 2 Client（皆 `-autotest -quitafter`），約結束前 35 秒砍掉
client2 做 Join/Leave regression，最後列出：怪物生成數、EnemyKilled 數、Accept／Progress／
Completed／解鎖次數、Server 的 `PASS` 行、兩個 Client 的 `[QuestSync]` 行數、靈印 log 數、
Exception 數。**Server 的 PASS 條件**：≥2 名玩家完成 Q_PHASE0D_001，且 Progress 事件 ≥10。

### 已做的驗證（雲端環境，無 Unity）

1. **離線編譯**：Roslyn C# 9（mono）+ UnityEngine 2021.3 參考組件 + 專案內 Fusion 2.1.2 DLL，
   `_Project` 全部執行期程式碼 0 error / 0 warning。Editor 腳本除 2018 版參考組件缺少的
   `PrefabUtility` 新 API（Phase0ANetworkSetup 已在 Unity 6 用過）外皆通過。
2. **單元測試**：`Tools/QuestLogicTests` 46/46 通過（合法/非法轉移全表、擊殺只在 InProgress 計數、
   TargetId 比對、進度上限、完整 1/3→2/3→3/3→Completed、前置解鎖鏈）。

### 本機實跑結果（2026-09-26，Windows Standalone Mono，Unity 6000.5.5f1＋Fusion 2.1.2 build 2279）

**自動測試**（`run_autotest.ps1 -Seconds 150 -LockOn`，1 Dedicated Server + 2 Client 走 Photon 雲端）：

| 項目 | 結果 | 期望 |
|---|---|---|
| StartGame / Player joined / Player left | 1 / 2 / 1 | 1 / 2 / 1 |
| 怪物生成 / EnemyKilled | 79 / 76 | ≥3 / >0 |
| 接任務 / Progress 行 | 4 / 16 | ≥2 / ≥10 |
| Q_PHASE0D_001 完成 / 002 解鎖 | 2 / 2 | 2 / ≥1 |
| PASS 行 | 1 | 1 |
| Client1 / Client2 `[QuestSync]` | 25 / 23 | >0 |
| Exception / NullReference | **0** | 0 |

**手動**（Editor Play＝Host，咖哩親測）：回報「都可以按」（含 Q 接任務）。
另回報「看不出拿什麼武器」→ debug HUD 已加「武器：劍（Tab 切換）」一行。

原本的三個風險點實跑結論：Fusion 自動註冊怪物 prefab 正常；headless 下 `Render()` 有呼叫
（`[QuestSync]` 有 log）；六武器都打得到中間怪（76 次擊殺）。

### 已知問題

1. **Fusion 2.1.2 的 `[Rpc]` 在 Mono 打包版不能用**：weaver 在 RPC 方法裡插入對 `Fusion.Runtime`
   internal 方法的呼叫（`CheckInvokeRpc`、`CreateRpcBuilder`、`NotifyRpcError`、`NetworkRunnerDebugRpcEvent.*`），
   Mono 執行時拒絕 → `MethodAccessException`。`[IgnoresAccessChecksTo]` 無效（Unity 的 Mono 不認）。
   **專案目前不能新增 `[Rpc]`**；Client→Server 的請求請走 `PlayerInputData`（參考 `QuestAcceptId`）。
   升級 Fusion 或改用 IL2CPP 打包時要重新測。`[Networked]` 屬性不受影響。
2. 測試出生點很擠（玩家與怪只差 1.5m），是為了讓 `-autotest` 固定往前打就能命中，數值可調整。

---

## Phase 0-E：Roadmap Phase 0 缺口補齊（資料表／雙搖桿／目標鎖定／基礎回饋）

> **狀態：鍵盤／網路／資料表部分 2026-09-26 本機實跑通過；觸控（`-touchui`／手機）尚未測，暫不標 COMPLETE。**
> 結果見本章「本機實跑結果」。不是 ChatGPT 發的 HANDOFF，是咖哩交代「先繼續做
> 其他的」後，Claude 對照 `docs/19_DEVELOPMENT_ROADMAP_V1.0.md` Phase 0 交付／驗收項目補上尚缺的部分
> （紀錄見 `docs/00_AI_HANDOFF_BRIDGE.md` CLAUDE-NOTE-006）。全部是原型等級，數值皆可調整。

| Roadmap Phase 0 項目 | 0-A～0-D 狀態 | Phase 0-E 補上 |
|---|---|---|
| 資料驅動配置表可透過表格切換測試，不需重新編譯 | ScriptableObject，數值寫在 Editor 腳本裡 | CSV 資料表＋匯入器＋Server 執行期覆寫 |
| 戰鬥雙搖桿：移動、目標鎖定 | 只有 WASD 移動，角色不會轉向 | 瞄準搖桿、面向規則、Server 權威目標鎖定 |
| 受擊判定、基礎回饋 | 只有 log | 受擊閃紅、傷害數字、血條 |
| 手機操作 | 無 | 虛擬搖桿原型（觸控裝置自動顯示） |

### 1. 資料表（`Assets/_Project/Config/Tables/`）

- `attacks.csv`／`weapons.csv`／`spirit_seals.csv`／`quests.csv`／`monsters.csv`：**欄名＝定義類別的欄位名**，列舉填英文名稱
  （Blade、Sphere…），布林 true/false，Vector3 寫 `1;1;1`，`#` 開頭的列是註解。可以直接用 Excel／
  Google 試算表編輯（UTF-8）。初始內容由目前 commit 的資產數值轉出，**行為完全不變**。
- **Editor 匯入**：選單 `JiuyaoTianxu/Config/Import All Tables`，或
  `Unity.exe -batchmode -projectPath . -executeMethod ConfigTableImporter.ImportAll -quit`。
  依鍵（AttackId／WeaponType／SealId／QuestNumId）找到既有資產**原地更新**，GUID 與所有參照不變；
  batch 模式有任何錯誤會拋例外（exit code ≠ 0）。`Phase0BWeaponDataSetup`／`Phase0CSpiritSealDataSetup`／
  `Phase0DSetup` 的數值部分都改成呼叫匯入器，程式碼裡不再寫死數值。
- `attacks.csv` 的 `InputMode`（Tap／HoldRelease／Cast）決定該招是一般連段、按住蓄力放開發射、還是施法延遲
  （原本寫死在 CombatController 依武器判斷，技術自審 D2）。
- `monsters.csv` 寫進 `EnemyIdentity.TargetId` 相符的怪物 prefab（MaxHp、DespawnDelay；技術自審 D6），
  只在 Editor 匯入，不在 Server 執行期覆寫範圍。
- **Server 執行期覆寫（不用重新打包）**：打包後在 `<Build>_Data/StreamingAssets/ConfigOverrides/`（或
  `-configdir <路徑>`）放同名 CSV，**只要寫鍵欄＋要改的欄**，重啟 Server 即生效。限制：只改 build 內已有的
  id、只在 Server 生效（戰鬥/靈印/任務結果本來就由 Server 決定）、Editor 內不套用（避免把測試值寫回資產）。
- 驗證：`Tools/ConfigTableTests`（解析/綁定 34 項）＋`validate_tables.py`（欄名是否存在、型別、鍵重複、
  連段/前置任務參照、**CSV 與已 commit 資產是否一致**——改了 CSV 忘了匯入會被抓出來）。

### 2. 雙搖桿與目標鎖定

- `PlayerInputData` 新增 `Aim`（右搖桿）與 `LockOn` 按鍵。
- **面向（Server 決定，NetworkTransform 同步）**：瞄準搖桿 ＞ 鎖定目標 ＞ 移動方向 ＞ 維持原方向；
  轉向速度 900°/秒。攻擊仍沿用 `transform.forward`，所以面向＝攻擊方向，戰鬥框架沒改。
- **目標鎖定 `TargetLock`**：按一次鎖定最佳目標 → 再按換下一個 → 最後一個之後解除。排序：非玩家目標
  優先，其次「距離＋角度懲罰」（正前方稍遠的勝過背後較近的）；目標死亡或超過 16m 自動解除（鎖定範圍
  12m，差距避免邊界閃爍）。只同步一個 NetworkId。鎖定**不影響命中判定**，只影響面向。
- 物理查詢仍只在 `HitDetectionService`（新增 `FindHealthInRadius`）。
- 鍵盤：WASD 移動、IJKL 瞄準、F 鎖定（原有 Space/左鍵攻擊、Tab 換武器、E 閃避測試、Q 接任務不變）。

### 3. 觸控操作與基礎回饋（IMGUI 原型，不是 15_UI_UX 的正式水墨 HUD）

- `VirtualControlsOverlay`：左下固定移動搖桿；右下「攻」鍵按住＝攻擊、拖曳＝瞄準（雙搖桿）；
  閃／鎖／換／任務四個小鍵。觸控裝置自動顯示；桌機加 `-touchui` 參數可用滑鼠模擬一根手指。
  單次點擊會鎖存到下一個網路 tick，不會因為點太快而遺失。
- `HealthFeedback`＋`CombatHudOverlay`：HP 下降時閃紅、跳傷害數字；頭上血條；鎖定目標標記；
  左上 debug 任務清單（HANDOFF-008 §14 不做正式任務追蹤 UI，所以只是文字）。全部只讀取同步狀態，
  headless（batchmode）自動關閉。
- `LocalPlayerCameraFollow`：鏡頭跟著自己的角色。

### 本機驗收步驟（在 Phase 0-D 步驟之後）

```
Unity.exe -batchmode -projectPath . -executeMethod ConfigTableImporter.ImportAll -quit
Unity.exe -batchmode -projectPath . -executeMethod Phase0ESetup.Run -quit      # Phase0DSetup 已自動呼叫，可略
Unity.exe -batchmode -projectPath . -executeMethod Phase0DBuild.Build -quit
pwsh Tools/Phase0D/run_autotest.ps1 -Seconds 90 -LockOn
```

- **資料表不重新打包的驗收**：把 `Config/Tables/spirit_seals.csv` 複製到別的資料夾，只留 `SealId,Cooldown`
  兩欄、把赤炎（1）改成 12，執行 `run_autotest.ps1 -ConfigDir <那個資料夾>`，比較赤炎觸發次數是否像 0-C
  那次一樣明顯下降（0-C 當時要重新打包，這次不用）。
- **手感（需要人看）**：Editor 開 `Phase0D_TestScene` 按 Play（Host），WASD＋IJKL＋F 試面向與鎖定；
  加 `-touchui` 打包或用手機測虛擬搖桿。
- **赤炎燒怪（技術自審 D1）**：燃燒改由目標身上的 `BurnStatus` 元件處理（玩家、怪物都有），
  報表的「赤炎 burned a monster」應大於 0；同時確認 0-C 三個靈印的觸發次數仍在合理範圍。
  **一定要先跑 `Phase0DSetup`（或 `Phase0ESetup`）**，Player prefab 才會加上 BurnStatus，否則玩家之間不會燃燒。
- 預設的 `-autotest` 按鍵節奏**完全沒變**（鎖定要加 `-autotest-lockon` 才會按），Phase 0-D 驗收結果不受影響。

### 本機實跑結果（2026-09-26）

| 項目 | 一般版（`-LockOn`） | 改數值版（`-ConfigDir`，赤炎冷卻 3→12） |
|---|---|---|
| `[TargetLock]` 行 | 119 | 0（沒加 `-LockOn`，預期） |
| `[ConfigOverride] applied` | 0 | 2 |
| 赤炎觸發（其中打在怪物上） | 51（45） | **18**（7）— 明顯下降 |
| 玄甲觸發 | 4 | 9 |
| 影遁 armed／consumed | 44／44 | 40／37 |
| 玩家倒地／復活 | 2／2 | 7／7 |
| PASS／Exception | 1／0 | 1／0 |

- 改數值版**沒有重新打包**（`Assembly-CSharp.dll` 時間戳前後相同）。
- 燃燒確實扣怪物血（log：`Phase0D_TestMonster#8 took 3 damage`，呼叫來源 `BurnStatus`）。
- 玄甲少於 0-C 的 10 次是死亡重生後的預期變化（見下方「對 0-C 靈印 regression 數字的影響」）。
- 手感：咖哩 Editor 實測，回報「都可以按」（各按鍵都有反應），沒有提出數值調整。
- **未測**：`-touchui` 與手機實機的虛擬搖桿。

### 已做的驗證（雲端環境，無 Unity）

1. 離線編譯（Roslyn＋UnityEngine 參考組件＋Fusion DLL）：執行期程式碼 0 error / 0 warning；
   Editor 腳本除 2018 版參考組件缺的 `PrefabUtility` 新 API 外全部通過。
2. 單元測試：ConfigTableTests 37/37、ControlsTests 31/31、QuestLogicTests 46/46；`validate_tables.py` OK，
   並用故意改錯的 CSV 確認驗證器會抓到（錯的列舉名＋與資產不一致）。

---

## 死亡／重生（測試版，技術自審 D3＋D11）

> 咖哩 2026-09-25 同意先做測試版，讓戰鬥循環可以一直重複；**正式死亡規則（懲罰、復活點、組隊救援、
> 副本內規則）仍需 HANDOFF 定規格**。數值可調整。

- `Health.IsDead`＝HP 0。怪物照舊由 `MonsterLifecycle` 0.5 秒後消失；玩家由新的
  `Gameplay/World/PlayerLifecycle` 處理：倒地 5 秒（`_respawnSeconds`）後，在**自己第一次出生的位置**
  滿血復活（`NetworkTransform.Teleport`，Client 不會看到角色滑過整張地圖）。
- 倒地期間：`CombatController`／`PlayerMovement`／`TargetLock`／`SpiritSealSystem`（閃避測試）都不接受
  輸入（但持續記錄按鍵，復活瞬間不會誤觸），鎖定自動解除，身上的燃燒停止，模型隱藏，
  自己畫面中央顯示「倒地中…… N 秒後復活」。
- **死亡目標不再被命中**（D11）：`HitDetectionService` 的近戰／施法範圍／箭矢查詢都略過 HP 0 的目標，
  所以不會再鞭屍，箭也不會被屍體擋掉。鎖定搜尋本來就會略過死亡目標。
- 驗收：`run_autotest.ps1` 報表多了「player down」「player respawned」兩項，兩者應大致相等且大於 0
  （0-A 場景的玩家互打、0-D 場景的槍/杖波及都會打倒對方）。

### ⚠️ 對 0-C 靈印 regression 數字的影響

0-C 當時玩家 HP 會一直停在 0，玄甲在 HP 0 時會反覆觸發（0-C README「已知問題 1」）。現在 HP 0 的玩家
會倒地、不能被打，**玄甲的觸發次數會比 0-C 那次少**，屬預期變化，不是退步。若要湊滿 HANDOFF-007 的
「每個靈印 ≥10 次」，把測試時間拉長（例如 `run_autotest.ps1 -Seconds 150`）。0-C 已知問題 1 同時解除。

## Photon App ID 設定（每台開發機都要做一次，不進版本控制）

`Assets/Photon/Fusion/Resources/PhotonAppSettings.asset` 這個檔案**已被gitignore**，
不會進git——因為它明文存放App ID，公開在GitHub上會讓任何人用我們的免費CCU額度。

Fusion套件本身有自動偵測機制：專案裡若缺這個檔案，Unity開啟或批次模式執行時
會自動生成一份空白的（`AppIdFusion`欄位是空的）。**每台新的開發機／每次重新clone
專案後，都需要手動把App ID填進這個檔案的`AppIdFusion`欄位**，來源見
`local-secrets/photon-app-id.txt`（同樣已gitignore，只在本機留一份備份）。

當前App ID已在本機填好並驗證可編譯通過，Network Runner連線測試（步驟3）尚未執行。
