using UnityEngine;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// Reads local keyboard state and produces a PlayerInputData. This is the only
    /// class in the project allowed to touch UnityEngine.Input directly — everything
    /// downstream (network layer, player controller) only ever sees PlayerInputData.
    /// Swapping in a mobile virtual joystick later means writing a second class like
    /// this one, not touching Net/ or Combat/.
    /// </summary>
    public static class KeyboardInputProvider
    {
        public static PlayerInputData Poll()
        {
            var data = new PlayerInputData
            {
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };

            // While touch controls are shown, a mouse click is a (simulated) finger on
            // the overlay, not an attack.
            if (Input.GetKey(KeyCode.Space) || (!TouchInputState.Active && Input.GetMouseButton(0)))
            {
                data.Buttons.Set(PlayerButton.Attack, true);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                data.Buttons.Set(PlayerButton.SwitchWeapon, true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                data.Buttons.Set(PlayerButton.DodgeTest, true);
            }

            // Phase 0-E: IJKL = aim stick stand-in on keyboard (arrow keys already
            // feed the default Horizontal/Vertical move axes). F = lock-on.
            data.Aim = new Vector2(
                (Input.GetKey(KeyCode.L) ? 1f : 0f) - (Input.GetKey(KeyCode.J) ? 1f : 0f),
                (Input.GetKey(KeyCode.I) ? 1f : 0f) - (Input.GetKey(KeyCode.K) ? 1f : 0f));

            if (Input.GetKeyDown(KeyCode.F))
            {
                data.Buttons.Set(PlayerButton.LockOn, true);
            }

            return data;
        }

        /// <summary>Phase 0-D: quest accept is a one-shot request that QuestTracker
        /// turns into PlayerInputData.QuestAcceptId (it needs the quest id, not a
        /// button) — but the key read still lives here to keep this the only class
        /// touching Input.</summary>
        public static bool QuestAcceptPressed() => Input.GetKeyDown(KeyCode.Q);
    }
}
