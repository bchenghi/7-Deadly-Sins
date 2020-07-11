using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlots : UISlot
{
    public Image icon;
    public Item item { get; private set; }
    public Text consumableCounter;
    public int count = 0;
    public int maxCount = 0;

    [HideInInspector]
    public ChestInventory currentChestInventory;

    private void Start()
    {
        maxCount = Inventory.instance.consumablesPerSlot;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        UpdateConsumableCounter();
    }

    public void AddConsumable(Item consumable, int num)
    {
        item = consumable;
        icon.sprite = item.icon;
        icon.enabled = true;
        count = num;
        UpdateConsumableCounter();
    }

    public void AddOthers(Item others, int num)
    {
        item = others;
        icon.sprite = item.icon;
        icon.enabled = true;
        count = num;
        UpdateOthersCounter();
    }

    public void ClearSlotCompletely()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        UpdateConsumableCounter();
    }


    public void ClearSlot()
    {
        if (item is Consumables)
        {
            ClearConsumable();
        }
        else
        {
            ClearNormalItem();
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            if (item.GetType() == typeof(Currency))
            {
                var currency = item as Currency;
                
                GoldCounter.instance.Earn(currency.GetPickUpAmount());
            } else
            {
                Inventory.instance.Add(item);
                
            }
            
            currentChestInventory.RemoveFromChest(item);
            ClearSlot();     
        }
    }

    public void ClearNormalItem()
    {
        ClearSlotCompletely();
    }

    public void ClearConsumable()
    {
        if (count == 0)
        {
            Debug.Log("InventorySlot: Could not clear slot as consumable count in this slot is 0");
        }
        else if (count == 1)
        {
            item = null;

            icon.sprite = null;
            icon.enabled = false;
            count--;
        }
        else
        {
            count--;
        }
        UpdateConsumableCounter();
    }

    public void UpdateConsumableCounter()
    {
        if (item is Consumables && count >= 1)
        {
            consumableCounter.text = count.ToString();
        }
        else
        {
            consumableCounter.text = null;
        }
    }

    public void UpdateOthersCounter()
    {
        if (item is Others && count >= 1)
        {
            consumableCounter.text = count.ToString();
        } else
        {
            consumableCounter.text = null;
        }
    }
}
