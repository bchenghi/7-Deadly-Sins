using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class FinalLevelManager : MonoBehaviour
{
    public bool AllSpawnerDestroyed;
    public Transform TargetPoint;
    public int EnemiesCount;
    EffectHandler effects;

    #region Singelton

    public static FinalLevelManager instance { get; private set; }

    
    #endregion

    public int totalSpawnersLeft;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;


        }
        else
        {
            Destroy(gameObject);
        }

        totalSpawnersLeft = 0;
    }

    private void Start()
    {
        effects = GetComponent<EffectHandler>();
    }

    private void Update()
    {
        CheckSpawnersCount();
        if (AllSpawnerDestroyed && EnemiesCount != 0)
        {
            effects.StartbossSpawnEffectEvent(8, TargetPoint.position);
        } else
        {

            effects.StopbossSpawnEffectEvent(8);
            Debug.Log("Spawn Leonard");
        }
    }



    public void IncreaseSpawnCount()
    {
        totalSpawnersLeft++;
    }

    public void DecreaseSpawnCount()
    {
        totalSpawnersLeft--;
    }


    private void CheckSpawnersCount()
    {
        if (totalSpawnersLeft == 0)
        {
            AllSpawnerDestroyed = true;
        }
    }


    public void IncreaseEnemyCount()
    {
        EnemiesCount++;
    }

    public void DecreaseEnemyCount()
    {
        EnemiesCount--;
    }

}
