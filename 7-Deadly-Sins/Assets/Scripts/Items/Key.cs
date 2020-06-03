using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Inventory/Consumable/Key")]
public class Key : Consumables
{
    Inventory inventory;

    public override void Use()
    {
        base.Use();
        inventory = Inventory.instance;
        //Remove from the chestUI Inventory

        inventory.Add(this); // add in to the main inventory
        
    }
}
