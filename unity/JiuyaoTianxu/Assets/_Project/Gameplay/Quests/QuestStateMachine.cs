namespace JiuyaoTianxu.Gameplay.Quests
{
    /// <summary>
    /// The whole quest state machine as a transition table (HANDOFF-008 §7).
    /// Pure C# — no Unity, no Fusion — so it is unit-tested outside Unity
    /// (unity/JiuyaoTianxu/Tools/QuestLogicTests). Nothing in here knows any
    /// QuestId: every quest runs through the same table, and quest-specific
    /// behaviour comes only from QuestDefinition data.
    /// </summary>
    public static class QuestStateMachine
    {
        public static QuestState InitialState(bool hasPrerequisite) =>
            hasPrerequisite ? QuestState.Locked : QuestState.Available;

        public static bool TryTransition(QuestState from, QuestTrigger trigger, out QuestState to)
        {
            to = (from, trigger) switch
            {
                (QuestState.Locked, QuestTrigger.PrerequisitesMet) => QuestState.Available,
                (QuestState.Available, QuestTrigger.Accept) => QuestState.Accepted,
                (QuestState.Accepted, QuestTrigger.BeginTracking) => QuestState.InProgress,
                (QuestState.InProgress, QuestTrigger.ObjectiveReached) => QuestState.Completed,
                _ => from,
            };
            return to != from;
        }
    }

    /// <summary>Objective progress rules, also pure and unit-tested.</summary>
    public static class QuestObjectiveLogic
    {
        /// <summary>
        /// Applies one "enemy with targetId died" fact to one quest entry. Returns
        /// true if progress advanced. Only InProgress quests count, only matching
        /// target ids count (ordinal compare, ids are data keys not display text),
        /// and progress never exceeds RequiredCount.
        /// </summary>
        public static bool TryApplyKill(QuestObjectiveType objectiveType, QuestState state, int progress,
            int requiredCount, string questTargetId, string killedTargetId, out int newProgress)
        {
            newProgress = progress;
            if (objectiveType != QuestObjectiveType.KillTarget) return false;
            if (state != QuestState.InProgress) return false;
            if (string.IsNullOrEmpty(questTargetId) || questTargetId != killedTargetId) return false;
            if (progress >= requiredCount) return false;

            newProgress = progress + 1;
            return true;
        }

        public static bool IsObjectiveMet(int progress, int requiredCount) => progress >= requiredCount;
    }
}
