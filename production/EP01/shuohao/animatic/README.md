# EP01 動態分鏡（animatic）

> 狀態：**v1**（2026-09-28）`EP01_animatic_v1.mp4` — **生成影片 4 切**（E01-01 #1～2、E01-02 #1～2，可靈 3.0 Omni）＋
> **分鏡圖 2 切**（E01-02 #3、E01-03 #1），其餘 24 切是字卡。用途是看整集 80 秒的節奏，不是成品。
> v0（2026-09-27，分鏡圖 4/30、無影片）保留為 `EP01_animatic_v0.mp4`。

## 內容

- 時間軸完全由 `storyboard/EP01-storyboard.json`（切點、秒數、景別、運鏡、畫面描述）＋
  `script/EP01-script.json`（台詞、說話人）產生，**不手寫時間**；分鏡表改了重跑即可。
- **有分鏡圖的切**：依運鏡做推鏡（Push In）／跟拍（Tracking，含 slight-shake 手持感）／固定（極慢微推）。
- **沒分鏡圖的切**：以該場景設定圖模糊打暗當底，字卡寫段號、景別、運鏡、秒數、畫面描述、人物道具、鏡頭說明。
- 字幕照劇本台詞，上方小字標說話人（畫外音標「（畫外）」）；同一切有兩句台詞時平分該切秒數。
- **有生成影片的切**：依 `clips.json` 取影片片段（檔案、起訖秒數），等速拉伸填滿分鏡秒數；左上角標出來源工具。
- 上方進度條：青綠＝已有生成影片、金色＝已有分鏡圖、灰色＝待補。
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
node render.mjs                           # → EP01_animatic_v1.mp4
```

需要 Node 22、Playwright、ffmpeg（含 libvpx）、Noto CJK 字型（`apt-get install ffmpeg fonts-noto-cjk`）。
雲端 session 沒有系統 ffmpeg 時：`pip install imageio-ffmpeg`，把它的執行檔連結成 `~/bin/ffmpeg` 再 `PATH=~/bin:$PATH node render.mjs`。
直接用瀏覽器開 `index.html` 可即時預覽（無聲；Chrome 需加 `--allow-file-access-from-files` 才讀得到本機圖片）。

## 換上影片片段（v1 起已支援）

1. 影片存成 `storyboard/export/h3/<段號>/<檔名>.mp4`，驗片結果記進 `../DECISIONS.md`。
2. 在 `clips.json` 加一筆：`seg`、`n`（第幾切）、`file`、`in`／`out`（取影片哪一段，秒）、`src`（工具名）。
   一支影片含多鏡時，照實際切點分成多筆（例：可靈 E01-01 在 2.8 秒切鏡 → #1 取 0～2.75、#2 取 2.85～5.0）。
3. 重跑 `node render.mjs`。Playwright 的 Chromium 不能解 H.264，render.mjs 會先把片段轉成 VP9 全關鍵幀暫存檔
   （`cache/`，不進版控）再逐格 seek。直接用瀏覽器開 `index.html` 預覽時也讀這個暫存檔，所以要先跑過一次 render。
「第七室」門牌字（E01-02 #3、E01-09 #2）依規定剪輯後製，等那兩格有圖、確定門牌位置後再加疊字。

## 已知限制

- 程式只能對靜態圖做推拉搖移，角色本身不會動；要角色動起來必須靠影片模型。
- 片段長度和分鏡秒數不同時是等速拉伸：E01-02 #1 用 2.25 秒片段填 3 秒，放慢約 0.75 倍，走路看得出略慢。
- 可靈免費版的浮水印（右下 KlingAI）會留在畫面上，只供內部看。
- MP4 每次重出都會進 git 歷史（約 14MB），只在分鏡圖有明顯進度時才重出並 commit。
