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

    public Skill[] skills;

    List<KeyValuePair<Skill, int>> skillTree = new List<KeyValuePair<Skill, int>>();

    private void Start()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skillTree.Add(new KeyValuePair<Skill, int>(skills[i], 1));
        }
    }


    public void upgrade(Skill skill)
    {
        for (int i = 0; i < skillTree.Count; i++)
        {
            if (skillTree[i].Key.Equals(skill))
            {
                int currentLevel = skillTree[i].Value;
                Skill currentSkill = skillTree[i].Key;
                if (currentLevel == currentSkill.MaxSkillLevel)
                {
                    Debug.Log("Already Max Level");
                }
                else
                {
                    skillTree[i] = new KeyValuePair<Skill, int>(skill, currentLevel + 1);
                }
                break;
            }
        }
    }

    public void UpgradeWithNumber(int position)
    {
       
        int currentLevel = skillTree[position].Value;
        Skill currentSkill = skillTree[position].Key;
        if (currentLevel == currentSkill.MaxSkillLevel)
        {
            Debug.Log("Already Max Level");
        }
        else
        {
            skillTree[position] = new KeyValuePair<Skill, int>(skillTree[position].Key, currentLevel + 1);
        }
    }

    public int returnLevel(int skillToCheck)
    {
        return skillTree[skillToCheck].Value;
    }


}
