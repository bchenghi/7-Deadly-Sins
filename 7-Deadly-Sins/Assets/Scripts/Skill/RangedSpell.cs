﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : Skill, IUsable
{
    GameObject player;
    CharacterCombat combat;
    EffectsManager effectsManager;
    EffectHandler effectHandler;
    public float spellRadius = 10f;
    bool hasActivated = false;
    public Transform target;
    int minDamage;
    int maxDamage;
    public int skilleffectNumber;
    public float offx;
    public float offy;
    public float offz;
 

    public void Awake() {
        if (skillLevel == 1)
        {
            minDamage = 15;
            maxDamage = 20;
        }
        else if (skillLevel == 2)
        {
            minDamage = 25;
            maxDamage = 30;
        }
        else
        {
            minDamage = 35;
            maxDamage = 40;
        }
        Description = "Cast a fire ball dealing " + minDamage + " - " + maxDamage + " damage";
    }
    

    public Sprite Image
    {
        get { return Icon; }
        set { return; }
    }


    public override void Use()
    {

        if (isCoolingDown)
        {
            //DisplayTextManager.instance.Display("Skill Cooling Down", 2f);
            return;
        }
        if (!EnoughMana()) {
            //DisplayTextManager.instance.Display("Not enough mana", 2f);
            return;
        }
        // Increase Bonus Damage
        //effect.transform.position = PlayerManager.instance.player.transform.position;
        
        //base.Use();
        ActivateSpell();
            
        
        
        //StartCoroutine(StartEffect());


    }

    public void CastSpell()
    {

        effectHandler.TargetEnemy(skilleffectNumber, target);

    }

    public void ActivateSpell()
    {
        
       
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        int interactableMask = LayerMask.GetMask("Interactable");
        if (Physics.Raycast(ray, out hit, 100, interactableMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && hit.transform.gameObject.GetComponent<Enemy>() != null)
            {
                float distance = Vector3.Distance(player.transform.position, interactable.transform.position);
                if (distance <= spellRadius)
                {
                    player.GetComponent<PlayerAnimator>().CastRangeSpell();
                    target = hit.collider.GetComponent<Interactable>().transform;
                    if ((target.GetComponent<EnemyStats>() &&
                    target.GetComponent<EnemyStats>().currentHealth > 0) || 
                    (target.GetComponent<ClownStats>() && 
                    target.GetComponent<ClownStats>().currentHealth > 0))
                    {
                        AudioManager.instance.Play(soundEffect);
                        hasActivated = true;
                        effectHandler.targetHit = false;
                        StartCoroutine(CoolDownRoutine());
                        effectHandler.EnableAndActivate(skilleffectNumber);
                        base.Use();
                    }
                        

                }
            }
        }
        
        
        
       
        
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
        player = PlayerManager.instance.player;
        combat = player.GetComponent<CharacterCombat>();
        effectsManager = EffectsManager.instance;
        effectHandler = player.GetComponent<EffectHandler>();


        MaxSkillLevel = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasActivated && effectHandler.targetHit == false)
        {
            CastSpell();
        }

        if (hasActivated)
        {
            if (effectHandler.targetHit)
            {
                
                target.GetComponent<CharacterStats>().TakeDamage(Random.Range(minDamage,maxDamage));
                
                hasActivated = false;
            }
        }


    }
}
