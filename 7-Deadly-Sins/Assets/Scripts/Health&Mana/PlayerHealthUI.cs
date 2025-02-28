﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    CharacterStats stats;

    Transform ui;
    Image healthSlider;
    Text display;

    // Set the text for health bar, initialize bar slider and health bar transform
    void Start()
    {
        stats = GetComponent<CharacterStats>();
        ui = uiPrefab.transform;
        healthSlider = ui.GetChild(0).GetComponent<Image>();
        display = ui.GetChild(1).GetComponent<Text>();
        display.text = stats.currentHealth + "/" + stats.maxHealth;
        stats.OnHealthChanged += OnHealthChanged;
        NewSceneSetUp();
    }


    // Updates slider and text on health bar
    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth >= 0)
            {
                display.text = currentHealth + "/" + maxHealth;
            }
            else
            {
                display.text = "0/" + maxHealth;
            }
        }
    }

    // Sets up health ui text and health bar in new scene
    void NewSceneSetUp() {
        OnHealthChanged(stats.maxHealth, stats.currentHealth);
    }
}
