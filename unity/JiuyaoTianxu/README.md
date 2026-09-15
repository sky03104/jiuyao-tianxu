# 《九曜：天墟》Unity 專案 — Phase 0-A

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
  Net/       — NetworkGameLauncher（Fusion啟動/回呼）、Net/Prefabs/（Player, NetworkRunner）
  Combat/    — Server權威戰鬥表現層：Health/PlayerMovement/PlayerCombat
  UI/        — 介面與HUD（尚未使用）
  Economy/   — 經濟資料展示層（尚未使用）
  Guild/     — 公會資料展示層（尚未使用）
  Config/    — 資料驅動配置讀取模組（尚未使用）
  Art/       — 美術資源（尚未使用）
  Scenes/    — 場景（Phase0A_NetworkTest.unity 為本階段測試場景）
  Editor/    — 一次性設定工具（Phase0ASetup/Phase0ANetworkSetup/Phase0ABuild）
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

## Photon App ID 設定（每台開發機都要做一次，不進版本控制）

`Assets/Photon/Fusion/Resources/PhotonAppSettings.asset` 這個檔案**已被gitignore**，
不會進git——因為它明文存放App ID，公開在GitHub上會讓任何人用我們的免費CCU額度。

Fusion套件本身有自動偵測機制：專案裡若缺這個檔案，Unity開啟或批次模式執行時
會自動生成一份空白的（`AppIdFusion`欄位是空的）。**每台新的開發機／每次重新clone
專案後，都需要手動把App ID填進這個檔案的`AppIdFusion`欄位**，來源見
`local-secrets/photon-app-id.txt`（同樣已gitignore，只在本機留一份備份）。

當前App ID已在本機填好並驗證可編譯通過，Network Runner連線測試（步驟3）尚未執行。
