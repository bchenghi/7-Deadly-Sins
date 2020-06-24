using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets up necessary components 
public class NewSceneSetUp : MonoBehaviour
{

    [SerializeField]
    SkinnedMeshRenderer targetMesh;

    [SerializeField]
    GameObject player;

    [SerializeField]
    string startSound;
    // Sets player and target mesh, and sets cursor to visible and unlocked
    void Awake(){
        if (PlayerManager.instance.player == null) {
            PlayerManager.instance.player = player;
        if (EquipmentManager.instance.targetMesh == null) {
            EquipmentManager.instance.targetMesh = targetMesh;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        }
    }

    void Start() {
        foreach(Equipment equipment in EquipmentManager.instance.currentEquipment){
            if (equipment != null){
                // set equipment mesh will make the player model wear the equipment
                EquipmentManager.instance.SetEquipmentMesh(equipment);
            }

        }
        if (startSound != null) {
            AudioManager.instance.StopPlayingAll();
            AudioManager.instance.Play(startSound);
        }
    }

}
