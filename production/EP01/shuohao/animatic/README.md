# EP01 動態分鏡（animatic）

> 狀態：**v0**（2026-09-27）— 分鏡圖 4/30，其餘 26 切是字卡。用途是在花錢生影片之前先看整集 80 秒的節奏，
> 不是成品。

## 內容

- 時間軸完全由 `storyboard/EP01-storyboard.json`（切點、秒數、景別、運鏡、畫面描述）＋
  `script/EP01-script.json`（台詞、說話人）產生，**不手寫時間**；分鏡表改了重跑即可。
- **有分鏡圖的切**：依運鏡做推鏡（Push In）／跟拍（Tracking，含 slight-shake 手持感）／固定（極慢微推）。
- **沒分鏡圖的切**：以該場景設定圖模糊打暗當底，字卡寫段號、景別、運鏡、秒數、畫面描述、人物道具、鏡頭說明。
- 字幕照劇本台詞，上方小字標說話人（畫外音標「（畫外）」）；同一切有兩句台詞時平分該切秒數。
- 上方進度條：金色＝已有分鏡圖、灰色＝待補。
- 配樂是程式合成的**暫定音軌**（`audio.mjs`，依分鏡表各段 music 欄：古琴單音 → 木質打擊 → 弦樂 → E01-07 抽掉配樂
  只剩低頻＋沉重腳步 → 最後一聲敲擊、靜音、鐘響），正式配樂另做。沒有配音。

## 補圖後重新輸出

1. 分鏡圖照 `docs/HANDOFF-009_CHATGPT_WEB_STORYBOARD.md` 出圖、驗圖（記進 `../DECISIONS.md`），
   存成 `storyboard/export/h3/<段號>/f<序>.png`（例：`E01-03/f2.png`）。
2. 重新輸出（build → audio → 逐格擷取 → ffmpeg，約 4 分鐘）：

```bash
cd production/EP01/shuohao/animatic
ln -s "$(npm root -g)" node_modules      # 使用全域 playwright（node_modules 不進版控）
node render.mjs --stills 1,16,40          # 先看幾格
node render.mjs                           # → EP01_animatic_v0.mp4
```

需要 Node 22、Playwright、ffmpeg、Noto CJK 字型（`apt-get install ffmpeg fonts-noto-cjk`）。
直接用瀏覽器開 `index.html` 可即時預覽（無聲；Chrome 需加 `--allow-file-access-from-files` 才讀得到本機圖片）。

## 之後換成影片片段

影片模型產出每段的片段後，`index.html` 的 `drawShot` 改成畫 `<video>` 當前格即可，字幕、配樂、門牌後製沿用同一套。
「第七室」門牌字（E01-02 #3、E01-09 #2）依規定剪輯後製，等那兩格有圖、確定門牌位置後再加疊字。

## 已知限制

- 程式只能對靜態圖做推拉搖移，角色本身不會動；要角色動起來必須靠影片模型。
- MP4 每次重出都會進 git 歷史（約 14MB），只在分鏡圖有明顯進度時才重出並 commit。
