using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FuryLvl2 : Skill, IUsable
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
        // Increase Bonus Damage
        //effect.transform.position = PlayerManager.instance.player.transform.position;
        base.Use();
        StartCoroutine(IncreaseDamageRountine());
        //StartCoroutine(StartEffect());


    }

    /*IEnumerator StartEffect()
    {
        foreach(ParticleSystem ps in systems)
        {
            ps.Play();
        }
        yield return new WaitForSeconds(2);
        foreach (ParticleSystem ps in systems)
        {
            ps.Stop();
        }
    }
    */

    IEnumerator IncreaseDamageRountine()
    {
        // Start cooldown routine
        StartCoroutine(CoolDownRoutine());
        AudioManager.instance.Play(soundEffect);
        PlayerManager.instance.player.GetComponent<PlayerStats>().damage.AddModifier(5 * skillLevel);
        PlayerManager.instance.player.GetComponent<EffectHandler>().UseEffect(7, 5);
        PlayerManager.instance.player.GetComponent<EffectHandler>().effectNum = 7;
        PlayerManager.instance.player.GetComponent<EffectHandler>().effectOnAndFollow = true;
        yield return new WaitForSeconds(5);
        PlayerManager.instance.player.GetComponent<EffectHandler>().effectOnAndFollow = false;
        PlayerManager.instance.player.GetComponent<PlayerStats>().damage.RemoveModifier(5 * skillLevel);
        
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
        Description = "Increases player's damage by 10 for 5 seconds";
        MaxSkillLevel = 3;
        //systems = effect.GetComponentsInChildren<ParticleSystem>();
    }

}
