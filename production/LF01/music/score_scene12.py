#!/usr/bin/env python3
"""LF01 第 12 場配樂＋音效＋環境聲（v6）：讀 scene12_timeline.json → scene12_bed_v6.mp3。

  python3 production/LF01/animatic/render_shots.py --timeline   # 先產生時間軸
  python3 production/LF01/music/score_scene12.py                # 再產生聲音底
  python3 production/LF01/animatic/render_shots.py              # 合成動態分鏡 v6

規則依 production/roles/05_剪輯.md（聲音三層、兩次安靜、聲音轉場、快慢交錯、不用鋼琴弦樂）。
調式：D 商調五聲（D E G A C）。樂器：古琴／琵琶（撥弦模型）、二胡、低音弦樂、圓號、大鼓、通鼓、框鼓、鑼、鈸、管鐘。
段落（鏡號依 scene12_shots.json）：
  S01–S07 緊張：低音弦持續音＋古琴零星單音＋高音弦震音；獸群蹄聲由遠而近（聲像由左掃到右）
  S08–S10 配樂壓低：只剩低音提琴與定音鼓心跳；妖將沉重腳步
  S11     安靜 1：全部抽掉，第一個聲音是妖將開口
  S14–S29 戰鬥：132 BPM 鼓組（大鼓＋通鼓＋框鼓）＋大提琴跳弓固定音型，三級逐步加厚（圓號→琵琶輪指→二胡＋震音）
  S30     安靜 2：配樂抽掉，光柱熄滅只剩三聲低沉的定音鼓
  S32–S39 餘波：風聲提前 1 秒進（聲音轉場）、鳥叫回來、古琴獨奏＋大提琴墊底、二胡收尾
"""
import json
import os
import subprocess
import sys

import numpy as np
from scipy.signal import butter, sosfilt

HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)
import samples as S  # noqa: E402

SR, add = S.SR, S.add
TL = json.load(open(os.path.join(HERE, "scene12_timeline.json"), encoding="utf-8"))
SH = {s["id"]: s for s in TL["shots"]}
TOTAL = TL["total"] + 0.5
OUT = os.path.join(HERE, "scene12_bed_v6.mp3")
rng = np.random.default_rng(3)
N = int(TOTAL * SR)
mus, sfx, amb = (np.zeros((N, 2), np.float32) for _ in range(3))
D, E, G, A, C = 50, 52, 55, 57, 60  # D3 E3 G3 A3 C4


def t0(i): return SH[i]["t0"]
def t1(i): return SH[i]["t1"]


# ── 合成小音效（本檔自寫）──
def noise(d): return rng.standard_normal(int(d * SR)).astype(np.float32)
def bp(x, lo, hi): return sosfilt(butter(2, [lo, hi], "band", fs=SR, output="sos"), x).astype(np.float32)
def lp(x, f): return sosfilt(butter(2, f, "low", fs=SR, output="sos"), x).astype(np.float32)
def env(d, a, r):
    n = int(d * SR); e = np.ones(n, np.float32)
    na, nr = int(a * SR), int(r * SR)
    if na: e[:na] = np.linspace(0, 1, na)
    if nr: e[-nr:] *= np.linspace(1, 0, nr)
    return e
def norm(x): return x / (np.abs(x).max() + 1e-9)


def rumble(d, v=1.0):
    b = np.cumsum(noise(d)); b -= np.linspace(b[0], b[-1], len(b))
    return norm(lp(b, 120)) * env(d, d * .3, d * .5) * v


def wind(d, v=1.0):
    x = bp(noise(d), 250, 900)
    tt = np.arange(len(x)) / SR
    lfo = .55 + .45 * np.sin(2 * np.pi * .13 * tt + rng.random() * 6) * np.sin(2 * np.pi * .041 * tt)
    return norm(x * lfo) * env(d, 1.0, 1.0) * v


def whoosh(d=.28, v=1.0, lo=800, hi=4000):
    x = noise(d); out = np.zeros_like(x); k = 1200
    for i in range(0, len(x), k):
        f = lo + (hi - lo) * np.sin(np.pi * i / len(x))
        out[i:i + k] = bp(x[max(0, i - 3000):i + k], f * .7, f * 1.3)[-len(out[i:i + k]):]
    return norm(out * np.sin(np.pi * np.arange(len(x)) / len(x)) ** 2) * v


