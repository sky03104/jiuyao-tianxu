using System.Linq;
using Fusion;
using JiuyaoTianxu.Combat;
using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Net;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// One-off Phase 0-A/0-B network bootstrap: builds the Player, Projectile and
/// NetworkRunner prefabs, wires the six WeaponDefinition assets into
/// CombatController, and wires a NetworkGameLauncher into the test scene.
/// Run via: Unity.exe -batchmode -executeMethod Phase0ANetworkSetup.Run -quit
/// </summary>
public static class Phase0ANetworkSetup
{
    private const string PrefabFolder = "Assets/_Project/Net/Prefabs";
    private const string PlayerPrefabPath = PrefabFolder + "/Player.prefab";
    private const string RunnerPrefabPath = PrefabFolder + "/NetworkRunner.prefab";
    private const string ProjectilePrefabPath = PrefabFolder + "/Projectile.prefab";
    private const string ScenePath = "Assets/_Project/Scenes/Phase0A_NetworkTest.unity";
    private const string WeaponsRoot = "Assets/_Project/Combat/Weapons";
    private const string SpiritSealsRoot = "Assets/_Project/Combat/SpiritSeals";

    private static readonly WeaponType[] WeaponOrder =
    {
        WeaponType.Blade, WeaponType.Sword, WeaponType.Spear,
        WeaponType.Bow, WeaponType.HeavyBlade, WeaponType.Staff,
    };

    public static void Run()
    {
        EnsureFolder("Assets/_Project", "Net");
        EnsureFolder("Assets/_Project/Net", "Prefabs");

        Phase0BWeaponDataSetup.Run();
        Phase0CSpiritSealDataSetup.Run();

        BuildPlayerPrefab();
        BuildRunnerPrefab();
        BuildProjectilePrefab();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // WireLauncherIntoScene loads everything by path itself, AFTER opening the
        // target scene — opening a different scene was observed to invalidate any
        // asset object reference obtained beforehand (even ones loaded fresh via
        // AssetDatabase.LoadAssetAtPath just before the call), silently leaving
        // fields null if resolved too early. See README "已知問題" for the full story.
        WireLauncherIntoScene();
        WireCombatControllerOnPrefab();
        WireSpiritSealSystemOnPrefab();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("[Phase0ANetworkSetup] Player + Projectile + NetworkRunner prefabs built and wired.");
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

        go.AddComponent<NetworkObject>();
        go.AddComponent<NetworkTransform>();
        go.AddComponent<PlayerMovement>();
        go.AddComponent<Health>();
        go.AddComponent<CombatState>();
        go.AddComponent<CombatController>();
        go.AddComponent<SpiritSealLoadout>();
        go.AddComponent<SpiritSealSystem>();
        go.AddComponent<BurnStatus>();  // tech review D1: burn lives on the target
        go.AddComponent<JiuyaoTianxu.Gameplay.World.PlayerLifecycle>(); // tech review D3: test respawn
        go.AddComponent<JiuyaoTianxu.Combat.Targeting.TargetLock>();  // Phase 0-E
        go.AddComponent<JiuyaoTianxu.UI.Hud.HealthFeedback>();         // Phase 0-E

        var savedPrefab = PrefabUtility.SaveAsPrefabAsset(go, PlayerPrefabPath);
        Object.DestroyImmediate(go);

        if (savedPrefab == null)
        {
            Debug.LogError("[Phase0ANetworkSetup] SaveAsPrefabAsset returned null for Player prefab.");
            return null;
        }

        return savedPrefab.GetComponent<NetworkObject>();
    }

    private static NetworkRunner BuildRunnerPrefab()
    {
        var go = new GameObject("NetworkRunner");
        go.AddComponent<NetworkRunner>();
        go.AddComponent<NetworkSceneManagerDefault>();
        go.AddComponent<NetworkObjectProviderDefault>();

        var savedPrefab = PrefabUtility.SaveAsPrefabAsset(go, RunnerPrefabPath);
        Object.DestroyImmediate(go);

        if (savedPrefab == null)
        {
            Debug.LogError("[Phase0ANetworkSetup] SaveAsPrefabAsset returned null for NetworkRunner prefab.");
            return null;
        }

        return savedPrefab.GetComponent<NetworkRunner>();
    }

