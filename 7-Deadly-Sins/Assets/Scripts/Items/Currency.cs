using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency", menuName = "Inventory/Currency")]
public class Currency : Item
{

    public enum pickUpObject { COPPER, SILVER, GOLD };
    public pickUpObject currentObject;
    private int pickUpQuantity;

    public int GetQuantity()
    {
        if (currentObject == pickUpObject.COPPER)
        {
            this.pickUpQuantity = 10;
        } else if (currentObject == pickUpObject.SILVER)
        {
            this.pickUpQuantity = 20;
        } else if (currentObject == pickUpObject.GOLD)
        {
            this.pickUpQuantity = 30;
        } else
        {
            return 0;
        }
        return this.pickUpQuantity;
        
    }
}
