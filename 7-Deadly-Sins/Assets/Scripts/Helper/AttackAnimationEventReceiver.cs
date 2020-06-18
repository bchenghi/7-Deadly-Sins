using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEventReceiver : MonoBehaviour
{
    CharacterCombat combat;
    EffectHandler effects;
    PlayerStats stats;

    private void Start()
    {
        combat = GetComponentInParent<CharacterCombat>();
        effects = GetComponentInParent<EffectHandler>();
        stats = GetComponentInParent<PlayerStats>();
    }
    public void AttackHitEvent()
    {
        Debug.Log("attack event receievd");
        combat.AttackHit_AnimationEvent();
    }

    public void MichellenousEvent(int eventNumberRange)
    {
        Debug.Log("Effect event");
        effects.BloodEffectEvent(combat.ReturnTargetTransform(), eventNumberRange);
    }

    public void DrinkPotionEvent()
    {
        if (stats.PotUsed)
        {
            stats.IncreaseHealth(stats.increaseInStats);
            effects.HealEffectEvent(2);
        } else
        {
            stats.IncreaseMana(stats.increaseInStats);
            effects.HealEffectEvent(3);
        }
    }

    public void StopDrinkingEvent()
    {
        Transform[] ts = transform.GetComponentsInChildren<Transform>(true);
        if (stats.PotUsed)
        {
            foreach (Transform t in ts)
            {
                Debug.Log(t.gameObject.name);
                if (t.gameObject.name == "PlayerHpBottle")
                {
                    t.gameObject.SetActive(false);
                    break;
                }
            }
        }
        else
        {
            foreach (Transform t in ts)
            {
                Debug.Log(t.gameObject.name);
                if (t.gameObject.name == "PlayerManaBottle")
                {
                    t.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}
