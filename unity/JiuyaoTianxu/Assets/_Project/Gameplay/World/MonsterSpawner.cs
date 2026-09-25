using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using JiuyaoTianxu.Core;

namespace JiuyaoTianxu.Gameplay.World
{
    /// <summary>
    /// Server-only: keeps one monster alive per MonsterSpawnPoint in the scene,
    /// respawning a replacement some seconds after the previous one despawns so a
    /// kill quest (and repeated regression runs) always has targets. Clients never
    /// run this — Begin() refuses on a non-server runner, and every monster is a
    /// server-spawned NetworkObject that simply replicates to clients.
    /// </summary>
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private NetworkObject _monsterPrefab;
        [SerializeField] private float _respawnDelay = 2f;

        private sealed class Slot
        {
            public MonsterSpawnPoint Point;
            public NetworkObject Current;
            public TickTimer RespawnTimer;
        }

        private readonly List<Slot> _slots = new();
        private NetworkRunner _runner;
        private int _spawnCount;

        /// <summary>Idempotent; safe to call from both StartGame completion and
        /// OnSceneLoadDone.</summary>
        public void Begin(NetworkRunner runner)
        {
            if (runner == null || !runner.IsServer) return;
            if (_runner != null) return;
            if (_monsterPrefab == null)
            {
                Debug.LogError("[MonsterSpawner] No monster prefab assigned.");
                return;
            }

            _runner = runner;
            var points = FindObjectsByType<MonsterSpawnPoint>(FindObjectsSortMode.None).OrderBy(p => p.Index);
            foreach (var point in points)
            {
                var slot = new Slot { Point = point };
                _slots.Add(slot);
                SpawnInto(slot);
            }

            GameLog.Info($"[MonsterSpawner] Began on server with {_slots.Count} spawn point(s).");
        }

        private void Update()
        {
            if (_runner == null || !_runner.IsRunning) return;

            foreach (var slot in _slots)
            {
                if (slot.Current != null && slot.Current.IsValid) continue;

                if (!slot.RespawnTimer.IsRunning)
                {
                    slot.RespawnTimer = TickTimer.CreateFromSeconds(_runner, _respawnDelay);
                }
                else if (slot.RespawnTimer.Expired(_runner))
                {
                    SpawnInto(slot);
                }
            }
        }

        private void SpawnInto(Slot slot)
        {
            var t = slot.Point.transform;
            slot.Current = _runner.Spawn(_monsterPrefab, t.position, t.rotation);
            slot.RespawnTimer = TickTimer.None;
            _spawnCount++;
            if (slot.Current != null)
            {
                slot.Current.name = $"{_monsterPrefab.name}#{_spawnCount}";
            }
            GameLog.Info($"[MonsterSpawner] Spawned {_monsterPrefab.name}#{_spawnCount} at point {slot.Point.Index}.");
        }
    }
}
