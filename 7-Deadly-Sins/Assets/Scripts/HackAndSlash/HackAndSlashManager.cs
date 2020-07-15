using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private bool level3Done;
    public bool allLevelDone;
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh.text = "";
        
    }

    private void Update()
    {
        if (level1Manager.allEnemiesDead && !level1Done)
        {
            liftMovement.level1Done = true;
            level1Done = true;
            textMesh.text = "Stage 1 Completed, Proceed to Lift";
            StartCoroutine(wait());
        } 

        if (level2Manager.allEnemiesDead && !level2Done)
        {
            liftMovement.level2Done = true;
            level2Done = true;
            textMesh.text = "Stage 2 Completed, Proceed to Lift";
            StartCoroutine(wait());
        }

        if (level3Manager.allEnemiesDead && !level3Done)
        {
            level3Done = true;
            allLevelDone = true;
            textMesh.text = "All Stages Completed, Proceed to Portal";
            StartCoroutine(wait());
            
            
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        textMesh.text = "";

    }


}
