using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Data-driven description of one Spirit Seal (HANDOFF-007 §4). SpiritSealSystem
    /// only ever reads these fields — there is no `if (sealId == Blaze) {...}`
    /// anywhere in the framework. A 4th seal means a 4th asset, not a code change.
    /// </summary>
    [CreateAssetMenu(menuName = "JiuyaoTianxu/Combat/Spirit Seal Definition", fileName = "SpiritSealDefinition")]
    public class SpiritSealDefinition : ScriptableObject
    {
        [Header("Identity")]
        [Tooltip("Stable numeric id — this is what actually gets networked, never the asset reference itself.")]
        public int SealId;
        public string DisplayName;
        public bool Equipable = true;

        [Header("Trigger")]
        public SpiritSealTriggerType TriggerType;
        [Tooltip("Seconds before this seal can trigger again after firing.")]
        public float Cooldown = 3f;

        [Header("赤炎 (OnAttackHit) — applies a burn to the target")]
        public int BurnDamagePerTick = 3;
        public int BurnTickCount = 3;
        public float BurnTickInterval = 1f;

        [Header("玄甲 (OnFatalDamage) — caps lethal damage instead of dying")]
        [Tooltip("HP the wearer is left at when this seal intercepts a killing blow.")]
        public int FatalSaveMinHp = 1;

        [Header("影遁 (OnDodgeEvent) — arms a one-shot bonus on the wearer's next attack")]
        public bool ArmsForNextAttack;
        public int BonusDamageWhenArmed = 5;
    }
}
