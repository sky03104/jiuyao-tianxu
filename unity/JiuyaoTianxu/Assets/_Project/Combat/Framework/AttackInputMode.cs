namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// How an attack is driven by the attack button (tech review D2). Previously
    /// CombatController derived this from WeaponType (Bow = charge, Staff = cast);
    /// now it is per-attack data, so a charging blade skill or a cast-type spear
    /// technique is a table row, not a controller change.
    /// Values are serialized by name in attacks.csv and by number in assets —
    /// never renumber.
    /// </summary>
    public enum AttackInputMode
    {
        Tap = 0,         // press → Startup → Active → Recovery (melee combos)
        HoldRelease = 1, // hold → Charging → release fires (Bow)
        Cast = 2,        // press → Casting for ChargeOrCastTime → resolves (Staff)
    }
}
