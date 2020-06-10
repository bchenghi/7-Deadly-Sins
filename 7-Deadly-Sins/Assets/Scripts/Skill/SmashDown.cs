using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDown : Skill, IUsable
{
    public bool nextAttackDone = true;
    GameObject player;
    PlayerStats playerStats;
    CharacterCombat combat;
    public Sprite Image
    {
        get { return Icon; }
        set { return; }
    }

    public void Use()
    {
        if (isCoolingDown)
        {
            return;
        }
        
        StartCoroutine(attackWithinTime());
        
    }


   
    //Starts the cooldown and add a modifier to player damage. sets a buffer time, activates player combat 
    // If after buffer time is over, no attack is allowed.
    IEnumerator attackWithinTime()
    {
        StartCoroutine(CoolDownRoutine());
        int currentDamage = playerStats.damage.GetValue();
        playerStats.damage.AddModifier(currentDamage * skillLevel);
        nextAttackDone = false;
        combat.SpecialActivated = true;
        yield return new WaitForSeconds(5);
        nextAttackDone = true;
        combat.SpecialActivated = false;
    }

    IEnumerator CoolDownRoutine()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(CooldownTime);
        isCoolingDown = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player;
        playerStats = player.GetComponent<PlayerStats>();
        combat = player.GetComponent<CharacterCombat>();
        Description = "Deals a heavy strike, use within 5 seconds of cast";
        MaxSkillLevel = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
}
