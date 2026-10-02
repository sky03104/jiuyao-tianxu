#!/usr/bin/env python3
"""LF01 第 12 場動態分鏡：分鏡圖＋運鏡＋配音＋字幕 → MP4。

時間軸全部由 ../lf01.json（段、切、秒數、運鏡、台詞）產生，不手寫時間；分鏡圖或配音更新後重跑即可。
  python3 production/LF01/animatic/render_scene12.py            → LF01_scene12_animatic_v3.mp4（台詞 v1.4）
  python3 production/LF01/animatic/render_scene12.py --stills   → 只輸出每切第一格 PNG 到 stills/（快速檢查）
需要 ffmpeg（含 libass；沒有系統 ffmpeg 時：pip install imageio-ffmpeg）與中文字型（文泉驛正黑或 Noto CJK）。
"""
import json, os, subprocess, sys, shutil, glob

HERE = os.path.dirname(os.path.abspath(__file__))
LF = os.path.dirname(HERE)
FRAMES = os.path.join(LF, "storyboard", "frames")
VOICE = os.path.join(LF, "voice", "final")
MANIFEST = os.path.join(LF, "voice", "scene12_manifest.json")
OUT = os.path.join(HERE, "LF01_scene12_animatic_v3.mp4")
TMP = os.path.join(HERE, "cache")
W, H, FPS = 1920, 1080, 30

try:
    FF = shutil.which("ffmpeg") or __import__("imageio_ffmpeg").get_ffmpeg_exe()
except ImportError:
    sys.exit("找不到 ffmpeg：apt-get install ffmpeg 或 pip install imageio-ffmpeg")
FONT = next((f for f in ["/usr/share/fonts/opentype/noto/NotoSansCJK-Regular.ttc",
                         "/usr/share/fonts/truetype/wqy/wqy-zenhei.ttc"] if os.path.exists(f)), None)
if not FONT:
    sys.exit("找不到中文字型：apt-get install fonts-noto-cjk 或 fonts-wqy-zenhei")
FONT_NAME = "Noto Sans CJK TC" if "Noto" in FONT else "WenQuanYi Zen Hei"

data = json.load(open(os.path.join(LF, "lf01.json"), encoding="utf-8"))
# 畫面文字字型：未選定前退回字幕字型
_cf = (data.get("captionStyle") or {}).get("font")
CAP_FILE = os.path.join(LF, "fonts", _cf) if _cf else None
CAP_DIR = bool(CAP_FILE and os.path.exists(CAP_FILE))
if CAP_DIR:
    from fontTools.ttLib import TTFont
    CAP_FONT = TTFont(CAP_FILE).get("name").getDebugName(1)
else:
    CAP_FONT = None  # 下方用 FONT_NAME 代替
SEGS = [s for s in data["segments"] if s["id"].startswith("B")]


def run(args):
    subprocess.run([FF, "-y", "-loglevel", "error"] + args, check=True)


def voice_files():
    """配音清單（scene12_v2_manifest.json）依序對上 lf01.json B 段每切的台詞；檔名相對 voice/。"""
    man = json.load(open(MANIFEST, encoding="utf-8"))
    out, k = {}, 0
    for s in SEGS:
        for ci, c in enumerate(s["cuts"]):
            for li, (spk, line) in enumerate(c.get("lines", [])):
                m = man[k]; k += 1
                assert m["spk"] == spk and m["text"] == line, f"清單第 {k} 句與 lf01.json 不符：{m['key']}"
                if not m["file"]:
                    sys.exit(f"缺配音 {m['key']}")
                out[(s["id"], ci, li)] = os.path.join(LF, "voice", m["file"])
    assert k == len(man), f"台詞 {k} 句、清單 {len(man)} 句，對不上"
    return out


