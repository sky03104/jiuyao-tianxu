namespace JiuyaoTianxu.Gameplay.Quests
{
    /// <summary>HANDOFF-008 §7. Values are networked as int — never renumber.
    /// Failed/Reset/Branch are deliberately not here yet (§14).</summary>
    public enum QuestState
    {
        Locked = 0,
        Available = 1,
        Accepted = 2,
        InProgress = 3,
        Completed = 4,
    }

    /// <summary>What can happen to a quest. The state machine decides which of
    /// these are legal from which state.</summary>
    public enum QuestTrigger
    {
        PrerequisitesMet = 0, // Locked     → Available
        Accept = 1,           // Available  → Accepted
        BeginTracking = 2,    // Accepted   → InProgress
        ObjectiveReached = 3, // InProgress → Completed
    }

    public enum QuestObjectiveType
    {
        KillTarget = 0,
    }

    public enum QuestRewardType
    {
        None = 0,
        /// <summary>Phase 0-D test reward: bumps a networked debug counter.
        /// No Inventory/Economy yet (HANDOFF-008 §9/§14).</summary>
        DebugCounter = 1,
    }
}
