﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : EnemyAnimator
{
    [SerializeField]
    AnimationClip[] PowerUpAttackAnimSet;
    [SerializeField]
    AnimatorOverrideController powerUpController;
    [SerializeField]
    public float triggerPowerUpHealth;
    public bool powerUp;
    bool changedAnimatorController = false;
    SoundHandler soundHandler;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        soundHandler = GetComponent<SoundHandler>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= triggerPowerUpHealth && !changedAnimatorController && powerUpController != null) {
            powerUp = true;
            animator.runtimeAnimatorController = powerUpController;
            currentAttackAnimSet = PowerUpAttackAnimSet;
            PowerUpAnimation();
            changedAnimatorController = true;
        }
    }

    public void PowerUpAnimation() {
        animator.SetTrigger("powerUp");
    }

    protected override void OnAttack()
    {
        base.OnAttack();
        soundHandler.Play2SoundRandomly("BossHit1", "BossHit2");
    }

   
}
