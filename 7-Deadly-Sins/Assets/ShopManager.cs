using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    // Factor to multiply the buy price, to get the sell price
    [SerializeField]
    [Range(0,1)]
    float factorOfBuyPriceForSellPrice = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // returns sell price of an item, multiplies buy price with a factor sellPriceFactor
    public int GetSellPrice(Item item) {
        int price = item.GetPrice();
        return (int) Math.Floor(price * factorOfBuyPriceForSellPrice);
    }
    

    // Removes item from inventory, and earns the sell price
    public void Sell(Item item) {
        Inventory.instance.Remove(item);
        GoldCounter.instance.Earn(GetSellPrice(item));
    }
}
