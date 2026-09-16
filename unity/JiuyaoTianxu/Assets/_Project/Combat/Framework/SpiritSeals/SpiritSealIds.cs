namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Stable Phase 0-C prototype seal ids, shared between the runtime
    /// (NetworkGameLauncher's test auto-equip) and the Editor-only
    /// Phase0CSpiritSealDataSetup that creates the matching assets. Must live
    /// outside Assets/_Project/Editor/ — Editor scripts are stripped from
    /// player builds, so runtime code can never reference a class defined there.
    /// </summary>
    public static class SpiritSealIds
    {
        public const int Blaze = 1;  // 赤炎
        public const int Guard = 2;  // 玄甲
        public const int Shadow = 3; // 影遁
    }
}
