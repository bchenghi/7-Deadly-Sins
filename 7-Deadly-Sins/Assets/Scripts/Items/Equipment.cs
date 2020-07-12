using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armorModifier;
    public int damageModifier;

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
        return (int) (armorModifier + damageModifier) * 10;
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}

public enum EquipmentMeshRegion { Legs, Arms, Torso}; // Corresponds to body blend shapes
