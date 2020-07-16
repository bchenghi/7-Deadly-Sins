using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    CharacterCombat combat;
    EffectHandler effects;
    PlayerStats stats;
    PlayerController controller;


    // In potion script, if potion is used, it will set this as the potion that was used.
    [HideInInspector]
    public Potions potionUsed;

    private void Start()
    {
        combat = GetComponentInParent<CharacterCombat>();
        effects = GetComponentInParent<EffectHandler>();
        stats = GetComponentInParent<PlayerStats>();
        controller = GetComponentInParent<PlayerController>();
    }
    public void AttackHitEvent()
    {
        Debug.Log("attack event receievd");
        combat.AttackHit_AnimationEvent();
    }

    public void MichellenousEvent(int eventNumberRange)
    {
        Debug.Log("Effect event");
        /*
        if (combat.returnCloseEnough())
        {
            effects.BloodEffectEvent(combat.ReturnTargetTransform(), eventNumberRange);
            combat.ResetCloseEnough();
        }
        */
    }

    public void DrinkPotionEvent()
    {
        if (stats.PotUsed)
        {
            stats.IncreaseHealth(stats.increaseInStats);
            effects.HealEffectEvent(2);
            if (potionUsed != null) {
                Inventory.instance.Remove(potionUsed);
            }
            
        } else
        {
            stats.IncreaseMana(stats.increaseInStats);
            effects.HealEffectEvent(3);
            if (potionUsed != null) {
                Inventory.instance.Remove(potionUsed);
            }
        }
    }

    public void StopDrinkingEvent()
    {
        controller.DisablePotionGFX();
    }

    public void EffectEventSmash(int number)
    {
        effects.EffectEvent(number);
    }
}
