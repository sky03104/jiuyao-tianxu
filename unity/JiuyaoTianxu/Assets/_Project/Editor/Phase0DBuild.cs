using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// Builds a Windows standalone player containing only Phase0D_TestScene for the
/// HANDOFF-008 1 Server + 2 Client headless run (see Tools/Phase0D/).
/// Run via: Unity.exe -batchmode -executeMethod Phase0DBuild.Build -quit
/// </summary>
public static class Phase0DBuild
{
    public static void Build()
    {
        var options = new BuildPlayerOptions
        {
            scenes = new[] { Phase0DSetup.ScenePath },
            locationPathName = "Builds/Phase0D/JiuyaoTianxu.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.Development,
        };

        var report = BuildPipeline.BuildPlayer(options);

        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"[Phase0DBuild] Build succeeded: {report.summary.outputPath}, size {report.summary.totalSize} bytes.");
        }
        else
        {
            Debug.LogError($"[Phase0DBuild] Build failed: {report.summary.result}, {report.summary.totalErrors} errors.");
        }
    }
}
