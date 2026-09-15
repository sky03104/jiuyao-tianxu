namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// The six established weapon flows (呼應03_COMBAT_SYSTEM 已定案六流派).
    /// Names/order must never be changed — they are locked in the project's
    /// world-lore documents, not just gameplay labels.
    /// </summary>
    public enum WeaponType
    {
        Blade,      // 刀 — 近戰爆發
        Sword,      // 劍 — 高機動連擊
        Spear,      // 槍 — 中距離控制
        Bow,        // 弓 — 遠程蓄力
        HeavyBlade, // 重刃 — 慢速防禦控場
        Staff,      // 靈杖 — 遠程法術控制
    }
}
