using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnemyCounter : MonoBehaviour
{
    FinalEnemyController controller;
    private bool isDead;

    private void Start()
    {
        controller = GetComponent<FinalEnemyController>();
        FinalLevelManager.instance.IncreaseEnemyCount();
    }

    private void Update()
    {
        if (controller.hasDisappeared && !isDead)
        {
            FinalLevelManager.instance.DecreaseEnemyCount();
            isDead = true;
        }
    }


}
