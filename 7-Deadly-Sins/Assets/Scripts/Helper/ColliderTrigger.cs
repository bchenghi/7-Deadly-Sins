using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    // Can retrigger means if the player walks into the trigger again,
    // the same dialogue box will appear again. If false, it will only
    //  appear once in the game
    [SerializeField]
    bool canRetrigger;
    bool triggered = false;
    public ITrigger trigger;
    void Start() {
        trigger = GetComponent<ITrigger>();
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (!canRetrigger && triggered)
                return;

            trigger.Trigger();
            triggered = true;
        }
    }
}
