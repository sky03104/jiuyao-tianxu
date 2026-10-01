"""
長篇動畫第一集試片（LF01）出圖交接檔產生器。

讀 production/LF01/lf01.json，沿用 EP01 的畫風規則（STYLE LOCK）與參考拼圖工具，產生：
  - 新設定圖 4 張（江祈璟、聞人澈、赤瞳妖將、陣眼遺跡）的提示詞，掛已定稿設定圖當畫風參考
  - 故事版（多格板，lf01.json 的 storyboardBoards）提示詞與參考拼圖；故事版驗過後存 storyboard/boards/<id>.png，
    本腳本會把每一格裁成 storyboard/boards/<id>/pN.jpg，單格出圖時附上當構圖參考
  - 分鏡圖（橫式 16:9）提示詞與參考拼圖（storyboard/chatgpt/<段號>/fN-refs.jpg）
  - docs/HANDOFF-010_LF01_LONGFORM_TEST.md（給 ChatGPT 網頁版照順序貼）

用法：python3 production/LF01/tools/lf01_handoff.py
需要 Pillow（拼參考圖）。
"""
import json
import os
import sys
from urllib.parse import quote

HERE = os.path.dirname(os.path.abspath(__file__))
LF = os.path.dirname(HERE)                                   # production/LF01
REPO = os.path.dirname(os.path.dirname(LF))
EP01 = os.path.join(REPO, "production", "EP01", "shuohao")
sys.path.insert(0, os.path.join(EP01, "imagegen"))
import gen_images as g  # noqa: E402  沿用 STYLE_RENDER／STYLE_WORLD／FIX／SHEET_RULE／NO_TEXT／make_board

BRANCH = os.environ.get("LF01_BRANCH", "main")
RAW = f"https://raw.githubusercontent.com/sky03104/jiuyao-tianxu/{BRANCH}/"
TREE = f"https://github.com/sky03104/jiuyao-tianxu/tree/{BRANCH}/"
HANDOFF = os.path.join(REPO, "docs", "HANDOFF-010_LF01_LONGFORM_TEST.md")
FRAME_SIZE = "1536x1024"

data = json.load(open(os.path.join(LF, "lf01.json"), encoding="utf-8"))
reuse = data["reuse"]
new_chars = {c["id"]: c for c in data["newCharacters"]}
new_scenes = {s["id"]: s for s in data["newScenes"]}
display = reuse.get("displayName", {})


def rel(p):
    return os.path.relpath(p, REPO)


def link(p):
    return f"[{os.path.basename(p)}]({RAW}{quote(rel(p))})"


def char_sheet(cid):
    if cid in reuse["characters"]:
        return g.char_sheet_path(reuse["characters"][cid])
    return os.path.join(LF, "characters", f"{new_chars[cid]['name']}-sheet.png")


def scene_sheet(sid):
    if sid in reuse["scenes"]:
        return g.art_sheet_path(reuse["scenes"][sid])
    return os.path.join(LF, "art", f"{new_scenes[sid]['name']}-sheet.png")


def prop_sheet(pid):
    return g.art_sheet_path(reuse["props"][pid])


def char_name(cid):
    return display.get(cid) or reuse["characters"].get(cid) or new_chars[cid]["name"]


