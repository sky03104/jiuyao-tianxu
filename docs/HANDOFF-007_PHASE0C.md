# 《九曜：天墟》HANDOFF-007

## Phase 0-C：靈印 Data-driven Prototype

**日期：2026-09-16**  
**狀態：READY TO EXECUTE**  
**前置：HANDOFF-006 Phase 0-B 已完成並通過 Combat Framework 驗收**

---

# 1. 本次任務目的

本階段不是製作完整的靈印系統，而是驗證：

> **現有 Combat Framework 能否安全承載「改變戰鬥規則」的靈印機制。**

Phase 0-C 完成後，至少要能讓玩家：

```text
取得／建立靈印
    ↓
裝備至固定 8 槽
    ↓
Combat Controller 讀取靈印
    ↓
觸發條件成立
    ↓
Modifier / Trigger 改變戰鬥結果
    ↓
Server Authority 判定
    ↓
同步到 Client
```

重點不是 UI，而是**資料結構、觸發管線、權限與可擴充性**。

---

# 2. 本階段只做 3 個代表性靈印

先實作：

1. **赤炎**：攻擊命中後施加簡化燃燒效果。
2. **玄甲**：受到致命傷害時產生一次簡化護盾／保命效果。
3. **影遁**：成功閃避／位移事件後，下一次攻擊獲得一次簡化特殊效果。

### 注意

這三個效果都是 Prototype。

不要自行加入完整的：

- 元素系統
- 完整 Status Effect 系統
- 完整護盾系統
- 完美閃避系統
- 隱身渲染系統
- 完整 Buff/Debuff UI
- 靈印連鎖
- 雙脈重新加權

如果某個效果需要尚未存在的系統，建立**最小可驗證 Hook**即可，不要順手把整套系統做完。

---

# 3. 固定 8 槽規則

角色永遠只有：

**8 個共用靈印槽位。**

禁止：

- 雙脈變成 16 槽
- 每把武器各有一套靈印槽
- Client 自己維護另一份權威靈印狀態

建議建立資料概念：

```text
SpiritSealLoadout
 ├─ Slot 0
 ├─ Slot 1
 ├─ Slot 2
 ├─ Slot 3
 ├─ Slot 4
 ├─ Slot 5
 ├─ Slot 6
 └─ Slot 7
```

本階段可只開放測試用裝備／卸下功能，不需要正式背包。

---

# 4. SpiritSealDefinition

建立 Data-driven ScriptableObject，例如：

`SpiritSealDefinition`

至少應能描述：

- SealId
- 顯示名稱
- 靈印類型／標籤
- 可觸發事件類型
- Cooldown
- 是否可裝備
- Prototype 所需的效果參數

**靈印資料不能硬編碼在 CombatController。**

例如不要做：

```csharp
if (sealName == "赤炎") { ... }
```

應該由資料描述效果，再由共用系統執行。

---

# 5. Trigger / Modifier 架構

建議最低分層：

```text
Combat Event
     ↓
SpiritSeal Trigger
     ↓
SpiritSeal Modifier
     ↓
Damage / Status / Shield Result
```

但不要為了漂亮架構一次建立過度複雜的 Event Bus / ECS / ScriptableObject Factory。

只要做到：

> **新增第四個靈印時，不需要修改 CombatController 的核心流程。**

就是成功。

---

# 6. Server Authority

靈印效果涉及戰鬥結果，因此必須由 Server 決定：

- 是否裝備成功
- 觸發條件是否成立
- Cooldown 是否可用
- 傷害／燃燒是否成立
- 護盾是否成立
- 影遁效果是否成立

Client 可以提出輸入／意圖，但不能直接：

- 修改靈印槽位
- 修改靈印 Cooldown
- 修改 HP
- 直接套用燃燒
- 直接產生護盾

Networked State 只同步必要狀態。

---

# 7. 三個 Prototype 的最低驗收行為

## 7.1 赤炎

最小版本：

```text
Attack 命中
 ↓
Server 判定
 ↓
赤炎 Trigger
 ↓
目標獲得 Burning 狀態
 ↓
後續一次或數次傷害增加／追加簡化火傷
```

不要求正式元素系統。

只要能證明：

**靈印可以透過 Combat Event 改變後續戰鬥結果。**

---

## 7.2 玄甲

最小版本：

```text
受到致命傷害
 ↓
Server 判定
 ↓
玄甲 Trigger
 ↓
消耗一次保命機會
 ↓
HP 保留在最低值
```

可把完整護盾暫時簡化成「一次致命傷害保命」。

目標是驗證：

**靈印可以介入 Health/Damage 流程。**

---

## 7.3 影遁

因完整 Dodge 尚未進入本階段，Prototype 可建立最小測試事件：

```text
測試 Dodge / Evade Event
 ↓
Server 判定成功
 ↓
影遁進入 Armed 狀態
 ↓
下一次 Attack 命中
 ↓
消耗 Armed
 ↓
產生一次特殊效果
```

「特殊效果」可先用簡單的額外傷害或命中標記驗證。

不要現在製作完整隱身視覺效果。

---

# 8. Cooldown

