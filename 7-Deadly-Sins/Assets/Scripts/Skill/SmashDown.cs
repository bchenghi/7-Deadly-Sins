using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDown : Skill, IUsable
{
    public bool nextAttackDone = true;
    GameObject player;
    EffectHandler effectHandler;
    CharacterCombat combat;
    public Sprite Image
    {
        get { return Icon; }
        set { return; }
    }

    public override void Use()
    {
        
        if (isCoolingDown || !EnoughMana())
        {
            return;
        }
        base.Use();
        StartCoroutine(attackWithinTime());
        
    }


   
    //Starts the cooldown and add a modifier to player damage. sets a buffer time, activates player combat 
    // If after buffer time is over, no attack is allowed.
    IEnumerator attackWithinTime()
    {
        StartCoroutine(CoolDownRoutine());
        AudioManager.instance.Play(soundEffect);
        int currentDamage = playerStats.damage.GetValue();
        playerStats.damage.AddModifier(currentDamage * skillLevel);
        nextAttackDone = false;
        combat.SpecialActivated = true;
        effectHandler.SmashDownSwitches[0] = true;
        yield return new WaitForSeconds(5);
        effectHandler.SmashDownSwitches[0] = false;
        playerStats.damage.RemoveModifier(currentDamage * skillLevel);
        nextAttackDone = true;
        combat.SpecialActivated = false;
    }

    IEnumerator CoolDownRoutine()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(CooldownTime);
        isCoolingDown = false;
    }

    public void Awake() {
        Description = "Deals a heavy strike (currentDamage * skill level), Press E in front of enemy within 5 seconds of cast";
    }


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        player = PlayerManager.instance.player;
        effectHandler = player.GetComponent<EffectHandler>();
        combat = player.GetComponent<CharacterCombat>();
        MaxSkillLevel = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
}
