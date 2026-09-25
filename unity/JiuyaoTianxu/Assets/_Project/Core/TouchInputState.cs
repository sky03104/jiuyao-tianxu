using UnityEngine;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// What the on-screen touch controls currently say (written by
    /// UI/TouchControls/VirtualControlsOverlay, read by LocalInputProvider). Lives in
    /// Core so the input layer never depends on UI code. One-shot buttons are
    /// latched until the next network input poll consumes them, so a tap shorter
    /// than one simulation tick is never lost.
    /// </summary>
    public static class TouchInputState
    {
        /// <summary>True while touch controls are on screen. Also stops a mouse
        /// click on the overlay from being read as a keyboard/mouse attack.</summary>
        public static bool Active;
        public static Vector2 Move;
        public static Vector2 Aim;
        public static bool AttackHeld;

        private static bool _switchWeapon, _dodge, _lockOn, _questAccept;

        public static void PressSwitchWeapon() => _switchWeapon = true;
        public static void PressDodge() => _dodge = true;
        public static void PressLockOn() => _lockOn = true;
        public static void PressQuestAccept() => _questAccept = true;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            Active = false;
            Move = Aim = Vector2.zero;
            AttackHeld = false;
            _switchWeapon = _dodge = _lockOn = _questAccept = false;
        }

        /// <summary>Merges touch into keyboard input: the larger stick deflection
        /// wins, buttons are OR-ed, latches are consumed.</summary>
        public static void ApplyTo(ref PlayerInputData data)
        {
            if (!Active) return;

            if (Move.sqrMagnitude > data.Move.sqrMagnitude) data.Move = Move;
            if (Aim.sqrMagnitude > data.Aim.sqrMagnitude) data.Aim = Aim;
            if (AttackHeld) data.Buttons.Set(PlayerButton.Attack, true);

            if (_switchWeapon) { data.Buttons.Set(PlayerButton.SwitchWeapon, true); _switchWeapon = false; }
            if (_dodge) { data.Buttons.Set(PlayerButton.DodgeTest, true); _dodge = false; }
            if (_lockOn) { data.Buttons.Set(PlayerButton.LockOn, true); _lockOn = false; }
        }

        /// <summary>Quest accept is an RPC request, not tick input (see QuestTracker).</summary>
        public static bool ConsumeQuestAccept()
        {
            var pressed = _questAccept;
            _questAccept = false;
            return pressed;
        }
    }
}
