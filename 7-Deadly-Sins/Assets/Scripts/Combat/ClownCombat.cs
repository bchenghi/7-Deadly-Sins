using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownCombat : MonoBehaviour
{
    private bool CloseEnough = false;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    float lastAttackTime;
    public int count = 0;

    SoundHandler soundHandler;
    ClownController controller;
    
    

    // Max distance from the opponent to be able damage it
    public float attackDistance;

    ClownStats myStats;
    CharacterStats opponentStats;

    public bool dead { get; set; }
    public bool attacking { get; private set; }
    public event System.Action OnAttack;
    ProjectileHandler projectileHandler;

    protected virtual void Start()
    {
        controller = GetComponent<ClownController>();
        myStats = GetComponent<ClownStats>();
        soundHandler = GetComponent<SoundHandler>();
        projectileHandler = GetComponentInParent<ProjectileHandler>();
    }

    protected virtual void Update()
    {
        attackCooldown -= Time.deltaTime;
    }


    // If not dead and attack is not on cooldown, set opponent stats variable, 
    // OnAttack() (animation for attack), reset cooldown, set in combat to true and set last 
    // attack time to current time
    public void Attack (CharacterStats targetStats)
    {
        Debug.Log(!controller.teleporting);
        if (!dead && !controller.teleporting)
        {
            if (attackCooldown <= 0f)
            {
                opponentStats = targetStats;
                if (OnAttack != null)
                {
                    Debug.Log("attack called in combat");
                    OnAttack();
                }

                attackCooldown = 1f / attackSpeed;
                
            }
        }
    }

    // Called via attack hit event in animation, will deal damage if target is within attackdistance
    // Also plays attack sounds
    public virtual void RangedDamage_AnimationEvent()
    {
        float distance = Vector3.Distance(opponentStats.transform.position, myStats.transform.position);
        if (distance <= attackDistance)
        /*
        {
            if (opponentStats.transform.GetComponent<PlayerStats>() != null)
            {
                if (opponentStats.transform.GetComponent<PlayerStats>().InvisibleAmt <= 0)
                {
                    opponentStats.TakeDamage(myStats.damage.GetValue()); 
                    soundHandler.PlayAttackSoundBy(myStats.transform);
                }
            } else
            {
                //opponentStats.TakeDamage(myStats.damage.GetValue());
                soundHandler.PlayAttackSoundBy(myStats.transform);
                CloseEnough = true;
            }
        } */
        {
            //shoot projectile, pass damage value into projectile, play sound for shooting
            projectileHandler.ShootProjectile(myStats.damage.GetValue(), myStats.projectileSpeed.GetValue(), opponentStats.transform);
        }
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
