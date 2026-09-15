# 《九曜：天墟》動畫製作流程 V1.0

> 文件狀態：正式規劃文件
>
> 目的：在 Unity Phase 0-A 正式開發前，先鎖定《九曜：天墟》的動畫生產工具鏈、成本控制方式、資產規格與 AI 輔助流程，避免後續因平台訂閱、匯出限制、商用授權或工具更換造成製作中斷。

---

## 1. 文件定位

《九曜：天墟》採「遊戲 × 動畫 × 短影音」跨媒體策略。

三者共用角色、武器、場景、特效與世界觀資產，但生產方式必須保持彈性：

- 遊戲：Unity 為主要執行平台。
- 動畫：Blender + AI 動作輔助 + Unity Cinematic 為主要流程。
- 短影音：由動畫與遊戲 Cinematic 素材二次剪輯產生。
- Claude Code：負責技術整合與專案檔案，不負責取代動畫師做創意動畫。

核心原則：

> **AI 負責加速，標準格式負責自由，Blender/Unity 負責掌控。**

不得讓單一 AI 平台成為《九曜：天墟》的唯一生產依賴。

---

# 2. 工具鏈總覽

## 2.1 第一階段固定工具

| 工作 | 主力工具 | 初期策略 | 定位 |
|---|---|---|---|
| 3D 建模／動畫修正 | Blender | 免費 | 動畫核心 |
| 遊戲開發 | Unity Personal | 免費起步 | 遊戲核心 |
| 遊戲劇情動畫 | Unity Timeline | 隨 Unity | Cutscene |
| 鏡頭 | Cinemachine | 隨 Unity | Cinematic Camera |
| AI 動作捕捉 | Rokoko Studio Starter | 免費起步 | 第一 AI 動作工具 |
| AI 動作備用 | DeepMotion | 有需要才付費 | 第二 AI 動作工具 |
| 影片剪輯 | DaVinci Resolve Free | 免費起步 | 最終剪輯 |
| 分鏡／概念 | ChatGPT + 生圖工具 | 既有工具優先 | 前期設計 |
| 配音 | AI Voice | 後期再選 | 角色聲音 |
| 音效 | AI / 免費音效庫 | 後期再選 | SFX |
| 音樂 | AI / 免費音樂工具 | 後期再選 | BGM |

## 2.2 成本原則

第一個可行性測試 EP00 不預設任何新增月費。

優先使用：

- Blender：免費。
- Unity Personal：符合官方資格時免費起步。
- Rokoko Studio Starter：先使用免費方案測試。
- DaVinci Resolve Free：免費剪輯。

只有當免費方案無法滿足實際製作需求時，才評估付費工具。

---

# 3. 工具選擇的七條硬規則

任何未來新增動畫工具，都必須通過以下條件。

### 3.1 必須可以低成本開始

至少要能用免費方案、試用額度或一次性低成本方式驗證工作流程。

### 3.2 必須可以輸出標準格式

優先：

- FBX
- GLB / GLTF
- BVH
- PNG
- WAV
- MP4

避免把最終資產鎖在單一平台的私有格式。

### 3.3 必須能離開平台

如果某工具停止服務，《九曜》仍必須能靠本地資產繼續製作。

### 3.4 必須檢查商用授權

任何 AI 工具在正式商用前，必須重新確認當期服務條款、輸出內容授權與素材來源限制。

> 價格與授權可能改變，因此本文件的工具價格描述不是永久法律或商務保證；正式付款前必須再次查看官方條款。

### 3.5 不允許無上限失控計費

優先固定月費、免費額度或可預測 credits。

若服務採 credits 計費，必須先測試「一次完整鏡頭」實際消耗量，再決定是否訂閱。

### 3.6 必須能與 Blender / Unity 串接

不能選擇只能在單一網站內完成、無法匯出標準資產的核心工具。

### 3.7 不因 AI 流行而增加工具數量

新工具必須解決現有工具無法解決的問題，否則不加入正式 Pipeline。

---

# 4. 《九曜》正式動畫 Pipeline

```text
世界觀／劇本
      ↓
分鏡腳本
      ↓
Shot List
      ↓
角色／場景／武器資產
      ↓
AI 動作生成／Motion Capture
      ↓
Blender 動作清理
      ↓
Unity 資產整合
      ↓
Timeline
      ↓
Cinemachine 鏡頭
      ↓
燈光／VFX／環境
      ↓
角色配音／BGM／SFX
      ↓
Render
      ↓
DaVinci Resolve
      ↓
YouTube 長片／Shorts／抖音
```

禁止的思維：

> 「讓 AI 一次生成完整 5～10 分鐘動畫。」

