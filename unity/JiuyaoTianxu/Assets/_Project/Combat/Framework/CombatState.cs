using Fusion;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Pure networked state holder (HANDOFF-006 §6/§7) — CombatController is the
    /// only thing that writes to this. Kept as its own component (not fields
    /// inside CombatController) so future systems (animation, UI combo counter)
    /// can read combat state without depending on CombatController's logic.
    /// </summary>
    public class CombatState : NetworkBehaviour
    {
        [Networked] public CombatPhase Phase { get; set; }
        [Networked] public int ComboStep { get; set; }
        [Networked] public TickTimer PhaseTimer { get; set; }
        [Networked] public TickTimer ComboWindowTimer { get; set; }
        [Networked] public NetworkBool BufferedNextAttack { get; set; }

        public override void Spawned()
        {
            Phase = CombatPhase.Idle;
            ComboStep = 0;
        }

        public bool IsIdle => Phase == CombatPhase.Idle;
    }
}
