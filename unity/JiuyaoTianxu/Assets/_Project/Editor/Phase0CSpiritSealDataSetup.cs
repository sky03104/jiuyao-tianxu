using System.Collections.Generic;

/// <summary>
/// Builds the Phase 0-C prototype Spirit Seal assets (HANDOFF-007 §2) plus the
/// registry that resolves a networked int SealId back to its asset. Since
/// Phase 0-E the values live in Assets/_Project/Config/Tables/spirit_seals.csv;
/// this entry point just runs the table importer for that table.
/// Run via: Unity.exe -batchmode -executeMethod Phase0CSpiritSealDataSetup.Run -quit
/// </summary>
public static class Phase0CSpiritSealDataSetup
{
    public static void Run()
    {
        var errors = new List<string>();
        ConfigTableImporter.ImportSpiritSeals(errors);
        ConfigTableImporter.Finish(errors, "spirit_seals.csv");
    }
}
