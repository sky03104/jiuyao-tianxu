using System;
using JiuyaoTianxu.Combat;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Phase 0-D (HANDOFF-008 §11): the one generic "combat result" event other
    /// systems may listen to. Raised by DamageService only, server-side only, and
    /// only on the hit that takes a target from HP &gt; 0 to HP 0 — hitting a corpse
    /// again never re-raises it. Combat knows nothing about who listens (Quest,
    /// test runner, future achievements); listeners can never reach back into
    /// Health or DamageService through this.
    /// </summary>
    public static class CombatEvents
    {
        /// <summary>(source, target). Source may be null (environment damage).</summary>
        public static event Action<Health, Health> TargetKilled;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics() => TargetKilled = null; // Enter Play Mode without domain reload.

        internal static void RaiseTargetKilled(Health source, Health target)
        {
            var handlers = TargetKilled;
            if (handlers == null) return;

            // A buggy listener must never break the damage pipeline that raised it.
            foreach (var handler in handlers.GetInvocationList())
            {
                try
                {
                    ((Action<Health, Health>)handler)(source, target);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}
