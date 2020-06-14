using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEventReceiver : MonoBehaviour
{
    CharacterCombat combat;

    private void Start()
    {
        combat = GetComponentInParent<CharacterCombat>();
    }
    public void AttackHitEvent()
    {
        Debug.Log("attack event receievd");
        combat.AttackHit_AnimationEvent();
    }
}
