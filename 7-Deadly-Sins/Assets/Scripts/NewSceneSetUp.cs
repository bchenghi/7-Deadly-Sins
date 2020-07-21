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

    [SerializeField]
    GameObject GameOverUI;


    [SerializeField]
    GameObject skillTreeUI;

    Skill[] skills;

    [SerializeField]
    string[] level1SkillNames;
 

    
    

    // Sets up components in gamemanager after transitioning to new scene, 
    // and sets cursor to visible and unlocked
    void Awake(){
        
        if (PlayerManager.instance != null && PlayerManager.instance.player == null) {
            PlayerManager.instance.player = player;
        if (EquipmentManager.instance != null && EquipmentManager.instance.targetMesh == null) {
            EquipmentManager.instance.targetMesh = targetMesh;
        } if (EffectsManager.instance != null && EffectsManager.instance.EffectsPool == null) {
            EffectsManager.instance.EffectsPool = effectsPool;
        } if (EffectsManager.instance != null && EffectsManager.instance.MichellenousEffects == null) {
            EffectsManager.instance.MichellenousEffects = miscelleaneousEffects;
        } 
        if (GameOver.instance != null && GameOver.instance.GameOverText == null) {
            GameOver.instance.GameOverText = GameOverUI;
            GameOver.instance.SetButton();
        }
        
        // sets up the skills for hotkeys to reference
        if (HotKeyBarManager.instance != null) {
            skills = skillTreeUI.GetComponentsInChildren<Skill>(true);
            HotKeyBarManager.instance.SetIUsableSkills(skills);
            HotKeyBar.instance.EnableAll();
        

        }

        // Sets up skills array in skill tree based on skill names sepcified in level1SkillNames
        if (SkillTree.instance != null)
            SkillTree.instance.NewSceneSetUp(skillTreeUI);
        

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        }
    }


    // Prepares the scene, such as making the player wear equipment, play bgm, 
    // disable all effects, and load stats into scene
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
        EffectsManager.instance.DisableAllEffects();
        SaveLoad.instance.Load();
    }

}
