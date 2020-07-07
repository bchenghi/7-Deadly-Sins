using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField]
    float visibleTime = 5f;

    float lastMadeVisibleTime;
    public GameObject bossHealthPrefab;
    CharacterStats stats;

    Transform ui;
    Image healthSlider;
    Text display;

    TextMeshProUGUI bossName;

    // Set the text for health bar, initialize bar slider and health bar transform
    void Start()
    {
        stats = GetComponent<CharacterStats>();
        ui = bossHealthPrefab.transform.GetChild(0).transform;
        healthSlider = ui.GetChild(0).GetComponent<Image>();
        display = ui.GetChild(1).GetComponent<Text>();
        bossName = bossHealthPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>();


        display.text = stats.currentHealth + "/" + stats.maxHealth;
        bossName.text = GetComponent<Enemy>().name;
        stats.OnHealthChanged += OnHealthChanged;

        bossHealthPrefab.SetActive(false);
    }

    private void Update()
    {
        if(stats.currentHealth <= 0)
        {
            bossHealthPrefab.SetActive(false);
        }
    }

    // Updates slider and text on health bar
    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            bossHealthPrefab.SetActive(true);
            lastMadeVisibleTime = Time.time;
            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth >= 0)
            {
                display.text = currentHealth + "/" + maxHealth;
            }
            else
            {
                bossHealthPrefab.SetActive(false);
            }
        }
    }

    void LateUpdate() {
        if (Time.time - lastMadeVisibleTime > visibleTime)
        {
            bossHealthPrefab.SetActive(false);
        }
    }

   
}
