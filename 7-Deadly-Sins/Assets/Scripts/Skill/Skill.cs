using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{
    public PlayerStats playerStats;
    public Sprite Icon;
    public int skillLevel;
    public bool isCoolingDown;
    public float CooldownTime;
    public int ManaCost;

    [HideInInspector]
    public string Description;

    [HideInInspector]
    public int MaxSkillLevel;


    public virtual void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

    }

    public virtual void Use()
    {
        playerStats.DecreaseMana(ManaCost);
    }

    public bool EnoughMana()
    {
        
        if (ManaCost > playerStats.CurrentMana)
        {
            return false;
        } else
        {
            return true;
        }
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
