using Fusion;
using JiuyaoTianxu.Combat;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// The one shared Trigger/Modifier system every Spirit Seal effect runs
    /// through (HANDOFF-007 §5). DamageService calls three generic hooks on this
    /// — it never knows which seal is equipped. Adding a 4th seal only means a
    /// new SpiritSealDefinition asset; nothing here or in CombatController/
    /// DamageService changes.
    ///
    /// 赤炎's burn itself lives on the TARGET's BurnStatus component (tech review
    /// D1) so monsters can burn too; this class only decides when to apply it.
    /// </summary>
    [RequireComponent(typeof(SpiritSealLoadout))]
    [RequireComponent(typeof(Health))]
    public class SpiritSealSystem : NetworkBehaviour
    {
        [SerializeField] private SpiritSealRegistry _registry;

        [Networked] private NetworkButtons _previousButtons { get; set; }

        private SpiritSealLoadout _loadout;
        private Health _health;

        public override void Spawned()
        {
            _loadout = GetComponent<SpiritSealLoadout>();
            _health = GetComponent<Health>();
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;

            if (GetInput(out PlayerInputData input))
            {
                if (input.Buttons.WasPressed(_previousButtons, PlayerButton.DodgeTest))
                {
                    TriggerDodgeTestEvent();
                }
                _previousButtons = input.Buttons;
            }
        }

        // ---- Test/bootstrap helpers ----

        /// <summary>Server-only convenience for Phase 0-C: no backpack UI exists yet,
        /// so the three prototype seals are simply equipped at spawn.</summary>
        public void EquipTestLoadout(int slot0SealId, int slot1SealId, int slot2SealId)
        {
            if (!Object.HasStateAuthority) return;
            _loadout.TryEquip(0, slot0SealId);
            _loadout.TryEquip(1, slot1SealId);
            _loadout.TryEquip(2, slot2SealId);
            Debug.Log($"[SpiritSealSystem] {name} equipped test loadout: {slot0SealId}, {slot1SealId}, {slot2SealId}.");
        }

        // ---- Hooks called by DamageService ----

        /// <summary>Attacker-side: consumes an armed 影遁-style bonus, if any, before
        /// the base damage is finalized.</summary>
        public int ModifyOutgoingDamage(int baseDamage)
        {
            for (var slot = 0; slot < SpiritSealLoadout.SlotCount; slot++)
            {
                var def = _registry.GetById(_loadout.SealIds[slot]);
                if (def == null || !def.ArmsForNextAttack) continue;
                if (!_loadout.Armed[slot]) continue;

                _loadout.Armed.Set(slot, false);
                Debug.Log($"[SpiritSealSystem] {name} consumed armed seal '{def.DisplayName}' for +{def.BonusDamageWhenArmed} damage.");
                return baseDamage + def.BonusDamageWhenArmed;
            }
            return baseDamage;
        }

        /// <summary>Attacker-side: fires after a real weapon hit lands (never for
        /// status-effect ticks, so 赤炎 can't re-trigger itself off its own burn).</summary>
        public void OnAttackHitDealt(Health target, int finalDamage)
        {
            for (var slot = 0; slot < SpiritSealLoadout.SlotCount; slot++)
            {
                var def = _registry.GetById(_loadout.SealIds[slot]);
                if (def == null || def.TriggerType != SpiritSealTriggerType.OnAttackHit) continue;
                if (!_loadout.Cooldowns[slot].ExpiredOrNotRunning(Runner)) continue;

                // A burn seal only fires (and only spends its cooldown) if the target
                // can actually burn. Before D1 this silently no-op'd on monsters while
                // still consuming the cooldown.
                var appliesBurn = def.BurnTickCount > 0 && def.BurnDamagePerTick > 0;
                if (appliesBurn)
                {
                    var burn = target.GetComponent<BurnStatus>();
                    if (burn == null) continue;
                    burn.Apply(_health, def.BurnDamagePerTick, def.BurnTickCount, def.BurnTickInterval);
                }

                _loadout.Cooldowns.Set(slot, TickTimer.CreateFromSeconds(Runner, def.Cooldown));
                Debug.Log($"[SpiritSealSystem] {name} triggered '{def.DisplayName}' on {target.name}.");
            }
        }

        /// <summary>Defender-side: caps a would-be-lethal hit so HP lands at the
        /// seal's configured floor instead of 0.</summary>
        public int TryPreventFatalDamage(int incomingDamage, int currentHp)
        {
            if (currentHp - incomingDamage > 0) return incomingDamage;

            for (var slot = 0; slot < SpiritSealLoadout.SlotCount; slot++)
            {
                var def = _registry.GetById(_loadout.SealIds[slot]);
                if (def == null || def.TriggerType != SpiritSealTriggerType.OnFatalDamage) continue;
                if (!_loadout.Cooldowns[slot].ExpiredOrNotRunning(Runner)) continue;

                _loadout.Cooldowns.Set(slot, TickTimer.CreateFromSeconds(Runner, def.Cooldown));
                var adjusted = Mathf.Max(0, currentHp - def.FatalSaveMinHp);
                Debug.Log($"[SpiritSealSystem] {name} triggered '{def.DisplayName}', capping fatal damage " +
                          $"{incomingDamage}->{adjusted} to survive at {def.FatalSaveMinHp} HP.");
                return adjusted;
            }

            return incomingDamage;
        }

        // ---- Dodge test event ----

        private void TriggerDodgeTestEvent()
        {
            for (var slot = 0; slot < SpiritSealLoadout.SlotCount; slot++)
            {
                var def = _registry.GetById(_loadout.SealIds[slot]);
                if (def == null || def.TriggerType != SpiritSealTriggerType.OnDodgeEvent) continue;
                if (!_loadout.Cooldowns[slot].ExpiredOrNotRunning(Runner)) continue;

                _loadout.Armed.Set(slot, true);
                _loadout.Cooldowns.Set(slot, TickTimer.CreateFromSeconds(Runner, def.Cooldown));
                Debug.Log($"[SpiritSealSystem] {name} armed '{def.DisplayName}' from test dodge event.");
            }
        }
    }
}
