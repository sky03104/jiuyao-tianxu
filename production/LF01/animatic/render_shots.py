#!/usr/bin/env python3
"""LF01 動態分鏡（鏡頭表版）：讀 ../scene12_shots.json＋配音清單 → MP4。

鏡頭表每個鏡頭：src（分鏡圖）＋crop（裁切 [x,y,w]）／new（storyboard/frames/new/<鏡號>.png）／reuse（沿用某鏡的新圖）、
景別、運鏡、音效 sfx、台詞（情緒／音量／速度／pre 前停頓／ovl 搶話重疊）。
時間軸：每句開始＝上一句結束＋pre（有 ovl 就提前 ovl 秒，兩句疊在一起）；鏡頭長度＝台詞講完＋0.35 秒（無台詞用 sec）。
聲音：配音＋暫時音效（ffmpeg 合成：地鳴、衝擊、鈴、風、箭、碎裂）＋低頻配樂底（「配樂抽掉／壓低」的鏡頭靜音，「配樂起戰鬥」後加心跳鼓）。
字幕與地點卡規則見 production/roles/06_字幕.md。

  python3 production/LF01/animatic/render_shots.py               → LF01_scene12_animatic_v5.mp4
  python3 production/LF01/animatic/render_shots.py --stills      → 只輸出每鏡第一格到 stills_v5/
"""
import json, os, re, shutil, subprocess, sys

HERE = os.path.dirname(os.path.abspath(__file__))
LF = os.path.dirname(HERE)
sys.path.insert(0, HERE)
import render_scene12 as base  # 沿用 ffmpeg、字型、字幕排版、地點卡字型、dur()

SHOTS = json.load(open(os.path.join(LF, "scene12_shots.json"), encoding="utf-8"))["shots"]
MAN = {e["key"]: e for e in json.load(open(os.path.join(LF, "voice", "scene12_shots_manifest.json"), encoding="utf-8"))}
OUT = os.path.join(HERE, "LF01_scene12_animatic_v5.mp4")
TMP = os.path.join(HERE, "cache_v5")
W, H, FPS = base.W, base.H, base.FPS
CAPTION = {"S01": {"top": "青嵐古林", "main": "陣眼遺跡"}}
run, FF = base.run, base.FF


def image_of(s):
    if "new" in s:
        return os.path.join(LF, "storyboard", "frames", "new", f"{s['id']}.png"), None
    if "reuse" in s:
        return os.path.join(LF, "storyboard", "frames", "new", f"{s['reuse']}.png"), s.get("crop")
    return os.path.join(LF, s["src"]), s.get("crop")


def motion(cam, sec, crop, mirror):
    n = max(int(sec * FPS), 2)
    pre = ""
    if crop:
        x, y, w = crop
        pre = f"crop=iw*{w}:ih*{w}:iw*{x}:ih*{y},"
    if cam.startswith("Push"):
        z, xx = f"1+0.12*on/{n}", "iw/2-(iw/zoom/2)"
    elif cam.startswith("Tracking"):
        z = "1.10"
        xx = f"(iw-iw/zoom)*(on/{n})" if not mirror else f"(iw-iw/zoom)*(1-on/{n})"
    else:
        z, xx = f"1+0.03*on/{n}", "iw/2-(iw/zoom/2)"
    return (f"{pre}scale=3840:2160,setsar=1,zoompan=z='{z}':x='{xx}':y='ih/2-(ih/zoom/2)':d={n}:s={W}x{H}:fps={FPS},"
            "setsar=1,format=yuv420p")


# 暫時音效：關鍵字 → ffmpeg 合成濾鏡（輸出約 1～3 秒）
SFX = [
    (r"地鳴|轟鳴|低頻嗡鳴", "anoisesrc=color=brown:amplitude=0.9:d=3:r=48000,lowpass=f=90,volume=0.9,afade=t=in:d=0.8,afade=t=out:st=1.8:d=1.2"),
    (r"衝擊|爆發|彈開|石板", "anoisesrc=color=pink:amplitude=1:d=0.9:r=48000,lowpass=f=600,afade=t=out:st=0.05:d=0.85,volume=1.2"),
    (r"鈴", "sine=f=2350:d=1.4:r=48000,volume=0.35,afade=t=out:st=0.02:d=1.35"),
    (r"風|霧合攏", "anoisesrc=color=white:amplitude=0.5:d=3.5:r=48000,bandpass=f=500:w=400,volume=0.25,afade=t=in:d=1,afade=t=out:st=2:d=1.5"),
    (r"箭|弓弦", "anoisesrc=color=white:amplitude=0.7:d=0.35:r=48000,highpass=f=2500,afade=t=out:st=0.02:d=0.3,volume=0.6"),
    (r"碎裂|斬|刺入|壓下", "anoisesrc=color=white:amplitude=0.9:d=0.7:r=48000,bandpass=f=1800:w=1500,afade=t=out:st=0.05:d=0.6,volume=0.8"),
    (r"蹄聲|奔跑|掠過", "anoisesrc=color=brown:amplitude=0.8:d=2.5:r=48000,lowpass=f=250,tremolo=f=7:d=0.8,volume=0.7,afade=t=in:d=0.5,afade=t=out:st=1.5:d=1"),
]


