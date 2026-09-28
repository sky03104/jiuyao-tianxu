// EP01 動態分鏡輸出：先跑 build.mjs、audio.mjs，再用 headless Chromium 逐格截圖交給 ffmpeg。
// 用法：node render.mjs                 → EP01_animatic_v0.mp4
//       node render.mjs --stills 1,16,40 → 只輸出指定秒數的 PNG 到 stills/
import { chromium } from 'playwright';
import { spawn, execFileSync } from 'node:child_process';
import { existsSync, mkdirSync, writeFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath, pathToFileURL } from 'node:url';

const here = dirname(fileURLToPath(import.meta.url));
const FPS = 30;
const args = process.argv.slice(2);
const stillsArg = args.includes('--stills') ? args[args.indexOf('--stills') + 1] : null;

execFileSync('node', [join(here, 'build.mjs')], { stdio: 'inherit' });
if (!stillsArg) execFileSync('node', [join(here, 'audio.mjs')], { stdio: 'inherit' });

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
  const audio = join(here, 'music.wav');
  const out = join(here, 'EP01_animatic_v0.mp4');
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
