#!/usr/bin/env python3
"""把 shuohao-skills 全部文字檔（SKILL.md / references / 腳本 / 範例 / 測試夾具）
從簡體轉成台灣正體（OpenCC s2twp，含用語轉換）。

腳本裡的介面文字、品質門關鍵字與範例/測試夾具必須一起轉，否則品質門會拿簡體詞
比對繁體內容而誤判。轉完務必跑全部 selftest。可重複執行（已是繁體的內容不會再變）。

用法：pip install opencc && python3 .claude/shuohao/to-traditional.py
"""
from pathlib import Path
import opencc

ROOT = Path(__file__).resolve().parent.parent  # .claude/
SKILLS = ["novel-outline", "novel-characters", "novel-art", "novel-script", "novel-storyboard"]
EXTS = {".md", ".mjs", ".json", ".txt"}

cc = opencc.OpenCC("s2twp")
# OpenCC 漏轉的「里→裡」（名詞後方位詞），逐條列出避免誤傷「千里」之類
FIX_LI = ["本里", "艙里", "道里", "角里", "卡里", "文里", "路里"]


# OpenCC 沒處理的大陸用語 → 台灣慣用
TERMS = {"質量": "品質", "對賬": "對帳"}


def fix(text):
    for a, b in TERMS.items():
        text = text.replace(a, b)
    for w in FIX_LI:
        text = text.replace(w, w[0] + "裡")
    return text

targets = [p for s in SKILLS for p in (ROOT / "skills" / s).rglob("*") if p.is_file() and p.suffix in EXTS]
targets += [ROOT / "shuohao" / "report.mjs", ROOT / "shuohao" / "report-selftest.mjs"]

changed = 0
for p in targets:
    src = p.read_text(encoding="utf-8")
    # OpenCC 遇到 NUL 字元會截斷/錯位（novel-characters.mjs 用 NUL 當 key 分隔符），分段轉再接回
    out = "\x00".join(fix(cc.convert(part)) for part in src.split("\x00"))
    if out != src:
        p.write_text(out, encoding="utf-8")
        changed += 1
print(f"轉換 {changed}/{len(targets)} 個檔案")

# 在每個 SKILL.md 的 frontmatter 後插入本專案的語言規則（已存在就跳過）
NOTE = ("\n> **本專案語言規則（九曜：天墟）**：所有人類可讀內容（改編說明、梗概、人物畫像、場景/道具說明、"
        "劇本動作與台詞、分鏡說明、報告文字）一律使用**繁體中文（台灣用語）**；輸入素材若是簡體也要以繁體輸出。"
        "`lang` 維持 `zh`（內建介面已轉為繁體）。出圖提示詞、TTS 音色提示詞照原規則維持英文。\n")
for s_ in SKILLS:
    p = ROOT / "skills" / s_ / "SKILL.md"
    t = p.read_text(encoding="utf-8")
    if "本專案語言規則" in t:
        continue
    end = t.index("\n---\n", 4) + len("\n---\n")
    p.write_text(t[:end] + NOTE + t[end:], encoding="utf-8")
    print("加入語言規則:", p.name, s_)
