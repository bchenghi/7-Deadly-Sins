using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : CharacterAnimator
{
    public AnimationClip replacebleAttackAnimRanged;
    public AnimationClip[] defaultAttackAnimSetRanged;
    protected AnimationClip[] currentAttackAnimSetRanged;

    new BossCombat combat;
    BossController bossController;
    public AnimatorOverrideController overrideContollerRanged;

    protected override void Start()
    {
        
        animator = GetComponent<Animator>();
        combat = GetComponent<BossCombat>();
        stats = GetComponent<EnemyStats>();
        bossController = GetComponent<BossController>();


        if (overrideContoller == null)
            overrideContoller = new AnimatorOverrideController(animator.runtimeAnimatorController);

        if (overrideContollerRanged == null)
            overrideContollerRanged = new AnimatorOverrideController(animator.runtimeAnimatorController);

        animator.runtimeAnimatorController = overrideContoller;
        animator.runtimeAnimatorController = overrideContollerRanged;

        currentAttackAnimSet = defaultAttackAnimSet;
        currentAttackAnimSetRanged = defaultAttackAnimSetRanged;

        combat.OnAttack += OnAttack;
        stats.takenDamage += takenDamage;
        combat.RangeAttack += RangeAttack;
    }

    protected override void Update()
    {
        animator.SetBool("InCombat", combat.InCombat);
        if (combat.dead)
        {

            Death();
        }
        animator.SetBool("WithinRangeAttack", bossController.withinRange);
    }

    public void RangeAttack()
    {
        animator.SetTrigger("RangedAttack");
        int attackIndex = Random.Range(0, currentAttackAnimSetRanged.Length);
        overrideContollerRanged[replacebleAttackAnim.name] = currentAttackAnimSetRanged[attackIndex];

    }
}
