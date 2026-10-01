#!/usr/bin/env python3
"""LF01 第 12 場台詞補強 v1.2：Kaggle 第 7 版配音回收 → 語音辨識比對 → 挑選 → 混音 → 填 scene12_v2_manifest.json。

  python3 scene12_v2_process.py decode <kaggle_log.json>   解碼執行紀錄裡的 @@MP3 → scene12-v2/raw/
  python3 scene12_v2_process.py asr                         gpt-4o-transcribe 逐檔辨識 → scene12-v2/asr.json
  python3 scene12_v2_process.py pick                        每句挑辨識最準的候選（同分取角色選定種子）→ 混音 → final/
需要 ffmpeg、opencc（pip install opencc-python-reimplemented）、requests；OpenAI 經代理自動帶金鑰。
"""
import base64, difflib, glob, json, os, re, shutil, subprocess, sys

HERE = os.path.dirname(os.path.abspath(__file__))
D = os.path.join(HERE, "scene12-v2")
RAW, FIN = os.path.join(D, "raw"), os.path.join(D, "final")
MAN = os.path.join(HERE, "scene12_v2_manifest.json")
try:
    FF = shutil.which("ffmpeg") or __import__("imageio_ffmpeg").get_ffmpeg_exe()
except ImportError:
    sys.exit("找不到 ffmpeg")

# 角色已選定的種子（selection.json）；辨識同分時優先
PREF = {"旁白": 11, "机遙": 33, "机遙（心聲）": 33, "郁岑燁": 22, "厲若楓": 11, "江祈璟": 11, "聞人澈": 11}
MIX = ("equalizer=f=110:t=q:w=1:g=5,equalizer=f=3500:t=q:w=1.5:g=2,"
       "acompressor=threshold=-20dB:ratio=4:attack=5:release=120:makeup=4,aecho=0.8:0.6:60|120:0.25|0.15")
# 心聲：貼近耳邊（去掉房間感、稍暖、輕殘響），整體略小聲
MIX_INNER = ("highpass=f=80,equalizer=f=200:t=q:w=1:g=3,equalizer=f=3500:t=q:w=1.5:g=-1,"
             "acompressor=threshold=-22dB:ratio=3:attack=5:release=150:makeup=3,aecho=0.8:0.5:35|70:0.2|0.12")


def norm(t):
    import opencc
    t = opencc.OpenCC("t2s").convert(t)
    return re.sub(r"[^\w]", "", t)


def decode(log):
    os.makedirs(RAW, exist_ok=True)
    j = json.load(open(log))
    txt = j.get("log", "")
    try:
        txt = "".join(e.get("data", "") for e in json.loads(txt))
    except Exception:
        pass
    n = 0
    for name, b in re.findall(r"@@MP3 (\S+\.mp3) ([A-Za-z0-9+/=]+)", txt):
        open(os.path.join(RAW, name), "wb").write(base64.b64decode(b)); n += 1
    print("decoded", n)


def asr():
    import requests
    out = json.load(open(os.path.join(D, "asr.json"))) if os.path.exists(os.path.join(D, "asr.json")) else {}
    for f in sorted(glob.glob(os.path.join(RAW, "*.mp3"))):
        k = os.path.basename(f)
        if k in out:
            continue
        r = requests.post("https://api.openai.com/v1/audio/transcriptions",
                          headers={"Authorization": "Bearer placeholder"},
                          files={"file": (k, open(f, "rb"), "audio/mpeg")},
                          data={"model": "gpt-4o-transcribe", "language": "zh"}, timeout=120)
        r.raise_for_status()
        out[k] = r.json()["text"]
        print(k, out[k])
        json.dump(out, open(os.path.join(D, "asr.json"), "w"), ensure_ascii=False, indent=1)


def dur(path):
    r = subprocess.run([FF, "-i", path], capture_output=True, text=True).stderr
    h, m, s = r.split("Duration: ")[1].split(",")[0].split(":")
    return int(h) * 3600 + int(m) * 60 + float(s)


def master(src, dst, inner):
    tmp = dst + ".tmp.wav"
    subprocess.run([FF, "-y", "-loglevel", "error", "-i", src, "-af", MIX_INNER if inner else MIX, tmp], check=True)
    r = subprocess.run([FF, "-i", tmp, "-af", "apad=pad_dur=2,ebur128", "-f", "null", "-"],
                       capture_output=True, text=True).stderr
    i = float(re.findall(r"I:\s+(-?[\d.]+) LUFS", r)[-1])
    target = -19 if inner else -16
    subprocess.run([FF, "-y", "-loglevel", "error", "-i", tmp, "-af",
                    f"volume={target - i:.2f}dB,alimiter=limit=0.84:level=false", "-ac", "1", "-b:a", "128k", dst],
                   check=True)
    os.remove(tmp)


def pick():
    a = json.load(open(os.path.join(D, "asr.json")))
    man = json.load(open(MAN, encoding="utf-8"))
    os.makedirs(FIN, exist_ok=True)
    report = []
    for m in man:
        if m["file"] and not m["file"].startswith("scene12-v2/"):
            continue  # 舊版已定稿的 8 句
        cands = [k for k in a if k.startswith(m["key"] + "_")]
        if not cands:
            report.append((m["key"], "缺候選", "")); continue
        ref = norm(m["text"])
        pref = PREF.get(m["spk"])

        def score(k):
            sim = difflib.SequenceMatcher(None, ref, norm(a[k])).ratio()
            seed = re.search(r"_s(\d+)\.mp3$", k)
            return (round(sim, 2), 1 if seed and int(seed.group(1)) == pref else 0)
        best = max(sorted(cands), key=score)
        sim = score(best)[0]
        dst = os.path.join(FIN, m["key"] + ".mp3")
        master(os.path.join(RAW, best), dst, "心聲" in m["spk"])
        m["file"] = os.path.relpath(dst, HERE)
        m["take"] = best
        m["asr"] = a[best]
        m["sim"] = sim
        report.append((m["key"], f"{sim:.2f}", a[best]))
    json.dump(man, open(MAN, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
    for r in report:
        print(*r, sep="\t")


if __name__ == "__main__":
    {"decode": lambda: decode(sys.argv[2]), "asr": asr, "pick": pick}[sys.argv[1]]()
