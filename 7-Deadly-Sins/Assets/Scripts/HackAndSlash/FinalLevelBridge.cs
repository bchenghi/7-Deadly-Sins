using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelBridge : MonoBehaviour
{
    ITrigger trigger;
    HackAndSlashManager manager;

    private void Start()
    {
        trigger = GetComponent<ITrigger>();
        manager = HackAndSlashManager.instance;
    }

    private void Update()
    {
        if (manager.allLevelDone)
        {
            trigger.Trigger();
        }
    }


}
