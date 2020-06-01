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
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough space");
                return false;
            }

            if (item is Consumable)
            {
                AddConsumable(item);
            }
            else
            {
                AddNormalItem(item);
            }

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        if (item is Consumable)
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

    public void AddNormalItem(Item item)
    {
        items.Add(new KeyValuePair<Item, int>(item, 1));
    }

    public void RemoveNormalItem(Item item)
    {
        int index = IndexOfItem(item);
        items.RemoveAt(index);
    }


    public void AddConsumable(Item consumable)
    {

        int indexFound = IndexOfConsumable(consumable);
        bool containsConsumable = (indexFound == -1 ? false : true);


        if (containsConsumable)
        {
            int currentNumOfConsumables = items[indexFound].Value;
            items[indexFound] = new KeyValuePair<Item, int>(consumable, currentNumOfConsumables + 1);
        }
        else
        {
            items.Add(new KeyValuePair<Item, int>(consumable, 1));
        }
    }


    public void RemoveConsumable(Item consumable)
    {
        int indexFound = IndexOfConsumable(consumable);
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

    // Finds index of the slot, of the given item furthest down the list
    public int IndexOfConsumable(Item item)
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
}

