#!/usr/bin/env python3
"""長篇全系列：劇本／片段表檢查與 Seedance 提示詞產生。規格見 ../README.md。

  python3 production/series/tools/series.py check EP01        # 檢查 clips.json 與劇本（有錯誤就 exit 1）
  python3 production/series/tools/series.py build EP01        # 產生 EP01/seedance_prompts.md
  python3 production/series/tools/series.py assets            # 產生 ASSETS_TODO.md（還沒出的設定圖）
  python3 production/series/tools/series.py all               # 全部集數 check＋build＋assets
"""
import difflib
import glob
import json
import os
import re
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
SERIES = os.path.dirname(HERE)
REPO = os.path.dirname(os.path.dirname(SERIES))
REG = json.load(open(os.path.join(SERIES, "registry.json"), encoding="utf-8"))
CH, LOC = REG["characters"], REG["locations"]
PARTS = {"cold", "main", "op", "ed"}
MAX_CPS = 4.5           # 一個 beat 內台詞字數／秒數上限
RUNTIME = (930, 990)    # 正片（cold＋main）秒數
CONTACT = ["撞上", "撞到", "擁抱", "抱住", "抱起", "拍肩", "拍了拍他", "拍了拍她", "握手", "牽著", "牽起", "扶起", "扶住", "揹起", "背起他", "背起她", "攙扶"]
PUNCT = re.compile(r"[\s，。？！、；：…—「」『』（）,.?!;:~～\-·]")


def clips_path(ep):
    return os.path.join(SERIES, ep, "clips.json")


def script_path(ep):
    hits = glob.glob(os.path.join(REPO, "docs", "longform", f"{ep}_SCRIPT*.md"))
    return hits[0] if hits else None


def base_name(spk):
    return re.sub(r"（.*?）|\(.*?\)", "", spk).strip()


def offscreen(spk):
    return bool(re.search(r"（.*?）", spk))


def script_lines(path):
    """劇本裡正片的台詞（跳過前情提要、下集預告、旁白、（預告））。回傳 [(說話人, 台詞, 行號)]、錯誤。"""
    out, errs, skip = [], [], False
    for n, line in enumerate(open(path, encoding="utf-8"), 1):
        s = line.strip()
        if s.startswith("## "):
            skip = ("前情提要" in s) or ("下集預告" in s)
            continue
        m = re.match(r"^\*\*(.+?)：\*\*(.*)$", s)
        if not m:
            continue
        spk, rest = m.group(1).strip(), m.group(2)
        if rest.count("「") != 1 or rest.count("」") != 1:
            errs.append(f"劇本第 {n} 行：一行要剛好一組「」（目前 {rest.count('「')} 組）：{s[:40]}")
            continue
        text = rest[rest.index("「") + 1:rest.index("」")]
        if skip or "旁白" in spk or "預告" in spk:
            continue
        out.append((spk, text, n))
    return out, errs


