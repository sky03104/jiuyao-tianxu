# LF01　長篇動畫第一集〈青嵐試煉〉試片

- 劇本：`docs/43_LONGFORM_ANIMATION_EP01_QINGLAN_TRIAL_PILOT_SCRIPT_V1.0.md`（長篇動畫劇本為正史第一來源）
- 試片範圍：第 2 場（第七室報到，180 秒，12 段 24 張）＋第 12 場（赤瞳妖將，130 秒，9 段 17 張），橫式 16:9
- 分鏡資料：`lf01.json`（新設定：江祈璟 C06、聞人澈 C07、赤瞳妖將 C08、陣眼遺跡 S04；其餘沿用 EP01 已定稿設定圖）
- **自動出圖（建議）**：Codex 照 `CODEX_RUNBOOK.md` 跑 `codex_jobs.json`，每張自己存檔、commit，紀錄在 `codex_log.md`
- 手動備援：`docs/HANDOFF-010_LF01_LONGFORM_TEST.md`（一般 ChatGPT 對話一張一張貼）
- 以上都由 `tools/lf01_handoff.py` 產生（改 `lf01.json` 後重跑）

```bash
pip install pillow
LF01_BRANCH=<分支名> python3 production/LF01/tools/lf01_handoff.py
```

| 資料夾 | 內容 |
|---|---|
| `characters/`、`art/` | 新設定圖（咖哩出圖後存入，驗圖紀錄寫在本檔下方） |
| `storyboard/frames/<段號>/fN.png` | 分鏡圖 |
| `storyboard/chatgpt/` | 參考拼圖（ChatGPT 一次只能上傳 2 張） |

## 驗圖紀錄

| 圖 | 結果 | 備註 |
|---|---|---|
| （尚未出圖） | | |
