# EP01 shuohao-skills 製作決策紀錄

> 本目錄是用 shuohao-skills 五段流程（outline → characters → art → script → storyboard）
> 製作 EP01〈第七室報到〉的產出。以下決策皆經咖哩確認，後續各段依此執行。

## 2026-09-24　製作基準

| 項目 | 決策 |
|---|---|
| 製作方法 | **照 shuohao-skills 的做法**。docs/30 原本的鏡頭拆法、秒數、版面等是推測性方案，降為參考，不再是規格 |
| 劇情／台詞來源 | docs/25 任務 01（遊戲正史，docs/22）逐字使用；動畫只省略或補畫面，不改寫 |
| 片長 | 暫定 1 集 × 1.25 分鐘（75 秒，skill 容差 ±15%），76 秒僅為參考；劇本段依實際估時再調 |
| 省略的正史內容 | 玩家選項 B/C、任務完成時厲若楓「明天見。」、系統解鎖提示（理由見 outline） |
| 結尾 | 照正史：放下行李、定格第七室門牌；docs/30 的「四人走向演武場」不演 |
| novel-outline 品質門 `major-early` | 單集製作結構性不過，標註不適用，不改 skill 腳本 |
| 角色設定圖版面 | 照 skill（半身像＋全身三視圖＋細節條）。MASTER_VISUAL_STYLE_LOCK 的**畫風**規定（高級東方玄幻 MMORPG 電影級 3D、非真人照片）在出圖層附加；STYLE LOCK 第六節「禁止三視圖」對本流程的角色設定圖資產不適用 |

## 2026-09-24　GPT 生圖參考稿選版

來源：`refs/gpt_draft/GPT_REF_SET_A.png`（16:9 橫格版，下稱 A）、`GPT_REF_SET_B.png`（直格版，下稱 B）。
兩張均為拼貼稿，單格解析度過低（約 600×320／300×520）且燒有文字與 logo，
**只作風格定案與外觀選版，不直接當生產 Reference**；正式參考圖依 novel-characters／novel-art 提示詞重生。

| 資產 | 選用 | 重生時要修正 |
|---|---|---|
| TIANXUAN_ACADEMY_REF_01 天玄院外景 | A | 去除人物，出空景 |
| EAST_CORRIDOR_REF_01 東廊 | B | — |
| ROOM_07_REF_01 第七室門牌 | A/B 皆可 | 影片用無字版，文字後製 |
| PLAYER_REF_01 玩家 | B | 不可用琥珀眼（厲若楓辨識特徵），避免與厲若楓撞臉 |
| QI_HENGLIE_REF_01 齊衡烈 | A | **髮色定為鮮紅**；避免肌肉展示 |
| YU_CENYE_REF_01 郁岑燁 | A | 確認纏布在右手 |
| LI_RUOFENG_REF_01 厲若楓 | A（臉與琥珀眼） | 拿掉兜帽與西式箭筒遊俠感，改三段式短弓 |
| XIAO_YAOLIN_REF_01 蕭曜霖 | A | 補上重刃 |

所有角色需補全身與武器，由 novel-characters 的設定圖指令補齊。
