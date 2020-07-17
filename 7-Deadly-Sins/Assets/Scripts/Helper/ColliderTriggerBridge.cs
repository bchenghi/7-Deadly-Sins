using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class ColliderTriggerBridge : MonoBehaviour
{
    // Can retrigger means if the player walks into the trigger again,
    // the same dialogue box will appear again. If false, it will only
    //  appear once in the game
    [SerializeField]
    bool canRetrigger;
    bool triggered = false;
    public ITrigger trigger;
    public bool ExternalTrigger;
    void Start() {
        trigger = GetComponent<ITrigger>();
    }

    public void OnTriggerEnter(Collider collider) {
        if (!ExternalTrigger)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (!canRetrigger && triggered)
                    return;

                trigger.Trigger();
                triggered = true;
            }
        } else
        {
            if (collider.CompareTag("TriggerDialogue"))
            {
                if (!canRetrigger && triggered)
                    return;
                trigger.Trigger();
                triggered = true;
            }
        }
    }
}
