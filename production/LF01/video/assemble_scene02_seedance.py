#!/usr/bin/env python3
"""第 2 場第七室段（A05 f2～A09 f2）：Seedance 生成的 4 支影片 → 接上我們的配音、字幕 → LF01_scene02_room7_seedance_v1.mp4。

影片原檔放 scene02/V1.mp4～V4.mp4（CapCut AI Lab 直接生影片，提示詞見 scene02_seedance_prompts.md）。
- Seedance 自帶的聲音（亂講的中文＋配樂）一律靜音。
- 左上角「CapCut Ai」浮水印用 delogo 抹掉。
- 每句台詞放在畫面上該角色開口／反應的時間點（人工看逐格決定，寫在 PLAN）。
- 字幕規則同 production/roles/06_字幕.md（沿用 animatic/render_scene12.py 的字型與排版）。

  python3 production/LF01/video/assemble_scene02_seedance.py
"""
import json
import os
import shutil
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
LF = os.path.dirname(HERE)
sys.path.insert(0, os.path.join(LF, "animatic"))
import render_scene12 as base  # noqa: E402  ffmpeg、字型、字幕排版

run, W, H, FPS = base.run, 1920, 1080, 30
MAN = {e["key"]: e for e in json.load(open(os.path.join(LF, "voice", "scene02_manifest.json"), encoding="utf-8"))}
SRC = os.path.join(HERE, "scene02")
TMP = os.path.join(HERE, "cache_scene02")
OUT = os.path.join(HERE, "LF01_scene02_room7_seedance_v1.mp4")
WATERMARK = "delogo=x=34:y=34:w=262:h=68"

# 支 → (浮水印?, [(開始秒, 配音 key)])；秒數＝該支影片內的時間
PLAN = [
    ("V1", False, [(1.6, "S02-17_A05f2_齊衡烈"), (3.2, "S02-18_A05f2_机遙"), (4.5, "S02-19_A06f1_齊衡烈")]),
    ("V2", True, [(0.9, "S02-20_A06f2_郁岑燁"), (5.5, "S02-21_A07f1_齊衡烈"), (7.15, "S02-22_A07f2_郁岑燁")]),
    ("V3", True, [(0.4, "S02-23_A08f1_厲若楓"), (3.0, "S02-24_A08f2_机遙心聲"), (6.8, "S02-25_A08f2_机遙")]),
    ("V4", True, [(0.9, "S02-26_A09f1_郁岑燁"), (2.8, "S02-27_A09f2_齊衡烈")]),
]


def main():
    os.makedirs(TMP, exist_ok=True)
    parts, voices, subs, t = [], [], [], 0.0
    for name, wm, lines in PLAN:
        src = os.path.join(SRC, f"{name}.mp4")
        if not os.path.exists(src):
            sys.exit(f"缺影片 {src}")
        d = base.dur(src)
        part = os.path.join(TMP, f"{name}.mp4")
        vf = (WATERMARK + "," if wm else "") + f"scale={W}:{H},setsar=1,fps={FPS},format=yuv420p"
        run(["-i", src, "-an", "-vf", vf, "-c:v", "libx264", "-preset", "veryfast", "-crf", "18", part])
        parts.append(part)
        for st, key in lines:
            m = MAN[key]
            f = os.path.join(LF, "voice", m["file"])
            ld = base.dur(f)
            if st + ld > d + 0.3:
                print(f"⚠ {key} 超出 {name} 結尾 {st + ld - d:.2f} 秒")
            voices.append((f, t + st))
            subs.append([t + st, t + st + max(1.2, ld + 0.3), m["text"]])
        t += d
    total = t
    subs.sort()
    for a, b in zip(subs, subs[1:]):
        a[1] = min(a[1], b[0] - 0.05)

    lst = os.path.join(TMP, "list.txt")
    open(lst, "w").write("".join(f"file '{p}'\n" for p in parts))
    video = os.path.join(TMP, "video.mp4")
    run(["-f", "concat", "-safe", "0", "-i", lst, "-c", "copy", video])

    def ts(x):
        return f"{int(x // 3600)}:{int(x % 3600 // 60):02d}:{x % 60:05.2f}"
    ass = os.path.join(TMP, "subs.ass")
    with open(ass, "w", encoding="utf-8") as f:
        f.write("[Script Info]\nScriptType: v4.00+\nPlayResX: %d\nPlayResY: %d\n\n" % (W, H))
        f.write("[V4+ Styles]\nFormat: Name, Fontname, Fontsize, PrimaryColour, OutlineColour, BackColour, Bold, "
                "BorderStyle, Outline, Shadow, Alignment, MarginV\n")
        f.write(f"Style: D,{base.FONT_NAME},50,&H00FFFFFF,&H00000000,&H80000000,0,1,3,1,2,60\n\n")
        f.write("[Events]\nFormat: Layer, Start, End, Style, Text\n")
        for a, b, line in subs:
            f.write(f"Dialogue: 0,{ts(a)},{ts(b)},D,{base.sub_text(line)}\n")
    fdir = os.path.join(TMP, "fonts")
    os.makedirs(fdir, exist_ok=True)
    shutil.copy(base.FONT, os.path.join(fdir, os.path.basename(base.FONT)))

    ins, fl, labels = ["-i", video], [], []
    for k, (f, st) in enumerate(voices, 1):
        ins += ["-i", f]
        ms = int(st * 1000)
        fl.append(f"[{k}:a]aformat=sample_rates=48000:channel_layouts=stereo,adelay={ms}|{ms}[v{k}]")
        labels.append(f"[v{k}]")
    # 室內底噪（很輕的棕噪＋低通），讓台詞之間不是全然死寂
    fl.append(f"anoisesrc=color=brown:amplitude=0.6:d={total}:r=48000,lowpass=f=400,volume=0.05,"
              f"aformat=channel_layouts=stereo,afade=t=in:d=1,afade=t=out:st={total - 1.5:.2f}:d=1.5[room]")
    labels.append("[room]")
    fl.append("".join(labels) + f"amix=inputs={len(labels)}:normalize=0:duration=longest,atrim=0:{total},"
              "alimiter=limit=0.9[aout]")
    vf = f"subtitles='{ass}':fontsdir='{fdir}',fade=t=in:st=0:d=0.6,fade=t=out:st={total - 1.0:.2f}:d=1.0"
    run(ins + ["-filter_complex", f"[0:v]{vf}[vout];" + ";".join(fl), "-map", "[vout]", "-map", "[aout]",
               "-c:v", "libx264", "-preset", "medium", "-crf", "24", "-c:a", "aac", "-b:a", "160k",
               "-movflags", "+faststart", OUT])
    print(f"✓ {OUT}（{total:.1f} 秒，{len(voices)} 句）")


if __name__ == "__main__":
    main()
