using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPortal : MonoBehaviour
{
    ITrigger trigger;

    private void Start()
    {
        trigger = GetComponent<ITrigger>();
    }

    private void Update()
    {
        if (GetComponent<CheckSequence>().Success)
        {
            trigger.Trigger();
        }
    }
}
