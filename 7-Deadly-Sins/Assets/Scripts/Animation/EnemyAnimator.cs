using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : CharacterAnimator
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
