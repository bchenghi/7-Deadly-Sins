using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousScene : MonoBehaviour
{

    #region Singleton
    public static PreviousScene instance;

    void Awake() {
        if (PreviousScene.instance == null) {
            PreviousScene.instance = this;
            previousSceneName = "Tutorial";
        } else {
            Destroy(this.gameObject);
        }
    }
    #endregion

    [HideInInspector]
    public string previousSceneName;

    public void UpdatePreviousSceneName(string name) {
        previousSceneName = name;
    }
}
