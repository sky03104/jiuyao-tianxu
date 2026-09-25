using System;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// Server-side guard for client-supplied stick values (Server Authority: never
    /// trust input). A modified client could send NaN/Infinity or a 1000-long
    /// vector; NaN would poison the player's transform on the server and
    /// replicate to everyone, a long vector would be a speed hack. Pure C#
    /// (unit-tested in Tools/ControlsTests).
    /// </summary>
    public static class InputSanitizer
    {
        /// <summary>Non-finite → (0,0); magnitude clamped to 1; direction kept.</summary>
        public static void ClampStick(ref float x, ref float y)
        {
            if (!IsFinite(x) || !IsFinite(y)) { x = 0f; y = 0f; return; }
            var sq = x * x + y * y;
            if (sq <= 1f) return;
            var m = (float)Math.Sqrt(sq);
            x /= m;
            y /= m;
        }

        private static bool IsFinite(float f) => !float.IsNaN(f) && !float.IsInfinity(f);
    }
}
