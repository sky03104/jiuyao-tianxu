# Windows: .NET SDK throwaway console project + the table validator. Usage: pwsh ./run.ps1
$ErrorActionPreference = "Stop"
$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$c = Join-Path $here "..\..\Assets\_Project\Config\Core"
$work = Join-Path ([IO.Path]::GetTempPath()) "ConfigTableTests"
New-Item -ItemType Directory -Force $work | Out-Null
@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><LangVersion>9</LangVersion><EnableDefaultCompileItems>false</EnableDefaultCompileItems></PropertyGroup>
  <ItemGroup>
    <Compile Include="$here\ConfigTableTests.cs" />
    <Compile Include="$c\CsvTable.cs" />
    <Compile Include="$c\TableBinder.cs" />
    <Compile Include="$c\MonsterTableRow.cs" />
  </ItemGroup>
</Project>
"@ | Set-Content -Encoding UTF8 (Join-Path $work "ConfigTableTests.csproj")
$env:MONSTERS_CSV = Join-Path $here "..\..\Assets\_Project\Config\Tables\monsters.csv"
dotnet run --project (Join-Path $work "ConfigTableTests.csproj")
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
python (Join-Path $here "validate_tables.py")
exit $LASTEXITCODE
