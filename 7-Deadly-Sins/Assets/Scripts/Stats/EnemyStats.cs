using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject lootDrop;

    LootDrop lootDropTest;
    
    override protected void Start() {
        base.Start();
        lootDropTest = GetComponent<LootDrop>();
    }

    public override void Die()
    {
        base.Die();
        Destroy(GetComponent<Collider>());
        // Add ragdoll/loot
        if (lootDrop != null)
            Instantiate(lootDrop, transform.position, Quaternion.identity);

        lootDropTest.DropLoot();

        Destroy(gameObject, 5);
    }

}
