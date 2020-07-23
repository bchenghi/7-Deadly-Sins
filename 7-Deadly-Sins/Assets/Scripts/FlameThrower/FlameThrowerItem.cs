using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Inventory/Others/FlameThrower")]
public class FlameThrowerItem : Others, IUsable
{
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

    public override void Use()
    {
        PlayerManager.instance.player.GetComponent<PlayerFlameThrower>().UseFlameThrower();
    }

    public virtual void Start() {

    }

    public override int GetPrice() {
        return 50;
    }
}
