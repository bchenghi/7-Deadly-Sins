using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextSceneButton : MonoBehaviour
{
    [SerializeField]
    NextSceneDatabase nextSceneDatabase;

    Button button;

    void Start() 
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => LoadNextSceneFromPreviousSceneName());
    }

    void LoadNextSceneFromPreviousSceneName() {
        SceneLoader.instance.LoadScene(nextSceneDatabase.GetNextScene());
    }
}
