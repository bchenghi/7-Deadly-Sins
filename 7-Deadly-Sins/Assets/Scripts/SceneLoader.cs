using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    #region Singleton
    public static SceneLoader instance;

    void Awake() {
        if (SceneLoader.instance == null) 
        {
            SceneLoader.instance = this;
        } 
        else 
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    [SerializeField]
    Animator animator;
    [SerializeField]
    float transitionDuration = 1f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithTransition(transitionDuration, sceneName));
    }

    public void LoadScene(int sceneNumber) {
        StartCoroutine(LoadSceneWithTransition(transitionDuration, sceneNumber));
    }

    public void LoadFirstStage(int sceneNumber) {
        StartCoroutine(LoadFirstStageWithTransition(transitionDuration, sceneNumber));
    }

    // Before loading new scene delegates need to be cleared as new scene will set up delegates again
    void ClearDelegates() {
        if (Inventory.instance.onItemChangedCallback != null) {
            foreach(Delegate d in Inventory.instance.onItemChangedCallback.GetInvocationList()) {
                Inventory.instance.onItemChangedCallback -= (Inventory.OnItemChanged) d;
            }
        }
        if ( EquipmentManager.instance.onEquipmentChanged != null) {
                foreach(Delegate d in EquipmentManager.instance.onEquipmentChanged.GetInvocationList()) {
                EquipmentManager.instance.onEquipmentChanged -= (EquipmentManager.OnEquipmentChanged) d;
            }
        }
        
        if (GoldCounter.instance.onGoldChange != null) {
            foreach(Delegate d in GoldCounter.instance.onGoldChange.GetInvocationList()) {
                GoldCounter.instance.onGoldChange -= (GoldCounter.OnGoldChange) d;
            }
        }
        
    }


    IEnumerator LoadSceneWithTransition(float transitionDuration, int sceneNumber) {
        animator.SetTrigger("StartTransition");
        SaveLoad.instance.Save();
        ClearDelegates();
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneNumber);
    }

    IEnumerator LoadSceneWithTransition(float transitionDuration, string sceneName) {
        animator.SetTrigger("StartTransition");
        SaveLoad.instance.Save();
        ClearDelegates();
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadFirstStageWithTransition(float transitionDuration, int sceneNumber) {
        animator.SetTrigger("StartTransition");
        ClearDelegates();
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneNumber);
    }


}
