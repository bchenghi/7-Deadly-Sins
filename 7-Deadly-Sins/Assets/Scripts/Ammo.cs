using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ammo", menuName = "Inventory/Others/Ammo")]
public class Ammo : Others
{
    // price of one
    public override int GetPrice() {
        return 1;
    }
}
