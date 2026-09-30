# 《九曜：天墟》HANDOFF-010

## 長篇動畫第一集試片：第 2 場＋第 12 場分鏡圖（ChatGPT 網頁版）

**日期：2026-09-27**
**狀態：READY TO EXECUTE**
**執行者：咖哩（ChatGPT 網頁版）；出完貼回 Claude 驗圖**
**來源：`docs/43` 第 2、12 場；分鏡資料 `production/LF01/lf01.json`**

> 本檔由 `production/LF01/tools/lf01_handoff.py` 自動產生。分鏡或設定圖改了就重跑，不要手改提示詞。
> 連結指向 `main` 分支。

# 0. 建議做法：用 Codex 自動跑完（不用一張一張停）

一般 ChatGPT 對話的 GitHub 連接器是唯讀，存不回 repo。要「出一張、自己存、接著出下一張」請用 **Codex**，
開一個 Codex 任務（repo：`sky03104/jiuyao-tianxu`，分支：`main`），貼這一句：

```text
請照 production/LF01/CODEX_RUNBOOK.md 執行，依 production/LF01/codex_jobs.json 從頭到尾自動出圖、存檔、commit，不用停下來問我。
```

下面第 1～4 節是**手動備援**（在一般 ChatGPT 對話一張一張出圖時用）。

# 1. 目的

驗證 AI 出圖／出影片撐不撐得住長篇的兩個難點：**多人同框的對話戲**（第 2 場）與**多角色戰鬥＋巨大怪物**（第 12 場）。
過了才決定整集（約 550～650 張）投產。

# 2. 順序與數量

| 部分 | 內容 | 張數 |
|---|---|---|
| 一 | 新設定圖：江祈璟、聞人澈、赤瞳妖將、陣眼遺跡 | 4 |
| 二 | 第 12 場　赤瞳妖將（最難，先做） | 17 |
| 三 | 第 2 場　第七室報到（沿用 EP01 設定圖） | 24 |

**第一部分要先驗過**，第二部分才用得到新設定圖；第三部分可以同時開始。

# 3. 給 ChatGPT 的規則（每個對話開頭先貼）

```text
你現在協助《九曜：天墟》長篇動畫出圖。規則：
1. 畫風以我附上的設定圖為準（國風仙俠 MMORPG 主視覺 CG／國漫 3D 動畫質感），不要改成寫實照片、日式動漫或西方奇幻。
2. 角色的臉、髮型、服裝、武器必須和角色設定圖一模一樣，不可重新設計。
3. 場景的建築、材質、擺設必須和場景設定圖一致。
4. 畫面裡不要出現任何文字、字幕、浮水印、邊框；門牌一律無字。
5. 分鏡圖一律出橫式 16:9（做不到就 3:2 橫式），單一完整畫面，不要拼貼、不要設定表版面。
6. 每次只出我指定的那一張，照我貼的提示詞畫。
```

# 4. 操作

1. 一段開一個新對話，先貼第 3 節規則。
2. 每張照「上傳」清單附圖（最多 2 張：參考拼圖＋本段 f1），貼上提示詞。
3. 滿意就照指定檔名存；不滿意在同一對話說哪裡不對請它重畫。
4. 出完一段就貼回 Claude 的 session，由 Claude 存進 repo 並逐張驗圖。

---

# 第一部分：新設定圖（4 張，先做）

新角色與新場景沒有 GPT 參考稿，這裡附上**已定稿的其他設定圖當畫風參考**（提示詞裡已註明「只參考畫風，不抄外型」）。
做完存成下列檔名，上傳到 [https://github.com/sky03104/jiuyao-tianxu/tree/main/production/LF01](https://github.com/sky03104/jiuyao-tianxu/tree/main/production/LF01) 對應資料夾，或直接貼回 Claude 的 session。

## 江祈璟　→ 存成 `production/LF01/characters/江祈璟-sheet.png`

**上傳：** [參考拼圖 江祈璟-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/sheets/%E6%B1%9F%E7%A5%88%E7%92%9F-refs.jpg)（2 張拼成一張）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): approved character sheet of a DIFFERENT character (郁岑燁) — use it ONLY as the rendering-style and sheet-layout reference; do not copy the face, hair, costume or weapon
  - Panel 2 (top-right): approved character sheet of a DIFFERENT character (齊衡烈) — use it ONLY as the rendering-style and sheet-layout reference; do not copy the face, hair, costume or weapon

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed.

