"""
第 12 場鏡頭表 v2 需要的新圖（近景 5 張＋厲若楓女版重出 2 張）的 Gemini 提示詞與參考拼圖。

讀 ../scene12_shots.json 的 new 欄位與 ../lf01.json，沿用 lf01_handoff.py 的畫風規則、光線與拼圖工具，產生：
  - docs/HANDOFF-011_LF01_SCENE12_NEWFRAMES.md
  - storyboard/chatgpt/scene12-new/<鏡號>-refs.jpg（參考拼圖）
存檔位置：近景存 storyboard/frames/new/<鏡號>.png；重出的存回 storyboard/frames/B04/f2.png、B09/f1.png（舊圖移到 _rejected/）。

用法：LF01_BRANCH=<分支> python3 production/LF01/tools/scene12_newframes.py
"""
import json
import os

import lf01_handoff as h

LF, g, data = h.LF, h.g, h.data
SEG = {s["id"]: s for s in data["segments"]}
OUT_DIR = os.path.join(LF, "storyboard", "chatgpt", "scene12-new")
DOC = os.path.join(h.REPO, "docs", "HANDOFF-011_LF01_SCENE12_NEWFRAMES.md")

# 鏡號 → (光線所屬段落, 角色, 英文畫面描述)
CLOSEUPS = {
    "S05": ("B01", ["C03"], "Medium close-up of 郁岑燁, three-quarter view facing screen-RIGHT (head and eyes turned toward the RIGHT edge of the frame), standing in drifting red mist; "
            "his straight sword half drawn from the scabbard at his hip, brows knitted tight, eyes fixed on the depth of the fog. "
            "Background: blurred twisted tree roots and red fog of the ancient forest ruins."),
    "S06": ("B01", ["C06"], "Medium close-up of 江祈璟, three-quarter view facing screen-RIGHT (head and eyes turned toward the RIGHT edge of the frame), staring into the depth of the red fog; "
            "jaw clenched, high ponytail with the silver hair ring, the long spear held upright beside him with its tip slightly "
            "raised and the small bronze bell visible under the spearhead. Background: blurred red fog and tree roots."),
    "S07": ("B01", ["C04"], "Medium close-up of 厲若楓 (a young WOMAN), three-quarter view facing screen-RIGHT (head and eyes turned toward the RIGHT edge of the frame), her three-section "
            "folding short bow drawn halfway, arrow nocked, amber eyes unblinking and focused on the fog; low ponytail, dark-teal "
            "cape with hood down. Background: blurred red fog and tree roots."),
    "S09": ("B02", ["C02"], "Close-up of 齊衡烈 from the chest up, front three-quarter view: his right hand grips the hilt of his "
            "heavy single-edged saber and is visibly trembling; his left hand presses down on it to stop the shaking; he glances "
            "down at his hands with a forced, nervous grin, a bead of sweat on his temple. Red-tinted fog behind him."),
    "S12": ("B02", ["C01"], "Close-up of 机遙 from the chest up, three-quarter view facing screen-RIGHT (head and eyes turned toward the RIGHT edge of the frame): startled and suspicious, "
            "eyes wide, a bead of sweat at his temple, lips slightly parted as if about to speak, looking toward something huge "
            "off-screen right. Red-tinted fog and blurred stone pillars behind him."),
}
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
            "Horizontal 16:9 widescreen cinematic frame. Only this one character appears. "
            "No text, no subtitles, no watermark, no borders — a single clean full-bleed frame.")
    return {"id": sid, "refs": refs, "descs": descs, "body": body,
            "out": os.path.join(LF, "storyboard", "frames", "new", f"{sid}.png"),
            "board": os.path.join(OUT_DIR, f"{sid}-refs.jpg")}


def main():
    os.makedirs(OUT_DIR, exist_ok=True)
    jobs = [closeup_job(k, *v) for k, v in CLOSEUPS.items()]
    redo = [j for j in h.frame_jobs() if (j["seg"]["id"], j["k"]) in {("B04", 2), ("B09", 1)}]
    L = ["# 《九曜：天墟》HANDOFF-011　第 12 場鏡頭表 v2 新圖（Gemini）", "",
         "> 由 `production/LF01/tools/scene12_newframes.py` 產生；鏡頭表見 `production/LF01/storyboard/scene12_shotlist_v2.md`。",
         "> Gemini 用 **Pro**、**每張開新對話**、只附該張的參考拼圖；出完傳回 Claude 驗圖。", ""]
    for j in jobs:
        ups, lines = h.board_or_single(j["refs"], j["descs"], j["board"])
        L += [f"## {j['id']}　→ 存成 `{h.rel(j['out'])}`", "", "**上傳：** " + "、".join(ups), "",
              "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"], "```", ""]
    for j in redo:
        ups, lines = h.board_or_single(j["refs"], j["descs"], j["board"])
        L += [f"## {j['seg']['id']} f{j['k']}（重出：厲若楓改女版）　→ 存成 `{h.rel(j['out'])}`（舊圖移 `_rejected/`）", "",
              "**上傳：** " + "、".join(ups), "",
              "```text", "Reference images:\n" + "\n".join(lines) + "\n\n" + j["body"], "```", ""]
    open(DOC, "w", encoding="utf-8").write("\n".join(L))
    print(f"✓ {h.rel(DOC)}：近景 {len(jobs)} 張、重出 {len(redo)} 張")


if __name__ == "__main__":
    main()
