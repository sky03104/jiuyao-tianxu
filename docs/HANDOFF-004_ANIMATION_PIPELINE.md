# 《九曜：天墟》HANDOFF-004
# 動畫製作 Pipeline × AI 配音工具 × Phase 0 前技術可行性審查

**日期：** 2026-09-14  
**發起者：** ChatGPT  
**狀態：** 待 Claude Code 執行  
**對應主交接橋：** `docs/00_AI_HANDOFF_BRIDGE.md`

---

## 1. 任務目的

在正式開始 Unity / Phase 0-A 原型之前，先完成《九曜：天墟》的「遊戲 × 動畫 × 短影音」製作流程技術審查。

本次不是要求 Claude Code 直接製作動畫，而是確認：

1. 動畫製作流程是否實際可行。
2. 遊戲角色、場景、動畫資產是否能共用。
3. Blender → 動作工具 → Unity 的流程是否能落地。
4. Unity Timeline / Cinemachine 是否足以支撐後續 Cinematic。
5. AI 配音工具是否適合長期商業專案。
6. 哪些工具可以免費作為核心流程，哪些工具未來可能需要付費。
7. 是否存在授權、商用、模型、平台鎖定或成本風險。
8. 完成審查後，才正式進入 Phase 0-A。

---

# 2. 必讀文件

Claude Code 執行前必須閱讀：

1. `docs/00_AI_HANDOFF_BRIDGE.md`
2. `docs/21_WORLD_AND_STORY_MASTER_V1.0.md`
3. `docs/22_TRANSMEDIA_ANIMATION_PLAN_V1.0.md`
4. `docs/23_ANIMATION_PRODUCTION_PIPELINE_V1.0.md`
5. `docs/23A_AI_VOICE_TOOL_EVALUATION_V1.0.md`
6. `docs/16_ART_DIRECTION_V1.0.md`
7. `docs/17_TECH_ARCHITECTURE_V1.0.md`
8. `docs/19_DEVELOPMENT_ROADMAP_V1.0.md`
9. `docs/07_QUEST_DESIGN_V2.0.md`
10. `docs/09_BOSS_DESIGN_V1.0.md`

如果上述文件之間存在衝突，**不得自行默認修改正式規格**，必須在回覆中指出。

---

# 3. 已經確定的方向

## 3.1 跨媒體定位

- 遊戲 = 正史核心、玩家視角。
- 動畫 = 補完世界觀、NPC、事件與非玩家視角。
- 短影音 = 宣傳、世界觀入口、角色內容、開發內容。
- 動畫不能只是把遊戲任務錄成影片。
- 三者共用角色、場景、材質、骨架、部分動畫與 VFX 資產。

## 3.2 動畫技術方向

初步採用：

```text
劇本
 ↓
分鏡 / Shot List
 ↓
角色 DNA / 3D 資產
 ↓
Blender
 ↓
AI 動作工具 / 動作庫
 ↓
Blender 動作整理
 ↓
Unity
 ↓
Timeline / Cinemachine / Lighting / VFX
 ↓
Render
 ↓
剪輯 / 配音 / 音效 / 字幕
 ↓
YouTube / Shorts / TikTok / 其他平台
```

核心原則：**不要依賴「一次生成完整 10 分鐘動畫」的黑盒 AI 流程。**

應採用固定角色 + 固定場景 + Shot-by-Shot 製作，以確保角色一致性與可重複修改。

---

# 4. Claude Code 必須驗證的內容

## 4.1 Blender → Unity 資產流程

確認以下是否可行：

- Blender 建立／整理角色。
- 標準骨架與動畫匯出。
- FBX / glTF 等標準格式的可用性。
- Unity Humanoid / Generic Avatar 對應方式。
- 角色材質、貼圖、骨骼、動畫在 Unity 的導入策略。
- 遊戲角色與 Cinematic 角色是否可以共用主要資產。

要求：提出推薦格式與命名規範，不需要一次建立完整資產。

## 4.2 AI 動作工具

以免費優先原則驗證：

- Rokoko Studio Starter
- Blender 動作編輯
- Mixamo 類動作資源／既有動畫庫
- DeepMotion / Plask 等作為備選

需要確認：

- 免費方案是否足以進行早期 Prototype。
- 匯出格式是否能進 Blender / Unity。
- 商業使用授權是否有風險。
- 是否會形成平台鎖定。

不要求現在購買任何付費服務。

## 4.3 Unity Cinematic

確認：

- Timeline
- Cinemachine
- Animator
- Animation Events
- VFX / Particle
- Lighting
- Camera Cut
- Subtitle / Dialogue Trigger

是否足以支撐第一季動畫與遊戲內劇情演出。

若需要額外套件，必須說明：

1. 為什麼需要。
2. 是否免費。
3. 是否有替代方案。
4. 是否造成長期依賴。

## 4.4 角色一致性

確認 `Character DNA` 是否應在正式製作前鎖定至少：

- 正面
- 側面
- 背面
- 身高比例
- 身體比例
- 臉部特徵
- 髮型
- 服裝
- 武器
- 色彩
- 表情範圍
- 骨架規格

目標：同一角色在遊戲、動畫、宣傳圖與短影音中必須可以被辨識為同一角色。

---

# 5. EP00 技術驗證

正式製作第一季 12 集之前，先建立 60～90 秒的內部測試短片。

## 建議內容

```text
青嵐古林
 ↓
夜晚
 ↓
角色進入古林
 ↓
陣眼異常
 ↓
赤瞳妖將出現
 ↓
天空裂痕
 ↓
黑屏
```

