using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace JiuyaoTianxu.Config
{
    /// <summary>
    /// Writes CSV cells onto an object's public instance fields, matching header
    /// name to field name exactly. Pure C#; Unity-only types (Vector3) plug in via
    /// <see cref="RegisterConverter"/> from Unity-side code.
    ///
    /// Rules (the same for the Editor importer and the server runtime override):
    ///   - a column with no matching public field is an ERROR (catches typos);
    ///   - an empty cell leaves the field unchanged (so override tables can carry
    ///     only the columns/cells being tuned);
    ///   - enums are written by NAME (Blade, Sphere…), never by number;
    ///   - numbers use invariant culture ("0.15", not "0,15");
    ///   - bool accepts true/false/1/0 (case-insensitive).
    /// </summary>
    public static class TableBinder
    {
        private static readonly Dictionary<Type, Func<string, object>> Converters = new()
        {
            [typeof(string)] = s => s,
            [typeof(int)] = s => int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture),
            [typeof(float)] = s => float.Parse(s, NumberStyles.Float, CultureInfo.InvariantCulture),
            [typeof(bool)] = ParseBool,
        };

        public static void RegisterConverter(Type type, Func<string, object> converter) => Converters[type] = converter;

        public static bool CanConvert(Type type) => type.IsEnum || Converters.ContainsKey(type);

        /// <summary>Binds every non-ignored column of <paramref name="row"/> onto
        /// <paramref name="target"/>. Returns the list of problems (empty = OK); a
        /// cell that fails to convert leaves its field untouched.</summary>
        public static List<string> Bind(object target, CsvRow row, ICollection<string> ignoredColumns = null)
        {
            var errors = new List<string>();
            var type = target.GetType();

            foreach (var column in row.Table.Headers)
            {
                if (ignoredColumns != null && ignoredColumns.Contains(column)) continue;

                var field = type.GetField(column, BindingFlags.Public | BindingFlags.Instance);
                if (field == null)
                {
                    errors.Add($"{row.Where(column)}: {type.Name} has no public field '{column}'.");
                    continue;
                }

                var cell = row.Get(column);
                if (string.IsNullOrEmpty(cell)) continue;

                if (!TryConvert(cell, field.FieldType, out var value, out var error))
                {
                    errors.Add($"{row.Where(column)}: {error}");
                    continue;
                }
                field.SetValue(target, value);
            }

            return errors;
        }

        public static bool TryConvert(string cell, Type type, out object value, out string error)
        {
            value = null;
            error = null;
            try
            {
                if (type.IsEnum)
                {
                    // Names only: a bare number would silently bind to whatever enum
                    // value happens to sit at that index today.
                    if (!Enum.IsDefined(type, cell))
                    {
                        error = $"'{cell}' is not a {type.Name} (use one of: {string.Join(", ", Enum.GetNames(type))}).";
                        return false;
                    }
                    value = Enum.Parse(type, cell);
                    return true;
                }

                if (!Converters.TryGetValue(type, out var convert))
                {
                    error = $"field type {type.Name} is not supported by TableBinder.";
                    return false;
                }
                value = convert(cell);
                return true;
            }
            catch (Exception e) when (e is FormatException || e is OverflowException)
            {
                error = $"'{cell}' is not a valid {type.Name}.";
                return false;
            }
        }

        private static object ParseBool(string s)
        {
            switch (s.Trim().ToLowerInvariant())
            {
                case "true": case "1": return true;
                case "false": case "0": return false;
                default: throw new FormatException();
            }
        }
    }
}
