using System;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// Virtual joystick response curve, pure C# (unit-tested in Tools/ControlsTests).
    /// Finger offset from the stick centre → output with magnitude 0..1:
    /// inside the deadzone = 0, at/after the rim = 1, linear in between (rescaled so
    /// the output starts from 0 right at the deadzone edge, no jump).
    /// </summary>
    public static class StickMath
    {
        public static void Evaluate(float dx, float dy, float radius, float deadzone, out float x, out float y)
        {
            x = 0f;
            y = 0f;
            if (radius <= 0f) return;

            var mx = dx / radius;
            var my = dy / radius;
            var m = (float)Math.Sqrt(mx * mx + my * my);
            deadzone = Math.Max(0f, Math.Min(0.95f, deadzone));
            if (m <= deadzone) return;

            var scaled = (Math.Min(m, 1f) - deadzone) / (1f - deadzone);
            x = mx / m * scaled;
            y = my / m * scaled;
        }
    }
}
