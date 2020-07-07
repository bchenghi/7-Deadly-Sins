using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneDatabase : MonoBehaviour
{
    [SerializeField]
    PreviousNextSceneName[] previousNextScenePairs;
    
    public string GetNextScene() {
        string previousScene = PreviousScene.instance.previousSceneName;
        string nextSceneName = null;
        foreach (PreviousNextSceneName pair in previousNextScenePairs) {
            if (pair.previous == previousScene) {
                nextSceneName = pair.next;
                break;
            }
        }
        return nextSceneName;
    }

}
