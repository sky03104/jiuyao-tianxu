using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// One weapon flow's full combo sequence. CombatController asks this for
    /// "what's my step-N attack" — it never touches AttackDefinition arrays
    /// directly, so adding a 7th weapon later is "add one asset", not "add a case".
    /// </summary>
    [CreateAssetMenu(menuName = "JiuyaoTianxu/Combat/Weapon Definition", fileName = "WeaponDefinition")]
    public class WeaponDefinition : ScriptableObject
    {
        public WeaponType WeaponType;
        [Tooltip("Ordered by ComboStep, index 0 = step 1.")]
        public AttackDefinition[] ComboSequence;

        [Tooltip("Base move speed while simply walking (not attacking).")]
        public float BaseMoveSpeed = 4f;

        public AttackDefinition GetStep(int comboStep)
        {
            if (ComboSequence == null || ComboSequence.Length == 0) return null;
            var index = Mathf.Clamp(comboStep - 1, 0, ComboSequence.Length - 1);
            return ComboSequence[index];
        }

        public int StepCount => ComboSequence?.Length ?? 0;
    }
}