def board_or_single(paths, descs, out):
    """一張就直接上傳；多張拼成參考拼圖（ChatGPT 一次只能上傳 2 張圖）。"""
    if len(paths) == 1:
        up = link(paths[0]) if os.path.exists(paths[0]) else f"`{rel(paths[0])}`（待出圖）"
        return [up], [f"Image 1: {descs[0]}"]
    missing = [x for x in paths if not os.path.exists(x)]
    if missing:  # 新設定圖還沒出：提示詞照寫，拼圖等設定圖驗收後重跑本腳本產生
        cols = 2 if len(paths) <= 4 else 3
        rows = (len(paths) + cols - 1) // cols
        wait = "、".join(os.path.basename(x).replace("-sheet.png", "") for x in missing)
        up = [f"參考拼圖 `{rel(out)}`（**待 {wait} 設定圖驗收後由 Claude 重跑產生**）"]
    else:
        cols, rows = g.make_board(paths, out)
        up = [f"[參考拼圖 {os.path.basename(out)}]({RAW}{quote(rel(out))})（{len(paths)} 張拼成一張）"]
    names = ["top", "middle", "bottom"][:rows] if rows <= 3 else [f"row {r + 1}" for r in range(rows)]
    xs = ["left", "middle", "right"] if cols == 3 else ["left", "right"]
    lines = [f"Image 1: a reference board of {len(paths)} panels separated by white gaps "
             "(it is a reference sheet only — do NOT reproduce the board layout):"]
    for i, d in enumerate(descs):
        lines.append(f"  - Panel {i + 1} ({names[i // cols]}-{xs[i % cols]}): {d}")
    return up, lines


# ---------- 新設定圖 ----------
def char_sheet_text(c):
    creature = c.get("creature")
    who = "The figure is " + c["figure"] + "."
    return (
        "Single character model sheet on ONE 16:9 landscape canvas (widescreen, width to height exactly 16:9). "
        "The canvas is divided into three zones by thin hairline rules. LEFT ZONE — a vertical column occupying about "
        "34% of the canvas width: one bust portrait, head and shoulders, front-facing, centred; both shoulders fully "
        "visible; the portrait ends in a clean straight horizontal cut just below the chest. The bust shows "
        f"{c['bust']}. LIGHTING IN THE LEFT ZONE ONLY: soft directional key light from the upper left with gentle "
        "falloff and subtle ambient occlusion. RIGHT-TOP ZONE: three FULL-BODY views of the SAME character standing "
        "side by side — front view, left side profile, back view — on one shared ground line, identical height and "
        "proportions, head to toe with clear margins, neutral relaxed standing posture. "
        f"{who} "
        + ("Show the true towering scale by keeping the figure alone and complete within the zone. " if creature else "")
        + "The faces on all three full-body views match the bust portrait exactly. LIGHTING IN THE RIGHT ZONES: flat "
        "even orthographic lighting, no cast shadows. RIGHT-BOTTOM ZONE: a row of four to five small isolated close-up "
        f"studies: {'; '.join(c['details'])}. Never shrink the full-body figures to make room. Plain pure white "
        f"background (#FFFFFF) throughout. {c['palette']}, readable silhouette from every angle."
    )


def scene_sheet_text(s):
    return (
        "Environment reference sheet on ONE 16:9 landscape canvas, panels separated by thin hairline rules. MAIN ZONE, "
        f"top-left, about 72% of the canvas width and 70% of its height: the master establishing view: {s['master']} — "
        "the canonical look of this location. RIGHT COLUMN and BOTTOM ROW: small close-up detail studies of "
        f"{'; '.join(s['details'])}. Every detail panel is a close-up crop of the SAME space shown in the master view. "
        "Absolutely no people anywhere on the sheet. Lighting in all panels: "
        f"{s['lighting']['午後林光']}."
    )


def sheet_jobs():
    jobs = []
    for c in data["newCharacters"]:
        refs = [char_sheet(r) for r in c["styleRefs"]]
        descs = [f"approved character sheet of a DIFFERENT character ({reuse['characters'][r]}) — use it ONLY as the "
                 "rendering-style and sheet-layout reference; do not copy the face, hair, costume or weapon"
                 for r in c["styleRefs"]]
        body = (f"{g.STYLE_RENDER}\n\n{char_sheet_text(c)}\n\n{g.SHEET_RULE}\n\n"
                f"Avoid: {c['negative']}. {g.NO_TEXT}")
        jobs.append({"key": c["name"], "out": char_sheet(c["id"]), "refs": refs, "descs": descs, "body": body})
    for s in data["newScenes"]:
        refs = [scene_sheet(r) for r in s["styleRefs"]]
        descs = ["approved environment sheet of a DIFFERENT location — use it ONLY as the rendering-style, "
                 "material and sheet-layout reference; do not copy its architecture"] * len(refs)
        body = (f"{g.STYLE_RENDER} {g.STYLE_WORLD}\n\n{scene_sheet_text(s)}\n\n"
                f"Avoid: {s['negative']}. {g.NO_TEXT}")
        jobs.append({"key": s["name"], "out": scene_sheet(s["id"]), "refs": refs, "descs": descs, "body": body})
    return jobs


