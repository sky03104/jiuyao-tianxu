# LF01 配樂與音效

> 2026-10-06 建立（咖哩同意採用 GitHub `lemomo-ai/lemo-opuscar` 的導演規則與免費樂器音色庫，選項 A＋B）。

## 檔案
| 檔案 | 說明 |
|---|---|
| `samples.py` | 本專案自寫的取樣播放器＋古琴／琵琶撥弦模型（Karplus-Strong）。只讀音色庫的錄音檔，不執行外部程式碼 |
| `score_scene12.py` | 第 12 場配樂＋音效＋環境聲，依時間軸自動對位、台詞下自動壓低 |
| `scene12_timeline.json` | 時間軸（`animatic/render_shots.py --timeline` 產生，每鏡起訖、每句台詞起訖） |
| `scene12_bed_v6.mp3` | 產生好的聲音底（約 -23 LUFS），`render_shots.py` 直接混進動態分鏡 |

流程：改鏡頭表 → `render_shots.py --timeline` → `score_scene12.py` → `render_shots.py`。

## 音色庫（不進 repo）
音色庫約 740 MB，雲端 session 每次要重新下載（只有改配樂才需要；`scene12_bed_v6.mp3` 已進 repo）：
```sh
GIT_LFS_SKIP_SMUDGE=1 git clone --depth 1 https://github.com/lemomo-ai/lemo-opuscar /home/user/lemomo-ai/lemo-opuscar
cd /home/user/lemomo-ai/lemo-opuscar && for l in vsco2ce vcsl karoryfer; do sh tools/fetch.sh instruments $l; done
```
位置不同就設環境變數 `LF01_SAMPLES` 指到 `core/audio/instruments`。需要 Python 套件 numpy、scipy、soundfile、soxr、numba。

## 授權（片尾 CREDITS 要寫）
| 來源 | 授權 | 本片用到 |
|---|---|---|
| VS Chamber Orchestra: Community Edition（Versilian Studios／Sam Gossner） | CC0 1.0 | 大提琴、低音提琴、高音弦震音、圓號、大鼓、鑼、定音鼓、對鈸 |
| Versilian Community Sample Library（VCSL） | CC0 1.0 | 大鼓 2、通鼓、框鼓、吊鈸、鑼 2、三角鐵、管鐘、手搖鐘 |
| Karoryfer Samples（aliexpress-erhu） | CC0 1.0 | 二胡 |
| 古琴、琵琶 | 本專案程式合成（`samples.py`） | — |

CC0 不強制署名，建議片尾寫一行：`Samples (CC0): Versilian Studios VSCO 2 CE & VCSL, Karoryfer Samples`。
**未使用** Salamander 鋼琴、MuldjordKit 鼓組（這兩個是 CC BY，要署名）。lemo-opuscar 本身是 MIT 授權，本片只參考其文件規則，未複製程式碼。

## 第 12 場聲音設計（v6）
調式 D 商調五聲（D E G A C），不用鋼琴加弦樂（`05_剪輯.md` 第 5 條）。
| 段落 | 環境聲 | 音效 | 配樂 |
|---|---|---|---|
| S01–S07 緊張 | 古林風聲 | 地鳴、獸群蹄聲由左掃到右、弓弦繃緊 | 低音弦持續音＋古琴零星單音＋高音弦震音 |
| S08–S10 壓低 | 風 | 妖將沉重腳步、低頻嗡鳴 | 只剩低音提琴＋定音鼓心跳 |
| **S11 安靜 1** | 無 | 無 | 無（第一個聲音是妖將開口） |
| S12–S13 | 極弱的風 | 鑼刮奏 | 極低的低音提琴 |
| S14–S29 戰鬥 | — | 轟鳴（提前 0.6 秒進＝聲音轉場）、衝擊、碎石、三箭與金屬彈開、法陣嗡鳴、鱗甲摩擦、小鈴二三聲、槍刺入、刀斬、光柱碎裂 | 132 BPM 大鼓＋通鼓＋框鼓、大提琴跳弓固定音型；三級加厚：圓號→琵琶輪指→二胡＋震音 |
| **S30 安靜 2** | 微風 | 光柱熄滅三聲低沉定音鼓 | 抽掉 |
| S32–S39 餘波 | 風聲提前 1 秒進（聲音轉場）、鳥叫回來 | 霧合攏 | 古琴獨奏＋大提琴墊底、二胡收尾 |

台詞下配樂壓低 9 dB（戰鬥段 5 dB，鼓不會每句都消失），環境聲壓 4 dB，音效不壓。
