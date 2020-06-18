using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    // Bool for SmashDown
    public bool[] SmashDownSwitches;

    public int effectNum = 0; //refers to the effect number in hierachy
    public bool effectOnAndFollow = false; // set to on in skill script
    EffectsManager effectsManager;
    Transform targetEnemy;
    public bool targetHit = false;
    /*public void EffectEvent(int number)
    {
        effectsManager.EnableEffectObject(number);
        Transform effect = effectsManager.returnEffect(number);
        effect.position = new Vector3(transform.position.x, transform.position.y + 0.9f , transform.position.z);
        effect.rotation = transform.rotation;
        effectsManager.ActivateParticleSystem(effect);
        
    }
    */


    // this Event is for SmashDown
    public void EffectEvent(int number) 
    {
        for (int i = 0; i < SmashDownSwitches.Length; i++)
        {
            if (SmashDownSwitches[i] == true && number == i)
            {
 
                StartCoroutine(SkillCoolDown(number, 6));
                Transform effect = effectsManager.returnEffect(number);
                Vector3 playerPos = transform.position;
                Vector3 playerDirection = transform.forward;
                float spawnDistance = 1;
                effect.rotation = transform.rotation;
                effect.position = playerPos + playerDirection * spawnDistance;
            }
        }
        
        

    }
    // Start is called before the first frame update
    void Start()
    {
        effectsManager = EffectsManager.instance;
        SmashDownSwitches = new bool[3];
    }

    //Activates the skill, after skill use time is over, deactivate after skill use time
    public void UseEffect(int effectNumber, int useTime)
    {
        
        StartCoroutine(SkillCoolDown(effectNumber, useTime));
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(effectOnAndFollow)
        {
            FollowPlayer(effectNum);
        }
    }

    IEnumerator SkillCoolDown(int effectNumber, int cooldown)
    {
        effectsManager.EnableEffectObject(effectNumber);
        Transform effect = effectsManager.returnEffect(effectNumber);
        effectsManager.ActivateParticleSystem(effect);
        yield return new WaitForSeconds(cooldown);
        effectsManager.DeactivateParticleSystem(effect);
        effectsManager.DisableEffectObject(effectNumber);
    }

    //Makes the effect follow player when played
    public void FollowPlayer(int effectNumber)
    {
        Transform effect = effectsManager.returnEffect(effectNumber);
        effect.position = transform.position;
    }

    public void EnableAndActivate(int effectNumber)
    {
        Transform effect = effectsManager.returnEffect(effectNumber);
        effect.position = transform.position;
        effectsManager.EnableEffectObject(effectNumber);
        effectsManager.ActivateParticleSystem(effect);
        
    }

    public void DisableAndDeactivate(int effectNumber)
    {
        effectsManager.DisableEffectObject(effectNumber);
        Transform effect = effectsManager.returnEffect(effectNumber);
        effectsManager.DeactivateParticleSystem(effect);
    }

    public void TargetEnemy(int effectNumber, Transform target)
    {
        
        targetEnemy = target;
        Transform effect = effectsManager.returnEffect(effectNumber);
        if (targetEnemy != null)
        {
            Vector3 targetPos = new Vector3(targetEnemy.position.x, targetEnemy.position.y, targetEnemy.position.z);
            effect.LookAt(targetPos);
            float distanceTo = Vector3.Distance(targetEnemy.position, effect.position);

            if (distanceTo > 0.3f)
            {
                effect.Translate(Vector3.forward * 2f * Time.deltaTime);
            } else
            {
                targetHit = true;
                DisableAndDeactivate(effectNumber);
                Debug.Log("Target Hit by Spell");
            }
        }
    }

    public void BloodEffectEvent(Transform targetPos, int effectNumber)
    {
        effectNumber = Random.Range(0, effectNumber);
        effectsManager.EnableMischellenousEffect(effectNumber);
        Transform effect = effectsManager.returnMichellenousEffects(effectNumber);
        effect.position = new Vector3(targetPos.position.x, targetPos.position.y + 0.7f, targetPos.position.z);
        effectsManager.ActivateParticleSystem(effect);

    }

    public void HealEffectEvent(int effectNumber)
    {
        effectsManager.EnableMischellenousEffect(effectNumber);
        Transform effect = effectsManager.returnMichellenousEffects(effectNumber);
        effect.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
        effectsManager.ActivateParticleSystem(effect);
    }

    
    


    
}
