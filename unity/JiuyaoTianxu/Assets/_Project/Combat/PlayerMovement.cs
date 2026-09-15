using Fusion;
using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.Combat
{
    /// <summary>
    /// Server-authoritative movement. Speed is scaled by CombatController's
    /// networked MoveSpeedMultiplier (0 while most attacks are active/recovering,
    /// 1 when idle, partial for weapons that explicitly allow moving mid-attack —
    /// see AttackDefinition.CanMoveDuringAttack). Movement itself knows nothing
    /// about weapons or attacks, only reads that one shared multiplier.
    /// </summary>
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private float _moveSpeed = 4f;

        private CombatController _combat;

        public override void Spawned()
        {
            _combat = GetComponent<CombatController>();
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;
            if (!GetInput(out PlayerInputData input)) return;

            var move = new Vector3(input.Move.x, 0f, input.Move.y);
            if (move.sqrMagnitude > 1f) move.Normalize();

            var speedMultiplier = _combat != null ? _combat.MoveSpeedMultiplier : 1f;
            transform.position += move * (_moveSpeed * speedMultiplier * Runner.DeltaTime);
        }
    }
}
