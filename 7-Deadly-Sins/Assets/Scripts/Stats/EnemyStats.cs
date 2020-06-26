using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public string enemyDeathSound;
    SoundHandler soundHandler;
    public GameObject lootDrop;
    LootDrop lootDropTest;
    
    override protected void Start() {
        base.Start();
        lootDropTest = GetComponent<LootDrop>();
        soundHandler = GetComponent<SoundHandler>();
    }

  

    public override void Die()
    {
        base.Die();
        soundHandler.PlaySoundByName(enemyDeathSound);
        Destroy(GetComponent<Collider>());
        // Add ragdoll/loot
        if (lootDrop != null)
            Instantiate(lootDrop, transform.position, Quaternion.identity);

        lootDropTest.DropLoot();

        Destroy(gameObject, 5);
    }

}