def check(ep, quiet=False):
    errs, warns = [], []
    p = clips_path(ep)
    if not os.path.exists(p):
        return [f"{ep}：沒有 clips.json"], []
    data = json.load(open(p, encoding="utf-8"))
    clips = data["clips"]
    seen, said = set(), []
    total = 0.0
    for c in clips:
        cid = c.get("id", "?")
        where = f"{cid}"
        if cid in seen:
            errs.append(f"{where}：片段編號重複")
        seen.add(cid)
        if not re.match(rf"^{ep}-(S\d\d|OP|ED)-\d\d[a-z]?$", cid):
            errs.append(f"{where}：編號格式要是 {ep}-S05-03")
        part = c.get("part")
        if part not in PARTS:
            errs.append(f"{where}：part 必須是 {sorted(PARTS)}")
        dur = c.get("dur", 0)
        if not (4 <= dur <= 12):
            errs.append(f"{where}：dur {dur} 不在 4～12 秒")
        if part in ("cold", "main"):
            total += dur
        chars = c.get("chars", [])
        if len(chars) > 3:
            errs.append(f"{where}：角色 {len(chars)} 人，超過 3 人")
        for k in chars:
            if k not in CH:
                errs.append(f"{where}：角色 {k} 沒有登記在 registry.json")
        if len(set(chars)) != len(chars):
            errs.append(f"{where}：同一角色重複列出")
        if c.get("loc") not in LOC:
            errs.append(f"{where}：場景 {c.get('loc')} 沒有登記在 registry.json")
        mode = c.get("mode")
        if mode not in ("ref", "firstframe"):
            errs.append(f"{where}：mode 必須是 ref 或 firstframe")
        if mode == "firstframe" and not c.get("firstframe"):
            errs.append(f"{where}：firstframe 模式要寫 firstframe（關鍵幀畫面描述）")
        for f in ("setting", "staging", "camera"):
            if not c.get(f):
                errs.append(f"{where}：缺 {f}")
        names = {CH[k]["name"] for k in chars if k in CH}
        beats = c.get("beats", [])
        t = 0
        for b in beats:
            a, z = b["t"]
            if a != t:
                errs.append(f"{where}：beat {a}–{z} 秒沒接上前一段（應從 {t} 秒開始）")
            if z <= a:
                errs.append(f"{where}：beat {a}–{z} 秒長度不對")
            t = z
            nchar = 0
            for s in b.get("say", []):
                if len(s) < 2:
                    errs.append(f"{where}：say 格式要是 [說話人, 台詞, 語氣]")
                    continue
                spk, text = s[0], s[1]
                said.append((spk, text, cid))
                nchar += len(PUNCT.sub("", text))
                bn = base_name(spk)
                if not offscreen(spk) and bn not in names and bn not in c.get("extras", ""):
                    errs.append(f"{where}：「{text[:12]}」的說話人 {spk} 不在畫面角色裡（畫外要標（畫外））")
                if "「" in text or "」" in text:
                    errs.append(f"{where}：台詞裡不要放「」：{text[:20]}")
            if z > a and nchar / (z - a) > MAX_CPS:
                errs.append(f"{where}：{a}–{z} 秒台詞 {nchar} 字，{nchar / (z - a):.1f} 字／秒，超過 {MAX_CPS}（拉長 beat 或拆片段）")
            for w in CONTACT:
                if w in b.get("action", ""):
                    warns.append(f"{where}：動作有肢體接觸「{w}」，Seedance 做不出來，改成反應鏡頭＋剪接")
        if beats and t != dur:
            errs.append(f"{where}：beats 結束在 {t} 秒，但 dur 是 {dur}")
        if not beats:
            errs.append(f"{where}：沒有 beats")
    lo, hi = RUNTIME
    if clips and any(c.get("part") in ("cold", "main") for c in clips) and not (lo <= total <= hi):
        errs.append(f"{ep}：正片 {total:.0f} 秒（{total / 60:.1f} 分），要在 {lo}～{hi} 秒")
    sp = script_path(ep)
    if sp and any(c.get("part") in ("cold", "main") for c in clips):
        lines, e2 = script_lines(sp)
        errs += e2
        a = [f"{s}｜{x}" for s, x, _ in lines]
        b = [f"{s}｜{x}" for s, x, _ in said]
        if a != b:
            diff = list(difflib.unified_diff(a, b, "劇本", "clips.json", n=0, lineterm=""))
            errs.append(f"{ep}：劇本台詞與片段台詞不一致（{len(a)} vs {len(b)} 句）：\n    " + "\n    ".join(diff[:30]))
    elif not sp and ep.startswith("EP"):
        warns.append(f"{ep}：找不到 docs/longform/{ep}_SCRIPT*.md，台詞沒有對照")
    if not quiet:
        print(f"── {ep}：{len(clips)} 支片段，正片 {total:.0f} 秒（{total / 60:.1f} 分），台詞 {len(said)} 句")
        for w in warns:
            print("  ⚠", w)
        for e in errs:
            print("  ✗", e)
        if not errs:
            print("  ✓ 通過")
    return errs, warns


def say_text(spk, text, tone):
    tone = f"（{tone}）" if tone else ""
    bn = base_name(spk)
    if "心聲" in spk:
        return f"（這裡是{bn}的心聲，畫面中{bn}嘴巴閉著、沒有開口）"
    if offscreen(spk):
        return f"畫外傳來{bn}的聲音{tone}：「{text}」（畫面中沒有人開口）"
    return f"{bn}{tone}說：「{text}」"


def clip_prompt(c):
    chars = c.get("chars", [])
    L = ["【畫面】橫式16:9，3D國漫動畫（半寫實、精緻光影），畫面中不要出現任何文字、字幕或浮水印。"]
    if c["mode"] == "firstframe":
        L.append("以上傳的圖1（關鍵幀）作為第一幀，人物外觀、服裝、站位都照圖1，不要改變。")
    if chars:
        n = len(chars)
        L.append(f"【角色】畫面中只有這{n}個人，每人只出現一次，不要出現長得一樣的人：" if n > 1 else "【角色】畫面中只有這1個人，只出現一次：")
        for i, k in enumerate(chars, 1):
            tag = f"圖{i} " if c["mode"] == "ref" else ""
            L.append(f"- {tag}{CH[k]['name']}：{CH[k]['seedance']}。")
    else:
        L.append("【角色】畫面中沒有主要角色。")
    if c.get("extras"):
        L.append(f"【背景人物】{c['extras']}（模糊、不看鏡頭、不排隊、不說話）。")
    loc = LOC[c["loc"]]
    tag = f"圖{len(chars) + 1}" if c["mode"] == "ref" else ""
    L.append(f"【場景】{tag}{loc['name']}：{c['setting']}。{c.get('time', '')}。")
    L.append(f"【站位】{c['staging']}")
    L.append("【分鏡與台詞】")
    for b in c["beats"]:
        a, z = b["t"]
        says = "".join(say_text(*(s + [""])[:3]) for s in b.get("say", []))
        L.append(f"{a}–{z}秒：{b.get('shot', '')}。{b['action']}{'。' if says and not b['action'].endswith('。') else ''}{says}")
    L.append(f"【鏡頭】{c['camera']}")
    forbid = ["文字", "字幕", "浮水印"] + (["第" + "一二三四"[len(chars)] + "個人", "重複出現的人"] if chars else []) + c.get("forbid", [])
    L.append("【禁止】" + "、".join(dict.fromkeys(forbid)) + "。")
    return "\n".join(L)


