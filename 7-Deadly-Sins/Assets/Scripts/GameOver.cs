using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    #region Singelton

    public static GameOver instance { get; private set; }

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

    public GameObject GameOverText;


    public void OnGameOverText()
    {
        GameOverText.SetActive(true);
    }

}

   
