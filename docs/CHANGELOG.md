# CHANGELOG — 《九曜：天墟》

## 2026-09-26（第三十六筆，Phase 0 本機實跑驗收：0-D COMPLETE、0-E 除觸控外通過）

- 本機 Unity 6000.5.5f1＋Fusion 2.1.2 (build 2279) 照 `PHASE0_LOCAL_VERIFICATION_RUNBOOK.md` 跑完步驟 0～5。
- **Phase 0-D 標記 COMPLETE**：1 Server + 2 Client 自動測試 PASS、例外 0、Join/Leave 正常；結果寫進
  CLAUDE-REPLY-008。**Phase 0-E** 資料表免重新打包（赤炎 43→18）、目標鎖定、燒怪、死亡重生都通過，
  觸控未測，暫不標 COMPLETE（CLAUDE-NOTE-006）。
- 修正：Fusion 2.1.2 的 `[Rpc]` 在 Mono 打包版丟 `MethodAccessException`（weaver 呼叫 internal 方法）。
  新增 `Core/ClientCommands`（Fusion `SendReliableDataToServer`）處理一次性 Client→Server 命令，接任務改走它；
  命令只送到發送者本人註冊的處理者（API 統一把關）；Host 自己的命令在本機直接處理，sender 是 None 的命令一律丟棄。
  **專案目前不能新增 `[Rpc]`**，是否定為專案標準請 ChatGPT 在 Code Review 裁定。
- 審查：三方審查（ChatGPT／DeepSeek／GLM＋獨立 Claude 程式審查兩輪＋文件查證兩輪；Codex 登入過期、Gemini 503
  未參與），抓到中間版本（請求放進輸入結構）的重送 bug、Host sender 推定無法證明安全，都已修正並重跑；
  補測 Host＋遠端 Client；文件寫得比證據樂觀的地方已修正。
- 修正：測試腳本在 PowerShell 5.1／中文路徑可用（csproj 改 UTF-8）；`Player left` 檢查改在結束前 35 秒砍 client2；
  `validate_tables.py` 讀得懂 Unity 折行的長字串（CI 誤報）。
- 新增：debug HUD 顯示目前武器（咖哩手感測試回報看不出拿什麼武器）；箭矢命中 log（技術債 D20），補上弓的命中證據。
- Commit Phase0DSetup 產生的任務資產、測試怪 prefab、`Phase0D_TestScene`、Build Settings。

## 2026-09-25（第三十五筆，離線檢查工具化＋GitHub Actions）

- 把雲端 session 一直手動做的離線編譯檢查整理進 repo：`unity/JiuyaoTianxu/Tools/CompileCheck/compile_check.sh`
  （自動下載 Roslyn、UnityEngine 2021.3 與 UnityEditor 參考組件並快取；runtime＋Editor 腳本都檢查；
  已知的 2018 參考組件 PrefabUtility 誤報會被排除；`--self-test` 確認故意寫錯的程式碼一定會被抓到）。
- `Tools/run_all_checks.sh`：離線編譯＋QuestLogic／ConfigTable（含 validate_tables）／Controls 三組測試一次跑完。
- `.github/workflows/unity-offline-checks.yml`：PR 與 main 有動到 `unity/JiuyaoTianxu/**` 時自動跑上述檢查
  （ubuntu-24.04＋apt mono-devel，參考組件有快取）。**只證明能編譯、測試通過，不取代本機 Unity 驗收。**
- CLAUDE.md、README 更新使用方式。

## 2026-09-25（第三十四筆，Phase 0 本機驗收操作手冊）

- 新增 `docs/PHASE0_LOCAL_VERIFICATION_RUNBOOK.md`：PR #5～#10 都只做過離線編譯與單元測試，這份手冊把
  0-D、0-E、技術自審修正（赤炎燒怪、死亡重生、InputMode 資料化等）排成一次跑完的順序，內容包括：單元測試 → Editor 設定 →
  1 Server + 2 Client 自動測試（每項報表的期望值）→ 不重新打包改數值 → 手感檢查 → 回報格式 → 常見失敗處理；
  附錄是可直接貼給 ChatGPT 的審查請求。
- CLAUDE.md 加上提示：有 Unity 的 session 先跑這份手冊。

## 2026-09-25（第三十三筆，死亡／重生測試版＋死亡目標不可命中（技術自審 D3 測試版、D11））

- 咖哩同意先做測試版。`Health` 新增 `IsDead`、`ServerRestoreFull()`；新增 `Gameplay/World/PlayerLifecycle`：
  HP 0 倒地 5 秒（可調整）→ 在自己第一次出生的位置滿血復活（`NetworkTransform.Teleport`），無懲罰。
- 倒地期間 CombatController／PlayerMovement／TargetLock／SpiritSealSystem 不接受輸入（持續記錄按鍵避免
  復活誤觸），鎖定解除、燃燒停止、模型隱藏；HUD 顯示復活倒數。
- `HitDetectionService` 近戰／施法範圍／箭矢查詢略過死亡目標（D11）。
- Setup 把 PlayerLifecycle 加到 Player prefab；`run_autotest.ps1` 新增 player down／respawned 計數。
- 影響：0-C 的玄甲觸發次數會比當初少（HP 0 不再被反覆攻擊），README 已說明；0-C 已知問題 1 解除。
- **正式死亡規則仍待 HANDOFF。** 離線編譯 0 error；單元測試全過；未在 Unity 實跑。

## 2026-09-25（第三十二筆，技術自審 P1/P2 清理：D7/D8/D10/D13/D15）

