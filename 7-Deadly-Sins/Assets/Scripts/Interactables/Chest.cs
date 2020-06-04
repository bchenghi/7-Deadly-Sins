using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;

public class Chest : Interactable
{
    private bool checkKey;
    Inventory inventory;
    public Item keyRequired;
    public GameObject floatingTextPrefab;
    private bool PlayerGavePermission;
    public event System.Action CanOpen;
    private bool hasTriggered;
    ChestInventoryUI chestInventoryUI;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    private void Start()
    {
        chestInventoryUI = GetComponent<ChestInventoryUI>();
        
        
}



    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.E))
        { 
            PlayerGavePermission = true;

            if (hasTriggered == false && checkKey == true)
            {
                
                if (CanOpen != null)
                {
                    CanOpen();
                }
                Inventory.instance.Remove(keyRequired);
                DisplayUI();
                hasTriggered = true;
                

            } 

        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            chestInventoryUI.UnDisplay();
        }



    }


    public override void Interact()
    {
        chestInventoryUI.ClearChest();
        if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        
        checkInventory();
        base.Interact();
        if (floatingTextPrefab)
        {
            ShowFloatingText();
        }
       
        if (checkKey == true)
        {
            Debug.Log("Has key, now require permission");
            if (PlayerGavePermission)
            {
                Debug.Log("permission Given");
            }
            
            

        } else
        {
            Debug.Log("No key");
            //Does nothing
        }



    }

    public void DisplayUI()
    {
        chestInventoryUI.DisplayUI();
    }



    public void checkInventory()
    {
        inventory = Inventory.instance;
        int indexOfKey = inventory.IndexOfItem(keyRequired);
        if (indexOfKey == -1)
        {
            checkKey = false;
        } else
        {

            checkKey = true;

        }


    }

    public void ShowFloatingText()
    {
        var floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        if (checkKey == true)
        {
            floatingText.GetComponent<TextMeshPro>().text = "You have a key, Press E to open chest";
            

        } else
        {
            floatingText.GetComponent<TextMeshPro>().text = "You do not have a key";
        }
    }


    public bool GetCheckKey()
    {
        return this.checkKey;
    }

    public bool GetPermission()
    {
        return this.PlayerGavePermission;
    }

}
