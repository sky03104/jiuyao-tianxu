#!/usr/bin/env python3
"""LF01 配音批次處理（通用版，由 scene12_v2_process.py 改來）：Kaggle 配音回收 → 語音辨識比對 → 音高篩選 → 挑選 → 混音 → 填配音清單。

  BATCH=batch-v9 python3 voice_batch.py decode <kaggle_log.json>   解碼 @@MP3 → <BATCH>/raw/
  BATCH=batch-v9 python3 voice_batch.py asr                         gpt-4o-transcribe 逐檔辨識 → <BATCH>/asr.json
  BATCH=batch-v9 python3 voice_batch.py pick                        挑選＋混音 → <BATCH>/final/，寫回 scene02/scene12_manifest.json
  （清單裡 file 為空、或已指向本批次的句子才會處理；已定稿的舊配音不動）
需要 ffmpeg、numpy、opencc（pip install opencc-python-reimplemented）、requests；OpenAI 經代理自動帶金鑰。
"""
import base64, difflib, glob, json, os, re, shutil, subprocess, sys

HERE = os.path.dirname(os.path.abspath(__file__))
D = os.path.join(HERE, os.environ.get("BATCH", "batch-v9"))
RAW, FIN = os.path.join(D, "raw"), os.path.join(D, "final")
MANS = [os.path.join(HERE, m) for m in ("scene02_manifest.json", "scene12_manifest.json")]
FEMALE = {"厲若楓", "裴含章", "陸鳴鸞"}  # docs/46；女聲音高篩選方向相反、不降調
MIN_F0_FEMALE = 165
try:
    FF = shutil.which("ffmpeg") or __import__("imageio_ffmpeg").get_ffmpeg_exe()
except ImportError:
    sys.exit("找不到 ffmpeg")

# 角色已選定的種子（selection.json）；辨識同分時優先
PREF = {"旁白": 11, "机遙": 33, "机遙（心聲）": 33, "郁岑燁": 22, "江祈璟": 11, "聞人澈": 11}
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


PITCH_TARGET = 150  # Hz；選定的候選高於此值就降調
MAX_F0 = 165  # Hz；本片角色全是男聲，候選中位音高超過此值多半聽起來像女聲（咖哩 2026-10-01 回報），優先淘汰


def f0(path):
    """粗估中位基頻（自相關，16kHz，只取有聲音框）。"""
    import numpy as np
    raw = subprocess.run([FF, "-loglevel", "error", "-i", path, "-ac", "1", "-ar", "16000", "-f", "s16le", "-"],
                         capture_output=True).stdout
    x = np.frombuffer(raw, np.int16).astype(float) / 32768
    fs, n, res = 16000, 640, []
    for i in range(0, len(x) - n, 320):
        w = x[i:i + n]
        if np.sqrt((w ** 2).mean()) < 0.03:
            continue
        w = w - w.mean()
        ac = np.correlate(w, w, "full")[n - 1:]
        lo, hi = fs // 400, fs // 60
        k = lo + int(np.argmax(ac[lo:hi]))
        if ac[k] > 0.4 * ac[0]:
            res.append(fs / k)
    return float(np.median(res)) if res else 0.0


def dur(path):
    r = subprocess.run([FF, "-i", path], capture_output=True, text=True).stderr
    h, m, s = r.split("Duration: ")[1].split(",")[0].split(":")
    return int(h) * 3600 + int(m) * 60 + float(s)


def master(src, dst, inner, semis=0.0):
    """混音＋響度；semis<0 時先降調（保持長度：asetrate 降頻再 atempo 拉回）。"""
    tmp = dst + ".tmp.wav"
    pre = ""
    if semis:
        r = 2 ** (semis / 12)
        pre = f"aresample=24000,asetrate={24000 * r:.0f},aresample=24000,atempo={1 / r:.4f},"
    subprocess.run([FF, "-y", "-loglevel", "error", "-i", src, "-af", pre + (MIX_INNER if inner else MIX), tmp],
                   check=True)
    r = subprocess.run([FF, "-i", tmp, "-af", "apad=pad_dur=2,ebur128", "-f", "null", "-"],
                       capture_output=True, text=True).stderr
    i = float(re.findall(r"I:\s+(-?[\d.]+) LUFS", r)[-1])
    target = -19 if inner else -16
    subprocess.run([FF, "-y", "-loglevel", "error", "-i", tmp, "-af",
                    f"volume={target - i:.2f}dB,alimiter=limit=0.84:level=false", "-ac", "1", "-b:a", "128k", dst],
                   check=True)
    os.remove(tmp)


def pick():
    for MAN in MANS:
        pick_one(MAN)


def pick_one(MAN):
    a = json.load(open(os.path.join(D, "asr.json")))
    man = json.load(open(MAN, encoding="utf-8"))
    os.makedirs(FIN, exist_ok=True)
    report = []
    batch = os.path.basename(D) + "/"
    for m in man:
        if m["file"] and not m["file"].startswith(batch):
            continue  # 已定稿的舊配音
        # 以「角色＋台詞」對上本批次的候選（清單重編號後 key 可能不同；index.json 記錄送 Kaggle 時的 key→簡體台詞）
        idx = json.load(open(os.path.join(D, "index.json"), encoding="utf-8")) if os.path.exists(os.path.join(D, "index.json")) else {}
        spk = m["key"].split("_")[-1]
        keys = [k for k, t in idx.items() if k.split("_")[-1] == spk and norm(t) == norm(m["text"])] or [m["key"]]
        cands = [k for k in a if any(k.startswith(x + "_") for x in keys)]
        if not cands:
            report.append((m["key"], "缺候選", "")); continue
        ref = norm(m["text"])
        pref = PREF.get(m["spk"])

        def score(k):
            sim = difflib.SequenceMatcher(None, ref, norm(a[k])).ratio()
            seed = re.search(r"_s(\d+)\.mp3$", k)
            return (round(sim, 2), 1 if seed and int(seed.group(1)) == pref else 0)
        pitch = {k: f0(os.path.join(RAW, k)) for k in cands}
        fem = m["spk"] in FEMALE
        male = [k for k in cands if (pitch[k] >= MIN_F0_FEMALE if fem else pitch[k] <= MAX_F0)]
        pool = male or [(max if fem else min)(cands, key=lambda k: pitch[k])]  # 全部不合就取最接近的，並在報告標出
        best = max(sorted(pool), key=score)
        sim = score(best)[0]
        dst = os.path.join(FIN, m["key"] + ".mp3")
        # 仍高於 PITCH_TARGET 的（多為喊叫或新角色），往目標降調，最多 3 個半音
        import math
        semis = -min(3.0, 12 * math.log2(pitch[best] / PITCH_TARGET)) if pitch[best] > PITCH_TARGET and not fem else 0.0
        master(os.path.join(RAW, best), dst, "心聲" in m["spk"], semis)
        m["pitch_shift"] = round(semis, 1)
        m["file"] = os.path.relpath(dst, HERE)
        m["take"] = best
        m["asr"] = a[best]
        m["sim"] = sim
        m["f0"] = round(pitch[best])
        report.append((m["key"], f"{sim:.2f}", f"{pitch[best]:.0f}Hz" + (f" 降{-semis:.1f}半音" if semis else "") + ("" if male else (" ⚠全部偏低" if fem else " ⚠全部偏高")), a[best]))
    json.dump(man, open(MAN, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
    for r in report:
        print(*r, sep="\t")


if __name__ == "__main__":
    {"decode": lambda: decode(sys.argv[2]), "asr": asr, "pick": pick}[sys.argv[1]]()
