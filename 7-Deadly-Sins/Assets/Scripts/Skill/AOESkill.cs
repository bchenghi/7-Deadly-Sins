using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESkill : Skill, IUsable
{
    public GameObject sphereCollider;
    public float damage;
    public float stunTime;
    public int skillEffectNumber;
    public Sprite Image
    {
        get { return Icon; }
        set { return; }
    }

    public override void Start()
    {
        base.Start();
        Description = "Cast an AOE spell, dealing " + damage + " damage to nearby enemies and stunning them for " + stunTime + " seconds";
        MaxSkillLevel = 3;
    }
    public override void Use()
    {
        
        if (isCoolingDown || !EnoughMana())
        {
            return;
        }
        base.Use();
        effect();
        CreateCollider();
        StartCoroutine(CoolDownRoutine());
       

    }


    private void effect()
    {
        PlayerManager.instance.player.GetComponent<EffectHandler>().UseEffect(skillEffectNumber, 2);
        PlayerManager.instance.player.GetComponent<EffectHandler>().SetEffectAtPlayer(skillEffectNumber);

    }

    private void CreateCollider()
    {
        GameObject Go = GameObject.Instantiate(sphereCollider, PlayerManager.instance.player.transform.position, Quaternion.identity);
        Go.transform.GetComponent<AOECollider>().damage = damage;
        Go.transform.GetComponent<AOECollider>().stunTime = stunTime;
        Go.transform.GetComponent<AOECollider>().isActive = true;
        StartCoroutine(DestroyCollider(Go));

    }

    IEnumerator DestroyCollider(GameObject Go)
    {
        StartCoroutine(SetInactive(Go));
        yield return new WaitForSeconds(stunTime + 0.2f);
        Destroy(Go);
    }

    IEnumerator CoolDownRoutine()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(CooldownTime);
        isCoolingDown = false;
    }

    IEnumerator SetInactive(GameObject Go)
    {
        yield return new WaitForSeconds(0.2f);
        Go.transform.GetComponent<AOECollider>().isActive = false;
    }
}
