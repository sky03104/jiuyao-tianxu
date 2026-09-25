using JiuyaoTianxu.Combat;
using UnityEngine;

namespace JiuyaoTianxu.UI.Hud
{
    /// <summary>
    /// Keeps the test camera on this peer's own player once it spawns (it can now
    /// walk away from the origin). Keeps the camera's authored angle; the offset is
    /// the camera's position in the scene as authored (relative to the origin).
    /// Does nothing on a dedicated server (no local player).
    /// </summary>
    public class LocalPlayerCameraFollow : MonoBehaviour
    {
        [SerializeField] private float _smoothing = 8f;

        private Vector3 _offset;

        private void Awake() => _offset = transform.position;

        private void LateUpdate()
        {
            var player = PlayerMovement.Local;
            if (player == null) return;
            var goal = player.transform.position + _offset;
            transform.position = Vector3.Lerp(transform.position, goal, 1f - Mathf.Exp(-_smoothing * Time.deltaTime));
        }
    }
}