# ---------- 故事版（多格板） ----------
BOARDS = os.path.join(LF, "storyboard", "boards")
POS3 = {1: ["only"], 2: ["left", "right"], 3: ["left", "middle", "right"], 4: ["far-left", "left-centre", "right-centre", "far-right"]}
ROW3 = {1: ["only"], 2: ["top", "bottom"], 3: ["top", "middle", "bottom"]}


def board_jobs():
    """依 storyboardBoards 把同一場的切依序每 perBoard 格分成一張故事版。回傳 [{id, cuts:[(seg, k, cut)], ...}]。"""
    jobs = []
    for b in data.get("storyboardBoards", []):
        cuts = [(seg, k, cut) for seg in data["segments"] if seg["id"].startswith(b["segPrefix"])
                for k, cut in enumerate(seg["cuts"], start=1)]
        for n in range(0, len(cuts), b["perBoard"]):
            part = cuts[n:n + b["perBoard"]]
            bid = f"{b['id']}-{n // b['perBoard'] + 1}"
            scenes, chars, props = [], [], []
            for seg, _, cut in part:
                scenes += [seg["scene"]] if seg["scene"] not in scenes else []
                chars += [c for c in cut["chars"] if c not in chars]
                props += [x for x in cut["props"] if x not in props]
            refs = [scene_sheet(x) for x in scenes] + [char_sheet(c) for c in chars] + [prop_sheet(x) for x in props]
            descs = (["environment sheet of a location used in this sequence — match its architecture and materials"] * len(scenes)
                     + [f"character model sheet of {char_name(c)} — the person called {char_name(c)} in the panel text; "
                        "same face, hairstyle, costume and weapon in every panel" for c in chars]
                     + [f"prop sheet of {reuse['props'][x]}" for x in props])
            cols, rows = b["cols"], b["rows"]
            panels = []
            for i, (seg, k, cut) in enumerate(part):
                where = f"{ROW3[rows][i // cols]} row, {POS3[cols][i % cols]}"
                who = "、".join(char_name(c) for c in cut["chars"]) or "no people"
                panels.append(
                    f"SHOT {i + 1} (grid cell: {where}) — {cut['size']}, {cut['camera']} (opening moment). Characters: {who}. "
                    f"Lighting: {light_of(seg)}. Shot (Chinese): {cut['frame']}"
                    + (f" LAYOUT: {cut['layout']}" if cut.get("layout") else "")
                    + "".join(f" {char_name(c)} MUST look like this: {data['mustLook'][c]}"
                              for c in cut["chars"] if c in data.get("mustLook", {})))
            body = (
                f"{g.STYLE_RENDER} {g.STYLE_WORLD}\n\n"
                f"STORYBOARD SHEET for one continuous sequence. ONE 16:9 landscape canvas divided into an exact grid of "
                f"{cols} columns x {rows} rows of equal 16:9 panels, separated by thin pure-white gutters, no outer border. "
                f"Read left to right, top to bottom; SHOT 1 fills the first cell. {len(part)} cells are used"
                + (f"; the remaining {cols * rows - len(part)} grid cell(s) at the end stay plain white" if len(part) < cols * rows else "")
                + ". Each panel is a finished colour frame in the same rendering style as the reference sheets (simpler detail "
                "is fine, but NOT a pencil sketch). The same character must look identical in every panel — face, hair, "
                "costume, weapon — and match the character sheets; the location stays consistent across panels. Vary the "
                "shot sizes exactly as listed. No text, no panel numbers, no captions, no speech bubbles, no arrows.\n\n"
                + "\n\n".join(panels))
            jobs.append({"id": bid, "cuts": part, "refs": refs, "descs": descs, "body": body, "cols": cols, "rows": rows,
                         "out": os.path.join(BOARDS, f"{bid}.png"),
                         "board": os.path.join(LF, "storyboard", "chatgpt", "boards", f"{bid}-refs.jpg")})
    return jobs


