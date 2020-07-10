using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReward : MonoBehaviour
{
    bool rewardGiven = false;
    

    public virtual void ActivateReward() {

    }

    public bool RewardGiven() {
        return rewardGiven;
    }
}
