# 《九曜：天墟》HANDOFF-006

## Phase 0-B：六大武器最小可玩 Loop + Combat Framework

**日期：2026-09-15**  
**狀態：READY TO EXECUTE**  
**前置：HANDOFF-005 Phase 0-A PASS**

---

# 1. 本階段目的

Phase 0-A 已完成「Dedicated Server + 2 Client + 移動 + 基礎攻擊 + Server Authority」技術地基。

Phase 0-B 現在正式把這個技術地基升級成可以承載正式遊戲戰鬥的 **Combat Framework**，並在同一套框架上完成六大武器路線的最小可玩 Loop。

本階段的核心不是做六套獨立戰鬥程式，而是驗證：

> **一套共用 Combat Framework，可以用 Data-driven / 可擴充方式承載六種明顯不同的戰鬥節奏。**

六大武器：

1. 刀修
2. 劍修
3. 槍修
4. 弓修
5. 重刃
6. 靈杖

---

# 2. 最重要的架構原則

## 2.1 禁止六套獨立 Combat Controller

不可做成：

```text
SwordCombat.cs
SpearCombat.cs
BowCombat.cs
HeavyBladeCombat.cs
StaffCombat.cs
KnifeCombat.cs
```

然後每個檔案各自處理攻擊、傷害、Cooldown、Hit Detection。

正確方向應該是：

```text
                Combat Framework
                       │
        ┌──────────────┼──────────────┐
        ▼              ▼              ▼
   Input Layer     Combat State    Hit/Damage
        │              │              │
        └──────────────┼──────────────┘
                       ▼
                 Weapon Definition
                       │
        ┌──────┬──────┼──────┬──────┐
        ▼      ▼      ▼      ▼      ▼
       刀      劍      槍      弓    重刃/靈杖
```

可以有少量武器專屬行為，但不能複製整套戰鬥架構。

---

# 3. Phase 0-B 必須完成的內容

## 0-B-01 Combat Framework 骨架

整理 Phase 0-A 現有：

- PlayerInputData
- PlayerMovement
- PlayerCombat
- Health
- Hit Detection

讓 Combat 不再全部依賴單一 PlayerCombat Script。

建議最小責任分離：

```text
CombatInput
CombatController
CombatState
AttackDefinition
HitDetection
DamageService
Health
```

實際類別名稱可由 Claude Code 決定，但責任邊界必須清楚。

---

## 0-B-02 Attack Definition / Data-driven

至少建立可資料驅動的 Attack Definition。

每個攻擊至少能配置：

- Attack ID
- 武器類型
- 攻擊段數
- Damage
- Attack Range
- Hit Window
- Cooldown / Recovery
- 是否可連段
- 是否可移動
- 基本資源消耗（若需要）

**不要把六種武器的數值全部硬編碼在 Combat Controller。**

可以先使用 ScriptableObject 或其他適合 Unity 的資料驅動方式。

本階段不要求最終資料格式定案，但必須確保後續能擴充。

---

# 4. 六大武器 MVP 規格

注意：以下是 Phase 0-B 的「辨識性驗證」，不是正式完整技能表。

每一種武器至少要做到：

`普攻 / 核心動作 → 命中 → 傷害 → 明顯不同的節奏或機制`

不要求正式美術、不要求正式特效、不要求完整 3D 動畫。

---

## 4.1 刀修 — 近戰爆發

核心特色：

- 近距離
- 3 段連擊
- 第 3 段較高傷害
- 末段有明顯硬直／擊退表現

MVP Loop：

```text
Attack 1
  ↓
Attack 2
  ↓
Attack 3
  ↓
Heavy Hit / Knockback
```

玩家應該能明顯感覺「重擊爆發」。

---

## 4.2 劍修 — 高機動連擊

核心特色：

- 近距離
- 4～5 段快速連擊
- Recovery 比刀短
- 可在連段中保持移動

MVP Loop：

```text
Fast Hit × 4~5
      ↓
短後搖
      ↓
快速重新接近目標
```

重點是讓玩家感覺「快」，而不是只把刀的 Damage 改小。

---

## 4.3 槍修 — 中距離控制

核心特色：

- 比刀劍更遠
- 突刺
- 穿透／直線攻擊概念
- 可加入簡單破甲標記

MVP Loop：

```text
中距離
 ↓
Thrust
 ↓
Hit
 ↓
Armor Break / Push
```

---

## 4.4 弓修 — 遠程蓄力

