using Fusion;
using JiuyaoTianxu.Combat;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// 燃燒 (赤炎) as its own tiny component on anything that can burn — players AND
    /// monsters (tech review D1, 咖哩裁定方案 A). Before this, burn state lived in the
    /// target's SpiritSealSystem, so a monster (no seal system) silently never
    /// burned while 赤炎 still spent its cooldown.
    ///
    /// Deliberately still NOT a Status Effect Framework (HANDOFF-007 §9 / 14): one
    /// effect, one component. Burn damage still goes through DamageService with
    /// IsStatusDamage, so it can't re-trigger the seal that applied it and a burn
    /// kill still raises CombatEvents.TargetKilled credited to the burn's source.
    /// Behaviour matches Phase 0-C exactly: a new burn replaces the old one.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class BurnStatus : NetworkBehaviour
    {
        [Networked] public int TicksRemaining { get; private set; }
        [Networked] private int DamagePerTick { get; set; }
        [Networked] private float TickInterval { get; set; }
        [Networked] private TickTimer TickTimer { get; set; }

        private Health _health;
        private Health _source; // plain ref: only ever read/written server-side.

        public bool IsBurning => TicksRemaining > 0;

        public override void Spawned()
        {
            _health = GetComponent<Health>();
        }

        /// <summary>Server-only. Starts (or restarts) the burn.</summary>
        public void Apply(Health source, int damagePerTick, int tickCount, float tickInterval)
        {
            if (!Object.HasStateAuthority) return;

            _source = source;
            DamagePerTick = damagePerTick;
            TickInterval = tickInterval;
            TicksRemaining = tickCount;
            TickTimer = TickTimer.CreateFromSeconds(Runner, tickInterval);
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;
            if (TicksRemaining <= 0) return;
            if (!TickTimer.ExpiredOrNotRunning(Runner)) return;

            DamageService.Resolve(new DamageRequest(_source, _health, null, DamagePerTick, isStatusDamage: true));
            TicksRemaining--;

            if (TicksRemaining > 0)
            {
                TickTimer = TickTimer.CreateFromSeconds(Runner, TickInterval);
            }
        }
    }
}
