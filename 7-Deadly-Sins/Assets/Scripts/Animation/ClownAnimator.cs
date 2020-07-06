using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAnimator : Monobehaviour
{
    // Setting on inspector to change, used as reference in code
    public bool reactsToDamage = true;

    // Actual react to damage bool used in code
    [HideInInspector]
    public bool damageReaction;
    public AnimationClip replacebleAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    protected const float locomationAnimationSmoothTime = 0.1f;


    protected Animator animator;
 
    public AnimatorOverrideController overrideContoller;
    ClownCombat clownCombat;
    ClownStats clownStats;
    protected virtual void Start(){
        damageReaction = reactsToDamage;
        animator = GetComponentInChildren<Animator>();
        clownCombat = GetComponent<ClownCombat>();
        clownStats = GetComponent<ClownStats>();


        if (overrideContoller == null)
            overrideContoller = new AnimatorOverrideController(animator.runtimeAnimatorController);

        animator.runtimeAnimatorController = overrideContoller;

        currentAttackAnimSet = defaultAttackAnimSet;
        clownCombat.OnAttack += OnAttack;
        //Debug.Log();
        clownStats.takenDamage += takenDamage;
    }
  
    protected virtual void Update()
    {
        if (clownCombat.dead)
        {
            Death();
        }

        if (reactsToDamage) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Magic Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Teleport")) {
                damageReaction = false;
            } 
            else if (!damageReaction) {
                damageReaction = true;
            }
        }
    }
        
    

    protected virtual void OnAttack()
    {
        Debug.Log("attack called in animator");
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideContoller[replacebleAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }

    public virtual void Death()
    {
        animator.SetTrigger("death");
    }

    protected virtual void takenDamage()
    {
        if (reactsToDamage && damageReaction)
            animator.SetTrigger("Hurt");
    }

}
