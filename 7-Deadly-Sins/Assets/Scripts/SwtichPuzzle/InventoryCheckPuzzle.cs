using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryCheckPuzzle : MonoBehaviour
{
    public Transform CluesCounter;
    public bool hasAllClues;
    public Item Clues;
    private int Count;

    private void Start()
    {
        Count = 0;
    }

    private void Update()
    {
        CluesCounters();
        CluesCounter.GetComponent<TextMeshProUGUI>().text = Count + "/15 Clues Found";
        if (Inventory.instance.getValue(Clues) == 15)
        {
            CluesCounter.GetComponent<TextMeshProUGUI>().text = "All Clues Found, Return back to Switches";
            hasAllClues = true;
        }
    }

    public void RemovePuzzles()
    {
        Inventory.instance.Remove(Clues, 15);
    }

    private void CluesCounters()
    {
        if (Inventory.instance.getValue(Clues) == -1)
        {
            Count = 0;
        }
        else
        {
            Count = Inventory.instance.getValue(Clues);
        }
    }

   
    

}
