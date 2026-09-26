# Phase 0 本機驗收操作手冊（一次跑完 0-D／0-E／技術自審修正）

> 對象：有 Unity 6000.5.5f1 的本機 Claude Code session（或咖哩手動）。
> 背景：2026-09-25 在雲端（沒有 Unity）合併了 PR #5～#10。它們都通過了離線編譯和單元測試，
> 但**都還沒在 Unity 實跑**。本手冊把所有待驗收項目排成一次跑完的順序。
> 跑完後把結果寫進 `docs/00_AI_HANDOFF_BRIDGE.md` 的 CLAUDE-REPLY-008（0-D）與 CLAUDE-NOTE-006（0-E）。

所有指令都在 `unity/JiuyaoTianxu` 資料夾執行。`Unity.exe` 指 Unity 6000.5.5f1 的執行檔路徑。

> **2026-09-26 已實跑一次**：0-D COMPLETE，0-E 只差觸控（結果見 CLAUDE-REPLY-008／CLAUDE-NOTE-006）。
> 那次踩到的坑，下次注意：
> - 沒裝 `pwsh`（PowerShell 7）也可以，改用 `powershell -ExecutionPolicy Bypass -File <腳本>`。
> - batchmode 指令執行時，Editor **不能**同時開著這個專案（專案會被鎖住）。
> - 影遁的 log 字樣是 `armed '影遁'`／`consumed armed seal '影遁'`，不是 `triggered`，報表的靈印總數有算進去。
> - Unity Hub 裡九曜是獨立專案（路徑 `jiuyao-tianxu/unity/JiuyaoTianxu`），別開成其他專案。
> - Host 模式（Editor 按 Play 用的模式）不用開 Editor 也能測：
>   `Builds/Phase0D/JiuyaoTianxu.exe -batchmode -nographics -autotest -quitafter 60 -netmode host -logFile Logs/HostTest/host.log`，
>   看 log 裡 `requesting accept` 與 `Available → Accepted` 次數是否相同。
>   要測「Host＋遠端 Client」：Host 用 `-quitafter 80` 先開，8 秒後再開一個 `-netmode client -quitafter 65`，
>   Host log 應該同時有 `[ClientCommands] command 1(...) from [Player:1]` 與 `from [Player:2]`，且沒有 `no handler`。
> - 報表的 `quest accepted` 應等於兩個 Client 的 `requesting accept` 總數（一次請求一次接取），`REJECTED` 應為 0。

---

## 步驟 0：先跑不需要 Unity 的檢查（1 分鐘）

```
pwsh Tools/QuestLogicTests/run.ps1      # 期望 46 passed
pwsh Tools/ConfigTableTests/run.ps1     # 期望 37 passed＋validate_tables: OK
pwsh Tools/ControlsTests/run.ps1        # 期望 31 passed
```

失敗就先停下來，不要進 Unity。（這三個需要 .NET 8 SDK 和 Python；沒有的話可以跳過，雲端已經跑過同樣的測試。）

## 步驟 1：Editor 設定（每一步都要看 log 有沒有 error）

```
Unity.exe -batchmode -projectPath . -executeMethod ConfigTableImporter.ImportAll -quit -logFile Logs/import.log
Unity.exe -batchmode -projectPath . -executeMethod Phase0DSetup.Run -quit -logFile Logs/setup0d.log
Unity.exe -batchmode -projectPath . -executeMethod Phase0DBuild.Build -quit -logFile Logs/build0d.log
```

- `Phase0DSetup` 會自動呼叫 `Phase0ESetup`。它會把 TargetLock／HealthFeedback／**BurnStatus**／**PlayerLifecycle**
  加到 Player prefab，並把 UI 加到兩個測試場景。**不能跳過**，否則赤炎燃燒與死亡重生都不會生效。
- 如果 `Assets/_Project/Net/Prefabs/Player.prefab` 不存在，要先跑 `Phase0ANetworkSetup.Run`。
- 第一次開專案時 Unity 會為新檔案產生／保留 `.meta`，並把新 prefab 註冊進 Fusion prefab table。
  跑完後用 `git status` 看有哪些新檔案（任務資產、怪物 prefab、場景、Build Settings），**連同改過的資產一起 commit**。
- import.log 應出現 `[ConfigTableImporter] Imported all tables: OK.`

## 步驟 2：自動測試（1 Server + 2 Client）

```
pwsh Tools/Phase0D/run_autotest.ps1 -Seconds 150 -LockOn
```

| 報表項目 | 期望 | 驗證的是 |
|---|---|---|
| StartGame succeeded / Player joined | ≥1 / 2 | 0-A 連線沒退步 |
| monsters spawned | ≥3（會持續重生） | 0-D 生怪 |
| EnemyKilled events | >0 | 0-D 戰鬥→事件鏈 |
| quest accepted / progress lines | ≥2 / ≥10 | 0-D 任務 |
| Q_PHASE0D_001 completed | 2（兩名玩家各一次） | 0-D 完整 3 殺循環 |
| Q_PHASE0D_002 unlocked | ≥1 | 0-D Locked→Available |
| PASS line | 1 | 0-D 驗收條件 |
| [TargetLock] lines | >0 | 0-E 鎖定（因為加了 `-LockOn`） |
| 赤炎 burned a monster (D1) | >0 | 赤炎對怪物有效 |
| player down / respawned (D3) | 兩者都 >0 且大致相等 | 死亡重生測試版 |
| 赤炎/玄甲/影遁 regression | 有觸發 | 0-C 沒退步（玄甲會比 0-C 少，屬預期，見 README） |
| client1/client2 QuestSync lines | >0 | 任務狀態同步到 Client |
| Exception/NullReference | **0** | 無例外 |

