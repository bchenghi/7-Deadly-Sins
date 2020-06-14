using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    // Bool for SmashDown
    public bool[] SmashDownSwitches;

    public int effectNum = 0; //refers to the effect number in hierachy
    public bool effectOnAndFollow = false; // set to on in skill script
    EffectsManager effectsManager;
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
                Debug.Log(number);
                StartCoroutine(SkillCoolDown(number, 6));
                Transform effect = effectsManager.returnEffect(number);
                effect.rotation = transform.rotation;
                effect.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                break;
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
}
