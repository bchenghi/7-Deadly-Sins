using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEventReceiver : MonoBehaviour
{
    CharacterCombat combat;
    EffectHandler effects;

    private void Start()
    {
        combat = GetComponentInParent<CharacterCombat>();
        effects = GetComponentInParent<EffectHandler>();
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
}
