# 《九曜：天墟》Unity 專案 — Phase 0-A / 0-B

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
  Combat/    — PlayerMovement、Health（Server權威狀態持有者）
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
  UI/        — 介面與HUD（尚未使用）
  Economy/   — 經濟資料展示層（尚未使用）
  Guild/     — 公會資料展示層（尚未使用）
  Config/    — 資料驅動配置讀取模組（尚未使用）
  Art/       — 美術資源（尚未使用）
  Scenes/    — 場景（Phase0A_NetworkTest.unity 為本階段測試場景）
  Editor/    — 一次性設定工具（Phase0ASetup/Phase0ANetworkSetup/Phase0BWeaponDataSetup/Phase0ABuild）
  Settings/  — URP Pipeline Asset 等專案設定資產
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

## Photon App ID 設定（每台開發機都要做一次，不進版本控制）

`Assets/Photon/Fusion/Resources/PhotonAppSettings.asset` 這個檔案**已被gitignore**，
不會進git——因為它明文存放App ID，公開在GitHub上會讓任何人用我們的免費CCU額度。

Fusion套件本身有自動偵測機制：專案裡若缺這個檔案，Unity開啟或批次模式執行時
會自動生成一份空白的（`AppIdFusion`欄位是空的）。**每台新的開發機／每次重新clone
專案後，都需要手動把App ID填進這個檔案的`AppIdFusion`欄位**，來源見
`local-secrets/photon-app-id.txt`（同樣已gitignore，只在本機留一份備份）。

當前App ID已在本機填好並驗證可編譯通過，Network Runner連線測試（步驟3）尚未執行。
