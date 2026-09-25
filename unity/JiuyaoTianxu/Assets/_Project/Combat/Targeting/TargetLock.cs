using System.Collections.Generic;
using Fusion;
using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Targeting
{
    /// <summary>
    /// 目標鎖定 (roadmap Phase 0 「戰鬥雙搖桿：移動、目標鎖定…」). Server-authoritative:
    /// the client only presses LockOn; the server picks/cycles/clears the target and
    /// replicates a single NetworkId. PlayerMovement turns toward it, the HUD draws a
    /// marker on it. It never changes what an attack hits — hit detection still
    /// comes only from the attack's own shape in front of the character.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class TargetLock : NetworkBehaviour
    {
        [SerializeField] private float _lockRange = 12f;          // 可調整
        [SerializeField] private float _breakRange = 16f;         // 可調整: > lock range, so a lock doesn't flicker at the edge
        [SerializeField] private float _anglePenaltyPerDeg = 0.03f; // 可調整: 180° behind ≈ +5.4m

        [Networked] public NetworkId LockedTargetId { get; private set; }
        [Networked] private NetworkButtons PreviousButtons { get; set; }

        private Health _self;

        /// <summary>This peer's own player's TargetLock (null on a dedicated server).
        /// Read by the HUD to draw the lock marker.</summary>
        public static TargetLock Local { get; private set; }

        public override void Spawned()
        {
            _self = GetComponent<Health>();
            if (Object.HasInputAuthority) Local = this;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (Local == this) Local = null;
        }

        /// <summary>Resolves the replicated lock on any peer.</summary>
        public bool TryGetLockedTarget(out Health target)
        {
            target = null;
            if (!LockedTargetId.IsValid || Runner == null) return false;
            if (!Runner.TryFindObject(LockedTargetId, out var obj) || obj == null) return false;
            target = obj.GetComponent<Health>();
            return target != null;
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;

            if (GetInput(out PlayerInputData input))
            {
                if (input.Buttons.WasPressed(PreviousButtons, PlayerButton.LockOn)) Cycle();
                PreviousButtons = input.Buttons;
            }

            if (TryGetLockedTarget(out var target))
            {
                if (LockOnSelector.ShouldBreak(target.HP > 0, ToPlanar(transform.position),
                        ToPlanar(target.transform.position), _breakRange))
                {
                    GameLog.Info($"[TargetLock] {name} lock on {target.name} broken.");
                    LockedTargetId = default;
                }
            }
            else if (LockedTargetId.IsValid)
            {
                LockedTargetId = default; // target despawned
            }
        }

        private void Cycle()
        {
            var byId = new Dictionary<uint, NetworkObject>();
            var candidates = new List<LockCandidate>();
            foreach (var health in HitDetectionService.FindHealthInRadius(transform.position, _lockRange, _self))
            {
                if (health.HP <= 0 || health.Object == null) continue;
                var id = health.Object.Id;
                byId[id.Raw] = health.Object;
                // "Enemy" = not controlled by any player; players are still lockable, just ranked after.
                var isEnemy = health.Object.InputAuthority == PlayerRef.None;
                candidates.Add(new LockCandidate(id.Raw, ToPlanar(health.transform.position), isEnemy));
            }

            var ranked = LockOnSelector.Rank(candidates, ToPlanar(transform.position), ToPlanar(transform.forward),
                _lockRange, _anglePenaltyPerDeg);
            var next = LockOnSelector.Next(ranked, LockedTargetId.IsValid ? LockedTargetId.Raw : 0);

            LockedTargetId = next != 0 ? byId[next].Id : default;
            GameLog.Info(next != 0
                ? $"[TargetLock] {name} locked {byId[next].name} ({ranked.Count} candidate(s))."
                : $"[TargetLock] {name} lock cleared.");
        }

        public static Planar ToPlanar(Vector3 v) => new Planar(v.x, v.z);
    }
}
