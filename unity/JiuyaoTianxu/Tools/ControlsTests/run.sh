#!/bin/bash
# Needs mono + a Roslyn csc that supports C# 9. Usage: CSC=/path/to/csc.exe ./run.sh
set -e
cd "$(dirname "$0")"
OUT=${TMPDIR:-/tmp}/ControlsTests.exe
mono "${CSC:?set CSC to Roslyn csc.exe}" -nologo -langversion:9 -out:"$OUT" ControlsTests.cs ../../Assets/_Project/Combat/Targeting/TargetingMath.cs ../../Assets/_Project/Core/StickMath.cs
mono "$OUT"