- D7：`HitDetectionService` 緩衝 16→64，滿格時警告。
- D8：Projectile 改用新的 `HitDetectionService.TryFindFirstHealth`（NonAlloc、不配置 List），不再自己呼叫
  會配置記憶體的 `Physics.OverlapSphere`；命中規則不變（第一個不是射手的 Health）。
- D10：新增 `Core/GameLog`（`[Conditional]` Editor／DEVELOPMENT_BUILD／JIUYAO_GAMELOG），戰鬥／靈印／任務／
  鎖定／怪物的逐事件 log 改用它，release build 連字串組合一起剝除；Phase 0 驗收用 Development build，log 不變。
- D13：出生點改分配「目前沒人佔用的最小編號」，離開時釋放，避免重新加入時疊在別人身上。
- D15：`CombatState.Spawned` 只在 state authority 寫同步屬性。
- 剩餘待裁定：D3 死亡/重生、D4 延遲補償、D5 Client 預測、D9 擊退碰撞、D11 死亡目標命中（隨 D3）、D12 房間人數。
- 離線編譯 0 error；三組單元測試全過。

## 2026-09-25（第三十一筆，技術自審 D1：赤炎燃燒改為 BurnStatus 元件，怪物也會燃燒）

- 咖哩裁定方案 A。新增 `Combat/Framework/StatusEffects/BurnStatus.cs`：只處理燃燒的最小 NetworkBehaviour
  （不是 Status Effect Framework），掛在玩家與怪物 prefab；燃燒傷害仍走 `DamageService`（`IsStatusDamage`），
  燒死的擊殺照樣發 `CombatEvents.TargetKilled` 並歸給施放者。
- `SpiritSealSystem` 移除自帶的燃燒狀態；赤炎命中時目標若沒有 `BurnStatus` 就**不觸發、不消耗冷卻**
  （修正前對怪物靜默失效但冷卻照扣）。
- `Phase0ANetworkSetup`／`Phase0DSetup`／`Phase0ESetup` 把 BurnStatus 加到 prefab；`run_autotest.ps1` 新增
  「赤炎 burned a monster」計數。離線編譯 0 error。**需本機重跑 0-C 靈印 regression。**

## 2026-09-25（第三十筆，Phase 0 技術自審＋低風險修正）

- 新增 `docs/PHASE0_TECH_REVIEW_CLAUDE_V1.0.md`：HANDOFF-008 §17「Phase 0 全面 Code Review」的 Claude 預審版
  （架構現況、P0/P1/P2 技術債 17 項、手機效能初步觀察、Phase 1 前建議順序、待決問題），供 ChatGPT 審查。
- 依自審順手修正（行為不變或純防護）：
  - F1：虛擬搖桿改用 `Application.isMobilePlatform` 判斷顯示（觸控筆電不再誤開、滑鼠攻擊不再被關）。
  - F2：新增 `Core/InputSanitizer`，Server 清洗 Client 搖桿值（NaN/Infinity→0、長度夾到 1，防座標污染與加速外掛）。
  - F3（D2）：`AttackDefinition.InputMode`（Tap/HoldRelease/Cast）取代 CombatController 依武器類型判斷蓄力/施法；
    attacks.csv 加欄、13 筆資產同步。
  - F4（D6）：新增 `monsters.csv`，怪物 MaxHp/DespawnDelay 由匯入器寫入 prefab，Phase0DSetup 不再寫死數值。
- **需要裁定、尚未動手**：D1 赤炎燃燒對怪物無效、D3 死亡/重生、D4 延遲補償、D5 Client 預測（詳見自審文件）。
- 測試：ConfigTableTests 37/37（含實際 monsters.csv 綁定）、ControlsTests 31/31、QuestLogicTests 46/46、
  validate_tables OK（含 InputMode 與資產一致、任務目標必須是表內怪物）；離線編譯 0 error。

## 2026-09-25（第二十九筆，Phase 0-E：Roadmap Phase 0 缺口補齊——資料表／雙搖桿／目標鎖定／基礎回饋）

- 咖哩交代等 Unity 環境期間「先繼續做其他的」；對照 `19_DEVELOPMENT_ROADMAP` Phase 0 交付／驗收項目，
  補上 0-A～0-D 尚缺的部分（非 ChatGPT HANDOFF，紀錄見 bridge CLAUDE-NOTE-006）。
- **資料表**：`Assets/_Project/Config/Tables/{attacks,weapons,spirit_seals,quests}.csv` 成為所有 Phase 0
  數值的來源（由現有資產數值轉出，行為不變）；純 C# `CsvTable`/`TableBinder`；Editor `ConfigTableImporter`
  依鍵原地更新資產；Phase0B/C/D setup 改讀表；Server 執行期可從 `StreamingAssets/ConfigOverrides` 或
  `-configdir` 覆寫數值，不必重新打包（對應 Roadmap「不需重新編譯即可切換測試內容」）。
- **雙搖桿／目標鎖定**：`PlayerInputData` 加 `Aim`、`LockOn`；Server 面向規則（瞄準＞鎖定＞移動＞維持）；
  `TargetLock` Server 權威鎖定（最佳→下一個→解除，死亡/超距自動解除）；`HitDetectionService.FindHealthInRadius`。
- **觸控與回饋（IMGUI 原型）**：`VirtualControlsOverlay` 虛擬搖桿、`HealthFeedback` 受擊閃紅＋傷害數字、
  `CombatHudOverlay` 血條/鎖定標記/debug 任務清單、`LocalPlayerCameraFollow`；`Phase0ESetup` 把元件加到
  prefab 與兩個測試場景（Phase0DSetup 會自動呼叫）。
