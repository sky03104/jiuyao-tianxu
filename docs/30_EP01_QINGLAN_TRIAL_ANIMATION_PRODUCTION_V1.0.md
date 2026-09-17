# 《九曜：天墟》EP01〈第七室報到〉AI 動畫實際製作規格 V1.0

> 狀態：**可直接進入 AI 圖像／AI 影片製作**
> 對應動畫總規格：`29_CHAPTER_01_ANIMATION_SHORT_VIDEO_V1.0.md`
> 對應遊戲劇情：`25_CHAPTER_01_QINGLAN_TRIAL_FULL_SCRIPT_V1.0.md`
> 對應角色設定：`02_CHARACTER_BIBLE_V1.0.md`
> 用途：將 EP01 從「分鏡構想」提升為可逐鏡生成、剪輯、配音的實際製作文件。
>
> **本文件只製作 EP01，不改寫第一章主線。**

---

# 一、EP01 定位

## 集數

**EP01｜第七室報到**

## 建議成片長度

**約 65～75 秒**

## 畫面比例

**9:16 直式**

## 影片基調

第一集不急著展示大世界觀，也不急著製造危機。

觀眾首先認識：

- 天玄院
- 玩家
- 第七室
- 齊衡烈
- 郁岑燁
- 厲若楓
- 蕭曜霖

最後用「半刻鐘後，新生演武場集合」把下一集的戰鬥教學鉤出來。

## 情緒曲線

**陌生 → 輕鬆 → 有趣 → 被觀察 → 壓力 → 期待**

---

# 二、EP01 最終成片結構

| Shot | 內容 | 秒數 | 功能 |
|---|---|---:|---|
| 01 | 天玄院外景 | 5s | 建立世界 |
| 02 | 玩家走入東廊 | 5s | 建立主角視角 |
| 03 | 第七室門牌 | 4s | 建立據點 |
| 04 | 齊衡烈探頭 | 6s | 第一個角色登場 |
| 05 | 玩家與齊衡烈 | 6s | 建立互動 |
| 06 | 郁岑燁加入對話 | 7s | 建立性格衝突 |
| 07 | 厲若楓觀察 | 6s | 建立觀察者 |
| 08 | 三人同框 | 6s | 建立第七室群像 |
| 09 | 蕭曜霖聲音出現 | 5s | 製造轉折 |
| 10 | 蕭曜霖登場 | 7s | 建立教官壓迫感 |
| 11 | 三人反應 | 5s | 喜劇式節奏收束 |
| 12 | 玩家走向演武場 | 6s | 下一集鉤子 |
| 13 | 片尾鉤子 | 4s | 留存 |

**總長：約 72 秒**

---

# 三、全 EP01 固定視覺基準

以下 Prompt 為所有鏡頭共同追加內容。

## Global Style Prompt

> premium 3D Eastern fantasy MMORPG cinematic, original Chinese xuanhuan fantasy world, ancient cultivation academy, realistic stylized 3D characters, high-end mobile MMORPG cinematic quality, physically based rendering, detailed fabric and armor materials, natural human proportions, cinematic depth of field, volumetric morning mist, subtle spiritual energy particles, elegant Chinese architecture, grounded fantasy, mature visual tone, restrained color palette, realistic lighting, cinematic composition, vertical 9:16

## Global Negative Prompt

> low quality, blurry face, deformed face, deformed hands, extra fingers, extra limbs, duplicate character, duplicate weapon, floating weapon, inconsistent costume, inconsistent hairstyle, different character identity, modern clothing, modern building, western medieval castle, sci-fi armor, guns, neon city, chibi, cartoon, exaggerated anime eyes, childish proportions, giant head, watermark, logo, text, subtitles, UI, game HUD

## AI 影片固定追加

> preserve exact character identity, preserve costume and hairstyle, natural body weight, realistic cloth movement, subtle facial animation, controlled cinematic camera movement, realistic eye movement, no sudden character transformation, no teleportation, no duplicated limbs, no camera shake unless explicitly requested, vertical 9:16

---

# 四、角色鎖定設定

## 4.1 玩家

### 角色用途

第一人稱視角核心，但不是天命主角。

### 固定 Prompt

