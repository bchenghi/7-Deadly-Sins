using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;
    public int armorModifier;
    public int damageModifier;

    public int level = 0;
    int maxLevel = 2;

    float[] statsLevelIncreaseFactors = new float[] {1, 1.5f, 2};

    public override void Use()
    {
        base.Use();
        DisplayTextManager.instance.Display("Equipped " + name, 3f);
        // Equip item
        EquipmentManager.instance.Equip(this);
        // Remove item from inventory
        RemoveFromInventory();
    }

    public override int GetPrice(){
        return (int) (ArmorModifier() + DamageModifier()) * 10;
    }

    public void Upgrade() {
        if (level < maxLevel) {
            level++;
            DisplayTextManager.instance.Display("Upgraded " + name + " to Level " + (level + 1), 2f);
        }
        else
        {
            Debug.Log("max level for equipment reached");
        }
    }

    public int ArmorModifier() {
        return (int) Math.Floor(armorModifier * statsLevelIncreaseFactors[level]);
    }

    public int DamageModifier() {
        return (int) Math.Floor(damageModifier * statsLevelIncreaseFactors[level]);
    }

    public bool CanUpgrade() {
        //Debug.Log("level " + level + " maxLevel " + maxLevel);
        return level < maxLevel;
    }

    public override bool Equals(object other)
    {
        if (other is Equipment)
        {
            return this.name == ((Equipment)other).name && this.level == ((Equipment)other).level;
        }
        else
        {
            return false;
        }

    }

    public bool EqualsButNotCaringAboutLevels(object other)
    {
        if (other is Equipment)
        {
            return this.name == ((Equipment)other).name;
        } else
        {
            return false;
        }
    }

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}

public enum EquipmentMeshRegion { Legs, Arms, Torso}; // Corresponds to body blend shapes