def creak(d=.6, v=1.0):
    tt = np.arange(int(d * SR)) / SR
    f0 = 90 + 40 * tt / d
    saw = 2 * ((np.cumsum(f0) / SR) % 1) - 1
    return norm(bp(saw * (np.abs(np.sin(2 * np.pi * 17 * tt)) ** 4), 500, 3500)) * env(d, .05, .2) * v


def thump(v=1.0, f=55, d=.5):
    tt = np.arange(int(d * SR)) / SR
    return (np.sin(2 * np.pi * f * tt * (1 - .25 * tt)) * np.exp(-tt / .12)).astype(np.float32) * v


def chirp(v=1.0):
    d = .12; tt = np.arange(int(d * SR)) / SR
    f = 3800 + 1600 * np.sin(np.pi * tt / d)
    return (np.sin(2 * np.pi * np.cumsum(f) / SR) * np.sin(np.pi * tt / d) ** 2).astype(np.float32) * v


def hum(d, v=1.0):
    tt = np.arange(int(d * SR)) / SR
    x = sum(np.sin(2 * np.pi * f * tt) * a for f, a in [(110, 1), (165, .5), (220.7, .4), (330, .2)])
    return (x * (0.6 + 0.4 * np.sin(2 * np.pi * 6 * tt)) * env(d, .3, .4) / 2).astype(np.float32) * v


def debris(d=.8, v=1.0):
    out = np.zeros(int(d * SR), np.float32)
    for _ in range(14):
        s = int(rng.random() ** 1.6 * (d - .1) * SR)
        g = bp(noise(.06), 900 + rng.random() * 2500, 5000) * np.exp(-np.arange(int(.06 * SR)) / (.01 * SR))
        out[s:s + len(g)] += g * (.3 + .7 * rng.random())
    return norm(out) * v


# ── 環境聲 ──
add(amb, wind(t0("S11") - 0.0, .22), 0.0)                    # 古林風聲，到妖將開口前抽掉
add(amb, wind(t0("S14") - t0("S12"), .08), t0("S12"))        # 只剩一點點
add(amb, wind(t0("S32") - t0("S30") + 1.0, .10), t0("S30") + 0.8)
add(amb, wind(TOTAL - t0("S32") + 1.0, .30), t0("S32") - 1.0)  # 聲音轉場：風聲比畫面早 1 秒進
for k, dt in enumerate([.4, .55, 1.5, 2.4, 2.55, 4.0, 6.5, 9.0, 12.0]):  # 鳥叫回來
    add(amb, chirp(.5 if k < 3 else .3), t0("S33") + dt, 1, rng.uniform(-.7, .7))

# ── 1. 緊張段 S01–S07 ──
a_end = t0("S08")
add(mus, S.note("contrabass", 38, a_end - 0.5, .5, release=1.5, attack=3), 0.2, .7)
add(mus, S.note("cellos", 45, a_end - 2.5, .4, release=1.2, attack=4), 2.0, .5, -.2)
add(mus, S.note("violins_trem", 81, a_end - t0("S04"), .25, release=1.0, attack=3), t0("S04"), .22, .3)
for t, m, b in [(1.0, D, None), (3.6, A, None), (t0("S04") + .3, C, (.5, 2)), (t0("S05") + .1, D + 12, None),
                (t0("S06") + .4, A, None), (t0("S07") + .1, G, None)]:
    add(mus, S.pluck(m, 4.0, .6, "guqin", b), t, .55, -.15)
# 地鳴＋獸群蹄聲（由遠而近，聲像左→右）
add(sfx, rumble(5.5, .35), 0.2)
add(sfx, S.hit("gran_cassa", "roll", .3, dur=5.0), 0.5, .22)
h0, h1 = 2.0, t1("S03")
t = h0
while t < h1:
    p = (t - h0) / (h1 - h0)
    near = np.sin(np.pi * min(1, p * 1.25)) ** 1.5
    inst, var = ("frame_drum", "large_muted") if rng.random() < .6 else ("toms", "low_mallet")
    add(sfx, S.hit(inst, var, .3 + .5 * near, dur=.4), t, .15 + .55 * near, -0.8 + 1.6 * p)
    t += rng.uniform(.06, .16) * (1.4 - .6 * near)
add(sfx, creak(.7, .5), t0("S07") + .2, 1, .3)                     # 弓弦繃緊

# ── 2. 配樂壓低 S08–S10：只剩低音提琴、定音鼓心跳、妖將腳步 ──
add(mus, S.note("contrabass", 38, t0("S11") - a_end, .35, release=.3), a_end, .45)
t = a_end + .4
while t < t0("S11") - .4:
    add(mus, S.hit("timpani", "drum1", .35, dur=.9), t, .5)
    add(mus, S.hit("timpani", "drum1", .25, dur=.9), t + .28, .35)
    t += 1.05
