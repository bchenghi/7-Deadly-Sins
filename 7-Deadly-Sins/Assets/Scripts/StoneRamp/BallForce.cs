using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody body = other.transform.GetComponent<Rigidbody>();
            body.AddForce(new Vector3(xSpeed, 0, ySpeed) * speed);
        }
    }
}
