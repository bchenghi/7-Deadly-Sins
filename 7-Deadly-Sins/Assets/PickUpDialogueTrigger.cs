using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDialogueTrigger : MonoBehaviour
{
    ITrigger trigger;
    public GameObject item;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<ITrigger>();
    }

    void Update() {
        if (item == null && !triggered) {
            trigger.Trigger();
            triggered = true;
        }
    }


}
