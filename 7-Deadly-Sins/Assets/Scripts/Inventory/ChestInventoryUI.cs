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

    }

    public void UnDisplay()
    {
        inventoryChestUI.SetActive(false);
    }

    void UpdateUI()
    {
        
        for (int i = 0; i < slots.Length; i++)
        {
            
            if (i < chestInventory.items.Count)
            {
               
                if (chestInventory.items[i].Key is Consumables)
                {
                    
                    slots[i].AddConsumable(chestInventory.items[i].Key, chestInventory.items[i].Value);
                    
                }
                else
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
