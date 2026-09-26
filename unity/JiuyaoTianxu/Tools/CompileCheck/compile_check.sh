#!/bin/bash
# Offline C# compile check for unity/JiuyaoTianxu/Assets/_Project — no Unity needed.
#
# Cloud sessions and CI have no Unity Editor, so this is the strongest check available
# there: Roslyn (C# 9) on mono, against UnityEngine 2021.3 reference modules (NuGet
# UnityEngine.Modules), the project's own Fusion 2.1.2 DLLs, and a 2018 UnityEditor
# reference (NuGet Unity3D.UnityEditor) for Editor scripts.
#
# It proves the code type-checks. It does NOT prove it runs, and Unity 6-only API
# differences can still surface in real Unity. Never report "network verified" from this.
#
# CS0649 ("never assigned") is silenced: [SerializeField] fields are assigned by Unity.
# Needs: mono (apt: mono-devel), curl, unzip.   Usage: ./compile_check.sh [--self-test]
set -euo pipefail
HERE="$(cd "$(dirname "$0")" && pwd)"
PROJECT="$(cd "$HERE/../.." && pwd)"
ASSETS="$PROJECT/Assets"
CACHE="${JIUYAO_CHECK_CACHE:-$HOME/.cache/jiuyao-compile-check}"
mkdir -p "$CACHE"

fetch() { # name version dir
  local url="https://api.nuget.org/v3-flatcontainer/$1/$2/$1.$2.nupkg"
  if [ ! -d "$CACHE/$3" ]; then
    echo "[compile-check] downloading $1 $2"
    curl -sSfL "$url" -o "$CACHE/$3.zip"
    mkdir -p "$CACHE/$3" && (cd "$CACHE/$3" && unzip -qo "../$3.zip")
  fi
  # Some packages (UnityEngine.Modules) store their DLLs with mode 000. Root can read
  # them anyway; a normal user (the CI runner) gets CS0009 "access denied". Always
  # normalise, which also repairs a cache restored from an earlier run.
  chmod -R u+rwX,go+rX "$CACHE/$3"
}
fetch microsoft.net.compilers 4.2.0 roslyn
fetch unityengine.modules 2021.3.33 unityengine
fetch unity3d.unityeditor 2018.1.6-f1 unityeditor