> young adult human cultivator, ordinary newcomer to a cultivation academy, customizable neutral appearance, practical dark teal and charcoal cultivation clothing, simple academy travel gear, youthful but mature face, average athletic build, calm observant eyes, no noble insignia, no royal symbols, no legendary bloodline markings, no glowing eyes, no excessive ornaments, grounded eastern fantasy character design

### 固定外觀規則

- 不使用金色主角光環。
- 不使用特殊血脈紋路。
- 不使用誇張武器。
- 不比其他角色更華麗。
- 臉部保持普通、耐看、容易讓玩家代入。
- 所有鏡頭保持同一髮型、服裝與臉型。

---

## 4.2 齊衡烈

> Qi Heng-Lie, young male saber cultivator, strong broad athletic build, energetic expression, short dark reddish-brown hair, red-gold arm-ring markings, heavy saber, warm confident smile, practical martial cultivation clothing, powerful stance, friendly but competitive personality

### 表演規則

- 動作幅度最大。
- 表情最容易笑。
- 說話直接。
- 身體微微前傾。
- 喜歡主動靠近新人。
- 不演成莽夫或搞笑角色。

---

## 4.3 郁岑燁

> Yu Cen-Ye, young male sword cultivator, short black-blue hair, luminous meridian mark on forehead, right hand wrapped with cloth, lean athletic build, sharp calm eyes, restrained expression, dark blue-black cultivation uniform, elegant straight sword at waist, precise posture, disciplined perfectionist personality

### 表演規則

- 背挺直。
- 動作少。
- 說話簡短。
- 眉頭輕微皺起。
- 不主動靠近。
- 不演成冷酷反派。

---

## 4.4 厲若楓

> Li Ruo-Feng, young male archer, amber eyes, quiet observant expression, short dark hair, three-section short bow carried on back, subtle old scar on left shoulder, slim athletic body, practical dark earth-tone cultivation clothing, calm posture, highly attentive gaze

### 表演規則

- 大部分時間先觀察。
- 不搶話。
- 眼神跟隨玩家腳步。
- 說話時保持平靜。
- 情緒主要透過眼神表現。

---

## 4.5 蕭曜霖

> Xiao Yao-Lin, mature male heavy-blade instructor, powerful broad build, heavy shoulder guard, gold meridian mark on forehead, dark instructor robe and armor, imposing but controlled presence, stern experienced eyes, large heavy blade carried securely, veteran warrior, calm authority

### 表演規則

- 動作極少。
- 不需要大聲吼叫。
- 只要站著就有壓迫感。
- 說話速度比年輕角色慢。
- 眼神穩定。

---

# 五、場景鎖定設定

## 5.1 天玄院外景 Reference

> enormous ancient Chinese cultivation academy built into a mountain, layered dark jade and warm stone architecture, elegant curved roofs, long elevated corridors, training courtyards, distant mountain peaks, morning clouds flowing through the academy, subtle spiritual energy in the air, young cultivators walking in the distance, premium 3D eastern fantasy MMORPG environment, cinematic sunrise, realistic architecture, vertical 9:16

## 5.2 東廊 Reference

> long elegant eastern fantasy academy corridor, dark wooden pillars, stone floor, warm wooden doors, hanging academy lanterns, mountain view visible through open side, young cultivators passing naturally in background, morning light and soft mist, realistic 3D environment, premium Chinese xuanhuan MMORPG cinematic

## 5.3 第七室門口 Reference

> quiet dormitory corridor inside an ancient Chinese cultivation academy, simple dark wooden door, small carved wooden plaque reading 第七室, warm morning light, subtle spiritual lantern, clean stone floor, realistic eastern fantasy architecture, cinematic depth of field, premium 3D MMORPG environment

> **注意：若 AI 文字生成不穩定，門牌「第七室」不要交給生圖模型生成。**
> 先生成無字門牌，再於剪輯階段後製文字。

---

# 六、Shot 01｜天玄院外景

## 時間

**0:00–0:05｜5 秒**

## 畫面

清晨。

天玄院依山而建。

雲霧從山腰緩慢流過，長廊與屋簷被晨光照亮。

遠處有少量新生與弟子行走。

整個畫面要讓觀眾感覺：

**這是一座已經存在很多年的修行學院。**

不是仙宮，不是皇宮，也不是西方魔法學院。

## 生圖 Prompt

