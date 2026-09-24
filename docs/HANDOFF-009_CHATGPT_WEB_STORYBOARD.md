# 《九曜：天墟》HANDOFF-009

## EP01 分鏡圖：用 ChatGPT 網頁版出圖（免 API 費用）

**日期：2026-09-24**
**狀態：READY TO EXECUTE**
**執行者：咖哩（在 ChatGPT 網頁版操作）；ChatGPT 讀本檔協助**
**前置：EP01 設定圖 10 張已定稿（PR #3 已合併 main）**

> 本檔由 `production/EP01/shuohao/imagegen/gen_images.py handoff` 自動產生，提示詞與分鏡／設定圖同步。
> 分鏡或設定圖改了就重跑產生，不要手改提示詞。

---

# 1. 為什麼用網頁版

API 出圖要另外付費（30 張約 US$8～15），ChatGPT 網頁版的生圖包含在訂閱裡。
而且本作的官方畫風樣板（GPT 參考稿）本來就是在 ChatGPT 出的，同一個模型畫風最接近。

# 2. 給 ChatGPT 的規則（每個對話開頭先貼這段）

```text
你現在協助《九曜：天墟》EP01 分鏡圖出圖。規則：
1. 畫風以我附上的設定圖為準（國風仙俠 MMORPG 主視覺 CG／國漫 3D 動畫質感），不要改成寫實照片、日式動漫或西方奇幻。
2. 角色的臉、髮型、服裝、武器必須和我附上的角色設定圖一模一樣，不可重新設計。
3. 場景的建築、材質、擺設必須和場景設定圖一致。
4. 畫面裡不要出現任何文字、字幕、浮水印、邊框；門牌一律無字。
5. 一律出直式圖（9:16，做不到就 2:3 直式），單一完整畫面，不要拼貼、不要設定表版面。
6. 天空乾淨，不要出現裂縫、裂隙、紅色裂痕。
7. 每次只出我指定的那一張，照我貼的提示詞畫。
```

# 3. 操作流程

1. **一段開一個新對話**（例如 E01-01 一個對話），先貼第 2 節的規則。
2. 照下面每張的清單，**按順序**下載並附上參考圖（點連結→右鍵另存；順序對應提示詞裡的 Image 1、2、3…）。
3. 貼上該張的提示詞送出。
4. 滿意就下載，**檔名改成 `f1.png`、`f2.png`…**；不滿意就在同一個對話說哪裡不對、請它重畫。
5. 同一段的 f2 之後都在**同一個對話**裡做，清單會提醒你再附一次 f1（保持光線、角色一致）。
6. 一段做完，把圖上傳回 GitHub：打開該段的「上傳位置」連結 → 右上 **Add file → Upload files** → 拖進 f1.png、f2.png… → Commit。
   （或直接貼回 Claude 的 session，由 Claude 存進 repo。）

**建議先只做 E01-01、E01-02（共 5 張）**，上傳後請 Claude 逐張驗圖，畫風與一致性沒問題再做後面 7 段。

# 4. 驗圖重點（自己先看一遍）

- 畫風跟設定圖、參考稿放在一起像同一部作品
- 角色沒被重新設計：髮色、瞳色（厲若楓琥珀眼、玩家深棕眼）、武器、配件位置
- 沒有文字、沒有天空裂隙、門牌無字
- 只出現提示詞點名的角色

# 5. 逐張清單

共 30 張、9 段。

## E01-01（2 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-01](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-01)

### E01-01 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院外景-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E5%A4%96%E6%99%AF-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院外景) — match its architecture, materials, layout and wear exactly; the frame shows the same place.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: early sunrise from the right, low warm sun rays slanting through a band of white cloud flowing across the mountainside at mid-height, cool grey-cyan shadows on the lower tiers, clear pale morning sky with no cracks or fissures

Blocking (Chinese): 開場空景，無人物。天玄院三層院落位於畫面中軸，三重簷主殿在上三分之一，白色雲帶橫過中層；第二鏡轉到山腳門樓，門樓居中、兩面旗幟分立左右。

