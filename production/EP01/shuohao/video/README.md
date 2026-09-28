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
| `storyboard/export/h3/E01-01/kling-v1.mp4` | 可靈網頁版 VIDEO 3.0 Omni（f1＋f2，一次生成含切鏡） | 通過（內部試片） | 36 點；約 2.8 秒切鏡 |
| `storyboard/export/h3/E01-01/wan22-test-shot1.mp4`、`-shot2.mp4` | Wan 2.2 TI2V-5B（Kaggle T4，480p） | 鏡 1 勉強、鏡 2 不合格 | 每鏡 3 秒約 10.6 分鐘；建築融化、旗幟變形 |
| `storyboard/export/h3/E01-02/kling-v1.mp4` | 可靈網頁版 VIDEO 3.0 Omni（f1＋f2＋玩家設定圖） | 通過（內部試片） | 36 點；走路自然、臉一致；路人同向走（非擦肩） |
| `storyboard/export/h3/E01-02/wan22-test-shot1.mp4`、`-shot2.mp4` | Wan 2.2 TI2V-5B（Kaggle T4，480p） | 鏡 1 通過、鏡 2 勉強 | 走路自然；鏡 2 臉後段漂移、憑證疑似換手 |
| `storyboard/export/h3/E01-02/jimeng-v1.mp4` | 即夢 Dreamina（AI Agent＋Auto，f1～f3＋玩家設定圖） | 部分可用 | 12 點；一鏡到底、約 2.9 秒人物原地翻轉；各段本身品質好、門牌無字 |

> **2026-09-28 修正上面的「一個分鏡切一段」做法**：可靈 3.0 Omni 可掛多張參考圖、在提示詞寫「第 N 秒硬切到 @圖片2」，
> 一段內多鏡一次生成可行（實測切點會提早 0.2～0.7 秒）。人物鏡頭加掛角色設定圖當最後一張參考。
> 可靈免費點數只能在網頁／App 用，Kling MCP 連接器（API）點數為 0。詳細驗片紀錄見 `../DECISIONS.md`。
