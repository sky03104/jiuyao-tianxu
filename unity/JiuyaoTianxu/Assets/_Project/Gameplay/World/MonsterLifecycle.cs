using Fusion;
using JiuyaoTianxu.Combat;
using UnityEngine;
using JiuyaoTianxu.Core;

namespace JiuyaoTianxu.Gameplay.World
{
    /// <summary>
    /// Phase 0-D minimal monster: Idle → 被攻擊 → 受傷 → 死亡 (HANDOFF-008 §4). No
    /// AI, no movement. Uses the existing Health — damage only ever arrives through
    /// the shared DamageService pipeline, this only watches HP to know when to
    /// leave the world. It never talks to quests: the kill is already announced by
    /// DamageService → CombatEvents on the exact hit that ended it.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class MonsterLifecycle : NetworkBehaviour
    {
        [SerializeField] private float _despawnDelay = 0.5f;

        [Networked] public NetworkBool IsDead { get; private set; }
        [Networked] private TickTimer DespawnTimer { get; set; }

        private Health _health;
        private Renderer[] _renderers;

        public override void Spawned()
        {
            _health = GetComponent<Health>();
            _renderers = GetComponentsInChildren<Renderer>();
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;

            if (!IsDead && _health.HP <= 0)
            {
                IsDead = true;
                DespawnTimer = TickTimer.CreateFromSeconds(Runner, _despawnDelay);
                GameLog.Info($"[MonsterLifecycle] {name} died; despawning in {_despawnDelay}s.");
            }

            if (IsDead && DespawnTimer.Expired(Runner))
            {
                Runner.Despawn(Object);
            }
        }

        public override void Render()
        {
            // Visual only: hide the corpse on every peer during the despawn delay.
            if (_renderers == null) return;
            foreach (var r in _renderers)
            {
                if (r != null) r.enabled = !IsDead;
            }
        }
    }
}
