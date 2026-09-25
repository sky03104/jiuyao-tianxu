using Fusion;

namespace JiuyaoTianxu.Gameplay.Quests
{
    /// <summary>One quest's networked state: three ints, never a ScriptableObject.</summary>
    public struct QuestEntry : INetworkStruct
    {
        public int QuestNumId;
        public int State;    // (int)QuestState
        public int Progress;

        public QuestState QuestState => (QuestState)State;
    }

    /// <summary>
    /// Per-player networked quest state (HANDOFF-008 §10). Pure data holder, like
    /// SpiritSealLoadout: QuestTracker (server) is the only writer, every client
    /// just reads the replicated values and resolves display data locally via
    /// QuestRegistry.
    /// </summary>
    public class PlayerQuestLog : NetworkBehaviour
    {
        public const int Capacity = 8;

        [Networked, Capacity(Capacity)]
        public NetworkArray<QuestEntry> Entries => default;

        /// <summary>Phase 0-D test reward sink (QuestRewardType.DebugCounter).</summary>
        [Networked] public int DebugRewardPoints { get; set; }

        public int FindSlot(int questNumId)
        {
            for (var i = 0; i < Capacity; i++)
            {
                if (questNumId > 0 && Entries[i].QuestNumId == questNumId) return i;
            }
            return -1;
        }
    }
}
