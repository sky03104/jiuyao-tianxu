# 《九曜：天墟》HANDOFF-008

## Phase 0-D：Map / Spawn / Quest State Machine Skeleton

**日期：2026-09-16**  
**狀態：READY TO EXECUTE**  
**前置：HANDOFF-007 Phase 0-C 已完成並通過驗收**

---

# 1. 本次任務目的

Phase 0-A～0-C 已完成：

```text
Network
  ↓
Player
  ↓
Combat Framework
  ↓
六大武器 MVP
  ↓
Spirit Seal Prototype
```

本階段補齊 Roadmap 中最後一塊 Phase 0 基礎：

> **地圖、Spawn、簡化怪物、任務狀態機。**

這一階段仍然是「Skeleton」，不是正式青嵐城內容製作。

完成後必須能驗證一個最小可玩的遊戲流程：

```text
進入測試地圖
 ↓
Player Spawn
 ↓
看到可互動／可戰鬥目標
 ↓
接取 Quest
 ↓
擊敗指定數量目標
 ↓
Quest Progress 更新
 ↓
達成條件
 ↓
Quest Complete
```

---

# 2. 本階段核心原則

## 2.1 Map 與 Quest 分離

不要把任務邏輯寫死在 Scene、Monster 或 Player Script 裡。

應維持：

```text
Map / Spawn
      ↓
World Entity
      ↓
Combat Event
      ↓
Quest System
      ↓
Quest State
```

Quest 不應直接依賴某一隻怪物的具體 class 名稱。

---

## 2.2 Quest 與 Combat 分離

任務系統可以「監聽戰鬥結果」，但不能直接控制 DamageService 或 Health。

例如：

```text
Monster Die
   ↓
Combat / Gameplay Event
   ↓
Quest Tracker
   ↓
KillCount + 1
```

禁止：

```csharp
QuestManager → Health.SetHP()
QuestManager → CombatController.Attack()
```

任務只消費事件與更新任務狀態。

---

## 2.3 Server Authority

所有影響遊戲狀態的資料由 Server 決定：

- Spawn
- Entity 存在／死亡
- Quest Accept
- Quest Progress
- Quest Complete
- Reward Grant（本階段可用簡化測試 Reward）

Client 只能提出 Request / Input。

---

# 3. Map Skeleton

建立一張極簡測試地圖即可。

不要求：

- 正式青嵐城
- 正式地形
- 正式美術
- 大世界
- Streaming
- LOD
- Terrain 美術

可以使用：

- Plane
- Cube
- Capsule
- Placeholder

目標是驗證 Scene / Spawn / Entity 管線。

建議 Scene：

`Phase0D_TestScene`

---

# 4. Spawn System

建立最小 Spawn 系統。

至少支援：

### Player Spawn
- 玩家進入時由 Server 決定 Spawn Point。
- 兩個 Client 可以正常生成。
- 不得由 Client 自己決定 Network Authority Entity。

### Monster Spawn
至少建立一種測試怪物，例如：

`Phase0D_TestMonster`

要求：

- Server Spawn
- Networked Entity
- HP 沿用既有 Health
- 可以被 Phase 0-B Combat Framework 攻擊
- 死亡後可被 Quest 系統觀察

本階段不需要正式怪物 AI。

可以只有：

```text
Idle
 ↓
被攻擊
 ↓
受傷
 ↓
死亡
```

---

# 5. Entity / Monster 架構

不要為 Phase 0-D 建立另一套 Health / Damage。

必須沿用：

```text
Attack
 ↓
HitDetection
 ↓
DamageService
 ↓
Health
```

怪物只是另一個可以擁有 Health 的 Network Entity。

未來 Boss、NPC、玩家等可以各自擴充，但不要現在建立大型 Entity Framework。

---

# 6. Quest Data-driven

建立最小：

`QuestDefinition`

建議使用 ScriptableObject。

至少包含：

- QuestId
- DisplayName
- Description
- ObjectiveType
- TargetId
- RequiredCount
- RewardId / RewardType

本階段至少建立一個測試任務：

```text
QuestId: Q_PHASE0D_001
名稱：清理測試區
目標：擊敗 Phase0D_TestMonster
數量：3
```

---

# 7. Quest State Machine

至少有：

```text
Locked
 ↓
Available
 ↓
Accepted
 ↓
InProgress
 ↓
Completed
```

如果需要失敗狀態可以預留，但本階段不要擴充完整失敗／重置／分支系統。

### 最重要規則

Quest State 必須由資料與狀態機控制。

禁止使用大量：

```csharp
if (questId == "Q001")
```

來硬寫任務邏輯。

---

# 8. Quest Tracker

建立獨立 Quest Tracker／Quest System。

它應該能接收通用事件，例如：

```text
EnemyKilled(targetId)
```

然後：

```text
TargetId == Quest TargetId
        ↓
Progress += 1
```

不要讓 Quest System 直接搜尋 Scene 裡的怪物。

---

# 9. Quest Accept / Complete

最小流程：

```text
Player
 ↓
Quest Accept Request
 ↓
Server Validate
 ↓
Quest State = Accepted/InProgress
```

擊殺達標：

```text
EnemyKilled Event
 ↓
QuestTracker
 ↓
Progress == RequiredCount
 ↓
Completed
```

如果要測 Reward：

可以先用簡單 Debug Log 或測試 Counter。

不要現在做完整 Inventory / Economy。

---

# 10. Network Sync