def main():
    stills = "--stills" in sys.argv
    os.makedirs(TMP, exist_ok=True)
    parts, voices, subs, sfx, caps = [], [], [], [], []
    music_off, battle_on = [], None
    t, last_end, track_i = 0.0, 0.0, 0
    for s in SHOTS:
        img, crop = image_of(s)
        if not os.path.exists(img):
            sys.exit(f"缺圖 {img}")
        shot_start = t
        cap = CAPTION.get(s["id"])
        if cap:
            caps.append((t + 0.5, t + 4.0, cap))
        lines = []
        for i, l in enumerate([] if stills else s["lines"], 1):
            key = f"{s['id']}_{i}_{l['spk'].replace('（', '').replace('）', '')}"
            m = MAN.get(key)
            if not m or not m.get("file"):
                sys.exit(f"缺配音 {key}")
            f = os.path.join(LF, "voice", m["file"])
            d = base.dur(f)
            st = last_end + l["pre"] - l.get("ovl", 0.0)
            if i == 1:
                st = max(st, shot_start + (4.2 if cap else 0.1))
            lines.append((f, st, d, l))
            last_end = st + d
        sec = s.get("sec", 1.5)
        end = max(shot_start + sec, (last_end + 0.35) if lines else 0)
        sec = round(end - shot_start, 2)
        if stills:
            os.makedirs(os.path.join(HERE, "stills_v5"), exist_ok=True)
            run(["-i", img, "-vf", motion(s["cam"], 0.1, crop, False), "-frames:v", "1",
                 os.path.join(HERE, "stills_v5", f"{s['id']}.png")])
            t = end; continue
        mirror = s["cam"].startswith("Tracking") and track_i % 2 == 1
        track_i += s["cam"].startswith("Tracking")
        part = os.path.join(TMP, f"{s['id']}.mp4")
        run(["-loop", "1", "-i", img, "-t", str(sec), "-vf", motion(s["cam"], sec, crop, mirror),
             "-r", str(FPS), "-c:v", "libx264", "-preset", "veryfast", "-crf", "20", part])
        parts.append(part)
        for f, st, d, l in lines:
            voices.append((f, st))
            subs.append((st, st + d + 0.3, l["spk"], l["text"]))
        note = s.get("sfx", "")
        for pat, flt in SFX:
            if re.search(pat, note):
                sfx.append((flt, shot_start + 0.05))
        if "抽掉" in note or "壓低" in note:
            music_off.append((shot_start, end))
        if "配樂起戰鬥" in note:
            battle_on = shot_start
        if "配樂抽掉" in note and battle_on is not None and shot_start > battle_on:
            music_off.append((shot_start, end + 999))
        t = end
    if stills:
        print("stills →", os.path.join(HERE, "stills_v5")); return
    total = t

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
        for a, b, spk, line in subs:
            f.write(f"Dialogue: 0,{ts(a)},{ts(b)},D,{base.sub_text(line)}\n")
        for a, b, cap in caps:
            f.write(f"Dialogue: 1,{ts(a)},{ts(b)},C,{{\\fad(600,600)\\pos(120,920)\\an1\\fsp26}}"
                    f"{{\\fs48\\fsp18\\c&HC8E1EB&}}{cap['top']}\\N{{\\fs120\\fsp26\\c&HFFFFFF&}}{cap['main']}\n")
    fdir = TMP + "/fonts"
    os.makedirs(fdir, exist_ok=True)
    for x in [base.FONT] + ([base.CAP_FILE] if base.CAP_DIR else []):
        dst = os.path.join(fdir, os.path.basename(x))
        if not os.path.exists(dst):
            shutil.copy(x, dst)
    vfilter = f"subtitles='{ass}':fontsdir='{fdir}',fade=t=in:st=0:d=1,fade=t=out:st={total - 1.5:.2f}:d=1.5"

    ins, fl, labels = ["-i", video], [], []
    k = 1
    for f, st in voices:
        ins += ["-i", f]; ms = int(st * 1000)
        fl.append(f"[{k}:a]aformat=sample_rates=48000:channel_layouts=stereo,adelay={ms}|{ms}[v{k}]"); labels.append(f"[v{k}]"); k += 1
    for j, (flt, st) in enumerate(sfx):
        ms = int(st * 1000)
        fl.append(f"{flt},aformat=sample_rates=48000:channel_layouts=stereo,adelay={ms}|{ms},volume=0.6[s{j}]"); labels.append(f"[s{j}]")
    # 配樂底：低頻雙音嗡鳴；靜音區段用 volume 表達式關掉；戰鬥段加 1.6Hz 心跳鼓
    mute = "+".join(f"between(t,{a:.2f},{b:.2f})" for a, b in music_off) or "0"
    fl.append(f"sine=f=55:d={total}:r=48000,volume=0.10[m1];sine=f=82.5:d={total}:r=48000,volume=0.05[m2];"
              f"[m1][m2]amix=inputs=2:normalize=0,tremolo=f=0.15:d=0.4,volume='if({mute},0,1)':eval=frame,"
              f"aformat=channel_layouts=stereo,afade=t=in:d=2,afade=t=out:st={total - 2}:d=2[mus]")
    labels.append("[mus]")
    if battle_on is not None:
        fl.append(f"anoisesrc=color=brown:amplitude=1:d={total}:r=48000,lowpass=f=70,"
                  f"volume='if(lt(mod(t,0.62),0.12)*gte(t,{battle_on:.2f})*not({mute}),1.4,0)':eval=frame,"
                  f"aformat=channel_layouts=stereo[drm]")
        labels.append("[drm]")
    fl.append("".join(labels) + f"amix=inputs={len(labels)}:normalize=0:duration=longest,atrim=0:{total},"
              "alimiter=limit=0.9[aout]")
    run(ins + ["-filter_complex", f"[0:v]{vfilter}[vout];" + ";".join(fl), "-map", "[vout]", "-map", "[aout]",
               "-c:v", "libx264", "-preset", "medium", "-crf", "27", "-c:a", "aac", "-b:a", "160k",
               "-movflags", "+faststart", OUT])
    print(f"✓ {OUT}（{total:.0f} 秒，{len(parts)} 鏡，{len(voices)} 句配音，{len(sfx)} 個音效）")


if __name__ == "__main__":
    main()
