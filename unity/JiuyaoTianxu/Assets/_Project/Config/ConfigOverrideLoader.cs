using System.Collections.Generic;
using System.IO;
using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Core;
using JiuyaoTianxu.Gameplay.Quests;
using UnityEngine;

namespace JiuyaoTianxu.Config
{
    /// <summary>
    /// Roadmap Phase 0 acceptance: "資料驅動配置表可透過表格切換測試，不需重新編譯".
    ///
    /// A built dedicated server looks for override tables in
    /// &lt;Build&gt;_Data/StreamingAssets/ConfigOverrides/ (or -configdir &lt;path&gt;) at
    /// startup and applies them onto the already-loaded definition assets, so a
    /// balance test is "edit CSV → restart server", no rebuild, no recompile.
    ///
    /// Scope, on purpose:
    ///   - server only: combat/seal/quest results are all decided server-side, so
    ///     that is where numbers matter (clients may show stale quest counts in
    ///     their logs — cosmetic);
    ///   - existing ids only: rows must name an AttackId/SealId/QuestNumId that is
    ///     in the build; new content still goes through the Editor importer;
    ///   - partial rows welcome: only non-empty cells are applied;
    ///   - never in the Editor: mutating ScriptableObject assets in Play Mode
    ///     would write the test values back into the project. Use the importer
    ///     (JiuyaoTianxu/Config/Import All Tables) there instead.
    /// </summary>
    public static class ConfigOverrideLoader
    {
        public static string DefaultDirectory => Path.Combine(Application.streamingAssetsPath, "ConfigOverrides");

        public static void ApplyServerOverrides()
        {
#if UNITY_EDITOR
            Debug.Log("[ConfigOverride] Skipped in the Editor (use the table importer instead).");
#else
            var dir = CommandLineFlags.ConfigDirectory ?? DefaultDirectory;
            if (!Directory.Exists(dir))
            {
                Debug.Log($"[ConfigOverride] No override directory at '{dir}'; using built-in data.");
                return;
            }

            UnityTableConverters.Install();
            var applied = 0;
            applied += Apply(dir, ConfigTableNames.Attacks, ConfigTableNames.AttackKey,
                IndexLoaded<AttackDefinition>(a => a.AttackId));
            applied += Apply(dir, ConfigTableNames.SpiritSeals, ConfigTableNames.SpiritSealKey,
                IndexLoaded<SpiritSealDefinition>(s => s.SealId.ToString()));
            applied += Apply(dir, ConfigTableNames.Quests, ConfigTableNames.QuestKey,
                IndexLoaded<QuestDefinition>(q => q.QuestNumId.ToString()));
            Debug.Log($"[ConfigOverride] Applied {applied} override row(s) from '{dir}'.");
#endif
        }

        /// <summary>Every loaded definition of type T by its table key. A duplicate
        /// key is reported and the first one wins — never a startup crash.</summary>
        private static Dictionary<string, T> IndexLoaded<T>(System.Func<T, string> key) where T : ScriptableObject
        {
            var result = new Dictionary<string, T>();
            foreach (var obj in Resources.FindObjectsOfTypeAll<T>())
            {
                var k = key(obj);
                if (string.IsNullOrEmpty(k)) continue;
                if (result.TryGetValue(k, out var existing))
                {
                    if (existing != obj)
                        Debug.LogWarning($"[ConfigOverride] Two loaded {typeof(T).Name} share key '{k}'; using '{existing.name}'.");
                    continue;
                }
                result[k] = obj;
            }
            return result;
        }

        private static int Apply<T>(string dir, string fileName, string keyColumn, Dictionary<string, T> byKey)
            where T : ScriptableObject
        {
            var path = Path.Combine(dir, fileName);
            if (!File.Exists(path)) return 0;

            CsvTable table;
            try
            {
                table = CsvTable.Parse(File.ReadAllText(path), fileName);
            }
            catch (System.FormatException e)
            {
                Debug.LogError($"[ConfigOverride] {e.Message}");
                return 0;
            }

            if (!table.HasColumn(keyColumn))
            {
                Debug.LogError($"[ConfigOverride] {fileName} has no key column '{keyColumn}'; ignored.");
                return 0;
            }

            var applied = 0;
            var ignoreKey = new[] { keyColumn };
            foreach (var row in table.Rows)
            {
                var key = row.Get(keyColumn);
                if (!byKey.TryGetValue(key, out var target))
                {
                    Debug.LogWarning($"[ConfigOverride] {row.Where(keyColumn)}: '{key}' is not in this build; skipped.");
                    continue;
                }

                var errors = TableBinder.Bind(target, row, ignoreKey);
                foreach (var e in errors) Debug.LogError($"[ConfigOverride] {e}");
                Debug.Log($"[ConfigOverride] {fileName} {key}: applied {table.Headers.Count - 1 - errors.Count} column(s).");
                applied++;
            }
            return applied;
        }
    }
}
