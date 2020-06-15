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
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;


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
            playerStats.DecreaseSkillPoints();
            index -= 1;
            Debug.Log(index);
            SkillTree.instance.UpgradeWithNumber(index);
            int UIActivated = SkillTree.instance.returnLevel(index) - 1;
            if (UIActivated <= Children[index].Count)
            {
                Transform transformActivated = Children[index][UIActivated];
                transformActivated.GetComponentInChildren<Image>().color = Color.white;
                Children[index][UIActivated].GetComponent<DragDropSkill>().enabled = true;
            }
        } else
        {
            Debug.Log("Not enough skill points");
        }
    }

}
