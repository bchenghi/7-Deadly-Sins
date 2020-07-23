using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnemyCounter : MonoBehaviour
{
    FinalEnemyController controller;
    private bool isDisappear;
    private bool isDead;
    EnemyStats stats;

    private void Start()
    {
        controller = GetComponent<FinalEnemyController>();
        FinalLevelManager.instance.IncreaseEnemyCount();
        stats = GetComponent<EnemyStats>();
        
    }

    private void Update()
    {
        if (controller.hasDisappeared && !isDisappear)
        {
            FinalLevelManager.instance.DecreaseEnemyCount();
            isDisappear = true;
        }

        if (stats.currentHealth <= 0 && !isDead)
        {
            FinalLevelManager.instance.DecreaseEnemyCount();
            isDead = true;
        }
    }


}
