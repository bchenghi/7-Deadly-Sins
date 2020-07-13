using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMissionCollect : NPCMission
{

    // A list with pairs containing an item and number, which are the items and number of them
    // needed from the player
    [SerializeField]
    List<ItemIntPair> itemsToTakeFromPlayer = new List<ItemIntPair>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    // Returns true if inventory contains items to find or currency to pay
    bool CheckPlayerForItems() {
        bool result = true;
        int hypotheticalGoldLeft = GoldCounter.instance.gold;
        foreach (ItemIntPair pair in itemsToTakeFromPlayer) {
            if (!(pair.item is Currency)) {
                if (!Inventory.instance.Exists(pair.item, pair.numberOfItem)) {
                result = false;
                break;
                }
            }
            else 
            {
                int goldRequired = ((Currency) pair.item).GetPickUpAmount() * pair.numberOfItem;
                hypotheticalGoldLeft -= goldRequired;
                if (hypotheticalGoldLeft < 0) {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }

    // Checks if mission is complete, and finishes transactions
    // (in foreach loop, add total amount of gold to take, before taking. For items,
    // takes item in each loop), sets missionComplete
    public override void RequestMissionComplete() {
        if (CheckAndSetIfMissionComplete()) {
            int goldToTake = 0;
            foreach (ItemIntPair pair in itemsToTakeFromPlayer) {
                if (!(pair.item is Currency)) {
                    Take(pair.item, pair.numberOfItem);
                } else {
                    goldToTake += ((Currency) pair.item).GetPickUpAmount() * pair.numberOfItem;
                }
            }
            GoldCounter.instance.Spend(goldToTake);
            GiveReward();
        } else {
            Debug.Log("Mission not complete");
        }
    }

    public override bool CheckAndSetIfMissionComplete() {
        missionComplete = CheckPlayerForItems();
        return missionComplete;
    }

    public void Take(Item item) {
        // remove from player inventory
        if (!(item is Currency)&& !(item is Others)) {
            Inventory.instance.Remove(item);
        } 
        else if ((item is Currency)) {
            GoldCounter.instance.Spend(((Currency) item).GetPickUpAmount());
        }
        else {
            Inventory.instance.Remove(item, 1);
        }
    }

    public void Take(Item item, int numberOfItems) {
        for (int i = 0; i < numberOfItems; i++) {
            Take(item);
        }
    }

    /*
    public override void SetState() {
        if (opening) {
            return;
        }
        else if (!missionComplete) {
            if (CheckAndSetIfMissionComplete()) {
                celebration = true;
            } else {
                disappointment = true;
            }
        } else if (missionComplete) {
            done = true;
        }
    }
    */


}