核心特色：

- 遠距離
- 蓄力時間
- 放開射擊
- 距離優勢

MVP Loop：

```text
Hold
 ↓
Charge
 ↓
Release
 ↓
Projectile
 ↓
Hit
```

本階段可以使用簡單 Projectile，不需要正式箭矢模型。

---

## 4.5 重刃 — 慢速防禦控場

核心特色：

- 攻擊速度慢
- 高傷害
- 具簡單霸體／減傷概念
- 擊退或範圍控制

MVP Loop：

```text
Wind-up
 ↓
Heavy Swing
 ↓
AOE Hit
 ↓
Knockback
```

至少要與刀修在速度／範圍／重量感上明顯不同。

---

## 4.6 靈杖 — 遠程法術控制

核心特色：

- 遠程
- 法術彈道或地面區域
- Casting Time
- AOE

MVP Loop：

```text
Cast
 ↓
Delay
 ↓
AOE / Projectile
 ↓
Hit
 ↓
Control
```

可以先用 Sphere / Plane / Particle Placeholder 表現。

---

# 5. 六種武器的辨識度驗收

不能只改：

- Damage
- Attack Speed
- Range

就說六種武器完成。

至少必須在以下項目中有明顯差異：

| 武器 | 主要辨識點 |
|---|---|
| 刀 | 近戰爆發、三段連擊、末段重擊 |
| 劍 | 快速多段、高機動 |
| 槍 | 中距離、直線突刺、破甲 |
| 弓 | 遠距離、蓄力、Projectile |
| 重刃 | 慢、重、AOE、控場 |
| 靈杖 | 遠程、吟唱、法術區域 |

驗收時應能在沒有正式美術的情況下，單靠操作與 Placeholder 表現分辨六種流派。

---

# 6. Combat State Machine

至少建立基本戰鬥狀態概念：

```text
Idle
 ↓
AttackStart
 ↓
AttackActive
 ↓
AttackRecovery
 ↓
Idle / NextCombo
```

後續需要能擴充：

```text
Dodge
Block
Stun
Knockback
Cast
Charge
Death
```

但 Phase 0-B 不要求一次全部完成。

**重要：不要把所有狀態都塞進一個巨大 switch-case PlayerCombat.cs。**

---

# 7. Combo 系統

本階段建立最小連段系統：

- Combo Step
- Combo Window
- Combo Reset
- Attack Recovery
- 下一段輸入緩衝（可選）

至少刀與劍要真正具有連段差異。

其他武器可以用自己的節奏驗證。

---

# 8. Hit Detection 設計

Phase 0-A 的簡單 `OverlapSphere` 可以保留作為技術原型，但 Phase 0-B 必須把 Hit Detection 從 PlayerCombat 中抽出。

建議支援：

- Sphere
- Box
- Capsule
- Projectile
- Area / AOE

第一版不需要做完整武器碰撞骨架。

但 API 必須允許未來替換為真正的攻擊判定框。

---

# 9. Damage 系統

Damage 不應該直接等於：

```text
Health -= 10
```

Phase 0-B 至少建立：

```text
Attack Definition
      ↓
Damage Request
      ↓
Damage Service
      ↓
Damage Result
      ↓
Health
```

Damage Result 未來需要能擴充：

- Raw Damage
- Crit
- Element
- Armor
- Resistance
- Shield
- Status Effect
- Source / Attacker

但是本階段只需要最小實作，不要提前完成完整屬性系統。

---

# 10. Server Authority 延續規則

Phase 0-A 的 Server Authority 必須完整保留。

Client：

- 提交 Input / Attack Intent
- 不直接決定 Damage
- 不直接修改目標 HP
- 不提交「我造成 100 傷害」這類結果資料

Server：

- 驗證 Attack Intent
- 驗證攻擊狀態
- 決定 Hit
- 決定 Damage
- 修改 HP
- 同步結果

本階段任何為了方便而加入的 Client-side Damage shortcut 都禁止進正式架構。

---

# 11. 暫時不要加入的系統

Phase 0-B 禁止擴張到：

- 雙修切換
- 完整閃避／完美閃避
- 完整格擋
- 完整 Boss 機制
- 元素完整系統
- 完整暴擊系統
- 完整裝備
- 完整靈印
- 完整養成
- PvP 排位
- MMO 大世界
- 公會
- 經濟
- 商城
- 抽卡
- 正式角色美術
- 正式動畫
- 完整手機 UI

