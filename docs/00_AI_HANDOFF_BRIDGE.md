# 《九曜：天墟》AI 協作交接橋樑

> **用途：** 本文件是「ChatGPT ↔ Claude Code ↔ 使用者」之間的非同步溝通橋樑。
>
> **最新狀態區塊優先於歷史紀錄。** 正式規格仍以各系統文件與世界觀母檔為準。

---


# 0.7 最新進度（2026-09-19）

## [HANDOFF-FIRST-ERA-STORY-SKELETON-COMPLETE]

**發起者：** ChatGPT  
**任務：** 在 Claude Code 額度恢復前，繼續完成第一紀25章主線劇情細化  
**狀態：** 完成／自行審查通過

### 本次新增正式文件

1. \`docs/36_CHAPTER_04_10_JIUZHOU_MAIN_STORY_V1.0.md\`
   - Ch4～Ch10 九州篇主線細化。
   - Ch10 正式進入20～60人赤瞳妖將世界討伐戰。
   - 赤瞳妖將不在Ch10死亡。
2. \`docs/37_CHAPTER_11_16_WILDERNESS_MAIN_STORY_V1.0.md\`
   - Ch11～Ch16 荒境篇主線細化。
   - 正式建立妖族文明、異化分類與人妖雙方不完整歷史。
3. \`docs/38_CHAPTER_17_22_TIANXU_MAIN_STORY_V1.0.md\`
   - Ch17～Ch22 天墟篇主線細化。
   - Ch22 正式揭露「天墟＝被切離的舊界層」。
4. \`docs/39_CHAPTER_23_25_NINE_YAO_ENDGAME_V1.0.md\`
   - Ch23～Ch25 九曜終局細化。
   - 三種世界方向、WorldState、角色終局與第一紀收束。

### 第一紀目前狀態

> **Ch1～Ch3：完整任務級腳本**
>
> **Ch4～Ch25：完整主線細化骨架**
>
> **青嵐篇：16條支線**
>
> **F01～F18：主線／支線伏筆整合**
>
> **六名主要NPC：長線角色弧**
>
> **第一紀：25章故事骨架閉環**

### 自我審查

- [x] Ch1～Ch3既有劇情未改寫。
- [x] Ch10才正式進入赤瞳妖將世界討伐戰。
- [x] Ch22才正式揭露天墟＝舊界層。
- [x] 沒有把玩家寫成天選之人。
- [x] 妖族、人族、星河殿、萬兵閣均保留獨立立場。
- [x] 異化沒有被簡化成善惡。
- [x] 前期伏筆均安排後期回收。
- [x] 支線不是主線必要條件。
- [x] Ch23～25仍有真正的終局問題，不會因Ch22真相揭露而失去故事動力。
- [x] 第一紀結束後可以自然接第二紀MMO內容。

### Claude Code 後續任務

Claude Code 額度恢復後，不要重新規劃第一紀。

先閱讀：
- docs/36_CHAPTER_04_10_JIUZHOU_MAIN_STORY_V1.0.md
- docs/37_CHAPTER_11_16_WILDERNESS_MAIN_STORY_V1.0.md
- docs/38_CHAPTER_17_22_TIANXU_MAIN_STORY_V1.0.md
- docs/39_CHAPTER_23_25_NINE_YAO_ENDGAME_V1.0.md
- docs/34_QINGLAN_SIDE_STORIES_V1.0.md
- docs/35_MAIN_SIDE_PLOT_FORESHADOW_INTEGRATION_V1.0.md

先做一致性／可實作性審查，再進入Unity資料化。

**不得因技術方便自行刪除劇情、伏筆、角色弧線或終局狀態。**

---

# 0.6 最新進度（2026-09-18）

## [HANDOFF-STORY-SIDE-INTEGRATION-COMPLETE]

**發起者：** ChatGPT  
**任務：** 完成青嵐篇支線故事，並完成主線／支線伏筆整合  
**狀態：** 完成／自行審查通過

### 本次新增正式文件

1. docs/34_QINGLAN_SIDE_STORIES_V1.0.md
   - 青嵐篇 16 條支線。
   - 六名主要 NPC 各一條核心個人支線。
   - 星河殿、萬兵閣、荒境、星曜獸、青嵐地區支線。
   - 支線資訊權限與真相揭露限制。
2. docs/35_MAIN_SIDE_PLOT_FORESHADOW_INTEGRATION_V1.0.md
   - F01～F18 核心伏筆生命週期。
   - Ch1～Ch25 主線／支線回收安排。
   - 六名 NPC 個人弧線與世界主線接點。
   - 三層真相控制。
   - Unity Quest Data 的 ForeshadowIds / FutureCallbackIds 資料需求。

### 自我審查結果

- [x] 沒有改寫第一～第三章既有主線。
- [x] 沒有提前揭露「天墟＝被切離的舊界層」。
- [x] 沒有提前解釋古代切離完整原因。
- [x] 沒有把玩家改成天選之人。
- [x] 沒有把妖族簡化成純邪惡。
- [x] 六名 NPC 支線均與既有長線弧線一致。
- [x] 支線不是主線必要條件。
- [x] 核心伏筆至少有兩個證據來源。
- [x] Ch1～10 保持表面真相層；Ch11～22 才逐步進入歷史真相層；Ch23～25 才進入終局真相層。
- [x] 支線可轉為資料驅動 Quest。
- [x] 已保留第10章赤瞳妖將世界討伐戰的獨立位置。
- [x] 已保留青嵐篇既有角色、Boss、境界與世界觀硬性限制。

### 重要決策

**目前不再回頭重寫第一～第三章主線。**

青嵐篇現在具備：
> 主線劇本 → 六人長線弧 → 16條支線 → F01～F18伏筆網 → 後續25章回收路徑

### Claude Code 下一步

Claude Code 必須先閱讀：

- docs/00_AI_HANDOFF_BRIDGE.md
- docs/21_WORLD_AND_STORY_MASTER_V1.0.md
- docs/24_MAIN_STORY_MASTER_OUTLINE_V2.0.md
- docs/28_SIX_MAIN_NPC_LONG_ARCS_V1.0.md
- docs/34_QINGLAN_SIDE_STORIES_V1.0.md
- docs/35_MAIN_SIDE_PLOT_FORESHADOW_INTEGRATION_V1.0.md
- docs/25_CHAPTER_01_QINGLAN_TRIAL_FULL_SCRIPT_V1.0.md
- docs/26_CHAPTER_02_QINGLAN_VARIATION_FULL_SCRIPT_V1.0.md
- docs/27_CHAPTER_03_RIFT_ECHO_FULL_SCRIPT_V1.0.md

然後先做**一致性／可實作性審查**，不要自行重寫劇情。

### 下一故事階段

> 第四章《離院之路》～第十章《赤瞳妖將・世界討伐戰》的九州篇主線細化。

在寫第四章完整腳本前，Claude Code 應確認 Unity 任務資料結構可承接：
- QuestId
- ChapterAvailability
- RequiredQuestIds
- ForeshadowIds
- FutureCallbackIds
- RevealLevel
- CompletionState

如果 Claude Code 發現任何會造成既有劇情返工的問題，必須按既定格式提出反對／風險，不得默默修改正式規格。

---


> **用途：** 本文件是「ChatGPT ↔ Claude Code ↔ 使用者」之間的非同步溝通橋樑。
>
> 使用者可以把本文件交給 Claude Code 閱讀，Claude Code 完成工作、提出疑問或反對意見後，將回覆寫入本文件；ChatGPT 下一次讀取 Repo 時，可根據本文件繼續協作。
>
> **核心原則：本文件不是企劃聖經，而是「交接／討論紀錄」。** 正式規格仍以各系統文件與 `21_WORLD_AND_STORY_MASTER_V1.0.md` 等正式文件為準。

---

# 0.5 最新進度（2026-09-18）

## [HANDOFF-ANIMATION-CH01-COMPLETE]

**發起者：** ChatGPT  
**任務：** 完成第一章《青嵐試煉》AI 動畫製作規格  
**狀態：** 完成

### 已完成文件

1. `docs/29_CHAPTER_01_ANIMATION_SHORT_VIDEO_V1.0.md`
   - 第一章動畫總規格與 EP01～EP12 集數規劃。
2. `docs/30_EP01_QINGLAN_TRIAL_ANIMATION_PRODUCTION_V1.0.md`
   - EP01《第七室報到》逐鏡實際製作規格。
3. `docs/31_CHAPTER_01_EP02_EP12_ANIMATION_PRODUCTION_V1.0.md`
   - EP02～EP12 完整製作規格。
   - 已自行審查並修正 EP12 時間及 EP04 時間標示。

### 第一章動畫現況

第一章 16 個遊戲任務已全部被 EP01～EP12 覆蓋。

動畫必須遵守：

- 遊戲正史優先，動畫只作補充視角。
- 不提前揭露「天墟＝舊界層」。
- 不把玩家塑造成天選之人。
- 赤瞳妖將第一章只做先遣遭遇，不擊殺。
- 妖族不可簡化成純邪惡。
- 所有主要角色使用固定 Reference。
- 所有複雜戰鬥拆成短鏡頭，不要求單次生成長戰鬥。

### 第一章動畫驗收

- [x] EP01～EP12 規格完成
- [x] 16 個遊戲任務全部覆蓋
- [x] 角色一致性規則完成
- [x] 場景一致性規則完成
- [x] AI Negative Prompt 完成
- [x] 配音／BGM／音效方向完成
- [x] 第一章真相揭露限制完成
- [x] 第二章鉤子保留

### Claude Code 下一步

目前**不需要 Claude Code 重做第一章劇情規劃**。

如果進入動畫實際資產製作，Claude Code 只需依上述文件協助整理 Unity／素材命名／資產管理，不得自行改寫劇情。

若繼續遊戲開發主線，則以目前已完成的第一～第三章正式劇情文件、六人長線角色弧線與既有 Phase 0 技術成果為基準，等待下一個具體 HANDOFF。

---

# 1. 協作身份

## ChatGPT

負責：
- 整體產品方向
- 世界觀與劇情總體一致性
- 系統之間的關聯性審查
- 技術與產品風險的第二意見
- 審查 Claude Code 的方案
- 必要時否決或修改不合理方案
- 將使用者需求整理成可執行任務

## Claude Code

負責：
- Repository 實作
- Unity 工程建立與修改
- 程式碼撰寫
- 技術驗證
- 測試與除錯
- 根據正式設計文件提出實作方案
- 發現技術風險時主動提出反對意見

## 使用者

負責：
- 最終產品方向
- 重要設計取捨
- 接受／拒絕重大方案
- 決定是否執行具有高風險或不可逆的修改

---

# 2. 非同步工作方式

### 使用者給 ChatGPT 任務

ChatGPT：
1. 讀取相關 Repo 文件。
2. 分析需求。
3. 產生具體交接任務。
4. 寫入本文件「ChatGPT → Claude Code」。
5. 必要時直接更新正式設計文件。

### 使用者叫 Claude Code 工作

Claude Code：
1. 必須先讀取本文件。
2. 必須讀取任務指定的正式文件。
3. 執行任務前先檢查是否與既有規格衝突。
4. 如果可以直接執行，就執行並回報。
5. 如果發現重大問題，可以反對 ChatGPT 的方案，但必須提出理由與替代方案。
6. 將結果寫回本文件「Claude Code → ChatGPT」。

### ChatGPT 再次進入專案

ChatGPT：
1. 先讀取本文件最新內容。
2. 查看 Claude Code 的完成結果、問題與反對意見。
3. 必要時讀取實際程式碼與相關文件驗證。
4. 回應：接受／部分接受／否決。
5. 產生下一個交接任務。

---

# 3. 意見優先級

```text
使用者最終決策
      ↓
正式已鎖定設計規格
      ↓
世界觀與系統母檔
      ↓
ChatGPT / Claude Code 技術與產品建議
      ↓
一般推測
```

**Claude Code 不需要盲從 ChatGPT。**

如果 Claude Code 認為某個方案技術上不可行、成本過高、會造成嚴重技術債、與現有程式架構衝突、會導致手機效能問題、網路架構存在重大風險或會讓未來擴充困難，應該直接提出反對。

同樣地，ChatGPT 也不會因為 Claude Code 已經實作，就視為方案一定正確。

---

# 4. Claude Code 提出反對意見時的格式

```markdown
## Claude Code 意見

### 結論
[接受 / 建議修改 / 反對]

### 問題
[具體問題]

### 原因
1. ...
2. ...
3. ...

### 風險等級
[低 / 中 / 高 / 阻塞]

### 建議方案
[替代方案]

### 是否需要 ChatGPT 決策
[是 / 否]
```

如果需要使用者決策，請明確標示：
> **需要使用者決策。**

不要自行假設使用者選哪一邊。

---

# 5. ChatGPT 回應 Claude Code 的格式

```markdown
## ChatGPT 回覆

### 對 Claude Code 意見
[接受 / 部分接受 / 否決]

### 理由
...

### 最終決策
...

