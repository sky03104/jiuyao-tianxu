# CLAUDE.md — 《九曜：天墟》專案規則

> 原創東方幻想手機MMORPG，暫定代號「九曜：天墟」，目前仍在企劃/Phase 0階段，尚未進入正式程式開發。

## 必讀文件（依序）

1. `docs/00_GAME_DESIGN_BIBLE_V1.0.md` — 世界觀、玩法、企劃總綱（原始版本，未修改）
2. `docs/01_ARCHITECTURE_DECISIONS_V1.0.md` — 連線架構/系統分工/商業化/戰鬥自動化的決策記錄，
   **這份是後補的，跟00有衝突時以01為準**
3. `docs/02_CHARACTER_BIBLE_V1.0.md` ~ `docs/19_DEVELOPMENT_ROADMAP_V1.0.md` —
   各系統設計文件（角色/戰鬥/靈印/養成/地圖/任務/怪物/BOSS/裝備/生活/社交/公會/經濟/UI/
   美術/技術架構/路線圖）。**全數18份已完成「細節定案」**（具體數值為初版示意值，
   標記為「可調整」，需Phase 0原型測試後校準），文件開頭都有狀態與修正紀錄標註。
4. `docs/44_PARTNER_TEAM_AND_CLASS_CHANGE_V1.0.md` — **2026-09-27 起戰鬥結構比照《斗羅大陸：史萊克學院》**：雙脈取消，机遙一個流派、
   戰鬥中不換人；找 NPC 用轉職卡換流派（副本內不可）；契友隊伍第一位 AI 上場、其餘增益；各自養成、劇情解鎖、不抽卡；
   取代 03 第3節、20 第4節、00 第7節。
   `docs/20_COMPANION_SYSTEM_V1.0.md` — 夥伴系統（星曜獸型可捕捉異獸＋宿命契友型
   固定隊友連攜技），呼應《斗羅大陸：史萊克學院》玩法參考中原先缺漏的夥伴支柱，
   純新增系統，**完全不修改02/03/04/05/06/07/12/14既有內容**
5. `docs/21_WORLD_AND_STORY_MASTER_V1.0.md` — 世界觀與主線劇情總綱，**優先級高於
   尚未細化的劇情文件**，九曜界/天墟真相/五大勢力立場/前三章劇情骨架與真相分層
   揭露節奏皆以此為準
6. `docs/07_QUEST_DESIGN_V2.0.md` — **取代07_V1作為正式任務規格**（V1保留為歷史
   記錄不刪除），第一章16節點任務級詳細規格＋第二/三章章節骨架，與21世界觀總綱
   對齊（河谷為古林前置區、境界突破/古林通行印記/赤瞳妖將規模區分等收斂結果）
7. `docs/00_AI_HANDOFF_BRIDGE.md` — ChatGPT/Claude Code協作交接紀錄，涉及世界觀/
   劇情/Phase0範圍的重大決策討論過程與使用者裁決紀錄都在這裡，跟劇情或Phase0範圍
   有關的任務動工前建議先查一次是否有尚未收斂的爭議
8. `docs/22_TRANSMEDIA_ANIMATION_PLAN_V1.0.md` — 跨媒體（遊戲×動畫×短影音）規格，
   **2026-09-27 起正史改為小說＋動畫、遊戲對照改編（主角机遙，遊戲中玩家扮演他），衝突時先修遊戲**
   （見 22 開頭裁定；文件內「遊戲正史」舊描述保留為歷史）；共用3D資產原則與Cinematic技術
   需求盤點；赤瞳妖將「先遣遭遇／世界討伐戰」的名稱區分規則亦記錄於此並同步反映
   在07_V2與09
9. `docs/23_ANIMATION_PRODUCTION_PIPELINE_V1.0.md` / `docs/23A_AI_VOICE_TOOL_EVALUATION_V1.0.md`
   — 動畫製作工具鏈與AI配音工具評估（Blender/Mixamo/Rokoko/DeepMotion動作捕捉、
   Chatterbox/CosyVoice/ElevenLabs/Fish Audio/Kokoro配音比較），已經HANDOFF-004
   技術查證修正過免費額度/授權/定價等細節，查證來源見`docs/HANDOFF-004_RESEARCH_NOTES.md`
10. `docs/CHANGELOG.md` — 版本歷史
11. `docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md`（內容為 v1.1）— **所有出圖的最高美術規範**：畫風以咖哩選定的
    GPT 參考稿為準（`production/EP01/shuohao/imagegen/refs/`），出圖一律附參考稿、不可只靠文字 prompt。
    EP01 出圖工具鏈與逐張驗圖紀錄見 `production/EP01/shuohao/DECISIONS.md`，腳本 `imagegen/gen_images.py`

