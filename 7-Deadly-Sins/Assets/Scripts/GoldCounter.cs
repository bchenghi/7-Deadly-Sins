using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
    #region Singleton
    public static GoldCounter instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public int gold = 0;
    public delegate void OnGoldChange(int previousGold, int currentGold);
    public OnGoldChange onGoldChange; 

    public void Earn(int gold)
    {
        if (gold >= 0) {
            this.gold += gold;
            onGoldChange(this.gold - gold, this.gold);
        }
        
    }

    public bool Spend(int gold)
    {
        if (this.gold >= gold)
        {
            this.gold -= gold;
            onGoldChange(this.gold + gold, this.gold);
            return true;
        }
        else
        {
            DisplayTextManager.instance.Display("Not enough gold", 2f);
            Debug.Log("Not enough gold");
            return false;
        }
    }
}