Shot (Chinese): 大遠景，直式 9:16，清晨的天玄院依山三層疊建，三重簷主殿在最上方，一條白色雲帶橫過院落中層緩緩流動，山腳青銅門樓與兩面深藍學院旗，晨光從右側斜照，天空乾淨無裂痕

Camera: 24mm 廣角，深景深; position 學院全貌 + 高位平視; composition 對稱; eyeline 無人物; focus 鎖定主殿與雲帶.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-01 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院外景-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E5%A4%96%E6%99%AF-sheet.png)
2. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院外景) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: early sunrise from the right, low warm sun rays slanting through a band of white cloud flowing across the mountainside at mid-height, cool grey-cyan shadows on the lower tiers, clear pale morning sky with no cracks or fissures

Blocking (Chinese): 開場空景，無人物。天玄院三層院落位於畫面中軸，三重簷主殿在上三分之一，白色雲帶橫過中層；第二鏡轉到山腳門樓，門樓居中、兩面旗幟分立左右。

Shot (Chinese): 全景，直式 9:16，山腳的古青銅門樓與玉白石臺階，兩面深藍學院旗在晨風中輕擺，門樓匾額無字，後方雲霧中的院落層層往上

Camera: 35mm 廣角，深景深; position 門樓 + 低位仰視 15°; composition 中心構圖; eyeline 無人物; focus 鎖定門樓與旗幟.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

## E01-02（3 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-02](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-02)

### E01-02 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. [入院憑證-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%85%A5%E9%99%A2%E6%86%91%E8%AD%89-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank, with no characters on it.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: early morning sun slanting in low from the open left side, long parallel bands of warm light and pillar shadows across the grey-cyan stone floor, soft mist beyond the railing, cool shade under the roof

Blocking (Chinese): 玩家背對鏡頭，從畫面下方偏右沿東廊中線往深處走；右側是一排木門，左側是欄杆與雲海；兩三名新生在他前方約五步外迎面走來。

Shot (Chinese): 中景，直式 9:16，玩家背影偏右，快步走在清晨的東廊上，右手攥著入院憑證，背上綁著行囊，兩側深色木柱往深處延伸，晨光從左側斜照在石板地上

Camera: 35mm 廣角，中景深; position 新生 + 背後肩下平視跟隨; composition 三分法; eyeline 右側房門; focus 鎖定新生背影與手中憑證.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-02 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: early morning sun slanting in low from the open left side, long parallel bands of warm light and pillar shadows across the grey-cyan stone floor, soft mist beyond the railing, cool shade under the roof

Blocking (Chinese): 玩家背對鏡頭，從畫面下方偏右沿東廊中線往深處走；右側是一排木門，左側是欄杆與雲海；兩三名新生在他前方約五步外迎面走來。

Shot (Chinese): 全景，直式 9:16，東廊中段，玩家從畫面中央走過，兩三名穿學院袍的新生從他身邊擦肩而過，左側欄杆外是雲海與遠山

