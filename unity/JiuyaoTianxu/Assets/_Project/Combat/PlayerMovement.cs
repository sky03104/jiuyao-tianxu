using Fusion;
using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Combat.Targeting;
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
        [SerializeField] private float _turnSpeedDegPerSec = 900f; // 可調整
        [SerializeField] private float _stickDeadzone = 0.2f;      // 可調整

        private CombatController _combat;
        private TargetLock _targetLock;
        private Health _health;

        /// <summary>This peer's own player (null on a dedicated server). Camera follow reads it.</summary>
        public static PlayerMovement Local { get; private set; }

        public override void Spawned()
        {
            _combat = GetComponent<CombatController>();
            _targetLock = GetComponent<TargetLock>();
            _health = GetComponent<Health>();
            if (Object.HasInputAuthority) Local = this;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (Local == this) Local = null;
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;
            if (!GetInput(out PlayerInputData input)) return;

            // Never trust client sticks: NaN/huge values are dropped/clamped here,
            // before anything touches the authoritative transform.
            input.Move = Sanitize(input.Move);
            input.Aim = Sanitize(input.Aim);

            if (_health != null && _health.IsDead) return; // tech review D3

            var move = new Vector3(input.Move.x, 0f, input.Move.y);

            var speedMultiplier = _combat != null ? _combat.MoveSpeedMultiplier : 1f;
            transform.position += move * (_moveSpeed * speedMultiplier * Runner.DeltaTime);

            UpdateFacing(input);
        }

        private static Vector2 Sanitize(Vector2 v)
        {
            InputSanitizer.ClampStick(ref v.x, ref v.y);
            return v;
        }

        /// <summary>Phase 0-E twin-stick facing (server-side, replicated through
        /// NetworkTransform): aim stick &gt; locked target &gt; movement &gt; keep.
        /// Attacks keep using transform.forward, so this is what aims them.</summary>
        private void UpdateFacing(PlayerInputData input)
        {
            var hasLock = false;
            var toTarget = default(Planar);
            if (_targetLock != null && _targetLock.TryGetLockedTarget(out var target))
            {
                hasLock = true;
                toTarget = TargetLock.ToPlanar(target.transform.position - transform.position);
            }

            var source = FacingLogic.Resolve(new Planar(input.Move.x, input.Move.y), new Planar(input.Aim.x, input.Aim.y),
                hasLock, toTarget, _stickDeadzone, out var dir);
            if (source == FacingLogic.Source.Keep) return;

            var desired = Quaternion.LookRotation(new Vector3(dir.X, 0f, dir.Z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desired,
                _turnSpeedDegPerSec * Runner.DeltaTime);
        }
    }
}
