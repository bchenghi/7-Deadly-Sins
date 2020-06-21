using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SaveLoad.instance.Save();
        ClearDelegates();
        SceneManager.LoadScene(sceneName);
    }

    // Before loading new scene delegates need to be cleared as new scene will set up delegates again
    void ClearDelegates() {
        foreach(Delegate d in Inventory.instance.onItemChangedCallback.GetInvocationList()) {
            Inventory.instance.onItemChangedCallback -= (Inventory.OnItemChanged) d;
        }
        foreach(Delegate d in EquipmentManager.instance.onEquipmentChanged.GetInvocationList()) {
            EquipmentManager.instance.onEquipmentChanged -= (EquipmentManager.OnEquipmentChanged) d;
        }
        foreach(Delegate d in GoldCounter.instance.onGoldChange.GetInvocationList()) {
            GoldCounter.instance.onGoldChange -= (GoldCounter.OnGoldChange) d;
        }
    }
}