CSC="$CACHE/roslyn/tools/csc.exe"
# Fusion.Runtime targets netstandard 2.1. Only mono's runtime facade (4.5/Facades) is
# 2.1 — the *-api/Facades copies are 2.0 (CS1705), and `find | head` order differs
# between machines (it picked a 2.0 one on the CI runner), so pin the path.
FACADE="${MONO_NETSTANDARD_FACADE:-/usr/lib/mono/4.5/Facades/netstandard.dll}"
[ -f "$FACADE" ] || { echo "netstandard 2.1 facade not found at $FACADE (install mono-devel)"; exit 1; }
REFS=()
for d in "$CACHE"/unityengine/lib/net45/*.dll; do REFS+=("-r:$d"); done
for d in "$ASSETS"/Photon/Fusion/Assemblies/Fusion.*.dll; do REFS+=("-r:$d"); done
REFS+=("-r:$FACADE")

OUT="$(mktemp -d)"
trap 'rm -rf "$OUT"' EXIT
STUBS="$OUT/FusionUnityStubs.cs"
cp "$HERE/FusionUnityStubs.cs.txt" "$STUBS"  # .txt so Unity never imports it

# Photon.Realtime ships as source (its own asmdef) and Fusion.Realtime's public types derive
# from it (FusionAppSettings : AppSettings, e.g. FixedRegion), so build it first like Unity
# does. No UNITY_* defines: every UnityEngine use in it is behind SUPPORTED_UNITY, and only
# its API matters here.
PHOTON_CLIENT="$ASSETS/Photon/PhotonLibs/netstandard2.0/release/PhotonClient.dll"
mapfile -t REALTIME < <(find "$ASSETS/Photon/PhotonRealtime/Code" -name '*.cs' | sort)
REALTIME_LOG="$(mono "$CSC" -nologo -langversion:9 -t:library -unsafe -nowarn:1701,1702,0618,0649 \
  -out:"$OUT/Photon.Realtime.dll" -r:"$FACADE" -r:"$PHOTON_CLIENT" "${REALTIME[@]}" 2>&1 || true)"
if [ ! -s "$OUT/Photon.Realtime.dll" ]; then
  echo "Photon.Realtime reference build failed (${#REALTIME[@]} files):"
  grep -E '(^|: )error CS' <<<"$REALTIME_LOG" | head -20 | sed "s#$ASSETS/##"
  exit 1
fi
REFS+=("-r:$OUT/Photon.Realtime.dll" "-r:$PHOTON_CLIENT")

mapfile -t RUNTIME < <(find "$ASSETS/_Project" -name '*.cs' -not -path '*/Editor/*' | sort)
# Phase0ASetup needs the URP package (not in the reference set); everything else is checked.
mapfile -t EDITOR < <(find "$ASSETS/_Project/Editor" -name '*.cs' -not -name 'Phase0ASetup.cs' | sort)

echo "[compile-check] $(mono --version | head -1)"

compile() { # label extra-args... ; prints the compiler output
  local label="$1"; shift
  local log="$OUT/$label.log"
  mono "$CSC" -nologo -langversion:9 -t:library -unsafe -nowarn:1701,1702,0618,0649 \
    -out:"$OUT/$label.dll" "${REFS[@]}" "$@" > "$log" 2>&1 || true
  cat "$log"
}

# A compile that reports no "error CS" lines must also have produced its DLL —
# otherwise the compiler itself never ran (missing runtime piece, crash…), and
# "0 errors" would be a false pass. First seen on the CI runner.
require_output() { # label log
  if [ ! -s "$OUT/$1.dll" ] && ! grep -qE '(^|: )error CS' <<<"$2"; then
    echo "$1: compiler produced no output and no diagnostics — compiler did not run. Log:"
    head -40 <<<"$2"
    FAIL=1
    return 1
  fi
}

FAIL=0

echo "== runtime (${#RUNTIME[@]} files) =="
RLOG="$(compile runtime "$STUBS" "${RUNTIME[@]}")"
R_ERR="$(grep -E '(^|: )error CS' <<<"$RLOG" || true)"
R_WARN="$(grep -E ': warning ' <<<"$RLOG" || true)"
[ -n "$R_WARN" ] && echo "$R_WARN" | sed "s#$ASSETS/##"
if [ -n "$R_ERR" ]; then echo "$R_ERR" | sed "s#$ASSETS/##"; FAIL=1
elif require_output runtime "$RLOG"; then echo "runtime: 0 errors"; fi

echo "== editor (${#EDITOR[@]} files + runtime) =="
ELOG="$(compile editor -r:"$CACHE/unityeditor/lib/UnityEditor.dll" "$STUBS" "${RUNTIME[@]}" "${EDITOR[@]}")"
# Known false positives: these PrefabUtility APIs arrived in Unity 2018.3, after the
# 2018.1 reference DLL; Phase0ANetworkSetup has used them successfully in Unity 6.
KNOWN="PrefabUtility' does not contain a definition for '(SaveAsPrefabAsset|LoadPrefabContents|UnloadPrefabContents)'"
E_ERR="$(grep -E '(^|: )error CS' <<<"$ELOG" | grep -Ev "$KNOWN" || true)"
if [ -n "$E_ERR" ]; then echo "$E_ERR" | sed "s#$ASSETS/##"; FAIL=1
elif ! grep -qE '(^|: )error CS' <<<"$ELOG" && ! require_output editor "$ELOG"; then :
else echo "editor: 0 unexpected errors"; fi

if [ "${1:-}" = "--self-test" ]; then
  echo "== self-test: a deliberately broken file must fail =="
  printf 'class CompileCheckSelfTest { void X() { JiuyaoTianxu.Combat.Health h = null; h.NotAMember(); } }\n' > "$OUT/Broken.cs"
  SLOG="$(compile selftest "$STUBS" "${RUNTIME[@]}" "$OUT/Broken.cs")"
  if grep -q "Broken.cs.*error CS1061" <<<"$SLOG"; then echo "self-test: OK (error detected)"
  else echo "self-test: FAILED — broken code was not reported. Compiler output:"; head -40 <<<"$SLOG"; FAIL=1; fi
fi

exit $FAIL
