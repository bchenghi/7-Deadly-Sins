using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryChestUI;

    
    ChestSlots[] slots;
    ChestInventory chestInventory;
    Chest chest;

    [HideInInspector]
    public bool displayOn = false;


    private void Start()
    {
        chestInventory = GetComponent<ChestInventory>();
        chest = GetComponent<Chest>();
        chest.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<ChestSlots>();
        
    }


    public void DisplayUI()
    {
        inventoryChestUI.SetActive(true);
        displayOn = true;

    }

    public void UnDisplay()
    {
        inventoryChestUI.SetActive(false);
        displayOn = false;
    }

    void UpdateUI()
    {
        foreach(ChestSlots slot in slots) {
            slot.currentChestInventory = chestInventory;
        }
        
        for (int i = 0; i < slots.Length; i++)
        {
            
            if (i < chestInventory.items.Count)
            {
               
                if (chestInventory.items[i].Key is Consumables)
                {
                    
                    slots[i].AddConsumable(chestInventory.items[i].Key, chestInventory.items[i].Value);
                    
                }
                else if (chestInventory.items[i].Key is Others) 
                {
                    slots[i].AddOthers(chestInventory.items[i].Key, chestInventory.items[i].Value);

                } else
                {
                    slots[i].AddItem(chestInventory.items[i].Key);
                }
            }
            else
            {
                slots[i].ClearSlotCompletely();
            }
        }
    }

    public void ClearChest()
    {
        for (int i = 0; i< slots.Length; i++)
        {
            slots[i].ClearSlotCompletely();
        }
    }

}
