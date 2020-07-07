using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownStats : CharacterStats
{
    public Stat projectileSpeed;

    protected ClownCombat clownCombat;
    public string enemyDeathSound;
    SoundHandler soundHandler;
    
    LootDrop lootDropTest;

    protected override void Start()
    {
        base.Start();
        clownCombat = GetComponent<ClownCombat>();
        soundHandler = GetComponent<SoundHandler>();
        lootDropTest = GetComponent<LootDrop>();

    }
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public override void TakeDamage (int damage)
    {
        base.TakeDamage(damage);
        

        if (currentHealth <= 0 && !clownCombat.dead)
        {
            Die();
        }
    }



    public override void Die()
    {
        //Die in some way
        //method meant to be overriden
        clownCombat.dead = true;
        Debug.Log(transform.name + " died.");
        Destroy(GetComponent<Collider>());

        lootDropTest.DropLoot();
        Destroy(gameObject, 5);


    }

}