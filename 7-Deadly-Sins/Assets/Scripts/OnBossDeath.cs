using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnBossDeath : MonoBehaviour
{
    public Transform BossSlainText;
    private bool bossDied;
    EnemyStats enemyStats;
    private bool TextAppeared;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        CheckHealth();
        if (bossDied && !TextAppeared)
        {
            StartCoroutine(BossSlainTextRoutine());
        }
    }

    private void CheckHealth()
    {
        if (enemyStats.currentHealth <= 0)
        {
            bossDied = true;
        }
    }

    IEnumerator BossSlainTextRoutine()
    {
        BossSlainText.GetComponent<TextMeshProUGUI>().enabled = true;
        TextAppeared = true;
        yield return new WaitForSeconds(3f);
        BossSlainText.GetComponent<TextMeshProUGUI>().enabled = false;

    }
}