## 劇情分工（2026-09-27 咖哩裁定）

- **劇情（動畫、小說、補完內容）由 Claude 設計，咖哩以觀眾身分看成品**，不必每個劇情選擇都先問。
- 仍受本文件「核心規則」約束：不改核心世界觀、遵守 `21` 真相揭露節奏與各章硬性規則；新增劇情一律標記並寫進 `/docs`。
- 會改動已定案正式設定（世界觀、角色身分、正史歸屬等）的事，仍要先問咖哩。
- 長篇動畫試點：`docs/43_LONGFORM_ANIMATION_EP01_QINGLAN_TRIAL_PILOT_SCRIPT_V1.0.md`（第一章＝一集約 28 分鐘，方向已通過）。
- **動畫製作分工看 `production/roles/`**（編劇／分鏡出圖／驗圖／配音／剪輯／字幕六本工作手冊，動工前先讀對應那本）；
  另有兩個 agent（`.claude/agents/`）：**script-reviewer**（劇本寫完、送配音前必跑，以觀眾身分抓看不懂／怪／死氣沉沉）、**frame-checker**（驗圖）。
- **長篇系列總綱已定案（2026-10-04）**：`docs/47_LONGFORM_SERIES_BIBLE_V1.0.md` 是長篇動畫的最高劇情依據（揭露上限仍以 `21` 為準）；改劇情先改總綱、跑 script-reviewer，再改劇本。宿舍男女分開，「室」＝修行小組。
- **女性角色（2026-10-02 咖哩裁定）**：主要角色男女至少 1：1，厲若楓改為女性，新增 5 位女性角色，見 `docs/46_FEMALE_CAST_EXPANSION_V1.0.md`（優先於 `02` 衝突處）。
- **動畫製作進度看這裡**：短影音 EP01〈第七室報到〉→ `production/EP01/shuohao/DECISIONS.md`（分鏡圖 6/30、可靈試片 E01-01／E01-02）；
  長篇 LF01（`43` 第 2、12 場，16:9）→ `production/LF01/README.md`（設定圖：江祈璟、聞人澈、赤瞳妖將、陣眼遺跡，及 2026-10-04 用 ChatGPT 出的厲若楓（女版）、裴含章、陸鳴鸞全數通過；**流程已改為先鏡頭表、後配音**（`production/roles/README.md`），第 12 場鏡頭表 v2 見 `production/LF01/storyboard/scene12_shotlist_v2.md`；分鏡圖 17/43（第 12 場完成，總覽 `production/LF01/storyboard/frames/LF01_scene12_contact.jpg`；第 2 場 26 張待出，先出 3 張故事版；第 12 場動態分鏡最新 v6 見 `production/LF01/animatic/`；配樂音效改用免費樂器音色庫，見 `production/LF01/music/README.md`；**2026-10-09 起日常戲改用 Seedance（CapCut）以設定圖直接生影片**，規則見 `production/roles/README.md`、成品 `production/LF01/video/`；**第 2 場完整版 v1（134 秒）已出**：`production/LF01/video/LF01_scene02_seedance_full_v1.mp4`；剪輯的聲音／節奏／運鏡規則見 `production/roles/05_剪輯.md`）；配音改走國漫配音風格：Qwen3-TTS VoiceDesign＋混音，26 句配音完成（咖哩確認）於 `production/LF01/voice/final/`；Qwen 提示詞一律用簡體，繁體會變粵語；Kaggle 由 Claude 經 API 直推執行）。
  **2026-09-30 咖哩裁定：主力改做長篇 LF01；短影音 EP01 只是流程測試，暫停出圖**（已驗證的工具鏈、配音、動態分鏡程式沿用到長篇）。
  短影音用直式 9:16、長篇用橫式 16:9；角色／場景設定圖（3:2）兩邊共用。
  **影片生成排程與工具分工看 `production/EP01/shuohao/video/PLAN.md`（2026-09-28 起漫劇式：多數格子靜態圖＋運鏡，影片只給關鍵鏡）；每一切只生一次，不要用不同工具重做已完成的片段**（2026-09-28 咖哩裁定）。
- **目前優先：先做動畫**（2026-09-27 咖哩更正）。**長篇動畫劇本（`43` 起）是正史第一來源**；小說暫停，已寫的楔子～第二章保留，之後改為依動畫劇本撰寫（`docs/45`、`novel/`）。遊戲暫停在企劃。

