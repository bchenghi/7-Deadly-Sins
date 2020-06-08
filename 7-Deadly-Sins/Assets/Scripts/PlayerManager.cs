using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    
    #region Singelton

    public static PlayerManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            
            
        } else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public GameObject player;


    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