- `-autotest` 預設節奏不變；新增 opt-in `-autotest-lockon`、`-configdir`；`run_autotest.ps1` 加 `-LockOn`、`-ConfigDir`。
- 驗證（雲端無 Unity）：離線編譯 0 error；ConfigTableTests 34/34、ControlsTests 27/27、QuestLogicTests 46/46、
  `validate_tables.py` OK。**Unity 實跑與手感測試尚未執行。**

## 2026-09-25（第二十八筆，HANDOFF-008：Phase 0-D Map/Spawn/Quest Skeleton 程式碼完成，Unity 實跑驗收待執行）

- 咖哩指示 GPT 生圖暫停期間先做遊戲本體，依 HANDOFF-008 實作 Phase 0-D。
- 新增 `Assets/_Project/Gameplay/`：Events（GameplayEvents、CombatToGameplayEventRouter）、
  World（PlayerSpawnPoint、MonsterSpawnPoint、MonsterSpawner、MonsterLifecycle、EnemyIdentity）、
  Quests（QuestDefinition、QuestRegistry、QuestStateMachine、PlayerQuestLog、QuestTracker、QuestEvents、QuestIds）、
  Testing（Phase0DTestRunner）。
- 核心檔最小擴充：`DamageService` 只在「HP 由 >0 變 0 的那一擊」發 `CombatEvents.TargetKilled`（新增檔），
  傷害計算未改；`NetworkGameLauncher` 支援場景出生點＋啟動 MonsterSpawner（無標記的 Phase0A 場景行為不變）；
  `-autotest` 解析抽到 `Core/CommandLineFlags`（另加 `-quitafter`）；Q 鍵接任務讀取放在 `KeyboardInputProvider`。
- Editor：`Phase0DSetup.Run`（任務資產、Phase0D_TestMonster prefab、Player prefab 加任務元件、
  Phase0D_TestScene、Build Settings）、`Phase0DBuild.Build`。
- 驗證：雲端容器無 Unity，改以 Roslyn（mono）+ UnityEngine 參考組件 + 專案 Fusion DLL 做離線編譯檢查，
  執行期程式碼 0 error；任務邏輯單元測試 `unity/JiuyaoTianxu/Tools/QuestLogicTests` 46/46 通過。
- **尚未完成**：1 Server + 2 Client 實跑、靈印 Regression、≥10 次 Progress 實測——需本機 Unity 執行
  `Tools/Phase0D/run_autotest.ps1`，結果補進 CLAUDE-REPLY-008 後才可標記 COMPLETE。

## 2026-09-24（第二十七筆，MASTER_VISUAL_STYLE_LOCK v1.1：改以 GPT 參考稿為畫風最高標準）

- 起因：EP01 設定圖第一批只靠文字 prompt 出圖，偏寫實照片感，與咖哩選定的 GPT 參考稿落差大，整批作廢。
- `MASTER_VISUAL_STYLE_LOCK_V1.0.md` 升 v1.1（檔名不變以免斷參照），咖哩裁定：
  - 新增第零節「官方畫風樣板」：參考稿（`production/EP01/shuohao/imagegen/refs/raw/`）為畫風最高標準，
    出圖一律附參考稿，文字與參考稿衝突以參考稿為準；武器、配件、疤痕、髮色瞳色等規格仍以文字為準。
  - 第三節：眼睛依參考稿清亮有神，但不到日式動漫大眼。
  - 第六節：設定圖資產（白底半身像＋三視圖＋細節條）例外允許；分鏡圖、影片、宣傳圖仍禁。
  - 第九節：厲若楓放寬為允許兜帽短披肩（兜帽放下）、右肩後東方漆木箭袋、左臂無袖（舊傷可見）。
  - 第十二、十三節：判斷與驗收加入「與參考稿並排比對」。
  - 第二節不展示肌肉（齊衡烈）、蕭曜霖不用毛領：維持不變。
- EP01 角色卡 `EP01-cast.json` 同步修改厲若楓外觀與出圖提示詞，novel-characters 校驗通過。

## 2026-09-24（第二十六筆，EP01 短影音改用 shuohao-skills 製作＋角色聖經代名詞修正）

- EP01〈第七室報到〉改照 shuohao-skills 五段流程製作，產出放在 `production/EP01/shuohao/`；
  docs/30 降為參考，劇情台詞以 docs/25 正史為準。決策紀錄見 `production/EP01/shuohao/DECISIONS.md`。
- `02_CHARACTER_BIBLE_V1.0.md`：厲若楓「與玩家的初步關係鉤子」段落兩處代名詞「她」修正為「他」
  （筆誤；docs/30 與 MASTER_VISUAL_STYLE_LOCK 均為年輕男性，咖哩確認）。

## 2026-09-24（第二十五筆，安裝 shuohao-skills AI短劇製作skill）

- 將第三方開源 skill 集 eternityspring/shuohao-skills（Apache 2.0）內建到
  `.claude/skills/`：novel-outline／novel-characters／novel-art／novel-script／
  novel-storyboard 五段短劇管線（大綱→角色→美術→劇本→分鏡）。
- 放 repo 內而非 `~/.claude/skills`，確保雲端 session 每次都能自動載入；
  來源版本、授權、更新方式記錄於 `.claude/shuohao/README.md`。
