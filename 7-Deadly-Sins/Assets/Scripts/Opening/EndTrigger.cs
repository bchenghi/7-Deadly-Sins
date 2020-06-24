using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour, ITrigger
{
    public GameObject MainMenu;

    public void Trigger()
    {
        MainMenu.SetActive(true);
        PlayerManager.instance.player.GetComponent<OpeningPlayerController>().walkSpeed = 0;
        PlayerManager.instance.player.GetComponent<PlayerAnimator>().KneelDown();


    }

    
}
