using Fusion;
using UnityEngine;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// Networked input abstraction for Phase 0-A. Keyboard is the only source today,
    /// but this struct is the seam a future mobile virtual joystick plugs into —
    /// anything upstream only needs to populate this struct, not know about
    /// keyboard/touch specifics.
    /// </summary>
    public struct PlayerInputData : INetworkInput
    {
        public Vector2 Move;
        public NetworkButtons Buttons;
    }

    public static class PlayerButton
    {
        public const int Attack = 0;
        public const int SwitchWeapon = 1;
        /// <summary>Phase 0-C test-only hook (HANDOFF-007 §7.3): real Dodge doesn't
        /// exist yet, this just lets SpiritSealSystem exercise the OnDodgeEvent
        /// trigger (影遁) without needing a Dodge system built first.</summary>
        public const int DodgeTest = 2;
    }
}
