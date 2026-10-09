#!/usr/bin/env python3
"""第 2 場 Seedance 段落組裝：Seedance 生成的影片 → 接上我們的配音、字幕、音效 → MP4。

段落（PLANS）：room7＝第七室（A05 f2～A09 f2）、corridor＝東廊撞人（A04，C1a＋C1 第 1 支後半段）。

影片原檔放 scene02/V1.mp4～V4.mp4（CapCut AI Lab 直接生影片，提示詞見 scene02_seedance_prompts.md）。
- Seedance 自帶的聲音（亂講的中文＋配樂）一律靜音。
- 左上角「CapCut Ai」浮水印用 delogo 抹掉。
- 每句台詞放在畫面上該角色開口／反應的時間點（人工看逐格決定，寫在 PLAN）。
- 字幕規則同 production/roles/06_字幕.md（沿用 animatic/render_scene12.py 的字型與排版）。

  python3 production/LF01/video/assemble_scene02_seedance.py room7
  python3 production/LF01/video/assemble_scene02_seedance.py corridor
每支：{src, wm（抹浮水印）, ss/to（只取這段秒數）, pad（結尾凍結秒數）, lines[(秒, 配音key, 音量)], hit[秒]（砰聲＋畫面震動）}；秒數都是「取出後」的時間。
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
WATERMARK = "delogo=x=34:y=34:w=262:h=68"

# room7 v2（2026-10-09）：咖哩回報 v1「配音好像沒對到」。改依 Seedance 自帶語音的說話時段（whisper-1 逐字時間戳）
# ＋逐格看嘴型重排：V1 齊衡烈 1.30／2.70 開口、机遙夾在中間；V2 郁岑燁 3.3 秒才抬眼、齊衡烈 5.3 秒開口；
# V3 厲若楓 0.72 開口、机遙 6.10 開口（心聲放在他低頭那段）；V4 郁岑燁 0～1.8、齊衡烈 2.3～4.0。
# corridor（2026-10-09）：撞人的瞬間模型演不出來（三支都像主動打招呼、一支鏡頭跳接），改成剪輯手法：
# C1a 机遙一人走路看憑證，4.0 秒「砰」＋畫面一震 → 切 C1 第 1 支 3.0 秒起（陸鳴鸞已在身旁笑著道歉、最後拍肩跑走）。
PLANS = {
    "room7": ("LF01_scene02_room7_seedance_v2.mp4", [
        dict(src="V1", wm=False, lines=[(1.3, "S02-17_A05f2_齊衡烈"), (2.25, "S02-18_A05f2_机遙"), (2.8, "S02-19_A06f1_齊衡烈")]),
        dict(src="V2", wm=True, lines=[(1.9, "S02-20_A06f2_郁岑燁"), (5.3, "S02-21_A07f1_齊衡烈"), (7.15, "S02-22_A07f2_郁岑燁")]),
        dict(src="V3", wm=True, lines=[(0.7, "S02-23_A08f1_厲若楓"), (2.45, "S02-24_A08f2_机遙心聲"), (6.1, "S02-25_A08f2_机遙")]),
        dict(src="V4", wm=True, lines=[(0.3, "S02-26_A09f1_郁岑燁"), (2.3, "S02-27_A09f2_齊衡烈")]),
    ]),
    "corridor": ("LF01_scene02_corridor_seedance_v1.mp4", [
        dict(src="C1a", wm=True, hit=[4.0], lines=[]),
        dict(src="C1_take1", wm=True, ss=3.0, to=12.0, lines=[
            (0.2, "S02-12_A04f2_陸鳴鸞"), (2.6, "S02-13_A04f2_机遙"), (3.4, "S02-14_A04f2_陸鳴鸞"),
            (5.5, "S02-15_A04f2_演武場遠", 0.35)]),
    ]),
    # 第 2 場完整版（2026-10-09）：開場 4 張分鏡圖（運鏡＋地點卡）＋ Seedance 影片 12 支。
    # R1 只有 8 秒放不下裴含章 3 句＋机遙 2 句 → 整支放慢到 0.82 倍，並把「新芽都在東廊。順著左手邊的迴廊走到底就是。」只留前半句（cut 1.6 秒）。
    "full": ("LF01_scene02_seedance_full_v1.mp4", [
        dict(img="storyboard/frames/A01/f1.png", dur=5.0, cam="push", caption={"top": "九曜界", "main": "天玄院"}, lines=[]),
        dict(img="storyboard/frames/A01/f2.png", dur=3.5, cam="static", lines=[]),
        dict(img="storyboard/frames/A02/f1.png", dur=3.5, cam="push", lines=[(0.8, "S02-01_A02f1_机遙心聲")]),
        dict(img="storyboard/frames/A02/f2.png", dur=5.0, cam="static", lines=[
            (0.5, "S02-02_A02f2_考官助手閃回"), (3.0, "S02-03_A02f2_机遙心聲")]),
        dict(src="R1", wm=True, speed=0.82, lines=[
            (0.9, "S02-04_A03f2_裴含章"), (4.1, "S02-05_A03f2_机遙"), (4.6, "S02-06_A03f2_裴含章"),
            (6.3, "S02-07_A03f2_裴含章", 1.0, 1.6), (8.4, "S02-08_A03f2_机遙")]),
        dict(src="R2", wm=True, lines=[(0.72, "S02-09_A03f2_裴含章"), (6.28, "S02-10_A03f2_机遙"), (8.24, "S02-11_A03f2_裴含章")]),
        dict(src="C1a", wm=True, hit=[4.0], lines=[]),
        dict(src="C1_take1", wm=True, ss=3.0, to=12.0, lines=[
            (0.2, "S02-12_A04f2_陸鳴鸞"), (2.6, "S02-13_A04f2_机遙"), (3.4, "S02-14_A04f2_陸鳴鸞"),
            (5.5, "S02-15_A04f2_演武場遠", 0.35)]),
        dict(src="D1", wm=True, lines=[(1.2, "S02-16_A05f1_机遙心聲")]),
        dict(src="V1", wm=False, lines=[(1.3, "S02-17_A05f2_齊衡烈"), (2.25, "S02-18_A05f2_机遙"), (2.8, "S02-19_A06f1_齊衡烈")]),
        dict(src="V2", wm=True, lines=[(1.9, "S02-20_A06f2_郁岑燁"), (5.3, "S02-21_A07f1_齊衡烈"), (7.15, "S02-22_A07f2_郁岑燁")]),
        dict(src="V3", wm=True, lines=[(0.7, "S02-23_A08f1_厲若楓"), (2.45, "S02-24_A08f2_机遙心聲"), (6.1, "S02-25_A08f2_机遙")]),
        dict(src="V4", wm=True, lines=[(0.3, "S02-26_A09f1_郁岑燁"), (2.3, "S02-27_A09f2_齊衡烈")]),
        dict(src="E2", wm=True, lines=[(2.9, "S02-28_A10f1_蕭曜霖畫外"), (6.1, "S02-29_A10f2_蕭曜霖")]),
        dict(src="F12", wm=True, pad=0.5, lines=[
            (0.0, "S02-30_A11f2_机遙"), (1.06, "S02-31_A11f2_齊衡烈"), (2.9, "S02-32_A12f2_齊衡烈"),
            (9.95, "S02-33_A12f2_厲若楓"), (11.55, "S02-34_A12f2_机遙")]),
        dict(src="F34", wm=True, pad=0.4, lines=[
            (0.0, "S02-35_A12f2_齊衡烈"), (1.16, "S02-36_A12f2_郁岑燁"), (3.4, "S02-37_A12f2_齊衡烈"),
            (8.0, "S02-38_A12f3_机遙"), (9.7, "S02-39_A12f3_郁岑燁")]),
        dict(src="F5", wm=True, lines=[(0.2, "S02-40_A12f3_厲若楓"), (6.2, "S02-41_A12f4_机遙心聲")]),
    ]),
    # F12（長片試驗，12 秒，Seedance 1.5）：這次模型照我們的台詞講，whisper 逐字時間戳直接當對位：
    # 這麼快 0.00／習慣就好 1.06／喂新來的… 2.82～9.78／我自己會說 9.92～11.21／机遙 11.58。結尾凍結 0.5 秒讓「机遙。」講完。
    "room7_intro": ("LF01_scene02_room7_intro_seedance_v1.mp4", [
        dict(src="F12", wm=True, pad=0.5, lines=[
            (0.0, "S02-30_A11f2_机遙"), (1.06, "S02-31_A11f2_齊衡烈"), (2.9, "S02-32_A12f2_齊衡烈"),
            (9.95, "S02-33_A12f2_厲若楓"), (11.55, "S02-34_A12f2_机遙")]),
    ]),
}


def add_lines(seg, t, d, voices, subs):
    """台詞 (開始秒, 配音key[, 音量[, 只取前幾秒]])；缺配音就停下列出。"""
    for st, key, *opt in seg["lines"]:
        m = MAN[key]
        if not m.get("file"):
            sys.exit(f"缺配音 {key}（{m['text']}）")
        f = os.path.join(LF, "voice", m["file"])
        vol = opt[0] if opt else 1.0
        cut = opt[1] if len(opt) > 1 else None
        ld = cut or base.dur(f)
        if st + ld > d + 0.3:
            print(f"⚠ {key} 超出段落結尾 {st + ld - d:.2f} 秒")
        voices.append((f, t + st, vol, cut))
        text = m["text"] if not cut else m["text"].split("。")[0] + "。"
        subs.append([t + st, t + st + max(1.2, ld + 0.3), text])


def shake(hits):
    """砰的瞬間畫面震 0.35 秒（先放大 4% 再左右上下抖）。"""
    if not hits:
        return ""
    on = "+".join(f"between(t,{h:.2f},{h + 0.35:.2f})" for h in hits)
    return (f"scale={int(W * 1.04)}:{int(H * 1.04)},crop={W}:{H}:"
            f"'(iw-{W})/2+({on})*18*sin(t*90)':'(ih-{H})/2+({on})*12*cos(t*77)',")


def main():
    which = sys.argv[1] if len(sys.argv) > 1 else "room7"
    out_name, plan = PLANS[which]
    out = os.path.join(HERE, out_name)
    os.makedirs(TMP, exist_ok=True)
    parts, voices, subs, hits, t = [], [], [], [], 0.0
    caps = []
    for n_seg, seg in enumerate(plan):
        if "img" in seg:  # 已通過的分鏡圖＋運鏡
            img, d = os.path.join(LF, seg["img"]), seg["dur"]
            if not os.path.exists(img):
                sys.exit(f"缺圖 {img}")
            n = int(d * FPS)
            z = f"1+0.10*on/{n}" if seg.get("cam") == "push" else f"1+0.02*on/{n}"
            part = os.path.join(TMP, f"{which}_{n_seg:02d}_img.mp4")
            run(["-loop", "1", "-i", img, "-t", f"{d:.3f}", "-vf",
                 f"scale={W * 2}:{H * 2}:force_original_aspect_ratio=increase,crop={W * 2}:{H * 2},setsar=1,"
                 f"zoompan=z='{z}':x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':d={n}:s={W}x{H}:fps={FPS},format=yuv420p",
                 "-c:v", "libx264", "-preset", "veryfast", "-crf", "18", part])
            parts.append(part)
            if seg.get("caption"):
                caps.append((t + 0.5, t + min(d, 4.7), seg["caption"]))
            add_lines(seg, t, d, voices, subs)
            t += d
            continue
        src = os.path.join(SRC, f"{seg['src']}.mp4")
        if not os.path.exists(src):
            sys.exit(f"缺影片 {src}")
        ss, to = seg.get("ss", 0.0), seg.get("to")
        pad, speed = seg.get("pad", 0.0), seg.get("speed", 1.0)
        d = ((to or base.dur(src)) - ss) / speed + pad
        part = os.path.join(TMP, f"{which}_{n_seg:02d}_{seg['src']}.mp4")
        vf = (WATERMARK + "," if seg["wm"] else "") + (f"setpts=PTS/{speed}," if speed != 1.0 else "") + \
            shake(seg.get("hit", [])) + \
            f"scale={W}:{H},setsar=1,fps={FPS},format=yuv420p" + \
            (f",tpad=stop_mode=clone:stop_duration={pad}" if pad else "")
        run(["-ss", str(ss)] + (["-t", f"{(to - ss):.3f}"] if to else []) + ["-i", src, "-t", f"{d:.3f}", "-an", "-vf", vf,
             "-c:v", "libx264", "-preset", "veryfast", "-crf", "18", part])
        parts.append(part)
        hits += [t + h for h in seg.get("hit", [])]
        add_lines(seg, t, d, voices, subs)
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
        f.write(f"Style: D,{base.FONT_NAME},50,&H00FFFFFF,&H00000000,&H80000000,0,1,3,1,2,60\n")
        f.write(f"Style: C,{base.CAP_FONT or base.FONT_NAME},120,&H00FFFFFF,&H00000000,&H64000000,0,1,0,4,1,140\n\n")
        f.write("[Events]\nFormat: Layer, Start, End, Style, Text\n")
        for a, b, line in subs:
            f.write(f"Dialogue: 0,{ts(a)},{ts(b)},D,{base.sub_text(line)}\n")
        for a, b, cap in caps:
            f.write(f"Dialogue: 1,{ts(a)},{ts(b)},C,{{\\fad(600,600)\\pos(120,920)\\an1\\fsp26}}"
                    f"{{\\fs48\\fsp18\\c&HC8E1EB&}}{cap['top']}\\N{{\\fs120\\fsp26\\c&HFFFFFF&}}{cap['main']}\n")
    fdir = os.path.join(TMP, "fonts")
    os.makedirs(fdir, exist_ok=True)
    for x in [base.FONT] + ([base.CAP_FILE] if base.CAP_DIR else []):
        shutil.copy(x, os.path.join(fdir, os.path.basename(x)))

    ins, fl, labels = ["-i", video], [], []
    for k, (f, st, vol, cut) in enumerate(voices, 1):
        ins += ["-i", f]
        ms = int(st * 1000)
        trim = f"atrim=0:{cut},afade=t=out:st={cut - 0.12:.2f}:d=0.12," if cut else ""
        fl.append(f"[{k}:a]{trim}aformat=sample_rates=48000:channel_layouts=stereo,volume={vol},adelay={ms}|{ms}[v{k}]")
        labels.append(f"[v{k}]")
    for j, h in enumerate(hits):  # 砰：低頻衝擊＋短促噪聲
        ms = int(h * 1000)
        fl.append(f"sine=f=70:d=0.35:r=48000,volume=1.6,afade=t=out:st=0.02:d=0.33[hs{j}];"
                  f"anoisesrc=color=pink:amplitude=0.8:d=0.25:r=48000,lowpass=f=900,afade=t=out:st=0.01:d=0.24[hn{j}];"
                  f"[hs{j}][hn{j}]amix=inputs=2:normalize=0,aformat=channel_layouts=stereo,adelay={ms}|{ms}[h{j}]")
        labels.append(f"[h{j}]")
    # 室內底噪（很輕的棕噪＋低通），讓台詞之間不是全然死寂
    fl.append(f"anoisesrc=color=brown:amplitude=0.6:d={total}:r=48000,lowpass=f=400,volume=0.05,"
              f"aformat=channel_layouts=stereo,afade=t=in:d=1,afade=t=out:st={total - 1.5:.2f}:d=1.5[room]")
    labels.append("[room]")
    fl.append("".join(labels) + f"amix=inputs={len(labels)}:normalize=0:duration=longest,atrim=0:{total},"
              "alimiter=limit=0.9[aout]")
    vf = f"subtitles='{ass}':fontsdir='{fdir}',fade=t=in:st=0:d=0.6,fade=t=out:st={total - 1.0:.2f}:d=1.0"
    run(ins + ["-filter_complex", f"[0:v]{vf}[vout];" + ";".join(fl), "-map", "[vout]", "-map", "[aout]",
               "-c:v", "libx264", "-preset", "medium", "-crf", "24", "-c:a", "aac", "-b:a", "160k",
               "-movflags", "+faststart", out])
    print(f"✓ {out}（{total:.1f} 秒，{len(voices)} 句）")


if __name__ == "__main__":
    main()
