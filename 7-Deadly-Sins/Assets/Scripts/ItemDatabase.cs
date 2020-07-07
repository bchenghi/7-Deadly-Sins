using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    #region Singleton
    public static ItemDatabase instance;

    void Awake() 
    {
        if (ItemDatabase.instance == null) 
        {
            instance = this;
        } 
        else 
        {
            Destroy(this.gameObject);
        }
            
        SortItemDatabase();
    }
    #endregion


    [SerializeField]
    public List<Item> allItems = new List<Item>();
    [SerializeField]
    string path;
    public bool sorted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SortItemDatabase() {
        ItemComparator itemComparator = new ItemComparator();
        allItems.Sort(0, allItems.Count, itemComparator);
        sorted = true;
    }
}

class ItemComparator : IComparer<Item> 
{   
    public int Compare(Item thisItem, Item thatItem) {
        Debug.Log("compare called");
        if (thisItem is Consumables && thatItem is Equipment) {
            return -1;
        }
        else if (thisItem is Equipment && thatItem is Consumables) 
        {
            return 1;
        }
        else if (thisItem is Equipment && thatItem is Equipment)
        {
            int result = 0;
            result += ((Equipment) thisItem).GetPrice();
            result -= ((Equipment) thatItem).GetPrice();
            return result;
        }
        else if (thisItem is Consumables && thatItem is Consumables) 
        {
            int result = 0;
            result += ((Consumables) thisItem).GetPrice();
            result -= ((Consumables) thatItem).GetPrice();
            return result;
        } 
        else 
        {
            return thisItem.GetPrice() - thatItem.GetPrice();
        }
    }
}
