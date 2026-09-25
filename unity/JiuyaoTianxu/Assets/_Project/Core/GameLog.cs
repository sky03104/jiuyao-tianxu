using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// Per-event gameplay logging (every hit, seal trigger, quest progress…) — tech
    /// review D10. Compiled in for the Editor and Development builds (every Phase 0
    /// verification build uses BuildOptions.Development, so the autotest log
    /// greps keep working) and stripped from release builds, INCLUDING the string
    /// formatting at the call site, so a busy release server doesn't pay for it.
    /// Define JIUYAO_GAMELOG to force it on in a release build.
    /// Warnings/errors and rare lifecycle messages keep using Debug.* directly.
    /// </summary>
    public static class GameLog
    {
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD"), Conditional("JIUYAO_GAMELOG")]
        public static void Info(string message) => Debug.Log(message);
    }
}
