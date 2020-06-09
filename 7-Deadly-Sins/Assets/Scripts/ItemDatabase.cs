using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    #region Singleton
    public static ItemDatabase instance;

    void Awake() 
    {
        instance = this;
    }
    #endregion


    [SerializeField]
    public List<Item> allItems = new List<Item>();
    [SerializeField]
    string path;
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