- 安裝前檢查腳本無對外連線/無外部依賴，Linux+Node 22 全部自測通過。
- 短影音仍依 `22_TRANSMEDIA_ANIMATION_PLAN` 原則：遊戲為正史，影音衝突時先改影音。
- 預設語言改為繁體中文（台灣用語）：以 `.claude/shuohao/to-traditional.py`（OpenCC s2twp）
  將 skill 指示、腳本介面文字、品質門關鍵字、範例與測試夾具一併轉換，並在各 SKILL.md
  加上繁體輸出規則；轉換後全部自測仍通過。出圖/TTS 提示詞維持英文。

## 2026-09-16（第二十四筆，HANDOFF-007：Phase 0-C完成，靈印Data-driven Prototype驗證通過）

- 執行HANDOFF-007全部完成條件(A~F)，CombatController.cs**零修改**——三個靈印
  的Trigger/Modifier邏輯全部集中在新增的SpiritSealSystem，透過DamageService
  新增的三個通用Hook呼叫，未建立第二套Health/Damage權威入口。
- 新增Spirit Seal Framework（Assets/_Project/Combat/Framework/SpiritSeals/）：
  SpiritSealDefinition(資料驅動ScriptableObject)、SpiritSealRegistry(網路int
  SealId→資產查表)、SpiritSealLoadout(固定8槽NetworkArray純資料)、
  SpiritSealSystem(唯一邏輯，資料迴圈決定觸發，無任何寫死if判斷)。
- 三個Prototype：赤炎(OnAttackHit觸發燃燒DoT，沿用同一條DamageService管線)、
  玄甲(OnFatalDamage封頂致命傷害保命)、影遁(OnDodgeEvent武裝→下次攻擊消耗
  加成)，Cooldown皆以Fusion TickTimer+Runner權威時間為準。
- 1 Server+2 Client headless測試：赤炎20次/玄甲10次/影遁36次武裝26次消耗，
  每項皆超過規定的≥10次、總數遠超≥30次，全程0個Exception，Join/Leave
  regression通過。
- 額外完成Data-driven修改驗收：僅修改赤炎.asset的Cooldown(3→12)，未碰任何
  .cs檔案，重新打包後同一時間窗口觸發次數從14降至5，證實改資料真能改變
  行為，驗證後已改回原型值。
- 除錯過程記錄一個會讓正式Build編譯失敗、之後可能重踩的坑：執行期程式碼
  一度誤引用Editor-only腳本(Phase0CSpiritSealDataSetup)的常數，Editor資料夾
  程式碼不會打包進Player build，已抽出獨立的執行期SpiritSealIds.cs解決，
  並記錄「執行期程式碼不可引用Assets/_Project/Editor/底下任何類別」規則。
- Phase 0-C全部完成，無阻塞項，可進入Phase 0-D（Map/Spawn/Quest Skeleton）。

## 2026-09-16（第二十三筆，HANDOFF-006：Phase 0-B完成，Combat Framework+六大武器MVP驗證通過）

- 執行HANDOFF-006全部14個子階段(0-B-01~14)，嚴格依序不跳步，未觸碰雙修切換/
  完整格擋閃避/完整Boss機制/元素暴擊系統/完整裝備靈印養成/PvP/MMO大世界/公會/
  經濟/商城/抽卡/正式美術動畫/完整手機UI等第11節明確禁止項目。
- 新增Combat Framework（Assets/_Project/Combat/Framework/）：CombatController
  取代Phase0A的PlayerCombat成為唯一戰鬥腳本，六武器差異100%來自
  AttackDefinition/WeaponDefinition(ScriptableObject)資料資產而非程式碼分支；
  HitDetectionService唯一呼叫Physics.Overlap*；DamageService唯一寫入Health；
  CombatState獨立持有網路化狀態；Projectile.cs供弓/杖使用。
- 六大武器MVP：刀(3段近戰+末段重擊擊退)、劍(5段快攻可邊移動)、槍(中距離破甲
  突刺)、弓(Hold-Charge-Release-Projectile)、重刃(慢速大AOE簡單霸體)、
  靈杖(Cast延遲後定點AOE)，在節奏/範圍/機制上明顯不同非僅數值差異。
- 1 Server+2 Client headless自動輪替六武器測試：27次命中(傷害隨武器不同
  8~22)、12次Projectile發射、HP正確遞減clamp於0、Server Authority guard全程
  有效、離線事件正確處理、全程0個Exception/NullReference，Phase0A已驗證的
  連線/移動/同步能力未被破壞。
- 除錯過程記錄三個已修正的測試工具瑕疵(非Framework本身錯誤)：(1)
  AutoTestInputProvider改用Time.frameCount導致與Fusion模擬tick脫鉤，劍的
  連段節奏共振使測試卡在Idle-only切換武器規則上超過30分鐘(log膨脹至4億多
  bytes)，改用NetworkRunner.Tick.Raw(反射工具查證確認存在)解決；(2)測試玩家
  生成朝向未面對面導致近戰打不到人，已修正NetworkGameLauncher讓兩玩家面對面
  生成；(3)靈杖初始Range/AreaRadius相對測試佈局過大，已調整。
- Phase 0-B全部完成，無阻塞項，可進入Phase 0-C（靈印資料驅動原型）。

## 2026-09-16（第二十二筆，HANDOFF-005：Phase 0-A完成，2-Client連線+Server權威戰鬥驗證通過）

- 執行HANDOFF-005第5節「建議執行順序」全部15步，未跳步、未提前擴張範圍（六大
  武器/雙修/完整靈印/裝備/任務/公會/經濟/PvP/副本/世界BOSS/完整劇情/正式美術/
  完整動畫等皆未觸碰，符合第3節明確禁止清單）。
