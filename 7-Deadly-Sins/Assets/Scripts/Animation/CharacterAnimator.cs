using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public bool reactsToDamage = true;
    public AnimationClip replacebleAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    protected const float locomationAnimationSmoothTime = 0.1f;


    protected Animator animator;
    protected CharacterCombat combat;
    protected CharacterStats stats;
    public AnimatorOverrideController overrideController;
    


    // Start is called before the first frame update
    protected virtual void Start()
    {

        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        

        if (overrideController == null)
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;

        combat.OnAttack += OnAttack;
        stats.takenDamage += takenDamage;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        animator.SetBool("InCombat", combat.InCombat);
        if (combat.dead)
        {

            Death();
        }
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replacebleAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }

    public virtual void Death()
    {
        animator.SetTrigger("death");
    }

    protected virtual void takenDamage()
    {
        if (reactsToDamage)
            animator.SetTrigger("Hurt");
    }

    

   

}
