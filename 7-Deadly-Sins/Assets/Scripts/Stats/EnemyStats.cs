
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
        soundHandler.PlaySoundByName(transform, enemyDeathSound);
        Destroy(GetComponent<Collider>());
        // Add ragdoll/loot
        if (lootDrop != null)
            Instantiate(lootDrop, transform.position, Quaternion.identity);

        lootDropTest.DropLoot();
        Debug.Log("DEstroyed");
        Destroy(gameObject, 5);
        
    }

}
