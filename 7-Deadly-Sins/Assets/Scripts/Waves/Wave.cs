using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int numberOfEnemies;
    public GameObject[] enemiesToChooseFrom;

    public float delayBetweenEachSpawn;
}
