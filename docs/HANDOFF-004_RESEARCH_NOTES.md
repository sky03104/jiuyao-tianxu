# HANDOFF-004 技術可行性查證報告（暫存查證檔）

> 查證日期：2026-09-15（美國時區搜尋結果，實際查證時效以來源頁面為準）
> 查證方式：WebSearch + WebFetch，逐項列來源連結
> 用途：驗證 `23_ANIMATION_PRODUCTION_PIPELINE_V1.0.md` 與 `23A_AI_VOICE_TOOL_EVALUATION_V1.0.md` 草案內容是否屬實、是否過時
> 不修改 `17_TECH_ARCHITECTURE_V1.0.md` 正式內容，僅在風險段落引用其既定架構做交叉檢查

---

## A. Blender → Unity 資產流程

**結論：業界標準流程，成熟可靠。信心：高。**

- FBX 適合任何有骨架/動畫的角色；glTF(.glb) 只建議用在靜態道具，且需額外安裝 `com.unity.cloud.gltfast` 套件才能在 Unity 端良好支援。草案（23文件第2.2節）把 FBX/GLB/BVH 並列為「標準格式」沒有錯，但沒有講清楚「glTF 動畫角色進 Unity 目前仍不如 FBX 成熟」——**建議補充**：角色/動畫資產主線走 FBX，glTF 僅用於跨引擎靜態資產備份。
- Blender 端建議設定：Scene Unit 設 Metric、Unit Scale 1.0；匯出時 Scale 1.00、Forward -Z / Up Y（預設）、勾選 Apply Unit；Unity Model 匯入設定勾選 Bake Axis Conversion。
- 骨架命名需對應人體部位名稱，Humanoid Avatar 才能自動辨識映射；T-pose 建模。
- 有現成 Blender 4.x 外掛（如 Auto-Rig Pro、UnityRig）可直接產出 Unity Humanoid 相容的骨架與重定向動畫。
- 與草案一致：23文件沒有明確寫命名規範細節（軸向/T-pose/骨骼命名），**建議補進 23 文件第 17-18 節**作為實際操作規範，目前只有資料夾與檔名規則，缺技術參數。

