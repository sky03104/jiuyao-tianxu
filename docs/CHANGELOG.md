# CHANGELOG — 《九曜：天墟》

## 2026-09-14（第二輪）

- 連線技術棧正式拍板：**Photon Fusion（Dedicated Server 模式）**，明確排除 Host 模式用於
  正式營運。流程：Claude 提出草稿審查意見（分區/副本人數定義不一致、防作弊立場模糊、
  成本門檻邏輯缺陷）→ GPT-5.2 修正 → 與 Gemini、DeepSeek 交叉驗證收斂 → 咖哩依實際預算
  調整成本門檻數字（改用 NT$500/1000 兩階，符合 Phase 0 自掏腰包測試規模而非新創標準）。
  詳見 `docs/01_ARCHITECTURE_DECISIONS_V1.0.md` 第1節。

## 2026-09-14

- 建立repo `sky03104/jiuyao-tianxu`
- 存檔原始設計總綱 `docs/00_GAME_DESIGN_BIBLE_V1.0.md`（企劃階段，未修改內容）
- 完成第一輪架構審視：Claude審視GDD提出5個缺口/矛盾點（連線架構、裝備靈印分工、
  戰力機制、商業化、戰鬥自動化），與外部模型（GPT）交叉討論後收斂結論，
  寫入 `docs/01_ARCHITECTURE_DECISIONS_V1.0.md`
- 尚未開始任何程式開發，目前仍在 Phase 0 企劃階段
