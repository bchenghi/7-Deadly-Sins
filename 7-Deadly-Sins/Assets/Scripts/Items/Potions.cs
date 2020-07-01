using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Consumable/Potion")]
public class Potions : Consumables , IUsable
{
    public int increaseStats;
    bool potUsed;
    public bool Health;
    public bool Mana;
    
    public override void Use()
    {
        if (Health)
        {
            potUsed = true;
        } else
        {
            potUsed = false;
        }
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        Animator animator = PlayerManager.instance.player.GetComponent<Animator>();
        PlayerAnimator playerAnim = PlayerManager.instance.player.GetComponent<PlayerAnimator>();
        base.Use();
       
        playerStats.SetIncreaseInStats(increaseStats, potUsed);
        Monobehaviour.instance.StartCoroutine(DisableHotkeyTiming(2.5f));
        Transform[] ts = PlayerManager.instance.player.transform.GetComponentsInChildren<Transform>(true);
        if (Health)
        {
            foreach (Transform t in ts)
            {
                Debug.Log(t.gameObject.name);
                if (t.gameObject.name == "PlayerHpBottle")
                {
                    t.gameObject.SetActive(true);
                    break;
                }
            }
        } else
        {
            foreach (Transform t in ts)
            {
                Debug.Log(t.gameObject.name);
                if (t.gameObject.name == "PlayerManaBottle")
                {
                    t.gameObject.SetActive(true);
                    break;
                }
            }
        }
        playerAnim.UsePotion();
        RemoveFromInventory();
        
    }



    public void Start()
    {
        
    }

    public Sprite Image
    {
        get 
        {
            return icon;
        }
        set
        {
            return;
        }
        

        
    }

    public override int GetPrice() {
        return (int) increaseStats;
    }

    IEnumerator DisableHotkeyTiming(float Timing)
    {
        HotKeyBar.instance.DisableAll();
        yield return new WaitForSeconds(Timing);
        HotKeyBar.instance.EnableAll();
    }

}