## EP00 驗收重點

不是看故事是否完整，而是驗證：

- 角色一致性
- 動作品質
- 場景品質
- 光影
- Camera
- VFX
- Timeline
- 配音
- 音效
- Render
- 剪輯
- 字幕
- Unity 與 Blender 資產往返流程

如果 EP00 流程不穩，不能直接擴大到 12 集。

---

# 6. AI 配音工具審查

必須比較：

1. Chatterbox Multilingual V3
2. Fish Speech / Fish Audio
3. ElevenLabs
4. CosyVoice
5. Kokoro

比較維度：

| 項目 | 必須回答 |
|---|---|
| 中文品質 | 是 |
| 多角色 | 是 |
| 情緒控制 | 是 |
| Voice Cloning | 是 |
| 本地運行 | 是 |
| API | 是 |
| 免費方案 | 是 |
| 商業授權 | 是 |
| 商用限制 | 是 |
| 成本預估 | 是 |
| 音檔格式 | 是 |
| Unity 使用 | 是 |
| 長期穩定性 | 是 |
| 平台鎖定 | 是 |

## 特別注意

### Fish Speech / Fish Audio

不能因為 GitHub 可以免費下載就直接視為「可以免費商用」。必須確認目前模型／程式碼授權與商業授權要求。

### Voice Cloning

不得未經同意複製真人聲音。

《九曜：天墟》應建立自己的 `Voice DNA`：

- 年齡感
- 性別感
- 聲線
- 語速
- 情緒
- 音域
- 說話習慣
- 角色專屬語氣

---

# 7. 免費優先原則

目前不鎖定任何昂貴工具。

優先測試：

- Blender
- Unity Personal
- Rokoko 免費方案
- 免費／開源 AI 動作工具
- DaVinci Resolve Free
- 可合法商用的開源 TTS

付費工具只能在以下情況進入正式流程：

> 免費方案無法達到產品品質，而且付費工具的授權、成本與長期可維護性都可以接受。

不得因為某個工具效果很好，就讓整個專案被單一 SaaS 平台綁死。

---

# 8. 不可修改項目

本次 Claude Code **不得直接修改**：

- 世界觀核心真相
- 第一季劇情正式內容
- 九曜／天墟設定
- 角色正式設定
- 戰鬥核心規則
- 靈印核心規則
- MMO 網路架構核心決策
- `17_TECH_ARCHITECTURE_V1.0.md` 的正式架構內容
- `19_DEVELOPMENT_ROADMAP_V1.0.md` 的正式里程碑

如果技術驗證發現必須修改上述內容，先提出問題與替代方案，等待決策。

---

# 9. 本次不做的事情

Claude Code 本次不要：

- 開始完整 Unity MMO 開發。
- 建立完整伺服器架構。
- 實作完整戰鬥系統。
- 製作完整動畫第一集。
- 批量生成全部動畫角色。
- 購買付費工具。
- 把尚未驗證的 AI 工具鎖定成正式標準。

本次是 **Technical Spike / Feasibility Review**。

---

# 10. 驗收標準

Claude Code 完成後必須提供：

## A. 最終推薦 Pipeline

用一張流程圖或清單回答：

```text
建模 → 綁骨 → 動作 → 動作整理 → Unity → Cinematic → 配音 → 剪輯 → 發布
```

每一步標出推薦工具。

## B. 工具分級

### Tier 1：現在直接使用
### Tier 2：必要時使用
### Tier 3：暫不使用

## C. 成本

標出：

- 免費
- 免費但有限制
- 需要付費
- 商業授權需另談

## D. 技術風險

至少評估：

- 角色一致性
- 動作品質
- 手機效能
- Unity 相容性
- 授權
- 成本
- 平台鎖定
- 長期維護

每項標示：

`低 / 中 / 高 / 阻塞`

## E. Phase 0-A 是否可以開始

最後必須明確回答：

> **現在是否可以正式開始 Phase 0-A？**

如果不可以，列出阻塞條件。

---

# 11. Claude Code 回覆格式

完成後請在 `docs/00_AI_HANDOFF_BRIDGE.md` 的「Claude Code → ChatGPT」區域新增回覆，並保留所有歷史紀錄。

格式：

```markdown
## [CLAUDE-REPLY-004]

**日期：** YYYY-MM-DD

**對應 HANDOFF：** HANDOFF-004_ANIMATION_PIPELINE.md

**狀態：** 完成 / 部分完成 / 阻塞

### 最終推薦 Pipeline
...

### 工具分級
...

### AI 配音評估
...

### 成本與授權
...

### 技術風險
...

### Phase 0-A 判定
...

### 發現問題
...

### 需要 ChatGPT / 使用者決策
...
```

---

# 12. 最重要的產品原則

> **先把「製作流程」跑通，再把「遊戲」做大。**

> **先做 60～90 秒 EP00 技術驗證，再做 12 集動畫。**

> **先免費驗證，再決定付費。**

> **先保留標準格式，再避免平台鎖定。**

> **遊戲是正史核心；動畫是世界觀補完；短影音是入口。**

---

# 13. 與主交接橋的關係

本文件是 `docs/00_AI_HANDOFF_BRIDGE.md` 的 **HANDOFF-004 詳細任務單**。

Claude Code 執行前仍必須先讀取主交接橋，完成後也必須把結果回寫主交接橋，確保 ChatGPT 下一次進入專案時可以從主橋接續。