def panel_of():
    """(段號, k) → 已驗過故事版的那一格裁切檔；故事版存在才裁。"""
    from PIL import Image
    out = {}
    for b in board_jobs():
        if not os.path.exists(b["out"]):
            continue
        im = Image.open(b["out"]).convert("RGB")
        W, H = im.size
        pw, ph = W / b["cols"], H / b["rows"]
        os.makedirs(os.path.join(BOARDS, b["id"]), exist_ok=True)
        for i, (seg, k, _) in enumerate(b["cuts"]):
            r, c = divmod(i, b["cols"])
            box = (int(c * pw), int(r * ph), int((c + 1) * pw), int((r + 1) * ph))
            dst = os.path.join(BOARDS, b["id"], f"p{i + 1}.jpg")
            im.crop(box).save(dst, quality=92)
            out[(seg["id"], k)] = dst
    return out


# ---------- 分鏡圖 ----------
def light_of(seg):
    s = new_scenes.get(seg["scene"])
    if s and seg["light"] in s["lighting"]:
        return s["lighting"][seg["light"]]
    if s and seg["light"].startswith("暗紅異變"):
        return s["lighting"]["暗紅異變"] + seg["light"].replace("暗紅異變", "")
    return seg["light"]


def frame_jobs():
    jobs = []
    panels = panel_of()
    for seg in data["segments"]:
        seg_dir = os.path.join(LF, "storyboard", "frames", seg["id"])
        for k, cut in enumerate(seg["cuts"], start=1):
            refs = [scene_sheet(seg["scene"])]
            descs = ["environment sheet of this location — match its architecture, materials, layout and wear exactly; "
                     "the frame shows the same place"]
            for cid in cut["chars"]:
                refs.append(char_sheet(cid))
                n = char_name(cid)
                descs.append(f"character model sheet of {n} — this is the person called {n} in the shot text; match "
                             "the SAME face, hairstyle, costume and weapon exactly")
            for pid in cut["props"]:
                refs.append(prop_sheet(pid))
                descs.append(f"prop sheet of {reuse['props'][pid]} — match this object exactly; any plaque or token "
                             "surface stays blank")
            body = (
                f"{g.STYLE_RENDER} {g.STYLE_WORLD}\n\n"
                f"Lighting: {light_of(seg)}\n\n"
                f"Blocking (Chinese): {seg['blocking']}\n\n"
                f"Shot (Chinese): {cut['frame']}\n\n"
                + (f"LAYOUT (must follow): {cut['layout']}\n\n" if cut.get("layout") else "")
                + "".join(f"{char_name(c)} MUST look like this: {data['mustLook'][c]}\n\n"
                          for c in cut["chars"] if c in data.get("mustLook", {}))
                + f"Shot size: {cut['size']}; camera move: {cut['camera']} (draw the opening moment of the shot).\n\n"
                "Horizontal 16:9 widescreen cinematic frame (1536x1024 is acceptable). Only the characters named in the "
                "shot text appear, plus any background students or animals the shot text explicitly mentions. "
                "No text, no subtitles, no watermark, no borders — a single clean full-bleed frame."
            )
            out_board = os.path.join(LF, "storyboard", "chatgpt", seg["id"], f"f{k}-refs.jpg")
            jobs.append({"seg": seg, "k": k, "cut": cut, "out": os.path.join(seg_dir, f"f{k}.png"),
                         "refs": refs, "descs": descs, "body": body, "board": out_board,
                         "panel": panels.get((seg["id"], k))})
    return jobs


