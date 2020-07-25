using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Detects if player is in position and pressed E
public class EditedTurretPlayerDetection : MonoBehaviour
{
    private bool Shooting;
    bool playerShooting = false;


    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !playerShooting)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                playerShooting = true;
            }

        } else if (Input.GetKeyUp(KeyCode.E) && playerShooting) 
        {
            playerShooting = false;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerController>()) {
            playerShooting = false;
        }
    }

    public bool PlayerRequestedEntry() {
        return playerShooting;
    }

    public void SetPlayerShooting(bool boolean) {
        playerShooting = boolean;
    }

}
