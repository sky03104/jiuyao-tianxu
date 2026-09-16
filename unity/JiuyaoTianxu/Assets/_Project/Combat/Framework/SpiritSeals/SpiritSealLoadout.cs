using Fusion;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// The fixed 8 shared Spirit Seal slots (HANDOFF-007 §3). One set per
    /// character, never doubled by dual-meridian switching, never per-weapon —
    /// that rule is enforced simply by this being the only loadout component on
    /// the Player prefab. Pure data holder; SpiritSealSystem decides what the
    /// data means.
    /// </summary>
    public class SpiritSealLoadout : NetworkBehaviour
    {
        public const int SlotCount = 8;

        [Networked, Capacity(SlotCount)]
        public NetworkArray<int> SealIds => default;

        [Networked, Capacity(SlotCount)]
        public NetworkArray<TickTimer> Cooldowns => default;

        [Networked, Capacity(SlotCount)]
        public NetworkArray<NetworkBool> Armed => default;

        /// <summary>Server-only. Equipping is not combat-critical enough to need
        /// its own request/response ceremony for Phase 0-C — a real backpack UI
        /// would route through an RPC, but there is no backpack yet (HANDOFF-007
        /// explicitly defers it).</summary>
        public bool TryEquip(int slot, int sealId)
        {
            if (!Object.HasStateAuthority)
            {
                Debug.LogWarning("[SpiritSealLoadout] TryEquip called without StateAuthority; ignored.");
                return false;
            }
            if (slot < 0 || slot >= SlotCount) return false;

            SealIds.Set(slot, sealId);
            Cooldowns.Set(slot, TickTimer.None);
            Armed.Set(slot, false);
            return true;
        }

        public void Unequip(int slot)
        {
            if (!Object.HasStateAuthority) return;
            if (slot < 0 || slot >= SlotCount) return;

            SealIds.Set(slot, 0);
        }
    }
}
