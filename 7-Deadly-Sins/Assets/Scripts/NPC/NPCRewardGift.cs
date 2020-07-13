using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRewardGift : NPCReward
{
    // A list with pairs containing an item and number, which are the items and number of them
    // needed from the player
    [SerializeField]
    List<ItemIntPair> itemsToGivePlayer = new List<ItemIntPair>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void ActivateReward() {
        int goldReward = 0;
        foreach(ItemIntPair pair in itemsToGivePlayer) {
            if (!(pair.item is Currency)) {
                Give(pair.item, pair.numberOfItem);
            } else {
                goldReward += ((Currency) pair.item).GetPickUpAmount() * pair.numberOfItem;
            }
        }
        GoldCounter.instance.Earn(goldReward);
    }

    void Give(Item item, int numberOfTheItem) {
        for (int i = 0; i < numberOfTheItem; i++) {
            Give(item);
        }
    }

    void Give(Item item) {
        if (item is Currency){
            GoldCounter.instance.Earn(((Currency)item).GetPickUpAmount());
        } else {
            Inventory.instance.Add(item);
        }
    }
}
