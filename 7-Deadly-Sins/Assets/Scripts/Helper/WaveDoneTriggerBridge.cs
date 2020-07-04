using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDoneTriggerBridge : MonoBehaviour
{
    [SerializeField]
    WaveManager waveManager;
    ITrigger trigger;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<ITrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveManager.done && !triggered) {
            trigger.Trigger();
            triggered = true;
        }
    }
}
