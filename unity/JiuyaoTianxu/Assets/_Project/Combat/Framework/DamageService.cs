using JiuyaoTianxu.Combat;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// The only writer of Health (HANDOFF-006 §9). CombatController builds a
    /// DamageRequest from an AttackDefinition and hands it here; this is where
    /// future Crit/Element/Armor/Resistance/Shield hooks get added without
    /// touching CombatController or Health at all.
    /// Server-only by construction: call sites already run inside
    /// `if (Object.HasStateAuthority)` blocks in CombatController, and this
    /// re-asserts that guard so a future caller can't skip it by mistake.
    /// </summary>
    public static class DamageService
    {
        public static DamageResult Resolve(DamageRequest request)
        {
            if (request.Target == null)
            {
                return new DamageResult(0, 0, false);
            }

            if (!request.Target.Object.HasStateAuthority)
            {
                Debug.LogWarning("[DamageService] Resolve called without StateAuthority over the target; ignored.");
                return new DamageResult(0, 0, false);
            }

            var raw = request.FlatDamageOverride ?? (request.Attack != null ? request.Attack.Damage : 0);

            // Phase 0-C: Spirit Seal hooks. DamageService never knows which seal is
            // equipped or why — SpiritSealSystem loops over its own equipped data.
            // Status-effect ticks (IsStatusDamage) skip the attacker-side hooks so a
            // burn tick can't re-trigger the seal that applied it.
            var sourceSeals = !request.IsStatusDamage && request.Source != null
                ? request.Source.GetComponent<SpiritSealSystem>()
                : null;
            if (sourceSeals != null)
            {
                raw = sourceSeals.ModifyOutgoingDamage(raw);
            }

            var final = raw;

            var targetSeals = request.Target.GetComponent<SpiritSealSystem>();
            if (targetSeals != null)
            {
                final = targetSeals.TryPreventFatalDamage(final, request.Target.HP);
            }

            var wasAlive = request.Target.HP > 0;
            request.Target.ApplyDamage(final);
            var died = request.Target.HP <= 0;

            sourceSeals?.OnAttackHitDealt(request.Target, final);

            // Phase 0-D: announce the alive→dead transition exactly once. Quest and
            // any other gameplay listener only ever consume this event.
            if (wasAlive && died)
            {
                CombatEvents.RaiseTargetKilled(request.Source, request.Target);
            }

            return new DamageResult(raw, final, died);
        }
    }
}
