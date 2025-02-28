﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Exactly the same as enemy animator, except animator component is obtained from child gfx object
public class TutorialEnemyAnimator : CharacterAnimator
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    protected override void Start()
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
        
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;

        animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);

        base.Update();
    }

    protected override void OnAttack()
    {
        base.OnAttack();
    }
}
