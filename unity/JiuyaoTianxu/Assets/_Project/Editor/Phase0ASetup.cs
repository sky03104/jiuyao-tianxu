using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

/// <summary>
/// One-off Phase 0-A bootstrap: creates the URP asset, assigns it as the
/// project's render pipeline, and builds a minimal test scene.
/// Run via: Unity.exe -batchmode -executeMethod Phase0ASetup.Run -quit
/// </summary>
public static class Phase0ASetup
{
    public static void Run()
    {
        SetupUrp();
        SetupTestScene();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void SetupUrp()
    {
        var settingsFolder = "Assets/_Project/Settings";
        if (!AssetDatabase.IsValidFolder(settingsFolder))
        {
            AssetDatabase.CreateFolder("Assets/_Project", "Settings");
        }

        // Route through the real "Create > Rendering > URP Asset (with Universal Renderer)"
        // menu command instead of ScriptableObject.CreateInstance directly — a bare
        // CreateInstance skips linking a UniversalRendererData, which leaves
        // scriptableRendererData null and breaks default material/shader lookups.
        var before = new System.Collections.Generic.HashSet<string>(AssetDatabase.FindAssets("t:UniversalRenderPipelineAsset"));

        var folderObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(settingsFolder);
        Selection.activeObject = folderObj;
        EditorApplication.ExecuteMenuItem("Assets/Create/Rendering/URP Asset (with Universal Renderer)");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        string newGuid = null;
        foreach (var guid in AssetDatabase.FindAssets("t:UniversalRenderPipelineAsset"))
        {
            if (!before.Contains(guid)) { newGuid = guid; break; }
        }

        if (newGuid == null)
        {
            Debug.LogError("[Phase0ASetup] Failed to locate the URP asset created via the menu command.");
            return;
        }

        var createdPath = AssetDatabase.GUIDToAssetPath(newGuid);
        var finalPath = settingsFolder + "/JiuyaoTianxu_URP.asset";
        var err = AssetDatabase.MoveAsset(createdPath, finalPath);
        if (!string.IsNullOrEmpty(err))
        {
            Debug.LogWarning("[Phase0ASetup] MoveAsset warning: " + err);
        }

        var pipelineAsset = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(finalPath);
        GraphicsSettings.defaultRenderPipeline = pipelineAsset;
        QualitySettings.renderPipeline = pipelineAsset;

        Debug.Log("[Phase0ASetup] URP asset created via menu command and assigned as default render pipeline.");
    }

    private static void SetupTestScene()
    {
        var scenesFolder = "Assets/_Project/Scenes";
        if (!AssetDatabase.IsValidFolder(scenesFolder))
        {
            AssetDatabase.CreateFolder("Assets/_Project", "Scenes");
        }

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(5f, 1f, 5f);

        var light = new GameObject("Directional Light");
        var lightComp = light.AddComponent<Light>();
        lightComp.type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        var camera = new GameObject("Main Camera");
        var camComp = camera.AddComponent<Camera>();
        camera.AddComponent<AudioListener>();
        camera.tag = "MainCamera";
        camera.transform.position = new Vector3(0f, 5f, -8f);
        camera.transform.rotation = Quaternion.Euler(20f, 0f, 0f);
        camComp.clearFlags = CameraClearFlags.Skybox;

        var scenePath = scenesFolder + "/Phase0A_NetworkTest.unity";
        EditorSceneManager.SaveScene(scene, scenePath);

        Debug.Log("[Phase0ASetup] Test scene created at " + scenePath);
    }
}
