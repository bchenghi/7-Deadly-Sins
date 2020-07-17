using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerStats : CharacterStats
{
    public bool PotUsed;
    public int increaseInStats;
    public StatUIManager statUIManager;
    public int maxMana = 100;
    public PlayerManaUI playerManaUI;
    public int CurrentMana { get; private set; }

    public int SkillPoints { get; private set; }

    public float InvisibleAmt;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //CurrentMana = maxMana;
        //playerManaUI.SetMaxMana(maxMana);
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
        NewSceneSetUp();
        IncreaseSkillPoints(2);
    }

    public override void Update()
    {
        base.Update();
        if (InvisibleAmt > 0)
        {
            InvisibleAmt -= Time.deltaTime;
        }

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
        statUIManager.UpdateStatUIs();
    }
 
    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }


    public void DecreaseMana(int reduce)
    {
        if (reduce > CurrentMana)
        {
            Debug.Log("Not enough Mana");
        } else
        {
            CurrentMana -= reduce;
            CurrentMana = Mathf.Clamp(CurrentMana, 0, maxMana);
            playerManaUI.SetMana(CurrentMana);

        }
        

    }

    public void IncreaseMana(int increase)
    {
        CurrentMana += increase;
        CurrentMana = Mathf.Clamp(CurrentMana, 0, maxMana);
        playerManaUI.SetMana(CurrentMana);
    }

    public void SetMana(int newCurrentMana) {
        CurrentMana = newCurrentMana;
        CurrentMana = Mathf.Clamp(CurrentMana, 0, maxMana);
        playerManaUI.SetMana(CurrentMana);
    }

    public void IncreaseInitialPoints(int amount)
    {
        SkillPoints = amount;
    }

    public void IncreaseSkillPoints(int amount)
    {
        SkillPoints += amount;
    }

    public void DecreaseSkillPoints()
    {
        SkillPoints -= 1;
        SkillPoints = Mathf.Clamp(SkillPoints, 0, int.MaxValue);
        Debug.Log(SkillPoints + "left");
    }

    public void SetIncreaseInStats(int value, bool HpOrMana)
    {
        increaseInStats = value;
        PotUsed = HpOrMana;
    }

    public void Invisible(float delay, float invisibleLength)
    {
        if (delay > 0)
        {
            StartCoroutine(StartInvisible(delay, invisibleLength));
        } else
        {
            InvisibleAmt = invisibleLength;
        }
    }

    IEnumerator StartInvisible(float delay, float invisibleLength)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Invisible");
        InvisibleAmt = invisibleLength;
    }

    public void SetSkillPoints(int skillPoints) {
        this.SkillPoints = skillPoints;
    }
    
    // Will update stats and update stat uis according to current equipment for armor and dmg, while health and mana
    // is based on saved data from previous scene.
    // used when entering new scene, called in Start.
    public void NewSceneSetUp() {
        foreach(Equipment equipment in EquipmentManager.instance.currentEquipment) {
            if (equipment != null) {
                onEquipmentChanged(equipment, null);
            }
        }
        
        /* these stats are set up in save load, load method
        SetHealth(SaveLoad.instance.HP);
        SetMana(SaveLoad.instance.Mana);
        SetSkillPoints(SaveLoad.instance.SkillPoints);
        */
    }
    


}
