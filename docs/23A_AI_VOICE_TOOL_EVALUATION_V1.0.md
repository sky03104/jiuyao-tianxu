# 《九曜：天墟》AI 配音工具評估 V1.0

> 文件狀態：正式評估文件
>
> 建立日期：2026-09-15
>
> 目的：在《九曜：天墟》正式投入動畫與遊戲配音前，先比較 AI TTS／Voice Cloning 工具的音質、中文能力、情緒表現、本地部署、成本與商用授權，避免製作中途因授權或費用問題更換整套聲音系統。

---

## 1. 最重要的結論

目前不把任何單一 AI 配音工具永久鎖死。

第一階段採：

**「開源／本地優先 + 商用授權清楚 + 標準 WAV 輸出 + Voice DNA 不綁工具」**。

目前候選順位：

1. **Chatterbox Multilingual V3：第一優先測試**
2. **Fish Speech / Fish Audio S2：第二優先測試**
3. **CosyVoice：中文能力與本地部署備選**
4. **ElevenLabs：高品質商用雲端備選**
5. **Kokoro：低成本／輕量 TTS 備選，但不作主要角色最終配音首選**

注意：這是「工程與商用策略順位」，不是單純音質排名。

---

# 2. 為什麼不能只看「聽起來最好」

《九曜：天墟》會有：

- 遊戲角色語音
- 主線劇情
- NPC 對話
- 動畫
- YouTube
- Shorts
- 抖音
- PV
- 宣傳片

因此真正需要的是：

```text
音質
+
角色一致性
+
中文品質
+
情緒控制
+
可大量生成
+
商用授權
+
成本可預測
+
長期可維護
```

任何一項完全不合格，都不適合直接成為正式核心方案。

---

# 3. 候選工具總表

| 工具 | 類型 | 中文 | Voice Clone | 情緒 | 本地部署 | 商用策略 | 初步定位 |
|---|---|---|---|---|---|---|---|
| Chatterbox V3 | 開源 TTS | ★★★★☆ | ★★★★☆ | ★★★★★ | ✅ | MIT 模型/程式授權，仍需逐項確認模型與素材權利 | **首測** |
| Fish Speech S2 | 開源 TTS | ★★★★★ | ★★★★★ | ★★★★★ | ✅ | **官方研究授權不含商用，商用需另簽** | **首測／候補** |
| CosyVoice | 開源 TTS | ★★★★★ | ★★★★☆ | ★★★★☆ | ✅ | Apache 2.0 程式碼，但需逐項確認模型/資料授權 | 中文備選 |
| ElevenLabs | 雲端 | ★★★★★ | ★★★★★ | ★★★★★ | ❌ | Starter 起即提供 Commercial License | 商用雲端備選 |
| Kokoro | 開源 TTS | ★★★☆☆ | ★★☆☆☆ | ★★☆☆☆ | ✅ | 模型授權需依實際版本確認 | 輕量備選 |

> 評星是《九曜》製作需求下的工程評估，不是官方基準測試。

---

# 4. Chatterbox Multilingual V3

## 4.1 為什麼目前排第一

官方 Resemble AI 的 Chatterbox 最新公開模型為 **Chatterbox Multilingual V3**，500M 級模型，支援 23+ 語言，包含中文；官方也提供中文專用 Single Language Pack。

官方範例直接展示中文文字生成與 reference audio voice cloning。

Chatterbox 同時提供：

- 多語言 TTS
- Zero-shot voice cloning
- 情緒／exaggeration 控制
- paralinguistic 能力
- 本地推理

這些特別符合動畫角色需求。

## 4.2 《九曜》的用途

適合：

- 江祈璟
- 蕭曜霖
- 厲若楓
- 齊衡烈
- 聞人澈
- 郁岑燁
- 赤瞳妖將
- NPC

尤其可以測試：

```text
平靜
憤怒
戰鬥
受傷
喘息
低語
大喊
悲傷
驚訝
```

## 4.3 商用注意

官方 repository 的程式碼與模型目前以 MIT 等開源資訊呈現，但正式商用時仍必須保存使用的具體版本、LICENSE、模型卡與第三方依賴紀錄。

因此《九曜》正式發行前不能只寫「Chatterbox 是 MIT，所以全部都可以商用」，而要做版本級授權檔案保存。

**結論：第一優先做 EP00 配音測試。**