至少同步必要 Quest 狀態：

- QuestId / 或固定測試 Quest 索引
- State
- Progress

不要把完整 ScriptableObject 網路同步。

建議：

```text
Networked Quest State
        ↓
QuestDefinition Registry
        ↓
Local Definition
```

與 Spirit Seal 的 Data-driven 網路原則一致。

---

# 11. 與 Combat Framework 整合

Phase 0-D 不修改 Combat Framework 的核心傷害管線。

允許增加通用 Gameplay Event，例如：

```text
CombatResult
 ↓
Target Died
 ↓
EnemyKilledEvent
```

Quest 只訂閱事件。

### 禁止

- Quest 直接呼叫 DamageService
- Quest 直接修改 Health
- Monster 自己偷偷修改 Quest Progress
- CombatController 硬編碼 QuestId

---

# 12. Test Automation

沿用之前的測試思維。

可以建立 `Phase0DTestRunner` 或類似測試工具。

至少能自動驗證：

```text
Connect
 ↓
Spawn Player
 ↓
Spawn 3 Test Monsters
 ↓
Accept Quest
 ↓
擊殺 Monster × 3
 ↓
Progress 1/3
Progress 2/3
Progress 3/3
 ↓
Quest Complete
```

如果自動測試與實際 Network 執行環境有差異，必須分別記錄。

不要只測本地單機就宣稱 Network Quest 完成。

---

# 13. Regression Test

至少保留：

### Network
- [ ] 1 Dedicated Server + 2 Client
- [ ] 玩家 Spawn
- [ ] 玩家同步
- [ ] Join / Leave

### Combat
- [ ] 玩家可以攻擊怪物
- [ ] DamageService 正常
- [ ] Health 正常
- [ ] 怪物死亡事件正常

### Spirit Seal
- [ ] Phase 0-C 三個靈印不被破壞
- [ ] 至少做一次赤炎／玄甲／影遁 Regression

### Quest
- [ ] Accept
- [ ] Progress
- [ ] Complete
- [ ] Quest State Sync

---

# 14. 明確禁止擴張

Phase 0-D 不做：

- 正式青嵐城
- 正式地圖美術
- 地形美術
- 大世界 Streaming
- LOD 系統
- 完整怪物 AI
- NPC 系統
- 對話系統
- 完整任務編輯器
- 任務分支
- 任務失敗系統
- 任務追蹤 UI 正式版
- 完整獎勵系統
- Inventory
- Economy
- Boss
- 副本
- PvP
- 公會
- 社交
- 雙修
- 完整元素系統
- 完整 Status Effect
- 商城／抽卡
- 正式動畫

---

# 15. 驗收標準

只有以下全部成立才可標記 COMPLETE：

### A. Map
- [ ] `Phase0D_TestScene` 可啟動
- [ ] Player Spawn 正常
- [ ] 2 Client 可進入同一場景

### B. Monster
- [ ] Test Monster Server Spawn
- [ ] 沿用既有 Health
- [ ] 可以被既有 Combat Framework 攻擊
- [ ] 死亡事件產生

### C. Quest
- [ ] QuestDefinition 建立
- [ ] QuestId / TargetId / RequiredCount Data-driven
- [ ] State Machine 建立
- [ ] Accept
- [ ] Progress
- [ ] Complete

### D. Network
- [ ] 1 Server + 2 Client
- [ ] Quest State Server Authority
- [ ] Progress 同步
- [ ] Join / Leave Regression

### E. Integration
- [ ] Combat → EnemyKilled → Quest Tracker
- [ ] 不建立第二套 Damage / Health
- [ ] Phase 0-C 靈印 Regression 通過

### F. Test
- [ ] 至少完成 1 次完整 3 Kill Quest Cycle
- [ ] 建議完成 ≥10 次 Quest Progress / Combat Event Regression
- [ ] 自動測試與實際 Network 測試結果分開記錄

### G. Documentation
- [ ] README 更新
- [ ] `docs/00_AI_HANDOFF_BRIDGE.md` 新增 CLAUDE-REPLY-008
- [ ] CHANGELOG 更新
- [ ] Unity / Fusion 版本記錄
- [ ] 測試結果與已知問題記錄

---

# 16. 架構問題處理

如果 Phase 0-D 發現既有 Combat / Network 架構不足：

不要直接大改。

先：

1. 找出問題
2. 找出最小擴充點
3. 保持 Phase 0-A～0-C 向後相容
4. 提出方案
5. 記錄到交接檔

只有真正阻塞 Phase 0-D 時才回頭修核心。

---

# 17. Phase 0-D 完成後

完成後 Phase 0 的核心技術驗證就具備：

```text
Network
   ↓
Player
   ↓
Combat
   ↓
Six Weapons
   ↓
Spirit Seals
   ↓
Map
   ↓
Spawn
   ↓
Monster
   ↓
Quest
```

下一階段正式進入：

# Phase 1 — 青嵐城 Vertical Slice

但在進 Phase 1 前，必須先由 ChatGPT 進行：

- Phase 0 全面 Code Review
- Network Architecture Review
- Combat Framework Review
- Data-driven Review
- Mobile Performance 初步檢查
- 技術債清單整理

**不要因為 Phase 0-D 完成就直接堆正式內容。**

---

# 18. 最重要的一句話

> **Phase 0-D 的目標不是做出地圖，而是證明「地圖、怪物、戰鬥、任務」可以在同一套 Server-authoritative 架構中正確串起來。**