來源：
- [Blender to Unity Export Checklist (Cinevva)](https://app.cinevva.com/guides/blender-to-unity-export-checklist)
- [UnityRig (GitHub)](https://github.com/nixkuroi/UnityRig)

---

## B. AI 動作捕捉工具（免費方案逐一查證）

### B1. Rokoko Studio（Starter/免費方案）

**結論：草案高估免費方案可用度。信心：高（官方定價頁直接查證）。**

實測官方定價頁（2026-09）：
- Video-to-Motion（Vision AI）：**每月僅 30 秒**
- Text-to-Motion：clips 生成不限，但「Studio 匯入」每月僅 5 次
- 匯出格式：免費方案**只有 FBX**，BVH／CSV 需付費方案（Basic $10/月起，年繳）
- 商用：頁面未特別限制商用（文字動作與影片轉動作生成內容可商用）
- 雲端儲存僅保留 1 個月

**與草案是否一致**：23文件寫「Rokoko Studio Starter：先使用免費方案測試」「EP00 不預設任何新增月費」——**草案過時/過度樂觀**。每月 30 秒的 Video-to-Motion 額度，對照 EP00 的 60~90 秒短片、9 個 Shot 的角色動作需求，很可能一個月內就用完，且需要反覆調整重錄（腳滑、穿模修正常態需要多次嘗試）。**建議修正**：EP00 測試期應直接預留「單月訂閱 Basic 方案（US$10~20，可隨時取消）」作為備案，不要假設純免費可以撐完整個 EP00 製作週期。

來源：
- [Rokoko Pricing 官方頁](https://www.rokoko.com/pricing)

### B2. Blender 內建動作編輯

**結論：草案定位正確，信心：中（未逐項查證所有動畫工具細節，但這是 Blender 核心功能，長年穩定）。**
Blender 的 NLA（非線性動畫編輯器）、Graph Editor、Pose Library 等內建工具足以做動作清理、混合、Retarget（配合 Rigify 等骨架系統）。這塊是 Blender 原生能力，沒有商用授權疑慮。

### B3. Mixamo

**結論：仍在營運、免費商用授權明確。信心：高。**

- 截至 2026-09，網站正常運作、Auto-Rigger 可用、Adobe 帳號免費。
- 官方 FAQ：Mixamo 角色與動畫「免費、無授權費/權利金，可無限商用或非商用」，不需標示來源。
- **限制**：不能把 Mixamo 原始動畫/角色檔案單獨包裝成資產包或引擎模板轉售；必須整合進最終專案中才能商用。
- 現況：動畫庫、綁骨流程、匯出工具多年沒有更新（功能凍結但仍堪用）。

**與草案是否一致**：HANDOFF-004 任務書提到「Mixamo 類動作資源」，23文件本身**沒有把 Mixamo 列入正式工具鏈**（只提 Rokoko/DeepMotion/Plask），這是**草案的疏漏**——Mixamo 目前免費商用條件比 Rokoko 免費方案更寬鬆（無時間額度限制，只是動畫庫本身較舊、風格未必貼合東方幻想武俠動作），**建議補入 Pipeline 作為「基礎動作庫」來源**（走路/跑步/待機等泛用動作可直接用 Mixamo，省下 Rokoko 額度給角色專屬招式動作）。

來源：
- [Mixamo License Guide (LicenseOrg)](https://www.licenseorg.com/guide/3d-assets/mixamo)
- [Adobe Community: Mixamo FAQ licensing](https://community.adobe.com/questions-696/mixamo-faq-licensing-royalties-ownership-eula-and-tos-589400)

### B4. DeepMotion

**結論：免費方案明確禁止商用，草案未點出這個關鍵限制。信心：高。**

官方定價頁（2026-09）：
- Freemium：每月 60 秒動畫額度（1 credit = 1 秒；手部/臉部追蹤各加 0.5 credit/秒）
- **免費方案明文僅供「個人、非商業用途」（personal, non-commercial use）**
- 商用授權需要付費方案（Studio 方案有 credit 優先權機制，7200 credits 內優先處理）

**與草案是否一致**：23文件把 DeepMotion 列為「AI 動作備用，有需要才付費」，語氣上沒有錯，但**沒有寫清楚「免費方案完全不能商用」**這個關鍵事實——如果《九曜》EP00 素材未來會被商業化使用（YouTube 營利、遊戲內使用），用 DeepMotion 免費方案產出的動作理論上不能直接用在正式商用內容中，必須升級付費方案。**這是本次查證中對草案最重要的修正之一，建議寫入 23 文件第 19 節商用授權檢查表旁註**。

來源：
- [DeepMotion Animate 3D Pricing 官方頁](https://www.deepmotion.com/pricing-animate3d)

### B5. Plask

**結論：免費方案可用但額度更緊，草案沒有具體數字。信心：中。**

- 免費方案：每日 15 秒動作捕捉額度（近期改為每日 900 credits 的 credit 制）
- 支援 FBX/GLB/BVH 匯出、多人動作捕捉、1GB 儲存
- 付費：Standard US$18/月（年繳）、Pro US$50/月

**與草案是否一致**：23文件只把 Plask 列在「備用」清單，沒有寫免費額度細節，屬於「查證後補充資訊，非錯誤」。Plask 免費方案雖然是「按日」而非「按月」給額度（15秒/天 = 理論上月累積量比 Rokoko 多），但單次動作長度短，適合短招式而非長鏡頭。

來源：
- [Plask Pricing 官方頁](https://plask.ai/en-US/pricing)

---

## C. Unity Cinematic 能力

**結論：Timeline + Cinemachine 免費、內建、成熟，足以支撐草案設想的用途。信心：高。**

- Cinemachine 3.x 是免費套件，任何專案皆可用；Unity 6000.0 起預設內建 Cinemachine 3。
- Timeline 是 Unity 內建套件，不需額外付費。
- Cinemachine 3 與 Timeline 整合良好，可透過 Timeline 的 Cinemachine Track 控制鏡頭切換、推拉、追蹤。
- 不需要額外付費套件即可完成草案要求的 Wide/Medium/Close-up/Tracking/Low Angle 等鏡頭語言與 Camera Cut。

**與草案是否一致**：一致。23文件（第6.1-6.2節）與 HANDOFF-004 任務書對 Timeline/Cinemachine 的定位符合官方現況，沒有發現過時或錯誤資訊。

來源：
- [Cinemachine 官方文件 3.1](https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/setup-timeline.html)
- [Cinemachine 3 新功能 (Unity Blog)](https://unity.com/blog/engine-platform/see-whats-new-with-cinemachine-3)

### 附帶查證：Unity Personal 免費資格

- Unity Personal 免費門檻：過去 12 個月營收或募資總額低於 US$200,000（滾動 12 個月計算，需持續監控）。
- 這與 `17_TECH_ARCHITECTURE_V1.0.md` 既定的 Unity 技術路線一致，本次審查未發現與正式架構文件衝突之處，僅供交叉確認。

來源：
- [Unity Editor Software Terms](https://unity.com/legal/editor-terms-of-service/software)

---

## D. AI 配音工具（5 款逐一查證，14 維度）

### D1. Chatterbox Multilingual V3（Resemble AI）

| 維度 | 查證結果 |
|---|---|
| 中文品質 | 有專屬 Mandarin Single Language Pack（500M 參數獨立模型），CER 0.41%，屬於 v3 系列中表現最好的語言包之一 |
| 多角色 | 支援 zero-shot voice cloning，可建立多組角色聲線 |
| 情緒控制 | 支援 exaggeration/paralinguistic 情緒控制 |
| Voice Cloning | 支援（zero-shot，需參考音檔） |
| 本地運行 | 支援，可本地推理 |
| API | 有社群/第三方 API 封裝，官方主要走開源自架 |
| 免費方案 | 完全開源免費，無使用次數限制（自架成本另計） |
| 商業授權 | **MIT 授權**，可商用、可修改、可再散布，無額外限制 |
| 商用限制 | 幾乎沒有——但輸出音檔預設內嵌 PerTh 浮水印（自架版本亦同） |
| 成本估算 | $0（僅自架運算成本：需 GPU） |
| 音檔格式 | 標準 WAV/PCM 輸出 |
| Unity 使用 | 可用，輸出標準音檔即可匯入 |
| 長期穩定性 | GitHub 2.5萬+ star，Resemble AI 持續維護，屬目前最活躍的開源 TTS 專案之一 |
| 平台鎖定 | 低，MIT 授權可自由遷移/微調/替換 |

**與 23A 草案是否一致**：**一致，且草案評估準確**。草案第4節寫「Chatterbox 官方 repository 的程式碼與模型目前以 MIT 等開源資訊呈現」是正確的，且建議保存版本紀錄的做法也正確。唯一補充：**輸出音檔預設帶浮水印**（PerTh watermark），草案沒有提到——這對動畫/遊戲配音通常無感知影響，但若做超高保真度混音後製，建議先實測浮水印是否可聞或影響音質。

來源：
- [Resemble AI Chatterbox Multilingual 官方頁](https://www.resemble.ai/learn/models/chatterbox-multilingual)
- [ResembleAI/chatterbox GitHub](https://github.com/resemble-ai/chatterbox)
- [Chatterbox Multilingual zh-cmn HuggingFace](https://huggingface.co/ResembleAI/Chatterbox-Multilingual-zh-cmn)

### D2. Fish Speech / Fish Audio S2

| 維度 | 查證結果 |
|---|---|
| 中文品質 | 業界公認中文能力強（阿里系團隊背景技術路線相近） |
| 多角色 | 支援 zero-shot/few-shot voice cloning |
| 情緒控制 | 支援語氣/情緒控制 |
| Voice Cloning | 強，few-shot 效果佳 |
| 本地運行 | 支援本地部署 |
| API | 官方 hosted API，s2-pro 模型 US$15 / 1M UTF-8 bytes |
| 免費方案 | GitHub 開源模型免費下載，但僅供研究/非商用 |
| 商業授權 | **Fish Audio Research License**——明確禁止商業使用，需另外向 Fish Audio 取得書面商業授權（本次查證確認此授權 2026-03-07 曾更新，條款持續有效） |
| 商用限制 | 「商業用途」定義明確包含 hosting 服務/API、內部商業營運、任何直接或間接產生營收的用途——範圍很廣，幾乎涵蓋《九曜》大部分使用情境 |
| 成本估算 | 若走官方 API：US$15/1M bytes，但**非拉丁文字（含中文）因 UTF-8 多位元組編碼，實際每字成本顯著高於英文**——這點必須納入試算，不能只看每百萬字節單價 |
| 音檔輸出格式 | 標準格式（WAV等），可用 |
| Unity 使用 | 可用 |
| 長期穩定性 | 專案活躍，但公司為新創（39 AI, Inc.），需觀察長期營運穩定度 |
| 平台鎖定 | 中——若走本地模型需另談商業授權，若走 API 則有平台依賴 |

**與 23A 草案是否一致**：**一致，草案這部分寫得最準確**。草案第5節「Fish Audio Research License…Commercial Purpose 需要另外取得書面商業授權」與本次查證結果完全吻合，HANDOFF-004 特別提醒的「GitHub 免費下載≠可商用」在這個案例上是**真實存在的風險，草案已經正確識別並警示**。**補充發現**（草案沒提到）：非拉丁文字的位元組成本問題——由於《九曜》配音以中文為主，若走 Fish Audio API，實際每字成本可能比英文專案高出 2~3 倍（中文 UTF-8 通常每字 3 bytes vs 英文每字約 1 byte），試算時務必用中文實際文本測試計費，不要套用官方英文範例的換算表。

來源：
- [Fish Speech LICENSE (GitHub)](https://github.com/fishaudio/fish-speech/blob/main/LICENSE)
- [Fish Audio Pricing 2026 (Smallest.ai 整理)](https://smallest.ai/blog/fish-audio-pricing-plans-api-billing-commercial-use-in-2026)
- [Fish Audio Developer Pricing 官方文件](https://docs.fish.audio/developer-guide/models-pricing/pricing-and-rate-limits)

### D3. ElevenLabs

| 維度 | 查證結果 |
|---|---|
| 中文品質 | 業界公認高品質多語言（含中文） |
| 多角色 | 支援 |
| 情緒控制 | 支援，表現力強 |
| Voice Cloning | 完整支援 |
| 本地運行 | 不支援，純雲端 |
| API | 官方 API 完整 |
| 免費方案 | Free：US$0，每月 10k credits，**不含商業授權** |
| 商業授權 | **Starter（US$6/月）起即含商業授權**，草案第7節描述正確 |
| 商用限制 | Free 方案不可商用；付費方案商用限制寬鬆 |
| 成本估算 | Starter $6/月（30k credits）、Creator 促銷後 $11/月常態價（首月 $22，草案寫的 $22 是促銷價非常態價，需注意）、Pro $99/月（600k credits）、Scale $299/月、Business $990/月 |
| 音檔輸出格式 | 標準格式 |
| Unity 使用 | 可用 |
| 長期穩定性 | 業界最成熟商用 TTS 廠商之一，穩定性高 |
| 平台鎖定 | 高——雲端服務，停止訂閱即無法產生新語音（但已產出音檔可保留使用） |

**與 23A 草案是否一致**：**大致一致，一處需小修正**。草案寫「Creator：US$22/月（頁面目前顯示首月優惠）」——本次查證確認 Creator **常態價為 $11/月，$22 是首月促銷價**，草案的寫法（標注為首月優惠）技術上沒有錯，但容易讓人誤讀成常態月費是 $22。**建議修正 23A 文件第7.1節，明確寫「常態 $11/月，首月促銷 $22」避免預算誤算**。

來源：
- [ElevenLabs Pricing 官方頁](https://elevenlabs.io/pricing)

### D4. CosyVoice

| 維度 | 查證結果 |
|---|---|
| 中文品質 | 強項，阿里巴巴 FunAudioLLM 團隊出品，中文導向設計 |
| 多角色 | 支援多角色/多說話人 |
| 情緒控制 | 支援，但實測情緒表現需自行測試（草案已提醒） |
| Voice Cloning | 支援 zero-shot |
| 本地運行 | 支援，本地模型+WebUI |
| API | 有第三方/雲端封裝，官方主打開源自架 |
| 免費方案 | 開源免費 |
| 商業授權 | 最新版本（CosyVoice2 / Fun-CosyVoice3）**程式碼與模型權重皆為 Apache 2.0**，可商用 |
| 商用限制 | 查證中發現**歷史上曾有「MIT 授權但文件同時提及學術用途」造成的社群困惑**（2025年issue），目前主線版本已明確為 Apache 2.0，但草案「不應只因 Apache 2.0 就忽略模型權重授權」的提醒**依然成立且必要**——正式商用前建議直接在 GitHub 開一個 issue 或查最新 LICENSE 檔案確認當下使用的具體 checkpoint 版本授權，避免用到早期版本的模糊授權 |
| 成本估算 | $0（自架運算成本另計） |
| 音檔輸出格式 | 標準格式 |
| Unity 使用 | 可用 |
| 長期穩定性 | 阿里巴巴團隊維護，GitHub 活躍度高 |
| 平台鎖定 | 低 |

**與 23A 草案是否一致**：**一致**。草案第8節「CosyVoice 程式碼採 Apache 2.0…不應只因 Apache 2.0 就忽略模型權重、資料與第三方依賴的具體授權」的謹慎態度是對的，本次查證也確實發現過歷史授權敘述不一致的案例，證明這個提醒有實際依據，不是過度謹慎。

來源：
- [CosyVoice LICENSE (GitHub)](https://github.com/QwenAudio/CosyVoice/blob/main/LICENSE)
- [GitHub Issue #1456: Is CosyVoice2 free for commercial use?](https://github.com/QwenAudio/CosyVoice/issues/1456)

### D5. Kokoro

| 維度 | 查證結果 |
|---|---|
| 中文品質 | 支援 Mandarin（8 種支援語言之一：美式/英式英文、西班牙文、法文、印地文、義大利文、日文、巴西葡萄牙文、中文），但中文語音包/聲音選項數量明顯少於英文（英文有 10 個 voicepack，其他語言含中文較少），**草案給中文 ★★★☆☆ 的評分方向正確** |
| 多角色 | 54 個聲音，但集中在英文，中文角色化聲音選擇有限 |
| 情緒控制 | 弱，設計目標是輕量高效而非情緒表現力，草案 ★★☆☆☆ 評分合理 |
| Voice Cloning | 弱/不是主打功能，草案 ★★☆☆☆ 評分合理 |
| 本地運行 | 支援，82M 參數屬輕量模型，資源需求低 |
| API | 有第三方 hosted API（如 Together AI），非官方唯一管道 |
| 免費方案 | 完全開源免費 |
| 商業授權 | **Apache 2.0**，可商用、可修改、可嵌入閉源產品，無需付費或申請 |
| 商用限制 | 幾乎無限制 |
| 成本估算 | $0（自架）；若用第三方 API 代管，市場行情約每百萬字元低於 US$1，或每小時語音低於 US$0.06 |
| 音檔輸出格式 | 標準格式 |
| Unity 使用 | 可用 |
| 長期穩定性 | 開源社群專案（hexgrad），2025年初發布，活躍度中等，非大廠背書但授權清楚穩定 |
| 平台鎖定 | 極低 |

**與 23A 草案是否一致**：**一致**。草案定位「低成本/輕量 TTS 備選，不作主要角色最終配音首選」符合查證結果——Kokoro 授權最乾淨（Apache 2.0，比 Chatterbox 的 MIT 更沒有浮水印問題），適合大量 NPC/系統提示音，但情緒與中文聲音選擇的限制確實存在，不適合江祈璟等主要角色。

來源：
- [Kokoro-82M HuggingFace](https://huggingface.co/hexgrad/Kokoro-82M)
- [Kokoro-82M Together AI](https://www.together.ai/models/kokoro-82m)

### D6. Voice Cloning 法律/倫理風險（HANDOFF-004 要求點出即可）

未經當事人同意複製真人聲音存在肖像權/人格權侵權風險，多數國家（含台灣民法人格權、部分美國州的聲音權利法案）已有相關判例或立法趨勢。23A 文件第14節「禁止事項」已明確列出「不得使用未經確認授權的真人聲音做 Voice Clone」「不得把網路影片真人聲音直接拿來訓練」，這個防護寫得足夠，**不需額外修改**，僅需在實際找配音員/聲優合作時，簽署書面同意書明確授權 AI 訓練與商用範圍。

---

## 總結

### 建議 Tier 分級

| 工具 | Tier | 理由 |
|---|---|---|
| Blender | Tier 1 | 免費、成熟、核心資產保險庫 |
| Unity Personal + Timeline + Cinemachine | Tier 1 | 免費（營收<$20萬美元期間）、官方內建 |
| Mixamo | Tier 1（**建議新增**） | 免費商用無時間額度限制，適合基礎泛用動作 |
| Rokoko Studio（免費方案） | Tier 2 | 額度極低（30秒/月），僅適合小量測試，**EP00 期間建議直接升級 Basic 付費方案** |
| DeepMotion（免費方案） | Tier 3（免費層） / Tier 2（付費層） | 免費方案禁止商用，只能拿來做純技術驗證，不能用在會公開營利的 EP00 素材 |
| Plask（免費方案） | Tier 2 | 額度少但可用於短招式測試 |
| Chatterbox Multilingual V3 | Tier 1 | MIT授權、中文品質佳、$0、本地可跑 |
| CosyVoice | Tier 1~2 | Apache 2.0、中文強，但情緒表現需要 EP00 實測驗證 |
| Kokoro | Tier 2 | Apache 2.0 最乾淨，但情緒/角色化不足，僅適合 NPC/系統音 |
| ElevenLabs | Tier 2（商用保底） | 付費才有商用授權，適合小量高品質正式片段 |
| Fish Speech / Fish Audio（本地模型） | Tier 3 | 研究授權不可商用，除非另簽商業授權 |
| Fish Audio API（官方 hosted） | Tier 2（需先試算中文實際成本） | 走商用服務可行，但中文計費比宣傳單價貴，需先試算 |

### 成本標示

- **免費**：Blender、Mixamo（商用無額度限制但動畫庫老舊）、Chatterbox、CosyVoice、Kokoro（自架情況下）
- **免費但有限制**：Unity Personal（營收門檻）、Rokoko Starter（30秒/月+僅FBX）、Plask（15秒/日）、DeepMotion Freemium（60秒/月但禁止商用）、ElevenLabs Free（不可商用）
- **需要付費**：Rokoko Basic起（US$10/月起）、DeepMotion付費方案（商用必須）、ElevenLabs Starter起（US$6/月起）
- **商業授權需另談**：Fish Speech/Fish Audio 本地模型商用

### 技術風險評估

| 風險項目 | 等級 | 說明 |
|---|---|---|
| 角色一致性 | 中 | Character DNA 制度設計合理，但尚未實測 AI Motion 產出是否會破壞模型比例/穿模，需 EP00 驗證 |
| 動作品質 | 中 | 免費方案額度普遍偏低（Rokoko 30秒/月最緊），需多次調整才能達到可用品質，可能拖慢 EP00 進度 |
| 手機效能 | 低~中 | 本次審查未涉及具體 Poly Count/材質預算數字，`17_TECH_ARCHITECTURE_V1.0.md` 若已有手機效能預算，動畫角色資產需另外對照該預算做 LOD/材質簡化，屬於後續銜接工作，非本次阻塞項 |
| Unity 相容性 | 低 | FBX→Humanoid 流程成熟，Timeline/Cinemachine 免費內建，無相容性風險 |
| 授權 | 中 | Fish Speech/DeepMotion 免費層皆有明確商用限制，若團隊不熟悉授權細節容易誤用；已建立的商用授權檢查表（23文件第19節）方向正確，需嚴格執行 |
| 成本 | 低~中 | 免費工具組合可以完成 EP00 的「技術驗證」目的，但若要達到「可公開發布/商用等級」的最終品質，Rokoko/DeepMotion 免費額度大機率不夠用，需預留小額測試預算（建議 EP00 抓 US$30~50 的彈性預算） |
| 平台鎖定 | 低 | 草案的「標準格式優先、可離開平台」原則落實良好，各工具皆可輸出 FBX/WAV 等標準格式 |
| 長期維護 | 低 | Chatterbox（2.5萬+ star）、CosyVoice（阿里團隊）、Blender、Unity 皆為長期活躍專案；Fish Audio為新創公司需持續觀察 |

### Phase 0-A 是否可以開始

**可以開始，但有 3 個非阻塞性但必須先處理的前置動作，不構成「阻塞（blocked）」等級：**

1. **修正 23 文件對 Rokoko/DeepMotion 免費額度的過度樂觀假設**——EP00 製作前應明確告知咖哩：純免費組合大機率不夠用，需準備約 US$30~50 的小額測試預算（Rokoko Basic 一個月或 DeepMotion 付費層），否則 EP00 進度會卡在動作捕捉額度不足。
2. **DeepMotion 免費方案禁止商用**——若 EP00 素材最終會公開在 YouTube/短影音等營利管道，用 DeepMotion 免費層產出的動作嚴格來說不能直接商用，需要在正式發布前重新用付費方案產出，或一開始就選擇 Rokoko/Mixamo 作為主力（兩者商用條件較寬鬆）。
3. **ElevenLabs Creator 定價需修正常態價**（$11/月非$22/月），避免後續成本試算錯誤。

以上三點都是「使用細節與預算認知」層級的修正，不影響 Pipeline 本身的技術可行性——**Blender→Unity 資產流程、Timeline/Cinemachine 演出能力、Chatterbox/CosyVoice 的中文配音可行性，查證結果全部支持草案的技術方向是對的**。因此：

> **技術上可以正式開始 Phase 0-A，並與動畫 Pipeline EP00 並行推進（符合 23 文件第24節「先完成 Pipeline 技術驗證，再與 Unity Phase 0-A 並行」的既定策略）**，但建議先把上述 3 點回饋給 ChatGPT/咖哩做預算與流程微調，避免 EP00 執行到一半才發現免費額度不夠而中斷。

### 查不到 / 無法確認的項目

- 手機端實際效能預算（Poly Count、Draw Call、材質數量上限）——這部分需要對照 `17_TECH_ARCHITECTURE_V1.0.md` 是否已有明確數字，本次查證範圍未涵蓋該文件細節比對，若該文件已有預算數字，建議另外指派任務比對動畫角色資產是否符合。
- Fish Audio Research License 是否對「非營利遊戲原型測試」有豁免——官方條款文字上定義商業用途範圍很廣，但沒有查到明確的「個人非商業原型測試」豁免細則，正式使用前建議直接發信詢問 Fish Audio 官方，不要用推測代替確認。
