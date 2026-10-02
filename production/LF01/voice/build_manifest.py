#!/usr/bin/env python3
"""依 lf01.json 目前的台詞，產生各場配音清單（scene02_manifest.json、scene12_manifest.json）。
同一角色、同一句台詞已有定稿配音就沿用（來源：final/README.md 表格、scene12_v2_manifest.json、舊清單），否則 file 留空待配。
厲若楓（2026-10-02 改為女性）與院方執事改裴含章：舊配音一律不沿用。
  python3 build_manifest.py
"""
import json, os, re
HERE = os.path.dirname(os.path.abspath(__file__))
LF = os.path.dirname(HERE)
data = json.load(open(os.path.join(LF, "lf01.json"), encoding="utf-8"))
NO_REUSE = {"厲若楓"}
known = {}
for row in open(os.path.join(HERE, "final", "README.md"), encoding="utf-8"):
    m = re.match(r"\| (\d+) \| (\w+) \| ([^|]+) \| ([^|]+) \|", row)
    if m:
        n, seg, spk, text = m.groups()
        f = next(x for x in os.listdir(os.path.join(HERE, "final")) if x.startswith(f"{int(n):02d}_"))
        known[(spk.strip(), text.strip())] = "final/" + f
for src in ["scene12_v2_manifest.json", "scene02_manifest.json", "scene12_manifest.json"]:
    p = os.path.join(HERE, src)
    if os.path.exists(p):
        for e in json.load(open(p, encoding="utf-8")):
            if e.get("file"):
                known.setdefault((e["spk"], e["text"]), e["file"])
for pre, out, tag in [("A", "scene02_manifest.json", "S02"), ("B", "scene12_manifest.json", "S12")]:
    man, n = [], 0
    for seg in data["segments"]:
        if not seg["id"].startswith(pre):
            continue
        for k, cut in enumerate(seg["cuts"], 1):
            for spk, text in cut.get("lines", []):
                n += 1
                f = None if spk in NO_REUSE else known.get((spk, text))
                if spk == "蕭曜霖（畫外）":
                    f = f or known.get(("蕭曜霖（畫外）", text))
                man.append({"key": f"{tag}-{n:02d}_{seg['id']}f{k}_{re.sub(r'[（）]', '', spk)}", "spk": spk, "text": text, "file": f})
    json.dump(man, open(os.path.join(HERE, out), "w", encoding="utf-8"), ensure_ascii=False, indent=1)
    todo = [e for e in man if not e["file"]]
    print(out, len(man), "句，待配", len(todo))
    for e in todo:
        print("  ", e["key"], e["text"])
