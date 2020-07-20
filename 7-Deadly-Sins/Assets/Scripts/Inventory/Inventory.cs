using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;

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
    public int othersPerSlot = 100;

    public bool Add(Item item)
    {
        bool successful = true;
        if (!item.isDefaultItem)
        {
            if (item is Others)
            {
                successful = AddOthers(item);
            }
            else if (item is Consumables)
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


    // Remove item by name
    public void Remove(string name) {
        for (int i = items.Count - 1; i >= 0; i--) {
            if (name == items[i].Key.name) {
                Remove(items[i].Key);
            }
        }
    }

    public bool Remove(Item item, int quantity)
    {
        bool result = false;
        if (item is Others)
        {
            result = RemoveOthers(item, quantity);
        }
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return result;
    }

    public bool AddNormalItem(Item item)
    {
        // Will search for item in equipment and inventory and upgrade it instead of assigning a slot
        if (item is Equipment && SearchAndUpgrade((Equipment) item)) {
            return true;

        } 

        if (SlotsUsed() < space)
        {
            items.Add(new KeyValuePair<Item, int>(item, 1));
            return true;
        }
        else
        {
            DisplayTextManager.instance.Display("Inventory: Not enough space!", 3f);
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
                DisplayTextManager.instance.Display("Inventory: Not enough space!", 3f);
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


    public bool AddOthers(Item others)
    {
        var othersItem = others as Others;
        int indexFound = IndexToAddOthers(others);
        bool hasSlotToAddOthers = (indexFound == -1 ? false : true);


        if (hasSlotToAddOthers)
        {
            int currentNumOfOthersInSlot = items[indexFound].Value;
           
            items[indexFound] = new KeyValuePair<Item, int>(others, currentNumOfOthersInSlot + othersItem.quantity);
        }
        else
        {
            if (SlotsUsed() < space)
            {
                items.Add(new KeyValuePair<Item, int>(others, othersItem.quantity));
            }

            else
            {
                DisplayTextManager.instance.Display("Inventory: Not enough space!", 3f);
                Debug.Log("Inventory: Not enough space");
                return false;
            }

        }
        return true;
    }


    // returns true if the given quantity of items are removed successfully, else false,
    // and no removal is made
    public bool RemoveOthers(Item others, int quantity)
    {
        /*
        var othersItem = others as Others;
        int indexFound = IndexOfLastOthers(others);
        bool containsOthers = (indexFound == -1 ? false : true);

        if (containsOthers)
        {
            if (items[indexFound].Value <= quantity)
            {
                items.RemoveAt(indexFound);
            }
            else
            {
                int currentNumOfOthers = items[indexFound].Value;
                items[indexFound] = new KeyValuePair<Item, int>(others, currentNumOfOthers - quantity);
            }
        }
        else
        {
            Debug.Log("Inventory: No such others in inventory to remove");
        }
        */
        bool numberOfItemsExists = Exists(others, quantity);
        Debug.Log(others.name + "quantity: " + quantity);
        if (numberOfItemsExists) {
            int numberLeftToRemove = quantity;
            for (int i = items.Count - 1; i >= 0 && numberLeftToRemove > 0; i--) {
                if (items[i].Key.name == others.name) {
                    if (items[i].Value > numberLeftToRemove) {
                        Debug.Log("items[i].Value > numberLeftToRemove");
                        items[i] = new KeyValuePair<Item, int>(others, items[i].Value - numberLeftToRemove);
                        numberLeftToRemove = 0;
                        Debug.Log("new value " + items[i].Value);
                    } else {
                        Debug.Log("items[i].Value <= numberLeftToRemove");
                        numberLeftToRemove -= items[i].Value;
                        items.RemoveAt(i);
                    }
                }
            }
            return true;
        } else {
            return false;
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

    public int IndexOfLastOthers(Item item)
    {
        int index = -1;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key.name == item.name && items[i].Value <= othersPerSlot)
            {
                index = i;

                if (items[i].Value < othersPerSlot)
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
            if (items[i].Key.name == item.name && items[i].Value <= consumablesPerSlot)
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
            if (items[i].Key.name == item.name && items[i].Value < consumablesPerSlot)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    public int IndexToAddOthers(Item item)
    {
        int index = -1;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key.name == item.name && items[i].Value < othersPerSlot)
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
        int totalValue = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key.name == item.name)
            {
                totalValue += items[i].Value;

            }
        }
        return totalValue;
    }

    // Returns true if at least one of the item exists in inventory
    public bool Exists(Item item) {
        bool result = false;
        foreach(KeyValuePair<Item, int> pair in items) {
            if (pair.Key.name == item.name) {
                result = true;
                break;
            }
        }
        return result;
    }

    // Returns true if there are at least the given number of the item in the inventory
    public bool Exists(Item item, int numberOfTheItem) {
        if (numberOfTheItem == 0) {
            return true;
        } else {
            bool result = false;
            int numberLeftToFind = numberOfTheItem;
            foreach(KeyValuePair<Item, int> pair in items) {
                Debug.Log("pair.key.name: "+pair.Key.name);
                Debug.Log("item.name: "+item.name);
                if (pair.Key.name == item.name) {
                    numberLeftToFind -= pair.Value;
                }
                if (numberLeftToFind <= 0) {
                    result = true;
                    break;
                }
            }
        return result;
        }
    }


    // If reference to item in inventory is required
    public Item ReferenceToItemInInventory(Item item) {
        foreach (KeyValuePair<Item, int> pair in items) {
            if (pair.Key.name == item.name) {
                return pair.Key;
            }
        }
        return null;
    }

    // Will loop through the items, will upgrade the first instance of equipment 
    // that was upgradable and return true, else returns false
    public bool UpgradeEquipment(Equipment newEquipment) {
        foreach (KeyValuePair<Item, int> pair in items) {
            if (pair.Key.name == (newEquipment.name)) {
                if (((Equipment) pair.Key).CanUpgradeUsing(newEquipment)) {
                    ((Equipment) pair.Key).UpgradeUsing(newEquipment);
                    return true;
                }
            }
        }
        return false;
    }

    // Will first search through equipment manager and upgrade equipment if found, otherwise will search through
    // Inventory and upgrade equipment if found and return true. If cant be found or not upgraded, return false;
    bool SearchAndUpgrade(Equipment equipment) {
        if (EquipmentManager.instance.IsEquipped(equipment)) {
            
            if (EquipmentManager.instance.ReferenceToEquipment(equipment).CanUpgradeUsing(equipment)) {
                //PlayerManager.instance.player.GetComponent<PlayerStats>().UpdateStatUI();
                int equipmentSlot = (int) EquipmentManager.instance.ReferenceToEquipment(equipment).equipmentSlot;
                Equipment equipmentToUpdate = EquipmentManager.instance.UnequipWithoutAddingToInventory(equipmentSlot);
                equipmentToUpdate.UpgradeUsing(equipment);
                EquipmentManager.instance.Equip(equipmentToUpdate);
                return true;
            } else
            {
                if (Inventory.instance.Exists(equipment))
                {
                    if (Inventory.instance.UpgradeEquipment(equipment))
                    {
                        return true;
                    }
                }
                return false;
            }
        } else if (Inventory.instance.Exists(equipment)) {
            //Debug.Log("exists in inventory");
            if (Inventory.instance.UpgradeEquipment(equipment)) {
                //Debug.Log("upgrade in inventory");
                return true;
            }
        } 
        return false;
    }
}

