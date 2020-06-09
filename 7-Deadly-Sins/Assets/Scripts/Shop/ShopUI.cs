using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    List<Item> allItems;
    [SerializeField]
    GameObject shopSlotParent;
    ShopSlot[] shopSlots;

    void Awake() {
        allItems =  ItemDatabase.instance.allItems;
        shopSlots = shopSlotParent.GetComponentsInChildren<ShopSlot>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI() {
        ResetUI();
        for (int i = 0; i < shopSlots.Length; i++) {
            if (i < allItems.Count) {
                shopSlots[i].AddItem(allItems[i]);
            } 
            else 
            {
                shopSlots[i].ClearSlot();
            }
        }
    }

    public void UpdateUIConsumable() {
        ResetUI();
        int itemDatabaseIndex = 0;
        for (int i = 0; i < shopSlots.Length; i++) {
            if (itemDatabaseIndex < allItems.Count && allItems[itemDatabaseIndex] is Consumables)
            {
                shopSlots[i].AddItem(allItems[itemDatabaseIndex]);
                itemDatabaseIndex++;
            } 
            else if (itemDatabaseIndex < allItems.Count) 
            {
                i--;
                itemDatabaseIndex++;
            } 
            else {
                shopSlots[i].ClearSlot();
            }
        }
    }

    public void UpdateUIEquipment() {
        ResetUI();
        int itemDatabaseIndex = 0;
        for (int i = 0; i < shopSlots.Length; i++) {
            if (itemDatabaseIndex < allItems.Count && allItems[itemDatabaseIndex] is Equipment)
            {
                shopSlots[i].AddItem(allItems[itemDatabaseIndex]);
                itemDatabaseIndex++;
            } 
            else if (itemDatabaseIndex < allItems.Count) 
            {
                i--;
                itemDatabaseIndex++;
            } 
            else {
                shopSlots[i].ClearSlot();
            }
        }
    }

    public void UpdateUIOthers() {
        ResetUI();
        int itemDatabaseIndex = 0;
        for (int i = 0; i < shopSlots.Length; i++) {
            if (itemDatabaseIndex < allItems.Count && !((allItems[itemDatabaseIndex] is Equipment) ||
            (allItems[itemDatabaseIndex] is Consumables)))
            {
                shopSlots[i].AddItem(allItems[itemDatabaseIndex]);
                itemDatabaseIndex++;
            } 
            else if (itemDatabaseIndex < allItems.Count) 
            {
                i--;
                itemDatabaseIndex++;
            } 
            else {
                shopSlots[i].ClearSlot();
            }
        }
    }

    void ResetUI() {
        for (int i = 0; i < shopSlots.Length; i++) {
            shopSlots[i].ClearSlot();
        }
    }
}