def sub_text(line, wrap=18):
    """字幕排版（production/roles/06_字幕.md）：句中逗號、句號改全形空格，句尾的句號、逗號去掉；
    問號、驚嘆號、刪節號、引號保留；超過 wrap 字從最靠近中間的空格斷成兩行。"""
    import re
    t = re.sub(r"[，。、；：]+$", "", line.strip())
    t = re.sub(r"[，。、；：]", "　", t)
    t = re.sub(r"　+", "　", t)
    if len(t) > wrap:
        cuts = [i for i, ch in enumerate(t) if ch == "　" or (ch in "？！" and i < len(t) - 1)]
        if cuts:
            i = min(cuts, key=lambda k: abs(k - len(t) / 2))
            t = t[:i + 1].rstrip("　") + "\\N" + t[i + 1:].lstrip("　")
    return t


def dur(path):
    r = subprocess.run([FF, "-i", path], capture_output=True, text=True).stderr
    h, m, s = r.split("Duration: ")[1].split(",")[0].split(":")
    return int(h) * 3600 + int(m) * 60 + float(s)


def motion(camera, sec, mirror_pan):
    """運鏡：Push In 推近 15%；Tracking 橫移；Static 極慢微推 4%。"""
    n = int(sec * FPS)
    if camera.startswith("Push"):
        z, x = f"1+0.15*on/{n}", "iw/2-(iw/zoom/2)"
    elif camera.startswith("Tracking"):
        z = "1.10"
        x = f"(iw-iw/zoom)*(on/{n})" if not mirror_pan else f"(iw-iw/zoom)*(1-on/{n})"
    else:
        z, x = f"1+0.04*on/{n}", "iw/2-(iw/zoom/2)"
    return (f"scale=3840:-2,zoompan=z='{z}':x='{x}':y='ih/2-(ih/zoom/2)':d={n}:s={W}x{H}:fps={FPS},"
            f"setsar=1,format=yuv420p")


