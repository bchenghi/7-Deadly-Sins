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
            Item clone = Object.Instantiate(item) as Item;
            Inventory.instance.Add(clone);
        }
    }
}