採用的思維：

> **固定 3D 資產 + AI 輔助動作 + Shot-by-Shot Cinematic。**

---

# 5. Blender 的核心定位

Blender 是《九曜：天墟》的動畫資產保險庫。

主要負責：

- 角色模型整理
- 骨架
- Rig
- IK
- 動作 Retarget
- 動作清理
- 動畫混合
- 動作微調
- 武器掛點
- 表情前置
- 場景整理
- 鏡頭測試
- 動畫匯出
- FBX / GLB / BVH 處理

即使所有 AI 動畫服務未來停止，《九曜》仍能透過 Blender 繼續處理既有資產。

---

# 6. Unity 的動畫定位

Unity 不負責取代完整動畫製作軟體，而是負責：

- 遊戲內角色動畫
- 遊戲內 Cutscene
- Timeline
- Cinemachine
- 場景燈光
- VFX
- NPC 行為
- 玩家角色
- Boss 演出
- 遊戲內鏡頭
- 最終互動式劇情

## 6.1 Timeline

所有正式遊戲 Cutscene 優先使用 Timeline 組織：

```text
Timeline
├── Camera Track
├── Character Track
├── Animation Track
├── Audio Track
├── VFX Track
└── Activation Track
```

## 6.2 Cinemachine

鏡頭不要全部寫死在程式中。

優先透過 Cinemachine 管理：

- 追蹤
- 推拉
- 轉向
- 戰鬥鏡頭
- Boss 鏡頭
- 特寫
- 運鏡切換

---

# 7. AI Motion Capture 工作流程

## 7.1 首選流程

```text
動作需求
 ↓
真人影片／文字動作描述
 ↓
Rokoko / AI Motion
 ↓
FBX / BVH 等標準格式
 ↓
Blender
 ↓
清理腳滑
 ↓
修正關節
 ↓
修正武器握持
 ↓
調整速度
 ↓
輸出 Unity 動畫
```

## 7.2 第二選擇

若第一工具無法得到理想結果：

```text
DeepMotion
 ↓
輸出
 ↓
Blender 清理
 ↓
Unity
```

不得同時長期訂閱多個 AI 動作服務，除非實際製作測試證明兩者各自有不可替代的價值。

---

# 8. 動作資產標準

每個主要角色至少建立：

### 基礎

- Idle
- Walk
- Run
- Jump
- Fall
- Land

### 戰鬥

- Normal Attack
- Skill 01
- Skill 02
- Skill 03
- Ultimate
- Dodge
- Hit
- Knockback
- Death

### 角色專屬

依武器與人物個性增加：

- Weapon Draw
- Weapon Sheathe
- Signature Action
- Dialogue Gesture
- Emotional Gesture

---

# 9. 角色一致性系統

主要角色必須建立 Character DNA。

至少包含：

```text
角色名稱
身高
體型
臉型
髮型
髮色
眼睛
膚色
服裝
鞋子
武器
配件
主色
副色
戰鬥姿態
待機姿勢
性格關鍵詞
```

同時保留：

- Front
- Side
- Back
- Expression Sheet
- Full Body
- Weapon Sheet

AI 不得重新自由設計主要角色。

AI 只能根據既有 Character DNA 產生：

- 動作
- 表情草稿
- 分鏡參考
- 概念參考

---

# 10. Gameplay Animation 與 Cinematic Animation 分離

兩者共用模型、骨架、武器與材質，但動畫用途分開。

## Gameplay

要求：

- 快
- 清楚
- 可循環
- 可被玩家中斷
- 低延遲

## Cinematic

要求：

- 演技
- 鏡頭感
- 情緒
- 動作完整
- 可以犧牲即時操作性換取畫面表現

不要強迫一套動畫同時滿足兩種用途。

---

# 11. Shot List 制度

每一段動畫都先拆成 Shot。

例如 EP00：

| Shot | 時間 | 內容 | 鏡頭 |
|---|---:|---|---|
| S001 | 6s | 青嵐古林夜景 | Wide |
| S002 | 5s | 樹林風吹 | Tracking |
| S003 | 5s | 陣眼特寫 | Close-up |
| S004 | 8s | 江祈璟進入畫面 | Medium |
| S005 | 8s | 赤瞳妖將出現 | Low Angle |
| S006 | 8s | 妖將抬頭 | Close-up |
| S007 | 10s | 天空裂痕 | Wide |
| S008 | 5s | 江祈璟反應 | Close-up |
| S009 | 5s | 黑屏 | Fade |

每個 Shot 必須可以獨立測試。

---

# 12. EP00 動畫品質測試

正式製作 12 集動畫以前，先做 60～90 秒內部測試片。

