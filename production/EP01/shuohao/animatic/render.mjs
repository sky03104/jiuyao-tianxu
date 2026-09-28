// EP01 動態分鏡輸出：先跑 build.mjs、audio.mjs，再用 headless Chromium 逐格截圖交給 ffmpeg。
// 用法：node render.mjs                 → EP01_animatic_v1.mp4
//       node render.mjs --stills 1,16,40 → 只輸出指定秒數的 PNG 到 stills/
import { chromium } from 'playwright';
import { spawn, execFileSync } from 'node:child_process';
import { existsSync, mkdirSync, readFileSync, writeFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath, pathToFileURL } from 'node:url';

const here = dirname(fileURLToPath(import.meta.url));
const FPS = 30;
const args = process.argv.slice(2);
const stillsArg = args.includes('--stills') ? args[args.indexOf('--stills') + 1] : null;

execFileSync('node', [join(here, 'build.mjs')], { stdio: 'inherit' });
// 生成影片片段轉 VP9 全關鍵幀暫存檔（Playwright 的 Chromium 不能解 H.264；全關鍵幀讓逐格 seek 精準）
const TLJ = JSON.parse(readFileSync(join(here, 'timeline.json'), 'utf8'));
for (const c of TLJ.cuts.filter(c => c.clip)) {
  const src = join(here, '..', c.clip.file), dst = join(here, '..', c.clip.proxy);
  if (existsSync(dst)) continue;
  mkdirSync(dirname(dst), { recursive: true });
  execFileSync('ffmpeg', ['-y', '-loglevel', 'error', '-i', src, '-an', '-c:v', 'libvpx-vp9', '-g', '1', '-crf', '28', '-b:v', '0', '-deadline', 'good', '-cpu-used', '4', dst], { stdio: 'inherit' });
}
if (!stillsArg) execFileSync('node', [join(here, 'audio.mjs')], { stdio: 'inherit' });
// 配音：依 voice/selection.json 選版本，每句去掉開頭靜音後放在字幕出現的時間點，疊在暫定配樂上 → mix.wav
let AUDIO = join(here, 'music.wav');
const SEL = join(here, '../voice/selection.json');
if (!stillsArg && existsSync(SEL)) {
  const sel = JSON.parse(readFileSync(SEL, 'utf8'));
  const lines = JSON.parse(readFileSync(join(here, '../voice/lines.json'), 'utf8')).lines;
  const TL0 = JSON.parse(readFileSync(join(here, 'timeline.json'), 'utf8'));
  const inputs = ['-i', join(here, 'music.wav')], chains = [];
  let k = 1;
  for (const c of TL0.cuts) c.lines.forEach((l, i) => {
    const id = `${c.seg}_c${c.n}_${i + 1}`, meta = lines.find(x => x.id === id);
    if (!meta) return;
    const f = sel.files?.[id] ? join(here, '../voice', sel.files[id])  // 指定到某一版某個 take
      : join(here, '../voice', sel.version, sel.override[id] || sel.default, `${id}_${meta.name}.wav`);
    if (!existsSync(f)) return;
    const at = c.start + (c.dur / c.lines.length) * i + 0.1;  // 與字幕同一時間點
    inputs.push('-i', f);
    chains.push(`[${k}:a]silenceremove=start_periods=1:start_threshold=-40dB,aresample=44100,pan=stereo|c0=c0|c1=c0,adelay=${Math.round(at * 1000)}:all=1[v${k}]`);
    k++;
  });
  if (k > 1) {
    const mix = join(here, 'mix.wav');
    const fc = `[0:a]volume=0.55[m];${chains.join(';')};[m]${Array.from({ length: k - 1 }, (_, j) => `[v${j + 1}]`).join('')}amix=inputs=${k}:normalize=0:duration=first[out]`;
    execFileSync('ffmpeg', ['-y', '-loglevel', 'error', ...inputs, '-filter_complex', fc, '-map', '[out]', mix], { stdio: 'inherit' });
    AUDIO = mix; console.log(`配音 ${k - 1} 句 → mix.wav`);
  }
}

// 允許 file:// 讀取 repo 內的分鏡圖與設定圖
const browser = await chromium.launch({ args: ['--allow-file-access-from-files'] });
const page = await browser.newPage({ viewport: { width: 1080, height: 1920 } });
await page.goto(pathToFileURL(join(here, 'index.html')).href + '?render=1');
await page.evaluate(async () => { await document.fonts.ready; await window.assetsReady(); });
const grab = async (t, type = 'jpeg') => {
  await page.evaluate(t => window.renderFrame(t), t);
  return page.screenshot({ type, ...(type === 'jpeg' ? { quality: 92 } : {}) });
};

if (stillsArg) {
  mkdirSync(join(here, 'stills'), { recursive: true });
  for (const s of stillsArg.split(',').map(Number)) writeFileSync(join(here, 'stills', `t${String(s).replace('.', '_')}.png`), await grab(s, 'png'));
} else {
  const duration = await page.evaluate(() => window.DURATION);
  const audio = AUDIO;
  const out = join(here, 'EP01_animatic_v1.mp4');
  const ff = spawn('ffmpeg', [
    '-y', '-loglevel', 'error', '-f', 'image2pipe', '-framerate', String(FPS), '-c:v', 'mjpeg', '-i', '-',
    ...(existsSync(audio) ? ['-i', audio, '-c:a', 'aac', '-b:a', '160k', '-shortest'] : []),
    '-c:v', 'libx264', '-pix_fmt', 'yuv420p', '-crf', '22', '-preset', 'medium', '-movflags', '+faststart', out,
  ], { stdio: ['pipe', 'inherit', 'inherit'] });
  const total = Math.round(duration * FPS);
  for (let f = 0; f < total; f++) {
    if (!ff.stdin.write(await grab(f / FPS))) await new Promise(r => ff.stdin.once('drain', r));
    if (f % 300 === 0) console.log(`frame ${f}/${total}`);
  }
  ff.stdin.end();
  await new Promise((res, rej) => ff.on('close', c => (c === 0 ? res() : rej(new Error('ffmpeg exit ' + c)))));
  console.log('done →', out);
}
await browser.close();
