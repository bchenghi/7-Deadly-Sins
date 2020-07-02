using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsAndPlayerDetector : MonoBehaviour
{
    public GameObject Spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            GameObject.Destroy(other.gameObject);
        } else if (other.GetComponent<PlayerController>())
        {
            Spawner.SetActive(true);
        }
    }
}
