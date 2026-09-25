// Unity-free unit tests for CsvTable / TableBinder (Assets/_Project/Config/Core).
// Run: ./run.sh (mono + Roslyn) or run.ps1 (.NET SDK).
using System;
using System.Collections.Generic;
using JiuyaoTianxu.Config;

public enum Shape { Sphere, Capsule }

public class Sample
{
    public string Id;
    public int Damage = 7;
    public float Range = 1.5f;
    public bool CanCombo = true;
    public Shape HitShape = Shape.Sphere;
    public string Note = "keep";
    public double Unsupported;
    private int _hidden;
    public int Hidden => _hidden;
}

public static class ConfigTableTests
{
    private static int _passed, _failed;

    private static void Check(bool ok, string name)
    {
        if (ok) _passed++;
        else { _failed++; Console.WriteLine("FAIL: " + name); }
    }

    private static void Throws(Action a, string contains, string name)
    {
        try { a(); Check(false, name + " (no exception)"); }
        catch (FormatException e) { Check(e.Message.Contains(contains), name + " — message: " + e.Message); }
    }

    public static int Main()
    {
        Parsing();
        ParsingErrors();
        Binding();
        BindingErrors();
        Console.WriteLine($"ConfigTableTests: {_passed} passed, {_failed} failed");
        return _failed == 0 ? 0 : 1;
    }

    private static void Parsing()
    {
        var text = "﻿# designer note, with commas\r\n" +
                   "Id,Damage,Note\r\n" +
                   "\r\n" +
                   "a, 5 ,plain\r\n" +
                   ",,\r\n" +
                   "# skipped row\r\n" +
                   "b,6,\"has, comma\"\r\n" +
                   "c,7,\"say \"\"hi\"\"\"\n" +
                   "d,8,\"two\nlines\"\n" +
                   "e,9";
        var t = CsvTable.Parse(text, "t.csv");
        Check(t.Headers.Count == 3 && t.Headers[0] == "Id", "BOM stripped, header read after comment");
        Check(t.Rows.Count == 5, "blank, all-empty and comment rows skipped (got " + t.Rows.Count + ")");
        Check(t.Rows[0].Get("Damage") == "5", "unquoted cells trimmed");
        Check(t.Rows[1].Get("Note") == "has, comma", "quoted comma");
        Check(t.Rows[2].Get("Note") == "say \"hi\"", "escaped quotes");
        Check(t.Rows[3].Get("Note") == "two\nlines", "newline inside quotes");
        Check(t.Rows[4].Get("Note") == "", "missing trailing cell reads as empty");
        Check(t.Rows[4].Get("Nope") == null, "unknown column reads as null");
        Check(t.Rows[0].LineNumber == 4 && t.Rows[4].LineNumber == 11, "line numbers track source lines (incl. quoted newline)");
        Check(t.Rows[1].Where("Note") == "t.csv line 7, column Note", "error location text");

        var trailing = CsvTable.Parse("A,B,\n1,2,\n", "x");
        Check(trailing.Headers.Count == 2, "trailing empty header column dropped (spreadsheet export)");
        Check(CsvTable.Parse("A\n\"q\"  \n", "x").Rows[0].Get("A") == "q", "whitespace after closing quote ignored");
    }

    private static void ParsingErrors()
    {
        Throws(() => CsvTable.Parse("", "e.csv"), "no header", "empty file");
        Throws(() => CsvTable.Parse("A,A\n1,2", "e.csv"), "duplicate header", "duplicate header");
        Throws(() => CsvTable.Parse("A,,B\n1,2,3", "e.csv"), "empty", "empty header in the middle");
        Throws(() => CsvTable.Parse("A\n\"open", "e.csv"), "unterminated", "unterminated quote");
        Throws(() => CsvTable.Parse("A\n\"x\"y", "e.csv"), "after a closing quote", "text after closing quote");
        Throws(() => CsvTable.Parse("A\nab\"c", "e.csv"), "quote in the middle", "stray quote");
        Throws(() => CsvTable.Parse("A\n1,2", "e.csv"), "more cells", "extra non-empty cell");
    }

    private static void Binding()
    {
        var t = CsvTable.Parse("Id,Damage,Range,CanCombo,HitShape,Note\nx,12,0.25,FALSE,Capsule,\n", "s.csv");
        var s = new Sample();
        var errors = TableBinder.Bind(s, t.Rows[0]);
        Check(errors.Count == 0, "valid row binds without errors: " + string.Join(" | ", errors));
        Check(s.Id == "x" && s.Damage == 12 && Math.Abs(s.Range - 0.25f) < 1e-6 && !s.CanCombo && s.HitShape == Shape.Capsule,
            "string/int/float/bool/enum bound");
        Check(s.Note == "keep", "empty cell leaves field unchanged (partial override)");

        var partial = CsvTable.Parse("Id,Range\nx,3\n", "p.csv");
        var p = new Sample();
        TableBinder.Bind(p, partial.Rows[0]);
        Check(p.Damage == 7 && Math.Abs(p.Range - 3f) < 1e-6, "partial table only touches its columns");

        var ignored = CsvTable.Parse("Id,Combo\nx,a|b\n", "i.csv");
        Check(TableBinder.Bind(new Sample(), ignored.Rows[0], new[] { "Combo" }).Count == 0, "ignored column skipped");

        Check(TableBinder.TryConvert("1", typeof(bool), out var b1, out _) && (bool)b1, "bool 1");
        Check(TableBinder.TryConvert("0.5", typeof(float), out var f, out _) && Math.Abs((float)f - 0.5f) < 1e-6,
            "invariant-culture float");
    }

    private static void BindingErrors()
    {
        var t = CsvTable.Parse("Id,Damag,Damage,HitShape,CanCombo,Range,Unsupported,Hidden\n" +
                               "x,1,abc,1,maybe,1,5,2\n", "b.csv");
        var s = new Sample();
        var errors = TableBinder.Bind(s, t.Rows[0]);
        bool Has(string col, string text) => errors.Exists(e => e.Contains("column " + col) && e.Contains(text));

        Check(Has("Damag", "no public field"), "typo column reported");
        Check(Has("Damage", "not a valid Int32"), "bad int reported with location");
        Check(Has("HitShape", "not a Shape"), "enum by number rejected (names only)");
        Check(Has("CanCombo", "not a valid Boolean"), "bad bool reported");
        Check(Has("Unsupported", "not supported"), "unsupported field type reported");
        Check(Has("Hidden", "no public field"), "properties/private fields are not bindable");
        Check(s.Damage == 7 && s.HitShape == Shape.Sphere && s.CanCombo, "failed cells leave fields untouched");
        Check(Math.Abs(s.Range - 1f) < 1e-6, "valid cells in a row with errors still bind");
    }
}
