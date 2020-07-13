using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionDeath : MonoBehaviour
{
    CompanionStats companionStats;
    CompanionsManager manager;
    private bool removed;
    private void Start()
    {
        companionStats = GetComponent<CompanionStats>();
        manager = CompanionsManager.instance;
        removed = false;
    }


    private void Update()
    {
        if (companionStats.currentHealth <= 0 && !removed)
        {
            removed = true;
            Debug.Log("TEST");
            manager.DecreaseCompanionInList(gameObject);
        }
    }
}
