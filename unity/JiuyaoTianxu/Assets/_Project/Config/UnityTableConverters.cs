using System;
using System.Globalization;
using UnityEngine;

namespace JiuyaoTianxu.Config
{
    /// <summary>
    /// Registers Unity-only value types with the pure-C# TableBinder.
    /// Vector3 cells are written "x;y;z" (semicolons, so the cell needs no CSV
    /// quoting). Called by both the Editor importer and the runtime override.
    /// </summary>
    public static class UnityTableConverters
    {
        private static bool _installed;

        public static void Install()
        {
            if (_installed) return;
            _installed = true;
            TableBinder.RegisterConverter(typeof(Vector3), ParseVector3);
        }

        private static object ParseVector3(string s)
        {
            var parts = s.Split(';');
            if (parts.Length != 3) throw new FormatException();
            return new Vector3(
                float.Parse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture),
                float.Parse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture),
                float.Parse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture));
        }
    }
}
