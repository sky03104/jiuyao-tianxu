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

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                data.Buttons.Set(PlayerButton.Attack, true);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                data.Buttons.Set(PlayerButton.SwitchWeapon, true);
            }

            return data;
        }
    }
}
