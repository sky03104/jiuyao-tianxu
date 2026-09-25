using System.Collections.Generic;
using Fusion;
using JiuyaoTianxu.Combat;
using JiuyaoTianxu.Gameplay.Quests;
using JiuyaoTianxu.Gameplay.Testing;
using JiuyaoTianxu.Gameplay.World;
using JiuyaoTianxu.Net;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// One-off Phase 0-D bootstrap (HANDOFF-008): imports quests.csv (the two test
/// quests + QuestRegistry), the Phase0D_TestMonster prefab, adds PlayerQuestLog/
/// QuestTracker to the shared Player prefab, and builds Phase0D_TestScene.
/// Requires Phase0ANetworkSetup to have been run first (Player/NetworkRunner
/// prefabs must exist).
/// Run via: Unity.exe -batchmode -executeMethod Phase0DSetup.Run -quit
/// </summary>
public static class Phase0DSetup
{
    private const string GameplayRoot = "Assets/_Project/Gameplay";
    private const string QuestDataFolder = GameplayRoot + "/Quests/Data";
    private const string RegistryPath = QuestDataFolder + "/QuestRegistry.asset";
    private const string WorldPrefabFolder = GameplayRoot + "/World/Prefabs";
    private const string MonsterPrefabPath = WorldPrefabFolder + "/Phase0D_TestMonster.prefab";
    private const string MonsterMaterialPath = WorldPrefabFolder + "/Phase0D_TestMonster.mat";
    private const string PlayerPrefabPath = "Assets/_Project/Net/Prefabs/Player.prefab";
    private const string RunnerPrefabPath = "Assets/_Project/Net/Prefabs/NetworkRunner.prefab";
    private const string Phase0AScenePath = "Assets/_Project/Scenes/Phase0A_NetworkTest.unity";
    public const string ScenePath = "Assets/_Project/Scenes/Phase0D_TestScene.unity";


    public static void Run()
    {
        EnsureFolder("Assets/_Project/Gameplay/Quests", "Data");
        EnsureFolder("Assets/_Project/Gameplay/World", "Prefabs");

        BuildQuestData();
        BuildMonsterPrefab();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        WireQuestComponentsOnPlayerPrefab();
        AssetDatabase.SaveAssets();

        // Scene last: BuildScene opens a new scene and only THEN loads prefabs by
        // path (Phase 0-A pitfall — references obtained before a scene switch can
        // silently go stale).
        BuildScene();
        RegisterScenesInBuildSettings();

        // Phase 0-E components (lock-on, hit feedback, touch controls, HUD) on the
        // freshly built prefab/scene.
        Phase0ESetup.Run();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("[Phase0DSetup] Quest data, test monster, Player quest components and Phase0D_TestScene built.");
    }

