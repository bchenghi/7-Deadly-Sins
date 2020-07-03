using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAndGFXBridge : MonoBehaviour
{
    AnimationEventReceiver eventReceiver;
    // Start is called before the first frame update
    void Start()
    {
        eventReceiver = GetComponentInParent<AnimationEventReceiver>();
    }

    public void AttackHitEvent() {
        Debug.Log("attack hit event called in bridge and called in event receiver");
        eventReceiver.AttackHitEvent();
    }
}