def main():
    stills = "--stills" in sys.argv
    os.makedirs(TMP, exist_ok=True)
    vf = {} if stills else voice_files()
    parts, voices, subs, caps, t0, track_i = [], [], [], [], 0.0, 0
    for s in SEGS:
        for ci, c in enumerate(s["cuts"]):
            img = os.path.join(FRAMES, s["id"], f"f{ci + 1}.png")
            # 台詞：切開始後 0.4 秒起依序放，句間 0.35 秒；台詞放不下時把這一切拉長（尾端留 0.6 秒）
            # 有畫面文字（地點卡）的切：文字 0.5～4 秒顯示，台詞等它淡出後才開始，避免和字幕疊在一起
            cap = c.get("caption")
            if cap and not stills:
                caps.append((t0 + 0.5, t0 + 4.0, cap))
            t, lines = t0 + (4.2 if cap else 0.4), []
            for li, (spk, line) in enumerate([] if stills else c.get("lines", [])):
                f = vf[(s["id"], ci, li)]
                d = dur(f)
                lines.append((f, t, d, spk, line))
                t += d + 0.35
            sec = round(max(c["sec"], t - 0.35 + 0.6 - t0), 2)
            if not os.path.exists(img):
                sys.exit(f"缺分鏡圖 {img}")
            if stills:
                os.makedirs(os.path.join(HERE, "stills"), exist_ok=True)
                run(["-i", img, "-vf", f"scale={W}:{H}", "-frames:v", "1",
                     os.path.join(HERE, "stills", f"{s['id']}_f{ci + 1}.png")])
                continue
            mirror = c["camera"].startswith("Tracking") and track_i % 2 == 1
            track_i += c["camera"].startswith("Tracking")
            part = os.path.join(TMP, f"{s['id']}_f{ci + 1}.mp4")
            run(["-loop", "1", "-i", img, "-t", str(sec), "-vf", motion(c["camera"], sec, mirror),
                 "-r", str(FPS), "-c:v", "libx264", "-preset", "veryfast", "-crf", "20", part])
            parts.append(part)
            for f, t, d, spk, line in lines:
                voices.append((f, t))
                subs.append((t, t + d + 0.3, spk, line))
            t0 += sec
    if stills:
        print("stills →", os.path.join(HERE, "stills")); return

    # 1) 串接畫面
    lst = os.path.join(TMP, "list.txt")
    open(lst, "w").write("".join(f"file '{p}'\n" for p in parts))
    video = os.path.join(TMP, "video.mp4")
    run(["-f", "concat", "-safe", "0", "-i", lst, "-c", "copy", video])

    # 2) 字幕（ASS：只有台詞，一律白字同一字型，咖哩 2026-10-02）＋畫面文字（地點卡）＋開頭結尾淡入淡出
    def ts(x):
        return f"{int(x // 3600)}:{int(x % 3600 // 60):02d}:{x % 60:05.2f}"
    ass = os.path.join(TMP, "subs.ass")
    with open(ass, "w", encoding="utf-8") as f:
        f.write("[Script Info]\nScriptType: v4.00+\nPlayResX: %d\nPlayResY: %d\n\n" % (W, H))
        f.write("[V4+ Styles]\nFormat: Name, Fontname, Fontsize, PrimaryColour, OutlineColour, BackColour, Bold, "
                "BorderStyle, Outline, Shadow, Alignment, MarginV\n")
        f.write(f"Style: D,{FONT_NAME},50,&H00FFFFFF,&H00000000,&H80000000,0,1,3,1,2,60\n")
        # 畫面文字（地點卡）：字型由咖哩選定（lf01.json captionStyle.font，字型檔放 production/LF01/fonts/），左下、淡入淡出
        f.write(f"Style: C,{CAP_FONT or FONT_NAME},120,&H00FFFFFF,&H00000000,&H64000000,0,1,0,4,1,140\n\n")
        f.write("[Events]\nFormat: Layer, Start, End, Style, Text\n")
        for a, b, spk, line in subs:
            # 只放台詞，不標說話人（咖哩 2026-10-01）
            f.write(f"Dialogue: 0,{ts(a)},{ts(b)},D,{sub_text(line)}\n")
        for a, b, cap in caps:
            f.write(f"Dialogue: 1,{ts(a)},{ts(b)},C,{{\\fad(600,600)\\pos(120,920)\\an1\\fsp26}}"
                    f"{{\\fs48\\fsp18\\c&HC8E1EB&}}{cap['top']}\\N{{\\fs120\\fsp26\\c&HFFFFFF&}}{cap['main']}\n")
    total = t0
    fdir = os.path.dirname(FONT)
    if caps and CAP_DIR:
        fdir = TMP + "/fonts"
        os.makedirs(fdir, exist_ok=True)
        for x in [FONT, CAP_FILE]:
            dst = os.path.join(fdir, os.path.basename(x))
            if not os.path.exists(dst):
                shutil.copy(x, dst)
    vfilter = (f"subtitles='{ass}':fontsdir='{fdir}',"
               f"fade=t=in:st=0:d=1,fade=t=out:st={total - 1.5:.2f}:d=1.5")

    # 3) 聲音：配音依時間放＋低頻環境底噪（暫定，正式配樂另做）
    ins, fl = ["-i", video], []
    for i, (f, t) in enumerate(voices):
        ins += ["-i", f]
        ms = int(t * 1000)
        fl.append(f"[{i + 1}:a]aformat=sample_rates=48000:channel_layouts=stereo,adelay={ms}|{ms}[v{i}]")
    n = len(voices)
    fl.append(f"anoisesrc=color=brown:amplitude=0.5:d={total}:r=48000,lowpass=f=180,"
              f"volume=0.06,aformat=channel_layouts=stereo,afade=t=in:d=2,afade=t=out:st={total - 2}:d=2[amb]")
    fl.append("".join(f"[v{i}]" for i in range(n)) + f"[amb]amix=inputs={n + 1}:normalize=0:duration=longest,"
              f"atrim=0:{total}[aout]")
    run(ins + ["-filter_complex", f"[0:v]{vfilter}[vout];" + ";".join(fl),
               "-map", "[vout]", "-map", "[aout]", "-c:v", "libx264", "-preset", "medium", "-crf", "27",
               "-c:a", "aac", "-b:a", "160k", "-movflags", "+faststart", OUT])
    print(f"✓ {OUT}（{total:.0f} 秒，{len(parts)} 切，{n} 句配音）")


if __name__ == "__main__":
    main()
