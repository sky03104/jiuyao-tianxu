"""LF01 配樂用的簡易取樣播放器＋撥弦模型（本專案自寫）。

只讀取免費樂器音色庫的錄音檔（FLAC）與其索引 index.json，不執行外部程式碼。
音色庫（皆 CC0 1.0）：VSCO 2 CE、VCSL（Versilian Studios）、Karoryfer 二胡；由 GitHub lemomo-ai/lemo-opuscar 的 assets 發行包下載，
解開後的資料夾設在環境變數 LF01_SAMPLES（預設 /home/user/lemomo-ai/lemo-opuscar/core/audio/instruments）。
音色庫很大（約 740 MB），不進 repo；產生好的配樂檔進 repo。

  note(inst, midi, dur, vel)  有音高的樂器（自動選最近的錄音、移調、長音延長、收尾淡出）
  hit(inst, var, vel)         打擊樂（鑼、大鼓、鈸…）；var 是變體名稱前綴
  pluck(midi, dur, vel, ...)  撥弦物理模型（Karplus-Strong）：古琴／琵琶音色
  add(buf, x, t, gain, pan)   放進立體聲緩衝區
"""
import functools
import json
import os

import numpy as np
import soundfile as sf
import soxr

SR = 48000
ROOT = os.environ.get("LF01_SAMPLES", "/home/user/lemomo-ai/lemo-opuscar/core/audio/instruments")
_rng = np.random.default_rng(12)


@functools.lru_cache(None)
def _index():
    p = os.path.join(ROOT, "index.json")
    if not os.path.exists(p):
        raise SystemExit(f"找不到音色庫 {p}（設定 LF01_SAMPLES，或依 production/LF01/music/README.md 下載）")
    return json.load(open(p))


@functools.lru_cache(None)
def _load(f, on):
    x, sr = sf.read(os.path.join(ROOT, f), dtype="float32", always_2d=True)
    x = x.mean(1)[on:]
    return x, sr


def _zones(inst, var=None):
    zs = _index()[inst]["zones"]
    if var:
        v = [z for z in zs if (z.get("var") or "").startswith(var)]
        zs = v or zs
    return zs


def _vel_pick(zs, vel):
    vs = sorted({z["vel"] or 1 for z in zs})
    target = vs[min(len(vs) - 1, int(round(vel * (len(vs) - 1))))]
    return [z for z in zs if (z["vel"] or 1) == target]


