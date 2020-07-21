using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStats : EnemyStats
{
    NavMeshAgent agent;
    BossAnimator bossAnimator;
    public int damageBoost;
    public int armorBoost;
    public float speedBoost;
    private bool statsIncrease;
    override protected void Start()
    {
        base.Start();
        bossAnimator = GetComponent<BossAnimator>();
        agent = GetComponent<NavMeshAgent>();

    }

    public override void Update()
    {
        base.Update();
        if (bossAnimator.powerUp && !statsIncrease)
        {
            statsIncrease = true;
            if (damageBoost != 0)
            {
                DamageIncrease(damageBoost);
            }

            if (armorBoost != 0)
            {
                ArmorIncrease(armorBoost);
            }
            
            if (speedBoost != 0)
            {
                SpeedIncrease(speedBoost);
            }
        }
    }

    private void DamageIncrease(int increase)
    {
        damage.AddModifier(increase);
    }

    private void ArmorIncrease(int increase)
    {
        armor.AddModifier(increase);
    }

    private void SpeedIncrease(float increase)
    {
        agent.speed += increase;
    }
}
