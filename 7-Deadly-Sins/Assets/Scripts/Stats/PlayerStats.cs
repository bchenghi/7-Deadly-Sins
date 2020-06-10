using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerStats : CharacterStats
{
    public int maxMana = 100;
    public PlayerManaUI playerManaUI;
    public int currentMana { get; private set; }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currentMana = maxMana;
        playerManaUI.SetMaxMana(maxMana);
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
    }

    
        

    void onEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        Debug.Log("equipment changed called in player stats");
        if (newItem != null) {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }


    public void DecreaseMana(int reduce)
    {
        if (reduce > currentMana)
        {
            Debug.Log("Not enough Mana");
        } else
        {
            currentMana -= reduce;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
            playerManaUI.SetMana(currentMana);

        }
        

    }

    public void IncreaseMana(int increase)
    {
        currentMana += increase;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
    }

}
