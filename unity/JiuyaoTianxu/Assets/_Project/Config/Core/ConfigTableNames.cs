namespace JiuyaoTianxu.Config
{
    /// <summary>
    /// The design tables and their key columns. The same file names are used by
    /// the Editor importer (Assets/_Project/Config/Tables/) and by the server's
    /// runtime override folder (see ConfigOverrideLoader).
    /// </summary>
    public static class ConfigTableNames
    {
        public const string Attacks = "attacks.csv";         // key: AttackId
        public const string Weapons = "weapons.csv";         // key: WeaponType
        public const string SpiritSeals = "spirit_seals.csv"; // key: SealId
        public const string Quests = "quests.csv";           // key: QuestNumId
        public const string Monsters = "monsters.csv";       // key: MonsterId (Editor import only)

        public const string AttackKey = "AttackId";
        public const string WeaponKey = "WeaponType";
        public const string SpiritSealKey = "SealId";
        public const string QuestKey = "QuestNumId";
        public const string MonsterKey = "MonsterId";

        /// <summary>weapons.csv: '|'-separated AttackIds, resolved by the importer
        /// (the binder skips it — it's a list of asset references, not a value).</summary>
        public const string WeaponComboColumn = "ComboSequence";
    }
}
