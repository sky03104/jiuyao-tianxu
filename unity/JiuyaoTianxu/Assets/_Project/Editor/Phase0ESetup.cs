using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Combat.Targeting;
using JiuyaoTianxu.UI.Hud;
using JiuyaoTianxu.UI.TouchControls;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// Phase 0-E (roadmap Phase 0 gaps: 雙搖桿/目標鎖定/基礎回饋/資料表): idempotently
/// adds TargetLock + HealthFeedback + BurnStatus to the Player prefab,
/// HealthFeedback + BurnStatus to the Phase 0-D test monster, and the touch controls / debug HUD / camera follow to
/// both test scenes. Safe to re-run; skips anything that doesn't exist yet.
/// Phase0DSetup.Run calls this at the end, so a fresh 0-D setup is already 0-E.
/// Run via: Unity.exe -batchmode -projectPath . -executeMethod Phase0ESetup.Run -quit
/// </summary>
public static class Phase0ESetup
{
    private const string PlayerPrefabPath = "Assets/_Project/Net/Prefabs/Player.prefab";
    private const string MonsterPrefabPath = "Assets/_Project/Gameplay/World/Prefabs/Phase0D_TestMonster.prefab";
    private static readonly string[] Scenes =
    {
        "Assets/_Project/Scenes/Phase0A_NetworkTest.unity",
        "Assets/_Project/Scenes/Phase0D_TestScene.unity",
    };

    public static void Run()
    {
        EnsurePrefab(PlayerPrefabPath, root =>
        {
            AddIfMissing<TargetLock>(root);
            AddIfMissing<HealthFeedback>(root);
            AddIfMissing<BurnStatus>(root); // tech review D1
            AddIfMissing<JiuyaoTianxu.Gameplay.World.PlayerLifecycle>(root); // tech review D3 (test respawn)
        });
        EnsurePrefab(MonsterPrefabPath, root =>
        {
            AddIfMissing<HealthFeedback>(root);
            AddIfMissing<BurnStatus>(root); // tech review D1
        });

        foreach (var scene in Scenes) EnsureScene(scene);

        AssetDatabase.SaveAssets();
        Debug.Log("[Phase0ESetup] Twin-stick/lock-on/HUD components ensured on prefabs and test scenes.");
    }

    private static void EnsurePrefab(string path, System.Action<GameObject> edit)
    {
        if (AssetDatabase.LoadAssetAtPath<GameObject>(path) == null)
        {
            Debug.Log($"[Phase0ESetup] {path} not found yet; skipped.");
            return;
        }

        var root = PrefabUtility.LoadPrefabContents(path);
        edit(root);
        PrefabUtility.SaveAsPrefabAsset(root, path);
        PrefabUtility.UnloadPrefabContents(root);
    }

    private static void EnsureScene(string path)
    {
        if (AssetDatabase.LoadAssetAtPath<SceneAsset>(path) == null)
        {
            Debug.Log($"[Phase0ESetup] {path} not found yet; skipped.");
            return;
        }

        var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);

        if (Object.FindAnyObjectByType<VirtualControlsOverlay>() == null ||
            Object.FindAnyObjectByType<CombatHudOverlay>() == null)
        {
            var ui = GameObject.Find("Phase0E_UI") ?? new GameObject("Phase0E_UI");
            AddIfMissing<VirtualControlsOverlay>(ui);
            AddIfMissing<CombatHudOverlay>(ui);
        }

        var cam = Camera.main != null ? Camera.main.gameObject : GameObject.Find("Main Camera");
        if (cam != null) AddIfMissing<LocalPlayerCameraFollow>(cam);

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
    }

    private static void AddIfMissing<T>(GameObject go) where T : Component
    {
        if (go.GetComponent<T>() == null) go.AddComponent<T>();
    }
}
