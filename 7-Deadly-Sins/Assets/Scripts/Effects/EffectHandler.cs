using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        effect.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
    }

    public void EnableAndActivate(int effectNumber)
    {
        Transform effect = effectsManager.returnEffect(effectNumber);
        effect.position = transform.position;
        effectsManager.EnableEffectObject(effectNumber);
        effectsManager.ActivateParticleSystem(effect);
        
    }

    public void EnableAndActivateWithOffSet(int effectNumber, float offx, float offy, float offz) 
    {
        Transform effect = effectsManager.returnEffect(effectNumber);
        effect.position = new Vector3(transform.position.x + offx, transform.position.y + offy, transform.position.z + offz);
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
                effect.Translate(Vector3.forward * 5f * Time.deltaTime);
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

    public void EnemiesToTargetPointEffectEvent(int effectNumber, float duration)
    {
        StartCoroutine(EnemiesToTarget(effectNumber, duration));
    }

    IEnumerator EnemiesToTarget(int effectNumber, float duration)
    {

        Transform effect = Instantiate(effectsManager.returnMichellenousEffects(effectNumber));
        effect.gameObject.SetActive(true);
        effect.position = transform.position;
        effectsManager.ActivateParticleSystem(effect);
        yield return new WaitForSeconds(effect.GetComponent<ParticleSystem>().main.duration);
        effectsManager.DeactivateParticleSystem(effect);
        Destroy(effect.gameObject);
    }


    public void StartbossSpawnEffectEvent(int effectNumber, Vector3 TargetPos)
    {
        effectsManager.EnableMischellenousEffect(effectNumber);
        Transform effect = effectsManager.returnMichellenousEffects(effectNumber);
        effect.position = TargetPos;
        effectsManager.Activate(effect);
    }

    public void StopbossSpawnEffectEvent(int effectNumber)
    {
        effectsManager.DisableMischellenousEffect(effectNumber);
        Transform effect = effectsManager.returnMichellenousEffects(effectNumber);
        effectsManager.DeactivateParticleSystem(effect);
    }

   



    // -------------------------- Smoke effect -----------------------------

    // Activates smoke effect coroutine, which will activate smoke effect for 
    // a given duration at the given position
    public void SmokeEffectEvent(Transform targetTransform, int effectNumber, float duration){
        StartCoroutine(SmokeEffectCoroutine(targetTransform.position, effectNumber, duration));
    }

    public void SmokeEffectEvent(Vector3 position, int effectNumber, float duration) {
        StartCoroutine(SmokeEffectCoroutine(position, effectNumber, duration));
    }



    // called in SmokeEffectEvent method. Activates smoke effect for a given duration at the given position
    public IEnumerator SmokeEffectCoroutine(Vector3 targetPos, int effectNumber, float duration) {
        Transform effect = Instantiate(effectsManager.returnMichellenousEffects(effectNumber));
        effect.gameObject.SetActive(true);
        effect.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);

        effectsManager.ActivateParticleSystem(effect);
        yield return new WaitForSeconds(duration);
        effectsManager.DeactivateParticleSystem(effect);
        Destroy(effect.gameObject, effect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
    }

    // Same as previous smoke effect coroutine, but has additional argument, scale, 
    // can increase or decrease scale of smoke effect
    public IEnumerator SmokeEffectCoroutine(Vector3 targetPos, Vector3 scale, 
    int effectNumber, float duration) {
        Transform effect = Instantiate(effectsManager.returnMichellenousEffects(effectNumber));
        effect.gameObject.SetActive(true);
        effect.position = targetPos;
        Vector3 previousScale = effect.localScale;
        effect.localScale = scale;

        effectsManager.ActivateParticleSystem(effect);
        yield return new WaitForSeconds(duration);
        effectsManager.DeactivateParticleSystem(effect);
        Destroy(effect.gameObject, effect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
    }

    // ----------------------- End of smoke effect --------------------------
}
