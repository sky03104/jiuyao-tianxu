#!/bin/bash
# Every check that runs without Unity, in one go (used by CI and by cloud sessions):
#   1. offline compile check (runtime + Editor scripts, with self-test)
#   2. QuestLogicTests / ConfigTableTests (+ validate_tables.py) / ControlsTests
# Needs: mono (mono-devel), curl, unzip, python3. First run downloads ~40 MB of
# reference assemblies into ~/.cache/jiuyao-compile-check (override: JIUYAO_CHECK_CACHE).
set -uo pipefail
HERE="$(cd "$(dirname "$0")" && pwd)"
export JIUYAO_CHECK_CACHE="${JIUYAO_CHECK_CACHE:-$HOME/.cache/jiuyao-compile-check}"
export TMPDIR="${TMPDIR:-/tmp}"
FAILED=()

step() { echo; echo "######## $1"; shift; "$@" || FAILED+=("$1"); }

step "compile check" "$HERE/CompileCheck/compile_check.sh" --self-test
export CSC="$JIUYAO_CHECK_CACHE/roslyn/tools/csc.exe" # downloaded by the compile check
step "QuestLogicTests" "$HERE/QuestLogicTests/run.sh"
step "ConfigTableTests" "$HERE/ConfigTableTests/run.sh"
step "ControlsTests" "$HERE/ControlsTests/run.sh"

echo
if [ ${#FAILED[@]} -eq 0 ]; then echo "ALL CHECKS PASSED (offline only — not a Unity/network verification)"; exit 0; fi
echo "FAILED: ${FAILED[*]}"; exit 1