---

# 5. Fish Speech / Fish Audio S2

## 5.1 優點

Fish Audio S2 是目前非常值得測試的角色語音方案：

- 中文能力強
- Zero-shot / Few-shot voice cloning
- 情緒與語氣控制
- 多語言
- 本地部署能力
- 可以輸出標準音訊供 Blender / Unity / DaVinci 使用

## 5.2 最大問題：授權

目前官方 Fish Speech repository 使用 **FISH AUDIO RESEARCH LICENSE**。

該授權明確允許 Research / Non-Commercial 使用；**Commercial Purpose 需要另外取得 Fish Audio 的書面商業授權**。

因此《九曜：天墟》不能把 GitHub 版本直接視為免費商用配音引擎。

## 5.3 正確用法

```text
Phase 0 / 個人測試
        ↓
Fish Speech S2
        ↓
測試音質與角色一致性
        ↓
若效果非常好
        ↓
詢問 Fish Audio 商業授權價格
        ↓
與 Chatterbox / ElevenLabs 成本比較
        ↓
再決定正式採用
```

**結論：非常值得測試，但目前不列為免費商用核心。**

---

# 6. Fish Audio API

Fish Audio 官方開發者頁目前提供 hosted API，採按量付費；頁面目前列出的主要 TTS API 價格為 **US$15 / 1M UTF-8 bytes**，並表示可使用與開放權重版本相同系列的模型。

這代表另一種可能：

```text
Fish Speech 本地模型
→ 授權問題

Fish Audio 官方 API
→ 直接走商用服務
→ 按量付費
```

如果未來動畫大量配音，必須實際計算：

- 每集字數
- 每季字數
- 重錄比例
- 遊戲 NPC 總字數
- API 成本

不能只看每百萬字單價。

**目前定位：值得做成本試算，但不先訂閱。**

---

# 7. ElevenLabs

## 7.1 優點

ElevenLabs 的最大優勢是：

- 成熟
- 音質高
- Voice Cloning 完整
- 情緒與表現力強
- 雲端使用簡單
- 商用方案規則相對清楚

目前官方定價頁列出：

- Free：US$0
- Starter：US$6/月，包含 Commercial License
- Creator：US$22/月（頁面目前顯示首月優惠）
- Pro：US$99/月

因此如果我們要快速做一個正式 PV 或動畫測試片，ElevenLabs 是非常實用的「付費保底方案」。

## 7.2 缺點

- 雲端依賴
- 依照 credits 使用
- 長期大量 NPC 配音可能產生持續成本
- 不像本地模型一樣能完全脫離服務商

**結論：不作免費核心，但保留為高品質商用備援。**

---

# 8. CosyVoice

CosyVoice 是阿里巴巴 QwenAudio 團隊的開源語音專案。

其程式碼採 Apache 2.0 授權資訊，並提供本地模型、WebUI 與多代模型支援。

優點：

- 中文導向能力強
- 本地部署
- 開源
- 適合中文角色配音研究

缺點：

- 實際動畫情緒表現需要自己測試
- Voice DNA 工作流需要自行建立
- 不應只因 Apache 2.0 就忽略模型權重、資料與第三方依賴的具體授權

**結論：列入中文本地 TTS 備選。**

---

# 9. Kokoro

Kokoro 是輕量級開源 TTS 路線。

優點：

- 模型較輕
- 本地運行
- 成本低
- 適合大量普通 NPC、系統提示、測試語音

但《九曜》的主要角色需要：

- 情緒
- 演技
- 角色辨識
- Voice Clone

所以不把 Kokoro 作為主要角色最終配音首選。

**結論：適合低成本大量 NPC 或原型。**

---

# 10. 《九曜》正式推薦架構

不要只選一套。

採用「一主兩備」：

```text
                    《九曜》Voice Pipeline
                              │
             ┌────────────────┼────────────────┐
             ↓                ↓                ↓
       Chatterbox V3     Fish Speech S2    ElevenLabs
          主力候選          技術候選          商用保底
             │                │                │
             └────────────────┼────────────────┘
                              ↓
                       標準 WAV / PCM
                              ↓
                     DaVinci / Unity / Blender
```

CosyVoice / Kokoro 作為本地備援與特殊用途。

---

# 11. Voice DNA 制度

不能把角色聲音設定綁死在某一個模型。

