using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventorySlot : UISlot
{
    public Image icon;
    public Button removeButton;
    public Item item { get; private set; }
    public Text consumableCounter;
    public int count = 0;
    public int maxCount = 0;

    private void Start()
    {
        maxCount = Inventory.instance.consumablesPerSlot;
    }

    void Update() {
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.enabled = true;
        icon.sprite = item.icon;
        removeButton.interactable = true;
        UpdateConsumableCounter();
    }

    public void AddConsumable(Item consumable, int num)
    {
        item = consumable;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        count = num;
        UpdateConsumableCounter();
    }


    public void ClearSlotCompletely()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
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

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
        HotKeyBar.instance.RefreshHotkeys();
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
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
            removeButton.interactable = false;
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
}
