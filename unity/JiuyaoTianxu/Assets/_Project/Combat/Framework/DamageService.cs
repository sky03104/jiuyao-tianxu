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

            var raw = request.Attack != null ? request.Attack.Damage : 0;
            // Final damage placeholder — armor/resistance/crit hooks land here later,
            // Phase 0-B intentionally ships raw==final.
            var final = raw;

            request.Target.ApplyDamage(final);
            var died = request.Target.HP <= 0;

            return new DamageResult(raw, final, died);
        }
    }
}