每個主要角色建立：

```text
Voice ID
性別感
年齡感
音域
音色
語速
咬字
情緒基線
憤怒模式
悲傷模式
戰鬥模式
喘息模式
低語模式
喊叫模式
參考音檔
參考台詞
```

例如：

### 江祈璟

```text
年齡感：20～22
聲線：中高音
個性：冷靜、直接
語速：中等偏快
平時：克制
戰鬥：爆發
憤怒：壓低聲線後突然提高
悲傷：降低語速
```

這份 Voice DNA 才是永久資產。

AI 模型可以換，但 Voice DNA 不換。

---

# 12. EP00 配音測試

動畫 Pipeline 的 60～90 秒 EP00 必須同時測試三套：

### A：Chatterbox V3
### B：Fish Speech S2
### C：ElevenLabs

同一份台詞、同一個角色設定、同一段情緒要求。

禁止不同台詞比較。

## 測試台詞類型

```text
1. 平靜說話
2. 緊張
3. 憤怒
4. 戰鬥喊叫
5. 受傷喘息
6. 低聲警告
7. 驚訝
8. 悲傷
```

然後由人工評分：

| 評分項目 | 權重 |
|---|---:|
| 中文自然度 | 20% |
| 聲音辨識度 | 15% |
| 情緒表現 | 20% |
| 角色一致性 | 20% |
| 長台詞穩定性 | 10% |
| 可控性 | 5% |
| 商用成本 | 5% |
| 授權清晰度 | 5% |

總分 100。

---

# 13. 最終決策規則

### 如果 Chatterbox ≥ Fish Speech 且商用授權最簡單

→ Chatterbox 主力。

### 如果 Fish Speech 明顯更好

→ 詢問 Fish Audio 商業授權。

### 如果 Fish Audio 商業授權過高

→ 回到 Chatterbox / ElevenLabs。

### 如果 ElevenLabs 明顯勝出但成本可接受

→ 主要角色使用 ElevenLabs，普通 NPC 使用本地模型。

### 如果本地模型已經足夠

→ 優先本地化，降低長期成本。

---

# 14. 禁止事項

正式商用前不得：

- 使用未確認授權的真人聲音做 Voice Clone。
- 把網路影片中的真人聲音直接拿來訓練角色。
- 只看到「GitHub 開源」就認定模型可以商用。
- 只看程式碼 LICENSE，不檢查模型權重 LICENSE。
- 不記錄使用的模型版本。
- 不保存當時的 LICENSE / Terms。
- 把主要角色聲音綁死在單一雲端平台。

---

# 15. 成本策略

## Phase 0

目標：$0 新增月費。

主要做：

- Chatterbox 測試
- Fish Speech 測試
- CosyVoice 測試
- 少量 ElevenLabs 試用

## EP00

如果本地模型品質足夠：

→ 優先使用本地模型。

如果需要商用品質：

→ 少量購買 ElevenLabs / Fish Audio API 做比較。

## 正式動畫

依實際每集字數與重錄量計算成本後才決定訂閱。

---

# 16. 最終暫定結論

截至 2026-09-15：

**第一優先：Chatterbox Multilingual V3**

原因：開源、本地、中文、Voice Cloning、情緒控制與目前《九曜》需求高度吻合。

**第二優先：Fish Speech S2**

原因：中文與角色表現非常值得測試，但官方研究授權不等於免費商用，正式商用需另外取得授權。

**第三優先：ElevenLabs**

原因：成熟且商用方案清楚，作為高品質雲端保底最安全，但長期成本與平台依賴較高。

**第四優先：CosyVoice**

原因：中文與本地部署有價值，作為本地備援。

**第五優先：Kokoro**

原因：低成本與輕量，但主要角色的情緒與角色化需求不是第一優勢。

---

# 17. 下一步

在正式動畫製作前，不直接決定唯一 TTS。

先完成：

```text
Chatterbox V3
      ↓
Fish Speech S2
      ↓
ElevenLabs
      ↓
同一角色／同一台詞／同一情緒
      ↓
EP00 Voice Test
      ↓
人工評分
      ↓
成本計算
      ↓
授權確認
      ↓
正式鎖定 Voice Pipeline
```

**最終原則：聲音品質可以比較，工具可以替換，但《九曜》的 Voice DNA 不得被任何單一 AI 平台綁架。**
