# Phase 0-E 手機觸控測試（網頁版）：開一個專用伺服器＋網頁伺服器，讓同一個 Wi-Fi 的手機用瀏覽器加入。
#
# 事前（打包一次就好，從 unity/JiuyaoTianxu 執行）：
#   Unity.exe -batchmode -projectPath . -executeMethod Phase0DBuild.Build -quit
#   Unity.exe -batchmode -projectPath . -executeMethod Phase0DBuild.BuildWebGL -quit
#
# 用法（從 unity/JiuyaoTianxu 執行）：
#   powershell -ExecutionPolicy Bypass -File Tools/Phase0D/run_webgl_touchtest.ps1
# 手機打開畫面上顯示的網址、轉成橫的；測完回到這個視窗按 Enter，會關掉伺服器並列出剛才的操作紀錄。
param(
    [int]$Port = 8000,
    [string]$LogDir = "Logs/WebGLTest"
)
$ErrorActionPreference = "Stop"
$exe = "Builds/Phase0D/JiuyaoTianxu.exe"
$web = "Builds/Phase0D_WebGL"
if (-not (Test-Path $exe)) { throw "找不到電腦版：$exe（先跑 Phase0DBuild.Build）" }
if (-not (Test-Path "$web/index.html")) { throw "找不到網頁版：$web（先跑 Phase0DBuild.BuildWebGL）" }
New-Item -ItemType Directory -Force $LogDir | Out-Null
$serverLog = "$LogDir/server.log"

$server = Start-Process $exe -PassThru -ArgumentList @("-batchmode", "-nographics", "-netmode", "server", "-logFile", $serverLog)
$http = Start-Process "python" -PassThru -WindowStyle Hidden -ArgumentList @("-m", "http.server", "$Port", "--bind", "0.0.0.0", "--directory", $web)
try {
    Start-Sleep -Seconds 12
    $started = Select-String -Path $serverLog -Pattern "StartGame succeeded" -Encoding utf8 | Select-Object -First 1
    if (-not $started) { throw "伺服器 12 秒內沒有連上 Photon，看 $serverLog" }
    Write-Host $started.Line
    $ips = Get-NetIPAddress -AddressFamily IPv4 |
        Where-Object { $_.IPAddress -notlike "127.*" -and $_.IPAddress -notlike "169.254.*" }
    Write-Host ""
    Write-Host "手機（跟這台電腦同一個 Wi-Fi）用瀏覽器打開下面其中一個網址，然後把手機轉成橫的："
    foreach ($ip in $ips) { Write-Host ("  http://{0}:{1}    （{2}）" -f $ip.IPAddress, $Port, $ip.InterfaceAlias) }
    Write-Host "不知道選哪個：iPhone 設定 → Wi-Fi → 點你連的網路 → 看「IP 位址」，前三段數字一樣的那個就對了。"
    Write-Host ""
    Read-Host "測完按 Enter 關閉伺服器"
}
finally {
    foreach ($p in @($server, $http)) { if ($p -and -not $p.HasExited) { Stop-Process -Id $p.Id -Force } }
}

# 只列出玩家操作留下的紀錄（網頁版的每個動作都由伺服器記錄）
Write-Host ""
Write-Host "==== 這次測試的操作紀錄 ===="
Select-String -Path $serverLog -Encoding utf8 -CaseSensitive -Pattern @(
    "Player joined", "Player left", "\[TargetLock\]", "switched to weapon", "\[ClientCommands\]",
    "Q_PHASE0D_\d+ .* → ", "armed '", "starts .* combo", " hit ", "projectile-hit", "\] EnemyKilled #"
) | ForEach-Object { $_.Line } | Select-Object -Last 60
