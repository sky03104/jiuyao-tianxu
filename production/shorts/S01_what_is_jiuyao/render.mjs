// 逐格呼叫 index.html 的 renderFrame(t)，用 headless Chromium 擷取畫面，交給 ffmpeg 合成 MP4。
// 用法：node render.mjs            → 產出 S01_what_is_jiuyao.mp4（先跑 node audio.mjs 產生配樂）
//       node render.mjs --stills 2,8,14,18,24,27.5,29  → 只輸出指定秒數的 PNG 到 stills/，檢查構圖用
import { chromium } from 'playwright';
import { spawn } from 'node:child_process';
import { existsSync, mkdirSync, writeFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath, pathToFileURL } from 'node:url';

const here = dirname(fileURLToPath(import.meta.url));
const FPS = 30;
const args = process.argv.slice(2);
const stillsArg = args.includes('--stills') ? args[args.indexOf('--stills') + 1] : null;

const browser = await chromium.launch({ executablePath: process.env.CHROMIUM_PATH || undefined });
const page = await browser.newPage({ viewport: { width: 1080, height: 1920 } });
await page.goto(pathToFileURL(join(here, 'index.html')).href + '?render=1');
await page.evaluate(() => document.fonts.ready);
const grab = t => page.evaluate(t => { window.renderFrame(t); return document.getElementById('c').toDataURL('image/png').split(',')[1]; }, t);

if (stillsArg) {
  mkdirSync(join(here, 'stills'), { recursive: true });
  for (const s of stillsArg.split(',').map(Number)) {
    writeFileSync(join(here, 'stills', `t${String(s).replace('.', '_')}.png`), Buffer.from(await grab(s), 'base64'));
  }
} else {
  const duration = await page.evaluate(() => window.DURATION);
  const audio = join(here, 'music.wav');
  const out = join(here, 'S01_what_is_jiuyao.mp4');
  const ff = spawn('ffmpeg', [
    '-y', '-f', 'image2pipe', '-framerate', String(FPS), '-i', '-',
    ...(existsSync(audio) ? ['-i', audio, '-c:a', 'aac', '-b:a', '160k', '-shortest'] : []),
    '-c:v', 'libx264', '-pix_fmt', 'yuv420p', '-crf', '20', '-preset', 'medium', '-movflags', '+faststart', out,
  ], { stdio: ['pipe', 'inherit', 'inherit'] });
  const total = Math.round(duration * FPS);
  for (let f = 0; f < total; f++) {
    const buf = Buffer.from(await grab(f / FPS), 'base64');
    if (!ff.stdin.write(buf)) await new Promise(r => ff.stdin.once('drain', r));
    if (f % 90 === 0) console.log(`frame ${f}/${total}`);
  }
  ff.stdin.end();
  await new Promise((res, rej) => ff.on('close', c => (c === 0 ? res() : rej(new Error('ffmpeg exit ' + c)))));
  console.log('done →', out);
}
await browser.close();
