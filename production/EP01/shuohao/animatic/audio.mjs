// EP01 動態分鏡的暫定配樂 music.wav：依 timeline.json 各段的 music／soundscape 描述程式合成，
// 只用來感受節奏，正式版配樂另做。段落對應（分鏡表 music 欄）：
//  E01-01 古琴單音、極慢 → E01-02～04 木質打擊（中慢→中速）→ E01-05 輕弦樂持續音
//  → E01-06 弦樂＋打擊揚起 → E01-07 配樂抽掉、只剩低頻弦樂（蕭曜霖登場）→ E01-08 打擊重新提起
//  → E01-09 最後一聲木質敲擊、靜音、遠處鐘響
import { readFileSync, writeFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';

const here = dirname(fileURLToPath(import.meta.url));
const TL = JSON.parse(readFileSync(join(here, 'timeline.json'), 'utf8'));
const SR = 44100, DUR = TL.total, N = Math.round(SR * DUR);
const L = new Float32Array(N), R = new Float32Array(N);
let seed = 5;
const rnd = () => { seed = (seed * 1664525 + 1013904223) >>> 0; return seed / 4294967296; };
const clamp = (x, a = 0, b = 1) => Math.max(a, Math.min(b, x));
const add = (i, v, pan = 0) => { if (i >= 0 && i < N) { L[i] += v * (1 - pan) * 0.7071; R[i] += v * (1 + pan) * 0.7071; } };

// 各段起訖時間
const segs = {};
for (const c of TL.cuts) {
  const s = segs[c.seg] ||= { start: c.start, end: c.start + c.dur };
  s.end = Math.max(s.end, c.start + c.dur);
}
const S = id => segs[id].start, E = id => segs[id].end;

function pluck(t0, freq, amp = 0.25, pan = 0, decay = 0.996) { // Karplus-Strong 撥弦（古琴感）
  const period = Math.round(SR / freq), buf = new Float32Array(period);
  for (let k = 0; k < period; k++) buf[k] = rnd() * 2 - 1;
  const start = Math.round(t0 * SR); let idx = 0, prev = 0;
  for (let n = 0; n < SR * 4; n++) {
    const cur = buf[idx]; buf[idx] = decay * 0.5 * (cur + buf[(idx + 1) % period]); idx = (idx + 1) % period;
    add(start + n, (0.6 * cur + 0.4 * prev) * amp * Math.min(1, n / 40), pan); prev = cur;
  }
}
function wood(t0, amp = 0.3, pitch = 1, pan = 0) { // 木魚／梆子類木質敲擊
  const start = Math.round(t0 * SR);
  for (let n = 0; n < SR * 0.25; n++) {
    const t = n / SR;
    const v = (Math.sin(2 * Math.PI * 820 * pitch * t) * 0.7 + Math.sin(2 * Math.PI * 1370 * pitch * t) * 0.3) * Math.exp(-t * 38)
      + (rnd() * 2 - 1) * 0.15 * Math.exp(-t * 200);
    add(start + n, v * amp, pan);
  }
}
function pad(t0, t1, freqs, amp, attack = 1, release = 1) { // 弦樂持續音（微走音疊加）
  const a = Math.round(t0 * SR), b = Math.min(N, Math.round(t1 * SR));
  for (let i = a; i < b; i++) {
    const t = i / SR, env = clamp((t - t0) / attack) * clamp((t1 - t) / release);
    let v = 0;
    for (const f of freqs) v += Math.sin(2 * Math.PI * f * t + Math.sin(t * 0.8) * 0.4) + 0.5 * Math.sin(2 * Math.PI * f * 1.003 * t);
    add(i, v * amp * env / freqs.length, 0.2 * Math.sin(t * 0.3));
  }
}
function bell(t0, amp = 0.25, base = 196) { // 遠處鐘響（非諧波分音）
  const start = Math.round(t0 * SR), parts = [[1, 1], [2.76, 0.45], [5.4, 0.2], [0.5, 0.5]];
  for (let n = 0; n < SR * 3.5; n++) {
    const t = n / SR; let v = 0;
    for (const [m, a] of parts) v += a * Math.sin(2 * Math.PI * base * m * t) * Math.exp(-t * (0.9 + m * 0.35));
    add(start + n, v * amp * Math.min(1, n / 80));
  }
}
const NOTE = { D3: 146.83, F3: 174.61, G3: 196, A3: 220, C4: 261.63, D4: 293.66, F4: 349.23, G4: 392, A4: 440 };

// E01-01：古琴單音起頭，極慢；結尾極遠處一聲輕鐘
pad(0, E('E01-01') + 0.5, [73.42, 110], 0.08, 2, 1);
[['D4', 0.3], ['A3', 1.9], ['D4', 3.2]].forEach(([n, t]) => pluck(t, NOTE[n], 0.3, -0.2));
bell(4.1, 0.08, 392);

// 木質打擊：一段一段的節奏型（每拍秒數、強弱）
function groove(t0, t1, beat, pattern, amp) {
  let k = 0;
  for (let t = t0; t < t1 - 0.05; t += beat, k++) {
    const p = pattern[k % pattern.length]; if (!p) continue;
    wood(t, amp * p, p > 0.8 ? 1 : 1.35, (k % 2 ? 0.25 : -0.25));
  }
}
groove(S('E01-02'), E('E01-02'), 0.5, [1, 0, 0.5, 0.4], 0.22);            // 中慢
groove(S('E01-03'), E('E01-04'), 0.4, [1, 0.4, 0.6, 0.4], 0.24);           // 稍亮、中速
pad(S('E01-03'), E('E01-04'), [146.83, 220], 0.03, 1, 1);
[['D4', 0], ['F4', 0.8], ['G4', 1.6]].forEach(([n, d]) => pluck(S('E01-03') + d, NOTE[n], 0.14, 0.3));
// 手掌拍臂環：E01-03 #3 開頭一聲金屬輕響
bell(S('E01-03') + 5.8, 0.05, 880);

// E01-05：極輕的弦樂持續音（厲若楓觀察腳步）
pad(S('E01-05'), E('E01-05') + 0.5, [146.83, 174.61, 220], 0.07, 1.5, 0.8);
// E01-06：弦樂與打擊稍微揚起
pad(S('E01-06'), E('E01-06'), [146.83, 220, 293.66], 0.07, 0.5, 0.3);
groove(S('E01-06'), E('E01-06'), 0.33, [1, 0.5, 0.7, 0.5], 0.26);
// E01-07：配樂抽掉，只剩低頻弦樂；之後沉重規律的腳步
pad(S('E01-07'), E('E01-07'), [55, 82.41], 0.035, 0.3, 0.6);
for (let t = S('E01-07') + 9.6; t < E('E01-07'); t += 0.62) wood(t, 0.12 * clamp((E('E01-07') - t) / 3), 0.35, 0.3);
// E01-08：木質打擊重新提起節奏
groove(S('E01-08'), E('E01-08'), 0.4, [1, 0, 0.6, 0.5], 0.25);
pad(S('E01-08'), E('E01-08'), [146.83, 196], 0.04, 1, 0.5);
// E01-09：最後一聲木質敲擊 → 靜音 → 遠處清亮鐘響
wood(S('E01-09') + 0.1, 0.35, 0.9);
bell(S('E01-09') + 2.9, 0.22, 196);

// 殘響
for (const [ch, d1, d2] of [[L, 1557, 2617], [R, 1617, 2743]]) {
  const src = Float32Array.from(ch);
  for (let i = 0; i < N; i++) ch[i] = src[i] + 0.25 * (i >= d1 ? ch[i - d1] : 0) + 0.18 * (i >= d2 ? ch[i - d2] : 0);
}
let peak = 0; for (let i = 0; i < N; i++) peak = Math.max(peak, Math.abs(L[i]), Math.abs(R[i]));
const g = 0.89 / peak, buf = Buffer.alloc(44 + N * 4);
buf.write('RIFF', 0); buf.writeUInt32LE(36 + N * 4, 4); buf.write('WAVE', 8); buf.write('fmt ', 12);
buf.writeUInt32LE(16, 16); buf.writeUInt16LE(1, 20); buf.writeUInt16LE(2, 22); buf.writeUInt32LE(SR, 24);
buf.writeUInt32LE(SR * 4, 28); buf.writeUInt16LE(4, 32); buf.writeUInt16LE(16, 34); buf.write('data', 36); buf.writeUInt32LE(N * 4, 40);
for (let i = 0; i < N; i++) {
  buf.writeInt16LE(Math.round(clamp(L[i] * g, -1, 1) * 32767), 44 + i * 4);
  buf.writeInt16LE(Math.round(clamp(R[i] * g, -1, 1) * 32767), 46 + i * 4);
}
writeFileSync(join(here, 'music.wav'), buf);
console.log(`music.wav ${DUR}s, peak ${peak.toFixed(3)}`);
