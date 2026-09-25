namespace JiuyaoTianxu.Config
{
    /// <summary>
    /// One row of monsters.csv (tech review D6: 怪物資料應可獨立管理, CLAUDE.md 核心規則 5).
    /// Monsters are prefabs rather than ScriptableObjects, so the Editor importer
    /// binds a row onto this plain class and then writes the values into the
    /// prefab whose EnemyIdentity.TargetId == MonsterId. Pure C#.
    /// Columns grow with 08_MONSTER_BIBLE needs (attack, drops …) — 可調整.
    /// </summary>
    public class MonsterTableRow
    {
        /// <summary>= EnemyIdentity.TargetId, also what quests target.</summary>
        public string MonsterId;
        public string DisplayName;
        public int MaxHp;
        /// <summary>Seconds a dead monster stays before despawning (MonsterLifecycle).</summary>
        public float DespawnDelay = 0.5f;
    }
}
