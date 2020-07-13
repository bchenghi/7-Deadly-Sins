using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int enemiesCount;
    public bool allEnemiesDead;
    public BoxCollider liftCollider;
    private bool colliderActivated;


    private void Awake()
    {
        enemiesCount = 0;
    }

    private void Start()
    {
        allEnemiesDead = false;
        Debug.Log(enemiesCount);
    }

    private void Update()
    {
        checkEnemiesCount();
        if (!colliderActivated && allEnemiesDead)
        {
            liftCollider.enabled = true;
            colliderActivated = true;
        }
    }

    public void EnemiesCountIncrease()
    {
        enemiesCount++;
    }

    public void EnemiesCountDecrease()
    {
        enemiesCount--;
    }

    public void checkEnemiesCount()
    {
        if (enemiesCount == 0)
        {
            allEnemiesDead = true;
        }
    }
}