> vast ancient Chinese cultivation academy built along a mountain, layered elegant dark wood and stone architecture, long elevated corridors, curved traditional roofs, distant mountain peaks, morning mist flowing through the academy, a few young cultivators walking naturally in the distance, warm early morning sunlight, subtle spiritual atmosphere, realistic stylized 3D, premium eastern fantasy MMORPG cinematic environment, physically based rendering, cinematic depth of field, mature grounded fantasy, vertical 9:16

## 影片 Prompt

> slow cinematic aerial push toward the ancient mountain academy, morning mist drifting naturally between buildings, distant banners moving gently in the wind, subtle sunlight rays, a few tiny cultivators walking in the distance, calm majestic atmosphere, realistic environmental motion, no dramatic action

## 鏡頭

- 高位遠景
- 緩慢向前推
- 不快速飛行
- 不旋轉

## 聲音

- 遠處風聲
- 山鳥
- 極輕鐘聲
- BGM 第一個長音

## 旁白

**「九曜界，天玄院。」**

---

# 七、Shot 02｜玩家走入東廊

## 時間

**0:05–0:10｜5 秒**

## 畫面

鏡頭切到玩家背後。

玩家手持入院憑證，沿著東廊前進。

背景中其他新生擦肩而過。

玩家不需要做英雄式動作，只是普通地走進一個陌生環境。

## 生圖 Prompt

> rear three-quarter view of a young adult ordinary newcomer cultivator walking through the eastern corridor of an ancient Chinese cultivation academy, practical dark teal and charcoal cultivation clothing, simple academy travel gear, holding an admission token in one hand, natural posture, other young cultivators passing in the background, warm morning sunlight, realistic stylized 3D eastern fantasy MMORPG cinematic, vertical 9:16

## 影片 Prompt

> camera follows slowly behind the young newcomer as he walks through the academy corridor, subtle cloth movement, natural footsteps, other students casually passing by, morning light moving across the stone floor, restrained cinematic camera movement

## 鏡頭

低於肩膀高度。

略微跟拍。

## 聲音

- 腳步
- 衣料摩擦
- 遠處演武聲
- BGM 開始加入輕木質打擊。

---

# 八、Shot 03｜第七室門牌

## 時間

**0:10–0:14｜4 秒**

## 畫面

玩家停在門前。

鏡頭從玩家肩後慢慢推向門牌。

門牌是本集第一個視覺記憶點。

## 生圖 Prompt

> over-the-shoulder view of a young cultivator standing before a simple dark wooden dormitory door inside an ancient Chinese cultivation academy, small carved wooden plaque area above the door, warm morning light, shallow depth of field, quiet atmosphere, realistic premium 3D eastern fantasy MMORPG cinematic

## 影片 Prompt

> slow over-the-shoulder camera push toward the dormitory door, the newcomer stops naturally, slight movement of his clothing in the breeze, focus gradually shifts from the character to the wooden plaque

## 後製

門牌文字：

**第七室**

## 聲音

- 腳步停止
- 木門輕微風響

---

# 九、Shot 04｜齊衡烈探頭

## 時間

**0:14–0:20｜6 秒**

## 畫面

門突然從裡面打開。

齊衡烈探出頭。

他不是衝出來，而是很自然地發現門口有人。

## 生圖 Prompt

> Qi Heng-Lie opening a dark wooden dormitory door and leaning slightly out of the doorway with a warm confident smile, strong broad athletic build, short dark reddish-brown hair, red-gold arm-ring markings, practical martial cultivation clothing, heavy saber visible at his side, young adult Chinese eastern fantasy character, realistic premium 3D MMORPG cinematic, warm morning light, vertical 9:16

## 影片 Prompt

> wooden door opens naturally, Qi Heng-Lie leans out with a friendly curious expression, notices the newcomer, slight smile, subtle eyebrow movement, natural body weight, realistic cloth physics, camera gently moves closer

## 台詞

**齊衡烈：**

「你就是新來的？」

## 配音

爽朗、直接。

不能演成搞笑腔。

## SFX

木門開啟聲。

---

# 十、Shot 05｜玩家與齊衡烈

## 時間

**0:20–0:26｜6 秒**

## 畫面

玩家與齊衡烈面對面。

玩家略顯不確定。

齊衡烈則像看到新隊友一樣自然。

## 生圖 Prompt

