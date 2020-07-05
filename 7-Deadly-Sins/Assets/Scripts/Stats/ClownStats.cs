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



    public override void Die()
    {
        //Die in some way
        //method meant to be overriden
        base.Die();
        Destroy(GetComponent<Collider>());

        lootDropTest.DropLoot();
        Destroy(gameObject, 5);


    }

}