using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : UISlot
{
    public Image icon;
    public Button removeButton;
    public Item item { get; private set; }

    public int count = 0;
    public int maxCount = Inventory.instance.consumablesPerSlot;

    public void AddItem(Item newItem)
    {
        AddNormalItem(newItem);
    }

    public void AddConsumable(Item consumable, int num)
    {
        item = consumable;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        count = num;
    }


    public void ClearSlotCompletely()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }


    public void ClearSlot()
    {
        if (item is Consumable)
        {
            ClearConsumable();
        }
        else
        {
            ClearNormalItem();
        }
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void AddNormalItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }


    public void ClearNormalItem()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void ClearConsumable()
    {
        if (count == 0)
        {
            Debug.Log("InventorySlot: consumable count in this slot is 0");
        }
        else if (count == 1)
        {
            item = null;

            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
            count--;
        }
        else
        {
            count--;
        }
    }
}