> medium two-shot of an ordinary young newcomer cultivator facing Qi Heng-Lie outside a dormitory room, newcomer slightly uncertain but calm, Qi Heng-Lie smiling openly and leaning forward with friendly confidence, ancient Chinese academy corridor, warm morning sunlight, realistic stylized 3D eastern fantasy MMORPG cinematic, natural character proportions, vertical 9:16

## 影片 Prompt

> natural conversational two-shot, newcomer gives a small uncertain response, Qi Heng-Lie smiles and casually gestures toward the dormitory, subtle facial animation, realistic eye contact, restrained camera movement

## 對話

**玩家：**

「應該是。」

**齊衡烈：**

「那就對了。」

稍停。

「第七室最近正缺個能打的。」

## 表演

齊衡烈說最後一句時輕輕拍一下自己的手臂。

不是挑釁，是興奮。

---

# 十一、Shot 06｜郁岑燁插話

## 時間

**0:26–0:33｜7 秒**

## 畫面

鏡頭切入室內。

郁岑燁原本坐在一側整理劍。

聽到齊衡烈的話後抬眼。

## 生圖 Prompt

> Yu Cen-Ye sitting calmly inside a simple cultivation dormitory, short black-blue hair, luminous meridian mark on forehead, right hand wrapped with cloth, lean athletic build, sharp calm eyes, restrained slightly critical expression, dark blue-black cultivation uniform, elegant straight sword beside him, ancient Chinese academy dormitory interior, realistic premium 3D eastern fantasy MMORPG cinematic, vertical 9:16

## 影片 Prompt

> Yu Cen-Ye quietly stops what he is doing, raises his eyes toward Qi Heng-Lie and the newcomer, slight frown, minimal movement, calm precise expression, subtle head turn, cinematic shallow depth of field

## 台詞

**郁岑燁：**

「你看都沒看他出手，怎麼知道他能打？」

## 表演

語氣平淡。

不是故意嗆人。

---

# 十二、Shot 07｜厲若楓觀察玩家

## 時間

**0:33–0:39｜6 秒**

## 畫面

厲若楓站在較後方。

他沒有加入爭論。

他的目光落在玩家腳步。

鏡頭短暫切到玩家鞋底與站姿，再切回厲若楓的眼睛。

## 生圖 Prompt

> Li Ruo-Feng quietly observing a newcomer from inside an ancient academy dormitory, amber eyes focused and analytical, short dark hair, slim athletic body, practical dark earth-tone cultivation clothing, three-section short bow visible on his back, calm restrained expression, realistic premium 3D eastern fantasy MMORPG character, cinematic eye lighting, vertical 9:16

## 影片 Prompt

> Li Ruo-Feng silently studies the newcomer's stance and footsteps, camera briefly cuts to the newcomer's feet, then returns to Li Ruo-Feng's amber eyes, subtle eye movement, calm analytical expression, no exaggerated motion

## 台詞

**厲若楓：**

「別急。」

停頓。

「他的腳步有點亂。」

---

# 十三、Shot 08｜三人第一次同框

## 時間

**0:39–0:45｜6 秒**

## 畫面

玩家、齊衡烈、郁岑燁、厲若楓第一次完整同框。

齊衡烈笑。

郁岑燁微皺眉。

厲若楓安靜觀察。

玩家有點不知所措。

這一鏡是 **第七室群像建立鏡頭**。

## 生圖 Prompt

> four-character group shot inside the Seventh Room dormitory entrance, ordinary young newcomer standing slightly uncertain in foreground, Qi Heng-Lie smiling warmly and confidently, Yu Cen-Ye standing upright with restrained critical expression, Li Ruo-Feng quietly observing with amber eyes, each character clearly distinct in face, hairstyle, body silhouette, costume and personality, ancient Chinese cultivation academy interior, premium realistic 3D eastern fantasy MMORPG cinematic, vertical 9:16

## 影片 Prompt

> four-character natural group interaction, Qi Heng-Lie gives a short laugh, Yu Cen-Ye glances toward him with mild disapproval, Li Ruo-Feng watches quietly, newcomer looks between them, subtle realistic facial animation, no exaggerated acting, slow gentle camera drift

## 對話

**玩家：**

「你們都看得出來？」

**郁岑燁：**

「至少我看得出來。」

齊衡烈笑。

---

