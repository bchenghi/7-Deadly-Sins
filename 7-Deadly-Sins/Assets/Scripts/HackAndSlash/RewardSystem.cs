using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    Timer time;
    HackAndSlashManager manager;
    public float tier1Time;
    public float tier2Time;
    public float tier3Time;
    public Item[] tier1Reward;
    public Item[] tier2Reward;
    public Item[] tier3Reward;
    private bool rewarded;

    private void Start()
    {
        time = GetComponent<Timer>();
        manager = HackAndSlashManager.instance;
    }

    private void Update()
    {
       if (manager.allLevelDone && !rewarded && time.finalTimeUpdated)
        {
            RewardPlayer();
            rewarded = true;
            
        }
    }


    private void RewardPlayer()
    {
        if (time.finalTime <= tier1Time)
        {
            
            foreach(Item item in tier1Reward)
            {
                Inventory.instance.Add(item);
            }
        }else if (time.finalTime <= tier2Time && time.finalTime > tier1Time)
        {
            foreach (Item item in tier2Reward)
            {
                Inventory.instance.Add(item);
            }
        }else if (time.finalTime <= tier3Time && time.finalTime > tier2Time)
        {
            foreach (Item item in tier3Reward)
            {
                Inventory.instance.Add(item);
            }
        }
    }


}
