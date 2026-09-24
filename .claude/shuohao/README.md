# shuohao-skills（第三方 AI 短劇製作 skill，已內建於本專案）

- 來源：https://github.com/eternityspring/shuohao-skills
- 版本：commit `ca1c30be78bde70fa84d3817453c71e0ef25b751`（2026-09-24 安裝）
- 授權：Apache 2.0（見同目錄 `LICENSE`、`NOTICE`），© 2026 烁皓
- 安裝位置：`.claude/skills/novel-{outline,characters,art,script,storyboard}/`
  （專案層級 skill，放進 repo 才能在雲端 session 每次開機都自動載入；
  原作者的 `install.sh` 是軟鏈到 `~/.claude/skills`，雲端容器重開就會消失，故不採用）
- 為了精簡已移除各 skill 的 `assets/`（僅 README 截圖，SKILL.md 不引用）
- 安裝前已檢查：腳本只用 Node 標準庫、無對外連線、無 npm 依賴；
  在 Linux + Node 22 跑過全部自測（151/337/249/154/323/92 項全通過）

## 五段管線

| skill | 做什麼 |
| --- | --- |
| novel-outline | 小說 → 短劇改編大綱（改編說明/人物表/爽點表/分集梗概/資產清單） |
| novel-characters | 角色設定集：人物畫像、形象提示詞、音色提示詞 |
| novel-art | 美術設定集：場景＋敘事道具提示詞 |
| novel-script | 劇本：場次＋節拍流，逐集時長折算，台詞本可直接接 TTS |
| novel-storyboard | 分鏡：段(≤15秒)→分鏡(2–5秒)→分鏡圖，輸出 MiniMax H3 / Seedance 投產包 |

順序建議：outline → characters → art → script → storyboard。
輸出以簡體中文為主（原作者設定），需要時可要求改用繁體。

## 合成單頁報告

```bash
node .claude/shuohao/report.mjs --from <工作目錄> --out report.html
```

## 更新到上游新版

```bash
git clone --depth 1 https://github.com/eternityspring/shuohao-skills.git /tmp/shuohao
for n in novel-outline novel-characters novel-art novel-script novel-storyboard; do
  rm -rf .claude/skills/$n && cp -r /tmp/shuohao/skills/$n .claude/skills/ && rm -rf .claude/skills/$n/assets
done
cp /tmp/shuohao/scripts/report*.mjs .claude/shuohao/
for f in .claude/skills/novel-*/scripts/selftest.mjs; do node "$f"; done
```
更新後記得改上方 commit 版本號。

## 與本專案的關係

- 本專案劇情正史以 `docs/21`、`docs/24`~`docs/42` 為準；短影音屬
  `docs/22_TRANSMEDIA_ANIMATION_PLAN` 的「動畫/短影音補完」範疇，遊戲為正史，
  衝突時先改影音。
- 這些 skill 產出的 json/md 建議放 `production/<集數>/shuohao/` 之下，
  報告 HTML 與分鏡 png 可重新產生，不進版控（見根目錄 `.gitignore`）。