## 出圖／素材保存規則（2026-09-24 咖哩裁定）

- 雲端 session 重開容器就清空：**參考稿、設定圖、分鏡圖等出圖結果一律 commit 進 repo**，不可只留在 session
  或對話附件裡（`.gitignore` 已不忽略 shuohao 出圖結果）。使用者在對話中上傳的參考圖，先存進 repo 再使用。
- 出圖後逐張對照角色卡／美術設定規格驗圖，結果記入 DECISIONS.md；不合格的不拿來當後續參考。

## 核心規則（摘自 GDD 第39章，長期有效）

1. 不要直接開始寫完整遊戲，先確認目前處於企劃/Vertical Slice/正式內容哪個階段
2. 不要擅自改變核心世界觀（九曜界/天墟裂變/天玄院等設定）
3. 新增系統前先檢查是否與 `01_ARCHITECTURE_DECISIONS_V1.0.md` 已定案的分工衝突
   （尤其裝備vs靈印、戰鬥自動化三層、商業化禁區）
4. 所有暫定設定標記為「可調整」，不要把未確認設定當最終規格
5. 資料驅動架構優先：武將/怪物/物品/技能/靈印資料應可獨立管理，不寫死在程式碼
6. 重要決策要寫入 `/docs`，不要只留在對話裡
7. 開發大型功能前先說明：要做什麼／改哪些檔案／為什麼這樣設計／如何測試
8. 不要未經確認執行破壞性操作

## ⚠️ IP/版權原則

本專案**不直接使用任何既有作品（含滄元圖、牧神記等）的人物、地名、勢力、專有名詞、
劇情或力量體系**，必須是獨立原創IP。可以吸收「東方修行、妖魔、人族、學院、少年成長、
世界危機」等廣泛題材元素，但世界觀、角色關係、劇情、名詞、系統與視覺識別必須自行建立。
商業化前仍應由專業IP/法律人士做最終檢視。

## 目前最優先的技術待辦

連線技術棧已拍板（Photon Fusion Dedicated Server，見 `01_ARCHITECTURE_DECISIONS_V1.0.md`
第1節）。下一步見 `docs/19_DEVELOPMENT_ROADMAP_V1.0.md`：進入 Phase 0 原型驗證
（引擎框架+連線同步驗證+戰鬥雙搖桿與六流派原型）。

Phase 0 進度：0-A 連線／0-B 戰鬥框架＋六武器／0-C 靈印 已完成實跑驗收；**0-D 地圖/Spawn/任務骨架
程式碼已完成（2026-09-25），尚待本機 Unity 實跑驗收**（步驟見 `unity/JiuyaoTianxu/README.md` Phase 0-D 章節，
結果補進 `docs/00_AI_HANDOFF_BRIDGE.md` CLAUDE-REPLY-008）。雲端 session 沒有 Unity，只能做離線檢查：
**改完程式跑 `unity/JiuyaoTianxu/Tools/run_all_checks.sh`**（需 `apt-get install mono-devel`；離線編譯＋單元測試＋
資料表驗證，PR 上 GitHub Actions「Unity offline checks」也會自動跑同一支），不可宣稱網路實跑通過。
**0-E（Roadmap Phase 0 缺口：資料表／雙搖桿／目標鎖定／基礎回饋）程式碼已完成（2026-09-25），同樣待本機驗收**；
Phase 0 數值來源已改為 `unity/JiuyaoTianxu/Assets/_Project/Config/Tables/*.csv`（改數值改 CSV 再跑匯入器，
不要改 Editor 腳本）。進 Phase 1 前的技術債與待裁定問題見 `docs/PHASE0_TECH_REVIEW_CLAUDE_V1.0.md`。
**有 Unity 的 session 請先照 `docs/PHASE0_LOCAL_VERIFICATION_RUNBOOK.md` 一次跑完所有待驗收項目**
（PR #5～#10 都還沒在 Unity 實跑過）。

## 開發階段

**規劃階段（Phase 0企劃）已完成**：02~19 共 18 份系統設計文件全數完成細節定案
（GPT/Gemini/DeepSeek 交叉討論、Claude審查收斂並修正多次模型自創錯誤世界觀名稱/
跨系統衝突的問題）。所有數值為初版示意值，標記為「可調整」，需 Phase 0 原型測試後校準。
下一步依 `19_DEVELOPMENT_ROADMAP_V1.0.md` 進入實際原型開發，不再是純企劃討論階段。
