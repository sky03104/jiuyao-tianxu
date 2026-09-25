using Fusion;
using JiuyaoTianxu.Combat;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.Gameplay.World
{
    /// <summary>
    /// 倒地／復活「測試版」(tech review D3, 咖哩 2026-09-25 同意先做): when a player's HP
    /// hits 0 they are down for <see cref="_respawnSeconds"/>, then the server puts
    /// them back at the spot they first spawned with full HP. No penalty, no
    /// revive-by-teammate, no dungeon rules — the real rules are still to be
    /// specified in a HANDOFF; this only makes the combat loop repeatable.
    ///
    /// While down, CombatController / PlayerMovement / TargetLock / SpiritSealSystem
    /// ignore input and HitDetectionService no longer returns the player as a target
    /// (they all read Health.IsDead) — so this class only owns the timer + respawn.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class PlayerLifecycle : NetworkBehaviour
    {
        [SerializeField] private float _respawnSeconds = 5f; // 可調整

        [Networked] public NetworkBool IsDown { get; private set; }
        [Networked] private TickTimer RespawnTimer { get; set; }
        [Networked] public int DeathCount { get; private set; }

        /// <summary>This peer's own player (null on a dedicated server). HUD reads it.</summary>
        public static PlayerLifecycle Local { get; private set; }

        private Health _health;
        private Renderer[] _renderers;
        private Vector3 _spawnPosition;
        private Quaternion _spawnRotation;

        public override void Spawned()
        {
            _health = GetComponent<Health>();
            _renderers = GetComponentsInChildren<Renderer>();
            _spawnPosition = transform.position;
            _spawnRotation = transform.rotation;
            if (Object.HasInputAuthority) Local = this;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (Local == this) Local = null;
        }

        /// <summary>Seconds until respawn on any peer; 0 when not down.</summary>
        public float SecondsUntilRespawn =>
            IsDown && Runner != null ? RespawnTimer.RemainingTime(Runner) ?? 0f : 0f;

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;

            if (!IsDown && _health.IsDead)
            {
                IsDown = true;
                DeathCount++;
                RespawnTimer = TickTimer.CreateFromSeconds(Runner, _respawnSeconds);
                GameLog.Info($"[PlayerLifecycle] {name} is down (death #{DeathCount}); respawn in {_respawnSeconds}s.");
                return;
            }

            if (IsDown && RespawnTimer.Expired(Runner))
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            var nt = GetComponent<NetworkTransform>();
            if (nt != null) nt.Teleport(_spawnPosition, _spawnRotation); // no lerp across the map on clients
            else transform.SetPositionAndRotation(_spawnPosition, _spawnRotation);

            _health.ServerRestoreFull();
            IsDown = false;
            RespawnTimer = TickTimer.None;
            GameLog.Info($"[PlayerLifecycle] {name} respawned with {_health.HP} HP.");
        }

        public override void Render()
        {
            if (_renderers == null) return;
            foreach (var r in _renderers)
            {
                if (r != null) r.enabled = !IsDown;
            }
        }
    }
}
