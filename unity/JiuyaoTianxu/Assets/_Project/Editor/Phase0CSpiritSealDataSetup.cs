using JiuyaoTianxu.Combat.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Builds the three Phase 0-C prototype Spirit Seal assets (HANDOFF-007 §2) plus
/// the registry that resolves a networked int SealId back to its asset.
/// Run via: Unity.exe -batchmode -executeMethod Phase0CSpiritSealDataSetup.Run -quit
/// </summary>
public static class Phase0CSpiritSealDataSetup
{
    private const string Root = "Assets/_Project/Combat/SpiritSeals";

    public static void Run()
    {
        if (!AssetDatabase.IsValidFolder(Root))
        {
            AssetDatabase.CreateFolder("Assets/_Project/Combat", "SpiritSeals");
        }

        var blaze = MakeSeal(SpiritSealIds.Blaze, "赤炎", SpiritSealTriggerType.OnAttackHit, cooldown: 3f,
            burnDamage: 3, burnTicks: 3, burnInterval: 1f);
        var guard = MakeSeal(SpiritSealIds.Guard, "玄甲", SpiritSealTriggerType.OnFatalDamage, cooldown: 10f,
            fatalSaveMinHp: 1);
        var shadow = MakeSeal(SpiritSealIds.Shadow, "影遁", SpiritSealTriggerType.OnDodgeEvent, cooldown: 5f,
            armsForNextAttack: true, bonusDamage: 5);

        var registry = ScriptableObject.CreateInstance<SpiritSealRegistry>();
        registry.All = new[] { blaze, guard, shadow };
        AssetDatabase.CreateAsset(registry, $"{Root}/SpiritSealRegistry.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("[Phase0CSpiritSealDataSetup] Three prototype Spirit Seals + registry built.");
    }

    private static SpiritSealDefinition MakeSeal(int id, string displayName, SpiritSealTriggerType trigger,
        float cooldown, int burnDamage = 0, int burnTicks = 0, float burnInterval = 0f,
        int fatalSaveMinHp = 1, bool armsForNextAttack = false, int bonusDamage = 0)
    {
        var s = ScriptableObject.CreateInstance<SpiritSealDefinition>();
        s.SealId = id;
        s.DisplayName = displayName;
        s.Equipable = true;
        s.TriggerType = trigger;
        s.Cooldown = cooldown;
        s.BurnDamagePerTick = burnDamage;
        s.BurnTickCount = burnTicks;
        s.BurnTickInterval = burnInterval;
        s.FatalSaveMinHp = fatalSaveMinHp;
        s.ArmsForNextAttack = armsForNextAttack;
        s.BonusDamageWhenArmed = bonusDamage;

        AssetDatabase.CreateAsset(s, $"{Root}/{displayName}.asset");
        return s;
    }
}
