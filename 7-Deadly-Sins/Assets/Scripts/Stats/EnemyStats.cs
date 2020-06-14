using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject lootDrop;
    
    public override void Die()
    {
        base.Die();

        // Add ragdoll/loot
        if (lootDrop != null)
            Instantiate(lootDrop, transform.position, Quaternion.identity);

        Destroy(gameObject, 5);
    }

}
