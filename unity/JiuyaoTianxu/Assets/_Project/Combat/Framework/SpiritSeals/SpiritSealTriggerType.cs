namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// When a Spirit Seal's effect fires (HANDOFF-007 §5). SpiritSealSystem is the
    /// only thing that switches on this — DamageService just calls generic hook
    /// methods, it never knows which seal is equipped or why.
    /// </summary>
    public enum SpiritSealTriggerType
    {
        /// <summary>Fires after this player's attack successfully deals damage (赤炎).</summary>
        OnAttackHit,
        /// <summary>Fires when incoming damage would otherwise kill this player (玄甲).</summary>
        OnFatalDamage,
        /// <summary>Fires on a (currently test-only) dodge/evade event (影遁).</summary>
        OnDodgeEvent,
    }
}
