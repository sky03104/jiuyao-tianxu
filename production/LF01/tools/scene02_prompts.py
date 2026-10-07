"""
第 2 場（第七室報到）Gemini 出圖提示詞：3 張 3×3 故事版（S02-1～3）＋鏡頭表的 2 張近景（T07 裴含章、T08 机遙）。

讀 ../lf01.json（storyboardBoards、A 段）與 ../scene02_shots.json（new 欄位），沿用 lf01_handoff.py 的畫風規則與拼圖工具，產生：
  - docs/HANDOFF-012_LF01_SCENE02_GEMINI.md
  - storyboard/chatgpt/boards/S02-N-refs.jpg、storyboard/chatgpt/scene02-new/<鏡號>-refs.jpg（參考拼圖）
存檔位置：故事版 storyboard/boards/S02-N.png；近景 storyboard/frames/new/<鏡號>.png。

用法：LF01_BRANCH=<分支> python3 production/LF01/tools/scene02_prompts.py
"""
import os

import lf01_handoff as h

LF, g, data = h.LF, h.g, h.data
SEG = {s["id"]: s for s in data["segments"]}
OUT_DIR = os.path.join(LF, "storyboard", "chatgpt", "scene02-new")
DOC = os.path.join(h.REPO, "docs", "HANDOFF-012_LF01_SCENE02_GEMINI.md")

# 鏡號 → (光線所屬段落, 角色, 英文畫面描述)。T06 過肩鏡是從裴含章肩後看机遙，所以裴含章朝畫面右、机遙朝畫面左。
CLOSEUPS = {
    "T07": ("A03", ["C10"], "Medium close-up of 裴含章 (a WOMAN in her thirties) seated behind the wooden registration desk, "
            "three-quarter view facing screen-RIGHT (body and head turned toward the RIGHT edge of the frame), head bowed: "
            "she is turning a page of a thick bound register with one hand and pressing a small bronze seal onto the page with "
            "the other, eyelids lowered, NOT looking up; perfectly flat, unbothered, slightly tired expression. Dark steward "
            "uniform, a writing brush clipped at her cuff, hair in a neat bun with not one strand loose. The register pages show "
            "only illegible scribble lines, no readable characters. Background: blurred line of new students in morning light."),
    "T08": ("A03", ["C01"], "Medium close-up of 机遙 standing in front of the registration desk on his first day, three-quarter "
            "view facing screen-LEFT (head and eyes turned toward the LEFT edge of the frame, toward the steward off-screen left): "
            "caught off guard by an unfamiliar word — eyebrows slightly raised, lips just parted as if repeating the word, a little "
            "stiff and polite; both hands holding the strap of the rolled cloth bundle on his shoulder. No weapon. Background: "
            "blurred courtyard and other students in clear morning light."),
}
# 已出圖但不合格、要單格重出的（修正寫在 lf01.json 該格的 layout）
# (段號, 第幾格, 為什麼重出)；2026-10-07 的 A01 f2、A02 f1 已重出通過，清空。驗圖紀錄見 ../README.md
REDO = []
LIPSYNC = ("The face is large, sharp and clearly visible (this shot will be used for lip-sync), no hands or objects covering "
           "the mouth, no motion blur on the face.")


def closeup_job(sid, segid, chars, desc):
    seg = SEG[segid]
    refs = [h.scene_sheet(seg["scene"])] + [h.char_sheet(c) for c in chars]
    descs = ["environment sheet of this location — the background must match its materials and mood"] + [
        f"character model sheet of {h.char_name(c)} — the person called {h.char_name(c)} in the shot text; match the SAME "
        "face, hairstyle, costume and weapon exactly" for c in chars]
    must = "".join(f"{h.char_name(c)} MUST look like this: {data['mustLook'][c]}\n\n"
                   for c in chars if c in data.get("mustLook", {}))
    body = (f"{g.STYLE_RENDER} {g.STYLE_WORLD}\n\nLighting: {h.light_of(seg)}\n\nShot: {desc}\n\n{must}{LIPSYNC}\n\n"
            "Horizontal 16:9 widescreen cinematic frame. Only this one character appears in focus. "
            "No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.")
    return {"id": sid, "refs": refs, "descs": descs, "body": body,
            "out": os.path.join(LF, "storyboard", "frames", "new", f"{sid}.png"),
            "board": os.path.join(OUT_DIR, f"{sid}-refs.jpg")}


def main():
    os.makedirs(OUT_DIR, exist_ok=True)
    boards = [b for b in h.board_jobs() if b["id"].startswith("S02-")]
    closeups = [closeup_job(k, *v) for k, v in CLOSEUPS.items()]
    L = ["# 《九曜：天墟》HANDOFF-012　第 2 場故事版＋近景（Gemini）", "",
         "> 由 `production/LF01/tools/scene02_prompts.py` 產生；鏡頭表見 `production/LF01/storyboard/scene02_shotlist_v1.md`。",
         "> Gemini 用 **Pro**、**每張開新對話**、只附該張的參考拼圖；出完傳回 Claude 驗圖。",
         "> 故事版是 3×3 九格一張，用來先驗構圖與角色一致性；通過後才一格一格出正式分鏡圖。", ""]
    for b in boards:
        ups, lines = h.board_or_single(b["refs"], b["descs"], b["board"])
        segs = "、".join(dict.fromkeys(f"{seg['id']} f{k}" for seg, k, _ in b["cuts"]))
        L += [f"## {b['id']}（{segs}）　→ 存成 `{h.rel(b['out'])}`", "", "**上傳：** " + "、".join(ups), "",
              "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + b["body"], "```", ""]
    for j in closeups:
        ups, lines = h.board_or_single(j["refs"], j["descs"], j["board"])
        L += [f"## {j['id']}（近景）　→ 存成 `{h.rel(j['out'])}`", "", "**上傳：** " + "、".join(ups), "",
              "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"], "```", ""]
    redo = {(a, b): why for a, b, why in REDO}
    for j in h.frame_jobs():
        why = redo.get((j["seg"]["id"], j["k"]))
        if not why:
            continue
        ups, lines = h.board_or_single(j["refs"], j["descs"], j["board"])
        L += [f"## {j['seg']['id']} f{j['k']}（單格重出：{why}）　→ 存成 `{h.rel(j['out'])}`", "", "**上傳：** " + "、".join(ups), "",
              "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"], "```", ""]
    # 故事版已通過、單格還沒出的：附該格裁切當構圖參考
    todo = [j for j in h.frame_jobs() if j["panel"] and not os.path.exists(j["out"])]
    if todo:
        L += ["# 單格正式分鏡圖（故事版已通過的格子）", "",
              "> 每張開新對話；參考拼圖最後一格是故事版裁下來的那一格，照它的構圖、但畫質要做到正式分鏡圖。", ""]
    for j in todo:
        refs = j["refs"] + [j["panel"]]
        descs = j["descs"] + ["the approved storyboard panel for THIS shot — follow its framing, camera angle and staging, "
                              "but render at full finished quality; ignore any small label text in its corner"]
        board = j["board"].replace(".jpg", "-panel.jpg")
        ups, lines = h.board_or_single(refs, descs, board)
        L += [f"## {j['seg']['id']} f{j['k']}　→ 存成 `{h.rel(j['out'])}`", "", "**上傳：** " + "、".join(ups), "",
              "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"], "```", ""]
    open(DOC, "w", encoding="utf-8").write("\n".join(L))
    print(f"✓ {h.rel(DOC)}：故事版 {len(boards)} 張、近景 {len(closeups)} 張")


if __name__ == "__main__":
    main()
