using System.Collections.Generic;
using System.Linq;
using JiuyaoTianxu.Core;
using JiuyaoTianxu.Gameplay.Events;
using JiuyaoTianxu.Gameplay.Quests;
using UnityEngine;

namespace JiuyaoTianxu.Gameplay.Testing
{
    /// <summary>
    /// HANDOFF-008 §12 observer. Pure listener: counts EnemyKilled / quest events
    /// on the server and prints a machine-greppable summary + one PASS line when
    /// the §15 quest criteria are met. It drives nothing — the actual run is
    /// driven by -autotest input (attacks) and QuestTracker's auto-accept.
    ///
    /// PASS = at least <see cref="_requiredPlayers"/> distinct players completed
    /// Q_PHASE0D_001 (full Accept → 1/3 → 2/3 → 3/3 → Completed cycle), and at
    /// least <see cref="_requiredProgressEvents"/> progress updates in total.
    /// Only a server log line counts as network evidence; client logs prove
    /// replication separately via [QuestSync] lines.
    /// </summary>
    public class Phase0DTestRunner : MonoBehaviour
    {
        [SerializeField] private int _requiredPlayers = 2;
        [SerializeField] private int _requiredProgressEvents = 10;
        [SerializeField] private float _summaryIntervalSeconds = 10f;

        private int _enemyKilled;
        private int _progressEvents;
        private int _accepts;
        private int _unlocks;
        private readonly Dictionary<int, HashSet<int>> _completedByQuest = new();
        private float _nextSummaryTime;
        private float _quitAt;
        private bool _passed;

        private void OnEnable()
        {
            GameplayEvents.EnemyKilled += OnEnemyKilled;
            QuestEvents.QuestChanged += OnQuestChanged;

            var quitAfter = CommandLineFlags.QuitAfterSeconds;
            _quitAt = quitAfter > 0f ? Time.unscaledTime + quitAfter : 0f;
            _nextSummaryTime = Time.unscaledTime + _summaryIntervalSeconds;
        }

        private void OnDisable()
        {
            GameplayEvents.EnemyKilled -= OnEnemyKilled;
            QuestEvents.QuestChanged -= OnQuestChanged;
        }

        private void OnEnemyKilled(EnemyKilledEvent evt)
        {
            _enemyKilled++;
            GameLog.Info($"[Phase0DTestRunner] EnemyKilled #{_enemyKilled}: {evt.TargetId} " +
                      $"by {(evt.Killer != null ? evt.Killer.name : "<none>")}");
        }

        private void OnQuestChanged(QuestChangedEvent evt)
        {
            if (evt.OldState == evt.NewState)
            {
                _progressEvents++;
            }
            else if (evt.NewState == QuestState.Accepted)
            {
                _accepts++;
            }
            else if (evt.OldState == QuestState.Locked && evt.NewState == QuestState.Available)
            {
                _unlocks++;
            }
            else if (evt.NewState == QuestState.Completed)
            {
                if (!_completedByQuest.TryGetValue(evt.QuestNumId, out var players))
                {
                    _completedByQuest[evt.QuestNumId] = players = new HashSet<int>();
                }
                players.Add(evt.Player.RawEncoded);
            }

            CheckPass();
        }

        private void CheckPass()
        {
            if (_passed) return;

            _completedByQuest.TryGetValue(QuestIds.Phase0DClearTestArea, out var done);
            var playersDone = done?.Count ?? 0;
            if (playersDone < _requiredPlayers || _progressEvents < _requiredProgressEvents) return;

            _passed = true;
            GameLog.Info($"[Phase0DTestRunner] PASS: {playersDone} player(s) completed Q_PHASE0D_001, " +
                      $"{_progressEvents} progress events, {_enemyKilled} EnemyKilled events.");
        }

        private void Update()
        {
            if (Time.unscaledTime >= _nextSummaryTime)
            {
                _nextSummaryTime = Time.unscaledTime + _summaryIntervalSeconds;
                LogSummary();
            }

            if (_quitAt > 0f && Time.unscaledTime >= _quitAt)
            {
                _quitAt = 0f;
                LogSummary();
                GameLog.Info("[Phase0DTestRunner] -quitafter reached; quitting.");
                Application.Quit();
            }
        }

        private void LogSummary()
        {
            var completed = string.Join(", ",
                _completedByQuest.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key}:{kv.Value.Count}p"));
            GameLog.Info($"[Phase0DTestRunner] SUMMARY enemyKilled={_enemyKilled} accepts={_accepts} " +
                      $"progress={_progressEvents} unlocks={_unlocks} completed=[{completed}] passed={_passed}");
        }
    }
}
