# 海螺 Hailuo（MiniMax H3）影片試作：E01-02 机遙走東廊

> 2026-09-28。hailuoai.video 可直接選 **MiniMax H3**＋**全能參考**（最多 12 張參考圖）。帳號 150 點，預設 2K／5 秒一支 60 點。
> 提示詞＝`storyboard/export/h3/E01-02/prompt.md` 原文（分鏡程式本來就是為 H3 寫的：圖片對齊秒數＋[Shot k] 切點），
> 只補三處：①第 4 張參考圖＝玩家設定圖鎖臉 ②路人走位寫死（可靈、即夢都拍成同向走）③憑證外觀。
> 同一段已有可靈（`kling-v1.mp4`）、即夢（`jimeng-v1.mp4`）、Wan 版本，這支用來比「誰最守分鏡切點」。

## 設定

| 項目 | 值 |
|---|---|
| 模型 | **MiniMax H3**（輸入框下方第一顆） |
| 模式 | **全能參考** |
| 解析度 | 選 **1080p 或 768p**（比 2K 省點；看按鈕上點數，150 點內能跑 2 支最好） |
| 長度 | **8 秒**（分鏡 7.5 秒；沒有 8 就選不少於 7 秒的最短一檔。只有 5／6 秒就選 6，並刪掉【Shot 3】） |
| 比例 | **9:16**（預設是 21:9，一定要改） |

## 參考圖（依序上傳，第 1～4 張）

| 順序 | 檔案 |
|---|---|
| 1 | `storyboard/export/h3/E01-02/f1.png`（背影走東廊，首幀） |
| 2 | `storyboard/export/h3/E01-02/f2.png`（正面走來＋路人） |
| 3 | `storyboard/export/h3/E01-02/f3.png`（過肩看門牌） |
| 4 | `characters/images/玩家-sheet.png`（机遙設定圖） |

## 提示詞（英文原文整段貼上；介面若要求用 @ 指定圖片，把 `<Picture N>` 換成 @ 選第 N 張）

```
How the reference pictures align with the target video — Picture 1 (from Shot 1) aligns with the 0.00-second mark of the target video; Picture 2 (from Shot 2) aligns with the 3.00-second mark of the target video; Picture 3 (from Shot 3) aligns with the 5.00-second mark of the target video. Picture 4 is the character model sheet of the newcomer: keep his face, hair, charcoal-grey robe and rolled travel bundle exactly as in Picture 4 in every shot.

integrated_multimodal_description:
[Shot 1] Following <Picture 1>, a young newcomer in a charcoal-grey robe with a rolled travel bundle on his back strides down a long academy corridor, an admission token gripped in his right hand, his steps slightly uneven as he glances at the doors on his right; a tracking shot follows him from behind at below-shoulder height.
[Shot 2] At 00:03.000, the camera cuts to <Picture 2>: the newcomer walks toward the camera; two or three academy students come from the far end of the corridor walking in the OPPOSITE direction, pass him in the middle of the frame and exit past the bottom of the frame — they do not walk alongside him; the camera holds a static shot as distant training shouts echo.
[Shot 3] At 00:05.000, the camera cuts to <Picture 3>: the newcomer stops before a dark wooden door, looks up at the small blank upright plaque beside the lintel, then down at the token in his hand; the camera makes a slow push in over his right shoulder toward the plaque.

The admission token is a palm-sized upright rectangular tablet with an antique bronze frame and a pale celadon jade face carved with a round academy emblem, tied with a dark-blue silk cord — not paper, not a letter, no writing on it. The plaque has no writing. Style: premium Eastern fantasy 3D animated film look matching the reference pictures, semi-realistic, not live-action, not 2D anime. No subtitles, no text anywhere. No dialogue.

overall_soundscape: Quick, slightly uneven footsteps tap on stone slabs, robes rustle, other students' footsteps pass by, and faint training shouts carry from a distant courtyard; the footsteps stop at a door.

non_diegetic_music: Light wooden percussion joins at a moderate tempo.
```

## 交給 Claude

影片下載傳進對話，存成 `storyboard/export/h3/E01-02/hailuo-h3-v1.mp4`。驗片重點：**3.00／5.00 秒切點準不準**、
路人是否迎面錯身、臉與設定圖、憑證外觀、門牌無字；並跟可靈、即夢、Wan 版本並排比較，結論記進 `../DECISIONS.md`。