Single character model sheet on ONE 16:9 landscape canvas (widescreen, width to height exactly 16:9). The canvas is divided into three zones by thin hairline rules. LEFT ZONE — a vertical column occupying about 34% of the canvas width: one bust portrait, head and shoulders, front-facing, centred; both shoulders fully visible; the portrait ends in a clean straight horizontal cut just below the chest. The bust shows a composed, sharp-featured young East Asian man around twenty with a narrow oval face, high cheekbones, clean jawline, straight nose, calm narrow dark eyes under straight dark brows that give a faintly appraising look, lips closed in a neutral line; long glossy black hair drawn back into a high single ponytail bound with a dark silver cord, a few fine strands at the temples; the collar of a deep navy cross-collar robe with silver-white trim and a small antique bronze academy badge inset with jade. LIGHTING IN THE LEFT ZONE ONLY: soft directional key light from the upper left with gentle falloff and subtle ambient occlusion. RIGHT-TOP ZONE: three FULL-BODY views of the SAME character standing side by side — front view, left side profile, back view — on one shared ground line, identical height and proportions, head to toe with clear margins, neutral relaxed standing posture. The figure is a lean, upright young East Asian man around twenty, a spear cultivator of an ancient-Chinese-inspired mountain academy from a respected family, posture straight and economical, wearing a fitted deep navy cross-collar robe with silver-white trim over a pale grey inner robe, narrow sleeves bound by dark leather bracers on both forearms, a slim dark leather belt with the academy badge of antique bronze inset with carved jade, dark trousers and soft leather boots; a long spear of straight dark hardwood with a leaf-shaped polished steel head, a small antique bronze bell tied just below the head with a short dark cord; fine calluses visible on the knuckles; long black hair in a high single ponytail reaching between the shoulder blades. The faces on all three full-body views match the bust portrait exactly. LIGHTING IN THE RIGHT ZONES: flat even orthographic lighting, no cast shadows. RIGHT-BOTTOM ZONE: a row of four to five small isolated close-up studies: the small antique bronze bell tied below the spearhead; the leaf-shaped polished steel spearhead; the high ponytail bound with a dark silver cord; the calloused knuckles of his right hand on the spear shaft; the dark leather forearm bracer. Never shrink the full-body figures to make room. Plain pure white background (#FFFFFF) throughout. deep navy, silver-white and polished steel palette, cool clear light, readable silhouette from every angle.

LAYOUT CHECK: exactly THREE full-body figures in the top-right zone — front, left profile, back. Not four, no three-quarter view.

Avoid: smirk, arrogant sneer, villain look, heavy armour, cape, twin ponytails, bun, short hair, halberd, trident, glowing weapon, western knight, text, watermark. No text, no letters, no labels, no captions, no logos, no watermark, no UI.
```

## 聞人澈　→ 存成 `production/LF01/characters/聞人澈-sheet.png`

**上傳：** [蕭曜霖-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E8%95%AD%E6%9B%9C%E9%9C%96-sheet.png)

```text
Reference images:
Image 1: approved character sheet of a DIFFERENT character (蕭曜霖) — use it ONLY as the rendering-style and sheet-layout reference; do not copy the face, hair, costume or weapon

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed.

Single character model sheet on ONE 16:9 landscape canvas (widescreen, width to height exactly 16:9). The canvas is divided into three zones by thin hairline rules. LEFT ZONE — a vertical column occupying about 34% of the canvas width: one bust portrait, head and shoulders, front-facing, centred; both shoulders fully visible; the portrait ends in a clean straight horizontal cut just below the chest. The bust shows a quiet, slightly slender young East Asian man around twenty with a soft oval face, gentle downturned dark eyes that look calm and a little tired, straight thin brows, a small straight nose, lips closed; dark hair falling just past the jaw, half of it loosely tied back at the crown with a plain wooden pin; the collar of a grey-blue hooded over-robe with the hood down, a dark inner robe beneath; a clean forehead with NO mark or tattoo. LIGHTING IN THE LEFT ZONE ONLY: soft directional key light from the upper left with gentle falloff and subtle ambient occlusion. RIGHT-TOP ZONE: three FULL-BODY views of the SAME character standing side by side — front view, left side profile, back view — on one shared ground line, identical height and proportions, head to toe with clear margins, neutral relaxed standing posture. The figure is a slender young East Asian man around twenty, a spell-staff cultivator of an ancient-Chinese-inspired mountain academy who gathers medicinal herbs, standing quietly with slightly rounded shoulders, wearing a long grey-blue hooded over-robe with the hood down over a dark charcoal inner robe, a cloth sash with the academy badge of antique bronze inset with carved jade, a small worn leather herb pouch at the right hip, dark trousers and soft cloth boots; in his right hand a short staff of dark xuan wood about forearm length with a rounded head carved with fine rune grooves; around his left wrist three thin interlocking talisman rings of antique bronze and pale jade; dark jaw-length hair half tied back with a wooden pin; he carries NO sword and NO blade of any kind — his only tool is the short staff. The faces on all three full-body views match the bust portrait exactly. LIGHTING IN THE RIGHT ZONES: flat even orthographic lighting, no cast shadows. RIGHT-BOTTOM ZONE: a row of four to five small isolated close-up studies: the short dark xuan-wood staff with its carved rune grooves; the three interlocking talisman rings of bronze and pale jade on the left wrist; the worn leather herb pouch; the grey-blue hood lying down on the shoulders; the wooden hair pin. Never shrink the full-body figures to make room. Plain pure white background (#FFFFFF) throughout. grey-blue, charcoal and pale jade palette, soft overcast light, readable silhouette from every angle.

LAYOUT CHECK: exactly THREE full-body figures in the top-right zone — front, left profile, back. Not four, no three-quarter view.

Avoid: sword, sabre, blade, scabbard at the hip, forehead mark, forehead tattoo, bandaged or wrapped hand, spiky short hair, dark navy robe, bronze shoulder armour, pauldrons, long wizard staff, crystal orb, pointed wizard hat, glowing eyes, western mage robe, smug expression, heavy armour, text, watermark. No text, no letters, no labels, no captions, no logos, no watermark, no UI.
```

## 赤瞳妖將　→ 存成 `production/LF01/characters/赤瞳妖將-sheet.png`

**上傳：** [蕭曜霖-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/characters/images/%E8%95%AD%E6%9B%9C%E9%9C%96-sheet.png)

```text
Reference images:
Image 1: approved character sheet of a DIFFERENT character (蕭曜霖) — use it ONLY as the rendering-style and sheet-layout reference; do not copy the face, hair, costume or weapon

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed.

Single character model sheet on ONE 16:9 landscape canvas (widescreen, width to height exactly 16:9). The canvas is divided into three zones by thin hairline rules. LEFT ZONE — a vertical column occupying about 34% of the canvas width: one bust portrait, head and shoulders, front-facing, centred; both shoulders fully visible; the portrait ends in a clean straight horizontal cut just below the chest. The bust shows the head and shoulders of a towering demon general of an Eastern fantasy world: a long, noble, half-beast half-human face with an elongated jaw and high sharp cheekbones, skin of dark ash-grey with fine lacquer-like scale texture along the temples and throat, two deep crimson eyes with dark sclera and a calm, heavy, unreadable gaze, two short swept-back horn ridges of black horn rising from above the brows along the skull, a long mane of ink-grey hair falling loose behind the shoulders; faint hairline seams of dull crimson light running along the scale edges at the neck; the collar of layered crimson-black scale plates. LIGHTING IN THE LEFT ZONE ONLY: soft directional key light from the upper left with gentle falloff and subtle ambient occlusion. RIGHT-TOP ZONE: three FULL-BODY views of the SAME character standing side by side — front view, left side profile, back view — on one shared ground line, identical height and proportions, head to toe with clear margins, neutral relaxed standing posture. The figure is a towering humanoid demon general about two and a half times the height of the academy students, long-limbed and lean rather than bulky, standing completely still with a regal, patient bearing; the body is covered in layered crimson-black scale plates that look grown rather than forged, like lacquered armour, with faint seams of dull crimson light along the plate edges; long dark-grey forearms ending in hands with long black claws; around the waist a torn, faded ceremonial sash of deep blue cloth with frayed edges, the only cloth on him; bare clawed feet; no weapon; long ink-grey mane falling to the middle of the back; two short swept-back black horn ridges; deep crimson eyes. Show the true towering scale by keeping the figure alone and complete within the zone. The faces on all three full-body views match the bust portrait exactly. LIGHTING IN THE RIGHT ZONES: flat even orthographic lighting, no cast shadows. RIGHT-BOTTOM ZONE: a row of four to five small isolated close-up studies: the deep crimson eyes with dark sclera; the short swept-back black horn ridges; the layered crimson-black scale plates with dull crimson seams; the long black claws; the torn faded deep-blue ceremonial sash at the waist. Never shrink the full-body figures to make room. Plain pure white background (#FFFFFF) throughout. crimson-black, ash grey and faded deep blue palette, cold misty forest light, readable silhouette from every angle.

LAYOUT CHECK: exactly THREE full-body figures in the top-right zone — front, left profile, back. Not four, no three-quarter view.

Avoid: western devil, bat wings, huge ram horns, goat legs, fire and brimstone, gore, blood, exposed bones, skull face, zombie, grotesque drooling mouth, bulging muscles, cute, chibi, weapon, armour made of metal, text, watermark. No text, no letters, no labels, no captions, no logos, no watermark, no UI.
```

## 陣眼遺跡　→ 存成 `production/LF01/art/陣眼遺跡-sheet.png`

**上傳：** [天玄院外景-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E5%A4%96%E6%99%AF-sheet.png)

```text
Reference images:
Image 1: approved environment sheet of a DIFFERENT location — use it ONLY as the rendering-style, material and sheet-layout reference; do not copy its architecture

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Environment reference sheet on ONE 16:9 landscape canvas, panels separated by thin hairline rules. MAIN ZONE, top-left, about 72% of the canvas width and 70% of its height: the master establishing view: a circular sunken stone courtyard deep inside an ancient misty forest: concentric rings of weathered grey stone slabs carved with broken, worn rune grooves, half swallowed by moss and huge twisting tree roots; at the centre a round stone pedestal holding a dark faceted crystal about the size of a person's head, set in a bronze-green corroded clasp; three waist-high carved stone pillars standing in a wide triangle around the courtyard, each with a flat top carved with a small circular sigil; enormous ancient trees closing overhead, shafts of light and mist between the trunks; the stone is old but sound — this place was built long before the academy — the canonical look of this location. RIGHT COLUMN and BOTTOM ROW: small close-up detail studies of the dark faceted crystal on its stone pedestal with the corroded bronze-green clasp; one of the three carved stone pillars with the circular sigil on its flat top; a section of the concentric rune rings with broken worn grooves and moss; huge tree roots gripping the edge of the stone courtyard. Every detail panel is a close-up crop of the SAME space shown in the master view. Absolutely no people anywhere on the sheet. Lighting in all panels: late afternoon light falling through the high forest canopy in soft shafts, cool green mist between the trunks, the crystal dark and quiet.

Avoid: people, characters, modern structures, western temple, greek columns, stonehenge copy, lava, fire, bright cartoon colours, text, watermark. No text, no letters, no labels, no captions, no logos, no watermark, no UI.
```

# 第三部分：第 2 場　第七室報到

## A01（15 秒，2 張）

### A01 f1　→ 存成 `production/LF01/storyboard/frames/A01/f1.png`

8 秒｜大遠景｜Push In｜台詞：旁白：「九曜界，天玄院。」

**上傳：** [天玄院外景-sheet.png](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/EP01/shuohao/art/images/%E5%A4%A9%E7%8E%84%E9%99%A2%E5%A4%96%E6%99%AF-sheet.png)

```text
Reference images:
Image 1: environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: clear early morning, a long band of white cloud drifting across the middle of the mountain, first sunlight turning the top hall pale gold

Blocking (Chinese): 開場空景接門樓人潮；机遙獨自一人拾級而上

Shot (Chinese): 大遠景，橫式 16:9。依山疊建的天玄院三層院落，白色雲帶從山腰緩緩流過，最上方三重簷主殿被晨光照成淡金色。

Shot size: 大遠景; camera move: Push In (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A01 f2　→ 存成 `production/LF01/storyboard/frames/A01/f2.png`

7 秒｜全景｜Static Shot｜台詞：（無台詞）

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A01/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: clear early morning, a long band of white cloud drifting across the middle of the mountain, first sunlight turning the top hall pale gold

Blocking (Chinese): 開場空景接門樓人潮；机遙獨自一人拾級而上

Shot (Chinese): 全景，橫式 16:9。山腳銅框門樓，兩面深藍學院旗分立左右；石階上新生與送行家人來來往往，有人挑著箱籠，机遙一個人背著捲布行囊走在石階中段，畫面右三分之一。

Shot size: 全景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A02（15 秒，2 張）

### A02 f1　→ 存成 `production/LF01/storyboard/frames/A02/f1.png`

8 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A02/f1-refs.jpg)（3 張拼成一張）

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: clear early morning, soft warm light on the gatehouse

Blocking (Chinese): 門樓下，机遙抬頭；閃回去年冬天考核失敗

Shot (Chinese): 中景低角度，橫式 16:9。机遙站在門樓下仰頭，看匾額位置上的圓形雲星徽紋（無字），右手握著入院憑證垂在身側。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A02 f2　→ 存成 `production/LF01/storyboard/frames/A02/f2.png`

7 秒｜全景｜Static Shot｜台詞：（無台詞）

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A02/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: clear early morning, soft warm light on the gatehouse

Blocking (Chinese): 門樓下，机遙抬頭；閃回去年冬天考核失敗

Shot (Chinese): 全景，橫式 16:9，冷灰色調的回憶畫面。同一座門樓在冬天是灰的，地上殘雪；考核場的站樁石圈裡，机遙被推出圈外、單膝跌在雪泥上，圈外考官背影模糊。

Shot size: 全景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A03（15 秒，2 張）

### A03 f1　→ 存成 `production/LF01/storyboard/frames/A03/f1.png`

7 秒｜特寫｜Static Shot｜台詞：（無台詞）

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A03/f1-refs.jpg)（2 張拼成一張）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning light in the shade under the gatehouse roof

Blocking (Chinese): 報到木案，執事驗憑證

Shot (Chinese): 特寫，橫式 16:9。報到木案上一塊銅盤，一隻手把玉白入院憑證按在銅盤上，銅盤亮起淡淡青光。

Shot size: 特寫; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A03 f2　→ 存成 `production/LF01/storyboard/frames/A03/f2.png`

8 秒｜中景｜Static Shot｜台詞：院方執事：「青嵐新契修，机遙。東廊，第七室。」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A03/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning light in the shade under the gatehouse roof

Blocking (Chinese): 報到木案，執事驗憑證

Shot (Chinese): 過肩中景，橫式 16:9。從院方執事（深色制服、背影與肩膀）肩後看出去，机遙站在木案前，身上沒有任何兵器；背景是排隊的新生。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A04（15 秒，2 張）

### A04 f1　→ 存成 `production/LF01/storyboard/frames/A04/f1.png`

8 秒｜中景｜Tracking Shot｜台詞：（無台詞）

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A04/f1-refs.jpg)（3 張拼成一張）

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): prop sheet of 入院憑證 — match this object exactly; any plaque or token surface stays blank

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: early morning sun slanting in low from the open left side, long parallel bands of warm light and pillar shadows across the floor