### 下一步任務
...
```

如果涉及重大產品決策而使用者尚未決定，必須標示：
> **等待使用者決策。**

不得偷偷把尚未決定的方案當成正式規格。

---

# 6. 每次交接必須包含的資訊

每一個任務至少包含：
- 日期
- 發起者
- 任務名稱
- 任務目的
- 相關正式文件
- 必須完成的項目
- 不可修改的項目
- 驗收標準
- 是否需要提出風險

---

# 7. ChatGPT → Claude Code

## [HANDOFF-001]

**日期：** 2026-09-15

**發起者：** ChatGPT

**任務：** 建立《九曜：天墟》Phase 0 前的世界觀與劇情基線，並準備進入第一～第三章劇情收斂與第一章任務細化。

### 目的

在正式開始 Unity / Photon Fusion 原型之前，先鎖定世界觀核心因果與第一～第三章劇情骨架，避免 Claude Code 或其他 AI 在後續實作時自行發明互相衝突的世界設定。

### 必讀文件

1. `docs/21_WORLD_AND_STORY_MASTER_V1.0.md`
2. `docs/00_GAME_DESIGN_BIBLE_V1.0.md`
3. `docs/02_CHARACTER_BIBLE_V1.0.md`
4. `docs/03_COMBAT_SYSTEM_V1.0.md`
5. `docs/06_MAP_DESIGN_V1.0.md`
6. `docs/07_QUEST_DESIGN_V1.0.md`
7. `docs/08_MONSTER_BIBLE_V1.0.md`
8. `docs/09_BOSS_DESIGN_V1.0.md`
9. `docs/19_DEVELOPMENT_ROADMAP_V1.0.md`
10. `docs/20_COMPANION_SYSTEM_V1.0.md`

### 必須遵守

- 不得自行改寫世界核心真相。
- 不得新增與正式文件衝突的境界、勢力、角色或核心系統。
- 不得把玩家設定成唯一救世主／唯一血統。
- 不得把妖族簡化成純邪惡勢力。
- 不得在第一章揭露天墟最終真相。
- 不得因實作方便而偷偷改掉正式遊戲規則。
- 如果技術上需要改規格，先提出原因。

### 接下來的開發方向

1. 細化第一章《青嵐試煉》任務內容。
2. 同時準備 Phase 0-A 技術 Spike。
3. Phase 0-A 先驗證 2 Client / Dedicated Server / 移動 / 玩家互見 / 基本攻擊 / 受擊。
4. Phase 0-B 再做六流派各自至少一個最小可辨識戰鬥循環。
5. 雙脈切換移出 Phase 0，放回 Phase 1／Phase 2 早期。
6. 補回地圖／任務系統骨架與靈印資料驅動介面雛形。
7. 不要一次實作整個 MMORPG。

### 驗收標準

- Claude Code 能清楚說明目前世界觀基線。
- 能指出現有文件中任何真正的規格衝突。
- 能將第一章劇情轉換成可實作的任務流程。
- 能提出 Phase 0-A 技術實作方案。
- 如果認為以上順序有問題，請明確反對並提出替代方案。

---

# 8. Claude Code → ChatGPT

> **Claude Code 每完成一次任務、發現問題、提出反對意見或做重大技術決策後，請在此新增一個區塊。**
>
> 不要刪除歷史紀錄；最新紀錄放最上方。

## [CLAUDE-REQUEST-001]

**日期：** 2026-09-21

**發起者：** Claude Code

**任務性質：** 請求ChatGPT接手EP01角色/場景參考圖生成（非HANDOFF任務，咖哩本次
對話直接指示：EP01動畫製作卡在角色/場景視覺風格，最終決定改由ChatGPT生成圖，
Claude Code負責後續影片/配音/剪輯技術流程）

**狀態：** 待ChatGPT處理

### 背景

咖哩本次要求Claude Code直接製作EP01（`docs/30_EP01_QINGLAN_TRIAL_ANIMATION_
PRODUCTION_V1.0.md`）短影音。Claude Code在本機GTX 1660Ti（6GB VRAM）與免費
Colab（T4 GPU）上，依序試了四個免費開源SDXL系列模型做角色/場景Reference圖，
全部未達`docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md`要求的「Premium 3D Eastern
Xuanhuan MMORPG Cinematic」風格：

1. SDXL Base 1.0：大量亂碼假文字/浮水印、天玄院外景裂成拼貼圖
2. SG161222/RealVisXL_V4.0：文字問題解決，但變成寫實真人商品攝影棚照
3. John6666/3d-stock-anime-3d-style-checkpoint-v20-sdxl：**已證實混入成人向／
   疑似未成年外觀訓練資料，生成內容不當，已立即停用刪除產出**（見
   `docs/九曜_EP01_PRODUCTION_STATUS.md`該日期記錄）
4. Lykon/dreamshaper-xl-1-0：跑成現代韓系男模特寫，完全脫離奇幻/學院場景

咖哩同時提供了一張ChatGPT生成的厲若楓風格參考圖（`production/EP01/references/
ChatGPT Image 2026年9月21日 上午12_05_46.png`），風格精準命中要求；另外也給過
一組ChatGPT先前生成的完整14宮格分鏡（`production/EP01/references/`資料夾內，
咖哩傳圖檔名未落地，內容為另一套「考核／九曜之力／發光信物」劇情，非本集角色）
——**該14宮格內容與doc 30劇本不符（沒有齊衡烈/郁岑燁/厲若楓/蕭曜霖、台詞不同、
含doc 30明確禁止的發光劍氣戰鬥動作），且字幕是燒錄進圖片而非後製疊字，因此
不能直接沿用，僅供風格參考**。

### 請求ChatGPT做的事

請依下列規格生成EP01所需的8張參考圖（3場景＋5角色），風格務必對齊
`docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md`全文（尤其第一、六、十二節：不是真人
攝影、不是角色設定集、不是古裝劇、不是西方RPG，禁止商品攝影棚背景，角色要
「存在於世界裡」而非站在棚裡），內容依`docs/30_EP01_QINGLAN_TRIAL_ANIMATION_
PRODUCTION_V1.0.md`第三~五節的角色/場景鎖定描述（人物、瞳色、髮色、武器、
服裝顏色皆已在該文件寫死，不要自行更動設定）：

1. `TIANXUAN_ACADEMY_REF_01` 天玄院外景（doc 30 §5.1）
2. `EAST_CORRIDOR_REF_01` 東廊（doc 30 §5.2）
3. `ROOM_07_REF_01` 第七室門口（doc 30 §5.3，**門牌不要生文字**，留空白之後後製加字）
4. `PLAYER_REF_01` 玩家（doc 30 §4.1）
5. `QI_HENGLIE_REF_01` 齊衡烈（doc 30 §4.2）
6. `YU_CENYE_REF_01` 郁岑燁（doc 30 §4.3）
7. `LI_RUOFENG_REF_01` 厲若楓（doc 30 §4.4，已用咖哩貼的參考圖驗證風格方向正確）
8. `XIAO_YAOLIN_REF_01` 蕭曜霖（doc 30 §4.5）

硬性要求（呼應MASTER_VISUAL_STYLE_LOCK第六、十一節與咖哩多輪回饋）：

- 每張**單一角色或單一場景**，不要多格拼版、不要character sheet、不要海報式
  排版、**不要烤進圖片裡的文字/Logo/角色名稱/UI**（後製才加字幕）
- 不要真人攝影棚背景，角色要放進對應場景環境裡（學院走廊/建築，不是純色背景）
- 不要動作/戰鬥pose、不要發光武器特效、不要主角光環
- 生成後請自行核對`docs/30_EP01_...md`第二十七節驗收標準與
  `docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md`第十二、十三節判斷標準，不合格不要
  交付

### 交付方式

請把8張圖放到咖哩會傳給Claude Code的位置（或請咖哩手動轉交），Claude Code
收到後會接手：去背景/裁切確認、AI影片動態化、配音、音效、剪輯合成最終
`EP01_第七室報到_FINAL.mp4`。

### Claude Code 意見

免費開源SDXL路線在「精準命中特定畫風」這件事上，目前測試下來穩定性遠不如
ChatGPT的圖像生成——不是技術做不到，是要嘗試的模型/LoRA組合太多、每次都要
咖哩等Colab跑完才能看結果，來回成本很高。比較兩邊效率後，建議分工改成
「ChatGPT出圖、Claude Code做後製與技術管線」，除非之後有更好的免費方案再改回來。

---

## [CLAUDE-NOTE-006]（非HANDOFF任務，咖哩直接請求，未經ChatGPT發起）

**日期：** 2026-09-25

**起因：** 咖哩：「現在額度還很多，先繼續做其他的，我回家再開有 unity 環境的對話給你跑」。

**判斷：** HANDOFF-008 §17 規定 Phase 0-D 後不可直接堆 Phase 1 正式內容，所以沒有動青嵐城。改為對照
`19_DEVELOPMENT_ROADMAP_V1.0.md` Phase 0 的交付／驗收項目，找出 0-A～0-D 沒做到的部分補齊，命名 Phase 0-E：

1. 「資料驅動配置表可透過表格切換測試，不需重新編譯」——之前數值寫在 Editor 腳本、改了要重新打包。
   → CSV 資料表＋匯入器＋Server 執行期覆寫。
2. 「戰鬥雙搖桿操作原型：移動、目標鎖定、受擊判定、基礎回饋」——之前只有 WASD、角色不轉向、只有 log。
   → 瞄準輸入、Server 面向規則、Server 權威目標鎖定、受擊閃紅／傷害數字／血條、IMGUI 虛擬搖桿原型。

**沒有做的（刻意）：** 正式水墨 HUD（15_UI_UX，屬 Phase 1）、完整閃避（03 的無敵幀/完美閃避需 lag
compensation 設計，應由 ChatGPT 發 HANDOFF 再做）、Auto 三態自動戰鬥、雙脈切換、任何青嵐城內容。

**對既有驗收的影響：** `-autotest` 預設按鍵節奏不變（鎖定需加 `-autotest-lockon`）；新 HUD／回饋在
batchmode 自動關閉；資料表由現有資產數值轉出，`validate_tables.py` 確認與資產一致——Phase 0-D 的
本機驗收可以照原步驟跑。

**驗證狀態：** 離線編譯 0 error；單元測試 ConfigTable 34／Controls 27／QuestLogic 46 全過。
Unity 實跑、手感、觸控實機**尚未測**，細節與驗收步驟見 `unity/JiuyaoTianxu/README.md` Phase 0-E 章節。

**給 ChatGPT 的建議：** HANDOFF-008 §17 的 Phase 0 全面 Code Review 可以把 0-E 一起納入；Claude 已先寫
預審版 `docs/PHASE0_TECH_REVIEW_CLAUDE_V1.0.md`（17 項技術債）。

**後續（同日）：** 依預審文件已修 F1～F5：觸控判斷、Server 輸入清洗、`AttackDefinition.InputMode` 取代
依武器類型判斷蓄力/施法（D2）、`monsters.csv`（D6）、**赤炎燃燒改為 `BurnStatus` 元件、怪物也會燃燒（D1，
咖哩裁定方案 A）**。D3 死亡/重生、D4 延遲補償、D5 Client 預測待 ChatGPT 審查後發 HANDOFF。

**本機實跑結果（2026-09-26）：** 與 CLAUDE-REPLY-008 同一次驗收，數字來自同一個最終版 build。

| Roadmap Phase 0 項目 | 證據 | 結果 |
|---|---|---|
| 資料表切換測試不需重新編譯 | `-ConfigDir` 把赤炎冷卻 3→12：`[ConfigOverride] applied` 2、赤炎觸發 43→18；兩次之間 build 檔時間戳不變 | ✅ |
| 目標鎖定（Server 權威） | `-LockOn` 自動測試 `[TargetLock]` 101 行，例外 0 | ✅ |
| 受擊判定 | 自動測試命中怪物 log：刀 19、劍 16、槍 46、重刃 24、靈杖 27；弓射出 28 箭但 `Projectile` 沒有命中 log，無法確認 | ✅（弓未確認） |
| 基礎回饋 | 咖哩 Editor 截圖可見傷害數字「3」與頭上血條；受擊閃紅沒有單獨確認；咖哩回報「測完了都可以按」 | ✅（閃紅未確認） |
| 赤炎燒怪（D1） | 赤炎打在怪物上 32 次；server.log 有 11 筆 `BurnStatus → DamageService` 的怪物扣血（例：`Logs/Final/Phase0D/server.log:8844`） | ✅ |
| 死亡重生測試版（D3） | 倒地 5／復活 5（改數值版 7／7） | ✅ |
| 觸控虛擬搖桿 | `-touchui`／手機實機**未測**。曾嘗試在桌機用 `-touchui` 自動點擊測試，但桌面上開著咖哩的個人視窗，為避免誤點而中止 | ⏳ |

**判斷：暫不標記 Phase 0-E COMPLETE**，唯一缺的是觸控實測（Roadmap 明列手機操作；兩指同時操作的雙搖桿只能在
手機上驗）。其餘項目都已實跑通過；觸控測完即可標記。

---

## [CLAUDE-REPLY-008]

**日期：** 2026-09-25

**對應 HANDOFF：** HANDOFF-008_PHASE0D.md

**狀態：** **COMPLETE**（2026-09-26 本機 Unity 實跑驗收通過，見本節最後「本機實跑結果」）

### 背景

咖哩指示「GPT 生圖工具還沒恢復，先繼續製作遊戲的部分」。本次在雲端 session 執行，
容器內**沒有 Unity Editor**，無法建場景、打包、跑 1 Server + 2 Client。依
HANDOFF-008 §12「不要只測本地單機就宣稱 Network Quest 完成」，本次只宣稱下列
已驗證的部分，其餘驗收項目等本機 Unity 實跑後再補 CLAUDE-REPLY-008 的結果段。

### 已完成（程式碼）

- **Map/Spawn**：`PlayerSpawnPoint`／`MonsterSpawnPoint` 場景標記；
  `NetworkGameLauncher` 由 Server 依 Index 輪流挑出生點（場景沒有標記時沿用
  Phase0A 舊佈局，Phase0A 場景行為不變）；`MonsterSpawner`（Server-only）每個
  出生點維持一隻怪，死亡消失後 2 秒重生。
- **Monster**：`Phase0D_TestMonster` = NetworkObject + NetworkTransform + 既有
  `Health`（HP 30，可調整）+ `EnemyIdentity`（TargetId 資料）+ `MonsterLifecycle`
  （Idle→受傷→死亡→0.5 秒後 Despawn）。**沒有第二套 Health/Damage**。
- **Combat → Quest 事件鏈**（§11，唯一動到的核心檔是 `DamageService`，只加
  「HP 由 >0 變 0 的那一擊」發一次事件，傷害計算一行未改）：
  `DamageService → CombatEvents.TargetKilled → CombatToGameplayEventRouter →
  GameplayEvents.EnemyKilled(targetId) → QuestTracker`。Combat 不知道 Quest，
  Quest 不碰 Health/DamageService、不搜尋場景怪物；沒有 EnemyIdentity 的目標
  （玩家互打）不算任務擊殺。
- **Quest Data-driven**：`QuestDefinition`（QuestNumId／QuestId／DisplayName／
  Description／ObjectiveType／TargetId／RequiredCount／PrerequisiteQuestNumId／
  RewardType／RewardId／RewardAmount）＋`QuestRegistry`。測試任務：
  `Q_PHASE0D_001 清理測試區`（擊敗 Phase0D_TestMonster × 3）、
  `Q_PHASE0D_002`（前置 001，× 5，用來驗證 Locked→Available 解鎖）。
- **State Machine**：`QuestStateMachine` 轉移表（Locked→Available→Accepted→
  InProgress→Completed），純 C# 無 Unity 依賴；全專案**沒有任何 `if (questId == ...)`**。
- **Network**：`PlayerQuestLog`（`NetworkArray<QuestEntry>`，每筆只有
  QuestNumId/State/Progress 三個 int）；Client 以 RPC 提出 Accept（2026-09-26 實跑後改為
  `ClientCommands`，見本節「本機實跑結果」），Server 驗證
  狀態後才改；Progress/Complete/Reward 全部 Server 決定。Client 端 `[QuestSync]`
  log 用來證明同步。
- **Reward**：只有測試用 `DebugRewardPoints` 計數器（未做 Inventory/Economy）。
- **Test**：`Phase0DTestRunner`（Server 端純觀察者，印 SUMMARY／PASS）、
  `-quitafter <秒>` 自動結束、`Tools/Phase0D/run_autotest.ps1`（1 Server + 2 Client、
  中途砍 client2 做 Join/Leave、彙整 log 計數）。
- **Editor**：`Phase0DSetup.Run`（建任務資產、怪物 prefab、Player prefab 加任務元件、
  `Phase0D_TestScene`、Build Settings）、`Phase0DBuild.Build`。

### 已驗證（本次雲端容器內能做到的）

1. **全部執行期程式碼編譯通過**：Roslyn C# 9（mono）+ UnityEngine 2021.3 參考組件 +
   專案內 Fusion 2.1.2 DLL，0 error 0 warning（Fusion.Unity 原始碼中 2 個類別以
   stub 代替）。已用故意寫錯的檔案確認此檢查會抓到錯誤。
2. **Editor 腳本**：除 `PrefabUtility.SaveAsPrefabAsset/LoadPrefabContents`（手邊參考
   組件是 2018.1 版太舊，Phase0ANetworkSetup 用同樣 API 在 Unity 6 已實際跑過）外，
   其餘全部通過型別檢查。
3. **任務邏輯單元測試 46/46 通過**（`Tools/QuestLogicTests`）：初始狀態、4 條合法
   轉移、全部非法轉移被拒且狀態不變、只有 InProgress 算擊殺、TargetId 不符不算、
   進度不超過需求數、完整 1/3→2/3→3/3→Completed、前置任務解鎖鏈。

### 尚未驗證（需在有 Unity 6000.5.5f1 的機器上跑）

- HANDOFF-008 §15 A（場景啟動/2 Client 同場景）、B（怪物 Server Spawn/可被既有
  戰鬥攻擊/死亡事件）、D（1 Server + 2 Client、Progress 同步、Join/Leave）、
  E 中的「Phase 0-C 靈印 Regression」、F（≥1 次完整 3 Kill 循環、≥10 次 Progress）。
- 執行步驟：README「Phase 0-D」章節（Setup → Build → `run_autotest.ps1`）。
- 可能需要現場微調的地方（先記下，不是已知 bug）：Fusion 對新 prefab 的自動註冊、
  headless 下 `Render()` 呼叫頻率（只影響 `[QuestSync]` log）、測試佈局距離
  （各武器是否都打得到中間怪）。

### Claude Code 意見

[接受]（範圍嚴守 §14：無正式地圖/AI/NPC/對話/任務編輯器/分支/失敗/UI/Inventory/
Economy。唯一超出最小需求的是第二個測試任務 Q_PHASE0D_002，理由是不做它就無法
驗證 Locked 狀態，仍屬 Skeleton 範圍）

### 是否需要 ChatGPT／使用者決策

需要咖哩：在本機 Unity 跑一次 Phase 0-D 驗收（或開一個有 Unity 的本機 Claude Code
session 代跑）。跑完把 `Logs/Phase0D/` 三份 log 或 `run_autotest.ps1` 的輸出貼回來，
Claude 會補上實跑結果並決定是否標記 COMPLETE。

### 下一步

- 本機實跑驗收 → 補結果 → 標記 Phase 0-D COMPLETE。（2026-09-26 已完成，見下）
- 依 HANDOFF-008 §17：Phase 0-D 完成後、進 Phase 1 青嵐城 Vertical Slice 前，
  需先由 ChatGPT 做 Phase 0 全面 Code Review／技術債清單。

### 本機實跑結果（2026-09-26，本機 Claude Code session 代跑＋咖哩手動）

**環境：** Windows 10、Unity 6000.5.5f1、Photon Fusion 2.1.2 stable build 2279（版本來源：
`unity/JiuyaoTianxu/Assets/Photon/Fusion/build_info.txt`；`Logs/setup0d.log` 載入 Fusion.Runtime 2.1.2.0）、
Windows Standalone（Mono）。依 `docs/PHASE0_LOCAL_VERIFICATION_RUNBOOK.md` 步驟 0～5 執行。以下數字全部來自
**最終版 build**（含下方修正；log 在 `unity/JiuyaoTianxu/Logs/Final/`，不進版控）。文件數字曾由沒看過過程的
獨立 agent 用 grep 逐一重算兩輪，之後依最終審查改了一次程式、全部重跑，數字已換成重跑結果。

**自動測試**（`run_autotest.ps1 -Seconds 150 -LockOn`）：1 Dedicated Server + 2 Client 是**同一台電腦上的 3 個
獨立行程**，經 Photon 雲端連線（有真實網路往返，但不是多台機器或手機網路）。

| §15 項目 | 證據 | 結果 |
|---|---|---|
| A 場景啟動、Player Spawn、2 Client 同場景 | StartGame 1、Player joined 2 | ✅ |
| B 怪物 Server Spawn、沿用 Health、可被攻擊、死亡事件 | 生成 68、EnemyKilled 65 | ✅ |
| C Accept／Progress／Complete | Client 送出請求 4、Server 收到 4、接任務 4（REJECTED 0）、Progress 16、001 完成 2、002 解鎖 2 | ✅ |
| D 1 Server + 2 Client、Server 權威、同步、Join/Leave | Client `[QuestSync]` 21／20、Player left 1；接任務由 Server 驗證狀態機，發送者由傳輸層決定 | ✅ |
| E-1 Combat → EnemyKilled → Quest Tracker | EnemyKilled 65 → Progress 16 | ✅ |
| E-2 不建立第二套 Damage/Health | 專案內 `Health.cs`／`DamageService.cs` 各只有一份；燃燒傷害也經 `DamageService`（server.log 呼叫鏈 `BurnStatus → DamageService.Resolve → Health.ApplyDamage`） | ✅ |
| E-3 Phase 0-C 靈印 regression | 赤炎 43、玄甲 6、影遁 armed 42／consumed 37（玄甲少於 0-C 的 10 次是死亡重生後的預期變化，見 README） | ✅ |
| F ≥1 次完整 3 Kill、≥10 次 Progress、自動／手動分開記錄 | PASS 1（2 名玩家完成）、Progress 16；手動測試另列於下 | ✅ |
| 例外 | Exception／NullReference 0 | ✅ |

**Host 模式**（headless `-netmode host -autotest`）：只有 Host——2 次請求 → 2 次接取，001、002 都完成；
Host＋1 個遠端 Client——Host 收到自己 `[Player:1]` 2 次、遠端 `[Player:2]` 2 次命令（遠端發送者是它自己的編號，
不是 None），兩名玩家都完成 001、002，PASS 1、REJECTED 0、例外 0。

**最終 build 回歸（commit `b178fc0`）：** 上面的數字來自 `a478bf2` 的 build。最終審查後又加了兩個防禦性修正
（取消註冊前比對處理者、runner 關閉時清掉註冊，不影響正常流程），在最終 build 重跑：1 Server + 2 Client 150 秒
PASS 1、Client 請求 4／Server 收到 4／接取 4、no handler 0、REJECTED 0、例外 0；Host＋遠端 Client 兩人都完成
001、002、PASS 1、例外 0（log 在 `Logs/Final2/`）。

**手動測試**（分開記錄，§15 F）：咖哩在 Editor 按 Play（截圖可見 `Host P1`）。先回報「看不出來現在拿什麼
武器」→ debug HUD 加一行「武器：劍（Tab 切換）」；請他重新 Play 後回報「測完了都可以按」（沒有逐項說明）。
⚠️ 手動測試時接任務還是改版前的做法；最終版的 Host 接任務只有上面 headless Host 測試驗過。

**實跑中發現並修正的問題：**

1. **接任務完全失敗。** 第一次 150 秒實跑，兩個 Client 每次請求都丟 `MethodAccessException`（該次報表合計
   268 筆；那次的 log 資料夾在後續重跑時被覆蓋，磁碟上已無留存）。根因：Fusion 2.1.2 的 weaver 在每個 `[Rpc]`
   方法插入對 `Fusion.Runtime` internal 方法的呼叫（`NetworkBehaviourUtils.CheckInvokeRpc`、
   `NetworkRunner.CreateRpcBuilder`、`NotifyRpcError`、`NetworkRunnerDebugRpcEvent.*`——掃描打包後
   `Assembly-CSharp.dll` 對 Fusion 的參照確認），Mono 打包版執行時做存取檢查而拒絕。QuestTracker 是專案第一個
   RPC，所以 0-A～0-C 沒踩到。嘗試紀錄：
   - (1) `[assembly: IgnoresAccessChecksTo("Fusion.Runtime")]`：無效（Unity 的 Mono 不支援，只有 .NET Core 認）。
   - (2) 請求放進 `PlayerInputData` 每 tick 送、Server 邊緣偵測：跑得過，但審查抓到**高風險 bug**——逾時後
     同一 Update 內對同一任務重送，Server 看不到變化而永遠忽略（autotest 下必現）。已放棄。
   - (3) **採用**：新增 `Core/ClientCommands`，用 Fusion 公開 API `SendReliableDataToServer`（可靠送達、送一次；
     Server 由傳輸層得知發送者，Client 無法冒充）。處理者按「(runner, 發送者, 命令)」註冊，命令只送到發送者本人
     的處理者（等於 RPC 的 `RpcSources.InputAuthority`，由 API 統一把關）。實測發現 Host 自己送的命令經 loopback
     回來時 sender 是 `PlayerRef.None`（官方文件沒寫）；最終版改成 Host 自己的命令直接在本機交給處理者、
     sender 是 None 的命令一律丟棄，不做「None 當成 Host」的推定（最終審查指出該推定無法證明安全）。
   HANDOFF-008 §9 只要求「Accept Request → Server Validate」，沒指定傳輸方式，Server 權威不變，**不算偏離規格**。
2. 測試腳本：PowerShell 5.1 以系統編碼寫 csproj，中文路徑變亂碼 → 加 `-Encoding UTF8`；
   `Player left` 檢查因 ConnectionTimeout 10 秒邊界太緊時有時無 → client2 改在結束前 35 秒砍。
3. CI：`validate_tables.py` 讀不懂 Unity 折行的長字串，誤報 quests.csv 不一致 → 修正解析，並用故意改錯的 CSV
   確認仍會抓到不一致。

### 三方審查紀錄（2026-09-26，咖哩授權自主進行）

| 角色 | 狀態 | 重點意見 | 處理 |
|---|---|---|---|
| Codex | ❌ 無法執行：登入 token 過期（需咖哩重新登入） | — | 以獨立 Claude 程式審查代替 |
| Gemini（llm-council） | ❌ 兩次都 503（Google 服務暫時不可用） | — | — |
| ChatGPT／DeepSeek／GLM（llm-council） | ✅ | 三者都指出嘗試 (2) 的重送 bug；三者都建議一次性命令的專案標準用 `SendReliableDataToServer` | 採用 → 嘗試 (3) |
| 獨立 Claude 程式審查 | ✅ | 嘗試 (2) 的重送 bug（高）、靜態信箱在同行程多 runner 會互蓋（中）、HUD 每次 OnGUI 都 GetComponent（低） | 前兩項隨改用 (3) 消失；HUD 改快取 |
| 獨立 Claude 文件查證（第 1 輪） | ✅ | 數字全部可重現；指出 268 無留存證據、E 列未附「無第二套 Damage」證據、手動測試描述超出咖哩原話、「實際網路」易誤讀 | 本節已全部修正 |
| 獨立 Claude 程式審查（最終版） | ✅ | (高)「sender 是 None 就當成 Host」無法證明安全，且沒測過 Host＋遠端 Client；(中) 發送者檢查要靠每個接收者自己寫，比 RPC 容易漏 | 改成 Host 本機直送＋None 一律丟棄；改成按發送者註冊處理者；補測 Host＋遠端 Client 通過 |
| 獨立 Claude 文件查證（第 2 輪） | ✅ | 25 項以上數字、4 處行號全部重現；手動測試描述沒有加碼；一句「A～F 全部有最終版證據」易誤讀成包含手動測試 | 已改寫；程式修改後全部重跑、數字已更新 |
| 獨立 Claude 最終驗收（第 3 輪） | ✅ 可交付 | 數字抽查全部吻合；(中) 斷線重連時舊物件取消註冊可能刪掉新註冊；(低) runner 關閉沒有統一清理；(不確定) Host 本機直送在 `Update` 裡改 `[Networked]` 狀態，Fusion 是否有限制查不到 | 前兩項已修（`b178fc0`）並回歸通過；第三項列為待裁定（見下） |

### Claude Code 意見（實跑後）

[接受] Phase 0-D 可標記 COMPLETE：§15 A～F 的自動測試項目都有最終版 build 的實跑證據（手動 Editor 測試是改版前
做的，最終版的 Host 路徑由 headless Host 測試補驗，見上）；G 文件（README、本節、CHANGELOG、版本紀錄、已知問題）
同步更新。

**請 ChatGPT 在 Phase 0 Code Review 時裁定：**
1. 目前**整個專案不能用 `[Rpc]`**（Mono 打包版）。一次性 Client→Server 命令暫以 `ClientCommands`
   （`SendReliableDataToServer`）處理——這是三個外部模型一致推薦的做法，但屬於**跨系統的架構約定，尚未寫進
   01_ARCHITECTURE_DECISIONS**，請裁定是否定為專案標準（之後的交易、組隊邀請、NPC 對話選項、商店都會用到）。
2. 手機正式打包會用 IL2CPP（不做這種存取檢查），`[Rpc]` 在手機上可能正常；但 Dedicated Server 若用 Mono 就不行。
   建議 Server 打包方式（Mono／IL2CPP）一起定案，或等 Photon 修正後重新驗證。
3. `ClientCommands` 的處理者在 `FixedUpdateNetwork` 之外執行（遠端命令在 Fusion 收資料的回呼裡、Host 自己的命令在
   `Update` 裡），並在那裡修改 `[Networked]` 狀態。實跑全部正常，但 Fusion 官方對「State Authority 在 tick 外改
   狀態」有沒有限制，我們查不到明文。若要當專案標準，建議改成「收到命令先排隊、在下一個 tick 處理」，或向 Photon 確認。

---

## [CLAUDE-REPLY-007]

**日期：** 2026-09-16

**對應 HANDOFF：** HANDOFF-007_PHASE0C.md

**狀態：** 完成

### 已完成

嚴格依HANDOFF-007完成條件(第14節A~F)逐項達成，未擴張範圍（未做8個靈印全部/
品質系統/3合1融煉/靈核經濟/靈印背包/正式UI/靈印強化/同屬性共鳴/連鎖組合/
雙脈重新加權/完整元素系統/完整Status Effect Framework/完整Shield Framework/
完整Dodge/裝備系統/六武器新增技能/Boss/PvP/MMO/商店抽卡商城等第13節明確禁止
項目）：

- **A. Data-driven**：`SpiritSealDefinition`(ScriptableObject)建立；固定8槽
  `SpiritSealLoadout`(`NetworkArray<int>`)建立；赤炎/玄甲/影遁三個皆為資料
  資產；已用實跑測試證明修改靈印參數不需要修改`CombatController`（見下方
  Data-driven驗收）。
- **B. Combat Integration**：Trigger/Modifier管線建立於`SpiritSealSystem`；
  沿用既有`DamageService`（新增三個通用Hook呼叫，非另建管線）；未建立第二套
  Health/Damage權威入口——赤炎的燃燒DoT也走同一條`DamageService.Resolve()`。
- **C. Prototype**：赤炎可觸發（20次）、玄甲可保命一次（10次，且驗證了
  HP=0後的邊界情況）、影遁可Armed→消耗（36次武裝/26次消耗）、Cooldown對三者
  皆生效（以`TickTimer`+`Runner`權威時間為準）。
- **D. Network**：1 Server+2 Client、Server Authority（`DamageService`內
  `Object.HasStateAuthority`把關）、必要狀態同步（兩Client皆確認裝備靈印
  1/2/3）、Client無法直接改靈印戰鬥結果。
- **E. Test**：赤炎20/玄甲10/影遁36+26，每項皆≥10，總數遠超30；Join/Leave
  regression通過；Data-driven修改測試完成（見下方）。
- **F. Documentation**：見下方修改文件清單，`unity/JiuyaoTianxu/README.md`
  新增完整Phase0-C章節，CHANGELOG已更新。

### 修改文件

- `unity/JiuyaoTianxu/Assets/_Project/Combat/Framework/SpiritSeals/`（新增6
  個檔案）：SpiritSealTriggerType.cs、SpiritSealDefinition.cs、
  SpiritSealRegistry.cs、SpiritSealIds.cs、SpiritSealLoadout.cs、
  SpiritSealSystem.cs。
- `unity/JiuyaoTianxu/Assets/_Project/Combat/Framework/DamageTypes.cs`
  （修改：`DamageRequest`新增`FlatDamageOverride`/`IsStatusDamage`欄位）。
- `unity/JiuyaoTianxu/Assets/_Project/Combat/Framework/DamageService.cs`
  （修改：新增三個Spirit Seal Hook呼叫，`CombatController.cs`**零修改**）。
- `unity/JiuyaoTianxu/Assets/_Project/Core/PlayerInputData.cs`（新增
  `DodgeTest`測試按鍵）、`KeyboardInputProvider.cs`（綁E鍵）、
  `AutoTestInputProvider.cs`（新增獨立於武器循環的定期Dodge測試按鍵）。
- `unity/JiuyaoTianxu/Assets/_Project/Net/NetworkGameLauncher.cs`（修改：
  玩家生成後自動裝備測試靈印組合）。
- `unity/JiuyaoTianxu/Assets/_Project/Editor/Phase0CSpiritSealDataSetup.cs`
  （新增）：建立三個靈印資產＋Registry。
- `unity/JiuyaoTianxu/Assets/_Project/Editor/Phase0ANetworkSetup.cs`
  （修改）：Player prefab新增SpiritSealLoadout+SpiritSealSystem，接線
  Registry參照。
- `unity/JiuyaoTianxu/Assets/_Project/Combat/SpiritSeals/`（新增）：
  赤炎.asset、玄甲.asset、影遁.asset、SpiritSealRegistry.asset。
- `unity/JiuyaoTianxu/README.md`（更新：完整Phase0-C章節）。

### 哪些資料已 Data-driven

`SpiritSealDefinition`（SealId/DisplayName/Equipable/TriggerType/Cooldown/
赤炎的Burn三參數/玄甲的FatalSaveMinHp/影遁的ArmsForNextAttack+
BonusDamageWhenArmed）皆為ScriptableObject欄位，`SpiritSealSystem`只透過
`SpiritSealRegistry.GetById(int)`查表讀取，沒有任何`if (sealId == 特定值)`
的寫死分支。

### Server Authority 如何維持

延續Phase0A/B的guard模式：
1. `SpiritSealLoadout.TryEquip`/`Unequip`皆guard在`Object.HasStateAuthority`。
2. `SpiritSealSystem`的三個Hook方法（`ModifyOutgoingDamage`/
   `OnAttackHitDealt`/`TryPreventFatalDamage`）只被`DamageService.Resolve`
   呼叫，而`DamageService.Resolve`本身已經guard在
   `request.Target.Object.HasStateAuthority`——即靈印邏輯天生只會在Server
   端執行，Client端無法繞過。
3. Cooldown/Armed狀態皆為`[Networked]`欄位（`NetworkArray<TickTimer>`/
   `NetworkArray<NetworkBool>`），只有Server寫入，Client只能讀取同步結果。

### 測試方式與測試結果（實跑證據）

沿用Phase0A/B的1 Server+2 Client headless模式。完整結果與Data-driven驗收
記錄在`unity/JiuyaoTianxu/README.md`「Phase 0-C」章節，摘要：

- 主測試（單次~45秒）：赤炎觸發20次、玄甲觸發10次、影遁武裝36次/消耗26次，
  三個Prototype皆≥10次，總數遠超≥30。全程`Exception`/`NullReference`/
  `Unhandled`：0命中。兩Client皆正常連線並確認裝備靈印。手動終止Client，
  Server正確觸發`Player left`未崩潰。
- **Data-driven驗收**（HANDOFF-007第10節要求）：赤炎`Cooldown`原型值3秒，
  同一~20秒窗口觸發14次；**只修改ScriptableObject資產**（`赤炎.asset`的
  `Cooldown`欄位改成12，**未觸碰任何.cs檔案**），重新打包後同樣~20秒窗口
  觸發降為5次——變化方向與量級符合預期，證實「改資料不改核心程式」可以
  改變測試結果。驗證後已改回Cooldown=3的原型值並重新打包確認建置正常。

### 是否有 blocker

否。

### 發現的問題（除錯過程與設計限制，誠實記錄）

**問題A（已修正，屬於會讓正式Build失敗的錯誤）**：實作過程中一度讓執行期
腳本`NetworkGameLauncher.cs`直接引用`Phase0CSpiritSealDataSetup`（一個
Editor-only腳本，位於`Assets/_Project/Editor/`資料夾）裡定義的常數。
Editor腳本不會被打包進正式Player build，這樣寫在Unity Editor裡可以正常
編譯（因為Editor組件在編輯器環境中可見），但實際執行`Phase0ABuild.Build`
打包Standalone Player時就會編譯失敗——因為Player build會排除Editor資料夾
的程式碼。**修法**：把三個靈印的穩定id常數抽到獨立的執行期類別
`SpiritSealIds.cs`（放在`Combat/Framework/SpiritSeals/`而非`Editor/`），
執行期程式碼與Editor設定腳本都改引用這個共用類別。修正後兩邊都編譯通過
（已用`Phase0ABuild.Build`實際打包驗證過，不是只在Editor裡確認）。這是
提醒自己以及未來接手的人：**任何要進正式Build的程式碼，都不能引用
`Assets/_Project/Editor/`底下的任何類別**，這條規則本次之前沒有明文寫下
來，藉這次事故正式記錄。

**問題B（設計限制，非bug，已在README記錄）**：Phase0-A/B從未實作HP回復
機制，HP降到0後永久停在0。這代表玄甲在角色第一次被保命後，若冷卻中又受到
攻擊，HP會真的觸底停在0；冷卻結束後再受攻擊，`TryPreventFatalDamage`因為
`currentHp(0) - incomingDamage > 0`恆為假，仍會判定為「致命」並再次觸發，
但實際上只是把已經是0的HP再夾一次到`FatalSaveMinHp`附近，是Prototype簡化
下的合理副作用而非邏輯錯誤——正式版本需要搭配HP回復或重生機制才有完整
意義，本階段依HANDOFF-007禁止範圍故意不做。

### Claude Code 意見

[接受]（HANDOFF-007任務設計與驗收標準清楚可執行，已依指示逐項完成並提供
實跑證據，包含額外完成的Data-driven修改驗收；問題A已修正並記錄成通用
規則供未來參考，問題B是明確在允許範圍內的Prototype簡化，皆不影響整體
驗收結果）

### 是否需要 ChatGPT／使用者決策

否（Phase 0-C已完成，可以進入HANDOFF-007第15節之後的下一階段：Phase 0-D
Map/Spawn/Quest Skeleton，或依19_DEVELOPMENT_ROADMAP的既定順序。若要開始，
需要ChatGPT發下一個HANDOFF任務單，或使用者直接指示開始）

### 下一步

- 等待ChatGPT/使用者指派Phase 0-D任務。
- HANDOFF-003/004遺留的兩個待確認問題（17技術架構的Cinematic建議、手機
  效能預算基準）仍未收到回覆，維持開放狀態，不影響後續Phase可以開始。
- 建議未來排入待辦：把問題A記錄的「Editor程式碼不可被執行期程式碼引用」
  規則正式寫進專案的技術規範文件（例如17_TECH_ARCHITECTURE或本README），
  避免未來其他協作者重踩同一個坑。

---

## [CLAUDE-REPLY-006]

**日期：** 2026-09-16

**對應 HANDOFF：** HANDOFF-006_PHASE0B.md

**狀態：** 完成

### 已完成

嚴格依HANDOFF-006第13節「建議實作順序」0-B-01~14一步一步做，未跳步、未提前
擴張範圍（未做雙修切換/完整閃避格擋/完整Boss機制/元素系統/暴擊系統/完整裝備/
靈印/養成/PvP排位/MMO大世界/公會/經濟/商城/抽卡/正式美術動畫/完整手機UI等
第11節明確禁止項目）：

- **0-B-01 Combat Framework骨架**：新增`Assets/_Project/Combat/Framework/`，
  `CombatController`取代Phase0A的`PlayerCombat`成為唯一戰鬥腳本。
- **0-B-02 Attack Definition資料驅動**：`AttackDefinition`/`WeaponDefinition`
  皆為ScriptableObject，六武器數值完全在資料資產裡，`CombatController`不含
  任何硬編碼武器數字。
- **0-B-03 Combo/State Machine**：`CombatState`獨立持有`Phase`/`ComboStep`/
  `PhaseTimer`/`ComboWindowTimer`等網路化狀態。
- **0-B-04 Hit Detection抽離**：`HitDetectionService`是唯一呼叫
  `Physics.Overlap*`的地方，支援Sphere/Box/Capsule/Area，Projectile另走
  獨立NetworkObject逐tick判定。
- **0-B-05 Damage Service**：`DamageService`是唯一寫入`Health`的地方，
  `DamageRequest`→`DamageResult`責任邊界清楚。
- **0-B-06~11 六大武器**：刀(3段近戰+末段重擊擊退)、劍(5段快攻可邊移動)、
  槍(中距離Capsule突刺+破甲擊退)、弓(Hold→Charge→Release→Projectile)、
  重刃(慢速大AOE+簡單霸體概念)、靈杖(Cast延遲→定點AOE)。
- **0-B-12 六流派對照測試**：1 Server+2 Client headless自動輪替全部六武器。
- **0-B-13 Server Authority/Network Regression**：確認Phase0A已驗證的連線/
  移動/同步能力未被破壞，且新增的戰鬥邏輯全部維持Server權威。
- **0-B-14 文件與測試報告**：見下方與`unity/JiuyaoTianxu/README.md`。

### 修改文件

- `unity/JiuyaoTianxu/Assets/_Project/Combat/Framework/`（新增8個檔案）：
  WeaponType.cs、CombatPhase.cs、HitShapeType.cs、AttackDefinition.cs、
  WeaponDefinition.cs、DamageTypes.cs、HitDetectionService.cs、
  DamageService.cs、CombatState.cs、CombatController.cs、Projectile.cs。
- `unity/JiuyaoTianxu/Assets/_Project/Combat/PlayerCombat.cs`（**刪除**，
  被CombatController取代）。
- `unity/JiuyaoTianxu/Assets/_Project/Combat/PlayerMovement.cs`（修改：改讀
  `CombatController.MoveSpeedMultiplier`而非寫死移動速度）。
- `unity/JiuyaoTianxu/Assets/_Project/Core/PlayerInputData.cs`（修改：新增
  `SwitchWeapon`按鍵）、`KeyboardInputProvider.cs`（新增Tab鍵切武器）、
  `AutoTestInputProvider.cs`（重寫，見下方「發現的問題」）。
- `unity/JiuyaoTianxu/Assets/_Project/Net/NetworkGameLauncher.cs`（修改：
  生成玩家改為面對面朝向；`OnInput`改用`runner.Tick.Raw`驅動測試輸入節奏）。
- `unity/JiuyaoTianxu/Assets/_Project/Editor/Phase0BWeaponDataSetup.cs`
  （新增）：建立六把武器的資料資產。
- `unity/JiuyaoTianxu/Assets/_Project/Editor/Phase0ANetworkSetup.cs`
  （修改）：Player prefab改組裝CombatState+CombatController，新增
  Projectile prefab建置，並把六個WeaponDefinition+Projectile prefab接線
  進CombatController。
- `unity/JiuyaoTianxu/Assets/_Project/Combat/Weapons/`（新增）：六個資料夾，
  每個含1個WeaponDefinition＋對應AttackDefinition資產。
- `unity/JiuyaoTianxu/Assets/_Project/Net/Prefabs/Projectile.prefab`（新增）。
- `unity/JiuyaoTianxu/README.md`（更新：Phase0-B完整進度表/Framework分層/
  六武器辨識度設計表/測試結果/已知問題）。

### Framework 如何分層

見`unity/JiuyaoTianxu/README.md`「Framework分層」一節的圖示，摘要：
`CombatController`（唯一戰鬥腳本）讀輸入→驅動`CombatState`（純狀態）→
向`HitDetectionService`或`Projectile`要「打到誰」→交給`DamageService`
（唯一寫Health的地方）→`Health`。六武器差異100%來自
`AttackDefinition`/`WeaponDefinition`資料，`CombatController`裡沒有任何
「if WeaponType==Blade then...」這類武器專屬分支邏輯（唯二例外：Bow/Staff
因為機制本質不同——蓄力/吟唱——需要`WeaponType`判斷走`Charging`/`Casting`
分支而非`AttackStart`分支，這是機制差異不是數值分支，符合HANDOFF-006
「可以有少量武器專屬行為，但不能複製整套戰鬥架構」的允許範圍）。

### 哪些資料已 Data-driven

`AttackDefinition`（AttackId/WeaponType/ComboStep/Damage/Startup-Active-
Recovery-Cooldown時間/ChargeOrCastTime/CanCombo/ComboWindow/
CanMoveDuringAttack+MoveSpeedMultiplier/ResourceCost/HitShape+Range+
HitExtents+ProjectileSpeed+AreaRadius/Knockback+ArmorBreak+SuperArmor
等旗標）與`WeaponDefinition`（WeaponType/ComboSequence/BaseMoveSpeed）
皆為ScriptableObject，六武器共18個攻擊步驟資產+6個武器資產，全部由
`Phase0BWeaponDataSetup.cs`一次性程式碼建立（供之後美術/數值人員直接
在Inspector調整，不需要改程式碼）。

### Server Authority 如何維持

延續Phase0A的guard模式，新增兩層：
1. `CombatController`所有邏輯（含新增的武器切換、連段、蓄力/吟唱判斷）
   都在`if (!Object.HasStateAuthority) return;`之後才執行，Client端
   只能送出input，不能決定任何戰鬥結果。
2. `DamageService.Resolve`額外對`target.Object.HasStateAuthority`做
   二次檢查才寫入HP——即使未來有人不小心在guard外的路徑呼叫
   `DamageService`，這道檢查仍會擋下並記警告，不會真的把傷害套用到
   非權威端。
3. `Projectile`同樣guard在`Object.HasStateAuthority`才執行飛行/命中/
   造成傷害邏輯。

### 測試方式與測試結果（0-B-12/13，實跑證據）

沿用Phase0A的1 Server+2 Client headless驗收模式，完整結果與過程記錄在
`unity/JiuyaoTianxu/README.md`「測試方式與結果」一節，摘要：
- 六種武器全部切換並攻擊過（Blade×17/Sword×31/Spear×18/Bow×12/
  HeavyBlade×6/Staff×6次組合啟動）。
- 27次命中，傷害隨武器不同（8~22），證實資料驅動生效非同一套寫死數字。
- 12次Projectile成功發射（Bow release、Staff cast皆會經過）。
- HP正確遞減並在0 clamp，無負值。
- 手動終止Client，Server正確觸發`Player left`，未崩潰。
- 全程搜尋`Exception`/`NullReference`/`Unhandled`：0命中。
- Phase0A已驗證的連線/移動/同步能力未被破壞。

### 是否有 blocker

否。

### 發現的問題（除錯過程誠實記錄，非隱藏）

本次除錯過程比Phase0-A更曲折，記錄完整過程供未來參考：

**問題A（已修正，過程中最花時間的一個）**：第一版`AutoTestInputProvider`
用`Time.frameCount`（Unity畫面更新幀數）安排「攻擊一陣子→靜置一陣子
（保證回到Idle）→按切換武器鍵」的節奏。實測時測試進程卡在劍（Sword）
超過30分鐘沒有任何進展，log檔長到4億多bytes。排查後發現：`-nographics`
無視窗headless進程裡，Unity的Update幀率跟Fusion的固定模擬tick完全
脫鉤，導致同一份輸入被套用到大量網路tick上；劍的5段快速連段節奏又剛好
跟原本設計的攻擊按鍵週期共振，使戰鬥狀態永遠回不到Idle，而切換武器的
規則是「只能在Idle時切換」，因此永久卡死在劍上。**修法**：改用
`NetworkRunner.Tick.Raw`（透過反射工具直接查證`Fusion.Runtime.dll`確認
存在此屬性，非憑記憶猜測）取代`Time.frameCount`，讓測試輸入節奏跟
`CombatController`內部`TickTimer`計時用的是同一個時鐘基準，問題排除。
修正前後都是靠實際跑測試、看log找證據排查，中間卡住時沒有跳過重試就
直接宣稱測試通過。

**問題B（已修正）**：兩個測試玩家生成時面朝同一方向（Phase0A遺留的
`Quaternion.identity`），而近戰命中判定的Sphere/Capsule是往「面朝方向」
偏移的，導致兩玩家實際上打不到彼此（第一輪測試0次命中）。已修正
`NetworkGameLauncher`讓生成的兩名玩家面對面。

**問題C（已修正）**：靈杖第一版Range=6/AreaRadius=2.5，相對測試用的1.5
單位生成間距，施法點會直接飛越目標打不到人。已調整為Range=2.5/
AreaRadius=2，正式數值仍待Phase 1實際地圖手感調整（已在README標註）。

以上三項都是**Phase 0-B測試工具本身的瑕疵**，不是Combat Framework或
六武器邏輯錯誤——排查過程完全基於實際log證據，符合「不自驗、要有實跑
證據」的要求。

### Claude Code 意見

[接受]（HANDOFF-006任務設計與驗收標準清楚可執行，已依指示逐步完成並
提供實跑證據；三個除錯過程中的問題都已修正並誠實記錄根因，不影響
Phase0-B整體驗收結果）

### 是否需要 ChatGPT／使用者決策

否（Phase 0-B已完成，可以進入HANDOFF-006第15節指定的下一階段：Phase 0-C
Spirit Seal Data-driven Prototype。若要開始，需要ChatGPT發下一個HANDOFF
任務單，或使用者直接指示開始）

### 下一步

- 等待ChatGPT/使用者指派Phase 0-C任務（靈印資料驅動原型：Spirit Seal
  Definition→Equip/Unequip→Combat Modifier→Server Authority→可切換
  測試內容，呼應04已定案靈印系統）。
- HANDOFF-003/004遺留的兩個待確認問題（17技術架構的Cinematic建議、手機
  效能預算基準）仍未收到回覆，維持開放狀態，不影響Phase 0-C可以開始。

---

## [CLAUDE-REPLY-005]

**日期：** 2026-09-16

**對應 HANDOFF：** HANDOFF-005_PHASE0A.md

**狀態：** 完成

### 已完成

嚴格依HANDOFF-005第5節「建議執行順序」1~15步一步一步做，沒有跳步或提前擴張範圍
（未做六大武器完整系統/雙修切換/完整靈印/裝備/任務/公會/經濟/PvP/副本/世界BOSS/
完整劇情/正式美術/完整動畫等第3節明確禁止項目）：

1. Unity 6000.5.5f1 + URP 17.5.0專案建立（上一輪已完成）。
2. Photon Fusion 2.1.2 (stable)套件匯入，咖哩透過Unity Asset Store取得
  `.unitypackage`提供（上一輪已完成，本輪修正編譯錯誤：Fusion Statistics面板
  依賴舊版uGUI，補上`com.unity.ugui`後編譯通過無錯誤）。
3. Network Runner基礎連線：手寫`NetworkGameLauncher`（未使用Fusion官方
  FusionBootstrap範例，避免Combat/Core跟Photon demo程式碼耦合），依`-netmode`
  命令列參數啟動Server/Client/Host。
4. 玩家移動：`PlayerMovement`讀取`PlayerInputData`（INetworkInput），
  Server權威下依輸入更新位置。
5. 玩家可見性與同步：`NetworkTransform`同步位置，`OnPlayerJoined`/`OnPlayerLeft`
  正確處理玩家加入/離開。
6. 基礎戰鬥：`PlayerCombat`做攻擊意圖→Sphere Overlap命中判定→`Health.ApplyDamage`。
7. Server Authority：`Health`/`PlayerMovement`/`PlayerCombat`所有狀態修改皆guard
  在`Object.HasStateAuthority`；玩家生成邏輯guard在`runner.IsServer`；未採用
  「Client自算傷害再告訴Server」的禁止架構。
8. 穩定性測試：見下方「實跑證據」。

### 修改文件

- `unity/JiuyaoTianxu/Assets/_Project/Core/`（新增）：PlayerInputData.cs（INetworkInput
  結構）、KeyboardInputProvider.cs（本機鍵盤輸入）、AutoTestInputProvider.cs
  （headless驗收用的程式化按鍵模擬，見下方說明）。
- `unity/JiuyaoTianxu/Assets/_Project/Net/NetworkGameLauncher.cs`（新增）：Fusion
  啟動器與完整`INetworkRunnerCallbacks`實作（19個方法簽章皆用反射工具直接讀取
  `Fusion.Runtime.dll`驗證過，不是憑訓練記憶猜測——Fusion 2的callback介面/
  `StartGameArgs`欄位/`NetworkInput.Set<T>()`等API細節每版可能微調，查證後才動手
  比較不會寫出編譯不過的架構）。
- `unity/JiuyaoTianxu/Assets/_Project/Combat/`（新增）：Health.cs、PlayerMovement.cs、
  PlayerCombat.cs。
- `unity/JiuyaoTianxu/Assets/_Project/Editor/Phase0ANetworkSetup.cs`（新增）：
  建立Player/NetworkRunner預製物並接線進測試場景。
- `unity/JiuyaoTianxu/Assets/_Project/Editor/Phase0ABuild.cs`（新增）：打包Windows
  Standalone供headless驗收用。
- `unity/JiuyaoTianxu/Assets/_Project/Net/Prefabs/Player.prefab`、
  `NetworkRunner.prefab`（新增）。
- `unity/JiuyaoTianxu/README.md`（更新）：完整進度表與驗收結果記錄。

### 使用哪個 Unity 版本 / Photon Fusion 版本

Unity Editor 6000.5.5f1；Photon Fusion 2.1.2 (stable, build 2279)。

### 測試方式與測試結果（0-A-08穩定性測試，實跑證據）

**測試方式**：因為要驗的是01_ARCHITECTURE_DECISIONS已定案的Dedicated Server拓樸
（不是Editor內Host模式），打包成Windows Standalone後啟動3個獨立headless進程：
1個Server + 2個Client（`-batchmode -nographics -netmode server/client -autotest`）。
`-autotest`旗標啟用`AutoTestInputProvider`——因為無視窗的headless進程收不到真實
鍵盤輸入，改用程式碼以固定節奏（0.5秒按、0.5秒放）模擬攻擊鍵，這是Phase0-A
headless驗收專用的測試替身，不是正式輸入方案（正式鍵盤/手機輸入走
`KeyboardInputProvider`，兩者共用同一個`PlayerInputData`抽象層，未來接手機
虛擬搖桿只需再加一個Provider，不用動Net/Combat）。

**測試結果**：
- 2個Client都成功連線（`StartGame succeeded as Client`／`Connected to server`），
  Server記錄兩名玩家加入（`Player joined: [Player:2]`、`[Player:3]`），兩個Client
  互相看見對方（各自的Local/Remote玩家對應正確）。
- **324次**完整的Attack→Hit→Damage→HP Sync循環（遠超過驗收標準的10次），HP從
  100正確遞減，並在`Mathf.Max(0, HP - amount)`處clamp在0（觀察到641次「HP now 0」
  紀錄，無負值，clamp邏輯正確）。
- 手動終止其中一個Client程序，Server正確觸發`Player left: [Player:3]`，未崩潰
  未卡死。
- Server log全程搜尋`Exception`/`NullReference`/`Unhandled`關鍵字，除了已知的
  本機Editor授權握手警告（與Fusion連線無關）外，無其他錯誤紀錄。

### 是否存在 blocker

否（Phase 0-A本次任務範圍已全部完成，無阻塞項）。

### 發現的問題（誠實記錄除錯過程，非隱藏）

**問題A（已排查並解決，過程記錄供未來參考）**：第一次用1 Server+1 Client測試時，
0次命中——排查後發現Dedicated Server模式下Server本身不生成本地玩家（這是預期
行為，Dedicated Server本來就不該有本地玩家），所以1 Server+1 Client組合下場上
只有1名玩家、打不到任何目標。改用1 Server+2 Client（2名玩家）後正常命中。這不是
程式錯誤，是我測試設計一開始沒考慮到的組合問題，已修正。

**問題B（已排查並解決，Unity Editor腳本已知坑，值得記錄避免未來重踩）**：
`Phase0ANetworkSetup.cs`第一版把`PrefabUtility.SaveAsPrefabAsset`回傳的物件
參照直接傳給`WireLauncherIntoScene`，結果場景檔裡`_runnerPrefab`/`_playerPrefab`
欄位存成`{fileID: 0}`（空引用）——三輪診斷後查明：`EditorSceneManager.OpenScene`
切換場景這個動作本身會讓「切換前拿到的」prefab物件參照失效，即使是剛存檔、剛用
`AssetDatabase.LoadAssetAtPath`重新讀取的參照也一樣，只要是在`OpenScene`呼叫
「之前」取得的都會失效。修法：改成先呼叫`OpenScene`，場景切換完成「之後」才用
`AssetDatabase.LoadAssetAtPath`依路徑讀取prefab。這是本次唯一花比較多輪次排查
的問題，已在README與此處記錄根因，避免未來寫類似Editor腳本時重踩。

### Claude Code 意見

[接受]（Phase 0-A本身沒有需要反對或建議修改的地方，HANDOFF-005的任務設計與
驗收標準都清楚可執行，已依指示逐步完成並提供實跑證據，不是憑印象宣稱完成）

### 是否需要 ChatGPT／使用者決策

否（Phase 0-A已完成，可以進入HANDOFF-005第8節指定的下一階段：Phase 0-B六大
武器路線最小可玩戰鬥Loop。若要開始，需要ChatGPT發下一個HANDOFF任務單，或使用者
直接指示我按19_DEVELOPMENT_ROADMAP的既定範圍開始）

### 下一步

- 等待ChatGPT/使用者指派Phase 0-B任務（六流派最小可辨識戰鬥循環，呼應19已定案
  範圍：刀/劍/槍/弓/重刃/靈杖）。
- HANDOFF-003/004遺留的兩個待確認問題（17技術架構的Cinematic建議、手機效能
  預算基準）仍未收到回覆，維持開放狀態，不影響Phase 0-B可以開始。

---

## [CLAUDE-NOTE-005]（非HANDOFF任務，咖哩直接請求，未經ChatGPT發起）

**日期：** 2026-09-16

**對應項目：** 無對應HANDOFF編號——咖哩在社群平台看到MiniMax H3/Veo 3/Seedance
三款生成式影片模型的比較貼文，直接請我評估是否對動畫製作有幫助，非ChatGPT
發起的正式交接任務。依本文件第2節「非同步工作方式」精神，重大內容變更仍應
留下紀錄，故補寫本筆讓ChatGPT下次讀取時知道23文件被改過。

### 已完成

- 用WebSearch查證MiniMax H3（本地部署，2K/24fps/含音訊，約US$0.13/秒）、
  Seedance（ByteDance雲端服務，約US$0.14/秒，480p~4K依解析度$0.067~$0.78/秒）、
  Veo 3（Google雲端服務）三款模型的技術規格/定價/授權。
- 判定：這三款屬於「文字/圖片直接生成整段影片」的技術路線，跟本專案`23_ANIMATION_
  PRODUCTION_PIPELINE_V1.0.md`既定的Blender+動作捕捉+Unity Cinematic路線完全不同，
  無法保證角色跨鏡頭一致性，牴觸文件核心原則「固定角色+固定場景+Shot-by-Shot製作」，
  不能用於需要辨識六位主要角色的劇情鏡頭。
- 經咖哩同意後，已將三款模型補進`23_ANIMATION_PRODUCTION_PIPELINE_V1.0.md`第2.2節，
  定位為**Tier 3輔助工具**（不影響既有Tier 1/2工具鏈），僅限：不需角色一致性的
  空鏡/氛圍過場鏡頭、短影音花絮素材、前期分鏡/運鏡概念驗證。
- 已加註授權提醒：MiniMax H3開源權重的社群授權**明確排除美國/歐盟/英國/南韓**商用，
  本地部署前需先確認台灣是否受限；Seedance需透過ByteDance官方API或付費訂閱方案
  才有明確商用授權。
- 已更新`docs/CHANGELOG.md`記錄本次變更。

### 修改文件

- `docs/23_ANIMATION_PRODUCTION_PIPELINE_V1.0.md`（修改：新增第2.2節「輔助工具：
  生成式影片模型」，工具鏈總覽表新增一列）
- `docs/CHANGELOG.md`（新增紀錄）

### Claude Code 意見

[接受，定位為輔助工具]——這三款模型技術上可行、成本低（US$8~13可做完一支
60~90秒空鏡素材），但**不建議、也未被允許用於主線角色動畫**，因為會直接牴觸
23文件已定案的角色一致性原則，這點已在文件內明確寫死，避免後續被誤用在
需要角色一致性的正式劇情鏡頭上。

### 風險等級

低（僅新增輔助工具選項，未修改任何既有Tier1/2工具鏈或正式規格，且已明確限定
使用範圍避免誤用）

### 是否需要 ChatGPT／使用者決策

否（本次為低風險的輔助工具補充，已直接執行；若ChatGPT認為這類生成式影片模型
應該有更明確的角色，歡迎在下一輪回覆中提出調整）

---

## [CLAUDE-REPLY-004]

**日期：** 2026-09-15

**對應 HANDOFF：** HANDOFF-004_ANIMATION_PIPELINE.md

**狀態：** 完成

### 執行方式說明

本次任務要求逐項查證5款AI配音工具與多款動作捕捉工具的真實授權/成本/能力，屬於
「需查證多個外部來源」的研究工作，我沒有憑訓練知識直接回答，而是派了一個獨立
研究agent用WebSearch/WebFetch逐項查證官方定價頁/授權文件，完整查證過程與來源
連結存檔在 `docs/HANDOFF-004_RESEARCH_NOTES.md`（供之後複查來源用，非正式規格
文件）。以下回覆是我整合查證結果後的結論。

### 最終推薦 Pipeline

```text
建模／綁骨 → Blender
動作（泛用：走路/跑步/待機）→ Mixamo（免費商用無額度限制，查證後新增）
動作（角色專屬招式）→ Rokoko Studio（免費層額度極低，建議搭配Basic付費方案）
動作整理／清理 → Blender NLA/Graph Editor
Unity 導入 → FBX＋Humanoid Avatar（glTF僅建議用於靜態道具，動畫角色不夠成熟）
Cinematic → Unity Timeline＋Cinemachine 3（免費內建，足以支撐演出需求）
配音 → Chatterbox Multilingual V3／CosyVoice（Tier1，中文表現佳且可商用）；
        ElevenLabs作為小量高品質正式片段的付費保底
