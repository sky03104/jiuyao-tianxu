using System;
using Fusion;
using JiuyaoTianxu.Combat;
using UnityEngine;

namespace JiuyaoTianxu.Gameplay.Events
{
    /// <summary>Server-side "an enemy with a gameplay identity died" fact.</summary>
    public readonly struct EnemyKilledEvent
    {
        /// <summary>Which runner this happened on — listeners filter by it so the
        /// event stays correct even if several runners share one process.</summary>
        public readonly NetworkRunner Runner;
        /// <summary>Data id from EnemyIdentity (e.g. "Phase0D_TestMonster"),
        /// matched against QuestDefinition.TargetId — never a C# class name.</summary>
        public readonly string TargetId;
        public readonly Health Killer;
        public readonly Health Victim;

        public EnemyKilledEvent(NetworkRunner runner, string targetId, Health killer, Health victim)
        {
            Runner = runner;
            TargetId = targetId;
            Killer = killer;
            Victim = victim;
        }
    }

    /// <summary>
    /// Gameplay-level event hub (HANDOFF-008 §8/§11). Quest Tracker subscribes
    /// here; it never searches the scene for monsters and never touches
    /// Health/DamageService. Server-side only.
    /// </summary>
    public static class GameplayEvents
    {
        public static event Action<EnemyKilledEvent> EnemyKilled;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics() => EnemyKilled = null;

        public static void RaiseEnemyKilled(in EnemyKilledEvent evt)
        {
            var handlers = EnemyKilled;
            if (handlers == null) return;

            foreach (var handler in handlers.GetInvocationList())
            {
                try
                {
                    ((Action<EnemyKilledEvent>)handler)(evt);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}
