using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; set; }

    public Stat damage;
    public Stat armor;
    public event System.Action takenDamage;

    protected CharacterCombat combat;

    protected virtual void Start()
    {
        combat = GetComponent<CharacterCombat>();

    }
    public event System.Action<int, int> OnHealthChanged;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void Update()
    {
    }

    // takes damage to healh, runs animation for receiving damage, updates health
    public virtual void TakeDamage (int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        if (takenDamage != null)
        {
            takenDamage();
        }

        Debug.Log (transform.name + " takes " + "damage.");


        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0 && combat != null && !combat.dead)
        {

            Die();

        }

        GetComponent<EffectHandler>().BloodEffectEvent(transform, 2);
    }

    public void IncreaseHealth(int healthIncrease)
    {
        currentHealth += healthIncrease;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public void SetHealth(int newCurrentHealth) {
        currentHealth = newCurrentHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (OnHealthChanged != null) {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public virtual void Die()
    {
        //Die in some way
        //method meant to be overriden
        combat.dead = true;
        Debug.Log(transform.name + " died.");
    }

}