Blocking (Chinese): 東廊跟拍机遙；新生擦肩

Shot (Chinese): 中景跟拍，橫式 16:9。机遙背影在畫面右三分之一，快步走在東廊上，左側欄杆外是雲海，柱影一條條鋪在石板地；他邊走邊看右側的房門。

Shot size: 中景; camera move: Tracking Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A04 f2　→ 存成 `production/LF01/storyboard/frames/A04/f2.png`

7 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A04/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: early morning sun slanting in low from the open left side, long parallel bands of warm light and pillar shadows across the floor

Blocking (Chinese): 東廊跟拍机遙；新生擦肩

Shot (Chinese): 中景，橫式 16:9。一個背大包袱的新生從机遙身邊擠過，包袱撞到机遙背上的行囊，那人回頭揮手道歉已經跑遠；机遙把行囊背帶往上拉。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A05（15 秒，2 張）

### A05 f1　→ 存成 `production/LF01/storyboard/frames/A05/f1.png`

6 秒｜中景｜Push In｜台詞：（無台詞）

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A05/f1-refs.jpg)（3 張拼成一張）

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): prop sheet of 第七室門牌 — match this object exactly; any plaque or token surface stays blank

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning, the far end of the corridor quieter, sunlight narrowing to a strip along the wall

Blocking (Chinese): 第七室門前；門被拉開，齊衡烈探身