def write_handoff():
    sj, fj = sheet_jobs(), frame_jobs()
    L = []
    L += ["# 第一部分：新設定圖（4 張，先做）", "",
          "新角色與新場景沒有 GPT 參考稿，這裡附上**已定稿的其他設定圖當畫風參考**（提示詞裡已註明「只參考畫風，不抄外型」）。",
          f"做完存成下列檔名，上傳到 [{TREE}production/LF01]({TREE}production/LF01) 對應資料夾，或直接貼回 Claude 的 session。", ""]
    for j in sj:
        ups, lines = board_or_single(j["refs"], j["descs"], os.path.join(LF, "storyboard", "chatgpt", "sheets",
                                                                          f"{j['key']}-refs.jpg"))
        L += [f"## {j['key']}　→ 存成 `{rel(j['out'])}`", "", "**上傳：** " + "、".join(ups), "",
              "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"], "```", ""]
    bj = board_jobs()
    if bj:
        L += ["# 故事版（多格板）：出單格之前先做", "",
              "每張故事版把同一場連續 9 個鏡頭畫在一張 3×3 圖裡，**先確認構圖、角色位置、連戲**，再出單格。"
              "Claude 驗過後存進 repo、重跑本腳本，單格提示詞會自動多附「故事版這一格」當構圖參考。",
              "Gemini 用 **Pro**、每張故事版開新對話；只附下面的參考拼圖。", ""]
        for b in bj:
            ups, lines = board_or_single(b["refs"], b["descs"], b["board"])
            span = f"{b['cuts'][0][0]['id']} f{b['cuts'][0][1]}～{b['cuts'][-1][0]['id']} f{b['cuts'][-1][1]}"
            L += [f"## 故事版 {b['id']}（{span}，{len(b['cuts'])} 格）　→ 存成 `{rel(b['out'])}`", "",
                  "**上傳：** " + "、".join(ups), "",
                  "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + b["body"], "```", ""]
    cur = None
    for j in fj:
        seg = j["seg"]
        if seg["id"] != cur:
            cur = seg["id"]
            part = "第二部分：第 12 場　赤瞳妖將（先做這段）" if cur == "B01" else (
                "第三部分：第 2 場　第七室報到" if cur == "A01" else None)
            if part:
                L += [f"# {part}", ""]
            secs = sum(c["sec"] for c in seg["cuts"])
            L += [f"## {seg['id']}（{secs} 秒，{len(seg['cuts'])} 張）", ""]
        cut = j["cut"]
        ups, lines = board_or_single(j["refs"], j["descs"], j["board"])
        if j["panel"]:
            ups.append(f"故事版這一格 {link(j['panel'])}")
            lines.append(f"Image {len(lines) - sum(1 for x in lines if x.startswith('  -')) + 1}: the approved storyboard "
                         "panel of THIS shot — follow its framing, camera angle, character positions and poses closely, "
                         "but render it as a full-quality frame matching the reference sheets exactly.")
        if j["k"] > 1:
            ups.append("本段已完成的 **f1.png**")
            lines.append(f"Image {len(lines) - sum(1 for x in lines if x.startswith('  -')) + 1}: the opening frame of "
                         "this same sequence — keep the world, lighting and every character's look consistent with it; "
                         "do not copy its composition.")
        say = "；".join(f"{w}：「{t}」" for w, t in cut["lines"]) or "（無台詞）"
        L += [f"### {seg['id']} f{j['k']}　→ 存成 `{rel(j['out'])}`", "",
              f"{cut['sec']} 秒｜{cut['size']}｜{cut['camera']}｜台詞：{say}", "",
              "**上傳：** " + "、".join(ups), "",
              "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"], "```", ""]
    n_b = sum(1 for j in fj if j["seg"]["id"].startswith("B"))
    n_a = len(fj) - n_b
    head = f"""# 《九曜：天墟》HANDOFF-010

## 長篇動畫第一集試片：第 2 場＋第 12 場分鏡圖（ChatGPT 網頁版）

**日期：2026-09-27**
**狀態：READY TO EXECUTE**
**執行者：咖哩（ChatGPT 網頁版）；出完貼回 Claude 驗圖**
**來源：`docs/43` 第 2、12 場；分鏡資料 `production/LF01/lf01.json`**

> 本檔由 `production/LF01/tools/lf01_handoff.py` 自動產生。分鏡或設定圖改了就重跑，不要手改提示詞。
> 連結指向 `{BRANCH}` 分支。

# 0. 建議做法：用 Codex 自動跑完（不用一張一張停）

一般 ChatGPT 對話的 GitHub 連接器是唯讀，存不回 repo。要「出一張、自己存、接著出下一張」請用 **Codex**，
開一個 Codex 任務（repo：`sky03104/jiuyao-tianxu`，分支：`{BRANCH}`），貼這一句：

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
| 一 | 新設定圖：江祈璟、聞人澈、赤瞳妖將、陣眼遺跡 | {len(sj)} |
| 二 | 第 12 場　赤瞳妖將（最難，先做） | {n_b} |
| 三 | 第 2 場　第七室報到（沿用 EP01 設定圖） | {n_a} |

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

"""
    with open(HANDOFF, "w", encoding="utf-8") as f:
        f.write(head + "\n".join(L))
    print(f"✓ {rel(HANDOFF)}：設定圖 {len(sj)} 張、分鏡 {len(fj)} 張（第 12 場 {n_b}、第 2 場 {n_a}）")


# ---------- Codex 自動出圖佇列 ----------
QUEUE = os.path.join(LF, "codex_jobs.json")


def write_codex_queue():
    """給 Codex（可讀寫 repo、內建 $imagegen）照順序自動出圖用：參考圖直接給 repo 路徑，不需要拼圖。"""
    jobs = []
    for j in sheet_jobs():
        lines = [f"Image {i + 1}: {rel(r)} — {d}" for i, (r, d) in enumerate(zip(j["refs"], j["descs"]))]
        jobs.append({"id": "sheet:" + j["key"], "phase": 1, "kind": "sheet", "out": rel(j["out"]),
                     "size": "1536x1024", "refs": [rel(r) for r in j["refs"]], "after": [],
                     "prompt": "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"]})
    sheet_out = {rel(j["out"]) for j in sheet_jobs()}
    for j in frame_jobs():
        seg, k = j["seg"], j["k"]
        refs = list(j["refs"])
        descs = list(j["descs"])
        if k > 1:
            refs.append(os.path.join(LF, "storyboard", "frames", seg["id"], "f1.png"))
            descs.append("the opening frame of this same sequence — keep the world, lighting, mist and every "
                         "character's look consistent with it; do not copy its composition")
        lines = [f"Image {i + 1}: {rel(r)} — {d}" for i, (r, d) in enumerate(zip(refs, descs))]
        rrefs = [rel(r) for r in refs]
        jobs.append({"id": f"{seg['id']}-f{k}", "phase": 2 if seg["id"].startswith("B") else 3, "kind": "frame",
                     "out": rel(j["out"]), "size": FRAME_SIZE, "refs": rrefs,
                     "after": [r for r in rrefs if r in sheet_out or r.endswith("/f1.png")],
                     "seconds": j["cut"]["sec"], "lines": j["cut"]["lines"],
                     "prompt": "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"]})
    jobs.sort(key=lambda x: x["phase"])
    with open(QUEUE, "w", encoding="utf-8") as f:
        json.dump({"_how": "見 production/LF01/CODEX_RUNBOOK.md；依陣列順序處理，out 已存在就跳過", "jobs": jobs},
                  f, ensure_ascii=False, indent=1)
    print(f"✓ {rel(QUEUE)}：{len(jobs)} 個工作")


if __name__ == "__main__":
    write_handoff()
    write_codex_queue()
