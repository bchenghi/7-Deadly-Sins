using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{

    [System.Serializable]
    public struct ItemsAndQuantity
    {
        public Item item;
        public int quantity;
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;



    public List<KeyValuePair<Item,int>> items = new List<KeyValuePair<Item,int>>();

    public ItemsAndQuantity[] itemsToBeAdded;

    private int space = 8;

    private int consumablesPerSlot = 4;

    private void Start()
    {
        

        for (int i = 0; i < itemsToBeAdded.Length; i++)
        {
            if (itemsToBeAdded[i].item is Consumables)
            {

                items.Add(new KeyValuePair<Item, int>(itemsToBeAdded[i].item, itemsToBeAdded[i].quantity));
            } else if (itemsToBeAdded[i].item is Others)
            {
                var othersObj = itemsToBeAdded[i].item as Others;
                items.Add(new KeyValuePair<Item, int>(itemsToBeAdded[i].item, othersObj.quantity));
            } else
            {
                items.Add(new KeyValuePair<Item, int>(itemsToBeAdded[i].item, itemsToBeAdded[i].quantity));
            }
           
        }

        //if (onItemChangedCallback != null)
           //onItemChangedCallback.Invoke();

    }

    public void AddToChest(Item item)
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
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

        }
        
    }

    public void RemoveFromChest(Item item)
    {
        if (item is Consumables)
        {
            RemoveConsumable(item);
        }
        else
        {
            RemoveNormalItem(item);
        }

        
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

        }
        return true;
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
            if (items[i].Key != null) {
                
                if (items[i].Key.name == item.name)
                {
                    index = i;
                    break;
                }
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





}
