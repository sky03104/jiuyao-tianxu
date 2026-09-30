# LF01 配音

## 2026-09-30　方向更正（咖哩裁定）

旁白、蕭曜霖試過 BreezyVoice（Qwen 參考聲音／OpenAI 台灣國語參考）、OpenAI TTS 台灣國語直出，咖哩都判定「不是我要的」：
問題是**氣勢／情緒與音色**，要的是**大陸國漫（3D 玄幻動畫）配音**的感覺——字正腔圓的標準普通話、史詩旁白、強者教官的壓迫感。
先前「台灣國語、自然口吻」的方向是 Claude 誤判，之後配音以國漫配音風格為準（机遙等少年角色是否同樣改國漫腔，待試聽後決定）。

| 資料夾 | 內容 | 結果 |
|---|---|---|
| `breezy-v1/` | BreezyVoice＋Qwen v3 參考（旁白、蕭曜霖、机遙） | 机遙字全對；旁白／蕭曜霖不合格 |
| `ref-openai/`、`openai-tts-v1/` | OpenAI TTS 台灣國語男聲（參考與直出） | 方向不對（不是國漫腔） |
| `breezy-v5-twref/` | BreezyVoice＋OpenAI 台灣國語參考（ash／onyx）×2 次 | 換掉 Qwen 參考後字幾乎全對（旁白 t2、蕭曜霖 t2 皆正確），證實先前錯字來自參考聲音；但仍是台灣國語方向，不採用 |
| `openai-tts-guoman/` | OpenAI TTS 國漫風格提示詞直出（onyx／ballad／ash） | 待咖哩試聽 |
| `qwen3tts_guoman_kaggle_LF01.ipynb` | Qwen3-TTS VoiceDesign 國漫風格，3 候選 × 2 次 | Kaggle 執行中 |