Shot (Chinese): 中景，橫式 16:9。机遙站在一扇深色舊木門前，抬頭看門楣旁的直立小門牌（無字），手裡拿著入院憑證。

Shot size: 中景; camera move: Push In (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A05 f2　→ 存成 `production/LF01/storyboard/frames/A05/f2.png`

9 秒｜中景｜Static Shot｜台詞：齊衡烈：「你就是新來的？」；机遙：「應該是。」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A05/f2-refs.jpg)（3 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning, the far end of the corridor quieter, sunlight narrowing to a strip along the wall

Blocking (Chinese): 第七室門前；門被拉開，齊衡烈探身

Shot (Chinese): 雙人中景，橫式 16:9。門從裡面被拉開，齊衡烈一手撐門框、探出半個身子上下打量机遙；机遙站在門檻外，兩人側面相對。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A06（15 秒，2 張）

### A06 f1　→ 存成 `production/LF01/storyboard/frames/A06/f1.png`

7 秒｜近景｜Static Shot｜台詞：齊衡烈：「那就對了。第七室最近正缺個能打的。」

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A06/f1-refs.jpg)（2 張拼成一張）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floor

Blocking (Chinese): 第七室門口與屋內：齊衡烈、郁岑燁

Shot (Chinese): 近景，橫式 16:9。齊衡烈咧嘴大笑，抬手拍在自己右上臂的赤金臂環上。

