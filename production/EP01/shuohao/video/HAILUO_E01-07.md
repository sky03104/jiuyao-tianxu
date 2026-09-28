# 海螺 MiniMax H3：E01-07 蕭曜霖登場（整段 5 鏡）

> 2026-09-28，依 `PLAN.md` 排程。E01-07 還沒有分鏡圖，改掛**角色與場景設定圖**當參考（H3 全能參考最多 12 張），
> 提示詞＝`storyboard/export/h3/E01-07/prompt.md` 的 [Shot k] 切點原文，把「Picture k 對齊秒數」改成「Picture 代表誰／哪裡」。
> 帳號剩 94 點（免費方案只有一次性點數），這支約 91 點，**只跑這一次**。

## 設定

| 項目 | 值 |
|---|---|
| 模型／模式 | **MiniMax H3**、**全能參考** |
| 解析度 | **768p** |
| 長度 | **13 秒**（分鏡 12.5 秒） |
| 比例 | **9:16** |
| 送出前 | 看點數：**超過 94 就把長度改 12 秒**，並把提示詞 [Shot 5] 的 `00:09.500` 維持不變（最後一鏡短一點沒關係） |

## 參考圖（依序上傳，第 1～7 張）

| 順序 | 檔案（`production/EP01/shuohao/` 底下） |
|---|---|
| 1 | `art/images/天玄院東廊-sheet.png` |
| 2 | `art/images/第七室-sheet.png` |
| 3 | `characters/images/蕭曜霖-sheet.png` |
| 4 | `characters/images/玩家-sheet.png` |
| 5 | `characters/images/齊衡烈-sheet.png` |
| 6 | `characters/images/郁岑燁-sheet.png` |
| 7 | `characters/images/厲若楓-sheet.png` |

## 提示詞（整段貼上）

```
Reference pictures: Picture 1 is the long academy corridor (the east corridor) — use its architecture, pillars, stone floor and morning light. Picture 2 is the dormitory room called Room Seven — its doorway opens onto the corridor. Picture 3 is the instructor: keep his face, colossal bronze pauldrons and the huge heavy sword on his back exactly as in Picture 3. Picture 4 is the newcomer (charcoal-grey robe, rolled travel bundle). Picture 5 is the red-haired young man with a bronze breastplate and a red-gold armband on his right upper arm. Picture 6 is the calm young swordsman in a dark-blue robe. Picture 7 is the quiet young archer. Keep every face, hairstyle and costume exactly as in its reference picture. Vertical 9:16 frame throughout.

integrated_multimodal_description:
[Shot 1] A wide static shot from the doorway of Room Seven looking down the long corridor of Picture 1, which recedes toward a far end glowing with backlit morning haze; no one is visible yet and the ambience drops away, as a mature man with a deep, dense, slow voice (S1) says in an off-screen voiceover, no one on screen speaking: <d>[Chinese] 不用等明天。</d>
[Shot 2] At 00:02.000, the camera cuts to a medium static shot from the side of the corridor looking at the doorway of Room Seven: the red-haired young man (Picture 5) and the quiet archer (Picture 7) at the doorway turn their heads toward the corridor at the same moment, the swordsman (Picture 6) looks up from a low table further inside, and the newcomer (Picture 4) at the threshold looks back too; mostly profiles and backs, no one speaks.
[Shot 3] At 00:04.000, the camera cuts to a wide shot: at the far end of the corridor the instructor (Picture 3) stands completely still in the backlight, the outline of his bronze pauldrons rimmed with morning light, the hilt of the heavy sword rising above his right shoulder; the camera makes a slow push in toward him.
[Shot 4] At 00:06.500, the camera cuts to a medium static shot of the instructor standing in the corridor, stern in the backlight, a faint pale-gold mark visible along his brow ridge; he fixes a steady gaze on the doorway and, without raising his voice, he (S1) announces slowly, word by word: <d>[Chinese] 半刻鐘後，新生演武場集合。</d>
[Shot 5] At 00:09.500, the camera cuts to a wide static shot: the instructor turns and walks away down the backlit corridor at an unhurried pace, the heavy sword swaying slightly on his back, until he is small in the haze.

Style: premium Eastern fantasy 3D animated film look matching the reference pictures, semi-realistic, not live-action, not 2D anime. No subtitles, no text anywhere.

overall_soundscape: Ambient sound drops noticeably just before the voice, then heavy, measured footsteps on stone recede down the corridor.

non_diegetic_music: The music drops out, leaving only a low sustained string drone.
```

## 交給 Claude

存成 `storyboard/export/h3/E01-07/hailuo-h3-v1.mp4`。驗片重點：切點 2.0／4.0／6.5／9.5 秒；蕭曜霖的青銅巨鎧、背後重劍劍柄在**右肩**、眉骨淡金曜痕；
第 2 鏡四人是否都對（齊衡烈臂環在右上臂）；無字幕。台詞聲音之後換成配音，只看嘴型是否在第 4 鏡開口。
