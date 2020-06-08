using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillTreeUI : MonoBehaviour
{

    public static int numSkills;
    //All skill Columns
    public GameObject[] numberOfSkills = new GameObject[numSkills];


    List<List<Transform>> Children;
    


    //All Buttons corresponding to each Column1
    public Button button1;
    public Button button2;
    public Button button3;


    // Start is called before the first frame update
    void Start()
    {
        Children = new List<List<Transform>>();

        for(int i = 0; i < numberOfSkills.Length; i++)
        {
            Children.Add(new List<Transform>());
            for (int t = 0; t<numberOfSkills[i].transform.childCount; t++)
            {
                Children[i].Add(numberOfSkills[i].transform.GetChild(t).GetChild(0));
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Upgrade(int index)
    {
        index -= 1;
        Debug.Log(index);
        SkillTree.instance.UpgradeWithNumber(index);
        int UIActivated = SkillTree.instance.returnLevel(index) - 1;
        if (UIActivated <= Children[index].Count)
        {
            Transform transformActivated = Children[index][UIActivated];
            transformActivated.GetComponentInChildren<Image>().color = Color.white;
        }
    }

}
