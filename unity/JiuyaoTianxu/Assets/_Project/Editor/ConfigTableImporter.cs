using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Config;
using JiuyaoTianxu.Gameplay.Quests;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Design tables (Assets/_Project/Config/Tables/*.csv) → ScriptableObject assets.
/// The CSVs are the source of truth for every Phase 0 number; the assets are
/// generated from them. Existing assets are found by their key (AttackId /
/// WeaponType / SealId / QuestNumId) and updated IN PLACE, so GUIDs — and every
/// prefab/registry reference to them — survive re-imports and renames.
///
/// Menu: JiuyaoTianxu/Config/Import All Tables
/// Batch: Unity.exe -batchmode -projectPath . -executeMethod ConfigTableImporter.ImportAll -quit
/// (batch mode throws on any table error so the process exits non-zero).
/// </summary>
public static class ConfigTableImporter
{
    private const string TablesDir = "Assets/_Project/Config/Tables";
    private const string WeaponsRoot = "Assets/_Project/Combat/Weapons";
    private const string SealsRoot = "Assets/_Project/Combat/SpiritSeals";
    private const string SealRegistryPath = SealsRoot + "/SpiritSealRegistry.asset";
    private const string QuestDataRoot = "Assets/_Project/Gameplay/Quests/Data";
    private const string QuestRegistryPath = QuestDataRoot + "/QuestRegistry.asset";

    [MenuItem("JiuyaoTianxu/Config/Import All Tables")]
    public static void ImportAll()
    {
        var errors = new List<string>();
        ImportAttacksAndWeapons(errors);
        ImportSpiritSeals(errors);
        ImportQuests(errors);
        ImportMonsters(errors);
        Finish(errors, "all tables");
    }

    // ---------------- Per-table entry points (also used by Phase0B/C/D setup) ----------------

    public static void ImportAttacksAndWeapons(List<string> errors)
    {
        var attacks = ImportTable<AttackDefinition>(ConfigTableNames.Attacks, ConfigTableNames.AttackKey,
            a => a.AttackId,
            row => $"{WeaponsRoot}/{row.Get("WeaponType")}/{row.Get(ConfigTableNames.AttackKey)}.asset",
            null, errors);
        var attackById = attacks.ToDictionary(a => a.AttackId);

        var weaponTable = Load(ConfigTableNames.Weapons, errors);
        if (weaponTable == null) return;

        var weapons = ImportRows<WeaponDefinition>(weaponTable, ConfigTableNames.WeaponKey,
            w => w.WeaponType.ToString(),
            row => $"{WeaponsRoot}/{row.Get(ConfigTableNames.WeaponKey)}/{row.Get(ConfigTableNames.WeaponKey)}_Weapon.asset",
            new[] { ConfigTableNames.WeaponComboColumn }, errors);

        for (var i = 0; i < weapons.Count; i++)
        {
            var row = weaponTable.Rows.First(r => r.Get(ConfigTableNames.WeaponKey) == weapons[i].WeaponType.ToString());
            var ids = (row.Get(ConfigTableNames.WeaponComboColumn) ?? "")
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
            var steps = new List<AttackDefinition>();
            foreach (var id in ids)
            {
                if (attackById.TryGetValue(id, out var attack)) steps.Add(attack);
                else errors.Add($"{row.Where(ConfigTableNames.WeaponComboColumn)}: unknown AttackId '{id}'.");
            }
            if (steps.Count == 0) errors.Add($"{row.Where(ConfigTableNames.WeaponComboColumn)}: weapon has no attacks.");
            weapons[i].ComboSequence = steps.ToArray();
            EditorUtility.SetDirty(weapons[i]);
        }
    }

    public static void ImportSpiritSeals(List<string> errors)
    {
        var seals = ImportTable<SpiritSealDefinition>(ConfigTableNames.SpiritSeals, ConfigTableNames.SpiritSealKey,
            s => s.SealId.ToString(), row => $"{SealsRoot}/{row.Get("DisplayName")}.asset", null, errors);

        var registry = LoadOrCreate<SpiritSealRegistry>(SealRegistryPath);
        registry.All = seals.ToArray();
        EditorUtility.SetDirty(registry);
    }

    public static void ImportQuests(List<string> errors)
    {
        var quests = ImportTable<QuestDefinition>(ConfigTableNames.Quests, ConfigTableNames.QuestKey,
            q => q.QuestNumId.ToString(), row => $"{QuestDataRoot}/{row.Get("QuestId")}.asset", null, errors);

        if (quests.Count > PlayerQuestLog.Capacity)
            errors.Add($"{ConfigTableNames.Quests}: {quests.Count} quests but PlayerQuestLog holds {PlayerQuestLog.Capacity}.");
        foreach (var q in quests.Where(q => q.PrerequisiteQuestNumId != 0))
        {
            if (quests.All(o => o.QuestNumId != q.PrerequisiteQuestNumId))
                errors.Add($"{ConfigTableNames.Quests}: {q.QuestId} prerequisite {q.PrerequisiteQuestNumId} is not in the table.");
        }

        var registry = LoadOrCreate<QuestRegistry>(QuestRegistryPath);
        registry.All = quests.ToArray();
        EditorUtility.SetDirty(registry);
    }

    /// <summary>
    /// monsters.csv → monster prefabs. Rows bind onto MonsterTableRow (same binder
    /// rules as every other table), then the values are written into the prefab
    /// under Assets/_Project whose EnemyIdentity.TargetId matches MonsterId. A row
    /// with no prefab yet is only a warning (Phase0DSetup builds the test monster).
    /// </summary>
    public static void ImportMonsters(List<string> errors)
    {
        var table = Load(ConfigTableNames.Monsters, errors);
        if (table == null) return;
        if (!table.HasColumn(ConfigTableNames.MonsterKey))
        {
            errors.Add($"{table.Name}: missing key column '{ConfigTableNames.MonsterKey}'.");
            return;
        }

        var prefabs = new Dictionary<string, string>();
        foreach (var guid in AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/_Project" }))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var identity = go != null ? go.GetComponent<JiuyaoTianxu.Gameplay.World.EnemyIdentity>() : null;
            if (identity != null && !prefabs.ContainsKey(identity.TargetId)) prefabs[identity.TargetId] = path;
        }

        var seen = new HashSet<string>();
        foreach (var row in table.Rows)
        {
            var data = new MonsterTableRow();
            errors.AddRange(TableBinder.Bind(data, row));
            if (string.IsNullOrEmpty(data.MonsterId)) { errors.Add($"{row.Where(ConfigTableNames.MonsterKey)}: empty key."); continue; }
            if (!seen.Add(data.MonsterId)) { errors.Add($"{row.Where(ConfigTableNames.MonsterKey)}: duplicate key '{data.MonsterId}'."); continue; }
            if (data.MaxHp <= 0) errors.Add($"{row.Where("MaxHp")}: must be > 0.");

            if (!prefabs.TryGetValue(data.MonsterId, out var prefabPath))
            {
                Debug.LogWarning($"[ConfigTableImporter] monsters.csv: no prefab with TargetId '{data.MonsterId}' yet; skipped.");
                continue;
            }

            var root = PrefabUtility.LoadPrefabContents(prefabPath);
            var healthComponent = root.GetComponent<JiuyaoTianxu.Combat.Health>();
            if (healthComponent == null)
            {
                errors.Add($"{row.Where(ConfigTableNames.MonsterKey)}: prefab {prefabPath} has no Health.");
                PrefabUtility.UnloadPrefabContents(root);
                continue;
            }
            var health = new SerializedObject(healthComponent);
            health.FindProperty("_maxHp").intValue = data.MaxHp;
            health.ApplyModifiedPropertiesWithoutUndo();
            var lifecycle = root.GetComponent<JiuyaoTianxu.Gameplay.World.MonsterLifecycle>();
            if (lifecycle != null)
            {
                var so = new SerializedObject(lifecycle);
                so.FindProperty("_despawnDelay").floatValue = data.DespawnDelay;
                so.ApplyModifiedPropertiesWithoutUndo();
            }
            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            PrefabUtility.UnloadPrefabContents(root);
        }
    }

    /// <summary>Saves and reports. In batch mode any error throws, so a CI /
    /// command-line import can't silently leave half-imported data behind.</summary>
    public static void Finish(List<string> errors, string what)
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        foreach (var e in errors) Debug.LogError($"[ConfigTableImporter] {e}");
        if (errors.Count == 0)
        {
            Debug.Log($"[ConfigTableImporter] Imported {what}: OK.");
            return;
        }
        if (Application.isBatchMode)
            throw new InvalidOperationException($"[ConfigTableImporter] {errors.Count} error(s) importing {what}.");
    }

    // ---------------- Generic machinery ----------------

    private static List<T> ImportTable<T>(string file, string keyColumn, Func<T, string> keyOf,
        Func<CsvRow, string> newAssetPath, ICollection<string> ignored, List<string> errors) where T : ScriptableObject
    {
        var table = Load(file, errors);
        return table == null
            ? new List<T>()
            : ImportRows(table, keyColumn, keyOf, newAssetPath, ignored, errors);
    }

    private static List<T> ImportRows<T>(CsvTable table, string keyColumn, Func<T, string> keyOf,
        Func<CsvRow, string> newAssetPath, ICollection<string> ignored, List<string> errors) where T : ScriptableObject
    {
        var result = new List<T>();
        if (!table.HasColumn(keyColumn))
        {
            errors.Add($"{table.Name}: missing key column '{keyColumn}'.");
            return result;
        }

        UnityTableConverters.Install();
        var existing = FindExisting(keyOf);
        var seen = new HashSet<string>();

        foreach (var row in table.Rows)
        {
            var key = row.Get(keyColumn);
            if (string.IsNullOrEmpty(key)) { errors.Add($"{row.Where(keyColumn)}: empty key."); continue; }
            if (!seen.Add(key)) { errors.Add($"{row.Where(keyColumn)}: duplicate key '{key}'."); continue; }

            if (!existing.TryGetValue(key, out var asset))
            {
                var path = newAssetPath(row);
                EnsureFolderFor(path);
                asset = LoadOrCreate<T>(path);
            }

            errors.AddRange(TableBinder.Bind(asset, row, ignored));
            EditorUtility.SetDirty(asset);
            result.Add(asset);
        }

        return result;
    }

    private static Dictionary<string, T> FindExisting<T>(Func<T, string> keyOf) where T : ScriptableObject
    {
        var result = new Dictionary<string, T>();
        foreach (var guid in AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { "Assets/_Project" }))
        {
            var asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
            if (asset == null) continue;
            var key = keyOf(asset);
            if (!string.IsNullOrEmpty(key) && !result.ContainsKey(key)) result[key] = asset;
        }
        return result;
    }

    private static CsvTable Load(string file, List<string> errors)
    {
        var path = $"{TablesDir}/{file}";
        if (!File.Exists(path)) { errors.Add($"{path} not found."); return null; }
        try
        {
            return CsvTable.Parse(File.ReadAllText(path), file);
        }
        catch (FormatException e)
        {
            errors.Add(e.Message);
            return null;
        }
    }

    private static T LoadOrCreate<T>(string path) where T : ScriptableObject
    {
        var existing = AssetDatabase.LoadAssetAtPath<T>(path);
        if (existing != null) return existing;
        var created = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(created, path);
        return created;
    }

    private static void EnsureFolderFor(string assetPath) =>
        EnsureFolder(Path.GetDirectoryName(assetPath)?.Replace('\\', '/'));

    private static void EnsureFolder(string folder)
    {
        if (string.IsNullOrEmpty(folder) || AssetDatabase.IsValidFolder(folder)) return;
        var parent = Path.GetDirectoryName(folder)?.Replace('\\', '/');
        EnsureFolder(parent);
        AssetDatabase.CreateFolder(parent, Path.GetFileName(folder));
    }
}
