using UnityEngine;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// Deterministic scripted input used only for headless verification
    /// (-batchmode -nographics processes have no real keyboard/window, so
    /// UnityEngine.Input never fires). Activated by the -autotest command-line
    /// flag.
    ///
    /// Paced by Fusion's own simulation tick (NetworkRunner.Tick.Raw), NOT
    /// Time.frameCount — a first version used Time.frameCount and got stuck for
    /// 30+ minutes on a single weapon. Root cause: in a headless nographics
    /// process, Unity's Update()/frame rate is decoupled from Fusion's fixed
    /// simulation tick rate, so a frame-count-paced input pattern gets sampled
    /// into FixedUpdateNetwork at a wildly different (and inconsistent) rate
    /// than intended, silently breaking any timing assumption (including the
    /// "rest gap before switching weapon" meant to guarantee CombatState
    /// returns to Idle). Ticks are what CombatController's own TickTimers are
    /// measured in, so pacing test input in ticks keeps both in the same clock.
    ///
    /// Each weapon window is: attack-pulse for a while, then a rest gap with no
    /// input at all (long enough for any weapon's worst-case Recovery+ComboWindow
    /// to elapse), then one SwitchWeapon press — SwitchWeapon only applies while
    /// CombatState.IsIdle, so the rest gap is required, not cosmetic.
    /// </summary>
    public static class AutoTestInputProvider
    {
        private const int CycleTicks = 30;          // attack press/release duty cycle.
        private const int WeaponWindowTicks = 360;   // total ticks spent on one weapon before switching.
        private const int RestBeforeSwitchTicks = 120; // ticks of total silence before the switch press.
        private const int DodgeTestTicks = 90;       // ~1.5s: periodic press to arm 影遁 (HANDOFF-007 §12).
        private const int LockOnTestTicks = 240;     // ~4s: Phase 0-E lock-on cycle (opt-in).

        public static PlayerInputData Poll(int tick)
        {
            var data = new PlayerInputData { Move = Vector2.zero };

            var windowPos = tick % WeaponWindowTicks;
            var attackWindowEnd = WeaponWindowTicks - RestBeforeSwitchTicks;

            if (windowPos < attackWindowEnd)
            {
                var phase = windowPos % CycleTicks;
                if (phase < CycleTicks / 2)
                {
                    data.Buttons.Set(PlayerButton.Attack, true);
                }
            }
            else if (windowPos == WeaponWindowTicks - 1)
            {
                // Rest gap has fully elapsed by this tick — safe to switch.
                data.Buttons.Set(PlayerButton.SwitchWeapon, true);
            }

            // Independent of the weapon/attack cycle above — SpiritSealSystem reads
            // this button itself, CombatController never sees it.
            if (tick % DodgeTestTicks == 0)
            {
                data.Buttons.Set(PlayerButton.DodgeTest, true);
            }

            // Phase 0-E, opt-in (-autotest-lockon): lock → cycle → clear through
            // TargetLock. Offset from the dodge tick so the two never coincide.
            if (CommandLineFlags.AutoTestLockOn && tick % LockOnTestTicks == LockOnTestTicks / 2)
            {
                data.Buttons.Set(PlayerButton.LockOn, true);
            }

            return data;
        }
    }
}
