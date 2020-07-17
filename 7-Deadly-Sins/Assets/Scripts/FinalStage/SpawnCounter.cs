using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCounter : MonoBehaviour
{
    FinalSpawnerDeath spawnerDeath;
    private bool reducedCount;

    private void Start()
    {
        spawnerDeath = GetComponent<FinalSpawnerDeath>();
        FinalLevelManager.instance.IncreaseSpawnCount();
    }

    private void Update()
    {
        if (spawnerDeath.isDead && !reducedCount)
        {
            FinalLevelManager.instance.DecreaseSpawnCount();
            reducedCount = true;
        }
    }


}
