using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Consumable/Potion")]
public class Potions : Consumables
{
    
    public override void Use()
    {
        base.Use();
        increaseStats = 10;
        PlayerManager.instance.player.GetComponent<CharacterStats>().IncreaseHealth(increaseStats);
        

        

    }
}