Shot size: 近景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A06 f2　→ 存成 `production/LF01/storyboard/frames/A06/f2.png`

8 秒｜中景｜Static Shot｜台詞：郁岑燁：「你看都沒看他出手，怎麼知道他能打？」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A06/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight entering through the large square lattice window opposite the door, crisp lattice shadows across the floor

Blocking (Chinese): 第七室門口與屋內：齊衡烈、郁岑燁

Shot (Chinese): 中景，橫式 16:9。屋內矮桌旁，郁岑燁坐著用布擦長劍，聽到話才抬起眼，目光冷淡；窗格光影落在桌面。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A07（15 秒，2 張）

### A07 f1　→ 存成 `production/LF01/storyboard/frames/A07/f1.png`

8 秒｜中景｜Static Shot｜台詞：齊衡烈：「所以才要打一場啊。」

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A07/f1-refs.jpg)（2 張拼成一張）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight through the lattice window

Blocking (Chinese): 齊衡烈提刀；郁岑燁低頭

Shot (Chinese): 中景，橫式 16:9。齊衡烈從門邊兵器架提起單刃厚背重刀扛上肩，咧嘴一笑。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A07 f2　→ 存成 `production/LF01/storyboard/frames/A07/f2.png`

7 秒｜近景｜Static Shot｜台詞：郁岑燁：「……無聊。」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A07/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight through the lattice window

Blocking (Chinese): 齊衡烈提刀；郁岑燁低頭

Shot (Chinese): 近景，橫式 16:9。郁岑燁低頭繼續擦劍，眉頭輕皺。

Shot size: 近景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A08（15 秒，2 張）

### A08 f1　→ 存成 `production/LF01/storyboard/frames/A08/f1.png`

8 秒｜中景｜Static Shot｜台詞：厲若楓：「別急。他的腳步有點亂。」

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A08/f1-refs.jpg)（2 張拼成一張）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight through the lattice window, the window side bright

Blocking (Chinese): 厲若楓看机遙的腳；机遙愣住

Shot (Chinese): 中景，橫式 16:9。厲若楓靠在窗邊牆上，背後三段式短弓，琥珀色眼睛往下看著門口的方向（看的是机遙的腳），窗光在他半邊臉上。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A08 f2　→ 存成 `production/LF01/storyboard/frames/A08/f2.png`

7 秒｜近景｜Static Shot｜台詞：机遙：「你們都看得出來？」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A08/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight through the lattice window, the window side bright

Blocking (Chinese): 厲若楓看机遙的腳；机遙愣住

Shot (Chinese): 近景，橫式 16:9。机遙站在門檻前愣了一下，視線從窗邊移向矮桌，表情是真心的疑問。

Shot size: 近景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A09（15 秒，2 張）

### A09 f1　→ 存成 `production/LF01/storyboard/frames/A09/f1.png`

7 秒｜近景｜Static Shot｜台詞：郁岑燁：「至少我看得出來。」

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A09/f1-refs.jpg)（2 張拼成一張）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight through the lattice window

Blocking (Chinese): 郁岑燁頭也不抬；齊衡烈大笑

Shot (Chinese): 近景，橫式 16:9。郁岑燁頭也沒抬，布在劍身上滑過。

Shot size: 近景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A09 f2　→ 存成 `production/LF01/storyboard/frames/A09/f2.png`

8 秒｜中景｜Static Shot｜台詞：齊衡烈：「哈哈，那更好！明天演武場見。」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A09/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight through the lattice window

Blocking (Chinese): 郁岑燁頭也不抬；齊衡烈大笑

