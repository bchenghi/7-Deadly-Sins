using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{
    public Sprite Icon;
    public int skillLevel;
    public bool isCoolingDown;
    public float CooldownTime;
    public int ManaCost;

    [HideInInspector]
    public string Description;

    [HideInInspector]
    public int MaxSkillLevel;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
