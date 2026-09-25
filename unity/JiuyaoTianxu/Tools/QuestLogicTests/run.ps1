# Windows: uses the .NET SDK's C# compiler via a throwaway console project.
# Usage (from this folder): pwsh ./run.ps1
$ErrorActionPreference = "Stop"
$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$q = Join-Path $here "..\..\Assets\_Project\Gameplay\Quests"
$work = Join-Path ([IO.Path]::GetTempPath()) "QuestLogicTests"
New-Item -ItemType Directory -Force $work | Out-Null
@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><LangVersion>9</LangVersion><EnableDefaultCompileItems>false</EnableDefaultCompileItems></PropertyGroup>
  <ItemGroup>
    <Compile Include="$here\QuestLogicTests.cs" />
    <Compile Include="$q\QuestState.cs" />
    <Compile Include="$q\QuestStateMachine.cs" />
  </ItemGroup>
</Project>
"@ | Set-Content (Join-Path $work "QuestLogicTests.csproj")
dotnet run --project (Join-Path $work "QuestLogicTests.csproj")
exit $LASTEXITCODE
