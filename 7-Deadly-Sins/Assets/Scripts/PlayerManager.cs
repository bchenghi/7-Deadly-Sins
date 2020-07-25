using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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
        if (player == null)
            player = GameObject.Find("Player");
    }

    #endregion
    
    public GameObject player;



    public void KillPlayer()
    {
        HotKeyBar.instance.DisableAll();
        StartCoroutine(PlayerDeathCoroutine());
    }

    IEnumerator PlayerDeathCoroutine() {
        RuntimeAnimatorController animController = player.GetComponentInChildren<Animator>().runtimeAnimatorController;
        float durationOfDeathAnim = Array.Find(animController.animationClips, x => x.name == "Sword And Shield Death").length;
        yield return new WaitForSeconds(durationOfDeathAnim);
        GameOver.instance.OnGameOverText();
        
    }

    public void LoadScene()
    {
        SceneLoader.instance.LoadWithoutStats(SceneManager.GetActiveScene().buildIndex);
    }

    
}
