using Fusion;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.Combat
{
    /// <summary>
    /// Minimal server-authoritative movement for Phase 0-A. Only StateAuthority
    /// (the dedicated server) advances position from input; every other peer just
    /// receives the networked Transform and interpolates. No animation, no character
    /// controller physics yet — that's Phase 0-B's job once the six weapon flows
    /// bring real movesets.
    /// </summary>
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private float _moveSpeed = 4f;

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;
            if (!GetInput(out PlayerInputData input)) return;

            var move = new Vector3(input.Move.x, 0f, input.Move.y);
            if (move.sqrMagnitude > 1f) move.Normalize();

            transform.position += move * (_moveSpeed * Runner.DeltaTime);
        }
    }
}
