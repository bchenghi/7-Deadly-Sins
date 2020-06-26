using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class BossEffectHandler : MonoBehaviour
{
    public bool PowerUp = false;
    public int bossPowerUpEffectNumber;
    BossAnimator bossAnimator;
    EnemyStats enemyStats;
    public bool CameraShake = false;


    private void Start()
    {
        bossAnimator = GetComponent<BossAnimator>();
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if (!PowerUp)
        {
            if (enemyStats.currentHealth <= bossAnimator.triggerPowerUpHealth)
            {
                ActivatePowerUpEffect(bossPowerUpEffectNumber);
                AudioManager.instance.Play("TutorialBossRoar");
                CameraShake = true;
                PowerUp = true;
                //CameraShaker.Instance.ShakeOnce(10f, 4f, 0.1f, 0.1f);
                StartCoroutine(CameraShakerBuffer());
                
            }
        }
    }

    public void ActivatePowerUpEffect(int effectNumber)
    {
        EffectsManager.instance.EnableMischellenousEffect(effectNumber);
        Transform effect = EffectsManager.instance.returnMichellenousEffects(effectNumber);
        EffectsManager.instance.ActivateParticleSystem(effect);
        effect.position = transform.position;
        StartCoroutine(PowerUpEffectTime(effectNumber));
    }

    IEnumerator PowerUpEffectTime(int effectNumber)
    {
        yield return new WaitForSeconds(3);
        Transform effect = EffectsManager.instance.returnMichellenousEffects(effectNumber);
        EffectsManager.instance.DeactivateParticleSystem(effect);
        EffectsManager.instance.DisableMischellenousEffect(effectNumber);
    }

    IEnumerator CameraShakerBuffer()
    {
        yield return new WaitForSeconds(0.1f);
        CameraShake = false;
    }
}
