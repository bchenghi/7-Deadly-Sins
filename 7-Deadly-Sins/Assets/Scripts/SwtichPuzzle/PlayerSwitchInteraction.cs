using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchInteraction : MonoBehaviour
{
    public Switch switches;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                switches.SetToEngage();
                
            }
        }
    }
}
