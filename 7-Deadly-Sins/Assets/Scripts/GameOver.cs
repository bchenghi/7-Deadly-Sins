using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
 
    public GameObject GameOverText;
    Button button;
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

   
    

    private void Start()
    {
        button = GameOverText.GetComponentInChildren<Button>();
        
    }

    




    public void OnGameOverText()
    {
        SetButton();
        GameOverText.SetActive(true);
    }

    public void SetButton()
    {
        button = GameOverText.GetComponentInChildren<Button>();
        button.onClick.AddListener(PlayerManager.instance.LoadScene);
       
    }

}

   
