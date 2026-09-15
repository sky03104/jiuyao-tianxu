using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// Builds a Windows standalone player for Phase 0-A headless network testing.
/// Run via: Unity.exe -batchmode -executeMethod Phase0ABuild.Build -quit
/// </summary>
public static class Phase0ABuild
{
    public static void Build()
    {
        var options = new BuildPlayerOptions
        {
            scenes = new[] { "Assets/_Project/Scenes/Phase0A_NetworkTest.unity" },
            locationPathName = "Builds/Phase0A/JiuyaoTianxu.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.Development,
        };

        var report = BuildPipeline.BuildPlayer(options);

        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"[Phase0ABuild] Build succeeded: {report.summary.outputPath}, size {report.summary.totalSize} bytes.");
        }
        else
        {
            Debug.LogError($"[Phase0ABuild] Build failed: {report.summary.result}, {report.summary.totalErrors} errors.");
        }
    }
}
