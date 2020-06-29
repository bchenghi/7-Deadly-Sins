using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When 
public class ColliderSceneLoad : MonoBehaviour
{
    SceneLoader sceneLoader;
    [SerializeField]
    string nameOfSceneToLoad;
    void Start() {
        sceneLoader = SceneLoader.instance;
    }
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Player")
            sceneLoader.LoadScene(nameOfSceneToLoad);
    }
}