剪輯 → DaVinci Resolve Free
發布 → YouTube／Shorts／抖音
```

與23文件原本的Pipeline方向一致，本次查證只在「動作捕捉」與「配音成本試算」兩處
補充/修正細節，沒有推翻整體流程設計。

### 工具分級

**Tier 1（現在直接使用）**：Blender、Unity Personal+Timeline+Cinemachine、
Mixamo（查證後新增）、Chatterbox Multilingual V3。

**Tier 2（必要時使用）**：Rokoko Studio（免費層額度不夠時升Basic付費）、
DeepMotion付費層（免費層禁止商用，不算Tier1）、Plask、CosyVoice（Apache 2.0
可商用但情緒表現需EP00實測）、Kokoro（授權最乾淨但情緒/中文聲音選擇有限，
適合NPC/系統音非主要角色）、ElevenLabs（商用保底）、Fish Audio官方hosted API
（可行但中文計費需先試算）。

**Tier 3（暫不使用）**：DeepMotion免費層（單獨列出提醒：僅供個人非商業用途，
不能用於會公開營利的素材）、Fish Speech/Fish Audio本地模型（Research License
明確禁止商用，除非另簽書面商業授權）。

### AI 配音評估（5款，詳細14維度比對見RESEARCH_NOTES）

- **Chatterbox Multilingual V3**：MIT授權、中文CER 0.41%表現佳、本地可跑、
  $0成本——23A草案評估準確，唯一補充：輸出音檔預設帶PerTh浮水印，一般用途無感，
  高保真後製前建議先實測。
- **Fish Speech/Fish Audio**：23A草案這部分寫得最準確——本地模型Research
  License明確禁止商用需另簽授權，草案已正確識別並警示。**查證補充**：官方
  hosted API採UTF-8 bytes計費，中文字每字約3 bytes、英文約1 byte，實際中文
  配音成本會比英文範例換算貴2~3倍，這點23A沒提到，已補進23A文件。
- **ElevenLabs**：Starter（$6/月）起含商用授權，草案方向正確。**修正**：
  Creator草案寫$22/月，查證確認$22是首月促銷價，常態價$11/月，已修正23A文件
  避免預算誤算。
- **CosyVoice**：最新主線版本（CosyVoice2/Fun-CosyVoice3）程式碼與模型權重
  皆為Apache 2.0可商用，23A「不應只因Apache 2.0就忽略模型權重授權」的謹慎
  提醒經查證確實有依據（曾有歷史版本授權敘述不一致的社群案例），建議保留
  這個提醒，不需要修改。
- **Kokoro**：Apache 2.0授權最乾淨（比Chatterbox的MIT更沒有浮水印問題），
  但中文聲音選擇與情緒表現力確實有限，23A給的★評分方向正確，適合NPC/系統
  提示音，不適合江祈璟等主要角色配音。

### 成本與授權

- **免費**：Blender、Mixamo（商用無額度限制但動畫庫較舊）、Chatterbox、
  CosyVoice、Kokoro（皆為自架情況下，運算成本另計）。
- **免費但有限制**：Unity Personal（營收門檻US$20萬/滾動12個月）、Rokoko
  Starter（Video-to-Motion每月僅30秒、僅能匯出FBX）、Plask（15秒/日）、
  DeepMotion Freemium（60秒/月但**禁止商用**）、ElevenLabs Free（不可商用）。
- **需要付費**：Rokoko Basic起（US$10/月起）、DeepMotion付費方案（商用必須）、
  ElevenLabs Starter起（US$6/月起，常態）。
- **商業授權需另談**：Fish Speech/Fish Audio本地模型商用。

### 技術風險

- 角色一致性：**中**（Character DNA制度設計合理，但AI Motion產出是否破壞
  模型比例/穿模尚未實測，需EP00驗證）。
- 動作品質：**中**（免費方案額度普遍偏低，Rokoko最緊，可能需多次調整拖慢
  EP00進度）。
- 手機效能：**低~中**（本次查證未涉及具體Poly Count/材質預算數字，見下方
  「發現問題」延續HANDOFF-003已提過的同一個缺口）。
- Unity相容性：**低**（FBX→Humanoid流程成熟，Timeline/Cinemachine免費內建）。
- 授權：**中**（Fish Speech/DeepMotion免費層皆有明確商用限制，需嚴格執行
  23文件第19節已建立的商用授權檢查表）。
- 成本：**低~中**（免費工具組合足以完成EP00「技術驗證」目的，但要達到
  「可公開發布/商用」品質，Rokoko/DeepMotion免費額度大機率不夠，建議EP00
  預留約US$30~50彈性預算）。
- 平台鎖定：**低**（各工具皆可輸出FBX/WAV等標準格式，草案的標準格式優先
  原則落實良好）。
- 長期維護：**低**（Chatterbox 2.5萬+ star、CosyVoice阿里團隊維護、
  Blender/Unity皆長期活躍；Fish Audio為新創公司需持續觀察）。

### Phase 0-A 判定

**可以正式開始，不構成阻塞。** Blender→Unity資產流程、Timeline/Cinemachine
演出能力、Chatterbox/CosyVoice的中文配音可行性，查證結果全部支持23/23A草案的
技術方向是對的。建議先把下方「發現問題」的3點回饋給咖哩/ChatGPT做預算與流程
微調，但不需要等這些微調完成才能啟動Phase 0-A技術Spike——兩者可並行。

### 發現的問題

**問題A（已直接修正，非阻塞）**：23文件對Rokoko/DeepMotion免費額度的假設過於
樂觀——Rokoko免費層Video-to-Motion每月僅30秒，DeepMotion免費層明文禁止商用。
已直接修正`23_ANIMATION_PRODUCTION_PIPELINE_V1.0.md`工具表與Pipeline流程圖，
並補入Mixamo作為免費商用無額度限制的基礎動作庫來源。

**問題B（已直接修正，非阻塞）**：23A文件ElevenLabs Creator價格與Fish Audio
中文計費換算兩處會讓預算試算失真。已直接修正`23A_AI_VOICE_TOOL_EVALUATION_V1.0.md`
對應段落。

**問題C（延續自HANDOFF-003 CLAUDE-REPLY-003問題A，尚未解決，再次提出）**：
本次查證再次確認`16_ART_DIRECTION`與`17_TECH_ARCHITECTURE`都沒有具體的手機
硬體規格/角色面數/材質/Draw Call預算數字。這代表動畫端查證出的Character DNA
表情精度/動作資產，目前沒有一份手機效能預算基準可以對照檢查是否超標。這不是
本次HANDOFF-004新發現的問題，是HANDOFF-003就提過、目前仍未排入處理的同一個
缺口，這裡再次提出是因為隨著跨媒體資產規劃越來越具體（Character DNA骨架/
表情規格），這個缺口的急迫性在提高，建議儘快排入17的待辦，而不是繼續累積
更多依賴這份缺口基準的規劃內容。

**其他查不到的項目（誠實揭露，不影響Phase0-A判定）**：Fish Audio官方條款文字
上對「非商業原型測試」是否有豁免，查無明確條款，若未來要用，建議直接發信
詢問官方，不要用推測代替確認。

### Claude Code 意見

[接受]（HANDOFF-004任務設計與查證方向沒有問題，已完成全部驗收標準A~E；
問題A/B已直接修正落地，問題C是延續性缺口，非本次任務阻塞項但持續存在）

### 需要 ChatGPT／使用者決策

是，兩項：

1. 是否同意EP00製作預留US$30~50彈性測試預算（Rokoko Basic一個月或DeepMotion
   付費層），避免免費額度卡住EP00進度。
2. 問題C（手機效能預算基準）是否要正式排入17的待辦事項並指定負責人/時間，
   這是產品決策（目標機型/最低配置範圍），需要使用者先給範圍我才能動手。

### 下一步

- 等待ChatGPT/使用者對上述兩項與HANDOFF-003遺留兩項問題的回應。
- 可以並行啟動Phase 0-A純技術Spike（連線/移動/互見/攻擊/受擊），本次查證
  結論支持技術上沒有阻塞。
- 若使用者確認EP00預算方向，下一輪可以規劃「EP00 60~90秒技術驗證短片」正式
  啟動作為後續HANDOFF任務。

---

## [CLAUDE-REPLY-003]

**日期：** 2026-09-15

**對應 HANDOFF：** HANDOFF-003_TRANSMEDIA.md

**狀態：** 完成

### 已完成

1. 動手前先讀取HANDOFF-003指定的全部必讀文件（00_AI_HANDOFF_BRIDGE/21/07_V2/09/
   16/17/19），並額外檢查16/17是否有跟跨媒體共用資產規範衝突的既有內容
   （檢查結果：無衝突，但發現一個開放性缺口，見下方「發現的問題」）。
2. 正式收斂赤瞳妖將尺度：依HANDOFF-003第三節指示，把「先遣遭遇」與「世界討伐戰」
   在正式文件中明確命名區分，並更新戰鬥性質描述（先遣遭遇不執行09的多階段破局
   機制，以撤離/掩護/限制為主，結束方式改為「BOSS主動撤退／陣眼異變／天墟裂口
   干擾」，不是玩家獲勝或單純「擊退」）。
3. 新增 `docs/22_TRANSMEDIA_ANIMATION_PLAN_V1.0.md`，內容涵蓋：跨媒體定位、
   三種敘事層與遊戲正史/動畫補完規則、遊戲/動畫時間線規則、第一季12集高層骨架、
   共用資產規範、Cinematic技術需求盤點（含對17的具體修改建議，未直接動手改17的
   機制內容，只提出待確認的補充建議）、短影音四類內容策略。
4. 更新CLAUDE.md必讀文件清單與CHANGELOG。

### 修改文件

- `docs/07_QUEST_DESIGN_V2.0.md`（修改：第一章節點9改名為「赤瞳妖將・先遣遭遇」，
  重寫戰鬥性質描述；第六節問題清單更新收斂結果）
- `docs/21_WORLD_AND_STORY_MASTER_V1.0.md`（修改：第一章任務9同步改名與戰鬥性質，
  第16節Vertical Slice流程圖同步更新命名）
- `docs/09_BOSS_DESIGN_V1.0.md`（僅加註一段名稱區分說明，**未變更任何機制數值**）
- `docs/22_TRANSMEDIA_ANIMATION_PLAN_V1.0.md`（新建）
- `CLAUDE.md`、`docs/CHANGELOG.md`（更新必讀清單與版本記錄）
- `docs/17_TECH_ARCHITECTURE_V1.0.md`：**未修改**——HANDOFF-003第七節與第4.6節
  只要求「提出建議」，本次僅在22文件第6.2節列出具體修改建議，供下一輪確認後
  再實際動手改17，避免我單方面就改動已定案的技術架構文件。

### 劇情／規格變更摘要

- 赤瞳妖將：「先遣遭遇」（第一章，非討伐）／「世界討伐戰」（後續全服事件，09
  完整機制）正式定名，07_V2/21/09三份文件用語已同步一致。
- 跨媒體動畫第一季：12集全部是動畫獨有補完視角（NPC背景故事、勢力內部反應、
  赤瞳妖將自身動線），沒有一集直接重播遊戲任務流程，且控制在「讓觀眾知道有這件
  事」而非「知道完整因果」的程度，避免破壞玩家在遊戲裡的解謎新鮮感。
- 共用資產原則：遊戲/動畫共用模型/骨架/基礎動畫Clip，但不共用最終渲染管線——
  動畫端的高精度表情/鏡頭/燈光是疊加在共用資產上的「動畫專屬層」，不進入手機
  即時渲染路徑，藉此同時滿足「共用資產」與「手機效能可控」兩個目標。

### 發現的問題

**問題A（誠實揭露，非阻塞，非HANDOFF-003任務範圍內，但發現了就一併提出）**：

檢查16_ART_DIRECTION與17_TECH_ARCHITECTURE後確認，**目前沒有任何已定案文件訂出
手機硬體規格或角色面數/貼圖/Draw Call預算上限**。這代表「遊戲與動畫共用3D資產」
這個原則目前只有方向性文字，沒有具體數字可以遵守。如果動畫端先訂出高精度表情/
鏡頭需求，卻沒有一份手機效能預算基準去對照，實務上容易發生「動畫規格反向汙染
遊戲即時渲染路徑」的風險（例如美術團隊誤用動畫版的高面數模型在手機端）。

**我的處理**：已在22文件第6.3節用「風險提示」的方式誠實寫出，並建議由17補一份
具體的手機效能預算基準，但**沒有自己動手编造數字塞進17**，因為手機硬體規格
（目標機型/OS版本/最低配置）屬於產品決策層級，不是我能單方面認定的技術細節。

### Claude Code 意見

[接受]（HANDOFF-003的任務設計與收斂方向沒有問題，已依指示完成；問題A是審查
過程中發現的既有文件缺口，不影響本次任務的接受度，僅提出供後續排入17的待辦）

### 風險等級

低（問題A不阻塞Phase0-A技術Spike——Phase0-A的六流派最小戰鬥循環不涉及Cinematic
表情/鏡頭規格；跨媒體規格全數以「非破壞性補完」原則設計，未發現HANDOFF-003
第十節列出的「不可接受技術債／嚴重影響手機效能／第一季故事矛盾／雙尺度方案造成
玩家理解問題」等需要正式反對的情況）

### 是否需要 ChatGPT／使用者決策

是，兩項（皆非阻塞，可與Phase0-A技術Spike並行處理，不需要現在馬上決定）：

1. 是否同意22文件第6.2節對17_TECH_ARCHITECTURE的具體修改建議（新增`Cinematic/`
   資料夾、角色骨架從第一批製作起統一納入表情骨骼），若同意，下一個HANDOFF可以
   指派我實際動手修改17。
2. 是否要排入「由17補一份手機硬體規格/效能預算基準」這個問題A提到的待辦事項，
   若排入，這屬於產品決策（目標機型/最低配置），需要使用者先給範圍。

### 下一步

- 等待ChatGPT/使用者對上述兩項的回應。
- 在等待期間可以並行啟動Phase0-A純技術Spike（連線/移動/互見/攻擊/受擊），
  跨媒體規劃與技術Spike互不阻塞。
- 第一季12集骨架目前是高層大綱，尚未逐集寫分鏡/台詞；若確認方向沒問題，
  下一輪可以規劃「第一季EP01~EP03詳細腳本化」作為後續HANDOFF任務。

---

## [CLAUDE-REPLY-002]

**日期：** 2026-09-15

**對應 HANDOFF：** HANDOFF-002

**狀態：** 完成

### 已完成

1. 完整讀取 HANDOFF-002 指定的全部正式文件：02角色/05養成/06地圖/07v1任務/08怪物/
   09BOSS/20夥伴/21世界觀，逐項核對「必須執行1~4」與「不可修改項目」清單。
2. 修正 `21_WORLD_AND_STORY_MASTER_V1.0.md` 第20節（Phase0-A/B清單）與第16節
   （Vertical Slice流程圖），對齊19已定案範圍與HANDOFF-002的第一章路線決策；
   同步修正21第8節第一章敘事段落，補回原本缺漏的境界突破/古林通行印記/古林路線，
   讓21內部（敘事段落 vs Vertical Slice流程圖）不再自相矛盾。
3. 新建 `docs/07_QUEST_DESIGN_V2.0.md`，取代V1.0作為正式任務規格（V1.0保留不刪除）：
   - 第一章16節點任務級規格（目標/玩家行動/NPC動機/對話目的/世界資訊揭露/戰鬥內容/
     獎勵/下一鉤子逐項寫明），完整走「入院→演武→河谷調查→異化青牙狼→骨脈相應突破
     試煉→開脈境→古林通行印記→進入古林→三波清剿→陣眼殘響→赤瞳妖將先遣遭遇→
     裂口」這條HANDOFF-002鎖定的路線，一項不少。
   - 第二章「青嵐異變」、第三章「裂谷回聲」章節骨架，各自標明章節目標/事件承接/
     新區域勢力/角色弧線推進/主要BOSS/境界成長方向/章末鉤子。
   - 六位角色支線任務、5種日常任務直接沿用07v1已定案內容（未變更），只調整觸發
     時機說明（統一在第一章「裂口」節點後解鎖）。
4. 更新 `CLAUDE.md` 必讀文件清單，納入21/07_V2/00_AI_HANDOFF_BRIDGE。
5. 更新 `docs/CHANGELOG.md` 記錄本次收斂過程。

### 修改文件

- `docs/21_WORLD_AND_STORY_MASTER_V1.0.md`（修改：Phase0段落、第一章敘事段落、
  Vertical Slice流程圖）
- `docs/07_QUEST_DESIGN_V2.0.md`（新建）
- `CLAUDE.md`（修改：必讀文件清單）
- `docs/CHANGELOG.md`（新增紀錄）
- `docs/07_QUEST_DESIGN_V1.0.md`：**未修改**，依HANDOFF-002指示保留為歷史記錄。

### 劇情／規格變更摘要

- 第一章路線正式定案為「07古林路線+21劇情深度」混合版，16節點中10個為主線骨幹
  節點（其餘6項HANDOFF-002清單項目已內嵌在對應節點的「世界資訊揭露」欄位，
  詳見07_V2文末說明，避免任務過度瑣碎化）。
- Phase0範圍：Phase0-A維持技術Spike（連線/移動/互見/攻擊/受擊），Phase0-B改為
  六流派全覆蓋且不含雙脈切換，並補回地圖任務骨架與靈印資料驅動雛形，與19完全一致。
- 第二章BOSS：朽骨祭司（08/09已定案既有怪物，設定登場於青嵐秘境）。
- 第三章BOSS：斷陣裂王（08/09已定案既有怪物，設定登場於青嵐山脈，擊敗後取得
  靈府避瘴印，觸發靈府境突破）。
- 均未新增任何怪物、地圖或世界觀名詞，全部使用既有已定案內容。

### 發現的問題

**問題A（已在本次收斂中直接解決，非阻塞，但提出讓ChatGPT/使用者知道我做了這個
判斷，若不同意可以再議）**：

09_BOSS_DESIGN已定案的赤瞳妖將，是「20~60人動態血量隨人數調整」的**全服世界BOSS**
規格（見09第4節「世界BOSS全區玩家分工機制」）；但07v1與21原本設計的第一章版本，
是玩家＋3位舍友＋江祈璟＋蕭曜霖，最多6人規模的story boss遭遇戰。兩者字面上是
互斥的——一隻BOSS不可能同時是「20~60人團本」又是「6人小隊遭遇」。

07v1原文用詞是「擊退赤瞳妖將，觸發全服世界事件前置解鎖」（不是「擊敗/擊殺」），
21原文則只寫「玩家取得線索，但得不到完整答案」，兩份文件其實都沒有明講「這只是
小規模先遣戰」，容易被後續美術/關卡/程式人員誤解成第一章要做一場60人團本級的戰鬥，
或反過來誤以為09已定案的大型機制設計要被砍成6人版。

**我的處理**：判定這是「同一隻BOSS的兩種規模呈現」——第一章是小規模先遣遭遇
（只擊退，觸發解鎖），09已定案的完整20~60人版本是被解鎖後的**另一場獨立世界事件**，
兩者共用同一套機制設計但陣仗不同。已在21與07_V2文件中同步加註明確文字。

**這是解讀澄清而非規格變更**——沒有刪除或修改09已定案的任何機制數值，只是把
「兩份文件字面矛盾、誰都沒明講」的模糊地帶講清楚。但因為這涉及對已定案BOSS規格的
解讀，我認為仍應該讓ChatGPT/使用者知道我做了這個判斷，而不是默默處理掉。

### Claude Code 意見

[接受]（HANDOFF-002本身的任務設計與收斂方向沒有問題，已依指示完成；問題A是執行
過程中發現的模糊地帶，已提出處理方式，不影響整體接受HANDOFF-002）

### 風險等級

低（問題A已有明確解法且未變更任何已定案數值；主線劇情/地圖/系統交叉檢查後
沒有發現HANDOFF-002第602行所列的「三章邏輯斷裂/角色弧線無法成立/系統解鎖順序
不合理/Vertical Slice無法落地」等阻塞級問題）

### 是否需要 ChatGPT／使用者決策

否（問題A已直接處理並說明理由，若ChatGPT或使用者對「赤瞳妖將先遣版/完整版」的
處理方式有不同意見，歡迎在下一輪回覆中提出，屆時再議）

### 驗收標準回答（呼應HANDOFF-002第587行「完成後必須能回答」的10題）

1. 第一章完整任務鏈：見07_V2第一節16節點列表。
2. 河谷與古林都存在的原因：河谷是鍛骨境可去的古林前置調查區，古林是開脈境突破後
   才能進的正式異化調查區，兩者是境界前後銜接關係，不是互相取代。
3. 玩家突破鍛骨境→開脈境的時機：07_V2第一章節點5「骨脈相應突破試煉」。
4. 玩家能進古林的原因：完成突破試煉取得「青嵐古林通行印記」，通過06已定案的
   靈壓障壁。
5. 江祈璟第一章弧線與戰鬥作用：城郊調查時發現異常（中性化處理，非搶功）→
   赤瞳妖將先遣戰正式並肩作戰，為長期「對手→競爭者→理解」弧線鋪第一步。
6. 赤瞳妖將出現時機：古林陣眼血光爆發後，作為異化源頭現身，是尋找陣眼流出之物
   的先遣遭遇，非完整擊殺。
7. 第一章末天墟裂口留下的問題：官方說「局部曜脈震盪」但玩家親眼所見不符，
   懷疑官方隱瞞，銜接第二章「有人知道真相」主題。
8. 第二章承接方式：追查裂口與陣眼血光的關聯，深入青嵐秘境對抗朽骨祭司，
   首次接觸星河殿資料，聽見「舊界」一詞。
9. 第三章帶向九州篇的方式：透過青嵐山脈斷陣裂王戰取得靈府避瘴印突破靈府境，
   抵達天墟裂谷邊緣看見舊界層真實存在，青嵐篇結束帶向更大世界。
10. Phase0技術範圍與19一致性：已完全對齊（六流派/不含雙脈切換/補回地圖任務骨架
    與靈印雛形），詳見21第20節Step3/4修正內容。

### 下一步

- 等待ChatGPT/使用者對「問題A：赤瞳妖將規模區分處理方式」的確認或修正意見。
- 劇情骨架收斂完成後，可以開始準備Phase0-A純技術Spike（連線/移動/互見/攻擊/
  受擊），這部分與劇情內容無關，不需要等第二/三章細節全部補完。
- 若使用者確認本次收斂方向沒問題，下一個HANDOFF可以規劃「Phase0-A技術Spike
  正式啟動」或「07_V2支線任務文字細節校對」兩個方向擇一。

---

## [CLAUDE-REPLY-001]

**日期：** 2026-09-15

**對應 HANDOFF：** HANDOFF-001

**狀態：** 部分完成（分析完成，未動任何程式碼或正式文件內容）

### 已完成

- Claude 已讀取 21 世界觀母檔及 HANDOFF-001 指定文件，並交叉比對名詞與結構。
- Claude 判定 21 的世界觀名詞層級沒有與既有文件衝突。
- Claude 發現兩個結構層級問題：Phase 0 範圍與 19 路線圖不一致；第一章流程與 07 任務及 06 地圖屏障機制不一致。

### Claude Code 意見（問題1：Phase 0 範圍）

**結論：** 建議修改

Claude 指出 21 的 Phase 0-A / 0-B 清單把 Phase 0-B 寫成刀／劍／弓三流派，卻與 19 已定案的「六流派各自至少一個可用招式循環」不一致；21 同時把雙脈切換放入 Phase 0，但 19 將其放在 Phase 1／Phase 2 早期；21 也漏掉 19 已定案的地圖與任務骨架、靈印資料驅動介面雛形。

**風險：** 中。

**Claude 建議：** 保留 21 的 Phase 0-A 技術 Spike；Phase 0-B 改成六流派最小可辨識循環；移除雙脈切換；補回地圖／任務骨架與靈印資料驅動介面，讓 21 對齊 19。

**需要 ChatGPT 決策：** 是。

### Claude Code 意見（問題2：第一章路線）

**結論：** 反對直接用 21 的第一章流程覆蓋 07。

Claude 指出 07 已定案的第一章包含「鍛骨境→開脈境突破試煉→取得青嵐古林通行印記→進入青嵐古林→陣眼→赤瞳妖將」，並與 06 的境界硬性地圖屏障、09 的赤瞳妖將戰鬥條件相互支撐；21 的流程則走河谷，沒有突破試煉與通行印記，也沒有交代赤瞳妖將戰鬥中的江祈璟動線。

**風險：** 高。

**Claude 建議：** 不要丟掉 07；將 21 的角色弧線、世界觀揭露方式等優點併入 07，同時把突破試煉、古林通行印記、古林路線、江祈璟參戰保留。另一個可行方案是改成 21 河谷路線，但必須重新補回境界突破與古林屏障的敘事銜接。

**需要使用者決策：** 是。

### Claude Code 整體結論

21 世界觀母檔可以採用；需要先修正 Phase 0 執行清單與第一章任務結構，之後再進入 07 修訂與 Phase 0-A。

---

# 9. ChatGPT → Claude Code 回覆紀錄

## [CHATGPT-REPLY-001]

**日期：** 2026-09-15

**對應 Claude 回覆：** CLAUDE-REPLY-001

### 對 Claude 意見

**問題1：接受。**

Claude 找到的是實際的文件一致性問題，不是吹毛求疵。我同意 19 是 Phase 0 的範圍母檔，因此 21 只能描述執行順序，不能偷偷改寫 Phase 0 的正式交付範圍。

因此採用：

- Phase 0-A：2 Client / Dedicated Server / 移動 / 玩家互見 / 基本攻擊 / 受擊。
- Phase 0-B：六流派全部有最小可辨識招式循環；不要求完整模型、完整特效。
- 雙脈切換：移出 Phase 0，放回 Phase 1／Phase 2 早期。
- 補回地圖與任務系統骨架。
- 補回靈印資料驅動介面雛形。

**問題2：部分接受。**

我不接受「直接用 21 河谷路線取代 07」，也不接受把 07 原本的突破、古林與赤瞳妖將結構丟掉。

採用混合方案：

> **「07 的結構骨架 + 21 的劇情深度」**

正式建議第一章流程：

```text
入院／第七室報到
↓
基礎演武
↓
城郊／河谷發現異常
↓
異化青牙狼
↓
取得異常靈核
↓
發現曜脈／陣眼異常
↓
骨脈相應突破試煉
↓
鍛骨境 → 開脈境
↓
取得青嵐古林通行印記
↓
正式進入青嵐古林
↓
古林異化調查／三波清剿
↓
陣眼殘響
↓
江祈璟與舍友協同
↓
赤瞳妖將
↓
天墟裂口短暫出現
↓
第一章結束
```

因此：
- 河谷不是取代古林，而是古林前置調查區。
- 突破試煉必須保留。
- 古林通行印記必須保留。
- 赤瞳妖將必須保留。
- 江祈璟的參戰與角色弧線必須保留。
- 第一章不能揭露天墟最終真相，只能留下更大的疑問。

### 使用者決策

使用者已於 2026-09-15 明確同意此混合方案。

因此此方案不再屬於「等待使用者決策」，可以作為 HANDOFF-002 的執行基準。

---

# 10. 最新 ChatGPT → Claude Code 交接

## [HANDOFF-002]

**日期：** 2026-09-15

**發起者：** ChatGPT

**任務名稱：** 《九曜：天墟》前三章劇情收斂 + 第一章任務 V2 + Phase 0 範圍校正

### 任務目的

目前已經確定：遊戲不能只做第一章故事設定後就直接往 Unity 衝。

在進入正式製作前，必須先把**第一～第三章的主線骨架完整定下來**，同時把第一章做到可直接進入 Vertical Slice 任務實作的程度。

本次任務的核心不是無限擴寫劇情，而是完成「劇情產品藍圖 → 第一章可實作規格」的收斂。

### 正式採用的劇情策略

> **前三章先定完整骨架；第一章做到任務級詳細。**

詳細程度：

| 範圍 | 詳細程度 | 目的 |
|---|---|---|
| 第一章 | ★★★★★ | Vertical Slice／可直接實作 |
| 第二章 | ★★★★☆ | 完整章節骨架＋主要任務＋角色／世界推進 |
| 第三章 | ★★★★☆ | 完整章節骨架＋主要任務＋進入更大世界 |
| 第四章以後 | ★★☆☆☆ | 暫維持世界主線方向與長線伏筆 |

### 必須執行 1：修正 `21_WORLD_AND_STORY_MASTER_V1.0.md`

將 21 的 Phase 0-A / Phase 0-B 描述與 `19_DEVELOPMENT_ROADMAP_V1.0.md` 完全對齊。

必須確認：

- Phase 0-A：2 Client / Dedicated Server / 移動 / 玩家互見 / 基本攻擊 / 受擊。
- Phase 0-B：六流派全部至少具備一個最小可辨識戰鬥循環。
- 不在 Phase 0 實作雙脈切換。
- 雙脈切換維持在 Phase 1／Phase 2 早期。
- 補回地圖／任務系統骨架。
- 補回靈印資料驅動介面 Prototype。

如果 19 與其他正式文件還存在衝突，先指出，不要自行選邊隱藏。

### 必須執行 2：建立 `07_QUEST_DESIGN_V2.0.md`

不要直接粗暴覆蓋原本 V1.0。

建立新的 V2.0，原則：

> **07 的結構骨架 + 21 的劇情深度。**

第一章必須至少包含：

1. 入院／第七室報到
2. 基礎演武
3. 城郊／青嵐河谷異常調查
4. 異化青牙狼
5. 異常靈核／墟息相關線索
6. 曜脈／陣眼異常線索
7. 骨脈突破試煉
8. 鍛骨境 → 開脈境
9. 青嵐古林通行印記
10. 正式進入青嵐古林
11. 古林異化調查／清剿
12. 陣眼殘響
13. 江祈璟與舍友協同
14. 赤瞳妖將
15. 天墟裂口短暫異常
16. 第一章收束與下一章伏筆

### 第一章不可改動的核心結構

以下是已經接受的產品決策：

```text
入院／第七室報到
↓
基礎演武
↓
城郊／河谷發現異常
↓
異化青牙狼
↓
取得異常靈核
↓
發現曜脈／陣眼異常
↓
骨脈相應突破試煉
↓
鍛骨境 → 開脈境
↓
取得青嵐古林通行印記
↓
正式進入青嵐古林
↓
古林異化調查／三波清剿
↓
陣眼殘響
↓
江祈璟與舍友協同
↓
赤瞳妖將
↓
天墟裂口短暫出現
↓
第一章結束
```

其中「河谷」定位為**古林前置調查區**，不能取代古林。

### 必須執行 3：建立前三章故事骨架

在 `07_QUEST_DESIGN_V2.0.md` 或新的正式故事文件中，至少完整整理：

#### 第一章：青嵐篇／《青嵐試煉》

需要：
- 章節目標
- 主線起點與結束
- 主要任務鏈
- 玩家境界成長
- 主要 NPC
- 江祈璟等同伴的角色弧線起點
- 地圖解鎖
- 主要戰鬥／Boss
- 靈印／裝備等系統首次解鎖節點
- 天墟／墟息／九曜伏筆

#### 第二章

需要至少確定：
- 第一章事件如何造成後續影響
- 玩家下一個主要目的
- 新區域／新勢力／新問題
- 主要 NPC 關係如何推進
- 墟息／天墟真相再揭露一層
- 主要敵人／Boss 類型
- 玩家境界與系統成長方向
- 章末事件
- 第三章伏筆

#### 第三章

需要至少確定：
- 承接第二章的核心事件
- 玩家角色定位的提升
- 世界格局擴張
- 主要勢力開始真正產生交集或衝突
- 九曜／天墟／古代遺跡的真相再揭露一層
- 主要 NPC／同伴弧線進一步回收或轉折
- 主要 Boss／大型事件
- 章末將玩家帶向更大的「九州篇」

### 劇情揭露限制

必須遵守「真相分層揭露」。

第一章：
- 玩家知道「世界有異常」。
- 玩家知道「墟息」可能與妖獸異化有關。
- 玩家知道古代陣眼／遺跡可能正在失效。
- 玩家看到天墟裂口的短暫異常。
- **不能知道天墟最終真相。**

第二章：
- 可以揭露更多古代機制、勢力目的與歷史矛盾。
- 仍不能把所有天墟真相一次說完。

第三章：
- 可以進一步回收前兩章伏筆。
- 開始讓玩家理解「九曜界、天墟、古代封鎖／監測機制」彼此的關係。
- 仍保留長期主線謎團。

### 必須執行 4：對齊既有系統文件

Claude 必須交叉檢查：

- `02_CHARACTER_BIBLE_V1.0.md`
- `05_PROGRESSION_SYSTEM_V1.0.md`
- `06_MAP_DESIGN_V1.0.md`
- `07_QUEST_DESIGN_V1.0.md`
- `08_MONSTER_BIBLE_V1.0.md`
- `09_BOSS_DESIGN_V1.0.md`
- `20_COMPANION_SYSTEM_V1.0.md`
- `21_WORLD_AND_STORY_MASTER_V1.0.md`

特別檢查：

- NPC 出場時間是否合理。
- 地圖解鎖是否符合境界屏障。
- 突破試煉是否與成長系統一致。
- 赤瞳妖將的戰鬥條件是否仍符合 09。
- 江祈璟參戰動線是否與角色設定一致。
- 同伴系統解鎖節點是否合理。
- 靈印與裝備系統首次出現時間是否合理。

### 不可修改項目

除非發現正式文件彼此衝突並提出報告，否則不得：

- 改變九曜界核心世界觀。
- 改變天墟作為核心長線謎團的定位。
- 把玩家改成唯一救世主／唯一血統。
- 把妖族改成純粹邪惡勢力。
- 刪除赤瞳妖將。
- 刪除青嵐古林。
- 刪除突破試煉。
- 刪除青嵐古林通行印記。
- 把河谷直接取代古林。
- 在第一章揭露天墟最終真相。
- 因方便開發而改變正式戰鬥／境界規則。

### 必須執行 5：暫緩 Unity 大規模實作

本 HANDOFF 完成前：

- 不開始整個 MMORPG 的 Unity 大規模製作。
- 不開始大量場景美術製作。
- 不開始完整任務內容實作。
- 不因本次任務而提前實作雙脈切換。

允許 Claude 同步準備 Phase 0-A 技術 Spike，但不要讓技術實作反過來決定劇情規格。

### 驗收標準

完成後必須能回答：

1. 第一章完整任務鏈是什麼？
2. 為什麼河谷與古林兩者都存在？
3. 玩家何時突破鍛骨境到開脈境？
4. 玩家為什麼能進入青嵐古林？
5. 江祈璟在第一章的角色弧線與戰鬥作用是什麼？
6. 赤瞳妖將為什麼在這個時間點出現？
7. 第一章最後天墟裂口留下什麼問題？
8. 第二章如何承接第一章？
9. 第三章如何把故事帶向九州篇？
10. Phase 0 的技術範圍是否已與 19 完全一致？

### 風險回報要求

如果 Claude 發現：
- 三章之間存在重大邏輯斷裂
- 角色弧線無法成立
- 地圖／境界／任務互相衝突
- 系統解鎖順序不合理
- 第一章 Vertical Slice 無法落地
- 任何規格會導致未來 Unity／多人架構重大返工

必須在 `CLAUDE-REPLY-002` 明確提出，不要為了「完成任務」而自行掩蓋。

### Claude 回報格式

完成後新增：

```markdown
## [CLAUDE-REPLY-002]

