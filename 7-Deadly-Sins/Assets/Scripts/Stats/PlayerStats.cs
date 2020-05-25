using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
}
