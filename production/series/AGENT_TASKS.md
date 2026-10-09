# 主持人派工範本（階段 2、3）

> 審查迴圈由主持人執行（子 agent 沒有派 agent 的工具）：作者寫完回報 → 主持人派審查員 → 用 SendMessage 把意見傳回作者修改 → 再派新審查員。
> 給主持人（Claude）用：分集指南過關後，照這裡的範本派 agent。把 {EP}、{季}、{上一集}、{下一集} 換成實際值。
> 同一季可以平行寫，但每位作者都要先讀前一集（如果已經寫好）。

---

## 階段 2：劇本作者（general-purpose）

你是《九曜：天墟》長篇動畫的編劇，負責 **{EP}**。

先讀：
- `production/series/AGENT_BRIEF.md`、`production/series/README.md`（劇本格式在第 4 節，要嚴格照寫）、`production/roles/01_編劇.md`；
- `docs/longform/{季}_GUIDE.md` 裡 {EP} 那一集（分場表、秒數、前情提要、下集預告都以它為準）；
- 上一集劇本 `docs/longform/{上一集}_SCRIPT.md`（如果有）；
- 指南列出的章節原稿：第 1 章 `docs/25`／`43`，第 2 章 `26`，第 3 章 `27`，第 4～6 章 `40`～`42`，其餘 `36`～`39`；
- `docs/21`（揭露上限）、`docs/47`、`docs/46`、`docs/02`；
- 第一章另外讀 `novel/vol01/`，有現成對白就優先沿用。

寫出 **`docs/longform/{EP}_SCRIPT.md`**：
- 結構：
  - 開頭：前情提要（EP01 是世界開場旁白）；
  - 冷開場（如果有）；
  - 正片各場：場次標題寫「約 m:ss」，用指南的秒數；
  - 結尾：下集預告（全文）。
  - 文件最後附「本集台詞變更說明」：和原稿比，改了哪些、刪了哪些、★改了哪些、為什麼。
- 台詞密度約每分鐘 10～14 句，總句數大約 160～200。
- **每一句台詞都要能回答兩個問題**：「他在對誰說？」「為什麼現在說？」
- 動作描述（△）只寫看得見、AI 影片做得出來的東西：
  - 不寫肢體接觸，撞人、扶人、擁抱、拍肩一律改成「反應鏡頭＋音效＋剪接」；
  - 一場同框最多 4～5 人；
  - 畫面上不能出現文字。

審查迴圈（一定要做，直到過關）：
寫完後，同時派兩個審查員：
1. `script-reviewer`：審台詞與邏輯；
2. general-purpose 的「設定審查員」：對照 `docs/21` 揭露上限、`docs/47`、`docs/48`／季指南、前一集劇本，查設定、時間線、角色外觀和稱呼的一致性，還有指南的每一場是否都有寫到。

兩份意見都改完後，再派**新的**審查員審，直到兩邊都「結論：過關」，最多 5 輪。
每輪摘要附加到 `production/series/{EP}/review.md`。需要咖哩決定的事寫進 `production/series/QUESTIONS.md`。

最後回報（200 字內）：總句數、各場秒數、審查輪數、是否過關、對指南做了哪些偏離（以及理由）。

---

## 階段 3：片段表作者（general-purpose）

你是《九曜：天墟》長篇動畫的分鏡與 AI 影片提示詞負責人，負責 **{EP}**。

先讀：
- `production/series/AGENT_BRIEF.md`；
- `production/series/README.md` 第 5、6 節（clips.json 格式與 Seedance 規則）；
- `production/series/registry.json`；
- 已過關的劇本 `docs/longform/{EP}_SCRIPT.md`；
- 參考範例：`production/LF01/video/scene02_seedance_prompts_part3.md`（實際驗證過的提示詞長相）。

寫出 **`production/series/{EP}/clips.json`**：
- 每支片段 4～12 秒，最多 3 人，只在一個地點。正片的 `dur` 加總要在 930～990 秒（就是劇本的片長）。
- 劇本正片的**每一句台詞都要依序放進片段的 `say`**，一字不差。前情提要的旁白與預告台詞不用放。
- `beats` 寫到逐秒：
  - 每段寫鏡頭（景別）、看得見的動作與表情、誰說話；
  - 說話速度不可超過每秒 4.5 個字，台詞長就拉長 beat；
  - 對話盡量一支片段放 2～3 句，讓嘴型和節奏自然。
- `mode`：日常戲用 `ref`。大場面、怪物、特效、多人站位複雜、需要精準構圖的鏡頭用 `firstframe`，這時要寫 `firstframe` 欄（關鍵幀畫面描述，寫清楚誰在哪、朝哪、光線）。
- `sfx`、`music`：列出這支片段需要的音效與配樂情緒，之後做音效設計用。
- 新角色或新場景：**不要改 `registry.json`**，寫進 `production/series/{EP}/registry_add.json`（格式同 registry.json 的 characters／locations）。
  - 編號用「集數＋流水號」：角色 `{EP}C1`、`{EP}C2`……，場景 `{EP}L1`……。
  - 每筆要寫 `name`、`label`（例如「沈昭寧-sheet」）、`seedance`（中文外觀）、`sheet: null`、`firstEp`、`sheetBrief`。
  - 外觀以 `docs/46`、`docs/02` 和季指南為準。
  - 新增前先查 registry.json 和其他集的 registry_add.json，同一個角色或場景已經登記過的話，直接用那個編號。

完成後跑 `python3 production/series/tools/series.py check {EP}`，改到沒有 ✗，再跑 `build {EP}`。

審查迴圈（一定要做，直到過關）：
派一個 general-purpose 的「提示詞審查員」，對照劇本逐支看 `production/series/{EP}/seedance_prompts.md`：
- 畫面有沒有忠實演出劇本；
- 每支片段的人物、站位、動作是否前後連戲（例如誰拿著什麼、誰在門內）；
- AI 能不能生成（人數、接觸、文字、同一支片段裡換地點）；
- 角色外觀與 registry 是否一致；
- 說話的人是否在畫面上；
- 心聲和畫外音有沒有標對。
依意見修改，再派新的審查員審，直到「結論：過關」，最多 5 輪。每輪摘要附加到 `production/series/{EP}/review.md`。

最後回報（150 字內）：片段數、正片秒數、firstframe 幾支、新登記的角色和場景、審查輪數、是否過關。