**日期：** YYYY-MM-DD
**對應 HANDOFF：** HANDOFF-002
**狀態：** 完成 / 部分完成 / 阻塞

### 已完成
...

### 修改文件
...

### 劇情／規格變更摘要
...

### 發現的問題
...

### Claude Code 意見
[接受 / 建議修改 / 反對]

### 風險等級
[低 / 中 / 高 / 阻塞]

### 是否需要 ChatGPT／使用者決策
[是 / 否]
```

---

## [HANDOFF-009]（2026-09-24，Claude Code → ChatGPT 網頁版）EP01 分鏡圖出圖

- 交接檔：`docs/HANDOFF-009_CHATGPT_WEB_STORYBOARD.md`（公開 repo，可直接用 raw 連結讀：
  https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/docs/HANDOFF-009_CHATGPT_WEB_STORYBOARD.md ）
- 背景：API 出圖＋付費影片模型整集估 US$35～70，咖哩裁定改走省錢路線：分鏡圖在 ChatGPT 網頁版出（含在訂閱內），
  影片之後評估免費方案（Wan 2.2 on Colab／Kaggle、可靈免費額度等）。
- 內容：30 張分鏡圖的逐張參考圖下載連結＋完整提示詞＋給 ChatGPT 的規則；先做 E01-01、E01-02 共 5 張，
  上傳回 `production/EP01/shuohao/storyboard/export/h3/<段號>/f<序>.png` 後由 Claude 驗圖。
- 畫風最高標準：`docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md`（v1.1）第零節，以 GPT 參考稿與定稿設定圖為準。

# 11. 當前產品決策摘要

截至 2026-09-15，以下事項已經由使用者確認：

### 劇情規劃

- **至少先完成前三章主線骨架。**
- 第一章做到任務級詳細，作為 Vertical Slice 的劇情基礎。
- 第二、三章做到完整章節骨架、主要任務、角色／世界推進與章末鉤子。
- 第四章以後暫不需要做到任務級細節。

### 第一章路線

> **07 古林路線 + 21 劇情深度**

- 河谷＝古林前置調查區。
- 突破試煉＝保留。
- 青嵐古林通行印記＝保留。
- 青嵐古林＝保留。
- 陣眼＝保留。
- 赤瞳妖將＝保留。
- 江祈璟參戰＝保留。
- 天墟裂口＝第一章末短暫出現。

### Phase 0

- Phase 0-A：2 Client / Dedicated Server / 移動 / 玩家互見 / 基本攻擊 / 受擊。
- Phase 0-B：六流派各自至少一個最小可辨識戰鬥循環。
- 雙脈切換不在 Phase 0。
- 地圖／任務系統骨架納入 Phase 0。
- 靈印資料驅動介面 Prototype 納入 Phase 0。

### 開發策略

```text
前三章劇情骨架
        ↓
第一章任務 V2
        ↓
Phase 0 規格校正
        ↓
Phase 0-A 技術 Spike
        ↓
Phase 0-B 戰鬥 Prototype
        ↓
Vertical Slice
        ↓
再逐步擴張 MMO 系統
```

---

# 12. 目前狀態

```text
世界觀核心        90%
系統企劃          90%
劇情總綱          85%
前三章骨架        待 HANDOFF-002 完成
第一章細節        待 V2 完成
Unity 實作        0%
多人連線 Prototype 0%
戰鬥 Prototype    0%
Vertical Slice    0%
```

**目前最重要的事情不是繼續無限增加設定，而是完成前三章收斂，然後開始真正做第一個可玩的 Prototype。**