- 新增輸入抽象層（Core/PlayerInputData/KeyboardInputProvider/AutoTestInputProvider）、
  手寫Fusion啟動器（Net/NetworkGameLauncher，未依賴Fusion官方FusionBootstrap範例，
  保持與Photon demo程式碼解耦；完整INetworkRunnerCallbacks實作，19個方法簽章
  皆用反射工具直接讀取Fusion.Runtime.dll查證，非憑訓練記憶猜測）、Server權威
  戰鬥（Combat/Health+PlayerMovement+PlayerCombat，所有狀態修改guard在
  Object.HasStateAuthority）。
- 打包Windows Standalone後以1 Server+2 Client三個獨立headless進程實跑驗證
  （`-autotest`旗標程式化模擬按鍵，因headless進程收不到真實鍵盤輸入）：
  兩Client互相看見對方、324次Attack→Hit→Damage→HP Sync循環（遠超驗收標準10次）、
  HP正確遞減並於0處clamp、手動終止Client後Server正確觸發OnPlayerLeft未崩潰。
- 除錯過程記錄兩個問題：(1) 首次測試用1 Server+1 Client，因Dedicated Server不
  生成本地玩家導致場上只有1名玩家打不到目標，改用1 Server+2 Client排除；
  (2) 發現並修正一個Unity Editor腳本已知坑——`PrefabUtility.SaveAsPrefabAsset`
  回傳的物件參照會在後續`EditorSceneManager.OpenScene`切換場景後失效，即使是
  切換前才重新讀取的參照也一樣，修法為場景開啟後才用`AssetDatabase.LoadAssetAtPath`
  依路徑讀取。
- Phase 0-A全部完成，無阻塞項，可進入Phase 0-B（六大武器最小可玩戰鬥Loop）。

## 2026-09-16（第二十一筆，補入生成式影片模型作為輔助工具）

- 咖哩提出候選：MiniMax H3（本地部署）、Veo 3、Seedance（雲端服務）三款文字/圖片
  生成影片模型，查證後補進`23_ANIMATION_PRODUCTION_PIPELINE_V1.0.md`第2.2節，
  定位為Tier 3輔助工具，不影響既有Tier 1/2工具鏈。
- 明確原因：這類模型是直接生成整段影片，無法保證角色跨鏡頭一致性，牴觸文件既定
  核心原則「固定角色+固定場景+Shot-by-Shot製作」，不能用於需要辨識六位主要角色的
  劇情鏡頭，僅限空鏡/氛圍過場、短影音花絮、前期分鏡概念驗證等不需角色一致性的用途。
- 查證成本（Seedance約US$0.14/秒、MiniMax H3約US$0.13/秒於2K）與授權注意事項
  （MiniMax H3開源權重社群授權明確排除美/歐盟/英/南韓商用；Seedance需走官方API
  或付費訂閱才有明確商用授權）。

## 2026-09-15（第二十筆，HANDOFF-004：動畫Pipeline與AI配音工具技術可行性查證）

- ChatGPT直接新增`23_ANIMATION_PRODUCTION_PIPELINE_V1.0.md`（動畫製作工具鏈：
  Blender+AI動作捕捉+Unity Cinematic）與`23A_AI_VOICE_TOOL_EVALUATION_V1.0.md`
  （5款AI配音工具評估），隨後發HANDOFF-004要求Claude Code獨立查證這兩份草案的
  真實性（不能只憑訓練知識回答，需用WebSearch/WebFetch逐項核對官方定價頁與授權
  文件），並判定Phase 0-A是否可以正式開始。
- 派研究agent查證Blender→Unity資產流程、5款動作捕捉工具（Rokoko/Blender內建/
  Mixamo/DeepMotion/Plask）、Unity Cinematic能力、5款AI配音工具（Chatterbox/
  Fish Speech/ElevenLabs/CosyVoice/Kokoro）共14個維度，查證存檔於
  `docs/HANDOFF-004_RESEARCH_NOTES.md`。
- 查證發現並修正23/23A草案的3處認知落差：(1) Rokoko免費層Video-to-Motion每月僅
  30秒額度、DeepMotion免費層明文禁止商用，23文件先前假設過度樂觀，已修正並補入
  Mixamo（免費商用無額度限制）作為基礎動作庫；(2) Fish Audio官方API按UTF-8
  bytes計費，中文實際成本比英文範例換算貴2~3倍，23A已補充；(3) ElevenLabs
  Creator常態價為US$11/月非草案寫的US$22/月（$22是首月促銷價），23A已修正。
- 查證同時確認23/23A準確的部分（Chatterbox MIT授權、Fish Speech研究授權不可
  商用的警示、CosyVoice授權謹慎提醒、Unity Timeline/Cinemachine免費內建）皆屬實，
  沒有過時或錯誤。
- 最終判定：**Phase 0-A技術上可以正式開始，不構成阻塞**，動畫Pipeline方向正確，
  僅建議EP00預留US$30~50彈性測試預算因應免費額度不足。
- 再次提出延續自HANDOFF-003的未解決問題：16/17仍未訂出手機硬體規格/角色面數
  預算基準，隨跨媒體資產規劃越來越具體，此缺口急迫性提高，已在CLAUDE-REPLY-004
  中向ChatGPT/使用者重申。

## 2026-09-15（第十九筆，HANDOFF-003：赤瞳妖將尺度正式收斂＋跨媒體動畫規格V1.0）

- 使用者同意《九曜：天墟》採「遊戲×動畫×短影音」跨媒體IP方向：遊戲為正史，動畫
  負責補完（不逐字重播遊戲劇情，且不得破壞遊戲主線謎團節奏），兩者衝突時先修動畫；
  遊戲/動畫/PV/短影音優先共用3D資產。
