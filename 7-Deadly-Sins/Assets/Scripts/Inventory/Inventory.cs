using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null) {
            Debug.Log("more than one instance of inventory found");
            return;
        }

        instance = this;
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();

    public int space = 20;

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough space");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}

