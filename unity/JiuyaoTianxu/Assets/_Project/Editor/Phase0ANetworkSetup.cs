using System.Linq;
using Fusion;
using JiuyaoTianxu.Combat;
using JiuyaoTianxu.Net;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// One-off Phase 0-A network bootstrap: builds the Player and NetworkRunner
/// prefabs, and wires a NetworkGameLauncher into the test scene.
/// Run via: Unity.exe -batchmode -executeMethod Phase0ANetworkSetup.Run -quit
/// </summary>
public static class Phase0ANetworkSetup
{
    private const string PrefabFolder = "Assets/_Project/Net/Prefabs";
    private const string PlayerPrefabPath = PrefabFolder + "/Player.prefab";
    private const string RunnerPrefabPath = PrefabFolder + "/NetworkRunner.prefab";
    private const string ScenePath = "Assets/_Project/Scenes/Phase0A_NetworkTest.unity";

    public static void Run()
    {
        EnsureFolder("Assets/_Project", "Net");
        EnsureFolder("Assets/_Project/Net", "Prefabs");

        BuildPlayerPrefab();
        BuildRunnerPrefab();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // WireLauncherIntoScene loads the prefabs by path itself, AFTER opening the
        // target scene — opening a different scene was observed to invalidate any
        // prefab object reference obtained beforehand (even ones loaded fresh via
        // AssetDatabase.LoadAssetAtPath just before the call), silently leaving the
        // launcher's fields null if resolved too early.
        WireLauncherIntoScene();

        Debug.Log("[Phase0ANetworkSetup] Player + NetworkRunner prefabs built and wired into test scene.");
    }

    private static void EnsureFolder(string parent, string name)
    {
        if (!AssetDatabase.IsValidFolder(parent + "/" + name))
        {
            AssetDatabase.CreateFolder(parent, name);
        }
    }

    private static NetworkObject BuildPlayerPrefab()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        go.name = "Player";

        var netObj = go.AddComponent<NetworkObject>();
        go.AddComponent<NetworkTransform>();
        go.AddComponent<PlayerMovement>();
        go.AddComponent<PlayerCombat>();
        go.AddComponent<Health>();

        var savedPrefab = PrefabUtility.SaveAsPrefabAsset(go, PlayerPrefabPath);
        Debug.Log($"[Phase0ANetworkSetup] BuildPlayerPrefab: savedPrefab={(savedPrefab == null ? "NULL" : savedPrefab.name)}, " +
                  $"components on go before destroy: {string.Join(",", go.GetComponents<Component>().Select(c => c.GetType().Name))}");
        Object.DestroyImmediate(go);

        if (savedPrefab == null)
        {
            Debug.LogError("[Phase0ANetworkSetup] SaveAsPrefabAsset returned null for Player prefab.");
            return null;
        }

        var netObjOnPrefab = savedPrefab.GetComponent<NetworkObject>();
        Debug.Log($"[Phase0ANetworkSetup] NetworkObject on saved prefab: {(netObjOnPrefab == null ? "NULL" : "OK")}");
        return netObjOnPrefab;
    }

    private static NetworkRunner BuildRunnerPrefab()
    {
        var go = new GameObject("NetworkRunner");
        go.AddComponent<NetworkRunner>();
        go.AddComponent<NetworkSceneManagerDefault>();
        go.AddComponent<NetworkObjectProviderDefault>();

        var savedPrefab = PrefabUtility.SaveAsPrefabAsset(go, RunnerPrefabPath);
        Debug.Log($"[Phase0ANetworkSetup] BuildRunnerPrefab: savedPrefab={(savedPrefab == null ? "NULL" : savedPrefab.name)}");
        Object.DestroyImmediate(go);

        if (savedPrefab == null)
        {
            Debug.LogError("[Phase0ANetworkSetup] SaveAsPrefabAsset returned null for NetworkRunner prefab.");
            return null;
        }

        return savedPrefab.GetComponent<NetworkRunner>();
    }

    private static void WireLauncherIntoScene()
    {
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);

        var playerPrefab = AssetDatabase.LoadAssetAtPath<NetworkObject>(PlayerPrefabPath);
        var runnerPrefab = AssetDatabase.LoadAssetAtPath<NetworkRunner>(RunnerPrefabPath);

        var existing = Object.FindAnyObjectByType<NetworkGameLauncher>();
        if (existing != null)
        {
            Object.DestroyImmediate(existing.gameObject);
        }

        var launcherGo = new GameObject("NetworkGameLauncher");
        var launcher = launcherGo.AddComponent<NetworkGameLauncher>();

        if (playerPrefab == null || runnerPrefab == null)
        {
            Debug.LogError($"[Phase0ANetworkSetup] Could not load prefabs by path: player={playerPrefab != null}, runner={runnerPrefab != null}");
        }

        var so = new SerializedObject(launcher);
        so.FindProperty("_runnerPrefab").objectReferenceValue = runnerPrefab;
        so.FindProperty("_playerPrefab").objectReferenceValue = playerPrefab;
        so.ApplyModifiedPropertiesWithoutUndo();

        Debug.Log($"[Phase0ANetworkSetup] Wired launcher: player={(playerPrefab == null ? "NULL" : "OK")}, runner={(runnerPrefab == null ? "NULL" : "OK")}");

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
    }
}
