using Fusion;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Orchestrates one shared Combat Framework across all six weapons
    /// (HANDOFF-006 §2.1 — this is deliberately the ONLY combat script; weapon
    /// differences live entirely in the WeaponDefinition/AttackDefinition data
    /// assigned per weapon, not in extra controller subclasses).
    ///
    /// Responsibilities: read input, drive CombatState's phase machine, ask
    /// HitDetectionService "who did I hit" (or spawn a Projectile for ranged
    /// weapons), hand the result to DamageService. Never touches Health directly,
    /// never does its own Physics call.
    /// </summary>
    [RequireComponent(typeof(CombatState))]
    [RequireComponent(typeof(Health))]
    public class CombatController : NetworkBehaviour
    {
        [SerializeField] private WeaponDefinition[] _weapons;
        [SerializeField] private NetworkObject _projectilePrefab;

        [Networked] private int WeaponIndex { get; set; }
        [Networked] private NetworkButtons PreviousButtons { get; set; }
        [Networked] public float MoveSpeedMultiplier { get; private set; } = 1f;

        private CombatState _state;
        private Health _health;

        private WeaponDefinition CurrentWeapon =>
            _weapons is { Length: > 0 } ? _weapons[WeaponIndex % _weapons.Length] : null;

        private AttackDefinition CurrentAttack => CurrentWeapon?.GetStep(_state.ComboStep);

        public override void Spawned()
        {
            _state = GetComponent<CombatState>();
            _health = GetComponent<Health>();
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority) return;
            if (!GetInput(out PlayerInputData input)) return;

            var attackPressed = input.Buttons.WasPressed(PreviousButtons, PlayerButton.Attack);
            var attackHeld = input.Buttons.IsSet(PlayerButton.Attack);
            var attackReleased = PreviousButtons.IsSet(PlayerButton.Attack) && !attackHeld;
            var switchPressed = input.Buttons.WasPressed(PreviousButtons, PlayerButton.SwitchWeapon);

            if (switchPressed && _state.IsIdle && _weapons is { Length: > 0 })
            {
                WeaponIndex = (WeaponIndex + 1) % _weapons.Length;
                GameLog.Info($"[CombatController] {name} switched to weapon {CurrentWeapon.WeaponType}.");
            }

            switch (_state.Phase)
            {
                case CombatPhase.Idle:
                    if (attackPressed) StartAttack(comboStep: 1);
                    break;

                case CombatPhase.AttackStart:
                    if (_state.PhaseTimer.ExpiredOrNotRunning(Runner)) EnterActive();
                    break;

                case CombatPhase.Charging:
                    if (attackReleased) ResolveChargeRelease();
                    break;

                case CombatPhase.Casting:
                    if (_state.PhaseTimer.ExpiredOrNotRunning(Runner)) ResolveCast();
                    break;

                case CombatPhase.AttackActive:
                    if (_state.PhaseTimer.ExpiredOrNotRunning(Runner)) EnterRecovery();
                    break;

                case CombatPhase.AttackRecovery:
                    if (attackPressed) _state.BufferedNextAttack = true;

                    if (_state.BufferedNextAttack && CurrentAttack != null && CurrentAttack.CanCombo &&
                        _state.PhaseTimer.ExpiredOrNotRunning(Runner))
                    {
                        StartAttack(_state.ComboStep + 1 > CurrentWeapon.StepCount ? 1 : _state.ComboStep + 1);
                    }
                    else if (_state.ComboWindowTimer.ExpiredOrNotRunning(Runner))
                    {
                        ReturnToIdle();
                    }
                    break;
            }

            PreviousButtons = input.Buttons;
        }

        private void StartAttack(int comboStep)
        {
            var weapon = CurrentWeapon;
            if (weapon == null) return;

            var attack = weapon.GetStep(comboStep);
            if (attack == null) return;

            _state.ComboStep = comboStep;
            _state.BufferedNextAttack = false;
            MoveSpeedMultiplier = attack.CanMoveDuringAttack ? attack.MoveSpeedMultiplier : 0f;

            // Tech review D2: driven by the attack's data, not by which weapon it is.
            if (attack.InputMode == AttackInputMode.HoldRelease)
            {
                _state.Phase = CombatPhase.Charging;
                // No PhaseTimer needed: Charging ends on button release, not on a timer.
            }
            else if (attack.InputMode == AttackInputMode.Cast)
            {
                _state.Phase = CombatPhase.Casting;
                _state.PhaseTimer = TickTimer.CreateFromSeconds(Runner, Mathf.Max(0.05f, attack.ChargeOrCastTime));
            }
            else
            {
                _state.Phase = CombatPhase.AttackStart;
                _state.PhaseTimer = TickTimer.CreateFromSeconds(Runner, Mathf.Max(0.01f, attack.StartupTime));
            }

            GameLog.Info($"[CombatController] {name} starts {weapon.WeaponType} combo step {comboStep} ({attack.AttackId}).");
        }

        private void EnterActive()
        {
            var attack = CurrentAttack;
            if (attack == null) { ReturnToIdle(); return; }

            _state.Phase = CombatPhase.AttackActive;
            _state.PhaseTimer = TickTimer.CreateFromSeconds(Runner, Mathf.Max(0.01f, attack.ActiveTime));

            ResolveMeleeHit(attack);
        }

        private void ResolveMeleeHit(AttackDefinition attack)
        {
            if (attack.HitShape == HitShapeType.Projectile)
            {
                FireProjectile(attack);
                return;
            }

            var targets = HitDetectionService.FindTargets(attack, transform, _health);
            foreach (var target in targets)
            {
                var result = DamageService.Resolve(new DamageRequest(_health, target, attack));
                if (attack.AppliesKnockback)
                {
                    ApplyKnockback(target, attack.KnockbackForce);
                }
                GameLog.Info($"[CombatController] {name} hit {target.name} for {result.FinalDamage} " +
                          $"(step {_state.ComboStep}, {attack.AttackId}).");
            }
        }

        private void ResolveChargeRelease()
        {
            var attack = CurrentAttack;
            if (attack == null) { ReturnToIdle(); return; }

            FireProjectile(attack);
            EnterRecoveryWith(attack);
        }

        private void ResolveCast()
        {
            var attack = CurrentAttack;
            if (attack == null) { ReturnToIdle(); return; }

            var castPoint = transform.position + transform.forward * attack.Range;

            if (attack.HitShape == HitShapeType.Projectile)
            {
                FireProjectile(attack);
            }
            else
            {
                var targets = HitDetectionService.FindTargetsAt(attack, castPoint, _health);
                foreach (var target in targets)
                {
                    var result = DamageService.Resolve(new DamageRequest(_health, target, attack));
                    GameLog.Info($"[CombatController] {name} staff-hit {target.name} for {result.FinalDamage} ({attack.AttackId}).");
                }
            }

            EnterRecoveryWith(attack);
        }

        private void FireProjectile(AttackDefinition attack)
        {
            if (_projectilePrefab == null)
            {
                Debug.LogWarning($"[CombatController] {name} has no projectile prefab assigned; skipping {attack.AttackId}.");
                return;
            }

            var direction = transform.forward;
            var spawnPos = transform.position + direction * 1f + Vector3.up * 0.5f;

            Runner.Spawn(_projectilePrefab, spawnPos, Quaternion.LookRotation(direction), Object.InputAuthority,
                (runner, obj) => Projectile.Initialize(runner, obj, direction, attack, _health));

            GameLog.Info($"[CombatController] {name} fired projectile ({attack.AttackId}).");
        }

        private void EnterRecovery()
        {
            EnterRecoveryWith(CurrentAttack);
        }

        private void EnterRecoveryWith(AttackDefinition attack)
        {
            if (attack == null) { ReturnToIdle(); return; }

            _state.Phase = CombatPhase.AttackRecovery;
            _state.BufferedNextAttack = false;
            _state.PhaseTimer = TickTimer.CreateFromSeconds(Runner, Mathf.Max(0.01f, attack.RecoveryTime));
            _state.ComboWindowTimer = TickTimer.CreateFromSeconds(
                Runner, Mathf.Max(0.01f, attack.RecoveryTime + attack.ComboWindow));
        }

        private void ReturnToIdle()
        {
            _state.Phase = CombatPhase.Idle;
            _state.ComboStep = 0;
            _state.BufferedNextAttack = false;
            MoveSpeedMultiplier = 1f;
        }

        private void ApplyKnockback(Health target, float force)
        {
            // Phase 0-B placeholder: instant position nudge, no physics impulse yet.
            var dir = (target.transform.position - transform.position).normalized;
            target.transform.position += dir * (force * 0.1f);
        }
    }
}
