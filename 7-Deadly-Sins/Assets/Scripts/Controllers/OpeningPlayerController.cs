using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningPlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    float speedSmoothTime = 0.1f;

    [SerializeField]
    float walkSpeed = 2f;
    float speedSmoothVelocity;
    float velocityY;
    
    float gravity = -9.8f;

    bool isGrounded = false;

    float distanceToGround;

    [SerializeField]
    GameObject distanceFromGroundReference;

    float groundRefOffset = 0.63f;

    float groundedDistance = 0.1f;



    float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("grounded: " + isGrounded);
        float animationSpeedPercent = currentSpeed / walkSpeed * 0.5f;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        Move();
        SetDistanceToGround();
        SetIsGrounded();
        
    }

    void Move()
    {
        float targetSpeed = walkSpeed;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        velocityY += (Time.deltaTime * gravity);
        
        Vector3 velocity = transform.forward * currentSpeed + (Vector3.up * velocityY);
        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (isGrounded) {
            velocityY = 0;
        }
    }

     void SetDistanceToGround()
    {
        int ignoreRaycastLayerMask = LayerMask.GetMask("Ignore Raycast");
        RaycastHit hit;
        float distance;
        if (Physics.BoxCast(distanceFromGroundReference.transform.position, 
            new Vector3(controller.radius * 0.5f,  0.01f, controller.radius * 0.5f),
            -transform.TransformDirection(Vector3.up), out hit, transform.rotation, 100, ~ignoreRaycastLayerMask))
        {
            Debug.Log("collided obj: " + hit.transform.name);
            distance = hit.distance;
        }
        else {
            distance = Mathf.Infinity;
        }
        Debug.Log("distance: " + (distance - groundRefOffset));
        distanceToGround =  Mathf.Clamp(distance - groundRefOffset, 0, distance - groundRefOffset);
    }
    void SetIsGrounded() {
        if ( distanceToGround <= groundedDistance)
        {
            isGrounded = true;
        }
        else
        {
           isGrounded =  false;
        }
    }
}