Shot (Chinese): 中景，橫式 16:9。齊衡烈仰頭大笑，把肩上的重刀顛了一下。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A10（15 秒，2 張）

### A10 f1　→ 存成 `production/LF01/storyboard/frames/A10/f1.png`

6 秒｜全景｜Static Shot｜台詞：蕭曜霖（畫外）：「不用等明天。」

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A10/f1-refs.jpg)（5 張拼成一張）

```text
Reference images:
Image 1: a reference board of 5 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-middle): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (top-right): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 4 (middle-left): character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 5 (middle-middle): character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze

Blocking (Chinese): 三人轉頭；走廊盡頭逆光中的蕭曜霖

Shot (Chinese): 全景，橫式 16:9，鏡頭在東廊上看進第七室敞開的門口。屋內齊衡烈、郁岑燁、厲若楓同時轉頭望向走廊（朝鏡頭方向），站在門檻上的机遙跟著回頭；四人都在門框之內。

Shot size: 全景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A10 f2　→ 存成 `production/LF01/storyboard/frames/A10/f2.png`

9 秒｜全景｜Push In｜台詞：蕭曜霖：「半刻鐘後，新生演武場集合。」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A10/f2-refs.jpg)（2 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 蕭曜霖 — this is the person called 蕭曜霖 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: looking down the corridor toward its far end with the morning sun directly behind the far opening, strong soft backlight and glowing haze

Blocking (Chinese): 三人轉頭；走廊盡頭逆光中的蕭曜霖

Shot (Chinese): 全景逆光，橫式 16:9。東廊盡頭晨光最亮處，蕭曜霖站著不動，厚重護肩邊緣有補過的痕跡，背後門板重劍的劍柄高出肩頭；身影在光霧中近乎剪影。

Shot size: 全景; camera move: Push In (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A11（15 秒，2 張）

### A11 f1　→ 存成 `production/LF01/storyboard/frames/A11/f1.png`

7 秒｜全景｜Static Shot｜台詞：（無台詞）

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A11/f1-refs.jpg)（2 張拼成一張）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 蕭曜霖 — this is the person called 蕭曜霖 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: strong morning backlight at the far end of the corridor

Blocking (Chinese): 蕭曜霖離去；机遙與齊衡烈

Shot (Chinese): 全景，橫式 16:9。蕭曜霖轉身沿走廊走遠的背影，兩旁房門口探頭的新生紛紛縮回頭。

Shot size: 全景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A11 f2　→ 存成 `production/LF01/storyboard/frames/A11/f2.png`

8 秒｜中景｜Static Shot｜台詞：机遙：「這麼快？」；齊衡烈：「習慣就好。」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A11/f2-refs.jpg)（3 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: strong morning backlight at the far end of the corridor

Blocking (Chinese): 蕭曜霖離去；机遙與齊衡烈

Shot (Chinese): 雙人中景，橫式 16:9。第七室門口，机遙看著走廊盡頭；齊衡烈握緊肩上的刀柄，眼睛發亮地笑。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## A12（15 秒，2 張）

### A12 f1　→ 存成 `production/LF01/storyboard/frames/A12/f1.png`

7 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** [參考拼圖 f1-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A12/f1-refs.jpg)（2 張拼成一張）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight through the lattice window

Blocking (Chinese): 机遙放下行囊；齊衡烈自我介紹

Shot (Chinese): 中景，橫式 16:9。机遙把捲布行囊放在靠窗那張疊得方正的空床上，窗外看得到更高一層院子的演武場一角。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### A12 f2　→ 存成 `production/LF01/storyboard/frames/A12/f2.png`

8 秒｜中景｜Static Shot｜台詞：齊衡烈：「喂，新來的。我叫齊衡烈。你呢？」；机遙：「机遙。」

**上傳：** [參考拼圖 f2-refs.jpg](https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/main/production/LF01/storyboard/chatgpt/A12/f2-refs.jpg)（3 張拼成一張）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: morning sunlight through the lattice window

Blocking (Chinese): 机遙放下行囊；齊衡烈自我介紹

Shot (Chinese): 雙人中景，橫式 16:9。齊衡烈已經走到門口，扛著刀回頭用拇指往身後一比；机遙在窗邊床前轉過身。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

# 第二部分：第 12 場　赤瞳妖將（先做這段）

## B01（15 秒，2 張）

### B01 f1　→ 存成 `production/LF01/storyboard/frames/B01/f1.png`

7 秒｜大遠景｜Static Shot｜台詞：（無台詞）

**上傳：** `production/LF01/art/陣眼遺跡-sheet.png`（待出圖）

```text
Reference images:
Image 1: environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 陣眼遺跡外，異化妖獸從隊伍身邊奔過

Shot (Chinese): 大遠景，橫式 16:9，從林中仰望。古林樹冠縫隙間透出暗紅色的光，霧被映成紅色，遠處林梢在搖動。

Shot size: 大遠景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### B01 f2　→ 存成 `production/LF01/storyboard/frames/B01/f2.png`

8 秒｜全景｜Static Shot｜台詞：齊衡烈：「它們在逃？」；江祈璟：「不是逃。是在讓路。」

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B01/f2-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 4 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 4 (middle-right): character model sheet of 江祈璟 — this is the person called 江祈璟 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 陣眼遺跡外，異化妖獸從隊伍身邊奔過

