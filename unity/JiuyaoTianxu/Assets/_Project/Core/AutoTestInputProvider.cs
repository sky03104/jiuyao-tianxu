using UnityEngine;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// Deterministic scripted input used only for Phase 0-A headless verification
    /// (two processes launched with -batchmode -nographics have no real keyboard/
    /// window, so UnityEngine.Input never fires). Activated by the -autotest
    /// command-line flag. Stays put and presses Attack on a fixed duty cycle so the
    /// 10-cycle Attack->Hit->Damage->HP-Sync loop required by HANDOFF-005 §0-A-08
    /// can be verified from logs without a human at a keyboard.
    /// </summary>
    public static class AutoTestInputProvider
    {
        private const int CycleFrames = 30; // ~0.5s at 60fps: press for the first half, release for the second.

        public static PlayerInputData Poll()
        {
            var data = new PlayerInputData { Move = Vector2.zero };

            var phase = Time.frameCount % CycleFrames;
            if (phase < CycleFrames / 2)
            {
                data.Buttons.Set(PlayerButton.Attack, true);
            }

            return data;
        }
    }
}
