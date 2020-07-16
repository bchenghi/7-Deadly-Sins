using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSpawner : MonoBehaviour
{
    public GameObject EnemyToSpawn;
    public bool SpawnOnCooldown;
    public float SpawnCooldownTime;
    private bool canSpawn;
    CharacterStats stats;

    private void Start()
    {
        SpawnOnCooldown = false;
        stats = GetComponent<CharacterStats>();
    }


    private void Update()
    {
        if (stats.currentHealth > 0) 
        {
            SpawnEnemies();
        }
    }


    IEnumerator SpawnCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnOnCooldown = false;
    }


    private void SpawnEnemies()
    {
        if (!SpawnOnCooldown)
        {
            Debug.Log("Spawning Enemies");
            GameObject Go = GameObject.Instantiate(EnemyToSpawn, transform.position, Quaternion.identity);
            Go.transform.position = new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), transform.position.y, Random.Range(transform.position.z - 1, transform.position.z + 1));
            SpawnOnCooldown = true;
            StartCoroutine(SpawnCoolDown(SpawnCooldownTime));
        }
        
    }



}
