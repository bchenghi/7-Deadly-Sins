using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Consumable/Potion")]
public class Potions : Consumables , IUsable
{
    PlayerStats playerStats;
    public bool Health;
    public bool Mana;
    
    public override void Use()
    {
        Debug.Log(playerStats);
        base.Use();
        if (Health)
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().IncreaseHealth(increaseStats);
        } else
        {

            PlayerManager.instance.player.GetComponent<PlayerStats>().IncreaseMana(increaseStats);
            
        }
        RemoveFromInventory();
        
    }

    public void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
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
