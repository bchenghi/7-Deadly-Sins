using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCheckPuzzle : MonoBehaviour
{
    public bool hasAllClues;
    public Item Clues;

    private void Update()
    {
        if (Inventory.instance.getValue(Clues) == 15)
        {
            hasAllClues = true;
        }
    }

    public void RemovePuzzles()
    {
        Inventory.instance.Remove(Clues, 15);
    }
    

}
