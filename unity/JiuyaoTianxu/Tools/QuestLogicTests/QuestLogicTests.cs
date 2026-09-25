// Unity-free unit tests for the Phase 0-D quest state machine / objective logic.
// Compiles QuestState.cs + QuestStateMachine.cs straight from Assets/ (they have
// no UnityEngine/Fusion dependency by design). Run: ./run.sh (mono) or run.ps1.
using System;
using System.Collections.Generic;
using JiuyaoTianxu.Gameplay.Quests;

public static class QuestLogicTests
{
    private static int _passed, _failed;

    private static void Check(bool condition, string name)
    {
        if (condition) { _passed++; }
        else { _failed++; Console.WriteLine("FAIL: " + name); }
    }

    public static int Main()
    {
        InitialStates();
        LegalTransitions();
        EveryIllegalTransitionIsRejected();
        KillOnlyCountsWhenInProgress();
        KillOnlyCountsMatchingTarget();
        ProgressNeverExceedsRequired();
        FullThreeKillCycle();
        PrerequisiteChain();

        Console.WriteLine($"QuestLogicTests: {_passed} passed, {_failed} failed");
        return _failed == 0 ? 0 : 1;
    }

    private static void InitialStates()
    {
        Check(QuestStateMachine.InitialState(false) == QuestState.Available, "no prerequisite → Available");
        Check(QuestStateMachine.InitialState(true) == QuestState.Locked, "prerequisite → Locked");
    }

    private static readonly (QuestState from, QuestTrigger trigger, QuestState to)[] Legal =
    {
        (QuestState.Locked, QuestTrigger.PrerequisitesMet, QuestState.Available),
        (QuestState.Available, QuestTrigger.Accept, QuestState.Accepted),
        (QuestState.Accepted, QuestTrigger.BeginTracking, QuestState.InProgress),
        (QuestState.InProgress, QuestTrigger.ObjectiveReached, QuestState.Completed),
    };

    private static void LegalTransitions()
    {
        foreach (var (from, trigger, to) in Legal)
        {
            var ok = QuestStateMachine.TryTransition(from, trigger, out var next);
            Check(ok && next == to, $"{from} --{trigger}--> {to}");
        }
    }

    private static void EveryIllegalTransitionIsRejected()
    {
        var legal = new HashSet<(QuestState, QuestTrigger)>();
        foreach (var (from, trigger, _) in Legal) legal.Add((from, trigger));

        foreach (QuestState from in Enum.GetValues(typeof(QuestState)))
        foreach (QuestTrigger trigger in Enum.GetValues(typeof(QuestTrigger)))
        {
            if (legal.Contains((from, trigger))) continue;
            var ok = QuestStateMachine.TryTransition(from, trigger, out var next);
            Check(!ok && next == from, $"illegal {from} --{trigger}--> rejected and state unchanged");
        }
    }

    private const string Monster = "Phase0D_TestMonster";

    private static void KillOnlyCountsWhenInProgress()
    {
        foreach (QuestState state in Enum.GetValues(typeof(QuestState)))
        {
            var ok = QuestObjectiveLogic.TryApplyKill(QuestObjectiveType.KillTarget, state, 0, 3, Monster, Monster,
                out var p);
            Check(ok == (state == QuestState.InProgress), $"kill counts only in InProgress (state={state})");
            Check(ok ? p == 1 : p == 0, $"progress value correct (state={state})");
        }
    }

    private static void KillOnlyCountsMatchingTarget()
    {
        Check(!QuestObjectiveLogic.TryApplyKill(QuestObjectiveType.KillTarget, QuestState.InProgress, 0, 3, Monster,
            "OtherMonster", out _), "different target id ignored");
        Check(!QuestObjectiveLogic.TryApplyKill(QuestObjectiveType.KillTarget, QuestState.InProgress, 0, 3, Monster,
            "phase0d_testmonster", out _), "target id compare is case-sensitive (data key)");
        Check(!QuestObjectiveLogic.TryApplyKill(QuestObjectiveType.KillTarget, QuestState.InProgress, 0, 3, "",
            "", out _), "empty quest target never matches");
    }

    private static void ProgressNeverExceedsRequired()
    {
        Check(!QuestObjectiveLogic.TryApplyKill(QuestObjectiveType.KillTarget, QuestState.InProgress, 3, 3, Monster,
            Monster, out var p) && p == 3, "no progress past RequiredCount");
    }

    private static void FullThreeKillCycle()
    {
        // Mirrors QuestTracker's server flow for Q_PHASE0D_001 (kill ×3).
        var state = QuestStateMachine.InitialState(false);
        var progress = 0;
        const int required = 3;

        Check(QuestStateMachine.TryTransition(state, QuestTrigger.Accept, out state), "accept");
        Check(QuestStateMachine.TryTransition(state, QuestTrigger.BeginTracking, out state), "begin tracking");

        var log = new List<string>();
        for (var kill = 0; kill < 5; kill++) // two extra kills after completion must not count
        {
            if (QuestObjectiveLogic.TryApplyKill(QuestObjectiveType.KillTarget, state, progress, required, Monster,
                    Monster, out progress))
            {
                log.Add($"{progress}/{required}");
                if (QuestObjectiveLogic.IsObjectiveMet(progress, required))
                {
                    QuestStateMachine.TryTransition(state, QuestTrigger.ObjectiveReached, out state);
                }
            }
        }

        Check(string.Join(",", log) == "1/3,2/3,3/3", "progress sequence 1/3,2/3,3/3 (got " + string.Join(",", log) + ")");
        Check(state == QuestState.Completed, "ends Completed");
        Check(progress == 3, "extra kills after completion ignored");
    }

    private static void PrerequisiteChain()
    {
        var q2 = QuestStateMachine.InitialState(true);
        Check(!QuestStateMachine.TryTransition(q2, QuestTrigger.Accept, out _), "locked quest cannot be accepted");
        Check(QuestStateMachine.TryTransition(q2, QuestTrigger.PrerequisitesMet, out q2) && q2 == QuestState.Available,
            "unlock after prerequisite completes");
        Check(!QuestStateMachine.TryTransition(q2, QuestTrigger.PrerequisitesMet, out _), "unlock is not repeatable");
        Check(QuestStateMachine.TryTransition(q2, QuestTrigger.Accept, out q2) && q2 == QuestState.Accepted,
            "unlocked quest can be accepted");
        Check(!QuestStateMachine.TryTransition(QuestState.Completed, QuestTrigger.Accept, out _),
            "completed quest cannot be re-accepted (no repeat/reset in Phase 0-D)");
    }
}
