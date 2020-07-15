using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackAndSlashEnemyChecker : MonoBehaviour
{
    EnemyStats enemyStats;
    public int level;
    LevelManager manager;
    private bool charDead;

    private void Start()
    {
        if (level == 1)
        {
            manager = HackAndSlashManager.instance.level1Manager;
        } else if (level == 2)
        {
            manager = HackAndSlashManager.instance.level2Manager;
        } else
        {
            manager = HackAndSlashManager.instance.level3Manager;
        }
        enemyStats = GetComponent<EnemyStats>();
        manager.EnemiesCountIncrease();
        Debug.Log("Counts increase");
    }


    private void Update()
    {
        if (enemyStats.currentHealth <= 0 && !charDead)
        {
            manager.EnemiesCountDecrease();
            charDead = true;
        }
    }
}
