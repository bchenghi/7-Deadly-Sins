using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class SkillTree : MonoBehaviour
{
    #region Singleton
    public static SkillTree instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of SkillTree Found");
            return;
        }

        instance = this;
    }
    #endregion

    public string[] level1SkillNames;
    
    public GameObject skillTreeUI;
    [HideInInspector]
    public Skill[] skills;


    [HideInInspector]

    public List<KeyValuePair<Skill, int>> skillTree = new List<KeyValuePair<Skill, int>>();

    public int maxLevel = 3;
    public bool setUpDone = false;

    private void Start()
    {
        SetUpSkillsArrayFromNames();
        for (int i = 0; i < skills.Length; i++)
        {
            skillTree.Add(new KeyValuePair<Skill, int>(skills[i], 1));
        }
    }




    public bool upgrade(Skill skill)
    {
        bool result = false;
        for (int i = 0; i < skillTree.Count; i++)
        {
            if (skillTree[i].Key.Equals(skill))
            {
                int currentLevel = skillTree[i].Value;
                Skill currentSkill = skillTree[i].Key;
                if (currentLevel == currentSkill.MaxSkillLevel)
                {
                    Debug.Log("Already Max Level");
                    result = false;
                }
                else
                {
                    skillTree[i] = new KeyValuePair<Skill, int>(skill, currentLevel + 1);
                    result = true;
                }
                break;
            }
        }
        return result;
    }

    public bool UpgradeWithNumber(int position)
    {
       
        int currentLevel = skillTree[position].Value;
        Skill currentSkill = skillTree[position].Key;
        Debug.Log(position);
        if (currentLevel == currentSkill.MaxSkillLevel)
        {
            Debug.Log("Already Max Level");
            return false;
        }
        else
        {
            skillTree[position] = new KeyValuePair<Skill, int>(skillTree[position].Key, currentLevel + 1);
            return true;
        }
    }

    public int returnLevel(int skillToCheck)
    {
        return skillTree[skillToCheck].Value;
    }


    // Sets up the skillTreeUI object first, then will search for the skills in the skillTreeUI
    public void NewSceneSetUp(GameObject skillTreeUI) {
        this.skillTreeUI = skillTreeUI;
        SetUpSkillsArrayFromNames();
        SetUpSkillTreeFromSkillsArray();
        setUpDone = true;
    }

    // Will form the skills array by finding the skills by name in the skilltreeUI
    void SetUpSkillsArrayFromNames() {
        List<Skill> listOfSkills = new List<Skill>();
        for (int i = 0; i < level1SkillNames.Length ; i++) {
            Transform[] ts = skillTreeUI.transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in ts)
            {
                if (t.gameObject.name == level1SkillNames[i])
                {
                    listOfSkills.Add(t.GetComponent<Skill>());
                }
            }
        }

        skills = new Skill[listOfSkills.Count];
        for (int i = 0; i < listOfSkills.Count; i++) {
            skills[i] = listOfSkills[i];
        }
    }


    // Will assign the skills in skill array to the skillTree skills
    // Method assumes all scenes with skills, will use the same skills in same order in skilltreeui
    void SetUpSkillTreeFromSkillsArray() {
        for (int i = 0; i < skills.Length; i++) {
            Skill skill = skills[i];

            for (int j = 0; j < skillTree.Count ; j++) {
                KeyValuePair<Skill, int> pair = skillTree[j];
                Debug.Log("skill.name: " + skill.name);
                skillTree[j] = new KeyValuePair<Skill, int>(skill, pair.Value);
            }
        }
        Debug.Log("final length of skill tree: " + skillTree.Count);
    }


}
