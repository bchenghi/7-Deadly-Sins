using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    float delayBetweenWaves;

    [SerializeField]
    Wave[] waves;
    // bottom left and upper right corners of spawn area
    [SerializeField]
    Cube[] spawnRegions;

    int waveNumber = 0;
    int numberOfWaves;

    float waveCoolDown;
    bool firstFrame = true;

    // If all enemies from all waves are killed, done set to true
    [HideInInspector]
    public bool done = false;

    // Tracks enemies spawned.
    List<GameObject> enemiesSpawned = new List<GameObject>();

    void Start() {
        numberOfWaves = waves.Length;
    }

    // In first frame, calls SpawnWaves()
    void Update() {
        if (firstFrame) {   
            StartCoroutine(SpawnWaves());
            firstFrame = false;
        }

        // checks if all enemies spawned from all waves are dead, assigns 'done' variable true or false.
        if (waveNumber == numberOfWaves && !done) {
            bool allDead = true;
            foreach(GameObject obj in enemiesSpawned) {
                if (obj != null) {
                    allDead = false;
                    break;
                }
            } done = allDead;
        }
    }

    // Starts spawning waves, wait for a duration 'delayBetweenWaves' before spawning next wave
    IEnumerator SpawnWaves() {
        while(waveNumber < numberOfWaves) {
            if (waveNumber < numberOfWaves - 1) {
                DisplayTextManager.instance.Display("Wave " + (waveNumber + 1) + " started!", 2f);
            } else {
                DisplayTextManager.instance.Display("Last wave started!", 2f);
            }
            StartCoroutine(SpawnWave(waveNumber));
            yield return new WaitForSeconds(delayBetweenWaves);
            waveNumber++;
        }
    }

    // Spawns a wave. Chooses wave to spawn based on index 'waveNum', from array of waves called 'waves'
    // spawns each enemy after a delay, delay specified in the wave itself
    IEnumerator SpawnWave(int waveNum) {
        Wave wave = waves[waveNum];
        for (int i = 0; i < wave.numberOfEnemies; i++) {
            GameObject enemy = ChooseRandomEnemy(wave.enemiesToChooseFrom);
            Vector3 spawnLocation = RandomSpawnPosition();
            enemy.GetComponent<EffectHandler>().SmokeEffectEvent(spawnLocation, 5, 2f);
            enemy.GetComponent<SoundHandler>().PlaySoundsRandomly("Whoosh", "Whoosh1");
            yield return new WaitForSeconds(0.5f);
            Instantiate(enemy, spawnLocation, Quaternion.identity);
            enemiesSpawned.Add(enemy);
            yield return new WaitForSeconds(wave.delayBetweenEachSpawn);
        }
    }

    // Returns a random position to spawn based on regions to spawn, specified in spawnRegions
    Vector3 RandomSpawnPosition() {
        int indexOfSpawnRegion = Random.Range(0, spawnRegions.Length);

        Vector3 lowerXYZ = spawnRegions[indexOfSpawnRegion].lowerXYZValues;
        Vector3 higherXYZ = spawnRegions[indexOfSpawnRegion].higherXYZValues;
        float x = Random.Range(lowerXYZ[0], higherXYZ[0]);
        float y = Random.Range(lowerXYZ[1], higherXYZ[1]);
        float z = Random.Range(lowerXYZ[2], higherXYZ[2]);
        return new Vector3(x, y, z);
    }

    // Used to choose a random enemy to spawn in SpawnWave()
    GameObject ChooseRandomEnemy(GameObject[] enemies) {
        int numberOfEnemiesInArr = enemies.Length;
        int index = Random.Range(0, numberOfEnemiesInArr);
        return enemies[index];
    }


}
