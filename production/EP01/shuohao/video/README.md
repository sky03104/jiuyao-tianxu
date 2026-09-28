# EP01 影片生成試作

> 2026-09-27 起。目的：先用已驗圖通過的 4 張分鏡圖，測「AI 圖生影片能不能保住本作畫風與角色」，再決定正式工具。

## 工具評估（2026-09-27 查證）

| 工具 | 費用 | 商用 | 評估 |
|---|---|---|---|
| **可靈 Kling 免費版** | 每天 66 點（當天用完就沒了，不累積） | ❌ 有浮水印、無商用授權 | **先用來測品質**：最快看到結果，只當內部試片 |
| Wan 2.2（Wan2GP）在 **Kaggle 免費 T4** | 免費（每週約 30 小時 GPU） | ✅ Apache 2.0 可商用 | 正式量產候選；480p 起步、一段 5 秒約 20～30 分鐘；需要設定筆記本 |
| Wan2GP 本機 | 免費 | ✅ | **不可行**：本機 GTX 1660 Ti 6GB 跑 SDXL 單張就超過 40 分鐘，影片模型更慢 |
| Higgsfield／Seedance 付費 | 月費或按秒計 | ✅ | 見 `production/shorts/README.md`；確認品質後再考慮 |

## 做法：一個分鏡切一段影片

H3 的「多鏡頭一次生成」提示詞只有 MiniMax H3 能用。可靈、Wan 都是**一張圖生一段**，所以改成**每個分鏡切各生一段 5 秒**，剪輯時再裁成分鏡表的秒數（`animatic/` 的剪輯程式會接手）。

## 試作清單（照順序）

存檔位置：本資料夾，檔名 `<段號>_c<切>.mp4`。

| # | 檔名 | 起始圖 | 用到秒數 | 提示詞（英文，整段貼） |
|---|---|---|---|---|
| 1 | `E01-02_c1.mp4` | `storyboard/export/h3/E01-02/f1.png` | 3 秒 | The young man in a charcoal-grey robe walks briskly away from the camera down the long wooden corridor, one step slightly quicker than the other, glancing at the doors on his right; pillar shadows slide past on the stone floor; the camera follows him from behind at shoulder height with a slight handheld sway. Keep his face, hair, clothing and backpack exactly as in the image. Cinematic, smooth, natural motion. |
| 2 | `E01-01_c1.mp4` | `storyboard/export/h3/E01-01/f1.png` | 3 秒 | Slow cinematic push-in toward the mountain academy. A band of white cloud drifts slowly across the middle of the mountain, banners stir in the morning wind, soft morning light. No people. Buildings stay solid and unchanged. |
| 3 | `E01-01_c2.mp4` | `storyboard/export/h3/E01-01/f2.png` | 2 秒 | Static camera. The two deep-blue banners sway gently in the morning wind, mist drifts slowly behind the gatehouse, lanterns hang still. Nothing else moves. No people. |
| 4 | `E01-02_c2.mp4` | `storyboard/export/h3/E01-02/f2.png` | 2 秒 | The young man walks toward the camera down the corridor holding the jade token in his right hand; the other students walk past him in the opposite direction; morning light; the camera slowly moves backward. Keep every face and costume exactly as in the image. |

可靈設定：圖生影片、5 秒、標準模式、**反向提示詞**貼：`text, subtitles, watermark text, morphing face, extra limbs, distorted hands, changing costume, flickering`。

**先做第 1 支**：角色走路最容易看出臉和服裝會不會跑掉，這支過了再做其他。

## 驗收重點

- 臉、髮型、服裝、行囊在整段影片裡不變形、不換人
- 建築不融化、不扭曲
- 動作自然（走路節奏、旗子擺動）
- 沒有多出文字

## 試作紀錄

| 檔案 | 工具 | 結果 | 備註 |
|---|---|---|---|
| （尚未生成） | | | |
