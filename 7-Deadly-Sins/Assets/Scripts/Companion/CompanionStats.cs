using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionStats : CharacterStats
{
    public bool RegenerativeCompanion;
    public float regenTime;
    [HideInInspector]
    public bool isRegen;
    private bool CanRegen;
    public float regenAmountPerSec;
    public string CompanionDeathSound;
    SoundHandler soundHandler;
    float regenHealth;
    Coroutine LookAtCoroutine;
    
    
   

    override protected void Start()
    {
        base.Start();
        soundHandler = GetComponent<SoundHandler>();
        isRegen = false;
        CanRegen = false;
        regenHealth = 0;
    }

    public override void Update()
    {
        base.Update();
        if (RegenerativeCompanion)
        {
            if (CanRegen)
            {
                isRegen = true;
            }
            if (currentHealth < maxHealth)
            {
                Regen();
            }
        }
        Debug.Log(isRegen);
        
    }



    public override void Die()
    {
        base.Die();
        soundHandler.PlaySoundByName(transform, CompanionDeathSound);
        Destroy(GetComponent<Collider>());
        // Add ragdoll/loot
        Destroy(gameObject, 5);
    }

    private void Regen()
    {
        if (!combat.InCombat && CanRegen)
        {
            Debug.Log("CAN");
            if (isRegen)
            {
                
                regenHealth += Time.deltaTime * regenAmountPerSec;
                if (regenHealth > 1)
                {
                    int floor = Mathf.FloorToInt(regenHealth);
                    currentHealth += floor;
                    regenHealth -= floor;
                }
                
                if (currentHealth == maxHealth)
                {
                    isRegen = false;
                }
                

            }
        } else if (combat.InCombat)
        {
            isRegen = false;
            CanRegen = false;
            StartLookAt();
            
        }
        

        
    }

    IEnumerator RegenBuffer(float time)
    {
        yield return new WaitForSeconds(time);
        CanRegen = true;
    }

    public void StartLookAt()
    {
        if (LookAtCoroutine != null)
        {
            StopCoroutine(LookAtCoroutine);
        }
        LookAtCoroutine = StartCoroutine(RegenBuffer(regenTime));
    }
}
