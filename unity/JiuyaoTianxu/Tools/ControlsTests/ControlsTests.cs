// Unity-free unit tests for twin-stick controls: FacingLogic / LockOnSelector (Combat/Targeting/TargetingMath.cs) and StickMath (Core/StickMath.cs).
using System;
using System.Collections.Generic;
using JiuyaoTianxu.Combat.Targeting;
using JiuyaoTianxu.Core;

public static class ControlsTests
{
    private static int _passed, _failed;

    private static void Check(bool ok, string name)
    {
        if (ok) _passed++;
        else { _failed++; Console.WriteLine("FAIL: " + name); }
    }

    private static bool Near(Planar p, float x, float z) => Math.Abs(p.X - x) < 1e-4 && Math.Abs(p.Z - z) < 1e-4;

    public static int Main()
    {
        Facing();
        Ranking();
        Cycling();
        Breaking();
        Stick();
        Console.WriteLine($"ControlsTests: {_passed} passed, {_failed} failed");
        return _failed == 0 ? 0 : 1;
    }

    private static void Facing()
    {
        var zero = new Planar(0, 0);
        const float dz = 0.2f;
        Check(FacingLogic.Resolve(zero, zero, false, zero, dz, out _) == FacingLogic.Source.Keep, "no input keeps facing");
        Check(FacingLogic.Resolve(new Planar(0.1f, 0), new Planar(0, 0.15f), false, zero, dz, out _) == FacingLogic.Source.Keep,
            "inputs inside deadzone ignored");
        Check(FacingLogic.Resolve(new Planar(3, 4), zero, false, zero, dz, out var d1) == FacingLogic.Source.Move && Near(d1, 0.6f, 0.8f),
            "move direction normalized");
        Check(FacingLogic.Resolve(new Planar(1, 0), zero, true, new Planar(0, -5), dz, out var d2) == FacingLogic.Source.LockTarget && Near(d2, 0, -1),
            "lock target beats movement (strafe while locked)");
        Check(FacingLogic.Resolve(new Planar(1, 0), new Planar(-1, 0), true, new Planar(0, -5), dz, out var d3) == FacingLogic.Source.Aim && Near(d3, -1, 0),
            "aim stick beats lock target");
        Check(FacingLogic.Resolve(new Planar(1, 0), zero, true, zero, dz, out _) == FacingLogic.Source.Move,
            "lock target on top of you falls back to movement");
    }

    private static readonly Planar Origin = new Planar(0, 0);
    private static readonly Planar Forward = new Planar(1, 0);

    private static void Ranking()
    {
        var c = new List<LockCandidate>
        {
            new LockCandidate(1, new Planar(-2, 0), true),   // enemy right behind, 2m
            new LockCandidate(2, new Planar(4, 0), true),    // enemy straight ahead, 4m
            new LockCandidate(3, new Planar(1, 0), false),   // player 1m ahead
            new LockCandidate(4, new Planar(20, 0), true),   // out of range
        };
        var ranked = LockOnSelector.Rank(c, Origin, Forward, 12f, 0.03f);
        Check(ranked.Count == 3, "out-of-range candidate excluded");
        Check(ranked[0].Id == 2, "ahead-but-further enemy beats behind-but-closer enemy (angle penalty)");
        Check(ranked[1].Id == 1, "other enemy next");
        Check(ranked[2].Id == 3, "players ranked after enemies even if closest");

        var tie = LockOnSelector.Rank(new[] { new LockCandidate(9, new Planar(2, 0), true), new LockCandidate(5, new Planar(2, 0), true) },
            Origin, Forward, 12f, 0.03f);
        Check(tie[0].Id == 5, "exact ties broken by id (deterministic on server)");
        Check(LockOnSelector.Rank(new List<LockCandidate>(), Origin, Forward, 12f, 0.03f).Count == 0, "no candidates → empty");
    }

    private static void Cycling()
    {
        var ranked = new List<LockCandidate>
        {
            new LockCandidate(7, new Planar(1, 0), true),
            new LockCandidate(8, new Planar(2, 0), true),
        };
        Check(LockOnSelector.Next(ranked, 0) == 7, "none → best");
        Check(LockOnSelector.Next(ranked, 7) == 8, "best → next");
        Check(LockOnSelector.Next(ranked, 8) == 0, "last → cleared");
        Check(LockOnSelector.Next(ranked, 99) == 7, "stale lock → best");
        Check(LockOnSelector.Next(new List<LockCandidate>(), 7) == 0, "nothing in range → cleared");
    }

    private static void Breaking()
    {
        Check(LockOnSelector.ShouldBreak(false, Origin, new Planar(1, 0), 16f), "dead target breaks lock");
        Check(LockOnSelector.ShouldBreak(true, Origin, new Planar(17, 0), 16f), "beyond break range breaks lock");
        Check(!LockOnSelector.ShouldBreak(true, Origin, new Planar(13, 0), 16f), "between lock and break range keeps lock (no flicker)");
    }

    private static void Stick()
    {
        StickMath.Evaluate(5, 0, 100, 0.1f, out var x0, out var y0);
        Check(x0 == 0 && y0 == 0, "inside deadzone → 0");
        StickMath.Evaluate(100, 0, 100, 0.1f, out var x1, out _);
        Check(Math.Abs(x1 - 1f) < 1e-5, "at rim → 1");
        StickMath.Evaluate(0, -300, 100, 0.1f, out var x2, out var y2);
        Check(Math.Abs(y2 + 1f) < 1e-5 && Math.Abs(x2) < 1e-6, "beyond rim clamped to 1, direction kept");
        StickMath.Evaluate(55, 0, 100, 0.1f, out var x3, out _);
        Check(Math.Abs(x3 - 0.5f) < 1e-5, "linear rescale between deadzone and rim");
        StickMath.Evaluate(11, 0, 100, 0.1f, out var x4, out _);
        Check(x4 > 0 && x4 < 0.02f, "no jump just outside the deadzone");
        StickMath.Evaluate(60, 80, 100, 0f, out var x5, out var y5);
        Check(Math.Abs(Math.Sqrt(x5 * x5 + y5 * y5) - 1) < 1e-5, "diagonal magnitude never exceeds 1");
        StickMath.Evaluate(50, 0, 0, 0.1f, out var x6, out _);
        Check(x6 == 0, "zero radius is safe");
    }
}
