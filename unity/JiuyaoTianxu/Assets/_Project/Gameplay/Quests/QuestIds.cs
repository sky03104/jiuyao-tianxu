namespace JiuyaoTianxu.Gameplay.Quests
{
    /// <summary>
    /// Stable Phase 0-D test quest ids, shared by the Editor setup script that
    /// builds the assets and anything at runtime that needs to name one (logs,
    /// tests). Runtime quest LOGIC never branches on these — see QuestTracker.
    /// Lives outside Editor/ because Editor code is stripped from player builds.
    /// </summary>
    public static class QuestIds
    {
        public const int Phase0DClearTestArea = 1001;       // Q_PHASE0D_001 清理測試區
        public const int Phase0DClearTestAreaFollowUp = 1002; // Q_PHASE0D_002 (Locked until 001 done)

        public const string Phase0DTestMonsterTargetId = "Phase0D_TestMonster";
    }
}