# 十四、Shot 09｜「所以才要打一場」

## 時間

**0:45–0:50｜5 秒**

## 畫面

齊衡烈轉身拿起自己的重刀。

不是拔刀。

只是順手拿起。

## 影片 Prompt

> Qi Heng-Lie casually reaches for the heavy saber beside him, smiling with competitive excitement, no aggressive attack, Yu Cen-Ye remains unimpressed in the background, newcomer watches with slight surprise, natural body motion, realistic cloth physics, cinematic eastern fantasy academy interior

## 台詞

**齊衡烈：**

「所以才要打一場啊。」

## 音效

重刀輕碰刀架。

## BGM

木質節奏稍微加快。

---

# 十五、Shot 10｜蕭曜霖聲音先到

## 時間

**0:50–0:55｜5 秒**

## 畫面

所有人突然停住。

鏡頭不先拍人。

先拍走廊。

一道成熟低沉的聲音從門外傳來。

## 影片 Prompt

> camera slowly turns from the four young cultivators toward the quiet academy corridor outside the dormitory, everyone gradually stops moving as an unseen mature male voice speaks from off screen, subtle shift in atmosphere, morning light, restrained cinematic tension, no sudden horror

## 台詞

**蕭曜霖（畫外）：**

「不用等明天。」

## 聲音設計

說話前：

**環境音降低約 20%。**

不要使用巨大音效。

這樣反而更有壓迫感。

---

# 十六、Shot 11｜蕭曜霖登場

## 時間

**0:55–1:02｜7 秒**

## 畫面

鏡頭從四人的肩膀後方慢慢轉過去。

蕭曜霖站在東廊另一端。

晨光從背後照進來。

他沒有拔刀。

只是站著。

## 生圖 Prompt

> Xiao Yao-Lin standing calmly at the far end of an ancient academy corridor, mature powerful male heavy-blade instructor, broad strong build, heavy shoulder guard, gold meridian mark on forehead, dark instructor robe and armor, large heavy blade secured at his side, stern experienced eyes, calm authoritative presence, morning backlight and soft mist, premium realistic 3D eastern fantasy MMORPG cinematic, vertical 9:16

## 影片 Prompt

> slow cinematic reveal of Xiao Yao-Lin standing at the end of the corridor, camera moves around the shoulders of the young cultivators toward him, morning backlight silhouettes his broad figure, he remains completely still, subtle cloth movement, calm intimidating presence

## 台詞

**蕭曜霖：**

「半刻鐘後。」

停頓。

「新生演武場集合。」

## 表演

最後一句說完後，不再補充。

---

# 十七、Shot 12｜四人反應

## 時間

**1:02–1:08｜6 秒**

## 畫面

快速但不凌亂地切三個反應。

### 齊衡烈

眼睛亮起。

「這才對嘛。」

### 郁岑燁

輕輕嘆氣。

### 厲若楓

看向玩家。

玩家則低頭看自己的入院憑證。

## 影片 Prompt

> rapid but clean cinematic reaction sequence, Qi Heng-Lie smiles with excited anticipation, Yu Cen-Ye gives a restrained sigh, Li Ruo-Feng calmly looks toward the newcomer, newcomer looks down briefly at his admission token, natural facial animation, distinct personalities, realistic eastern fantasy MMORPG cinematic

## BGM

節奏稍微提升。

---

# 十八、Shot 13｜走向演武場

## 時間

**1:08–1:12｜4 秒**

## 畫面

玩家走出第七室。

齊衡烈從後方跟上。

郁岑燁與厲若楓也走出門。

最後一個畫面是四人的背影沿東廊走向遠處演武場。

## 影片 Prompt

> four young cultivators walking together down a long ancient academy corridor toward a distant training courtyard, newcomer walking among them naturally, Qi Heng-Lie slightly ahead, Yu Cen-Ye composed, Li Ruo-Feng observant, morning sunlight, subtle mist, cinematic tracking shot from behind, realistic cloth movement, premium 3D eastern fantasy MMORPG

## 聲音

腳步逐漸遠去。

---

# 十九、片尾鉤子

## 時間

**1:12–1:16｜4 秒**

## 畫面

鏡頭停留在空無一人的第七室門口。

門牌「第七室」。

畫面慢慢暗下。

## 後製文字

只出現一句：

