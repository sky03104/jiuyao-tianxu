#!/bin/bash
# Needs mono + a Roslyn csc that supports C# 9 (e.g. NuGet Microsoft.Net.Compilers 4.x tools/csc.exe).
# Usage: CSC=/path/to/csc.exe ./run.sh
set -e
cd "$(dirname "$0")"
Q=../../Assets/_Project/Gameplay/Quests
OUT=${TMPDIR:-/tmp}/QuestLogicTests.exe
mono "${CSC:?set CSC to Roslyn csc.exe}" -nologo -langversion:9 -out:"$OUT" QuestLogicTests.cs "$Q/QuestState.cs" "$Q/QuestStateMachine.cs"
mono "$OUT"