跑完後 client2 會被砍掉，server.log 應出現 `Player left`（Join/Leave regression）。

## 步驟 3：不重新打包改數值（Roadmap「不需重新編譯」驗收）

1. 建一個資料夾，例如 `Logs/override/`，放一個 `spirit_seals.csv`，內容只有：
   ```
   SealId,Cooldown
   1,12
   ```
2. `pwsh Tools/Phase0D/run_autotest.ps1 -Seconds 150 -LogDir Logs/Phase0D_override -ConfigDir Logs/override`
3. 報表應出現 `[ConfigOverride] applied` > 0；比較兩次 server.log 裡 `triggered '赤炎'` 的次數，
   第二次應明顯較少（冷卻 3→12 秒）。**全程不重新打包。**

## 步驟 4：手感（要人看，約 10 分鐘）

Editor 開 `Assets/_Project/Scenes/Phase0D_TestScene.unity`，按 Play（Host 模式）：

- WASD 移動、IJKL 瞄準（角色會轉向）、F 鎖定（目標頭上出現「▼ 鎖定」，再按一次換目標，最後一次解除）
- Space 攻擊、Tab 換武器、Q 接任務（左上 debug 任務清單會更新）
- 打怪時怪物閃紅、跳傷害數字、頭上血條減少；弓要按住放開、杖有施法延遲（InputMode 資料化後要跟以前一樣）
- 觸控 UI：打包時加 `-touchui` 參數，或直接在手機上跑。**iPhone（沒有 Mac）用網頁版**：
  `powershell -ExecutionPolicy Bypass -File Tools/Phase0D/run_webgl_touchtest.ps1`，手機用 Safari 開它印出的網址
  （選跟手機同一個 Wi-Fi 的那個）、轉橫的測；細節見 README「手機觸控測試（網頁版）」。

有覺得不對的地方都記下來：轉向太快或太慢、鎖定選到不合理的目標、搖桿大小等等（數值都可調整）。

## 步驟 5：回報

把 `run_autotest.ps1` 兩次的輸出，加上步驟 4 的手感筆記，貼回 Claude。Claude 會：
- 更新 CLAUDE-REPLY-008／CLAUDE-NOTE-006 的實跑結果，決定能不能標記 Phase 0-D／0-E COMPLETE
- 修掉實跑中發現的問題

## 如果失敗

- **編譯錯誤**：雲端離線檢查用的是 Unity 2021 參考組件，Unity 6 若有 API 差異會在這裡出現。
  把錯誤訊息整段貼回來。
- **prefab 沒有被 Fusion 註冊**（`NetworkObject prefab not found` 類錯誤）：在 Editor 開一次
  Fusion 的 Network Project Config，按 Rebuild Prefab Table，再重新 Build。
- **怪物打不到**：檢查 `Phase0D_TestScene` 的出生點距離，這個數值可以調整。
- **靈印次數偏低**：先確認步驟 1 的 setup 有跑（Player prefab 上要有 BurnStatus），再把 `-Seconds` 拉長。

---

## 附錄：交給 ChatGPT 的審查請求（可直接複製）

> 2026-09-26 更新：本機實跑後多了 D18～D23，Phase 0-D 已 COMPLETE（CLAUDE-REPLY-008）。

> 請依 HANDOFF-008 §17 審查《九曜：天墟》Phase 0 程式碼。Claude 的預審版在
> `docs/PHASE0_TECH_REVIEW_CLAUDE_V1.0.md`，本機實跑結果在 `docs/00_AI_HANDOFF_BRIDGE.md` 的
> CLAUDE-REPLY-008（0-D，已 COMPLETE）與 CLAUDE-NOTE-006（0-E，只差真人手機手感）。
> 請針對尚未處理的項目給出裁定並發 HANDOFF：
> (1) **D18（最優先）**：Fusion 2.1.2 的 `[Rpc]` 在 Mono 打包版整個不能用（weaver 呼叫 internal 方法 →
>     MethodAccessException）。目前一次性 Client→Server 命令改走 `Core/ClientCommands`
>     （`SendReliableDataToServer`，按發送者註冊處理者）。請裁定：是否定為專案標準並寫進 01_ARCHITECTURE_DECISIONS？
>     Dedicated Server 用 Mono 還是 IL2CPP 打包？（命令已改成排隊到下一個 tick 才處理，跟 RPC 一樣在 tick 內生效。）
> (2) D4 延遲補償：Fusion LagCompensation 目前關閉，03 戰鬥文件要求短窗口判定要能回溯；
> (3) D5 Client 預測：只預測移動，還是連攻擊起手也預測？
> (4) D3 正式死亡規則：懲罰、復活點、組隊救援、副本內規則；
> (5) D21 Photon 區域：目前固定 `hk`（各端自己測速會選到不同區域而互相找不到），正式上線的區域策略？
> (6) D9 擊退碰撞、D12 房間人數上限的處理時機。
> 另請確認：Phase 0-E（資料表／雙搖桿／目標鎖定／基礎回饋）是否符合 Roadmap Phase 0 的驗收意圖；
> 以及 iOS 正式打包（需要 Mac 或雲端打包＋Apple 開發者帳號，每年約 US$99）要排在哪個階段（D23）。
