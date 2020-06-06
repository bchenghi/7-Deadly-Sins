using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Consumable/Potion")]
public class Potions : Consumables , IUsable
{
    
    public override void Use()
    {
        base.Use();
        increaseStats = 10;
        PlayerManager.instance.player.GetComponent<CharacterStats>().IncreaseHealth(increaseStats);
        RemoveFromInventory();
        
    }

    public Sprite Image
    {
        get {
            return icon;
        }

        set {
            throw new System.NotImplementedException();
        }
    }

}