def _extend(x, n):
    """長音不夠長：取錄音中段反覆交叉淡化接上。"""
    if len(x) >= n:
        return x[:n]
    a, b = int(len(x) * .35), int(len(x) * .85)
    seg, fade = x[a:b], min(int(.15 * SR), (b - a) // 3)
    out = x[:b].copy()
    w = np.linspace(0, 1, fade, dtype=np.float32)
    while len(out) < n:
        s = seg * (np.sqrt(np.mean(out[-fade:] ** 2) + 1e-9) / (np.sqrt(np.mean(seg[:fade] ** 2)) + 1e-9))
        out[-fade:] = out[-fade:] * (1 - w) + s[:fade] * w
        out = np.concatenate([out, s[fade:]])
    return out[:n]


def note(inst, midi, dur, vel=.7, release=.8, attack=0.0):
    zs = [z for z in _zones(inst) if z.get("root") is not None]
    zs = _vel_pick(zs, vel)
    best = min(abs(z["root"] - midi) for z in zs)
    near = [z for z in zs if abs(z["root"] - midi) == best]
    z = near[_rng.integers(len(near))]
    x, sr = _load(z["f"], z.get("on") or 0)
    ratio = 2 ** ((midi - z["root"]) / 12)
    y = soxr.resample(x, sr * ratio, SR).astype(np.float32)  # 當作較高取樣率讀 → 播放時音高改變
    n, r = int(dur * SR), int(release * SR)
    y = _extend(y, n + r)
    env = np.ones(len(y), np.float32)
    env[n:] = np.exp(-np.arange(len(y) - n) / (r / 6.9 + 1))
    if attack:
        a = int(attack * SR); env[:a] *= np.linspace(0, 1, a)
    y = y * env
    return y * (0.12 / (z["rms"] + 1e-6)) * (0.35 + 0.65 * vel)


def hit(inst, var=None, vel=.7, dur=None):
    zs = _vel_pick(_zones(inst, var), vel)
    z = zs[_rng.integers(len(zs))]
    x, sr = _load(z["f"], z.get("on") or 0)
    y = soxr.resample(x, sr, SR).astype(np.float32)
    if dur:
        y = y[:int(dur * SR)].copy()
        f = min(len(y), int(.05 * SR)); y[-f:] *= np.linspace(1, 0, f)
    return y * (0.5 / (z["pk"] + 1e-6)) * (0.3 + 0.7 * vel)


def _ks(n, period, t60, bright, pos):
    """Karplus-Strong：延遲線＋平均低通；t60 控制餘韻，bright 控制明亮度。"""
    from numba import njit

    @njit(cache=False)
    def run(buf, n, period, g, b):
        out = np.zeros(n, np.float32)
        L = len(buf)
        i = 0
        for k in range(n):
            v = buf[i]
            out[k] = v
            nxt = buf[(i + 1) % L]
            buf[i] = g * ((1 - b) * 0.5 * (v + nxt) + b * v)
            i = (i + 1) % L
        return out
    L = max(2, int(round(period)))
    exc = _rng.standard_normal(L).astype(np.float32)
    p = max(1, int(L * pos))
    exc = exc - np.roll(exc, p)  # 撥弦位置造成的梳狀濾波
    g = 10 ** (-3 / (t60 * SR / L))
    return run(exc, n, L, np.float32(g), np.float32(bright))


def pluck(midi, dur=3.0, vel=.7, kind="guqin", bend=None):
    """kind=guqin（長餘韻、溫暖）／pipa（明亮、短）。bend=(開始秒, 半音) 做按音上滑。"""
    f = 440 * 2 ** ((midi - 69) / 12)
    t60, bright, pos = (6.0, .08, .18) if kind == "guqin" else (2.2, .35, .1)
    n = int(dur * SR)
    if bend:  # 用較長的延遲線生成後以變速重取樣做滑音
        y = _ks(n + SR, SR / f, t60, bright, pos)
        tt = np.arange(n) / SR
        semi = np.clip((tt - bend[0]) / .35, 0, 1) * bend[1]
        idx = np.cumsum(2 ** (semi / 12))
        y = np.interp(idx, np.arange(len(y)), y).astype(np.float32)
    else:
        y = _ks(n, SR / f, t60, bright, pos)
    y = y / (np.abs(y).max() + 1e-9)
    # 琴身共鳴（約 180 Hz 與 420 Hz 的帶通疊加）
    from scipy.signal import butter, sosfilt
    body = sosfilt(butter(2, [150, 220], "band", fs=SR, output="sos"), y) * 1.2 + \
        sosfilt(butter(2, [380, 480], "band", fs=SR, output="sos"), y) * .6
    y = y * .8 + body.astype(np.float32)
    fade = int(.03 * SR); y[-fade:] *= np.linspace(1, 0, fade)
    return (y / (np.abs(y).max() + 1e-9) * (0.25 + 0.6 * vel)).astype(np.float32)


def add(buf, x, t, gain=1.0, pan=0.0):
    s = int(t * SR)
    if s >= len(buf) or len(x) == 0:
        return
    e = min(len(buf), s + len(x))
    l, r = np.cos((pan + 1) * np.pi / 4), np.sin((pan + 1) * np.pi / 4)
    buf[s:e, 0] += x[:e - s] * gain * l * 1.414
    buf[s:e, 1] += x[:e - s] * gain * r * 1.414
