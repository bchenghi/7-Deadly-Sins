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

    

    // Returns true if inventory contains items to find
    bool CheckPlayerInventoryForItems() {
        bool result = true;
        foreach (ItemIntPair pair in itemsToTakeFromPlayer) {
            if (!Inventory.instance.Exists(pair.item, pair.numberOfItem)) {
                result = false;
                break;
            }
        }
        return result;
    }

    // Checks if mission is complete, and finishes transactions, sets missionComplete
    public override void RequestMissionComplete() {
        if (CheckAndSetIfMissionComplete()) {
            foreach (ItemIntPair pair in itemsToTakeFromPlayer) {
                Take(pair.item, pair.numberOfItem);
            }
            GiveReward();
        } else {
            Debug.Log("Mission not complete");
        }
    }

    public override bool CheckAndSetIfMissionComplete() {
        missionComplete = CheckPlayerInventoryForItems();
        return missionComplete;
    }

    public void Take(Item item) {
        // remove from player inventory
        if (!(item is Others)) {
            Inventory.instance.Remove(item);
        } else {
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
