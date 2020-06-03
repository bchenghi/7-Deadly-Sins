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



    


    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Key code E down");

            PlayerGavePermission = true;
            if (hasTriggered == false && checkKey == true && PlayerGavePermission == true)
            {
                if (CanOpen != null)
                {
                    CanOpen();
                }
                hasTriggered = true;
                Inventory.instance.Remove(keyRequired);
            }

        }
        

    }


    public override void Interact()
    {
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
            
            // Display the chest Inventory UI

        } else
        {
            Debug.Log("No key");
            //Does nothing
        }



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