原因：先驗證 Combat Framework 能否承載六種戰鬥節奏。

---

# 12. 與現有戰鬥規格的關係

`03_COMBAT_SYSTEM_V1.0.md` 已定義正式方向：

- 刀：近戰爆發
- 劍：高機動連擊
- 槍：中距離控制穿透
- 弓：遠程風箏／蓄力
- 重刃：防禦坦克／控場
- 靈杖：法術範圍控制

Phase 0-B 只驗證這些「戰鬥身份」，不把 V1.0 所有正式數值與高階機制一次做完。fileciteturn68file0

正式數值仍以 Phase 0 原型結果調整。

---

# 13. 建議實作順序

嚴格按照：

```text
0-B-01 Combat Framework 骨架
        ↓
0-B-02 Attack Definition
        ↓
0-B-03 Combo / State Machine
        ↓
0-B-04 Hit Detection 抽離
        ↓
0-B-05 Damage Service
        ↓
0-B-06 刀修
        ↓
0-B-07 劍修
        ↓
0-B-08 槍修
        ↓
0-B-09 弓修
        ↓
0-B-10 重刃
        ↓
0-B-11 靈杖
        ↓
0-B-12 六流派對照測試
        ↓
0-B-13 Server Authority / Network Regression
        ↓
0-B-14 文件與測試報告
```

如果 Combat Framework 在刀修之前就已經出現架構問題，先修 Framework，不要繼續複製問題到其他武器。

---

# 14. 驗收標準

Phase 0-B 必須全部符合才可以標記 COMPLETE。

## A. Framework

- [ ] Combat 不再依賴單一巨大 PlayerCombat Script
- [ ] Attack Definition 可資料驅動
- [ ] Hit Detection 可獨立替換
- [ ] Damage 流程有清楚責任邊界
- [ ] Combo / State 有基本架構

## B. 六大武器

- [ ] 刀修可完成三段近戰爆發 Loop
- [ ] 劍修可完成快速多段連擊 Loop
- [ ] 槍修可完成中距離突刺 Loop
- [ ] 弓修可完成蓄力 Projectile Loop
- [ ] 重刃可完成慢速 AOE 控場 Loop
- [ ] 靈杖可完成 Casting + 遠程／AOE Loop

## C. 辨識度

- [ ] 六種武器不能只靠數值差異
- [ ] 玩家能透過操作節奏辨認武器
- [ ] 至少刀／劍／弓／重刃在手感上明顯不同

## D. Network

- [ ] 仍由 Server 決定 Hit
- [ ] 仍由 Server 決定 Damage
- [ ] 仍由 Server 修改 HP
- [ ] 2 Client regression test 通過
- [ ] 至少每種武器完成多次攻擊測試

## E. Documentation

- [ ] README 更新
- [ ] 新增／修改檔案清單
- [ ] Unity / Fusion 版本維持記錄
- [ ] 測試結果記錄
- [ ] 已知問題記錄
- [ ] Claude Code 回報寫入 `docs/00_AI_HANDOFF_BRIDGE.md`

---

# 15. Phase 0-B 完成後

不要直接進入完整 MMORPG。

下一步：

### Phase 0-C
**Spirit Seal Data-driven Prototype**

驗證：

```text
Spirit Seal Definition
        ↓
Equip / Unequip
        ↓
Combat Modifier / Rule Modifier
        ↓
Server Authority
        ↓
可切換測試內容
```

接著：

### Phase 0-D
**Map / Spawn / Quest Skeleton**

最後才進：

### Phase 1
**青嵐城 Vertical Slice**

---

# 16. Claude Code 執行規則

每完成一個子階段都要回報：

- 做了什麼
- 修改哪些檔案
- Framework 如何分層
- 哪些資料已 Data-driven
- Server Authority 如何維持
- 測試方式
- 測試結果
- 是否有 blocker
- 下一步

如果發現現有 Phase 0-A 程式需要重構：

> 可以重構，但不得破壞 Phase 0-A 已驗證的 Dedicated Server、2 Client、Movement、Network Sync、Server Authority 能力。

如果發現需要大規模改架構：

> 先停下來回報，不要自行擴張成完整 MMORPG Framework。

---

# 17. 本階段最重要的一句話

> **六種武器可以不同，但六種武器不能變成六套遊戲。**

Combat Framework 是 Phase 0-B 真正的交付成果；六大武器只是用來證明這套 Framework 真的能用。

# 文件結束
