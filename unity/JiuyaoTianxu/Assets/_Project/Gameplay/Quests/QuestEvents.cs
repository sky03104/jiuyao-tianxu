using System;
using Fusion;
using UnityEngine;

namespace JiuyaoTianxu.Gameplay.Quests
{
    public readonly struct QuestChangedEvent
    {
        public readonly NetworkRunner Runner;
        public readonly PlayerRef Player;
        public readonly int QuestNumId;
        public readonly QuestState OldState;
        public readonly QuestState NewState;
        public readonly int Progress;
        public readonly int RequiredCount;

        public QuestChangedEvent(NetworkRunner runner, PlayerRef player, int questNumId, QuestState oldState,
            QuestState newState, int progress, int requiredCount)
        {
            Runner = runner;
            Player = player;
            QuestNumId = questNumId;
            OldState = oldState;
            NewState = newState;
            Progress = progress;
            RequiredCount = requiredCount;
        }
    }

    /// <summary>
    /// Server-side notifications FROM the quest system (state/progress changed).
    /// Observers only — Phase0DTestRunner counts these; a future quest-tracker UI
    /// would read the replicated PlayerQuestLog instead.
    /// </summary>
    public static class QuestEvents
    {
        public static event Action<QuestChangedEvent> QuestChanged;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics() => QuestChanged = null;

        internal static void Raise(in QuestChangedEvent evt)
        {
            var handlers = QuestChanged;
            if (handlers == null) return;
            foreach (var handler in handlers.GetInvocationList())
            {
                try
                {
                    ((Action<QuestChangedEvent>)handler)(evt);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}