    private static NetworkObject BuildProjectilePrefab()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = "Projectile";
        go.transform.localScale = Vector3.one * 0.3f;
        var col = go.GetComponent<Collider>();
        if (col != null) col.isTrigger = true; // visual/marker collider only; Projectile.cs does its own OverlapSphere.

        go.AddComponent<NetworkObject>();
        go.AddComponent<NetworkTransform>();
        go.AddComponent<Projectile>();

        var savedPrefab = PrefabUtility.SaveAsPrefabAsset(go, ProjectilePrefabPath);
        Object.DestroyImmediate(go);

        if (savedPrefab == null)
        {
            Debug.LogError("[Phase0ANetworkSetup] SaveAsPrefabAsset returned null for Projectile prefab.");
            return null;
        }

        return savedPrefab.GetComponent<NetworkObject>();
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

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
    }

    /// <summary>
    /// Wires the six WeaponDefinition assets + the Projectile prefab into
    /// CombatController on the Player PREFAB ASSET (not a scene instance) —
    /// PrefabUtility.LoadPrefabContents/SaveAsPrefabAsset round-trip so every
    /// spawned Player picks these up without needing a scene-instance override.
    /// </summary>
    private static void WireCombatControllerOnPrefab()
    {
        var root = PrefabUtility.LoadPrefabContents(PlayerPrefabPath);
        var controller = root.GetComponent<CombatController>();

        var weapons = WeaponOrder
            .Select(w => AssetDatabase.LoadAssetAtPath<WeaponDefinition>($"{WeaponsRoot}/{w}/{w}_Weapon.asset"))
            .ToArray();

        var missing = weapons.Where(w => w == null).Count();
        if (missing > 0)
        {
            Debug.LogError($"[Phase0ANetworkSetup] {missing} WeaponDefinition asset(s) failed to load.");
        }

        var projectilePrefab = AssetDatabase.LoadAssetAtPath<NetworkObject>(ProjectilePrefabPath);

        var so = new SerializedObject(controller);
        var weaponsProp = so.FindProperty("_weapons");
        weaponsProp.arraySize = weapons.Length;
        for (var i = 0; i < weapons.Length; i++)
        {
            weaponsProp.GetArrayElementAtIndex(i).objectReferenceValue = weapons[i];
        }
        so.FindProperty("_projectilePrefab").objectReferenceValue = projectilePrefab;
        so.ApplyModifiedPropertiesWithoutUndo();

        PrefabUtility.SaveAsPrefabAsset(root, PlayerPrefabPath);
        PrefabUtility.UnloadPrefabContents(root);

        Debug.Log($"[Phase0ANetworkSetup] Wired {weapons.Length - missing}/{weapons.Length} weapons + projectile prefab into CombatController.");
    }

    /// <summary>Wires the Spirit Seal registry into SpiritSealSystem on the Player
    /// prefab asset, same LoadPrefabContents/SaveAsPrefabAsset round-trip as
    /// WireCombatControllerOnPrefab (no scene open happens here, so this isn't
    /// exposed to the "reference invalidated after OpenScene" pitfall).</summary>
    private static void WireSpiritSealSystemOnPrefab()
    {
        var root = PrefabUtility.LoadPrefabContents(PlayerPrefabPath);
        var sealSystem = root.GetComponent<SpiritSealSystem>();

        var registry = AssetDatabase.LoadAssetAtPath<SpiritSealRegistry>($"{SpiritSealsRoot}/SpiritSealRegistry.asset");
        if (registry == null)
        {
            Debug.LogError("[Phase0ANetworkSetup] SpiritSealRegistry asset failed to load.");
        }

        var so = new SerializedObject(sealSystem);
        so.FindProperty("_registry").objectReferenceValue = registry;
        so.ApplyModifiedPropertiesWithoutUndo();

        PrefabUtility.SaveAsPrefabAsset(root, PlayerPrefabPath);
        PrefabUtility.UnloadPrefabContents(root);

        Debug.Log($"[Phase0ANetworkSetup] Wired SpiritSealRegistry into SpiritSealSystem: {(registry != null ? "OK" : "FAILED")}.");
    }
}
