// 程式合成配樂 music.wav（30 秒、44.1kHz 立體聲），時間點對齊 index.html 的分段：
//  0–4.5 低鳴鋪底 → 4.5–16 撥弦五聲音階（曜脈、契點）→ 16 裂口重擊＋低頻隆隆
//  → 22 撥弦上行（天玄院）→ 26.5 靜默懸念 → 28.4 標題鐘聲
// 撥弦用 Karplus-Strong 演算法，全程不用任何外部音檔。
import { writeFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';

const SR = 44100, DUR = 30, N = SR * DUR;
const L = new Float32Array(N), R = new Float32Array(N);
let seed = 97;
const rnd = () => { seed = (seed * 1664525 + 1013904223) >>> 0; return seed / 4294967296; };
const clamp = (x, a = 0, b = 1) => Math.max(a, Math.min(b, x));
const add = (i, v, pan = 0) => { if (i >= 0 && i < N) { L[i] += v * (1 - pan) * 0.5 * 2 ** 0.5; R[i] += v * (1 + pan) * 0.5 * 2 ** 0.5; } };

// 低鳴鋪底（D 小調根音＋五度），裂口後下沉、懸念處抽掉
for (let i = 0; i < N; i++) {
  const t = i / SR;
  const env = clamp(t / 3) * (1 - 0.8 * clamp((t - 26.3) / 0.4) + 0.8 * clamp((t - 28.3) / 0.3)) * (1 - clamp((t - 29) / 1));
  const detune = t > 16 && t < 22 ? 1 - 0.03 * clamp((t - 16) / 3) : 1;
  const lfo = 0.75 + 0.25 * Math.sin(2 * Math.PI * 0.13 * t);
  const v = (Math.sin(2 * Math.PI * 73.42 * detune * t) + 0.55 * Math.sin(2 * Math.PI * 110 * detune * t)
    + 0.25 * Math.sin(2 * Math.PI * 146.83 * t + Math.sin(t * 0.7))) * 0.07 * env * lfo;
  add(i, v, 0.15 * Math.sin(t * 0.21));
}

// Karplus-Strong 撥弦（近似古琴／箏的顆粒感）
function pluck(t0, freq, amp = 0.25, pan = 0, decay = 0.996) {
  const period = Math.round(SR / freq), buf = new Float32Array(period);
  for (let k = 0; k < period; k++) buf[k] = rnd() * 2 - 1;
  const len = SR * 3.5, start = Math.round(t0 * SR);
  let idx = 0, prev = 0;
  for (let n = 0; n < len; n++) {
    const cur = buf[idx], next = buf[(idx + 1) % period];
    const v = decay * 0.5 * (cur + next);
    buf[idx] = v; idx = (idx + 1) % period;
    const out = 0.6 * cur + 0.4 * prev; prev = cur; // 輕微低通，去掉刺耳高頻
    add(start + n, out * amp * Math.min(1, n / 40), pan);
  }
}
const D = { D3: 146.83, F3: 174.61, G3: 196, A3: 220, C4: 261.63, D4: 293.66, F4: 349.23, G4: 392, A4: 440, C5: 523.25, D5: 587.33 };
// 曜脈一條條亮起：九個音對應九條曜脈的錯開時間（4.6 + i*0.18 附近）
['D4', 'F4', 'G4', 'A4', 'C5', 'A4', 'G4', 'F4', 'D5'].forEach((n, i) => pluck(4.6 + i * 0.36, D[n], 0.2, (i - 4) / 6));
// 維繫生命、修行與空間：慢一點的樂句
[['A3', 7.8], ['C4', 8.4], ['D4', 9.0], ['F4', 9.9], ['D4', 10.5]].forEach(([n, t], i) => pluck(t, D[n], 0.22, i % 2 ? 0.3 : -0.3));
// 契點連結（13.7 秒漣漪）
[['D4', 11.4], ['G4', 12.2], ['A4', 12.9]].forEach(([n, t]) => pluck(t, D[n], 0.2, -0.4));
[D.D5, D.A4, D.D4].forEach((f, k) => pluck(13.7 + k * 0.05, f, 0.26, 0.2, 0.997));
[['C5', 14.6], ['A4', 15.2]].forEach(([n, t]) => pluck(t, D[n], 0.14, 0.4));
// 天玄院：上行
[['D3', 22.6], ['F3', 23.1], ['G3', 23.6], ['A3', 24.1], ['C4', 24.6], ['D4', 25.2], ['A4', 25.8]].forEach(([n, t], i) => pluck(t, D[n], 0.22, (i - 3) / 5));

// 16 秒：裂口重擊（下掃正弦＋雜訊爆裂）與隨後的低頻隆隆
{
  const s0 = 16 * SR;
  for (let n = 0; n < SR * 2.5; n++) {
    const t = n / SR, f = 30 + 60 * Math.exp(-t * 4);
    const ph = 2 * Math.PI * (30 * t + 60 * (1 - Math.exp(-t * 4)) / 4);
    add(s0 + n, Math.sin(ph) * 0.55 * Math.exp(-t * 2.2) + (rnd() * 2 - 1) * 0.3 * Math.exp(-t * 9));
    void f;
  }
  let lp = 0;
  for (let n = 0; n < SR * 6.3; n++) {
    const t = n / SR; lp += 0.02 * ((rnd() * 2 - 1) - lp);
    const env = clamp(t / 0.8) * (1 - clamp((t - 5) / 1.3));
    add(s0 + n, lp * 1.6 * env * (0.7 + 0.3 * Math.sin(t * 5)), Math.sin(t * 1.3) * 0.5);
  }
  // 裂紋碎響
  for (let k = 0; k < 14; k++) {
    const st = Math.round((16.2 + rnd() * 5.5) * SR), pan = rnd() * 2 - 1;
    for (let n = 0; n < 1800; n++) add(st + n, (rnd() * 2 - 1) * 0.12 * Math.exp(-n / 300), pan);
  }
}

// 28.4 秒：標題鐘聲（非諧波分音）
{
  const s0 = Math.round(28.4 * SR), partials = [[1, 1], [2.76, 0.5], [5.4, 0.25], [8.93, 0.12], [0.5, 0.6]];
  for (let n = 0; n < SR * 1.6; n++) {
    const t = n / SR;
    let v = 0; for (const [m, a] of partials) v += a * Math.sin(2 * Math.PI * 146.83 * m * t) * Math.exp(-t * (1.2 + m * 0.4));
    add(s0 + n, v * 0.22 * Math.min(1, n / 60));
  }
}

// 簡易殘響（兩組回授延遲）
for (const [ch, d1, d2] of [[L, 1557, 2617], [R, 1617, 2743]]) {
  const src = Float32Array.from(ch);
  for (let i = 0; i < N; i++) {
    const a = i >= d1 ? ch[i - d1] : 0, b = i >= d2 ? ch[i - d2] : 0;
    ch[i] = src[i] + 0.28 * a + 0.2 * b;
  }
}

// 正規化＋收尾淡出，寫出 16-bit WAV
let peak = 0; for (let i = 0; i < N; i++) peak = Math.max(peak, Math.abs(L[i]), Math.abs(R[i]));
const g = 0.89 / peak;
const buf = Buffer.alloc(44 + N * 4);
buf.write('RIFF', 0); buf.writeUInt32LE(36 + N * 4, 4); buf.write('WAVE', 8); buf.write('fmt ', 12);
buf.writeUInt32LE(16, 16); buf.writeUInt16LE(1, 20); buf.writeUInt16LE(2, 22); buf.writeUInt32LE(SR, 24);
buf.writeUInt32LE(SR * 4, 28); buf.writeUInt16LE(4, 32); buf.writeUInt16LE(16, 34); buf.write('data', 36); buf.writeUInt32LE(N * 4, 40);
for (let i = 0; i < N; i++) {
  const fade = Math.min(1, (N - i) / (SR * 0.5));
  buf.writeInt16LE(Math.round(clamp(L[i] * g * fade, -1, 1) * 32767), 44 + i * 4);
  buf.writeInt16LE(Math.round(clamp(R[i] * g * fade, -1, 1) * 32767), 46 + i * 4);
}
const out = join(dirname(fileURLToPath(import.meta.url)), 'music.wav');
writeFileSync(out, buf);
console.log('wrote', out, 'peak', peak.toFixed(3));
