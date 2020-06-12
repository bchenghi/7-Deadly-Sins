using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDialogueTrigger : MonoBehaviour
{
    ITrigger trigger;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<ITrigger>();
    }

    public void OnTriggerExit(Collider collider) {
        if (collider.name == item.name)
            trigger.Trigger();
    }
}
