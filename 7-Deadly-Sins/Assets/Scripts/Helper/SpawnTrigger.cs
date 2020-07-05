using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    GameObject objectToSpawn;
    [SerializeField]
    Vector3 positionToSpawn;
    

    public void Trigger() {
        Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity);
        Destroy(objectToSpawn);
    }
}
