using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets up necessary components 
public class NewSceneSetUp : MonoBehaviour
{
    [SerializeField]
    GameObject effectsPool;
    [SerializeField]
    GameObject miscelleaneousEffects;

    [SerializeField]
    SkinnedMeshRenderer targetMesh;

    [SerializeField]
    GameObject player;

    [SerializeField]
    string startSound;

    [SerializeField]
    GameObject hotKeysParent;
    // Sets player and target mesh, and sets cursor to visible and unlocked
    void Awake(){
        if (PlayerManager.instance.player == null) {
            PlayerManager.instance.player = player;
        if (EquipmentManager.instance.targetMesh == null) {
            EquipmentManager.instance.targetMesh = targetMesh;
        } if (EffectsManager.instance.EffectsPool == null) {
            EffectsManager.instance.EffectsPool = effectsPool;
        } if (EffectsManager.instance.MichellenousEffects == null) {
            EffectsManager.instance.MichellenousEffects = miscelleaneousEffects;
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
        EffectsManager.instance.Start();
        SaveLoad.instance.Load();
    }

}