for t in np.arange(a_end + .3, t0("S11") - .3, 1.45):                # 沉重腳步
    add(sfx, S.hit("bass_drum", None, .55, dur=1.2), t, .55)
    add(sfx, thump(.5, 42), t)
add(sfx, hum(t0("S11") - a_end, .12), a_end)                          # 低頻嗡鳴

# ── 3. S11 安靜（什麼都不放）；S12–S13 只剩極低的弦 ──
add(mus, S.note("contrabass", 37, t0("S14") - t0("S12"), .2, release=.4, attack=2), t0("S12"), .3)
add(sfx, S.hit("gong2", "scrape", .25, dur=3.0), t0("S12") + .3, .2, .4)

# ── 4. 戰鬥 S14–S29 ──
b0, b_end = t0("S14"), t0("S30")
add(sfx, rumble(3.5, .9), b0 - .6)                                    # 聲音轉場：轟鳴比畫面早 0.6 秒
add(sfx, S.hit("gong", None, .9, dur=6), b0, .8)
add(sfx, S.hit("bass_drum", None, 1.0, dur=2), b0, .7)
lv = lambda t: 1 if t < t0("S20") else 2 if t < t0("S26") else 3
beat = 60 / 132 / 2  # 八分音符
OST = [D, D, A - 12, D, C - 12, D, A - 12, G - 12]  # 大提琴跳弓固定音型
k, t = 0, b0 + 1.2
while t < b_end - .05:
    L, i = lv(t), k % 8
    if i in (0, 4):
        add(mus, S.hit("bass_drum", None, .75 if i == 0 else .55, dur=1.0), t, .55)
    if i in (3, 6):
        add(mus, S.hit("toms", "low_mallet", .6, dur=.6), t, .4, -.3)
    if i == 7:
        add(mus, S.hit("toms", "high_mallet", .4, dur=.4), t, .3, .3)
    if L >= 2:
        add(mus, S.hit("frame_drum", "small", .3 + .2 * (i % 2 == 0), dur=.3), t, .25, .4)
    add(mus, S.note("cellos_spic", OST[i], beat * .9, .5 + .1 * L, release=.15), t, .35 + .1 * L, -.15)
    if i == 0 and k % 16 == 0 and L >= 2:
        add(mus, S.note("horn", 50, beat * 10, .55, release=.8, attack=.2), t, .45, .1)
        if L >= 3:
            add(mus, S.note("horn", 57, beat * 10, .5, release=.8, attack=.2), t, .35, .2)
    if i == 0 and k % 16 == 8 and L >= 2:                            # 琵琶輪指
        for j in range(int(1.0 * 14)):
            add(mus, S.pluck([74, 72, 69][(k // 16) % 3], .25, .5, "pipa"), t + j / 14, .25, .35)
    k += 1
    t += beat
add(mus, S.note("violins_trem", 74, b_end - t0("S26"), .5, release=.3, attack=1.0), t0("S26"), .35, .3)
for tt, m, d in [(t0("S26"), 74, 2.2), (t0("S26") + 2.2, 72, 1.4), (t0("S28"), 76, 1.6), (t0("S29"), 74, b_end - t0("S29"))]:
    add(mus, S.note("erhu", m, d, .7, release=.3, attack=.15), tt, .5, -.1)
# 戰鬥音效
add(sfx, S.hit("bass_drum", None, 1.0, dur=1.5), t0("S16") + .1, .8)    # 無形衝擊
add(sfx, S.hit("clash", "short", .8, dur=1.2), t0("S16") + .12, .45)
add(sfx, debris(.9, .5), t0("S16") + .2, 1, .2)
add(sfx, rumble(1.5, .6), t0("S16"))
add(sfx, bp(noise(.8), 300, 2500) * env(.8, .02, .6) * .5, t0("S18") + .1, 1, -.3)  # 急煞、沙土
for j in range(3):                                                        # 三箭連發、彈開
    tt = t0("S19") + .2 + j * .28
    add(sfx, whoosh(.22, .55, 1500, 6000), tt, 1, -.5 + .5 * j)
    add(sfx, S.hit("triangle", "muted", .7, dur=.5), tt + .16, .45, .3)
    add(sfx, S.note("tubular_bells", 76 + j, .1, .35, release=.6), tt + .17, .25, .3)
add(sfx, hum(2.0, .45), t0("S21"))                                        # 法陣嗡鳴
add(sfx, S.hit("bass_drum", None, .9, dur=1.2), t0("S21") + 1.2, .6)
add(sfx, S.hit("sus_cymbal", "scrape", .6, dur=1.5), t0("S26") + .1, .4)  # 鱗甲摩擦
add(sfx, S.note("hand_chimes", 93, .2, .6, release=1.6), t0("S26") + .6, .5, .2)  # 小鈴第二聲
add(sfx, S.note("hand_chimes", 93, .2, .7, release=1.6), t0("S27") + .1, .55, .2)  # 小鈴第三聲
add(sfx, whoosh(.3, .7), t0("S27") + .8, 1, .4)                           # 槍刺入
add(sfx, S.hit("gran_cassa", "hit", .8, dur=1.0), t0("S27") + 1.05, .6)
add(sfx, thump(.6, 70), t0("S27") + 1.05)
add(sfx, whoosh(.25, .8, 1200, 7000), t0("S28") + .05, 1, -.4)           # 刀斬
add(sfx, S.hit("clash", "crash", .9, dur=2.5), t0("S28") + .3, .5)        # 光柱碎裂
add(sfx, debris(1.0, .5), t0("S28") + .3, 1, .3)
add(sfx, S.hit("bass_drum", None, 1.0, dur=1.5), t0("S29") + .2, .8)      # 兵器壓下、紅光噴出
add(sfx, S.hit("gran_cassa", "cresc", .7, dur=b_end - t0("S29")), t0("S29"), .4)
add(sfx, rumble(b_end - t0("S29") + .5, .7), t0("S29"))

# ── 5. S30 安靜 2：配樂抽掉，光柱熄滅三聲 ──
for j in range(3):
    add(sfx, S.hit("timpani", "drum1", .45 - .1 * j, dur=2.0), t0("S30") + .3 + j * 1.0, .55)
    add(sfx, thump(.35 - .08 * j, 48), t0("S30") + .3 + j * 1.0)

# ── 6. 餘波 S32–S39 ──
add(sfx, rumble(3.5, .25), t0("S32"))                                     # 霧合攏
add(mus, S.note("cellos", 50, TOTAL - t0("S34") - 1, .3, release=2.0, attack=3), t0("S34"), .35)
for tt, m, b in [(t0("S34") + .2, D + 12, None), (t0("S35") + .2, A, None), (t0("S36") + .3, G, (.6, 2)),
                 (t0("S38") + .1, D, None), (t0("S39") + 2.0, D + 12, None)]:
    add(mus, S.pluck(m, 4.5, .55, "guqin", b), tt, .6, -.15)
add(mus, S.note("erhu", 69, TOTAL - t0("S38") - 1.2, .5, release=1.5, attack=1.2), t0("S38"), .35, .15)

# ── 台詞下壓低（配樂 -9 dB，戰鬥段 -5 dB；環境 -4 dB；音效不壓）──
def duck(depth_db, battle_db=None):
    """battle_db：戰鬥段（S14–S29）壓得少一點，鼓不要每句都消失。"""
    g = np.ones(N, np.float32)
    for s in TL["shots"]:
        dd = battle_db if battle_db is not None and t0("S14") <= s["t0"] < t0("S30") else depth_db
        for l in s["lines"]:
            a, b = int((l["t0"] - .1) * SR), int((l["t1"] + .15) * SR)
            g[max(0, a):b] = 10 ** (-dd / 20)
    k = int(.25 * SR)  # 平滑
    return np.convolve(g, np.ones(k) / k, "same").astype(np.float32)[:, None]


bed = mus * duck(9, 5) + amb * duck(4) + sfx * .9
f = int(2 * SR); bed[-f:] *= np.linspace(1, 0, f)[:, None]
bed = np.tanh(bed * 1.1) / 1.1
import soundfile as sf  # noqa: E402
tmp = OUT + ".wav"
sf.write(tmp, bed, SR)
FF = __import__("imageio_ffmpeg").get_ffmpeg_exe()
# 聲音底整體約 -23 LUFS（台詞約 -16 LUFS，高出約 7 dB）
subprocess.run([FF, "-y", "-loglevel", "error", "-i", tmp, "-af", "loudnorm=I=-23:TP=-2:LRA=14", "-ar", "48000",
                "-b:a", "192k", OUT], check=True)
os.remove(tmp)
print(f"✓ {OUT}（{TOTAL:.1f} 秒）")
