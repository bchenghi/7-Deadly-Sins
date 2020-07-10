using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class that stores status of mission and reward if any, child classes specify mission itself
public class NPCMission : MonoBehaviour
{

    [SerializeField]
    NPCReward reward;

    protected bool missionComplete = false;

    // states npc can be in, dialogues changes based on state
    [HideInInspector]
    public bool celebration = false;
    [HideInInspector]
    public bool disappointment = false;
    [HideInInspector]
    public bool opening = true;
    [HideInInspector]
    public bool done = false; 
    

    public virtual void SetState() {
        if (opening) {
            return;
        }
        else if (!missionComplete) {
            if (CheckAndSetIfMissionComplete()) {
                RequestMissionComplete();
                celebration = true;
            } else {
                disappointment = true;
            }
        } else if (missionComplete) {
            done = true;
        }
    }

    public virtual bool CheckAndSetIfMissionComplete() {
        return missionComplete;
    }

    public virtual void GiveReward() {
        if (reward != null) {
            reward.ActivateReward();
        } else {
            Debug.Log("No reward specified");
        }
    }

    public virtual void ResetState() {
        opening = false;
        celebration = false;
        done = false;
        disappointment = false;
    }

    public virtual void RequestMissionComplete() {

    }
}
