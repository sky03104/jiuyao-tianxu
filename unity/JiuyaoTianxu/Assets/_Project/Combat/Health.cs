using Fusion;
using UnityEngine;

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
            Debug.Log($"[Health] {name} took {amount} damage, HP now {HP}");
        }
    }
}