def uploads(c):
    if c["mode"] == "firstframe":
        return [f"關鍵幀 `production/series/{c['id'][:4]}/keyframes/{c['id']}.png`"]
    out = []
    for k in c.get("chars", []):
        ch = CH[k]
        out.append(ch["label"] + ("" if ch.get("sheet") else "（待出圖）"))
    loc = LOC[c["loc"]]
    out.append(loc["label"] + ("" if loc.get("sheet") else "（待出圖）"))
    return out


def keyframe_prompt(c):
    refs = [CH[k]["label"] for k in c.get("chars", [])] + [LOC[c["loc"]]["label"]]
    return ("Reference images: " + "、".join(refs) + "\n\n" +
            "Semi-realistic 3D Chinese animation (guoman) style, cinematic lighting, horizontal 16:9 single full-bleed frame, "
            "no text, no subtitles, no watermark, no borders. Every character must match their reference sheet exactly "
            "(face, hair, costume, weapon).\n\n畫面：" + c["firstframe"])


def build(ep):
    data = json.load(open(clips_path(ep), encoding="utf-8"))
    clips = data["clips"]
    L = [f"# {ep}〈{data.get('title', '')}〉Seedance 提示詞", "",
         f"> 由 `production/series/tools/series.py build {ep}` 從 `{ep}/clips.json` 產生，**不要手改**；改內容請改 clips.json 再重跑。",
         "> CapCut AI Lab：上傳順序＝圖1、圖2…；一支最長 12 秒；存檔 `production/series/" + ep + "/video/<片段編號>.mp4`。",
         "> 標「（待出圖）」的設定圖還沒做，見 `production/series/ASSETS_TODO.md`。", "",
         "| 片段 | 秒 | 模式 | 上傳（依序） | 內容 |", "|---|---|---|---|---|"]
    for c in clips:
        first = c["beats"][0]["action"][:24] if c.get("beats") else ""
        L.append(f"| {c['id']} | {c['dur']} | {'首幀' if c['mode'] == 'firstframe' else '參考圖'} | {'、'.join(uploads(c))} | {first}… |")
    L.append("")
    scene = None
    for c in clips:
        if c.get("scene") != scene:
            scene = c.get("scene")
            L += [f"## {scene}", ""]
        L += [f"### {c['id']}（{c['dur']} 秒）", "", "**上傳：** " + "、".join(uploads(c)), ""]
        if c["mode"] == "firstframe":
            L += ["**關鍵幀（先用 Gemini 出圖，存成 `production/series/" + ep + f"/keyframes/{c['id']}.png`）**", "",
                  "```text", keyframe_prompt(c), "```", "", "**Seedance**", ""]
        L += ["```text", clip_prompt(c), "```", ""]
    out = os.path.join(SERIES, ep, "seedance_prompts.md")
    open(out, "w", encoding="utf-8").write("\n".join(L))
    print(f"✓ {os.path.relpath(out, REPO)}（{len(clips)} 支）")


def episodes():
    return sorted(os.path.basename(os.path.dirname(p)) for p in glob.glob(os.path.join(SERIES, "*", "clips.json")))


def assets():
    need = {}
    for ep in episodes():
        for c in json.load(open(clips_path(ep), encoding="utf-8"))["clips"]:
            for k in c.get("chars", []) + [c.get("loc")]:
                e = CH.get(k) or LOC.get(k)
                if e and not e.get("sheet"):
                    d = need.setdefault(k, {"first": c["id"], "n": 0})
                    d["n"] += 1
    L = ["# 還沒出的設定圖", "", "> 由 `series.py assets` 產生。依第一次用到的片段排序；出圖規範見 `docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md`，"
         "流程沿用 `production/LF01/tools/lf01_handoff.py`（角色 3:2 設定圖、場景設定圖）。", "",
         "| 編號 | 名稱 | 第一次用到 | 用到幾支 | 外觀依據 |", "|---|---|---|---|---|"]
    for k, d in sorted(need.items(), key=lambda kv: kv[1]["first"]):
        e = CH.get(k) or LOC.get(k)
        L.append(f"| {k} | {e['name']} | {d['first']} | {d['n']} | {e.get('sheetBrief') or e['seedance']} |")
    open(os.path.join(SERIES, "ASSETS_TODO.md"), "w", encoding="utf-8").write("\n".join(L) + "\n")
    print(f"✓ ASSETS_TODO.md（{len(need)} 項）")


def main():
    cmd = sys.argv[1] if len(sys.argv) > 1 else "all"
    eps = sys.argv[2:] or episodes()
    bad = False
    if cmd in ("check", "all"):
        for ep in eps:
            e, _ = check(ep)
            bad |= bool(e)
    if cmd in ("build", "all"):
        for ep in eps:
            build(ep)
    if cmd in ("assets", "all"):
        assets()
    sys.exit(1 if bad else 0)


if __name__ == "__main__":
    main()
