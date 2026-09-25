using Fusion;
using UnityEngine;
using JiuyaoTianxu.Core;

namespace JiuyaoTianxu.Combat
{
    /// <summary>
    /// Server-authoritative HP. Only the object's StateAuthority (the dedicated
    /// server in Phase 0-A's Server topology) is allowed to mutate HP — clients only
    /// ever read the networked value and play back the result.
    /// </summary>
    public class Health : NetworkBehaviour
    {
        [SerializeField] private int _maxHp = 100;

        [Networked] public int HP { get; private set; }

        /// <summary>Read-only, for HUD bars (Phase 0-E).</summary>
        public int MaxHp => _maxHp;

        /// <summary>Tech review D3/D11: HP 0 means dead — can't act, can't be hit.
        /// Monsters leave via MonsterLifecycle, players come back via PlayerLifecycle.</summary>
        public bool IsDead => HP <= 0;

        public override void Spawned()
        {
            if (Object.HasStateAuthority)
            {
                HP = _maxHp;
            }
        }

        /// <summary>
        /// Server-only. Never call this from a client and trust the result locally —
        /// the networked HP property is the single source of truth for all peers.
        /// </summary>
        public void ApplyDamage(int amount)
        {
            if (!Object.HasStateAuthority)
            {
                Debug.LogWarning($"[Health] ApplyDamage called without StateAuthority on {name}; ignored.");
                return;
            }

            HP = Mathf.Max(0, HP - amount);
            GameLog.Info($"[Health] {name} took {amount} damage, HP now {HP}");
        }

        /// <summary>Server-only. Refill to max (respawn). Not a heal system — there is
        /// none yet; only PlayerLifecycle's test respawn calls this.</summary>
        public void ServerRestoreFull()
        {
            if (!Object.HasStateAuthority) return;
            HP = _maxHp;
        }
    }
}