**「半刻鐘後，他會知道天玄院的第一條規矩。」**

不要加入「敬請期待」。

## BGM

最後一個木質敲擊音。

---

# 二十、EP01 完整配音稿

以下為實際配音順序。

### 旁白

「九曜界，天玄院。」

### 齊衡烈

「你就是新來的？」

### 玩家

「應該是。」

### 齊衡烈

「那就對了。第七室最近正缺個能打的。」

### 郁岑燁

「你看都沒看他出手，怎麼知道他能打？」

### 齊衡烈

「所以才要打一場啊。」

### 厲若楓

「別急。他的腳步有點亂。」

### 玩家

「你們都看得出來？」

### 郁岑燁

「至少我看得出來。」

### 齊衡烈

「所以才要打一場啊。」

> 若 Shot 09 已完整說過，這裡不要重複配音。實際成片只保留一次。

### 蕭曜霖

「不用等明天。」

「半刻鐘後，新生演武場集合。」

---

# 二十一、配音表演規格

| 角色 | 音色 | 速度 | 表演 |
|---|---|---|---|
| 旁白 | 中性成熟 | 慢 | 世界觀介紹 |
| 玩家 | 年輕自然 | 中 | 不要英雄腔 |
| 齊衡烈 | 明亮、有力量 | 偏快 | 真誠、直接 |
| 郁岑燁 | 偏冷、乾淨 | 中慢 | 不耐多話 |
| 厲若楓 | 低沉、安靜 | 慢 | 觀察後才說 |
| 蕭曜霖 | 成熟低沉 | 慢 | 不怒自威 |

---

# 二十二、BGM／音效時間軸

| 時間 | 音樂／音效 |
|---|---|
| 0:00 | 空靈古風環境音 |
| 0:02 | 遠處鐘聲 |
| 0:05 | 木質打擊加入 |
| 0:14 | 木門開啟聲 |
| 0:20 | BGM 稍微明亮 |
| 0:26 | 齊衡烈說話時保持輕快 |
| 0:39 | 加入極輕弦樂 |
| 0:45 | 重刀碰刀架 |
| 0:50 | BGM 突然降低 |
| 0:50 | 蕭曜霖畫外音 |
| 0:55 | 低頻弦樂進入 |
| 1:02 | 節奏重新提升 |
| 1:08 | BGM 開始收束 |
| 1:12 | 只剩單一木質敲擊 |
| 1:16 | 靜音 |

---

# 二十三、剪輯規則

## 不要

- 快速炫技剪輯。
- 過多粒子。
- 大量轉場。
- 每個角色出場都使用特效。
- 使用遊戲 UI。
- 使用字幕特效遮住人物。
- 把蕭曜霖做成反派。
- 把齊衡烈做成搞笑角色。
- 把郁岑燁做成面癱反派。
- 把玩家做成天選之人。

## 要

- 讓人物對話自然。
- 讓鏡頭有呼吸感。
- 使用環境聲建立空間。
- 讓不同角色靠「站姿、眼神、動作」區分。
- 保持東方玄幻世界的質感。
- 第一集以人物關係為核心。

---

# 二十四、角色一致性檢查

正式生成前，建立以下 Reference ID：

- `PLAYER_REF_01`
- `QI_HENGLIE_REF_01`
- `YU_CENYE_REF_01`
- `LI_RUOFENG_REF_01`
- `XIAO_YAOLIN_REF_01`
- `TIANXUAN_ACADEMY_REF_01`
- `EAST_CORRIDOR_REF_01`
- `ROOM_07_REF_01`

後續所有影片生成：

**禁止重新隨機生成角色外觀。**

必須使用同一張 Reference。

---

# 二十五、AI 生圖實際流程

## Step 1

先生成：

**天玄院外景**

確認建築風格。

## Step 2

生成：

**東廊**

確認與天玄院外景屬於同一座學院。

## Step 3

生成：

**第七室**

確認門、木材、石材與東廊一致。

## Step 4

生成：

**玩家角色 Reference**

確認臉、服裝、身材。

## Step 5

生成：

**齊衡烈 Reference**

## Step 6

生成：

**郁岑燁 Reference**

## Step 7

生成：

**厲若楓 Reference**

## Step 8

生成：

**蕭曜霖 Reference**

## Step 9

生成 Shot 01～13。

---

