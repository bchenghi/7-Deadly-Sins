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
        foreach(ItemIntPair pair in itemsToGivePlayer) {
            Give(pair.item, pair.numberOfItem);
        }
    }

    void Give(Item item, int numberOfTheItem) {
        for (int i = 0; i < numberOfTheItem; i++) {
            Give(item);
        }
    }

    void Give(Item item) {
        Inventory.instance.Add(item);
    }
}
