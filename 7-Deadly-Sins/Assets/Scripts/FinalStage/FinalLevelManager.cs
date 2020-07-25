using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevelManager : MonoBehaviour
{
    public bool AllSpawnerDestroyed;
    public Transform TargetPoint;
    public int EnemiesCount;
    EffectHandler effects;
    private bool bossSpawned;
    private bool spawnersDestroyed;
    public GameObject LeonardBoss;
    public GameObject bossHealth;
    public bool bossKilled;
    public TextMeshProUGUI textMesh;
    ITrigger trigger;

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
        trigger = GetComponent<ITrigger>();
        effects = GetComponent<EffectHandler>();
    }

    private void Update()
    {
        CheckSpawnersCount();
        if (AllSpawnerDestroyed && EnemiesCount != 0 && !spawnersDestroyed)
        {
            spawnersDestroyed = true;
            effects.StartbossSpawnEffectEvent(8, TargetPoint.position);
            textMesh.text = "All Spawners destroyed, Something's amiss in the centre!";
            StartCoroutine(Wait());
        } else if (AllSpawnerDestroyed && EnemiesCount == 0 && !bossSpawned)
        {
            bossSpawned = true;
            effects.StopbossSpawnEffectEvent(8);
            GameObject.Instantiate(LeonardBoss, TargetPoint.position, Quaternion.identity);
        }

        if (bossKilled)
        {
            trigger.Trigger();
            textMesh.text = "Final Boss Killed, Enter Portal";
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        textMesh.text = "";
    }

}
