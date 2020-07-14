using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousSceneTriggerBridge : MonoBehaviour
{
    ITrigger trigger;
    [SerializeField]
    string previousSceneNameToTrigger;

    bool firstFrame = true;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<ITrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame) {
            if (previousSceneNameToTrigger == 
            PreviousScene.instance.previousSceneName && !triggered) {
                trigger.Trigger();
                firstFrame = false;
                triggered = true;
            }
        }
    }
}