Phase 0-B 已經在 AttackDefinition 建立 Cooldown 資料欄位，但本階段要開始建立**靈印自己的 Cooldown 概念**。

最低需求：

```text
Trigger
 ↓
Cooldown Start
 ↓
Cooldown Active
 ↓
再次 Trigger
 ↓
被拒絕
 ↓
Cooldown 結束
 ↓
可以再次 Trigger
```

Cooldown 必須以 Server 時間／Network Tick 等權威時間概念為基礎。

不要使用 Client 本地時間作為正式權威判定。

---

# 9. 與 DamageService 的整合

現有 Phase 0-B 已建立：

```text
DamageRequest
 ↓
DamageService
 ↓
DamageResult
 ↓
Health
```

Phase 0-C 優先沿用這條管線。

不要另外建立第二套傷害系統。

建議方向：

```text
Attack
 ↓
Combat Event
 ↓
Spirit Seal Modifier
 ↓
DamageRequest / DamageResult
 ↓
DamageService
 ↓
Health
```

如果需要追加效果，建立最小的 Status / Effect Hook，但仍須維持單一權威傷害入口。

---

# 10. Data-driven 驗收方式

必須證明「改資料，不改核心程式」可以改變測試結果。

至少完成一次：

```text
赤炎 Damage/Trigger 參數
        ↓
修改 ScriptableObject 資料
        ↓
不修改 CombatController
        ↓
重新執行測試
        ↓
結果發生預期變化
```

這是本階段的重要驗收項目。

---

# 11. Network Regression Test

沿用 Phase 0-A / 0-B：

**1 Dedicated Server + 2 Client**

至少驗證：

- 兩 Client 都能裝備／看到正確的測試靈印狀態
- Server 決定 Trigger
- Server 決定 Cooldown
- 赤炎效果同步
- 玄甲保命同步
- 影遁 Armed / 消耗狀態同步
- Client 無法直接修改上述狀態
- 玩家 Join / Leave 後系統不崩潰

至少完成 **10 次靈印相關 Combat Cycle**。

建議每個 Prototype 至少各測 10 次，總數 ≥30 次。

---

# 12. 測試替身

可以沿用 Phase 0-B 的 `AutoTestInputProvider`。

允許增加：

- 自動裝備測試靈印
- 自動切換測試靈印
- 自動觸發 Dodge/Evade 測試事件
- 自動攻擊

但測試程式不得成為正式遊戲邏輯依賴。

---

# 13. 明確禁止擴張

Phase 0-C 不做：

- 8 個靈印全部完成
- 靈印品質／黃玄地天完整系統
- 3合1融煉
- 靈核經濟
- 靈印背包
- 靈印 UI 正式版
- 靈印強化
- 同屬性共鳴
- 連鎖組合
- 雙脈靈印重新加權
- 完整元素系統
- 完整 Status Effect Framework
- 完整 Shield Framework
- 完整 Dodge / Perfect Dodge
- 裝備系統
- 六武器新增技能
- Boss
- PvP
- MMO 大世界
- 商店／抽卡／商城

這些全部留到後續階段。

---

# 14. 完成條件

只有以下全部成立才可標記 COMPLETE：

### A. Data-driven
- [ ] `SpiritSealDefinition` 建立
- [ ] 固定 8 槽建立
- [ ] 赤炎／玄甲／影遁為資料資產
- [ ] 新增／調整靈印參數不需要修改 CombatController

### B. Combat Integration
- [ ] Trigger 管線建立
- [ ] Modifier / Effect Hook 建立
- [ ] 沿用既有 DamageService
- [ ] 不建立第二套 Health/Damage 權威入口

### C. Prototype
- [ ] 赤炎可觸發
- [ ] 玄甲可保命一次
- [ ] 影遁可 Armed → 下一次攻擊消耗
- [ ] Cooldown 生效

### D. Network
- [ ] 1 Server + 2 Client
- [ ] Server Authority
- [ ] 必要狀態同步
- [ ] Client 無法直接改靈印戰鬥結果

### E. Test
- [ ] 每個 Prototype 至少 10 次
- [ ] 總靈印 Combat Cycle ≥30
- [ ] Join / Leave regression
- [ ] Data-driven 修改測試

### F. Documentation
- [ ] README 更新
- [ ] `docs/00_AI_HANDOFF_BRIDGE.md` 新增 CLAUDE-REPLY-007
- [ ] CHANGELOG 更新
- [ ] 記錄 Unity / Fusion 版本
- [ ] 記錄測試方式、結果與已知問題

---

# 15. 遇到架構問題時的處理原則

如果 Claude Code 發現現有 Combat Framework 無法乾淨支援靈印：

**不要直接重寫 Phase 0-B。**

先：

1. 指出問題
2. 找出最小擴充點
3. 提出方案
4. 優先採向後相容方式
5. 記錄於交接檔

只有在確實阻塞 Phase 0-C 時，才回頭修 Phase 0-B。

---

# 16. 最重要的一句話

> **Phase 0-C 不是把靈印做多，而是證明「靈印可以成為可擴充的戰鬥規則層」。**

如果三個靈印可以在不修改 CombatController 核心流程的前提下運作，並通過 Dedicated Server + 2 Client 測試，本階段即達成目的。
