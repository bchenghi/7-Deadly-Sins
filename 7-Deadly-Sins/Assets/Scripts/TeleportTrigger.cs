using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    GameObject objectToTeleport;
    [SerializeField]
    Vector3 positionToTeleport;
    // Start is called before the first frame update
    

    public void Trigger() {
        objectToTeleport.transform.position = positionToTeleport;
        objectToTeleport.SetActive(true);
    }
}
