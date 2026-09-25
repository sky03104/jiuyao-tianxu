using System;
using System.Collections.Generic;

namespace JiuyaoTianxu.Combat.Targeting
{
    /// <summary>Ground-plane (x, z) vector. Pure C# so facing and lock-on rules are
    /// unit-tested outside Unity (Tools/TargetingTests); Unity code converts
    /// Vector3/Vector2 at the edges.</summary>
    public readonly struct Planar
    {
        public readonly float X;
        public readonly float Z;

        public Planar(float x, float z) { X = x; Z = z; }

        public float Length => (float)Math.Sqrt(X * X + Z * Z);
        public Planar Normalized => Length > 1e-6f ? new Planar(X / Length, Z / Length) : new Planar(0f, 0f);
        public static Planar operator -(Planar a, Planar b) => new Planar(a.X - b.X, a.Z - b.Z);
        public static float Dot(Planar a, Planar b) => a.X * b.X + a.Z * b.Z;

        /// <summary>Unsigned angle in degrees between two directions (0..180).</summary>
        public static float AngleDeg(Planar a, Planar b)
        {
            var la = a.Length; var lb = b.Length;
            if (la < 1e-6f || lb < 1e-6f) return 0f;
            var cos = Math.Max(-1f, Math.Min(1f, Dot(a, b) / (la * lb)));
            return (float)(Math.Acos(cos) * 180.0 / Math.PI);
        }
    }

    /// <summary>
    /// Which way the character should face this tick (twin-stick rules,
    /// 03 戰鬥雙搖桿): right-stick aim beats a locked target, which beats the
    /// movement direction; with none of them the character keeps its facing.
    /// </summary>
    public static class FacingLogic
    {
        public enum Source { Keep, Aim, LockTarget, Move }

        public static Source Resolve(Planar move, Planar aim, bool hasLockTarget, Planar toLockTarget,
            float deadzone, out Planar direction)
        {
            if (aim.Length > deadzone) { direction = aim.Normalized; return Source.Aim; }
            if (hasLockTarget && toLockTarget.Length > 1e-3f) { direction = toLockTarget.Normalized; return Source.LockTarget; }
            if (move.Length > deadzone) { direction = move.Normalized; return Source.Move; }
            direction = default;
            return Source.Keep;
        }
    }

    public readonly struct LockCandidate
    {
        public readonly uint Id;
        public readonly Planar Position;
        public readonly bool IsEnemy;

        public LockCandidate(uint id, Planar position, bool isEnemy)
        {
            Id = id;
            Position = position;
            IsEnemy = isEnemy;
        }
    }

    /// <summary>
    /// Lock-on target choice (目標鎖定). Candidates within <c>range</c> are ranked
    /// by: enemies before other targets (players), then distance plus an angle
    /// penalty so something slightly further but straight ahead beats something
    /// right behind you. Pressing lock again cycles to the next in that order and
    /// finally clears the lock. All values 可調整.
    /// </summary>
    public static class LockOnSelector
    {
        public static List<LockCandidate> Rank(IEnumerable<LockCandidate> candidates, Planar origin, Planar forward,
            float range, float anglePenaltyPerDeg)
        {
            var ranked = new List<(LockCandidate c, float score)>();
            foreach (var c in candidates)
            {
                var to = c.Position - origin;
                var dist = to.Length;
                if (dist > range) continue;
                var score = dist + Planar.AngleDeg(forward, to) * anglePenaltyPerDeg;
                ranked.Add((c, score));
            }

            ranked.Sort((a, b) =>
            {
                if (a.c.IsEnemy != b.c.IsEnemy) return a.c.IsEnemy ? -1 : 1;
                var byScore = a.score.CompareTo(b.score);
                return byScore != 0 ? byScore : a.c.Id.CompareTo(b.c.Id); // deterministic ties
            });

            var result = new List<LockCandidate>(ranked.Count);
            foreach (var r in ranked) result.Add(r.c);
            return result;
        }

        /// <summary>Next lock after <paramref name="currentId"/> (0 = none):
        /// none → best; current → the one after it; last → cleared (0);
        /// current no longer a candidate → best.</summary>
        public static uint Next(IReadOnlyList<LockCandidate> ranked, uint currentId)
        {
            if (ranked.Count == 0) return 0;
            if (currentId == 0) return ranked[0].Id;
            for (var i = 0; i < ranked.Count; i++)
            {
                if (ranked[i].Id != currentId) continue;
                return i + 1 < ranked.Count ? ranked[i + 1].Id : 0;
            }
            return ranked[0].Id;
        }

        /// <summary>Whether an existing lock should be dropped automatically.</summary>
        public static bool ShouldBreak(bool targetAlive, Planar origin, Planar target, float breakRange) =>
            !targetAlive || (target - origin).Length > breakRange;
    }
}
