using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SkillTreeUI : MonoBehaviour
{

    public static int numSkills;
    //All skill Columns
    public GameObject[] numberOfSkills = new GameObject[numSkills];
    PlayerStats playerStats;


    List<List<Transform>> Children;
    


    //All Buttons corresponding to each Column1
   

    public GameObject Counter;
    Transform counterTransform;
    


    // Start is called before the first frame update
    void Start()
    {
        counterTransform = Counter.transform;
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        Children = new List<List<Transform>>();

        for(int i = 0; i < numberOfSkills.Length; i++)
        {
            Children.Add(new List<Transform>());
            for (int t = 0; t<numberOfSkills[i].transform.childCount; t++)
            {
                Children[i].Add(numberOfSkills[i].transform.GetChild(t).GetChild(0));
            }
        }

        for (int i = 0; i < numberOfSkills.Length; i++)
        {
            for (int k = 0; k < Children[i].Count; k++)
            {
                if (k != 0)
                {
                    Children[i][k].GetComponent<DragDropSkill>().enabled = false;
                }
            }
        }

        NewSceneSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        counterTransform.GetComponent<TextMeshProUGUI>().text = "Skill Points: " + playerStats.SkillPoints;
    }


    public void Upgrade(int index)
    {
        if (playerStats.SkillPoints > 0)
        {
            index -= 1;
            if (SkillTree.instance.UpgradeWithNumber(index)) {
                playerStats.DecreaseSkillPoints();
            
                Debug.Log(index);
                
                int UIActivated = SkillTree.instance.returnLevel(index) - 1;
                if (UIActivated - 1 >= 0) {
                    Children[index][UIActivated - 1].GetComponent<DragDropSkill>().enabled = false;
                }
                if (UIActivated <= Children[index].Count)
                {
                    Transform transformActivated = Children[index][UIActivated];
                    transformActivated.GetComponentInChildren<Image>().color = Color.white;
                    Children[index][UIActivated].GetComponent<DragDropSkill>().enabled = true;
                }
            } 
            else 
            {
                Debug.Log("Already max level");
                DisplayTextManager.instance.Display("Already Max Level!", 2f);
            }
            
        } else
        {
            Debug.Log("Not enough skill points");
            DisplayTextManager.instance.Display("Not enough Skill Points", 2f);
        }
    }

    // upgrades the ui, and enables the slot to be dragged
    public void UpgradeSkillInUI(int skillNum, int level) {
        skillNum -= 1;
        //Debug.Log("index of skill that is turned white"+ skillNum);
        //int UIActivated = SkillTree.instance.returnLevel(index) - 1;
        //Debug.Log("skill level that is achieved" + UIActivated);
        for (int levelToUpgrade = 0; levelToUpgrade < level ; levelToUpgrade++) {
            Transform transformActivated = Children[skillNum][levelToUpgrade];
            transformActivated.GetComponentInChildren<Image>().color = Color.white;
            if (levelToUpgrade - 1 >= 0) {
                Children[skillNum][levelToUpgrade - 1].GetComponent<DragDropSkill>().enabled = false;
            }
            Children[skillNum][levelToUpgrade].GetComponent<DragDropSkill>().enabled = true;
        }
        
    }

    // Upgrades the skill tree ui based on the skill tree
    void NewSceneSetUp() {
        for (int skillNum = 0; skillNum < SkillTree.instance.skillTree.Count ; skillNum++) {
            KeyValuePair<Skill, int> pair = SkillTree.instance.skillTree[skillNum];
            UpgradeSkillInUI(skillNum + 1, pair.Value);
        }
    }

}
