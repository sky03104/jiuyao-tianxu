using Fusion;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.Combat
{
    /// <summary>
    /// Minimal server-authoritative combat loop for Phase 0-A:
    /// input intent -> Server hit detection -> Server damage calc -> HP change -> synced to clients.
    /// This is intentionally dumb (one flat-damage sphere overlap, no weapon flow, no VFX) —
    /// it exists only to prove the network+combat wiring, not to be a real combat system.
    /// </summary>
    public class PlayerCombat : NetworkBehaviour
    {
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private int _attackDamage = 10;

        [Networked] private NetworkButtons _previousButtons { get; set; }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;
            if (!GetInput(out PlayerInputData input)) return;

            if (input.Buttons.WasPressed(_previousButtons, PlayerButton.Attack))
            {
                TryAttack();
            }

            _previousButtons = input.Buttons;
        }

        private void TryAttack()
        {
            Debug.Log($"[PlayerCombat] {name} attacks.");

            foreach (var hitCollider in Physics.OverlapSphere(transform.position, _attackRange))
            {
                if (hitCollider.gameObject == gameObject) continue;

                var target = hitCollider.GetComponentInParent<Health>();
                if (target == null) continue;

                target.ApplyDamage(_attackDamage);
                Debug.Log($"[PlayerCombat] {name} hit {target.name} for {_attackDamage}.");
                break;
            }
        }
    }
}