Camera: 28mm 廣角，深景深; position 長廊 + 平視側前方; composition 對角線; eyeline 前方長廊; focus 鎖定新生.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-02 f3　→ 存成 `f3.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. [入院憑證-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%85%A5%E9%99%A2%E6%86%91%E8%AD%89-sheet.png)
4. [第七室門牌-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4%E9%96%80%E7%89%8C-sheet.png)
5. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank, with no characters on it.
Image 4: prop sheet of 第七室門牌 — match this object exactly; any plaque or token surface stays blank, with no characters on it.
Image 5: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: early morning sun slanting in low from the open left side, long parallel bands of warm light and pillar shadows across the grey-cyan stone floor, soft mist beyond the railing, cool shade under the roof

Blocking (Chinese): 玩家背對鏡頭，從畫面下方偏右沿東廊中線往深處走；右側是一排木門，左側是欄杆與雲海；兩三名新生在他前方約五步外迎面走來。

Shot (Chinese): 中景，直式 9:16，玩家在一扇深色木門前停下，從他右肩後方看過去，門楣右側掛著一塊無字的直立小木牌，他抬頭看著木牌，入院憑證握在胸前

Camera: 50mm 標準，淺景深; position 新生 + 右肩後過肩; composition 三分法; eyeline 門楣旁的木牌; focus 由新生肩膀轉到木牌.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

## E01-03（3 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-03](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-03)

### E01-03 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. [齊衡烈-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%BD%8A%E8%A1%A1%E7%83%88-sheet.png)
4. [入院憑證-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%85%A5%E9%99%A2%E6%86%91%E8%AD%89-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 4: prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank, with no characters on it.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 玩家站在門外走廊，位於畫面左側、側對鏡頭；齊衡烈從右側門內探出上半身，兩人隔著門檻相距約一步、面對面。

Shot (Chinese): 中景，直式 9:16，第七室的深色木門從裡面被拉開，齊衡烈從門內探出半個身子，笑著上下打量站在門外左側的玩家，室內晨光從他背後透出

Camera: 35mm 廣角，中景深; position 門口雙人 + 平視側面; composition 三分法; eyeline 壯碩青年看向新生; focus 鎖定探出門的壯碩青年.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-03 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. [齊衡烈-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%BD%8A%E8%A1%A1%E7%83%88-sheet.png)
4. [入院憑證-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%85%A5%E9%99%A2%E6%86%91%E8%AD%89-sheet.png)
5. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 4: prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank, with no characters on it.
Image 5: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 玩家站在門外走廊，位於畫面左側、側對鏡頭；齊衡烈從右側門內探出上半身，兩人隔著門檻相距約一步、面對面。

Shot (Chinese): 中景，直式 9:16，雙人鏡頭，齊衡烈在門內右側身體前傾、爽朗發問，玩家在門外左側握著憑證，略帶不確定地回答

Camera: 50mm 標準，中淺景深; position 雙人 + 平視側面; composition 三分法; eyeline 彼此面部; focus 鎖定兩人面部.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-03 f3　→ 存成 `f3.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [齊衡烈-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%BD%8A%E8%A1%A1%E7%83%88-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 玩家站在門外走廊，位於畫面左側、側對鏡頭；齊衡烈從右側門內探出上半身，兩人隔著門檻相距約一步、面對面。

Shot (Chinese): 特寫，直式 9:16，齊衡烈咧嘴笑開，眼角擠出笑紋，一手拍了拍自己戴著赤金臂環的右上臂，背後是第七室室內的晨光

Camera: 85mm 長焦，淺景深; position 壯碩青年 + 平視正面; composition 中心構圖; eyeline 新生面部; focus 鎖定壯碩青年面部與臂環.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

## E01-04（4 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-04](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-04)

### E01-04 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [郁岑燁-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%83%81%E5%B2%91%E7%87%81-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 室內：郁岑燁坐在中央矮桌後、畫面偏右，面朝門口，長劍橫在膝上；齊衡烈站在門內左側，兵器架在他身旁牆上；玩家在門檻外，這一段只露肩背。

Shot (Chinese): 中景，直式 9:16，第七室室內，郁岑燁坐在矮桌旁，一手拿布擦拭橫放膝上的長劍，聽見聲音抬起眼看向門口，窗櫺晨光打在他側臉

Camera: 50mm 標準，中淺景深; position 劍修 + 平視正側面; composition 三分法; eyeline 門口; focus 鎖定劍修雙眼.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-04 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [郁岑燁-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%83%81%E5%B2%91%E7%87%81-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 室內：郁岑燁坐在中央矮桌後、畫面偏右，面朝門口，長劍橫在膝上；齊衡烈站在門內左側，兵器架在他身旁牆上；玩家在門檻外，這一段只露肩背。

Shot (Chinese): 特寫，直式 9:16，郁岑燁的臉，眉頭輕皺，額間淡藍曜紋清晰，目光平直看向門口，語氣平淡地質疑

Camera: 85mm 長焦，淺景深; position 劍修 + 平視正面; composition 中心構圖; eyeline 門口的壯碩青年; focus 鎖定劍修面部.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-04 f3　→ 存成 `f3.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [齊衡烈-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%BD%8A%E8%A1%A1%E7%83%88-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 室內：郁岑燁坐在中央矮桌後、畫面偏右，面朝門口，長劍橫在膝上；齊衡烈站在門內左側，兵器架在他身旁牆上；玩家在門檻外，這一段只露肩背。

Shot (Chinese): 中景，直式 9:16，齊衡烈站在門內左側，順手從牆上木製兵器架提起粗鍛黑鐵的厚背重刀，沒有拔刀，咧嘴一笑，把刀提在身側

Camera: 35mm 廣角，中景深; position 壯碩青年 + 平視側前方; composition 三分法; eyeline 劍修; focus 鎖定壯碩青年與重刀.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-04 f4　→ 存成 `f4.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [郁岑燁-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%83%81%E5%B2%91%E7%87%81-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 室內：郁岑燁坐在中央矮桌後、畫面偏右，面朝門口，長劍橫在膝上；齊衡烈站在門內左側，兵器架在他身旁牆上；玩家在門檻外，這一段只露肩背。

Shot (Chinese): 特寫，直式 9:16，郁岑燁低下頭繼續擦劍，眉頭輕皺，嘴角沒有笑意

Camera: 85mm 長焦，淺景深; position 劍修 + 平視側面; composition 三分法; eyeline 手中長劍; focus 鎖定劍修側臉.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

## E01-05（5 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-05](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-05)

### E01-05 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [厲若楓-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E5%8E%B2%E8%8B%A5%E6%A5%93-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 厲若楓站在窗邊、畫面右側，側身對著門口，距門約四步；玩家在畫面左側門檻外；郁岑燁坐在中央矮桌後。

Shot (Chinese): 中景，直式 9:16，厲若楓靠窗站著，背後方格窗櫺透進晨光，雙手自然垂下，琥珀色的眼睛往下看向門口玩家的腳

Camera: 50mm 標準，中淺景深; position 弓修 + 平視側面; composition 三分法; eyeline 門口新生的腳; focus 鎖定弓修雙眼.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-05 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 厲若楓站在窗邊、畫面右側，側身對著門口，距門約四步；玩家在畫面左側門檻外；郁岑燁坐在中央矮桌後。

Shot (Chinese): 大特寫，直式 9:16，玩家的一雙軟布靴停在第七室門檻前的石板上，重心在兩腳之間換了一下

Camera: 100mm 微距，極淺景深; position 雙腳 + 低位平視; composition 中心構圖; eyeline 無; focus 鎖定雙腳.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-05 f3　→ 存成 `f3.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [厲若楓-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E5%8E%B2%E8%8B%A5%E6%A5%93-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 厲若楓站在窗邊、畫面右側，側身對著門口，距門約四步；玩家在畫面左側門檻外；郁岑燁坐在中央矮桌後。

Shot (Chinese): 特寫，直式 9:16，厲若楓抬起眼，琥珀色的眼睛平靜地看向玩家，神情沒有起伏

Camera: 85mm 長焦，淺景深; position 弓修 + 平視正面; composition 中心構圖; eyeline 新生面部; focus 鎖定琥珀色眼睛.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-05 f4　→ 存成 `f4.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 厲若楓站在窗邊、畫面右側，側身對著門口，距門約四步；玩家在畫面左側門檻外；郁岑燁坐在中央矮桌後。

Shot (Chinese): 中景，直式 9:16，玩家站在門檻外愣了一下，先看向右邊窗前的厲若楓，再看向矮桌旁的郁岑燁

Camera: 50mm 標準，中淺景深; position 新生 + 平視正面; composition 三分法; eyeline 先窗邊、再矮桌; focus 鎖定新生面部.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-05 f5　→ 存成 `f5.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. [入院憑證-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%85%A5%E9%99%A2%E6%86%91%E8%AD%89-sheet.png)
4. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank, with no characters on it.
Image 4: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 厲若楓站在窗邊、畫面右側，側身對著門口，距門約四步；玩家在畫面左側門檻外；郁岑燁坐在中央矮桌後。

Shot (Chinese): 特寫，直式 9:16，玩家真心發問的表情，眉頭微揚，手裡仍握著入院憑證

Camera: 85mm 長焦，淺景深; position 新生 + 平視正面; composition 中心構圖; eyeline 屋內三人; focus 鎖定新生面部.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

## E01-06（2 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-06](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-06)

### E01-06 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [郁岑燁-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%83%81%E5%B2%91%E7%87%81-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 第七室群像：玩家在畫面左前方門檻外，齊衡烈在他右側門內，郁岑燁坐在中後方矮桌，厲若楓在右後方窗邊；四人彼此相距兩到四步。

Shot (Chinese): 特寫，直式 9:16，郁岑燁頭也沒抬，繼續擦劍，語氣理所當然

Camera: 85mm 長焦，淺景深; position 劍修 + 平視側面; composition 三分法; eyeline 手中長劍; focus 鎖定劍修側臉與劍.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-06 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. [齊衡烈-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%BD%8A%E8%A1%A1%E7%83%88-sheet.png)
4. [郁岑燁-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%83%81%E5%B2%91%E7%87%81-sheet.png)
5. [厲若楓-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E5%8E%B2%E8%8B%A5%E6%A5%93-sheet.png)
6. [入院憑證-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%85%A5%E9%99%A2%E6%86%91%E8%AD%89-sheet.png)
7. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 4: character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 5: character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 6: prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank, with no characters on it.
Image 7: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 第七室群像：玩家在畫面左前方門檻外，齊衡烈在他右側門內，郁岑燁坐在中後方矮桌，厲若楓在右後方窗邊；四人彼此相距兩到四步。

Shot (Chinese): 全景，直式 9:16，第七室群像：玩家在左前方門檻外握著憑證，齊衡烈在他右側門內大笑著把重刀扛上肩，郁岑燁坐在中後方矮桌旁擦劍，厲若楓站在右後方窗邊看著玩家，窗櫺晨光灑滿室內

Camera: 28mm 廣角，深景深; position 四人 + 門內斜角平視; composition 對角線; eyeline 壯碩青年與弓修看向新生; focus 鎖定前景兩人.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

## E01-07（5 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-07](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-07)

### E01-07 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門外東廊：鏡頭在第七室門口一側朝走廊深處看；齊衡烈與厲若楓在門口，郁岑燁在門內後方矮桌旁，玩家在門檻外最靠近走廊；蕭曜霖站在走廊盡頭約二十步外的逆光裡。

Shot (Chinese): 全景，直式 9:16，從第七室門口望向東廊深處，走廊盡頭一片逆光晨霧，柱列與石板倒影往深處延伸，還看不見人

Camera: 35mm 廣角，深景深; position 走廊 + 平視一點透視; composition 對稱; eyeline 無; focus 鎖定走廊盡頭.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-07 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. [齊衡烈-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%BD%8A%E8%A1%A1%E7%83%88-sheet.png)
4. [郁岑燁-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%83%81%E5%B2%91%E7%87%81-sheet.png)
5. [厲若楓-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E5%8E%B2%E8%8B%A5%E6%A5%93-sheet.png)
6. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 4: character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 5: character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 6: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門外東廊：鏡頭在第七室門口一側朝走廊深處看；齊衡烈與厲若楓在門口，郁岑燁在門內後方矮桌旁，玩家在門檻外最靠近走廊；蕭曜霖站在走廊盡頭約二十步外的逆光裡。

Shot (Chinese): 中景，直式 9:16，從走廊側看向第七室門口，齊衡烈與厲若楓在門口同時轉頭望向走廊，郁岑燁在後方矮桌旁抬頭，玩家在門檻外跟著回頭，四人多為側臉或背影

Camera: 35mm 廣角，中景深; position 門口四人 + 走廊側平視; composition 三分法; eyeline 走廊盡頭; focus 鎖定門口四人.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-07 f3　→ 存成 `f3.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [蕭曜霖-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E8%95%AD%E6%9B%9C%E9%9C%96-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 蕭曜霖 — this is the person called 蕭曜霖 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門外東廊：鏡頭在第七室門口一側朝走廊深處看；齊衡烈與厲若楓在門口，郁岑燁在門內後方矮桌旁，玩家在門檻外最靠近走廊；蕭曜霖站在走廊盡頭約二十步外的逆光裡。

Shot (Chinese): 全景，直式 9:16，東廊盡頭逆光中，蕭曜霖高大的身影靜靜站著，雙肩青銅巨鎧的輪廓被晨光勾出亮邊，背後門板重劍的劍柄高出右肩

Camera: 50mm 標準，中景深; position 教官 + 過門口眾人肩後平視; composition 中心構圖; eyeline 門口的新生; focus 鎖定教官輪廓.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-07 f4　→ 存成 `f4.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [蕭曜霖-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E8%95%AD%E6%9B%9C%E9%9C%96-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 蕭曜霖 — this is the person called 蕭曜霖 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門外東廊：鏡頭在第七室門口一側朝走廊深處看；齊衡烈與厲若楓在門口，郁岑燁在門內後方矮桌旁，玩家在門檻外最靠近走廊；蕭曜霖站在走廊盡頭約二十步外的逆光裡。

Shot (Chinese): 中景，直式 9:16，蕭曜霖站在走廊上，逆光中神情嚴肅，眉骨淡金曜痕隱約可見，目光穩穩看向門口，開口宣布

Camera: 85mm 長焦，淺景深; position 教官 + 平視正面; composition 中心構圖; eyeline 門口四人; focus 鎖定教官面部.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-07 f5　→ 存成 `f5.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [蕭曜霖-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E8%95%AD%E6%9B%9C%E9%9C%96-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 蕭曜霖 — this is the person called 蕭曜霖 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門外東廊：鏡頭在第七室門口一側朝走廊深處看；齊衡烈與厲若楓在門口，郁岑燁在門內後方矮桌旁，玩家在門檻外最靠近走廊；蕭曜霖站在走廊盡頭約二十步外的逆光裡。

Shot (Chinese): 全景，直式 9:16，蕭曜霖轉身，沿著逆光的東廊往深處走遠，背後重劍隨步伐微晃

Camera: 35mm 廣角，深景深; position 教官背影 + 平視; composition 對稱; eyeline 走廊深處; focus 鎖定教官背影.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

## E01-08（4 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-08](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-08)

### E01-08 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [齊衡烈-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%BD%8A%E8%A1%A1%E7%83%88-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門口位置不變：齊衡烈在門內左側扛刀，厲若楓在右側門框旁，郁岑燁在後方矮桌旁，玩家在門檻外、畫面前景偏右，手中握著憑證。

Shot (Chinese): 特寫，直式 9:16，齊衡烈眼睛一亮，嘴角揚起，握緊肩上重刀的刀柄

Camera: 85mm 長焦，淺景深; position 壯碩青年 + 平視正面; composition 中心構圖; eyeline 走廊盡頭; focus 鎖定壯碩青年面部.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-08 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [郁岑燁-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E9%83%81%E5%B2%91%E7%87%81-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門口位置不變：齊衡烈在門內左側扛刀，厲若楓在右側門框旁，郁岑燁在後方矮桌旁，玩家在門檻外、畫面前景偏右，手中握著憑證。

Shot (Chinese): 中景，直式 9:16，郁岑燁坐在矮桌旁輕輕嘆了口氣，把擦好的長劍收回黑漆劍鞘

Camera: 50mm 標準，中淺景深; position 劍修 + 平視側面; composition 三分法; eyeline 手中劍鞘; focus 鎖定劍修與長劍.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-08 f3　→ 存成 `f3.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [厲若楓-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E5%8E%B2%E8%8B%A5%E6%A5%93-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門口位置不變：齊衡烈在門內左側扛刀，厲若楓在右側門框旁，郁岑燁在後方矮桌旁，玩家在門檻外、畫面前景偏右，手中握著憑證。

Shot (Chinese): 特寫，直式 9:16，厲若楓轉過頭，琥珀色的眼睛看向玩家

Camera: 85mm 長焦，淺景深; position 弓修 + 平視側面; composition 三分法; eyeline 新生; focus 鎖定弓修雙眼.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-08 f4　→ 存成 `f4.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [天玄院東廊-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E6%9D%B1%E5%BB%8A-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)
3. [入院憑證-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%85%A5%E9%99%A2%E6%86%91%E8%AD%89-sheet.png)
4. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (天玄院東廊) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.
Image 3: prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank, with no characters on it.
Image 4: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze at the end, pillars and floor reflections falling into silhouette toward the far end, warm rim light on the pillar edges

Blocking (Chinese): 門口位置不變：齊衡烈在門內左側扛刀，厲若楓在右側門框旁，郁岑燁在後方矮桌旁，玩家在門檻外、畫面前景偏右，手中握著憑證。

Shot (Chinese): 特寫，直式 9:16，玩家低頭看著手中的入院憑證，淡青白玉面上的學院圓紋映著晨光，表情壓住一點緊張

Camera: 85mm 長焦，淺景深; position 新生 + 平視略俯; composition 中心構圖; eyeline 手中憑證; focus 鎖定新生面部與憑證.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

## E01-09（2 張）

上傳位置：[https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-09](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/EP01/shuohao/storyboard/export/h3/E01-09)

### E01-09 f1　→ 存成 `f1.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [玩家-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E7%8E%A9%E5%AE%B6-sheet.png)

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: character model sheet of 玩家 — this is the person called 玩家 in the shot text; match the SAME face, hairstyle, costume and weapon exactly.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 第七室室內：玩家從畫面左側門口走進，往右後方窗邊的空床走去；第二鏡轉到門外，門牌在畫面中央。

Shot (Chinese): 中景，直式 9:16，玩家走進第七室，把背上的行囊放在靠窗那張鋪蓋整齊的空床上，窗櫺晨光斜照

Camera: 35mm 廣角，中景深; position 新生 + 側後方跟隨; composition 三分法; eyeline 空床; focus 鎖定新生與行囊.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```

### E01-09 f2　→ 存成 `f2.png`

**依序附上參考圖（順序＝提示詞裡的 Image 1、2、3…）：**

1. [第七室-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4-sheet.png)
2. [第七室門牌-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E7%AC%AC%E4%B8%83%E5%AE%A4%E9%96%80%E7%89%8C-sheet.png)
3. 本段你剛生成的 **f1.png**（同一個對話裡直接再附一次）

**提示詞（整段複製貼上）：**

```text
Reference images:
Image 1: environment sheet of this location (第七室) — match its architecture, materials, layout and wear exactly; the frame shows the same place.
Image 2: prop sheet of 第七室門牌 — match this object exactly; any plaque or token surface stays blank, with no characters on it.
Image 3: the opening frame of this same sequence — keep the world, lighting, mist density and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floorboards and the low table, soft bounce light on pale plaster walls, a few dust motes in the beam, the bright corridor visible through the open doorway behind

Blocking (Chinese): 第七室室內：玩家從畫面左側門口走進，往右後方窗邊的空床走去；第二鏡轉到門外，門牌在畫面中央。

Shot (Chinese): 特寫，直式 9:16，門楣右側的第七室門牌，深色硬木牌、古青銅包邊、青藍流蘇，牌面刻框留空，晨光斜照，畫面在此定格

Camera: 85mm 長焦，淺景深; position 門牌 + 平視正面; composition 中心構圖; eyeline 無; focus 鎖定門牌.

Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any background students the shot text explicitly mentions. No text, no watermark, no borders — a single clean full-bleed frame.
```
