using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackAndSlashManager : MonoBehaviour
{
    #region Singelton

    public static HackAndSlashManager instance { get; private set; }

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
    }

    #endregion

    public LevelManager level1Manager;
    public LevelManager level2Manager;
    public LevelManager level3Manager;
    public LiftMovement liftMovement;
    private bool level1Done;
    private bool level2Done;

    private void Update()
    {
        if (level1Manager.allEnemiesDead && !level1Done)
        {
            liftMovement.level1Done = true;
            level1Done = true;
        } 

        if (level2Manager.allEnemiesDead && !level2Done)
        {
            liftMovement.level2Done = true;
            level2Done = true;
        }

        if (level3Manager.allEnemiesDead)
        {
            Debug.Log("Success");
        }
    }


}
