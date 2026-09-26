using System;
using System.IO;
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

    private const string MobileDprOff = "// config.devicePixelRatio = 1;";

    /// <summary>Turns on the template's own (commented-out) mobile line: render at CSS
    /// pixels, so the fixed-size IMGUI debug text isn't a third of its size on a 3x
    /// iPhone screen (and the frame rate is better).</summary>
    private static void RenderMobileAtCssPixels(string indexHtml)
    {
        var html = File.ReadAllText(indexHtml);
        if (!html.Contains(MobileDprOff))
        {
            Debug.LogWarning($"[Phase0DBuild] '{MobileDprOff}' not found in {indexHtml}; template changed?");
            return;
        }
        File.WriteAllText(indexHtml, html.Replace(MobileDprOff, "config.devicePixelRatio = 1;"));
    }

    private const string FusionConfigPath = "Assets/Photon/Fusion/Resources/NetworkProjectConfig.fusion";
    private const string WebGLClientServerOff = "\"AllowClientServerModesInWebGL\": false";
    private const string WebGLClientServerOn = "\"AllowClientServerModesInWebGL\": true";

    /// <summary>
    /// Phase 0-E touch test on an iPhone (no Mac, so no iOS build): a WebGL page that
    /// joins a PC-hosted session as a Client (the non-Editor default) from Safari.
    /// Everything it changes is restored afterwards, so the repo's settings and the
    /// Windows build stay as they are: Fusion's AllowClientServerModesInWebGL (Photon
    /// ships it off and calls it "not recommended"), compression (off here so any plain
    /// HTTP server can serve the files) and the active build target. Managed stripping
    /// is left alone: this project's WebGL default is already Minimal, which is what
    /// Fusion needs without a link.xml — keep it that way.
    /// Run via: Unity.exe -batchmode -executeMethod Phase0DBuild.BuildWebGL -quit
    /// </summary>
    public static void BuildWebGL()
    {
        var originalConfig = File.ReadAllText(FusionConfigPath);
        var originalCompression = PlayerSettings.WebGL.compressionFormat;
        try
        {
            if (!originalConfig.Contains(WebGLClientServerOff))
            {
                throw new InvalidOperationException($"[Phase0DBuild] {FusionConfigPath} has no '{WebGLClientServerOff}'.");
            }
            File.WriteAllText(FusionConfigPath, originalConfig.Replace(WebGLClientServerOff, WebGLClientServerOn));
            AssetDatabase.ImportAsset(FusionConfigPath, ImportAssetOptions.ForceUpdate);
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;

            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = new[] { Phase0DSetup.ScenePath },
                locationPathName = "Builds/Phase0D_WebGL",
                target = BuildTarget.WebGL,
                // Not Development: that .wasm was 113 MB, too much for iPhone Safari. The
                // evidence lives in the PC server's log anyway.
                options = BuildOptions.None,
            });

            if (report.summary.result == BuildResult.Succeeded)
            {
                RenderMobileAtCssPixels(Path.Combine(report.summary.outputPath, "index.html"));
                Debug.Log($"[Phase0DBuild] WebGL build succeeded: {report.summary.outputPath}, size {report.summary.totalSize} bytes.");
            }
            else
            {
                Debug.LogError($"[Phase0DBuild] WebGL build failed: {report.summary.result}, {report.summary.totalErrors} errors.");
            }
        }
        finally
        {
            File.WriteAllText(FusionConfigPath, originalConfig);
            AssetDatabase.ImportAsset(FusionConfigPath, ImportAssetOptions.ForceUpdate);
            PlayerSettings.WebGL.compressionFormat = originalCompression;
            AssetDatabase.SaveAssets();
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        }
    }
}
