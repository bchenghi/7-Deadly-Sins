using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

// Movement, focused gameobject, and interaction of player
public class PlayerController : MonoBehaviour
{
    // movement

    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -9.8f;
    public float jumpHeight;
    [Range(0,1)]
    public float airControlPercent;


    // animation

    bool jumping = false;
    // Vertical velocity that shld trigger falling animation.
    float vertVelocityTriggerFallAnim = -0.6f;
    // Distance above ground to trigger landing animation
    float distanceAboveGroundTriggerLandAnim = 0.5f;
    // Max distance the player is above the ground to count as grounded
    float groundedDistance = 0.05f;
    public GameObject distanceFromGroundReference;
    float groundRefOffset = 0.4f;

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
    LineRenderer lineRenderer;

    Interactable focus;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //DistanceToGround();
        // Debugging line
        /*
        Ray ray1 = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit1;
        int playerLayerMask1 = LayerMask.GetMask("Player");
        if (Physics.Raycast(ray1, out hit1, 100, ~playerLayerMask1))
        {
            lineRenderer.SetPosition(0, transform.Find("Target Look").position);
            lineRenderer.SetPosition(1, hit1.point);
        }
        */

        if (ActionsAllowed())
        {
            if (focus != null)
            {
                FaceTarget();
            }

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            bool running = Input.GetKey(KeyCode.LeftShift);

            Move(inputDir, running);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            FallAnim();
            LandAnim();

            float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
            animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

            // if left mouse or e is pressed shoot a ray. If right mouse clicked and is item or if E is pressed and is enemy, interact.
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                RaycastHit hit;
                int playerLayerMask = LayerMask.GetMask("Player");
                if (Physics.Raycast(ray, out hit, 100, ~playerLayerMask))
                {
                    Debug.Log("ray hit " + hit.transform.name);
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.GetComponent<ItemPickUp>() != null
                        || Input.GetKeyDown(KeyCode.E) && hit.transform.gameObject.GetComponent<Enemy>() != null
                        || Input.GetMouseButtonDown(0) && hit.transform.gameObject.GetComponent<Chest>() != null)
                    {
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
        velocityY += (Time.deltaTime * gravity);
        Vector3 velocity = transform.forward * currentSpeed + (Vector3.up * velocityY);
        //Debug.Log("velY " + velocityY + "vecup * vely " + (Vector3.up * velocityY));
        //Debug.Log("forward Speed: " + currentSpeed);
        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        
        if (DistanceToGround() <= groundedDistance)
        {
            velocityY = 0;
        }
        
    }

    // If grounded, jump and animate jump
    void Jump()
    {
        if (DistanceToGround() <= groundedDistance && !jumping)
        {
            RemoveFocus();
            StartCoroutine(JumpCoroutine());
        }
    }

    // if distance above ground is high enough and is falling but fall animation not running
    // , run fall animation
    void FallAnim()
    {
        if (DistanceToGround() >= 0 && (velocityY < vertVelocityTriggerFallAnim || jumping ) && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Fall")) // and maybe duration player falling
        {
            animator.SetBool("fall", true);
        }
    }

    // Animates landing animation when distance to ground is close enough
    void LandAnim()
    {
        if (DistanceToGround() <= distanceAboveGroundTriggerLandAnim)
        {
            animator.SetBool("fall", false);
            jumping = false;
        }
    }


    float GetModifiedSmoothTime(float smoothTime)
    {
        if (DistanceToGround() <= groundedDistance)
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

    // If player is not in hurt, attack, landing animation, return true.
    // In Update method, he can do actions (move, jump, set focus, face target, pick up)
    bool ActionsAllowed()
    {
        return !animator.GetCurrentAnimatorStateInfo(0).IsName("Reaction") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Punching") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Land");
    }

    // Returns distance to mesh collider below player. 
    // (Origin of boxcast is set to be 0.1 above the floor, so returned value is -0.1f.)
    float DistanceToGround()
    {
        RaycastHit hit;

        if (Physics.BoxCast(distanceFromGroundReference.transform.position + Vector3.up * 0.05f, 
            new Vector3(controller.radius * 0.8f,  0.05f, controller.radius * 0.8f),
            -transform.TransformDirection(Vector3.up), out hit, transform.rotation, 100))
        {
            Debug.DrawRay(distanceFromGroundReference.transform.position, hit.point, Color.cyan);
            Debug.Log("collided obj: " + hit.transform.name);
        }
        float distance = hit.distance;
        Debug.Log("distance: " + (distance - groundRefOffset));
        return Mathf.Clamp(distance - groundRefOffset, 0, distance - groundRefOffset);
    }

    // Runs jump animation, sets vert velocity and set jumping to true
    // Stops code for a period of time as jump anim has squatting down motion before player shld leave the ground
    IEnumerator JumpCoroutine()
    {
        animator.SetTrigger("jump");
        yield return new WaitForSeconds(0.52f);
        float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
        velocityY = jumpVelocity;
        jumping = true;
    }
}