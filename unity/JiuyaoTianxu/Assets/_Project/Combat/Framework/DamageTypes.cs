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

        public DamageRequest(Health source, Health target, AttackDefinition attack)
        {
            Source = source;
            Target = target;
            Attack = attack;
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
