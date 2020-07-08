using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    float delayBeforeStartWaves;
    [SerializeField]
    float delayBetweenWaves;

    [SerializeField]
    Wave[] waves;

    // regions to spawn gameobjects
    [SerializeField]
    Regions regionsToSpawn;
    [SerializeField]
    bool infiniteLoop;

    int waveNumber = 0;
    int numberOfWaves;

    bool wavesStarted = false;

    // If all enemies from all waves are killed, done set to true
    [HideInInspector]
    public bool done = false;

    // Tracks enemies spawned.
    List<GameObject> enemiesSpawned = new List<GameObject>();
    // Each list represents each wave, each list contains the enemies spawned by that wave.
    List<GameObject>[] enemiesInWaves;
    float waveCoolDown = 0;
    // array of boolean values, each index representing a wave. True if wave is done spawning.
    bool[] wavesDoneSpawning;
    void Start() {
        numberOfWaves = waves.Length;
        
        enemiesInWaves = new List<GameObject>[numberOfWaves];
        for (int i = 0; i< numberOfWaves; i++) {
            enemiesInWaves[i] = new List<GameObject>();
        }

        ResetWavesDoneSpawning();
    }

    void Update() {
        waveCoolDown += Time.deltaTime;


        if (delayBeforeStartWaves > 0) {
            delayBeforeStartWaves -= Time.deltaTime;
        }
        if (!infiniteLoop && delayBeforeStartWaves <= 0 && !wavesStarted) {   
            //Debug.Log("spawnwaves called");
            StartCoroutine(SpawnWaves());
            wavesStarted = true;
        }

        if (infiniteLoop && delayBeforeStartWaves <= 0 && !wavesStarted) {
            StartCoroutine(SpawnWavesInfinite());
            wavesStarted = true;
        }

        // checks if all enemies spawned from all waves are dead, assigns 'done' variable true or false.
        if (waveNumber == numberOfWaves && !done && !infiniteLoop) {
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
        //Debug.Log("inside spawnwaves");
        while(waveNumber < numberOfWaves) {
            int currentWaveNumber = waveNumber;
            //Debug.Log("entered while loop in spawnwaves");
            if (waveNumber < numberOfWaves - 1)  {
                DisplayTextManager.instance.Display("Wave " + (waveNumber + 1) + " started!", 2f);
            } else {
                DisplayTextManager.instance.Display("Last wave started!", 2f);
            }
            StartCoroutine(SpawnWave(waveNumber));
            

            waveCoolDown = 0;
            while (waveCoolDown < delayBetweenWaves) {

                if (CheckCurrentAndBeforeWavesCleared(currentWaveNumber)) {
                    Debug.Log("check cleared wave");
                    waveCoolDown = delayBetweenWaves;
                    yield return new WaitForSeconds(0);
                    break;
                }
                else 
                {
                    yield return new WaitForSeconds(0);
                }
            }
            yield return new WaitForSeconds(0);
            waveNumber++;
        }
    }

    // Spawns a wave. Chooses wave to spawn based on index 'waveNum', from array of waves called 'waves'
    // spawns each enemy after a delay, delay specified in the wave itself
    IEnumerator SpawnWave(int waveNum) {
        Wave wave = waves[waveNum];
        for (int i = 0; i < wave.numberOfEnemies; i++) 
        {
            GameObject enemy = ChooseRandomEnemy(wave.enemiesToChooseFrom);
            Vector3 spawnLocation = RandomSpawnPosition();
            
            enemy.GetComponent<EffectHandler>().SmokeEffectEvent(spawnLocation, 5, 2f);

            yield return new WaitForSeconds(0.5f);

            GameObject newEnemy = Instantiate(enemy, spawnLocation, Quaternion.identity);
            
            string[] sounds = new string[] {"Whoosh", "Whoosh1"};
            enemy.GetComponent<SoundHandler>().PlaySoundRandomly(sounds, enemy.transform);

            enemiesSpawned.Add(newEnemy);
            enemiesInWaves[waveNum].Add(newEnemy);
            yield return new WaitForSeconds(wave.delayBetweenEachSpawn);
        }
        wavesDoneSpawning[waveNum] = true;
    }

    IEnumerator SpawnWavesInfinite() {
        while(waveNumber < numberOfWaves && !done) {
            if (waveNumber == 0) {
                ResetWavesDoneSpawning();
            }
            int currentWaveNumber = waveNumber;
            //Debug.Log("entered while loop in spawnwaves");
            StartCoroutine(SpawnWave(waveNumber));
            

            waveCoolDown = 0;
            while (waveCoolDown < delayBetweenWaves) {

                if (CheckCurrentAndBeforeWavesCleared(currentWaveNumber)) {
                    Debug.Log("check cleared wave");
                    waveCoolDown = delayBetweenWaves;
                    yield return new WaitForSeconds(0);
                    break;
                }
                else 
                {
                    yield return new WaitForSeconds(0);
                }
            }

            if (waveNumber < numberOfWaves - 1) 
            {
                waveNumber++;
            }
            else 
            {
                waveNumber = 0;
            }
            yield return new WaitForSeconds(0);
        }
        
    }

    // Returns a random position to spawn based on regions to spawn, specified in spawnRegions
    Vector3 RandomSpawnPosition() {
        return regionsToSpawn.RandomPosition();
    }

    // Used to choose a random enemy to spawn in SpawnWave()
    GameObject ChooseRandomEnemy(GameObject[] enemies) {
        int numberOfEnemiesInArr = enemies.Length;
        int index = Random.Range(0, numberOfEnemiesInArr);
        return enemies[index];
    }

    // Checks if wave of 'waveNum' and all previous waves have been cleared, 
    // returns true or false depending on whether the player
    // killed all enemies in the waves
    bool CheckCurrentAndBeforeWavesCleared(int waveNum) {
        bool wavesUpToWavenumSpawned = true;
        for (int i = 0; i <= waveNum; i++) {
            if (!wavesDoneSpawning[i]) {
                wavesUpToWavenumSpawned = false;
                break;
            }
        }

        if (wavesUpToWavenumSpawned) {
            bool allEnemiesKilled = true;
            for (int i = 0; i <= waveNum && allEnemiesKilled; i++) {
                foreach(GameObject enemy in enemiesInWaves[i]) {
                    if (enemy != null) {
                        allEnemiesKilled = false;
                        break;
                    }
                }
            }
            return allEnemiesKilled;
        } 
        else 
        {
            return false;
        }
    }


    void ResetWavesDoneSpawning() {
        wavesDoneSpawning = new bool[numberOfWaves];
        for (int i = 0; i<numberOfWaves; i++) {
            wavesDoneSpawning[i] = false;
        }
    }
        


}