## 12.1 建議內容

```text
青嵐古林
 ↓
夜晚
 ↓
陣眼
 ↓
江祈璟出現
 ↓
赤瞳妖將現身
 ↓
天空裂痕
 ↓
黑屏
```

## 12.2 測試項目

### 角色

- 模型一致
- 服裝一致
- 武器一致
- 比例一致

### 動作

- AI Motion 品質
- 腳部滑動
- 武器穿模
- 手部穿模
- 動作速度
- 動作重量感

### 鏡頭

- Wide
- Medium
- Close-up
- Tracking
- Low Angle

### 場景

- 夜景
- 燈光
- Fog
- Environment VFX

### Boss

- 出場壓迫感
- 體型比例
- 動作重量

### 天墟裂痕

- VFX
- 光照
- 聲音
- 空間扭曲感

### 後期

- BGM
- SFX
- 配音
- 字幕
- Color Grading
- Export

---

# 13. EP00 通過標準

EP00 不要求「電影級完成度」。

但必須證明：

1. 角色可以保持一致。
2. AI 動作可以實際進入 Blender。
3. Blender 可以完成清理。
4. Unity 可以正常播放。
5. Timeline 可以控制演出。
6. Cinemachine 可以完成鏡頭。
7. VFX 可以與動畫配合。
8. 聲音可以後製。
9. DaVinci 可以完成最終剪輯。
10. 同一套資產可以再次用於 Shorts。

若 10 項中有任何核心環節失敗，先修 Pipeline，不急著製作 EP01。

---

# 14. 動畫成本控制策略

## Level 1：Shorts

長度：10～60 秒。

用途：

- 世界觀介紹
- 角色介紹
- 戰鬥片段
- Boss 片段
- 開發花絮

成本最低。

## Level 2：Story Short

長度：2～5 分鐘。

用途：

- NPC 故事
- 支線故事
- 世界觀補完
- 角色回憶

## Level 3：正式動畫

長度：5～10 分鐘以上。

用途：

- 第一季主線
- 重大角色篇章
- 世界觀事件

只有 Level 1 / Level 2 流程穩定後，才進入 Level 3。

---

# 15. 同一素材多次利用

一個角色資產必須盡量做到：

```text
3D角色
 ├── 遊戲角色
 ├── NPC
 ├── Cutscene
 ├── 動畫
 ├── PV
 ├── YouTube
 ├── Shorts
 ├── 抖音
 └── 宣傳圖
```

一個 Boss 資產：

```text
Boss
 ├── 世界Boss
 ├── 副本Boss
 ├── 主線Cutscene
 ├── 動畫
 ├── Boss PV
 └── Shorts
```

這是降低長期成本的核心方法。

---

# 16. Claude Code 的責任

Claude Code 可以負責：

- Unity 專案設定
- 資產資料夾建立
- Animator Controller
- Animation Controller 整合
- Timeline
- Cinemachine
- Cutscene Scene
- VFX 掛點
- 音效掛點
- Prefab
- Addressables / 資產管理
- 動畫匯入設定
- Naming Convention
- Build
- 測試
- 技術問題排查

Claude Code 不應被要求：

- 自己決定角色演技
- 自己設計電影級鏡頭語言
- 自己生成完整動畫
- 自己決定角色個性動作
- 自己取代動畫導演

正確工作方式：

```text
ChatGPT / 使用者
↓
劇情＋Shot List＋演出要求
↓
Claude Code
↓
Unity 技術實作
↓
使用者／動畫工具
↓
動作與演出調整
```

---

# 17. 檔案結構

建議動畫相關資料：

```text
Assets/
├── Animation/
│   ├── Characters/
│   │   ├── JiangQiJing/
│   │   ├── XiaoYaoLin/
│   │   └── ChiTongYaoJiang/
│   ├── Combat/
│   ├── Cinematic/
│   │   ├── EP00/
│   │   ├── EP01/
│   │   └── EP02/
│   └── MotionSource/
│
├── Characters/
├── Weapons/
├── Environments/
├── VFX/
├── Audio/
└── Cinematics/
```

Blender 工作檔：

```text
Blender/
├── Characters/
├── Motion/
├── Retarget/
├── Cinematics/
└── Export/
```

影片後期：

```text
Video/
├── EP00/
├── EP01/
├── Shorts/
├── PV/
└── Export/
```

---

# 18. 命名規則

角色：

```text
CHR_JiangQiJing
CHR_XiaoYaoLin
CHR_ChiTongYaoJiang
```

動畫：

