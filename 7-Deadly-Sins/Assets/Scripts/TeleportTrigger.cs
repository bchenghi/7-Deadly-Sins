using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    GameObject objectToTeleport;
    [SerializeField]
    Vector3 positionToTeleport;
    // Start is called before the first frame update
    

    public void Trigger() {
        if (objectToTeleport.GetComponent<NavMeshAgent>()) {
            objectToTeleport.GetComponent<NavMeshAgent>().Warp(positionToTeleport);
        } else if (objectToTeleport.GetComponent<CharacterController>()){
            CharacterController controller = objectToTeleport.GetComponent<CharacterController>();
            controller.enabled = false;
            objectToTeleport.transform.position = positionToTeleport;
            controller.enabled = true;
        } else {
            objectToTeleport.transform.position = positionToTeleport;
        }
        objectToTeleport.SetActive(true);
    }
}
