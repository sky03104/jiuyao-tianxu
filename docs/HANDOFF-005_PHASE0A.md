# 《九曜：天墟》HANDOFF-005

## Phase 0-A：Unity + URP + Photon Fusion 網路／戰鬥技術原型

**日期：2026-09-15**  
**狀態：READY TO EXECUTE**  
**前置：HANDOFF-004_ANIMATION_PIPELINE 已完成，Phase 0-A 已確認無阻塞項目**

---

## 1. 本次任務目的

正式開始《九曜：天墟》的第一個 Unity 技術原型階段。

本階段不是製作完整遊戲，而是驗證最核心的「多人同步 + 玩家移動 + 基礎戰鬥 + Server Authority」技術鏈。

完成後必須能證明：

> 兩個 Client 可以進入同一個房間，在同一場景中看見彼此、移動、進行基礎攻擊，攻擊命中後由 Server 判定傷害並同步 HP 狀態。

---

# 2. Phase 0-A 範圍

## 必須完成

### 0-A-01 Unity 專案骨架
- 建立 Unity 專案
- 使用 URP
- 建立清楚的 Assets 資料夾結構
- 建立基本測試 Scene
- 建立 README / 執行說明
- 記錄實際使用的 Unity 版本
- 記錄實際使用的 Photon Fusion 版本

### 0-A-02 Photon Fusion 基礎連線
- 建立 Host / Server / Client 所需架構
- 優先採 Server Authoritative 思維
- 建立最小可運作的 Network Runner
- 可以建立／加入測試房間
- 不可把 API Key、App ID、Secret 等敏感資訊直接 commit 到 GitHub
- 如果 Photon 帳號、App ID 或套件取得遇到外部設定阻塞，必須明確記錄，不得用假資料宣稱完成

### 0-A-03 兩 Client 同房
- Client A 進入房間
- Client B 進入同一房間
- 兩邊都能看見另一名玩家
- 玩家進入／離開狀態可以正確處理

### 0-A-04 玩家移動
- WASD／鍵盤可作為 Phase 0-A 測試輸入
- 架構必須保留未來接手機虛擬搖桿的輸入抽象層
- 移動由 Server Authority 架構處理
- 不先做完整角色動畫
- 使用 Placeholder Player 即可

### 0-A-05 玩家可見性與同步
至少同步：
- Player Spawn
- Position
- Rotation（如實作需要）
- 玩家存在／離開
- 基本 Network State

原則：

> 只同步必要狀態，不提前同步整個角色資料。

### 0-A-06 基礎戰鬥
建立最小戰鬥閉環：

`輸入攻擊 → Server 收到攻擊意圖 → Server 判定命中 → Server 計算傷害 → HP 改變 → Client 同步結果`

最低需求：
- 一個基本攻擊
- 一個簡單命中判定
- HP
- Damage
- Target
- 基本受擊回饋

可以使用簡單 Box / Sphere Collider 或其他最小化 Hit Detection。

暫時不追求漂亮特效。

### 0-A-07 Server Authority
所有重要結果必須由 Server 決定：
- 是否命中
- 傷害數值
- HP 變化
- 玩家死亡／不可戰鬥狀態（如本階段實作）

Client 不可以直接修改其他玩家 HP。

禁止採用「Client 自己算傷害，再把結果告訴 Server」作為正式架構。

### 0-A-08 穩定性測試
至少完成：
- 2 Client 同房
- 連續至少 10 次攻擊循環
- 每次循環包含：Attack → Hit/Miss → Damage → HP Sync
- 確認兩端狀態基本一致
- 測試玩家加入／離開後是否仍可繼續遊戲
- 記錄已知問題與同步誤差

---

# 3. 本階段明確禁止擴張

Phase 0-A 暫時不要做：

- 六大武器完整系統
- 雙修切換
- 完整靈印系統
- 八格靈印
- 裝備系統
- 完整角色養成
- 完整任務系統
- 完整青嵐城
- MMO 大世界
- 公會
- 經濟系統
- 商店
- 抽卡
- Battle Pass
- 完整 PvP
- 完整副本
- 世界 Boss
- 完整劇情
- 正式角色美術
- 完整角色動畫
- 動畫番劇製作

原因：

> Phase 0-A 的目標是驗證「技術地基」，不是提前製作內容。

---

# 4. 技術原則

## 4.1 Server Authoritative

網路架構必須從第一版就避免 Client Authority 汙染後續系統。

玩家可以送出「我想移動／我想攻擊」的輸入或意圖，但最終重要結果由 Server 決定。

## 4.2 Combat 與 Network 解耦

戰鬥邏輯不要全部寫死在 Network Behaviour 裡。

建議拆分概念：

- Player Input
- Player Controller
- Combat Controller
- Hit Detection
- Damage / HP
- Network Synchronization

目標是未來可以把單機測試、網路測試、正式戰鬥系統逐步替換，而不需要整套重寫。

