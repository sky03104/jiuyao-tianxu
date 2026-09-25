# HANDOFF-008 Phase 0-D headless verification: 1 Dedicated Server + 2 Clients.
#
# Prerequisites (once, from unity/JiuyaoTianxu):
#   Unity.exe -batchmode -projectPath . -executeMethod Phase0ANetworkSetup.Run -quit   # only if prefabs don't exist yet
#   Unity.exe -batchmode -projectPath . -executeMethod Phase0DSetup.Run -quit
#   Unity.exe -batchmode -projectPath . -executeMethod Phase0DBuild.Build -quit
#
# Usage (from unity/JiuyaoTianxu):  pwsh Tools/Phase0D/run_autotest.ps1 [-Seconds 90]
param(
    [int]$Seconds = 90,
    [string]$Exe = "Builds/Phase0D/JiuyaoTianxu.exe",
    [string]$LogDir = "Logs/Phase0D"
)
$ErrorActionPreference = "Stop"
if (-not (Test-Path $Exe)) { throw "Build not found: $Exe (run Phase0DBuild.Build first)" }
New-Item -ItemType Directory -Force $LogDir | Out-Null

$common = @("-batchmode", "-nographics", "-autotest", "-quitafter", "$Seconds")
$server = Start-Process $Exe -PassThru -ArgumentList ($common + @("-netmode", "server", "-logFile", "$LogDir/server.log"))
Start-Sleep -Seconds 8   # let the server register the session before clients join
$c1 = Start-Process $Exe -PassThru -ArgumentList ($common + @("-netmode", "client", "-logFile", "$LogDir/client1.log"))
$c2 = Start-Process $Exe -PassThru -ArgumentList ($common + @("-netmode", "client", "-logFile", "$LogDir/client2.log"))

# Join/Leave regression: kill client2 before the run ends (it has had time to finish Q_PHASE0D_001).
Start-Sleep -Seconds ([Math]::Max(10, $Seconds - 15))
if (-not $c2.HasExited) { Stop-Process -Id $c2.Id -Force; Write-Host "client2 killed (Join/Leave regression)" }

foreach ($p in @($server, $c1)) { if (-not $p.WaitForExit(($Seconds + 30) * 1000)) { Stop-Process -Id $p.Id -Force } }

function Count($file, $pattern) { (Select-String -Path $file -Pattern $pattern -AllMatches | Measure-Object).Count }
$s = "$LogDir/server.log"
$report = [ordered]@{
    "server: StartGame succeeded"          = Count $s "StartGame succeeded"
    "server: Player joined"                = Count $s "Player joined"
    "server: Player left"                  = Count $s "Player left"
    "server: monsters spawned"             = Count $s "\[MonsterSpawner\] Spawned"
    "server: EnemyKilled events"           = Count $s "\[Phase0DTestRunner\] EnemyKilled"
    "server: quest accepted"               = Count $s "Available → Accepted"
    "server: quest progress lines"         = Count $s "\[QuestTracker\].* progress \d+/\d+"
    "server: Q_PHASE0D_001 completed"      = Count $s "Q_PHASE0D_001 InProgress → Completed"
    "server: Q_PHASE0D_002 unlocked"       = Count $s "Q_PHASE0D_002 Locked → Available"
    "server: PASS line"                    = Count $s "\[Phase0DTestRunner\] PASS"
    "server: 赤炎/玄甲/影遁 regression"      = Count $s "\[SpiritSealSystem\]"
    "client1: QuestSync lines"             = Count "$LogDir/client1.log" "\[QuestSync\]"
    "client2: QuestSync lines"             = Count "$LogDir/client2.log" "\[QuestSync\]"
    "ALL: Exception/NullReference"         = (Count $s "Exception|NullReference|Unhandled") +
                                             (Count "$LogDir/client1.log" "Exception|NullReference|Unhandled") +
                                             (Count "$LogDir/client2.log" "Exception|NullReference|Unhandled")
}
$report.GetEnumerator() | ForEach-Object { "{0,-40} {1}" -f $_.Key, $_.Value }
Select-String -Path $s -Pattern "\[Phase0DTestRunner\] (SUMMARY|PASS)" | Select-Object -Last 2 | ForEach-Object { $_.Line }
