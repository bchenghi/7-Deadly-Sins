using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : EnemyAnimator
{
    [SerializeField]
    AnimatorOverrideController powerUpController;
    [SerializeField]
    float triggerPowerUpHealth;
    bool changedAnimatorController = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= triggerPowerUpHealth && !changedAnimatorController && powerUpController != null) {
            animator.runtimeAnimatorController = powerUpController;
            PowerUpAnimation();
            changedAnimatorController = true;
        }
    }

    public void PowerUpAnimation() {
        animator.SetTrigger("powerUp");
    }
}
