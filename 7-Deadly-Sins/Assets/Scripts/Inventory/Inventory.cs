using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of inventory found");
            return;
        }

        instance = this;
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    // List of key value pairs where keys are the items, and values are the number of the item in the slot.
    public List<KeyValuePair<Item, int>> items = new List<KeyValuePair<Item, int>>();
    public int space = 20;

    public int consumablesPerSlot = 4;

    public bool Add(Item item)
    {
        bool successful = true;
        if (!item.isDefaultItem)
        {

            if (item is Consumables)
            {
                successful = AddConsumable(item);
            }
            else
            {
                successful = AddNormalItem(item);
            }

            if (onItemChangedCallback != null && successful)
                onItemChangedCallback.Invoke();
        }
        return successful;
    }

    public void Remove(Item item)
    {
        if (item is Consumables)
        {
            RemoveConsumable(item);
        }
        else
        {
            RemoveNormalItem(item);
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool AddNormalItem(Item item)
    {
        if (SlotsUsed() < space)
        {
            items.Add(new KeyValuePair<Item, int>(item, 1));
            return true;
        }
        else
        {
            Debug.Log("Inventory: Not enough space " + SlotsUsed());
            return false;
        }

    }

    public void RemoveNormalItem(Item item)
    {
        int index = IndexOfItem(item);
        items.RemoveAt(index);
    }


    public bool AddConsumable(Item consumable)
    {

        int indexFound = IndexToAddConsumable(consumable);
        bool hasSlotToAddConsumable = (indexFound == -1 ? false : true);


        if (hasSlotToAddConsumable)
        {
            int currentNumOfConsumablesInSlot = items[indexFound].Value;
            items[indexFound] = new KeyValuePair<Item, int>(consumable, currentNumOfConsumablesInSlot + 1);
        }
        else
        {
            if (SlotsUsed() < space)
            {
                items.Add(new KeyValuePair<Item, int>(consumable, 1));
            }

            else
            {
                Debug.Log("Inventory: Not enough space");
                return false;
            }

        } return true;
    }


    public void RemoveConsumable(Item consumable)
    {
        int indexFound = IndexOfLastConsumable(consumable);
        bool containsConsumable = (indexFound == -1 ? false : true);

        if (containsConsumable)
        {
            if (items[indexFound].Value == 1)
            {
                items.RemoveAt(indexFound);
            }
            else
            {
                int currentNumOfConsumables = items[indexFound].Value;
                items[indexFound] = new KeyValuePair<Item, int>(consumable, currentNumOfConsumables - 1);
            }
        }
        else
        {
            Debug.Log("Inventory: No such consumable in inventory to remove");
        }
    }


    // Returns index of item. -1 if item is not in list
    public int IndexOfItem(Item item)
    {
        int index = -1;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key.Equals(item))
            {
                index = i;
                break;
            }
        }
        return index;
    }

    // Finds index of the slot, of the given item furthest down the list, -1 if consumable doesnt exist
    public int IndexOfLastConsumable(Item item)
    {
        int index = -1;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == item && items[i].Value <= consumablesPerSlot)
            {
                index = i;

                if (items[i].Value < consumablesPerSlot)
                    break;
            }
        }
        return index;
    }

    // Returns the index of an existing slot to add the consumable, -1 if there are no slots to add the consumable
    public int IndexToAddConsumable(Item item)
    {
        int index = -1;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == item && items[i].Value < consumablesPerSlot)
            {
                index = i;
                break;
            }
        }
        return index;
    }
    
    // Returns number of slots used
    public int SlotsUsed()
    {
        return items.Count;
    }

    public int getValue(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == item)
            {
                return items[i].Value;

            }
        }
        return -1;
    }
}

