using JiuyaoTianxu.Combat;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// What CombatController asks DamageService to resolve. Never applied to
    /// Health directly by the controller — DamageService is the only writer.
    /// </summary>
    public readonly struct DamageRequest
    {
        public readonly Health Source;
        public readonly Health Target;
        public readonly AttackDefinition Attack;
        /// <summary>Phase 0-C: lets status-effect ticks (e.g. 赤炎's burn) supply a
        /// flat number with no AttackDefinition asset behind them.</summary>
        public readonly int? FlatDamageOverride;
        /// <summary>Phase 0-C: true for status-effect ticks. DamageService skips the
        /// attacker-side Spirit Seal hooks for these, so a burn tick can't
        /// re-trigger the very seal that applied it.</summary>
        public readonly bool IsStatusDamage;

        public DamageRequest(Health source, Health target, AttackDefinition attack,
            int? flatDamageOverride = null, bool isStatusDamage = false)
        {
            Source = source;
            Target = target;
            Attack = attack;
            FlatDamageOverride = flatDamageOverride;
            IsStatusDamage = isStatusDamage;
        }
    }

    /// <summary>
    /// What DamageService hands back after resolving a request. Deliberately flat
    /// today (HANDOFF-006 §9 — "only the minimal implementation"); Crit/Element/
    /// Armor/Resistance/Shield/StatusEffect are future fields on this same struct,
    /// not a reason to redesign the pipeline later.
    /// </summary>
    public readonly struct DamageResult
    {
        public readonly int RawDamage;
        public readonly int FinalDamage;
        public readonly bool TargetDied;

        public DamageResult(int rawDamage, int finalDamage, bool targetDied)
        {
            RawDamage = rawDamage;
            FinalDamage = finalDamage;
            TargetDied = targetDied;
        }
    }
}
