using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPerson : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            transform.GetComponent<DroppingPlatform>().PlayerDetected = true;
        }
    }
}