- 正式收斂HANDOFF-002遺留的赤瞳妖將尺度問題：命名區分為「赤瞳妖將・先遣遭遇」
  （第一章，非討伐戰，玩家一行不執行09已定案的多階段破局機制，以撤離/掩護/限制
  為主，最終由BOSS撤退或環境干擾中止戰鬥）與「赤瞳妖將・世界討伐戰」（後續全服
  世界事件，完整保留09已定案的20~60人動態機制），已同步修正 `07_QUEST_DESIGN_V2.0`、
  `21_WORLD_AND_STORY_MASTER`、`09_BOSS_DESIGN`（09僅加註名稱區分說明，未變更任何
  機制數值）。
- 新增 `22_TRANSMEDIA_ANIMATION_PLAN_V1.0.md`：三種敘事層與正史/補完規則、第一季
  「青嵐篇」12集高層骨架（全部為動畫獨有補完視角，不重播遊戲任務流程，不揭露天墟
  最終真相）、共用3D資產規範、對17_TECH_ARCHITECTURE的Cinematic技術需求建議
  （Timeline/表情骨架/鏡頭Rig/資產共用邊界）、短影音四類內容策略。
- 審查發現一個誠實揭露的技術風險（非阻塞，不在本次HANDOFF任務範圍但一併提出）：
  16/17均未訂出手機硬體規格與角色面數/貼圖預算上限，跨媒體共用資產計畫目前無法
  確認動畫端的表情精度/鏡頭需求會不會被誤用在遊戲即時渲染路徑造成手機端效能問題，
  建議後續由17補一份具體手機效能預算基準。

## 2026-09-15（第十八筆，HANDOFF-002：前三章劇情收斂＋07_QUEST_DESIGN_V2.0＋Phase0範圍校正）

- 建立ChatGPT/Claude Code非同步協作機制 `00_AI_HANDOFF_BRIDGE.md`，ChatGPT負責世界觀
  與產品方向、Claude Code負責實作與技術驗證、使用者做最終決策，往返討論紀錄全部保留
  在該檔案不刪除。
- ChatGPT撰寫 `21_WORLD_AND_STORY_MASTER_V1.0.md`世界觀與主線劇情總綱：九曜界底層規則、
  天墟三層真相（表層/中層/長期核心真相：曾被切離的舊界層正在回流）、五大勢力真實立場
  （非簡單善惡二分）、前三章故事主軸與角色長期弧線、真相分層揭露節奏表。
- Claude Code審查（HANDOFF-001回覆）：世界觀名詞層級與既有02/06/08/09/19文件完全相容，
  但發現兩個結構性衝突：(1) 21的Phase0-A/B技術清單只做3流派且誤把雙脈切換排入Phase0，
  跟19已定案的六流派+不含雙脈切換範圍不符；(2) 21新寫的第一章路線(河谷)跟07v1已定案的
  第一章路線(古林,含骨脈相應突破試煉+06已定案境界屏障通行印記)互斥。
- ChatGPT回覆（HANDOFF-001回覆）：問題1全盤接受；問題2提出「07的結構骨架+21的劇情深度」
  混合方案（河谷為古林前置調查區，突破試煉/古林通行印記/古林/陣眼/赤瞳妖將/江祈璟參戰
  全部保留），使用者已核准此方案。
- 執行HANDOFF-002：
  - 修正 `21_WORLD_AND_STORY_MASTER_V1.0.md` 的Phase0-A/B清單與第一章敘事流程，
    使其與19已定案範圍、07新版路線完全對齊。
  - 新建 `07_QUEST_DESIGN_V2.0.md`（取代V1.0作為正式任務規格，V1.0保留為歷史記錄）：
    第一章16節點任務級詳細規格（含目標/玩家行動/NPC動機/對話目的/世界資訊揭露/戰鬥
    內容/獎勵/下一鉤子逐項寫明）、第二章「青嵐異變」與第三章「裂谷回聲」章節骨架
    （分別以08/09已定案的既有怪物朽骨祭司/斷陣裂王作為章節BOSS，未新增怪物或地圖）。
  - 審查過程額外發現並解決一個先前兩份文件都沒明講的規模衝突：09已定案的赤瞳妖將是
    20~60人動態血量全服世界BOSS，07v1/21的第一章版本卻是6人小隊遭遇戰。已在21與
    07_V2中同步明確定調為「同一隻BOSS的兩種規模呈現，第一章只擊退不擊殺，完整版
    另作全服世界事件」，避免後續實作者誤解成第一章要塞一場60人團本。
- 更新CLAUDE.md必讀文件清單，納入21/07_V2/00_AI_HANDOFF_BRIDGE三份新文件。

## 2026-09-15（第十七筆，新增20_COMPANION_SYSTEM，補完夥伴玩法支柱）

- 咖哩指出企劃全程參考《斗羅大陸：史萊克學院》，但02~19共18份文件完全缺漏「夥伴」
  這個核心玩法支柱，屬於中途發現的規劃缺口，非既定範圍變更。
- 經GPT/Gemini/Claude三方討論「新增獨立系統」vs「深度重構02/03核心」兩種整合方案，
  兩模型一致強烈建議前者(小型團隊承受不起後者12份文件的連動修改風險)，咖哩核准後
  採用「新增獨立系統」方向。
