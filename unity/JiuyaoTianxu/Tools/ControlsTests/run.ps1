# Windows: uses the .NET SDK's C# compiler via a throwaway console project.
# Usage (from this folder): pwsh ./run.ps1
$ErrorActionPreference = "Stop"
$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$t = Join-Path $here "..\..\Assets\_Project\Combat\Targeting"
$work = Join-Path ([IO.Path]::GetTempPath()) "ControlsTests"
New-Item -ItemType Directory -Force $work | Out-Null
@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><LangVersion>9</LangVersion><EnableDefaultCompileItems>false</EnableDefaultCompileItems></PropertyGroup>
  <ItemGroup>
    <Compile Include="$here\ControlsTests.cs" />
    <Compile Include="$t\TargetingMath.cs" />
    <Compile Include="$t\..\..\Core\StickMath.cs" />
    <Compile Include="$t\..\..\Core\InputSanitizer.cs" />
  </ItemGroup>
</Project>
"@ | Set-Content -Encoding UTF8 (Join-Path $work "ControlsTests.csproj")
dotnet run --project (Join-Path $work "ControlsTests.csproj")
exit $LASTEXITCODE
