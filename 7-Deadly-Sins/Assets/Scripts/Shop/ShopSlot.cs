using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : UISlot
{

    public Item item {get; private set;}
    public Image icon;

    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void Buy() {
        Debug.Log("buy called");
        int price = item.GetPrice();
        if (GoldCounter.instance.Spend(price)) {
            DisplayTextManager.instance.Display("Bought " + item.name + "!", 2f);
            Item clone = Object.Instantiate(item) as Item;
            Inventory.instance.Add(clone);
        }
    }

    
    // Will first search through equipment manager and upgrade equipment if found, otherwise will search through
    // Inventory and upgrade equipment if found and return true. If cant be found or not upgraded, return false;
    bool SearchAndUpgrade(Equipment equipment) {
        if (EquipmentManager.instance.IsEquipped(equipment)) {
            if (EquipmentManager.instance.ReferenceToEquipment(equipment).CanUpgrade()) {
                EquipmentManager.instance.ReferenceToEquipment(equipment).Upgrade();
                return true;
            }
        } else if (Inventory.instance.Exists(equipment)) {
            if (Inventory.instance.UpgradeEquipment(equipment)) {
                return true;
            }
        } 
        return false;
    }
    
}