- 新增 `20_COMPANION_SYSTEM_V1.0.md`，雙軌並行：星曜獸型夥伴(可捕捉/孵化的原創異獸，
  獨立成長線，戰鬥中僅提供控場/破防/回收等環境輔助，不碰六流派招式與04已定案8槽靈印)
  ＋宿命契友型夥伴(直接用02已定案6位角色，靠07好感度解鎖，利用03已定案雙脈切換
  8秒冷卻視窗觸發羈絆連攜技)。完全不修改02/03/04/05/06/07/12/14既有內容。
- IP原則已擴大明確排除斗羅大陸專有名詞(武魂/魂環/魂骨/魂力/魂師/魂導師)，經Claude
  審查確認GPT與Gemini兩份草稿均未誤用；同時確認兩份草稿都沒有再犯先前反覆出現的
  「發明不存在境界名稱」錯誤，星曜獸成長曲線與05已定案9大境界正確保持獨立。
- 整合時修正Gemini草稿的資質分級命名與04靈印(黃玄地天聖)、10裝備(凡精靈曜帝)品階
  用字重複的問題，改用純T1~T6分級避免玩家混淆三套品質系統。

## 2026-09-15（第十六筆，18份系統設計文件全數完成細節定案）

- `19_DEVELOPMENT_ROADMAP` 完成細節定案：Phase0~4各階段具體交付項目、時間估算、
  驗收標準、風險依賴關係、小型團隊優先級建議(不能砍/可縮減/可延後)。審查發現Gemini
  在Phase4商業化清單寫入「抽卡/抽靈印系統」，正面牴觸01/14已定案的不做抽卡原則，
  已移除；Phase3新增的「3v3競技場」未見於13已定案範圍，已收斂回裂隙封印戰/領地演武。
- **至此，02~19（缺18，已併入01）共18份系統設計文件全數完成細節定案**，
  九曜：天墟企劃階段的完整規劃告一段落。

## 2026-09-15（第十五筆）

- `17_TECH_ARCHITECTURE` 完成細節定案：客戶端專案架構/模組劃分、伺服器端雙迴圈架構
  (呼應03頻率解耦與09韌性條實作)、資料驅動配置表格式與版本管理、後端服務拆分
  (呼應14經濟拍賣場一致性需求)、資安防作弊技術手段。三模型草稿皆無跨題衝突，整合採用。

## 2026-09-15（第十四筆）

- `16_ART_DIRECTION` 完成細節定案：六流派角色美術規範、四分類怪物美術規範、青嵐城
  8子區域與4擴充區域場景美術、色彩系統呼應15 UI配色、5項可執行的原創性查核建議。
  修正GPT草稿對8子區域使用暫定代稱，改用06已定案正確名稱。

## 2026-09-15（第十三筆）

- `15_UI_UX` 完成細節定案：戰鬥介面完整佈局(呼應03/09)、非戰鬥介面設計(角色/背包/
  靈印/修行/社交)、漸進式新手引導原則、無障礙易用性設計、UI視覺風格呼應16美術方向。
  三模型草稿皆無跨題衝突，直接整合。

## 2026-09-15（第十二筆）

- `14_ECONOMY` 完成細節定案：靈砥/曜金各6項產出與消耗管道、拍賣場交易規則(手續費/
  上架限制/防詐騙)、通膨監控指標與分級自動應對機制、月卡具體內容(不影響數值平衡)、
  RMT偵測規則。三模型草稿皆無跨題衝突，直接整合。

## 2026-09-15（第十一筆）

- `13_GUILD_SYSTEM` 完成細節定案：公會建立與階級制度、裂隙封印戰玩法設計(呼應09
  BOSS機制)、駐地建築系統、技能樹設計(不超越個人核心Build/不突破境界壓制)、與12社交
  系統招募管理串接。修正Gemini與DeepSeek再次使用不存在境界名稱(金丹初期/元嬰一重)。

## 2026-09-15（第十筆）

- `12_SOCIAL_SYSTEM` 完成細節定案：好友系統機制、助力印記規則(含防濫用)、院內名冊
  Build配對系統、與07NPC好感度系統雙層獨立不互轉的整合方式、安全區戰鬥區聊天分流。
  三模型草稿皆無跨題衝突，直接整合。

## 2026-09-15（第九筆）

- `11_LIFE_SYSTEM` 完成細節定案：7種生活玩法具體操作機制、生活等級與熟練度設計、
  8子區域資源產出對應、與10裝備/04靈印系統銜接規則、社交經濟價值。修正Gemini再次
  使用不存在境界名稱(練氣期/築基期)、以及擅自替換06已定案8子區域名稱兩個錯誤。

## 2026-09-15（第八筆）

- `09_BOSS_DESIGN` 完成細節定案：赤瞳妖將雙階段完整機制、朽骨祭司/斷陣裂王完整BOSS戰
  設計、副本BOSS韌性條規則、世界BOSS全區分工機制(狂暴倒數/動態血量/流派分工)、
  BOSS掉落對應Build缺口的判定規則。修正Gemini再次混入不存在境界名稱(金丹期/築基期)。

## 2026-09-15（第七筆）

- `08_MONSTER_BIBLE` 完成細節定案：四分類具體怪物設計(含5隻已定案怪物)、異化機制的
  視覺/數值/行為變化規則、圖鑑解鎖加成規則、古林血色陣眼3波清剿設計。審查發現兩模型
  在「怪物剋制武器流派」段落自創「潮拳」等不存在或偏離既定名稱的招式稱呼，
  未使用03已定案的六流派名稱，該段落由Claude直接重寫修正。

## 2026-09-15（第六筆）

