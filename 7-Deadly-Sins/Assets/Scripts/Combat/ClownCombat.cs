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
        dead = false;
        attacking = false;
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
        if (!dead && !controller.teleporting)
        {
            if (attackCooldown <= 0f)
            {
                opponentStats = targetStats;
                if (OnAttack != null)
                {
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
        Debug.Log("fire fireball from combat");
        projectileHandler.ShootProjectile(myStats.damage.GetValue(), 
        myStats.projectileSpeed.GetValue(), opponentStats.transform);
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
