namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Minimal combat state machine (HANDOFF-006 §6). Only the phases needed for
    /// Phase 0-B's six weapon MVPs are here; Dodge/Block/Stun/Knockback/Death are
    /// explicitly deferred — adding them later should only mean adding enum values
    /// and CombatController transitions, not rewriting this type.
    /// </summary>
    public enum CombatPhase
    {
        Idle,
        AttackStart,
        AttackActive,
        AttackRecovery,
        Charging,   // Bow: hold-to-charge before release.
        Casting,    // Staff: cast delay before the spell resolves.
    }
}
