using System;
using System.Collections.Generic;
using System.Text;

namespace JiuyaoTianxu.Config
{
    /// <summary>One data row plus where it came from, so every error can say
    /// "attacks.csv line 7, column Damage".</summary>
    public sealed class CsvRow
    {
        public readonly CsvTable Table;
        public readonly int LineNumber;
        private readonly string[] _cells;

        internal CsvRow(CsvTable table, int lineNumber, string[] cells)
        {
            Table = table;
            LineNumber = lineNumber;
            _cells = cells;
        }

        /// <summary>Cell by header name; null if the table has no such column.
        /// Missing trailing cells read as "".</summary>
        public string Get(string column)
        {
            var index = Table.IndexOf(column);
            if (index < 0) return null;
            return index < _cells.Length ? _cells[index] : "";
        }

        public string Where(string column) => $"{Table.Name} line {LineNumber}, column {column}";
    }

    /// <summary>
    /// Minimal RFC 4180 CSV reader for design tables (Excel / Google Sheets
    /// export). Pure C# — no Unity — so it is unit-tested outside Unity
    /// (unity/JiuyaoTianxu/Tools/ConfigTableTests).
    ///
    /// Rules: first non-comment row is the header; a row whose first cell starts
    /// with '#' is a comment (designer notes); blank rows are skipped; quoted cells
    /// may contain commas, "" escapes and newlines; a UTF-8 BOM is ignored;
    /// unquoted cells are trimmed.
    /// </summary>
    public sealed class CsvTable
    {
        public readonly string Name;
        public readonly IReadOnlyList<string> Headers;
        public readonly IReadOnlyList<CsvRow> Rows;
        private readonly Dictionary<string, int> _headerIndex;

        private CsvTable(string name, List<string> headers, List<(int line, string[] cells)> rows)
        {
            Name = name;
            Headers = headers;
            _headerIndex = new Dictionary<string, int>(StringComparer.Ordinal);
            for (var i = 0; i < headers.Count; i++)
            {
                if (headers[i].Length == 0) throw new FormatException($"{name}: header column {i + 1} is empty.");
                if (_headerIndex.ContainsKey(headers[i]))
                    throw new FormatException($"{name}: duplicate header column '{headers[i]}'.");
                _headerIndex[headers[i]] = i;
            }

            var built = new List<CsvRow>(rows.Count);
            foreach (var (line, cells) in rows)
            {
                if (cells.Length > headers.Count)
                {
                    for (var i = headers.Count; i < cells.Length; i++)
                    {
                        if (cells[i].Length > 0)
                            throw new FormatException($"{name} line {line}: more cells than header columns.");
                    }
                }
                built.Add(new CsvRow(this, line, cells));
            }
            Rows = built;
        }

        public int IndexOf(string column) => _headerIndex.TryGetValue(column, out var i) ? i : -1;
        public bool HasColumn(string column) => _headerIndex.ContainsKey(column);

        public static CsvTable Parse(string text, string name)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (text.Length > 0 && text[0] == '﻿') text = text.Substring(1);

            List<string> headers = null;
            var rows = new List<(int, string[])>();

            foreach (var (line, cells) in ReadRecords(text, name))
            {
                if (cells.Length == 1 && cells[0].Length == 0) continue; // blank line
                if (IsAllEmpty(cells)) continue;                         // ",,," spreadsheet filler
                if (cells[0].StartsWith("#", StringComparison.Ordinal)) continue;

                if (headers == null) headers = new List<string>(cells);
                else rows.Add((line, cells));
            }

            if (headers == null) throw new FormatException($"{name}: no header row.");
            while (headers.Count > 0 && headers[headers.Count - 1].Length == 0) headers.RemoveAt(headers.Count - 1);
            return new CsvTable(name, headers, rows);
        }

        private static bool IsAllEmpty(string[] cells)
        {
            foreach (var c in cells) if (c.Length > 0) return false;
            return true;
        }

        private static IEnumerable<(int line, string[] cells)> ReadRecords(string text, string name)
        {
            var cells = new List<string>();
            var cell = new StringBuilder();
            var line = 1;
            var recordLine = 1;
            var i = 0;
            var inQuotes = false;
            var cellWasQuoted = false;

            while (i < text.Length)
            {
                var c = text[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < text.Length && text[i + 1] == '"') { cell.Append('"'); i += 2; continue; }
                        inQuotes = false; i++; continue;
                    }
                    if (c == '\n') line++;
                    cell.Append(c); i++;
                    continue;
                }

                switch (c)
                {
                    case '"':
                        if (cell.ToString().Trim().Length > 0)
                            throw new FormatException($"{name} line {line}: quote in the middle of an unquoted cell.");
                        cell.Clear();
                        inQuotes = true;
                        cellWasQuoted = true;
                        i++;
                        break;
                    case ',':
                        cells.Add(Finish(cell, cellWasQuoted));
                        cellWasQuoted = false;
                        i++;
                        break;
                    case '\r':
                        i++;
                        break;
                    case '\n':
                        cells.Add(Finish(cell, cellWasQuoted));
                        cellWasQuoted = false;
                        yield return (recordLine, cells.ToArray());
                        cells.Clear();
                        line++;
                        recordLine = line;
                        i++;
                        break;
                    default:
                        if (cellWasQuoted)
                        {
                            if (!char.IsWhiteSpace(c))
                                throw new FormatException($"{name} line {line}: text after a closing quote.");
                            i++; // whitespace between closing quote and delimiter
                            break;
                        }
                        cell.Append(c);
                        i++;
                        break;
                }
            }

            if (inQuotes) throw new FormatException($"{name} line {recordLine}: unterminated quoted cell.");
            if (cells.Count > 0 || cell.Length > 0 || cellWasQuoted)
            {
                cells.Add(Finish(cell, cellWasQuoted));
                yield return (recordLine, cells.ToArray());
            }
        }

        private static string Finish(StringBuilder cell, bool quoted)
        {
            var s = quoted ? cell.ToString() : cell.ToString().Trim();
            cell.Clear();
            return s;
        }
    }
}
