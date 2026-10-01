# 海螺 MiniMax H3：E01-07 #4～5 蕭曜霖開口、轉身走遠（兩鏡）

> 2026-09-28 改版：EP01 改漫劇式（見 `PLAN.md`），E01-07 #1～3（空走廊、三人回頭、推向逆光教官）改用分鏡靜態圖＋運鏡，
> 海螺只跑需要動作的 #4、#5。原本整段 5 鏡約 91 點，改成 6 秒約一半，剩下的點數留給之後的關鍵鏡。
> 帳號剩 94 點（免費方案只有一次性點數）。**只跑這一次。**
> 最好先有 ChatGPT 出的 E01-07 分鏡圖 f4（當首幀）；還沒有就只掛設定圖。

## 設定

| 項目 | 值 |
|---|---|
| 模型／模式 | **MiniMax H3**、**全能參考** |
| 解析度 | **768p** |
| 長度 | **6 秒**（沒有 6 就選不少於 6 秒的最短一檔） |
| 比例 | **9:16** |

## 參考圖（依序上傳）

| 順序 | 檔案（`production/EP01/shuohao/` 底下） |
|---|---|
| 1 | `storyboard/export/h3/E01-07/f4.png`（有的話；沒有就跳過，下面編號往前遞補，提示詞第一句刪掉） |
| 2 | `characters/images/蕭曜霖-sheet.png` |
| 3 | `art/images/天玄院東廊-sheet.png` |

## 提示詞（整段貼上）

```
Picture 1 is the first frame of the video. Picture 2 is the instructor: keep his face, colossal bronze pauldrons and the huge heavy sword on his back (hilt above his right shoulder) exactly as in Picture 2. Picture 3 is the long academy corridor — use its architecture, pillars, stone floor and backlit morning haze. Vertical 9:16 frame throughout.

integrated_multimodal_description:
[Shot 1] A medium static shot of the instructor (Picture 2) standing in the corridor of Picture 3, stern in the backlight, a faint pale-gold mark visible along his brow ridge; he fixes a steady gaze toward a doorway just off-screen and, without raising his voice, he (S1, a mature man with a deep, dense, slow voice) announces slowly, word by word: <d>[Chinese] 半刻鐘後，新生演武場集合。</d>
[Shot 2] At 00:03.000, the camera cuts to a wide static shot: the instructor turns and walks away down the backlit corridor at an unhurried pace, the heavy sword swaying slightly on his back, until he is small in the haze.

Style: premium Eastern fantasy 3D animated film look matching the reference pictures, semi-realistic, not live-action, not 2D anime. No subtitles, no text anywhere.

overall_soundscape: Near silence, then heavy, measured footsteps on stone recede down the corridor.

non_diegetic_music: A low sustained string drone.
```

## 交給 Claude

存成 `storyboard/export/h3/E01-07/hailuo-h3-v1.mp4`。驗片重點：切點 3.0 秒；蕭曜霖的青銅巨鎧、背後重劍劍柄在**右肩**、眉骨淡金曜痕；
無字幕。台詞聲音之後換成配音，只看嘴型是否在第 1 鏡開口。clips.json：#4 取 0～3 秒、#5 取 3～6 秒。
