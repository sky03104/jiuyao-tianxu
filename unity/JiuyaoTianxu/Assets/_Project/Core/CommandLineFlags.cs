using System;
using System.Globalization;
using System.Linq;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// Shared command-line switches for headless verification runs. Previously
    /// NetworkGameLauncher parsed -autotest privately; Phase 0-D needs the same
    /// flag in the quest auto-accept driver and the test runner, so it lives here
    /// once instead of being re-parsed in three places.
    /// </summary>
    public static class CommandLineFlags
    {
        // Args never change during a run; parsing once avoids an allocation per
        // tick (AutoTest is read from OnInput every simulation tick).
        private static string[] _args;
        private static string[] Args => _args ??= Environment.GetCommandLineArgs();

        /// <summary>-autotest: scripted input + scripted quest accept (headless runs).</summary>
        public static bool AutoTest => Args.Contains("-autotest");

        /// <summary>-autotest-lockon: additionally press LockOn periodically during
        /// -autotest (Phase 0-E). Opt-in so the default -autotest pattern that
        /// Phase 0-A~0-D were verified with stays byte-for-byte the same.</summary>
        public static bool AutoTestLockOn => Args.Contains("-autotest-lockon");

        /// <summary>-configdir &lt;path&gt;: server-side override tables folder
        /// (ConfigOverrideLoader). null = default StreamingAssets/ConfigOverrides.</summary>
        public static string ConfigDirectory
        {
            get
            {
                var args = Args;
                for (var i = 0; i < args.Length - 1; i++)
                {
                    if (args[i] == "-configdir") return args[i + 1];
                }
                return null;
            }
        }

        /// <summary>-quitafter &lt;seconds&gt;: optional auto-quit so a scripted
        /// 1 Server + 2 Client run ends by itself. 0 = never.</summary>
        public static float QuitAfterSeconds
        {
            get
            {
                var args = Args;
                for (var i = 0; i < args.Length - 1; i++)
                {
                    if (args[i] == "-quitafter" &&
                        float.TryParse(args[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out var s))
                    {
                        return s;
                    }
                }
                return 0f;
            }
        }
    }
}
