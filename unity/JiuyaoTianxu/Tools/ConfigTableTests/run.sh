#!/bin/bash
# Needs mono + a Roslyn csc that supports C# 9. Usage: CSC=/path/to/csc.exe ./run.sh
set -e
cd "$(dirname "$0")"
C=../../Assets/_Project/Config/Core
OUT=${TMPDIR:-/tmp}/ConfigTableTests.exe
mono "${CSC:?set CSC to Roslyn csc.exe}" -nologo -langversion:9 -out:"$OUT" ConfigTableTests.cs "$C/CsvTable.cs" "$C/TableBinder.cs"
mono "$OUT"
python3 validate_tables.py