# 二十六、實際生成順序

不要從 Shot 01 開始一路生成到 Shot 13。

推薦：

### 第一批：場景

1. 天玄院
2. 東廊
3. 第七室

### 第二批：人物

1. 玩家
2. 齊衡烈
3. 郁岑燁
4. 厲若楓
5. 蕭曜霖

### 第三批：靜態分鏡

先生成：

**Shot 01、03、04、06、07、08、11**

這七張作為視覺關鍵幀。

### 第四批：影片

再把關鍵幀轉成：

**Shot 01 → 13**

---

# 二十七、EP01 生成驗收標準

完成後必須逐項檢查。

## A. 世界

- [ ] 天玄院是東方玄幻學院
- [ ] 沒有西方城堡
- [ ] 沒有現代建築
- [ ] 沒有科幻元素
- [ ] 建築前後一致

## B. 玩家

- [ ] 是普通新生
- [ ] 沒有主角光環
- [ ] 沒有特殊血脈
- [ ] 沒有神秘發光眼睛
- [ ] 服裝前後一致

## C. 齊衡烈

- [ ] 體格明顯比其他年輕角色壯
- [ ] 紅金臂環明顯
- [ ] 重刀存在
- [ ] 表情較開朗
- [ ] 不像郁岑燁

## D. 郁岑燁

- [ ] 黑藍短髮
- [ ] 額頭曜紋
- [ ] 右手包紮
- [ ] 身形較修長
- [ ] 表情克制

## E. 厲若楓

- [ ] 琥珀色眼睛
- [ ] 身形較纖細
- [ ] 弓存在
- [ ] 表情安靜
- [ ] 與郁岑燁臉型不同

## F. 蕭曜霖

- [ ] 明顯成熟
- [ ] 體格厚重
- [ ] 肩甲
- [ ] 額頭金色曜紋
- [ ] 重刃
- [ ] 有教官壓迫感

## G. 劇情

- [ ] 玩家進入第七室
- [ ] 齊衡烈先開口
- [ ] 郁岑燁提出質疑
- [ ] 厲若楓觀察玩家腳步
- [ ] 蕭曜霖宣布半刻鐘後演武
- [ ] 沒有提前出現赤瞳妖將
- [ ] 沒有提前揭露天墟真相

## H. 成片

- [ ] 9:16
- [ ] 約 65～80 秒
- [ ] 沒有水印
- [ ] 沒有 AI 文字亂碼
- [ ] 沒有多手多腳
- [ ] 沒有角色換臉
- [ ] 沒有突然換服裝
- [ ] 對話與嘴型基本同步
- [ ] Shot 之間光線連續
- [ ] 片尾留下演武場鉤子

---

# 二十八、自我審查結果

## 劇情一致性

**通過**

EP01 僅使用第一章任務 01《第七室報到》的內容，沒有提前使用後續異化、古林、赤瞳妖將、裂口等資訊。

## 世界觀一致性

**通過**

沒有把玩家設定成預言主角，也沒有新增特殊血統。

## 角色一致性

**通過**

五名角色均以 `02_CHARACTER_BIBLE_V1.0.md` 已存在設定為基礎。

## 動畫製作可行性

**通過**

所有複雜動作被限制在自然走路、開門、轉頭、對話、拿刀等低難度動作。

## AI 生圖風險

**已修正**

最大風險是 AI 無法穩定生成「第七室」文字，因此已規定：

**門牌文字後製，不要求生圖模型生成。**

## AI 影片風險

**已修正**

沒有要求單鏡生成多人複雜戰鬥。

多人鏡頭主要為：

**站立＋對話＋自然表情。**

## 人物辨識度

**通過**

齊衡烈、郁岑燁、厲若楓、蕭曜霖在體型、髮型、服裝、武器與表演方式上均有明確差異。

## 節奏

**通過**

前 50 秒建立人物，50 秒後由蕭曜霖切入，最後進入演武場。

## 是否需要修改

**目前無需修改。**

---

# 二十九、EP01 最終製作版本

**文件狀態：READY FOR PRODUCTION**

第一集的製作核心不是「炫技」。

而是讓觀眾看完後記住：

> **「我想知道第七室這群人接下來會發生什麼。」**

第二集才開始逐步把觀眾從「人物故事」帶進「青嵐異常」。

---

# 文件結束
