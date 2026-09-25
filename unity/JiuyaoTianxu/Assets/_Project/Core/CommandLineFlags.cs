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
        /// <summary>-autotest: scripted input + scripted quest accept (headless runs).</summary>
        public static bool AutoTest => Environment.GetCommandLineArgs().Contains("-autotest");

        /// <summary>-quitafter &lt;seconds&gt;: optional auto-quit so a scripted
        /// 1 Server + 2 Client run ends by itself. 0 = never.</summary>
        public static float QuitAfterSeconds
        {
            get
            {
                var args = Environment.GetCommandLineArgs();
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
