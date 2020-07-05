using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAnimationEventReceiver : MonoBehaviour
{
    ClownStats stats;
    ClownCombat combat;
    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponentInParent<ClownCombat>();
        stats = GetComponentInParent<ClownStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RangedAttackEvent() {
        combat.RangedDamage_AnimationEvent();
    }
}
