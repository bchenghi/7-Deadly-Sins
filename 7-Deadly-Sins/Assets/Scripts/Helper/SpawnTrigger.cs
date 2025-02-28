﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    GameObject objectToSpawn;
    [SerializeField]
    Vector3 positionToSpawn;
    

    public void Trigger() {
        GameObject spawn = Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity);
        spawn.SetActive(true);
    }
}
