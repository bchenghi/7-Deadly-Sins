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
    [SerializeField]
    bool requiresKey = true;
    [SerializeField]
    bool canReopen = false;
    
    // bool value to know if chest was interacted by the player (left click)


    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    GameObject floatingText = null;

    LootBoxAnimation animator;

    Coroutine currentTemporaryFloatingTextCoroutine;

    // A flag used to check if chest was clicked on, if clicked can use E to open
    bool interacted = false;

    private void Start()
    {
        chestInventoryUI = GetComponent<ChestInventoryUI>();
        animator = GetComponent<LootBoxAnimation>();
        
    }



    protected override void Update()
    {
        base.Update();
        
        if (interacted && Input.GetKeyDown(KeyCode.E) && !chestInventoryUI.displayOn 
        && PlayerWithinRadius())
        { 
            PlayerGavePermission = true;
            if (!canReopen) {
                if (hasTriggered == false && ((requiresKey && checkKey == true) || !requiresKey))
                {
                    
                    if (CanOpen != null)
                    {
                        CanOpen();
                    }
                    if (keyRequired) {
                        Inventory.instance.Remove(keyRequired);
                    }
                    DisplayUI();
                    hasTriggered = true;
                    animator.OpenChest();
                }
            }
            else if (canReopen) {
                if ((requiresKey && checkKey == true) || !requiresKey) {
                    if (CanOpen != null)
                    {
                        CanOpen();
                    }
                    if (keyRequired) {
                        Inventory.instance.Remove(keyRequired);
                    }
                    DisplayUI();
                    hasTriggered = true;
                    animator.OpenChest();
                }
            }
        }


        else if (interacted && Input.GetKeyDown(KeyCode.E) && chestInventoryUI.displayOn)
        {
            chestInventoryUI.UnDisplay();
            RemoveFloatingText();
            animator.CloseChest();
            interacted = false;
        }


        if (!isFocus) {
            RemoveFloatingText();
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
        interacted = true;
    }

    public void DisplayUI()
    {
        chestInventoryUI.DisplayUI();
    }



    public void checkInventory()
    {
        if (requiresKey) {
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
    }

    public void ShowFloatingText()
    {
        RemoveFloatingText();
        
        if ((!hasTriggered && !canReopen) || canReopen) {
            floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            if (requiresKey && checkKey == true)
            {
                floatingText.GetComponent<TextMeshPro>().text = "You have a key, Press E to open chest";
            } else if (requiresKey && !checkKey)
            {
                currentTemporaryFloatingTextCoroutine = 
                StartCoroutine(TemporaryFloatingText("You do not have a key", 3f));
            } else if (!requiresKey) 
            {
                floatingText.GetComponent<TextMeshPro>().text = "Press E to open chest";
            }
        } else if (hasTriggered && !canReopen) {
            currentTemporaryFloatingTextCoroutine = 
            StartCoroutine(TemporaryFloatingText("This chest can only be opened once", 3f));
        }
        
    }

    IEnumerator TemporaryFloatingText(string text, float duration) {
        if (currentTemporaryFloatingTextCoroutine != null) {
            StopCoroutine(currentTemporaryFloatingTextCoroutine);
        }
        floatingText.GetComponent<TextMeshPro>().text = text;
        yield return new WaitForSeconds(duration);
        RemoveFloatingText();
    }

    void RemoveFloatingText() {
        if (floatingText != null) {
            floatingText.GetComponent<TextMeshPro>().text = "";
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
