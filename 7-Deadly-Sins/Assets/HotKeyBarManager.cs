using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeyBarManager : MonoBehaviour
{
    #region Singleton
    public static HotKeyBarManager instance;
    void Awake() {
        if (HotKeyBarManager.instance == null) {
            HotKeyBarManager.instance = this;
        } else {
            Destroy(this);
        }
        
        hotKeyMemory = new IUsable[8];
    }

    #endregion

    // memorises the IUsables in the hotkeys for scene transitions
    [HideInInspector]
    public IUsable[] hotKeyMemory;

    // array of skills scripts belonging to the scene, used for updating hot keys after changing scene, 
    // referencing the skills used in the current scene, instead of old scene. Is setup in newSceneSetUp
    Skill[] iusableSkills;
    // Start is called before the first frame update
    void Start()
    {
        if (iusableSkills != null) {
            foreach(Skill skill in iusableSkills) {
                if (!(skill is IUsable)) {
                    // maybe can remove not isuable skills from array
                    Debug.LogWarning("Skill " + skill + " is not IUsable");
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIUsableSkills(Skill[] skillsArray) {
        iusableSkills = skillsArray;
    }

    public Skill[] GetIUsableSkills() {
        return iusableSkills;
    }
}
