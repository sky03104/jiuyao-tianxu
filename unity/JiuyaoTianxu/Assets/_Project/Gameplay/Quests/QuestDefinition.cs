using UnityEngine;

namespace JiuyaoTianxu.Gameplay.Quests
{
    /// <summary>
    /// Data-driven quest (HANDOFF-008 §6). Only QuestNumId ever travels over the
    /// network; every peer resolves the rest locally through QuestRegistry — the
    /// same rule SpiritSealDefinition follows.
    /// Numbers in the Phase 0-D test assets are placeholders（可調整）.
    /// </summary>
    [CreateAssetMenu(menuName = "JiuyaoTianxu/Gameplay/Quest Definition", fileName = "QuestDefinition")]
    public class QuestDefinition : ScriptableObject
    {
        [Tooltip("Stable numeric key used on the network. Never reuse or renumber.")]
        public int QuestNumId;
        [Tooltip("Human-readable id, e.g. Q_PHASE0D_001.")]
        public string QuestId;
        public string DisplayName;
        [TextArea] public string Description;

        [Header("Objective")]
        public QuestObjectiveType ObjectiveType = QuestObjectiveType.KillTarget;
        [Tooltip("Matches EnemyIdentity.TargetId, e.g. Phase0D_TestMonster.")]
        public string TargetId;
        [Min(1)] public int RequiredCount = 1;

        [Header("Unlock")]
        [Tooltip("QuestNumId that must be Completed first. 0 = none (starts Available).")]
        public int PrerequisiteQuestNumId;

        [Header("Reward (Phase 0-D: test counter only)")]
        public QuestRewardType RewardType = QuestRewardType.None;
        public string RewardId;
        public int RewardAmount;
    }
}
