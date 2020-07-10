using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMissionDeath : NPCMission
{
    [SerializeField]
    GameObject[] enemiesToKill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool CheckAndSetIfMissionComplete(){
        bool result = true;
        foreach(GameObject enemy in enemiesToKill) {
            if (enemy != null && !enemy.GetComponent<CharacterCombat>().dead) {
                result = false;
                break;
            }
        }
        missionComplete = result;
        return missionComplete;
    }

    public override void RequestMissionComplete() {
        if (CheckAndSetIfMissionComplete()) {
            GiveReward();
        } else {
            Debug.Log("Mission not yet complete");
        }
    }
}