## 4.3 Data-driven 準備

Phase 0-A 不需要完整資料表，但程式架構不要把武器、技能、傷害全部硬編碼在 UI 或單一 Player Script。

後續 Phase 0-B 六大武器與靈印系統會接上 Data-driven 設計。

## 4.4 Mobile-ready

Phase 0-A 可以先使用 PC / Unity Editor 作為快速網路測試環境。

但輸入系統不能直接綁死 WASD，必須讓未來可以替換為：

`Keyboard Input / Mobile Virtual Joystick → Input Layer → Player Controller`

本階段不要求完成手機 UI。

---

# 5. 建議執行順序

請嚴格按照以下順序，每一步完成後再進下一步：

1. Unity + URP 專案建立
2. Photon Fusion 套件／版本確認
3. Network Runner 基礎連線
4. Host / Server / Client 測試
5. Player Spawn
6. Player Movement
7. 兩 Client 互相看見
8. 基礎 Attack
9. Server Hit Detection
10. Server Damage / HP
11. Client HP Sync
12. 10 次連續 Combat Cycle
13. 加入／離開測試
14. 整理 README、測試報告、已知問題
15. 回報 Phase 0-A 完成度

如果前一步失敗，不要跳過直接堆功能。

---

# 6. Claude Code 執行規則

### 每一個階段完成後必須回報

- 做了什麼
- 修改哪些檔案
- 使用哪個 Unity 版本
- 使用哪個 Photon Fusion 版本
- 測試方式
- 測試結果
- 是否存在 blocker
- 下一步建議

### 如果遇到技術選擇

優先選：

1. 可維護
2. 可擴充
3. Server Authority
4. Data-driven
5. 手機相容
6. 最小可行實作

不要為了「看起來完成很多」而提前加入大型框架。

### 如果發現設計文件互相衝突

不要自行刪除舊規格。

請：
1. 找出衝突
2. 記錄衝突
3. 提出建議
4. 等待 ChatGPT／使用者決策，或依照已鎖定的 Handoff 決策執行

---

# 7. Phase 0-A 驗收標準

Phase 0-A 只有在以下條件全部成立後，才可以標記 COMPLETE：

### A. Network
- [ ] 2 Client 可以進入同一測試房間
- [ ] 兩端可以看到彼此
- [ ] 玩家位置同步正常
- [ ] 玩家加入／離開正常

### B. Combat
- [ ] 基本攻擊可以執行
- [ ] Server 判定命中
- [ ] Server 計算 Damage
- [ ] HP 正確同步
- [ ] Client 無法直接決定對方 HP

### C. Stability
- [ ] 至少完成 10 次 Attack → Hit → Damage → HP Sync 循環
- [ ] 測試過加入／離開
- [ ] 沒有重大同步錯誤

### D. Documentation
- [ ] Unity 版本已記錄
- [ ] Fusion 版本已記錄
- [ ] README 已更新
- [ ] 測試結果已記錄
- [ ] 已知問題已記錄

---

# 8. Phase 0-A 完成後的下一階段

Phase 0-A 完成後，不要直接跳到完整 MMORPG。

下一步依 Roadmap 進入：

### Phase 0-B
六大武器路線最小可玩戰鬥 Loop：

- 刀修
- 劍修
- 槍修
- 弓修
- 重刃
- 靈杖

每個路線至少具備一個可以辨識其戰鬥特色的技能／攻擊 Loop。

之後再進入：

- 靈印 Data-driven Prototype
- 地圖／Spawn 骨架
- Quest State Machine
- Phase 1 青嵐城 Vertical Slice

雙修切換按照既有決策，不提前塞回 Phase 0-A。

---

# 9. 與動畫線的關係

動畫製作仍然是第二軌，不阻塞遊戲技術開發。

目前：

- 不需要為 EP00 支付 US$30–50 測試預算
- 優先採用免費／低成本工具驗證
- 不讓動畫工具選型阻塞 Unity Phase 0-A

動畫資產未來仍要與遊戲角色／場景資產共享，但本階段不製作正式動畫。

---

# 10. Mobile Performance Budget

目前不把完整手機硬體／Polygon／Draw Call／FPS 預算當成 Phase 0-A blocker。

但是 Phase 0-A 執行期間請開始記錄：

- Editor / Standalone 基本 FPS
- CPU / GPU 大致狀況
- Network Tick / RTT
- Spawn 數量
- 基礎 Scene 負載

正式 Mobile Performance Budget 預計在取得第一批技術測試資料後，再更新 `17_TECH_ARCHITECTURE_V1.0.md`。

---

# 11. 最重要的一句話

> **先把「兩個玩家能穩定連線、移動、互相攻擊、由 Server 正確判定傷害」做成，再開始堆內容。**

這是《九曜：天墟》正式進入開發階段的第一個技術里程碑。
