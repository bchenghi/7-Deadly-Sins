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
    // If trigger is not yet triggered, and CheckTriggerCondition is true, 
    // trigger the ITrigger
    void Update()
    {
        if (!triggered) {
            if(CheckTriggerCondition()) {
                trigger.Trigger();
                triggered = true;
            }
        }
    }

    // Returns true if all obj in array have character combat and are dead, 
    // false if at least one is dead
    bool CheckTriggerCondition() {
        bool willTrigger = true;
        foreach(GameObject obj in gameObjects) {
            if (obj.GetComponent<CharacterCombat>()) {
                if (!obj.GetComponent<CharacterCombat>().dead) {
                    willTrigger = false;
                    break;
                }
            }
            else 
            {
                willTrigger = false;
            }
        }
        return willTrigger;
    }
}
