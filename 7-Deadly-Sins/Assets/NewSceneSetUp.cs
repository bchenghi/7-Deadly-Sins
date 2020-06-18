using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets up necessary components 
public class NewSceneSetUp : MonoBehaviour
{

    // Sets player 
    void Awake(){
        if (PlayerManager.instance.player == null) {
            PlayerManager.instance.player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        }
    }

}
