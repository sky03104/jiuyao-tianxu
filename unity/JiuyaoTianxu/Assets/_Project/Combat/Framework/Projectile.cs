using Fusion;
using JiuyaoTianxu.Combat;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Minimal server-authoritative projectile for Bow/Staff (HANDOFF-006 §4.4/§4.6:
    /// "可以先使用簡單 Projectile，不需要正式箭矢模型"). Straight-line travel,
    /// per-tick overlap check, single target, despawns on hit or timeout.
    /// Not a full CCD/lag-compensated hit system — that's a later phase's job.
    /// </summary>
    public class Projectile : NetworkBehaviour
    {
        [Networked] private Vector3 Direction { get; set; }
        [Networked] private float Speed { get; set; }
        [Networked] private float RemainingLifetime { get; set; }

        private AttackDefinition _attack;
        private Health _source;

        public static void Initialize(NetworkRunner runner, NetworkObject obj, Vector3 direction, AttackDefinition attack, Health source)
        {
            var projectile = obj.GetComponent<Projectile>();
            projectile.Direction = direction.normalized;
            projectile.Speed = attack.ProjectileSpeed;
            projectile.RemainingLifetime = 3f;
            projectile._attack = attack;
            projectile._source = source;
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;

            transform.position += Direction * (Speed * Runner.DeltaTime);
            RemainingLifetime -= Runner.DeltaTime;
            if (RemainingLifetime <= 0f)
            {
                Runner.Despawn(Object);
                return;
            }

            // Tech review D8: shared NonAlloc query (was an allocating
            // Physics.OverlapSphere every tick). Same rule: first Health that
            // isn't the shooter takes the hit.
            var radius = _attack != null && _attack.AreaRadius > 0f ? _attack.AreaRadius : 0.5f;
            if (HitDetectionService.TryFindFirstHealth(transform.position, radius, _source, out var target))
            {
                var result = DamageService.Resolve(new DamageRequest(_source, target, _attack));
                // Tech review D20: without this line a regression run can't tell whether arrows hit.
                GameLog.Info($"[Projectile] {(_source != null ? _source.name : "<none>")} projectile-hit " +
                             $"{target.name} for {result.FinalDamage} ({(_attack != null ? _attack.AttackId : "?")}).");
                Runner.Despawn(Object);
            }
        }
    }
}
