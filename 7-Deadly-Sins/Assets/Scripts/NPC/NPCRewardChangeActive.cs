using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRewardChangeActive : NPCReward
{

    [SerializeField]
    GameObject gameObjectToChangeActive;
    [SerializeField]
    bool setActiveTo;
    
    public override void ActivateReward() {
        gameObjectToChangeActive.SetActive(setActiveTo);
    }
}
