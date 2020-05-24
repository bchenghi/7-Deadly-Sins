using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEventReceiver : MonoBehaviour
{
    CharacterCombat combat;

    private void Start()
    {
        combat = GetComponent<CharacterCombat>();
    }
    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }
}
