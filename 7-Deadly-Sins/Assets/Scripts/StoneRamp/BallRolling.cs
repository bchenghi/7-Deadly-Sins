using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRolling : MonoBehaviour
{
    public float ballSpeed;
    public float xSpeed;
    public float ySpeed;
    Rigidbody body;


    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        body.AddTorque(new Vector3(xSpeed, 0, ySpeed) * ballSpeed * Time.deltaTime);
        
    }
}
