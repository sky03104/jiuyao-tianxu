using System.Collections.Generic;
using Fusion;
using JiuyaoTianxu.Combat;
using UnityEngine;

namespace JiuyaoTianxu.UI.Hud
{
    /// <summary>
    /// 基礎受擊回饋 (roadmap Phase 0 「受擊判定、基礎回饋」), client-side only: watches
    /// the replicated HP and, when it drops, flashes the model and queues a damage
    /// number for CombatHudOverlay. Purely presentational — it reads Health, it never
    /// writes anything, so it cannot affect server-authoritative results.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class HealthFeedback : NetworkBehaviour
    {
        [SerializeField] private Color _flashColor = new(1f, 0.35f, 0.3f);
        [SerializeField] private float _flashSeconds = 0.12f;

        public struct DamageNumber
        {
            public Vector3 WorldPosition;
            public int Amount;
            public float Time;
        }

        public static readonly List<HealthFeedback> All = new();
        public static readonly List<DamageNumber> RecentNumbers = new();

        public Health Health { get; private set; }

        private int _lastHp = -1;
        private float _flashUntil;
        private Renderer[] _renderers;
        private MaterialPropertyBlock _block;
        private bool _flashing;

        public override void Spawned()
        {
            Health = GetComponent<Health>();
            _renderers = GetComponentsInChildren<Renderer>();
            _block = new MaterialPropertyBlock();
            All.Add(this);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            All.Remove(this);
        }

        public override void Render()
        {
            if (Application.isBatchMode) return; // headless server/autotest: nothing to show.

            var hp = Health.HP;
            if (_lastHp >= 0 && hp < _lastHp)
            {
                _flashUntil = Time.time + _flashSeconds;
                RecentNumbers.Add(new DamageNumber
                {
                    WorldPosition = transform.position + Vector3.up * 1.2f,
                    Amount = _lastHp - hp,
                    Time = Time.time,
                });
            }
            _lastHp = hp;

            var shouldFlash = Time.time < _flashUntil;
            if (shouldFlash == _flashing) return;
            _flashing = shouldFlash;

            foreach (var r in _renderers)
            {
                if (r == null) continue;
                if (shouldFlash)
                {
                    r.GetPropertyBlock(_block);
                    _block.SetColor("_BaseColor", _flashColor); // URP Lit
                    _block.SetColor("_Color", _flashColor);     // Built-in fallback
                    r.SetPropertyBlock(_block);
                }
                else
                {
                    r.SetPropertyBlock(null);
                }
            }
        }
    }
}
