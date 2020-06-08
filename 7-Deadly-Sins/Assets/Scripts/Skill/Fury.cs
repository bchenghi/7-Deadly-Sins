using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fury : Skill, IUsable
{
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
        // Increase Bonus Damage
        StartCoroutine(IncreaseDamageRountine());


    }

    IEnumerator IncreaseDamageRountine()
    {
        // Start cooldown routine
        StartCoroutine(CoolDownRoutine());
        PlayerManager.instance.player.GetComponent<PlayerStats>().damage.AddModifier(5 * skillLevel);
        yield return new WaitForSeconds(5);
        PlayerManager.instance.player.GetComponent<PlayerStats>().damage.RemoveModifier(5 * skillLevel);
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
        Description = "Increases players damage by 5 * skill Level for 5 seconds";
        MaxSkillLevel = 5;
    }

}
