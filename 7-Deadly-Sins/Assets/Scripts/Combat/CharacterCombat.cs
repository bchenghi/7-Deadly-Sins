using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private bool CloseEnough = false;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCooldown = 5;
    float lastAttackTime;
    public bool SpecialActivated = false;
    public int count = 0;
    
    

    // Max distance from the opponent to be able damage it
    public float attackDistance;

    CharacterStats myStats;
    CharacterStats opponentStats;

    public bool InCombat { get; private set; }
    public bool dead { get; set; }
    public bool attacking { get; private set; }
    public event System.Action OnAttack;

    protected virtual void Start()
    {
        myStats = GetComponent<CharacterStats>();
        
    }

    protected virtual void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    // If not dead and attack is not on cooldown, set opponent stats variable, 
    // OnAttack() (animation for attack), reset cooldown, set in combat to true and set last 
    // attack time to current time
    public void Attack (CharacterStats targetStats)
    {
        if (!dead)
        {
            if (attackCooldown <= 0f)
            {
                opponentStats = targetStats;
                if (OnAttack != null)
                {
                    OnAttack();
                }

                attackCooldown = 1f / attackSpeed;
                SetInCombat();
            }
        }
    }

    // Called via attack hit event in animation, will deal damage if target is within attackdistance
    public virtual void AttackHit_AnimationEvent()
    {
        float distance = Vector3.Distance(opponentStats.transform.position, myStats.transform.position);
        if (distance <= attackDistance)
        {
            if (opponentStats.transform.GetComponent<PlayerStats>() != null)
            {
                if (opponentStats.transform.GetComponent<PlayerStats>().InvisibleAmt <= 0)
                {
                    opponentStats.TakeDamage(myStats.damage.GetValue()); 
                }
            } else
            {
                opponentStats.TakeDamage(myStats.damage.GetValue());
                CloseEnough = true;
            }
        }

        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

    public void SetInCombat() 
    {
        InCombat = true;
        lastAttackTime = Time.time;
    }

    public Transform ReturnTargetTransform()
    {
        return opponentStats.transform;
    }

    public bool returnCloseEnough()
    {
        return this.CloseEnough;
    }

    public void ResetCloseEnough()
    {
        this.CloseEnough = false;
    }

}
