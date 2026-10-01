// 由分鏡表＋劇本產生動態分鏡時間軸（timeline.json / timeline.js）。
// 每切一筆：起點、秒數、景別、運鏡、畫面描述、該切的台詞，以及分鏡圖路徑（還沒出圖就是 null → 畫字卡）。
// 分鏡圖放到 storyboard/export/h3/<段號>/f<序>.png 後重跑本腳本即可自動換上。
import { existsSync, readFileSync, writeFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';

const here = dirname(fileURLToPath(import.meta.url));
const root = join(here, '..');
const sb = JSON.parse(readFileSync(join(root, 'storyboard/EP01-storyboard.json'), 'utf8'));
const sc = JSON.parse(readFileSync(join(root, 'script/EP01-script.json'), 'utf8'));
const cast = JSON.parse(readFileSync(join(root, 'characters/EP01-cast.json'), 'utf8'));
const art = JSON.parse(readFileSync(join(root, 'art/EP01-art.json'), 'utf8'));
const clips = existsSync(join(here, 'clips.json')) ? JSON.parse(readFileSync(join(here, 'clips.json'), 'utf8')).clips : [];

const names = Object.fromEntries(cast.characters.map(c => [c.id, c.name]));
names.VO = '旁白';
const sceneNames = Object.fromEntries(art.scenes.map(s => [s.id, s.name]));
const propNames = Object.fromEntries(art.props.map(p => [p.id, p.name]));
const scenes = sc.episodes[0].scenes;
const SIZE = { 'extreme-wide': '大遠景', wide: '全景', full: '全景', medium: '中景', 'medium-close': '中近景', close: '近景', 'close-up': '特寫', 'extreme-close': '大特寫' };

const cuts = [];
let t = 0;
for (const seg of sb.episodes[0].segments) {
  const scene = scenes[seg.sceneIndex - 1];
  seg.cuts.forEach((c, i) => {
    const [b0, b1] = c.beats;
    const flow = scene.flow.slice(b0 - 1, b1);
    const img = `storyboard/export/h3/${seg.id}/f${i + 1}.png`;
    cuts.push({
      seg: seg.id, n: i + 1, start: +t.toFixed(3), dur: c.seconds,
      size: SIZE[c.size] || c.size || '', camera: c.camera || '', stability: c.stability || 'stable',
      frame: c.frame || '', shot: c.shot || '',
      actions: flow.filter(b => b.action).map(b => b.action),
      lines: flow.filter(b => b.line).map(b => ({
        who: (names[b.speaker] || b.speaker) + (b.speaker !== 'VO' && /畫外/.test(b.delivery || '') ? '（畫外）' : ''), text: b.line })),
      characters: (c.characters || []).map(id => names[id] || id),
      props: (c.props || []).map(id => propNames[id] || id),
      scene: sceneNames[scene.sceneId] || scene.sceneId,
      sceneSheet: `art/images/${sceneNames[scene.sceneId]}-sheet.png`,
      image: existsSync(join(root, img)) ? img : null,
      clip: (({ file, in: a, out: b, src }) => ({ file, in: a, out: b, src, proxy: file && 'animatic/cache/' + file.split('/').slice(-2).join('_').replace(/\.mp4$/, '.webm') }))(clips.find(x => x.seg === seg.id && x.n === i + 1 && existsSync(join(root, x.file))) || {}),
      music: i === 0 ? seg.music : null,
    });
    t += c.seconds;
  });
}
const timeline = { episode: 'EP01', total: +t.toFixed(3), cuts };
for (const c of cuts) if (!c.clip.file) c.clip = null;
const have = cuts.filter(c => c.image).length, vids = cuts.filter(c => c.clip).length;
writeFileSync(join(here, 'timeline.json'), JSON.stringify(timeline, null, 1));
writeFileSync(join(here, 'timeline.js'), '// 由 build.mjs 產生，勿手改\nwindow.TIMELINE = ' + JSON.stringify(timeline) + ';\n');
console.log(`timeline: ${cuts.length} 切、${t} 秒；分鏡圖 ${have}/${cuts.length}、影片片段 ${vids}/${cuts.length}`);
