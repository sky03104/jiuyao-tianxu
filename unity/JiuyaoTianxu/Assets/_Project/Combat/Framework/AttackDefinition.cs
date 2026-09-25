using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Data-driven description of one attack / combo step (HANDOFF-006 §0-B-02).
    /// CombatController and DamageService only ever read from instances of this —
    /// no weapon numbers are hardcoded in the controller. Six weapons therefore
    /// mean six sets of assets, not six scripts.
    /// </summary>
    [CreateAssetMenu(menuName = "JiuyaoTianxu/Combat/Attack Definition", fileName = "AttackDefinition")]
    public class AttackDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string AttackId;
        public WeaponType WeaponType;
        [Tooltip("1-based position in the weapon's combo sequence.")]
        public int ComboStep = 1;

        [Header("Damage")]
        public int Damage = 10;

        [Header("Timing (seconds)")]
        [Tooltip("Delay before the hit window opens — wind-up / telegraph.")]
        public float StartupTime = 0.1f;
        [Tooltip("How long the hit window stays open once active.")]
        public float ActiveTime = 0.15f;
        [Tooltip("Recovery after the active window before Idle/next combo step.")]
        public float RecoveryTime = 0.2f;
        [Tooltip("Extra cooldown before this attack can be used again (beyond recovery).")]
        public float Cooldown = 0f;
        [Tooltip("Bow/Staff only: time the button must be held before release/cast resolves.")]
        public float ChargeOrCastTime = 0f;

        [Header("Input")]
        [Tooltip("Tap = normal combo step; HoldRelease = charge then fire on release; Cast = cast delay then resolve.")]
        public AttackInputMode InputMode = AttackInputMode.Tap;

        [Header("Combo")]
        public bool CanCombo = true;
        [Tooltip("Window after RecoveryTime ends during which the next combo input still chains.")]
        public float ComboWindow = 0.4f;

        [Header("Movement")]
        public bool CanMoveDuringAttack = false;
        [Range(0f, 1f)] public float MoveSpeedMultiplier = 0f;

        [Header("Resource")]
        public int ResourceCost = 0;

        [Header("Hit Detection")]
        public HitShapeType HitShape = HitShapeType.Sphere;
        public float Range = 2f;
        [Tooltip("Box/Capsule only.")]
        public Vector3 HitExtents = new(1f, 1f, 1f);
        [Tooltip("Projectile/Area only: travel speed (0 = instant/hitscan for Area).")]
        public float ProjectileSpeed = 15f;
        [Tooltip("Area/Projectile-impact AOE radius; 0 = single target.")]
        public float AreaRadius = 0f;

        [Header("Weapon Feel Hooks (Phase 0-B placeholder, no VFX/animation yet)")]
        public bool AppliesKnockback = false;
        public float KnockbackForce = 0f;
        public bool AppliesArmorBreak = false;
        public bool GrantsSuperArmor = false; // 重刃 "簡單霸體概念"
    }
}
