using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    GameObject objectToTeleport;
    [SerializeField]
    Regions regionsToTeleport;

    [SerializeField]
    float minDistanceFromPlayer;

    int maxTriesToFindPosition = 10;
    // Start is called before the first frame update
    

    public void Trigger() {
        Vector3 positionToTeleport = RandomPosition();
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

    Vector3 RandomPosition() {
        Vector3 newPosition = regionsToTeleport.RandomPosition();
        if (regionsToTeleport.Diameter() >= minDistanceFromPlayer) {
            int numOfTries = 0;
            while(numOfTries < maxTriesToFindPosition) {
                if (Vector3.Distance(newPosition, 
                PlayerManager.instance.player.transform.position) < minDistanceFromPlayer) {
                    newPosition = regionsToTeleport.RandomPosition();
                    numOfTries++;
                    continue;
                } else {
                    break;
                }
            }

            if (numOfTries == maxTriesToFindPosition) {
                Debug.Log("Could not find position far enough after " + maxTriesToFindPosition + " tries");
            }
        } 
        else
        {
            Debug.Log("min distance to teleport from player is too low, not possible");
        }
        return newPosition;
    }
}
