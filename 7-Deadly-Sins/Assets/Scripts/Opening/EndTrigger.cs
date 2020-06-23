using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour, ITrigger
{
    public GameObject MainMenu;

    public void Trigger()
    {
        MainMenu.SetActive(true);
    }

    
}
