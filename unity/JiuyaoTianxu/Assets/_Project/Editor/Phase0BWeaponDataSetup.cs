using System.Collections.Generic;
using JiuyaoTianxu.Combat.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Builds the six WeaponDefinition/AttackDefinition data assets for Phase 0-B
/// (HANDOFF-006 §4). Numbers here are Phase 0-B "identifiability" placeholders,
/// not final 03_COMBAT_SYSTEM balance — the whole point is that they live in
/// data assets, not hardcoded in CombatController, so they're trivial to retune
/// without touching code.
/// Run via: Unity.exe -batchmode -executeMethod Phase0BWeaponDataSetup.Run -quit
/// </summary>
public static class Phase0BWeaponDataSetup
{
    private const string Root = "Assets/_Project/Combat/Weapons";

    public static void Run()
    {
        EnsureFolder("Assets/_Project/Combat", "Weapons");

        BuildBlade();
        BuildSword();
        BuildSpear();
        BuildBow();
        BuildHeavyBlade();
        BuildStaff();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("[Phase0BWeaponDataSetup] Six WeaponDefinition assets built.");
    }

    private static void EnsureFolder(string parent, string name)
    {
        if (!AssetDatabase.IsValidFolder(parent + "/" + name))
        {
            AssetDatabase.CreateFolder(parent, name);
        }
    }

    private static AttackDefinition MakeAttack(string id, WeaponType weapon, int comboStep, int damage,
        float startup, float active, float recovery, float comboWindow, bool canCombo,
        HitShapeType shape, float range, bool canMove = false, float moveMul = 0f,
        bool knockback = false, float knockbackForce = 0f, bool armorBreak = false, bool superArmor = false,
        float chargeOrCast = 0f, float projectileSpeed = 15f, float areaRadius = 0f)
    {
        var a = ScriptableObject.CreateInstance<AttackDefinition>();
        a.AttackId = id;
        a.WeaponType = weapon;
        a.ComboStep = comboStep;
        a.Damage = damage;
        a.StartupTime = startup;
        a.ActiveTime = active;
        a.RecoveryTime = recovery;
        a.ComboWindow = comboWindow;
        a.CanCombo = canCombo;
        a.HitShape = shape;
        a.Range = range;
        a.CanMoveDuringAttack = canMove;
        a.MoveSpeedMultiplier = moveMul;
        a.AppliesKnockback = knockback;
        a.KnockbackForce = knockbackForce;
        a.AppliesArmorBreak = armorBreak;
        a.GrantsSuperArmor = superArmor;
        a.ChargeOrCastTime = chargeOrCast;
        a.ProjectileSpeed = projectileSpeed;
        a.AreaRadius = areaRadius;

        var folder = $"{Root}/{weapon}";
        if (!AssetDatabase.IsValidFolder(folder))
        {
            AssetDatabase.CreateFolder(Root, weapon.ToString());
        }
        AssetDatabase.CreateAsset(a, $"{folder}/{id}.asset");
        return AssetDatabase.LoadAssetAtPath<AttackDefinition>($"{folder}/{id}.asset");
    }

    private static void SaveWeapon(WeaponType type, float baseSpeed, List<AttackDefinition> steps)
    {
        var w = ScriptableObject.CreateInstance<WeaponDefinition>();
        w.WeaponType = type;
        w.BaseMoveSpeed = baseSpeed;
        w.ComboSequence = steps.ToArray();
        AssetDatabase.CreateAsset(w, $"{Root}/{type}/{type}_Weapon.asset");
    }

    // 刀修：近戰爆發，3段連擊，末段重擊+擊退
    private static void BuildBlade()
    {
        var steps = new List<AttackDefinition>
        {
            MakeAttack("Blade_Step1", WeaponType.Blade, 1, 8, 0.08f, 0.12f, 0.18f, 0.35f, true, HitShapeType.Sphere, 1.5f),
            MakeAttack("Blade_Step2", WeaponType.Blade, 2, 10, 0.08f, 0.12f, 0.18f, 0.35f, true, HitShapeType.Sphere, 1.5f),
            MakeAttack("Blade_Step3_Heavy", WeaponType.Blade, 3, 22, 0.15f, 0.15f, 0.4f, 0.2f, false, HitShapeType.Sphere, 1.7f,
                knockback: true, knockbackForce: 4f),
        };
        SaveWeapon(WeaponType.Blade, 4f, steps);
    }

    // 劍修：高機動連擊，4~5段快速，短後搖，可邊移動
    private static void BuildSword()
    {
        var steps = new List<AttackDefinition>();
        for (var i = 1; i <= 5; i++)
        {
            steps.Add(MakeAttack($"Sword_Step{i}", WeaponType.Sword, i, 5, 0.05f, 0.07f, 0.08f, 0.5f, true,
                HitShapeType.Sphere, 1.3f, canMove: true, moveMul: 0.6f));
        }
        SaveWeapon(WeaponType.Sword, 4.5f, steps);
    }

    // 槍修：中距離突刺+破甲/擊退
    private static void BuildSpear()
    {
        var steps = new List<AttackDefinition>
        {
            MakeAttack("Spear_Thrust1", WeaponType.Spear, 1, 12, 0.12f, 0.1f, 0.25f, 0.3f, true,
                HitShapeType.Capsule, 3.5f, armorBreak: true),
            MakeAttack("Spear_Thrust2", WeaponType.Spear, 2, 14, 0.12f, 0.1f, 0.3f, 0.2f, false,
                HitShapeType.Capsule, 3.5f, knockback: true, knockbackForce: 2f, armorBreak: true),
        };
        SaveWeapon(WeaponType.Spear, 3.8f, steps);
    }

    // 弓：Hold -> Charge -> Release -> Projectile -> Hit
    private static void BuildBow()
    {
        var steps = new List<AttackDefinition>
        {
            MakeAttack("Bow_Shot", WeaponType.Bow, 1, 16, 0f, 0f, 0.3f, 0.1f, false,
                HitShapeType.Projectile, 12f, projectileSpeed: 22f),
        };
        SaveWeapon(WeaponType.Bow, 3.5f, steps);
    }

    // 重刃：慢、重、AOE、控場，具簡單霸體概念
    private static void BuildHeavyBlade()
    {
        var steps = new List<AttackDefinition>
        {
            MakeAttack("HeavyBlade_Swing", WeaponType.HeavyBlade, 1, 30, 0.45f, 0.2f, 0.55f, 0.1f, false,
                HitShapeType.Sphere, 2.8f, knockback: true, knockbackForce: 6f, superArmor: true),
        };
        SaveWeapon(WeaponType.HeavyBlade, 2.8f, steps);
    }

    // 靈杖：Cast -> Delay -> AOE -> Hit -> Control
    private static void BuildStaff()
    {
        var steps = new List<AttackDefinition>
        {
            MakeAttack("Staff_Cast", WeaponType.Staff, 1, 12, 0f, 0f, 0.35f, 0.1f, false,
                HitShapeType.Area, 2.5f, chargeOrCast: 0.6f, areaRadius: 2f),
        };
        SaveWeapon(WeaponType.Staff, 3.5f, steps);
    }
}