Shot (Chinese): 全景，橫式 16:9。一群眼睛血紅、背上有暗色脈紋的異化狼與狐從前景奔過，繞開站在石庭邊緣的机遙、齊衡烈、江祈璟，全都往林外跑。

Shot size: 全景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## B02（15 秒，2 張）

### B02 f1　→ 存成 `production/LF01/storyboard/frames/B02/f1.png`

8 秒｜全景｜Push In｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B02/f1-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 赤瞳妖將 — this is the person called 赤瞳妖將 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 赤瞳妖將現身，看陣眼再看机遙

Shot (Chinese): 全景低角度，橫式 16:9。紅霧向兩側散開讓出一條路，赤瞳妖將從霧中走出，身高是人的兩倍多，姿態靜而沉，沒有吼叫；背後林木被映紅。

Shot size: 全景; camera move: Push In (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### B02 f2　→ 存成 `production/LF01/storyboard/frames/B02/f2.png`

7 秒｜特寫｜Static Shot｜台詞：赤瞳妖將：「……人族。你們也開始聽見了嗎？」；机遙：「你在說什麼？」

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B02/f2-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 赤瞳妖將 — this is the person called 赤瞳妖將 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 赤瞳妖將現身，看陣眼再看机遙

Shot (Chinese): 特寫，橫式 16:9。赤瞳妖將的臉與一雙暗紅眼睛，目光從畫面左側（陣眼方向）慢慢移向右側（机遙方向）。

Shot size: 特寫; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## B03（15 秒，2 張）

### B03 f1　→ 存成 `production/LF01/storyboard/frames/B03/f1.png`

8 秒｜大遠景｜Static Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B03/f1-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）

```text
Reference images:
Image 1: a reference board of 8 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-middle): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (top-right): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 4 (middle-left): character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 5 (middle-middle): character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 6 (middle-right): character model sheet of 江祈璟 — this is the person called 江祈璟 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 7 (bottom-left): character model sheet of 聞人澈 — this is the person called 聞人澈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 8 (bottom-middle): character model sheet of 赤瞳妖將 — this is the person called 赤瞳妖將 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 三陣點失控；妖將抬手推飛齊衡烈

Shot (Chinese): 大遠景，橫式 16:9，高位俯視。圓形石庭三個石柱同時噴出暗紅光柱直衝樹冠，中央晶體發紅光；六名年輕修行者分散在石庭四周，赤瞳妖將站在石庭另一端。

Shot size: 大遠景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### B03 f2　→ 存成 `production/LF01/storyboard/frames/B03/f2.png`

7 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B03/f2-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 赤瞳妖將 — this is the person called 赤瞳妖將 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 三陣點失控；妖將抬手推飛齊衡烈

Shot (Chinese): 中景，橫式 16:9。赤瞳妖將只是抬起一隻手，一股無形壓力把舉刀衝上來的齊衡烈連人帶刀往後推飛，地上石板翻起碎片。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## B04（15 秒，2 張）

### B04 f1　→ 存成 `production/LF01/storyboard/frames/B04/f1.png`

7 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B04/f1-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 赤瞳妖將 — this is the person called 赤瞳妖將 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 郁岑燁被看穿；厲若楓的箭被彈開

Shot (Chinese): 中景，橫式 16:9。郁岑燁從側面持劍切入，赤瞳妖將的眼睛已經轉向他，郁岑燁被迫硬生生收劍後撤。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### B04 f2　→ 存成 `production/LF01/storyboard/frames/B04/f2.png`

8 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B04/f2-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 赤瞳妖將 — this is the person called 赤瞳妖將 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 郁岑燁被看穿；厲若楓的箭被彈開

Shot (Chinese): 中景，橫式 16:9。厲若楓站在樹根上拉開三段式短弓連射，箭矢打在赤瞳妖將的鱗甲上被彈開，火星四濺。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## B05（15 秒，2 張）

### B05 f1　→ 存成 `production/LF01/storyboard/frames/B05/f1.png`

7 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B05/f1-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 聞人澈 — this is the person called 聞人澈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 聞人澈法陣；江祈璟與机遙並肩

Shot (Chinese): 中景，橫式 16:9。聞人澈短杖點地，地面展開一圈淡青色曜紋法陣擋住紅色衝擊波，他咬著牙，嘴角滲出一絲血。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### B05 f2　→ 存成 `production/LF01/storyboard/frames/B05/f2.png`

8 秒｜中景｜Static Shot｜台詞：江祈璟：「我左邊，你右邊。」；机遙：「你不是一直不服我？」；江祈璟：「現在不是比這個的時候。」

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B05/f2-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 江祈璟 — this is the person called 江祈璟 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 聞人澈法陣；江祈璟與机遙並肩

Shot (Chinese): 雙人中景，橫式 16:9。江祈璟與机遙背靠背站在石庭中，江祈璟長槍斜指左方，机遙持新生制式長刀面向右方。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## B06（15 秒，2 張）

### B06 f1　→ 存成 `production/LF01/storyboard/frames/B06/f1.png`

7 秒｜中景｜Tracking Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B06/f1-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 江祈璟 — this is the person called 江祈璟 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 兩人衝向兩根光柱

Shot (Chinese): 中景動態，橫式 16:9。江祈璟衝向左側石柱，長槍刺進石柱頂端發光的圓形印紋，槍頭下的小銅鈴甩動。

Shot size: 中景; camera move: Tracking Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### B06 f2　→ 存成 `production/LF01/storyboard/frames/B06/f2.png`

8 秒｜中景｜Tracking Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B06/f2-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface

Blocking (Chinese): 兩人衝向兩根光柱

Shot (Chinese): 中景動態，橫式 16:9。机遙衝向右側石柱，一刀橫斬砍斷暗紅光柱與石柱之間的連結，紅光碎散。

Shot size: 中景; camera move: Tracking Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## B07（15 秒，2 張）

### B07 f1　→ 存成 `production/LF01/storyboard/frames/B07/f1.png`

7 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B07/f1-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface fading

Blocking (Chinese): 第三陣點被壓下；光柱熄滅

Shot (Chinese): 中景，橫式 16:9。郁岑燁和齊衡烈一左一右，劍與刀同時壓在第三根石柱頂端的印紋上，紅光從他們手下往外噴。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### B07 f2　→ 存成 `production/LF01/storyboard/frames/B07/f2.png`

8 秒｜全景｜Static Shot｜台詞：（無台詞）

**上傳：** `production/LF01/art/陣眼遺跡-sheet.png`（待出圖）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the three stone pillars erupt with columns of dark crimson light shooting up through the canopy, the crystal pulsing with a deep red glow from within, a crimson glare seeping through the canopy from the sky above, the mist lit red from below, harsh red rim light on every surface fading

Blocking (Chinese): 第三陣點被壓下；光柱熄滅

Shot (Chinese): 全景，橫式 16:9，空景。三根石柱的暗紅光柱一根一根熄滅，中央晶體的紅光暗下去，紅霧開始變淡。

Shot size: 全景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## B08（15 秒，2 張）

### B08 f1　→ 存成 `production/LF01/storyboard/frames/B08/f1.png`

7 秒｜中景｜Static Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B08/f1-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）

```text
Reference images:
Image 1: a reference board of 3 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (middle-left): character model sheet of 赤瞳妖將 — this is the person called 赤瞳妖將 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the crimson light dying away, cold grey-green forest mist returning

Blocking (Chinese): 妖將收手、看机遙、轉身消失

Shot (Chinese): 過肩中景，橫式 16:9。從机遙肩後看出去，赤瞳妖將放下手，看著熄滅的石柱，然後把目光落在机遙身上。

Shot size: 中景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

### B08 f2　→ 存成 `production/LF01/storyboard/frames/B08/f2.png`

8 秒｜全景｜Static Shot｜台詞：赤瞳妖將：「這裡不是你們該來的地方。」

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B08/f2-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）、本段已完成的 **f1.png**

```text
Reference images:
Image 1: a reference board of 2 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-right): character model sheet of 赤瞳妖將 — this is the person called 赤瞳妖將 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
Image 2: the opening frame of this same sequence — keep the world, lighting, mist and every character's look consistent with it; do not copy its composition.

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: the crimson light dying away, cold grey-green forest mist returning

Blocking (Chinese): 妖將收手、看机遙、轉身消失

Shot (Chinese): 全景，橫式 16:9。赤瞳妖將轉身走入霧中，霧在他身後合攏，只剩破舊的深藍腰帶最後一角沒入白霧。

Shot size: 全景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```

## B09（10 秒，1 張）

### B09 f1　→ 存成 `production/LF01/storyboard/frames/B09/f1.png`

10 秒｜全景｜Static Shot｜台詞：（無台詞）

**上傳：** 參考拼圖 `production/LF01/storyboard/chatgpt/B09/f1-refs.jpg`（**待 陣眼遺跡 設定圖驗收後由 Claude 重跑產生**）

```text
Reference images:
Image 1: a reference board of 7 panels separated by white gaps (it is a reference sheet only — do NOT reproduce the board layout):
  - Panel 1 (top-left): environment sheet of this location — match its architecture, materials, layout and wear exactly; the frame shows the same place
  - Panel 2 (top-middle): character model sheet of 机遙 — this is the person called 机遙 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 3 (top-right): character model sheet of 齊衡烈 — this is the person called 齊衡烈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 4 (middle-left): character model sheet of 郁岑燁 — this is the person called 郁岑燁 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 5 (middle-middle): character model sheet of 厲若楓 — this is the person called 厲若楓 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 6 (middle-right): character model sheet of 江祈璟 — this is the person called 江祈璟 in the shot text; match the SAME face, hairstyle, costume and weapon exactly
  - Panel 7 (bottom-left): character model sheet of 聞人澈 — this is the person called 聞人澈 in the shot text; match the SAME face, hairstyle, costume and weapon exactly

Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, a 3D scan or a costume-drama still. Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, warm gold accents, small touches of vermilion. All characters are adults and fully clothed. Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, natural shadows; everything exists inside the world, never a studio backdrop.

Lighting: cold grey-green forest mist, late afternoon, quiet

Blocking (Chinese): 所有人站在原地

Shot (Chinese): 全景，橫式 16:9。石庭恢復灰暗安靜，六名年輕修行者站在原地沒有人說話；齊衡烈握刀的手還在發抖，聞人澈扶著短杖喘氣。

Shot size: 全景; camera move: Static Shot (draw the opening moment of the shot).

Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the shot text appear, plus any background students or animals the shot text explicitly mentions. No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.
```
