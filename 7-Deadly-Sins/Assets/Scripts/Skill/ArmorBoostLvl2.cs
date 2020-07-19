using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBoostLvl2 : Skill, IUsable
{

    
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
        StartCoroutine(IncreaseArmorRountine());
    }

    IEnumerator IncreaseArmorRountine()
    {
        // Start cooldown routine
        StartCoroutine(CoolDownRoutine());
        AudioManager.instance.Play(soundEffect);
        PlayerManager.instance.player.GetComponent<PlayerStats>().armor.AddModifier(10 * skillLevel);
        PlayerManager.instance.player.GetComponent<PlayerController>().walkSpeed -= 1;
        PlayerManager.instance.player.GetComponent<PlayerController>().runSpeed -= 3;
        PlayerManager.instance.player.GetComponent<EffectHandler>().UseEffect(4, 5);
        PlayerManager.instance.player.GetComponent<EffectHandler>().effectNum = 4;
        PlayerManager.instance.player.GetComponent<EffectHandler>().effectOnAndFollow = true;
        
        yield return new WaitForSeconds(5);
        PlayerManager.instance.player.GetComponent<EffectHandler>().effectOnAndFollow = false;
        PlayerManager.instance.player.GetComponent<PlayerStats>().armor.RemoveModifier(10 * skillLevel);
        PlayerManager.instance.player.GetComponent<PlayerController>().walkSpeed += 1;
        PlayerManager.instance.player.GetComponent<PlayerController>().runSpeed += 3;
    }
    
    IEnumerator CoolDownRoutine()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(CooldownTime);
        isCoolingDown = false;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Description = "Increases player's armor by 20 but decreases walkspeed by 1 and runspeed by 3 for 5 seconds";
        MaxSkillLevel = 3;
        //systems = effect.GetComponentsInChildren<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
