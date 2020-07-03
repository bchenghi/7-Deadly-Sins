using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    float delayBetweenWaves;
    [SerializeField]
    bool startNextWaveOnceCleared;

    [SerializeField]
    Wave[] waves;
    // bottom left and upper right corners of spawn area
    [SerializeField]
    Cube[] spawnRegions;
    // each list of enemies refers to 
    List<GameObject>[] spawnedEnemies;

    int waveNumber = 0;
    int numberOfWaves;

    float waveCoolDown;
    bool firstFrame = true;

    void Start() {
        numberOfWaves = waves.Length;
    }


    void Update() {
        if (firstFrame) {   
            StartCoroutine(SpawnWaves());
            firstFrame = false;
        }
    }

    IEnumerator SpawnWaves() {
        while(waveNumber < numberOfWaves) {
            Debug.Log("spawn waves, wave number is " + waveNumber + " number of waves " + numberOfWaves);
            DisplayTextManager.instance.Display("Wave " + (waveNumber + 1) + " started!", 2f);
            StartCoroutine(SpawnWave(waveNumber));
            yield return new WaitForSeconds(delayBetweenWaves);
            waveNumber++;
            Debug.Log("+ 1 to wavenumber");
        }
    }

    IEnumerator SpawnWave(int waveNum) {
        Wave wave = waves[waveNum];
        for (int i = 0; i < wave.numberOfEnemies; i++) {
            Debug.Log("spawn a single wave: i is " + i);
            GameObject enemy = ChooseRandomEnemy(wave.enemiesToChooseFrom);
            //add enemy to list for given wave
            Vector3 spawnLocation = RandomSpawnPosition();
            Instantiate(enemy, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(wave.delayBetweenEachSpawn);
        }
    }


    Vector3 RandomSpawnPosition() {
        int indexOfSpawnRegion = Random.Range(0, spawnRegions.Length);

        Vector3 lowerLeft = spawnRegions[indexOfSpawnRegion].lowerXYZValues;
        Vector3 upperRight = spawnRegions[indexOfSpawnRegion].higherXYZValues;
        float x = Random.Range(lowerLeft[0], upperRight[0]);
        float y = Random.Range(lowerLeft[1], upperRight[1]);
        float z = Random.Range(lowerLeft[2], upperRight[2]);
        return new Vector3(x, y, z);
    }

    GameObject ChooseRandomEnemy(GameObject[] enemies) {
        int numberOfEnemies = enemies.Length;
        int index = Random.Range(0, numberOfEnemies);
        return enemies[index];
    }


}
