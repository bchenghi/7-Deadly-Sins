using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0,1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Camera cam;
    Animator animator;
    Transform cameraT;
    CharacterController controller;

    Interactable focus;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (focus != null)
        {
            FaceTarget();
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        Move(inputDir, running);

        if (Input.GetKeyDown (KeyCode.Space))
        {
            Jump();
        }


        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            int playerLayerMask = LayerMask.GetMask("Player");
            if (Physics.Raycast(ray, out hit,  100, ~playerLayerMask))
            {
                Debug.Log("ray hit " + hit.transform.name);
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    //interact if within interaction radius and face the interactable
                    float distance = Vector3.Distance(transform.position, interactable.transform.position);
                    if (interactable.radius >= distance)
                    {
                        SetFocus(interactable);
                    }
                    
                }
            }
        }
    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero)
        {
            RemoveFocus();
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }


        float targetSpeed = (running) ? runSpeed : walkSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));
        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;


        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
        } 
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            RemoveFocus();
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }
        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

    void SetFocus(Interactable interactable)
    {
        if (focus != interactable)
        {
            if (focus != null)
                focus.UnFocus();
            
            focus = interactable;
        }
        interactable.OnFocus(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.UnFocus();

        focus = null;
        
    }

    void FaceTarget()
    {
        Vector3 direction = (focus.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}