    private static void EnsureFolder(string parent, string name)
    {
        var parts = parent.Split('/');
        var path = parts[0];
        for (var i = 1; i < parts.Length; i++)
        {
            var next = path + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next)) AssetDatabase.CreateFolder(path, parts[i]);
            path = next;
        }
        if (!AssetDatabase.IsValidFolder(parent + "/" + name)) AssetDatabase.CreateFolder(parent, name);
    }

    // ---------------- Quest data ----------------

    /// <summary>Quest values live in Assets/_Project/Config/Tables/quests.csv
    /// (Q_PHASE0D_001 / Q_PHASE0D_002); this just imports that table.</summary>
    private static void BuildQuestData()
    {
        var errors = new List<string>();
        ConfigTableImporter.ImportQuests(errors);
        ConfigTableImporter.Finish(errors, "quests.csv");
    }

    // ---------------- Monster prefab ----------------

    private static void BuildMonsterPrefab()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        go.name = "Phase0D_TestMonster";

        var shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader != null)
        {
            var mat = AssetDatabase.LoadAssetAtPath<Material>(MonsterMaterialPath);
            if (mat == null)
            {
                mat = new Material(shader);
                AssetDatabase.CreateAsset(mat, MonsterMaterialPath);
            }
            mat.color = new Color(0.75f, 0.2f, 0.2f);
            EditorUtility.SetDirty(mat);
            go.GetComponent<Renderer>().sharedMaterial = mat;
        }

        go.AddComponent<NetworkObject>();
        go.AddComponent<NetworkTransform>();
        go.AddComponent<Health>(); // MaxHp comes from monsters.csv (ImportMonsters below)
        var identity = go.AddComponent<EnemyIdentity>();
        go.AddComponent<MonsterLifecycle>();
        go.AddComponent<JiuyaoTianxu.Combat.Framework.BurnStatus>(); // 赤炎 can burn monsters (tech review D1)

        var idSo = new SerializedObject(identity);
        idSo.FindProperty("_targetId").stringValue = QuestIds.Phase0DTestMonsterTargetId;
        idSo.ApplyModifiedPropertiesWithoutUndo();

        var saved = PrefabUtility.SaveAsPrefabAsset(go, MonsterPrefabPath);
        Object.DestroyImmediate(go);
        if (saved == null) Debug.LogError("[Phase0DSetup] SaveAsPrefabAsset returned null for the test monster.");

        // Stats (MaxHp 30, DespawnDelay 0.5 — 可調整) live in Config/Tables/monsters.csv.
        AssetDatabase.Refresh(); // make sure FindAssets sees the prefab just saved
        var errors = new List<string>();
        ConfigTableImporter.ImportMonsters(errors);
        ConfigTableImporter.Finish(errors, "monsters.csv");
    }

    // ---------------- Player prefab ----------------

    private static void WireQuestComponentsOnPlayerPrefab()
    {
        var root = PrefabUtility.LoadPrefabContents(PlayerPrefabPath);
        if (root == null)
        {
            Debug.LogError("[Phase0DSetup] Player prefab not found — run Phase0ANetworkSetup.Run first.");
            return;
        }

        if (root.GetComponent<PlayerQuestLog>() == null) root.AddComponent<PlayerQuestLog>();
        var tracker = root.GetComponent<QuestTracker>();
        if (tracker == null) tracker = root.AddComponent<QuestTracker>();

        var registry = AssetDatabase.LoadAssetAtPath<QuestRegistry>(RegistryPath);
        var so = new SerializedObject(tracker);
        so.FindProperty("_registry").objectReferenceValue = registry;
        so.ApplyModifiedPropertiesWithoutUndo();

        PrefabUtility.SaveAsPrefabAsset(root, PlayerPrefabPath);
        PrefabUtility.UnloadPrefabContents(root);
        Debug.Log($"[Phase0DSetup] PlayerQuestLog + QuestTracker on Player prefab, registry {(registry != null ? "OK" : "MISSING")}.");
    }

    // ---------------- Scene ----------------

    private static void BuildScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(3f, 1f, 3f);

        var light = new GameObject("Directional Light");
        light.AddComponent<Light>().type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        var camera = new GameObject("Main Camera");
        camera.AddComponent<Camera>();
        camera.AddComponent<AudioListener>();
        camera.tag = "MainCamera";
        camera.transform.position = new Vector3(0f, 7f, -7f);
        camera.transform.rotation = Quaternion.Euler(45f, 0f, 0f);

        // Layout (可調整): two players face each other across the monster lane so
        // the -autotest attack pattern (always attacks straight ahead) lands on
        // monsters with every weapon type.
        MakeMarker<PlayerSpawnPoint>("PlayerSpawn_0", 0, new Vector3(-1.5f, 1f, 0f), Vector3.right);
        MakeMarker<PlayerSpawnPoint>("PlayerSpawn_1", 1, new Vector3(1.5f, 1f, 0f), Vector3.left);
        MakeMarker<MonsterSpawnPoint>("MonsterSpawn_0", 0, new Vector3(0f, 1f, 0f), Vector3.back);
        MakeMarker<MonsterSpawnPoint>("MonsterSpawn_1", 1, new Vector3(0f, 1f, 1.2f), Vector3.back);
        MakeMarker<MonsterSpawnPoint>("MonsterSpawn_2", 2, new Vector3(0f, 1f, -1.2f), Vector3.back);

        var monsterPrefab = AssetDatabase.LoadAssetAtPath<NetworkObject>(MonsterPrefabPath);
        var playerPrefab = AssetDatabase.LoadAssetAtPath<NetworkObject>(PlayerPrefabPath);
        var runnerPrefab = AssetDatabase.LoadAssetAtPath<NetworkRunner>(RunnerPrefabPath);
        if (monsterPrefab == null || playerPrefab == null || runnerPrefab == null)
        {
            Debug.LogError($"[Phase0DSetup] Prefab load failed: monster={monsterPrefab != null}, " +
                           $"player={playerPrefab != null}, runner={runnerPrefab != null}");
        }

        var spawner = new GameObject("MonsterSpawner").AddComponent<MonsterSpawner>();
        var spawnerSo = new SerializedObject(spawner);
        spawnerSo.FindProperty("_monsterPrefab").objectReferenceValue = monsterPrefab;
        spawnerSo.ApplyModifiedPropertiesWithoutUndo();

        var launcher = new GameObject("NetworkGameLauncher").AddComponent<NetworkGameLauncher>();
        var launcherSo = new SerializedObject(launcher);
        launcherSo.FindProperty("_runnerPrefab").objectReferenceValue = runnerPrefab;
        launcherSo.FindProperty("_playerPrefab").objectReferenceValue = playerPrefab;
        launcherSo.FindProperty("_sessionName").stringValue = "Phase0DTest";
        launcherSo.ApplyModifiedPropertiesWithoutUndo();

        new GameObject("Phase0DTestRunner").AddComponent<Phase0DTestRunner>();

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        Debug.Log("[Phase0DSetup] Scene saved at " + ScenePath);
    }

    private static void MakeMarker<T>(string name, int index, Vector3 position, Vector3 forward) where T : MonoBehaviour
    {
        var go = new GameObject(name);
        go.transform.SetPositionAndRotation(position, Quaternion.LookRotation(forward));
        var marker = go.AddComponent<T>();
        switch (marker)
        {
            case PlayerSpawnPoint p: p.Index = index; break;
            case MonsterSpawnPoint m: m.Index = index; break;
        }
    }

    /// <summary>Lets Editor Play resolve a real build index for either test scene
    /// (NetworkGameLauncher builds its SceneRef from the active scene's index).
    /// Standalone builds still pass their scene list explicitly.</summary>
    private static void RegisterScenesInBuildSettings()
    {
        EditorBuildSettings.scenes = new[]
        {
            new EditorBuildSettingsScene(Phase0AScenePath, true),
            new EditorBuildSettingsScene(ScenePath, true),
        };
    }
}
