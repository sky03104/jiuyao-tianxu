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
  Core/      — 底層通用工具、依賴注入、事件總線（呼應17_TECH_ARCHITECTURE）
  Net/       — Photon Fusion封裝、NetworkRunner狀態同步接合層
  Combat/    — 純表現層：特效/動畫/受擊反饋，不做傷害計算
  UI/        — 介面與HUD
  Economy/   — 經濟資料展示層
  Guild/     — 公會資料展示層
  Config/    — 資料驅動配置讀取模組
  Art/       — 美術資源
  Scenes/    — 場景（Phase0A_NetworkTest.unity 為本階段測試場景）
  Editor/    — 編輯器/一次性設定工具（Phase0ASetup.cs）
  Settings/  — URP Pipeline Asset 等專案設定資產
```

## 如何開啟專案
1. 用 Unity Hub 開啟 `unity/JiuyaoTianxu` 資料夾（需要 Unity 6000.5.5f1，或相容版本）。
2. 開啟 `Assets/_Project/Scenes/Phase0A_NetworkTest.unity`。
3. 目前場景僅含地板/光源/攝影機（URP渲染管線已設定），尚未接上Fusion連線與戰鬥邏輯。

## Phase 0-A 進度（依 `docs/HANDOFF-005_PHASE0A.md` 執行順序）

| 步驟 | 狀態 |
|---|---|
| 1. Unity + URP 專案建立 | ✅ 完成 |
| 2. Photon Fusion 套件／版本確認 | ✅ 完成（2.1.2 stable，編譯通過無錯誤） |
| 3. Network Runner 基礎連線 | 待做 |
| 4~15 | 待步驟3接續 |

## Photon App ID 設定（每台開發機都要做一次，不進版本控制）

`Assets/Photon/Fusion/Resources/PhotonAppSettings.asset` 這個檔案**已被gitignore**，
不會進git——因為它明文存放App ID，公開在GitHub上會讓任何人用我們的免費CCU額度。

Fusion套件本身有自動偵測機制：專案裡若缺這個檔案，Unity開啟或批次模式執行時
會自動生成一份空白的（`AppIdFusion`欄位是空的）。**每台新的開發機／每次重新clone
專案後，都需要手動把App ID填進這個檔案的`AppIdFusion`欄位**，來源見
`local-secrets/photon-app-id.txt`（同樣已gitignore，只在本機留一份備份）。

當前App ID已在本機填好並驗證可編譯通過，Network Runner連線測試（步驟3）尚未執行。