```text
ANM_JiangQiJing_Idle
ANM_JiangQiJing_Run
ANM_JiangQiJing_Attack01
ANM_JiangQiJing_Dodge
ANM_ChiTongYaoJiang_Intro
```

Cinematic：

```text
CUT_EP00_S001
CUT_EP00_S002
CUT_EP00_S003
```

這些命名規則必須保持穩定，以方便 Claude Code、Unity、Blender 與未來動畫團隊共同使用。

---

# 19. 商用授權檢查表

新增任何 AI 或素材服務前：

- [ ] 是否允許商業用途？
- [ ] AI 輸出是否允許商用？
- [ ] 是否要求標示來源？
- [ ] 是否限制模型／角色用途？
- [ ] 是否限制 YouTube / TikTok / 抖音？
- [ ] 是否限制遊戲用途？
- [ ] 是否限制再販售？
- [ ] 是否保留輸出檔永久使用權？
- [ ] 是否可以下載標準格式？
- [ ] 停止訂閱後能否繼續使用已產出的素材？
- [ ] 是否存在 credits / API 額外費用？

任何一項不清楚：

> **先不要把它放進正式 Pipeline。**

---

# 20. 付費工具決策流程

```text
免費工具能否完成？
       │
      是
       ↓
     不付費

      否
       ↓
是否只需要少量使用？
       │
      是
       ↓
單次／短期付費

      否
       ↓
是否會長期大量使用？
       │
      是
       ↓
評估月費／年費
       ↓
計算每分鐘／每個Shot成本
       ↓
確認商用授權
       ↓
才加入正式Pipeline
```

---

# 21. 第一階段不要買的東西

在 EP00 完成以前，不預設購買：

- 多套 AI Motion 月費
- 高級 AI Video 月費
- 高級 AI Voice 年費
- 高級 AI Music 年費
- 高級 Render Farm
- 高級 Mocap 硬體
- 大量素材庫訂閱

先證明 Pipeline 可行，再花錢。

---

# 22. 目前正式採用方案

### 核心

```text
Blender
Unity Personal
Rokoko Starter
DaVinci Resolve Free
```

### 備用

```text
DeepMotion
Plask
其他 AI Motion
```

### 後期再決定

```text
AI Voice
AI Music
AI SFX
AI Video
```

---

# 23. 第一個實際製作目標

不要直接做第一集動畫。

先完成：

# EP00《陣眼裂痕》

長度：60～90 秒。

內容：

```text
青嵐古林夜景
↓
陣眼異常
↓
江祈璟出現
↓
環境聲音異常
↓
赤瞳妖將現身
↓
短暫對視
↓
天空出現天墟裂痕
↓
陣眼爆發
↓
黑屏
```

EP00 的真正目的不是公開，而是：

> **驗證《九曜：天墟》是否已經擁有一條可以長期生產動畫的低成本 Pipeline。**

---

# 24. Phase 0-A 前置門檻

在正式進入 Unity Phase 0-A 前，動畫 Pipeline 至少完成：

1. 工具鏈確認。
2. Blender → Unity 匯出成功。
3. 至少一個 AI Motion → Blender → Unity 成功。
4. Timeline 成功播放角色動畫。
5. Cinemachine 成功完成至少三種鏡頭。
6. 一個簡單 VFX 成功加入 Cutscene。
7. 動畫資產命名規則確認。
8. 商用授權檢查制度建立。
9. EP00 Shot List 完成。
10. Claude Code 完成技術可行性審查。

不要求 EP00 完片後才開始遊戲開發。

正確策略是：

> **先完成 Pipeline 技術驗證，再與 Unity Phase 0-A 並行。**

---

# 25. 最終原則

《九曜：天墟》動畫製作不追求「AI 一鍵生成」。

真正要建立的是：

> **一套即使 AI 工具更換、漲價甚至消失，《九曜》仍然能繼續製作的生產系統。**

最重要的三個核心：

```text
Blender = 資產與動畫控制權
Unity   = 遊戲與互動式 Cinematic
AI      = 加速生產，而不是綁架生產
```

---

## 版本紀錄

### V1.0

- 建立《九曜：天墟》動畫正式生產 Pipeline。
- 建立免費優先與成本控制原則。
- 確立 Blender / Unity / Rokoko / DaVinci Resolve 為第一階段核心工具。
- 建立 AI Motion → Blender → Unity 工作流程。
- 建立 Character DNA、Shot List、命名與資料夾規範。
- 建立 EP00 60～90 秒動畫品質測試。
- 建立商用授權檢查制度。
- 明確定義 Claude Code 在動畫 Pipeline 中的責任範圍。
- 規定正式動畫製作前先驗證 Pipeline，不直接投入完整第一集。
