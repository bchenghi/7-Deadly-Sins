using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replacebleAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    protected const float locomationAnimationSmoothTime = 0.1f;


    protected Animator animator;
    protected CharacterCombat combat;
    protected CharacterStats stats;
    protected AnimatorOverrideController overrideContoller;
    


    // Start is called before the first frame update
    protected virtual void Start()
    {

        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        

        if (overrideContoller == null)
            overrideContoller = new AnimatorOverrideController(animator.runtimeAnimatorController);

        animator.runtimeAnimatorController = overrideContoller;

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
        overrideContoller[replacebleAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }

    public virtual void Death()
    {
        animator.SetTrigger("death");
    }

    protected virtual void takenDamage()
    {
        animator.SetTrigger("Hurt");
    }

}
