using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;
    InventorySlot[] slots; 

    void Awake() {
 
    }

    // Start is called before the first frame update
    void Start()
    {
       inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        NewSceneSetUp();
    }



    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                if (inventory.items[i].Key is Others)
                {
                    slots[i].AddOthers(inventory.items[i].Key, inventory.items[i].Value);
                }
                else if (inventory.items[i].Key is Consumables)
                {
                    slots[i].AddConsumable(inventory.items[i].Key, inventory.items[i].Value);
                }
                else
                {
                    slots[i].AddItem(inventory.items[i].Key);
                }
            }
            else
            {
                slots[i].ClearSlotCompletely();
            }
        }
    }

    // Updates ui for inventory, used when entering new scene, called in Start.
    void NewSceneSetUp() {
        UpdateUI();
    }

}