- `07_QUEST_DESIGN` 完成細節定案：第一章7個主線任務節點(串起02角色/06地圖屏障/00異化
  伏筆與赤瞳妖將BOSS戰)、六位角色專屬支線任務、5種日常任務範例、任務系統與境界屏障的
  防卡關配合機制(強引導突破/印記即獎勵/軟性引導)、不做抽卡的獎勵設計原則。

## 2026-09-15（第五筆）

- `06_MAP_DESIGN` 完成細節定案：青嵐城8子區域配置、4個長期擴充區域(星辰/浮空/焚天/
  雷劫世界觀命名,避開雪山沙漠刻板印象)、Zone(城鎮野外)與Instance(秘境)切分規則、
  境界硬性屏障跑圖機制(不同於05的PvE傷害壓制,防止蠻力跑深圖)、4種探索型互動設計。

## 2026-09-15（第四筆）

- `10_EQUIPMENT_SYSTEM` 完成細節定案：8槽位設計(呼應04靈印8槽)、5級品質分級(獨立於
  靈印命名)、詞條設計邏輯(嚴禁機制型詞條)、強化保底機制、洗鍊重鑄系統、裝備取得管道
  對應境界(不突破05已定案境界壓制上限)。三模型槽位數量分歧(8/10/10)，採用8槽版本。

## 2026-09-15（第三筆）

- `05_PROGRESSION_SYSTEM` 完成細節定案：9境界修為曲線、6屬性點數分配與洗點機制、
  屬性戰鬥數值轉換邏輯、9個境界各異的突破試煉形式、低境界以小博大的境界壓制上限
  與姿態打破例外機制、青嵐地區(1~6境界)預估45~55小時養成時程。

## 2026-09-15（第二筆）

- `04_SPIRIT_SEAL_SYSTEM` 完成細節定案：8槽位解鎖節點、稀有度分級、融煉升階邏輯、
  8個靈印範例、3組連鎖組合、靈印與雙脈互動規則。審查抓到兩個錯誤並修正：
  1) 模型擅自發明不存在的境界名稱（如破穴境/氣海境/元嬰境）對應槽位解鎖，
     違反「不要擅自改變核心世界觀」規則，已改用已定案的9大境界名稱重新對應；
  2) 其中一版把雙脈設計成各自獨立8槽（變相16槽），與已定案「8槽位」總量衝突，
     已統一為角色共用固定8槽、子效果依當前脈動態加權。

## 2026-09-15

- `03_COMBAT_SYSTEM` 完成細節定案：戰鬥資源條、六流派操作節奏、雙脈切換規則、
  閃避/格擋機制、自動戰鬥AI優先序、手動/自動獎懲設計。過程中Claude審查發現
  「完美閃避判定窗口」（0.05~0.1秒）與已定案的連線同步頻率（10-15Hz）衝突——
  三個模型收斂出解法：伺服器內部戰鬥判定頻率與對外廣播頻率解耦，內部用更高頻率
  （30-60Hz）搭配延遲補償/時間回溯做權威判定，客戶端只送意圖不送結果，
  短窗口手感與伺服器權威可並存不留作弊空間。

## 2026-09-14（第三輪）

- 建立 `02_CHARACTER_BIBLE_V1.0.md`：玩家身份框架（青嵐新契修）、天玄院四人宿舍第一批角色
  （3位舍友+1位編外補靈杖流派+教官+競爭對手，六大武器流派全數有代表）。流程：GPT起草→
  Claude審查發現靈杖流派缺人代表→GPT修正時誤把缺口搬到弓修身上→改採DeepSeek「新增編外
  角色」方案而非「置換既有角色」，避免顧此失彼。
- 建立 `03~19`（缺18，已併入01）共16份系統設計文件的**大綱級定案**：改採兩階段工作流程
  （先全部題目出大綱、Claude一次審查全局找跨題衝突、GPT修正收斂，之後才回頭補細節），
  避免逐題深挖導致效率低落。審查抓出兩個跨題目缺口：
  1) 遊戲引擎完全沒人提到，經審查明確拍板 Unity 2022/2023 LTS + URP
     （因連線架構已定案Photon Fusion，引擎選擇已被間接鎖定）
  2) 經濟系統原方案未區分綁定/可交易貨幣，改採雙幣制（靈砥綁定/曜金可交易）防代練洗幣
  其餘14題三模型方向一致、無跨題衝突，直接採用。

## 2026-09-14（第二輪）

- 連線技術棧正式拍板：**Photon Fusion（Dedicated Server 模式）**，明確排除 Host 模式用於
  正式營運。流程：Claude 提出草稿審查意見（分區/副本人數定義不一致、防作弊立場模糊、
  成本門檻邏輯缺陷）→ GPT-5.2 修正 → 與 Gemini、DeepSeek 交叉驗證收斂 → 咖哩依實際預算
  調整成本門檻數字（改用 NT$500/1000 兩階，符合 Phase 0 自掏腰包測試規模而非新創標準）。
  詳見 `docs/01_ARCHITECTURE_DECISIONS_V1.0.md` 第1節。

## 2026-09-14

- 建立repo `sky03104/jiuyao-tianxu`
- 存檔原始設計總綱 `docs/00_GAME_DESIGN_BIBLE_V1.0.md`（企劃階段，未修改內容）
- 完成第一輪架構審視：Claude審視GDD提出5個缺口/矛盾點（連線架構、裝備靈印分工、
  戰力機制、商業化、戰鬥自動化），與外部模型（GPT）交叉討論後收斂結論，
  寫入 `docs/01_ARCHITECTURE_DECISIONS_V1.0.md`
- 尚未開始任何程式開發，目前仍在 Phase 0 企劃階段
