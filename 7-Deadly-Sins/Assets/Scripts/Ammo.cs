using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ammo", menuName = "Inventory/Others/Ammo")]
public class Ammo : Others
{
    // price of one
    public override int GetPrice() {
        if (name == "Turret Ammo") {
            return 1;
        } else if (name == "Flame Thrower Ammo") {
            return 30;
        } else {
            return 0;
        }
    }
}
