using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will trigger the ITrigger if gameObjects in the array are all null
public class DeathTriggerBridge : MonoBehaviour
{
    ITrigger trigger;
    [SerializeField]
    GameObject[] gameObjects;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<ITrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered) {
            if(CheckTriggerCondition()) {
                trigger.Trigger();
                triggered = true;
            }
        }
    }

    bool CheckTriggerCondition() {
        bool willTrigger = true;
        foreach(GameObject obj in gameObjects) {
            if (obj != null) {
                willTrigger = false;
                break;
            }
        }
        return willTrigger;
    }
}
