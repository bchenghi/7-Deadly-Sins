using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrates : Interactable
{
    public GameObject spawnLoot;

    public override void Interact()
    {
        base.Interact();
        Instantiate(spawnLoot, transform.position, Quaternion.identity);


    }
}
