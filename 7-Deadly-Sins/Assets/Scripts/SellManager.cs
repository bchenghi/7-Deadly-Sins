using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellManager : MonoBehaviour
{
    #region Singelton

    public static SellManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;


        }
        else
        {
            Destroy(gameObject);
        }

    }

    #endregion


    public Item itemTobeSold;
    public TMP_InputField field;
    private int getQuantity;
    public GameObject inputField;
    public float sellPercentage;
    DisplayTextManager DisplayManager;


    private void Start()
    {
        DisplayManager = DisplayTextManager.instance;
    }
    public void DoneButton()
    {
        getQuantity = int.Parse(field.text);
        field.text = "";
        HotKeyBar.instance.EnableAllMaster();
        inputField.SetActive(false);
        Sell(itemTobeSold, getQuantity);
       

       
    }

    public int GetInputFieldQty()
    {
        return getQuantity;
    }

    private void Sell(Item item, int quantityTobeSold)
    {
        int quantityInInventory = Inventory.instance.getValue(itemTobeSold);
        if (quantityTobeSold <= quantityInInventory)
        {
            if (item is Others)
            {
                Inventory.instance.Remove(item, quantityTobeSold);
                EarnFromSelling(item, quantityTobeSold);
            }
            else
            {
                for (int i = 0; i < quantityTobeSold; i++)
                {
                    Inventory.instance.Remove(item);
                    
                }
                EarnFromSelling(item, quantityTobeSold);
            }




        } else
        {
            DisplayManager.Display("Quantity exceeds inventory count", 1.5f);
            Debug.Log("Quantity exceeds inventory count");
        }

    }

    public void SellSingleItem(Item item)
    {
        if (item is Others)
        {
            Inventory.instance.Remove(item, 1);
            EarnFromSelling(item, 1);
        }
        else
        {
            Inventory.instance.Remove(item);
            EarnFromSelling(item, 1);
        }


    }

    public void EarnFromSelling(Item item, int quantity)
    {
        if (item is Equipment)
        {
            var equipment = item as Equipment;
            int cashBack = item.GetPrice();
            Debug.Log(item.GetPrice());
            int cashAfterCut = (int)(cashBack * sellPercentage); 
            GoldCounter.instance.Earn(cashAfterCut);
            DisplayText(item, quantity, cashAfterCut);
        } else
        {
            int cashEarned = (int)(item.GetPrice() * quantity * sellPercentage);
            Debug.Log(cashEarned);
            GoldCounter.instance.Earn(cashEarned);
            DisplayText(item, quantity, cashEarned);
            
        }
    }

    public int SellPrice(Item item, int quantity) {
        if (item is Equipment)
        {
            var equipment = item as Equipment;
            int cashBack = item.GetPrice();
            //Debug.Log(item.GetPrice());
            int sellPrice = (int)(cashBack * sellPercentage); 
            return sellPrice;
        } else
        {
            int sellPrice = (int)(item.GetPrice() * quantity * sellPercentage);
            //Debug.Log(sellPrice);
            return sellPrice;
            
        }
    }

    private void DisplayText(Item item, int quantity, int goldEarned)
    {
        string sellString = "Sold " + quantity + " " + item.name + " for " + goldEarned + " gold";
        DisplayManager.Display(sellString, 1.5f);
    }
}

    
