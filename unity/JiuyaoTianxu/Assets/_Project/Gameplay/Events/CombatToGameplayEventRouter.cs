using JiuyaoTianxu.Combat;
using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Gameplay.World;
using UnityEngine;

namespace JiuyaoTianxu.Gameplay.Events
{
    /// <summary>
    /// Translates the generic combat fact (CombatEvents.TargetKilled) into the
    /// gameplay fact quests care about (GameplayEvents.EnemyKilled):
    ///
    ///   DamageService → CombatEvents.TargetKilled → [this] → GameplayEvents.EnemyKilled → QuestTracker
    ///
    /// Combat stays ignorant of "enemies"/"quests"; Quest stays ignorant of
    /// Health. Only targets carrying an EnemyIdentity become EnemyKilled events,
    /// so player-vs-player kills never count toward a kill quest.
    /// </summary>
    public static class CombatToGameplayEventRouter
    {
        // AfterAssembliesLoaded runs after SubsystemRegistration, i.e. after
        // CombatEvents has reset its handlers, so this subscription survives.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Install()
        {
            CombatEvents.TargetKilled -= OnTargetKilled;
            CombatEvents.TargetKilled += OnTargetKilled;
        }

        private static void OnTargetKilled(Health source, Health target)
        {
            if (target == null) return;

            var identity = target.GetComponent<EnemyIdentity>();
            if (identity == null) return;

            GameplayEvents.RaiseEnemyKilled(new EnemyKilledEvent(target.Runner, identity.TargetId, source, target));
        }
    }
}
