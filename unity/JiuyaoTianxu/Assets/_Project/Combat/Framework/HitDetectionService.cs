using System.Collections.Generic;
using JiuyaoTianxu.Combat;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// The only place in the project allowed to call Physics.Overlap* for combat
    /// (HANDOFF-006 §8). CombatController asks this "who did I hit", it never
    /// queries Physics itself. Projectile hits are the one exception — a
    /// Projectile is a spawned NetworkObject that resolves its own hit on trigger
    /// and calls DamageService directly, since "a thing flying through the world
    /// over time" isn't an instantaneous overlap query.
    /// </summary>
    public static class HitDetectionService
    {
        private static readonly Collider[] Buffer = new Collider[16];

        /// <summary>
        /// Resolves Sphere/Box/Capsule/Area shapes as an instantaneous overlap
        /// query at the attacker's current position/facing. Not valid for
        /// HitShapeType.Projectile — spawn a projectile for that instead.
        /// </summary>
        public static List<Health> FindTargets(AttackDefinition attack, Transform origin, Health self)
        {
            var results = new List<Health>();
            if (attack.HitShape == HitShapeType.Projectile)
            {
                Debug.LogWarning($"[HitDetectionService] {attack.AttackId} is Projectile-shaped; " +
                                  "FindTargets doesn't resolve those, spawn a Projectile instead.");
                return results;
            }

            var count = attack.HitShape switch
            {
                HitShapeType.Sphere => Physics.OverlapSphereNonAlloc(
                    origin.position + origin.forward * (attack.Range * 0.5f), attack.Range * 0.5f, Buffer),
                HitShapeType.Box => Physics.OverlapBoxNonAlloc(
                    origin.position + origin.forward * (attack.Range * 0.5f), attack.HitExtents * 0.5f, Buffer,
                    origin.rotation),
                HitShapeType.Capsule => Physics.OverlapCapsuleNonAlloc(
                    origin.position, origin.position + origin.forward * attack.Range, attack.HitExtents.x, Buffer),
                HitShapeType.Area => Physics.OverlapSphereNonAlloc(origin.position, attack.AreaRadius, Buffer),
                _ => 0,
            };

            for (var i = 0; i < count; i++)
            {
                if (Buffer[i].gameObject == self.gameObject) continue;
                var target = Buffer[i].GetComponentInParent<Health>();
                if (target != null && !results.Contains(target)) results.Add(target);
            }

            return results;
        }

        /// <summary>Every Health within <paramref name="radius"/> of a point, excluding
        /// <paramref name="self"/>. Used by lock-on target search (never applies damage)
        /// so Physics queries still live only in this class.</summary>
        public static List<Health> FindHealthInRadius(Vector3 center, float radius, Health self)
        {
            var results = new List<Health>();
            var count = Physics.OverlapSphereNonAlloc(center, radius, Buffer);
            for (var i = 0; i < count; i++)
            {
                var target = Buffer[i].GetComponentInParent<Health>();
                if (target == null || target == self || results.Contains(target)) continue;
                results.Add(target);
            }
            return results;
        }

        /// <summary>Area query centered at an arbitrary world point (e.g. a staff ground AOE).</summary>
        public static List<Health> FindTargetsAt(AttackDefinition attack, Vector3 center, Health self)
        {
            var results = new List<Health>();
            var radius = attack.AreaRadius > 0f ? attack.AreaRadius : attack.Range;
            var count = Physics.OverlapSphereNonAlloc(center, radius, Buffer);
            for (var i = 0; i < count; i++)
            {
                if (Buffer[i].gameObject == self.gameObject) continue;
                var target = Buffer[i].GetComponentInParent<Health>();
                if (target != null && !results.Contains(target)) results.Add(target);
            }

            return results;
        }
    }
}
