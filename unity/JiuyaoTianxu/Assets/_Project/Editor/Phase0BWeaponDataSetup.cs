using System.Collections.Generic;

/// <summary>
/// Builds the six WeaponDefinition/AttackDefinition data assets for Phase 0-B
/// (HANDOFF-006 §4). Since Phase 0-E the numbers live in
/// Assets/_Project/Config/Tables/attacks.csv + weapons.csv (初版示意值，可調整);
/// this entry point is kept so Phase0ANetworkSetup and old command lines still
/// work, and simply runs the table importer for those two tables.
/// Run via: Unity.exe -batchmode -executeMethod Phase0BWeaponDataSetup.Run -quit
/// </summary>
public static class Phase0BWeaponDataSetup
{
    public static void Run()
    {
        var errors = new List<string>();
        ConfigTableImporter.ImportAttacksAndWeapons(errors);
        ConfigTableImporter.Finish(errors, "attacks.csv + weapons.csv");
    }
}
