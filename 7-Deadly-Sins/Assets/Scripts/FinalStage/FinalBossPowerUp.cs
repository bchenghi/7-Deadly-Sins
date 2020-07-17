using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalBossPowerUp : MonoBehaviour
{
    public Transform Sword;
    public Transform Shield;
    public Transform faceMask;
    private bool damageAndArmorUp;
    public int damageUp;
    public int armorUp;

    BossAnimator animator;
    EnemyStats stats;
    Enemy enemy;
    ITrigger trigger;
    private FinalBossHealthUI healthUI;

    private void Start()
    {
        animator = GetComponent<BossAnimator>();
        stats = GetComponent<EnemyStats>();
        enemy = GetComponent<Enemy>();
        healthUI = GetComponent<FinalBossHealthUI>();
        trigger = GetComponent<ITrigger>();
    }


    private void Update()
    {
        if (animator.powerUp)
        {
            Sword.gameObject.SetActive(true);
            Shield.gameObject.SetActive(true);
            faceMask.gameObject.SetActive(false);
            enemy.name = "Leonard";
            healthUI.SetName();

            if (!damageAndArmorUp)
            {
                stats.damage.AddModifier(damageUp);
                stats.armor.AddModifier(armorUp);
                damageAndArmorUp = true;
                trigger.Trigger();
            }
        }

        if (stats.currentHealth <= 0)
        {
            FinalLevelManager.instance.bossKilled = true;
        }
    }
}
