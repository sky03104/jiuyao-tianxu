namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Hit detection shapes an AttackDefinition can request (HANDOFF-006 §8).
    /// HitDetectionService is the only place that switches on this — attack data
    /// and combat control never do their own Physics calls.
    /// </summary>
    public enum HitShapeType
    {
        Sphere,
        Box,
        Capsule,
        Projectile,
        Area, // Stationary AOE placed at a point (e.g. staff ground effect).
    }
}
