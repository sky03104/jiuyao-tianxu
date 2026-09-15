# 《九曜：天墟》Unity 專案 — Phase 0-A

## 版本資訊
- Unity Editor：**6000.5.5f1**
- URP：**17.5.0**（隨此 Editor 版本內建）
- Photon Fusion：**尚未安裝**（Fusion 2 SDK 需從 Photon 帳號的 SDK 下載頁面手動取得
  `.unitypackage`，無法透過 Package Manager 直接安裝，見下方「已知阻塞」）

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
| 2. Photon Fusion 套件／版本確認 | ⚠️ **阻塞**，見下方 |
| 3~15 | 待步驟2解除阻塞後接續 |

## 已知阻塞

**Photon Fusion 2 SDK 無法透過程式碼或Package Manager直接取得。**

Fusion 2 SDK 是以 `.unitypackage` 檔案形式發佈，需要登入 Photon Engine 帳號到
SDK下載頁面（https://doc.photonengine.com/fusion/v2/getting-started/sdk-download）
手動下載，這一步只能由使用者（咖哩）操作，Claude Code 無法代為取得帳號或下載檔案。

**目前狀態**：
- Photon App ID 已取得，存放於 `local-secrets/photon-app-id.txt`（已gitignore，
  不會進版本控制），待Fusion套件匯入後會遷移進正式的 `PhotonAppSettings` 資產。
- Fusion 2 `.unitypackage` 檔案尚未取得，等使用者下載後放入專案資料夾，
  即可用 `Unity.exe -batchmode -importPackage <path> -quit` 匯入。

依 HANDOFF-005 第0-A-02節規則：**遇到此類外部設定阻塞須明確記錄，不得用假資料
宣稱完成**——本README即為該記錄。
