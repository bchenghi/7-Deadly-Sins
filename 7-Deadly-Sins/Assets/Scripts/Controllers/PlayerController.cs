using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.Video;

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

    //For Dodging 
    public float DelayBeforeInvisible = 0.2f;
    public float InvisibleDuration = 0.5f;
    public float DodgeCoolDown = 1f;
    private float ActCoolDown;
    private bool isRolling;


    // For climbing
    [HideInInspector]
    public bool Climbing;
    [HideInInspector]
    public bool ClimbingButStop;
    private float prevSpeed;
    

    [HideInInspector]
    public bool running = false;
    [HideInInspector]
    public bool isSlowed = false;
    // animation
    bool falling = false;
    // True when squatting part in jump anim is over and is in the air. Set to false when falling or landing.
    // Needed as !jumpAnimStart as condition for land animation doesnt work
    bool jumping = false;
    // Used for preventing jump from being called again once jump anim has started. 
    // Set to false when falling or landing
    bool jumpAnimStart = false;
    // min distance above ground that will trigger falling animation (needs specific adjustment based on anim)
    float distanceAboveGroundTriggerFallAnim = 1f;
    // Distance above ground to trigger landing animation (needs specific adjustment based on anim)
    float distanceAboveGroundTriggerLandAnim = 0.7f;
    // Max distance the player is above the ground to count as grounded
    [SerializeField]
    float groundedDistance = 0.1f;

    // A gameobject that contains gameobjects as children that are used measure dist from ground
    public GameObject distFromGroundComponents;
    //Reference object distance to ground is measured from
    Transform distanceFromGroundReference;
    // Reference for feet/bottom of player
    Transform feetReference;
    // Distance to subtract from height from reference to ground, as the reference is above ground level 
    // (value needs specific adjustment)
    float groundRefOffset;
    bool isGrounded;

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
    PlayerStats playerStats;
    LineRenderer lineRenderer;
    SoundHandler soundHandler;

    Interactable focus;

    float distanceToGround;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main;
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();
        //previousPosition = transform.position;
        playerStats = GetComponent<PlayerStats>();
        soundHandler = GetComponent<SoundHandler>();
        feetReference = distFromGroundComponents.transform.GetChild(1);
        distanceFromGroundReference = distFromGroundComponents.transform.GetChild(0);
        groundRefOffset = Vector3.Distance(feetReference.position, distanceFromGroundReference.position);
        prevSpeed = animator.speed;
    }

    // Update is called once per frame
    void Update()
    {
        SetIsGrounded();
        SetDistanceToGround();
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

        //Stops the player from moving, add namespace for any animation that requires this
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Drinking"))
        {
            return;
        }

        if (Climbing)
        {
            if (ClimbingButStop == false)
            {
                animator.speed = prevSpeed;
                animator.SetBool("Climbing", true);
            } else
            {
                animator.speed = 0;
            }
            
        } else
        {
            animator.SetBool("Climbing", false);
        }
        
        

        if (ActionsAllowed() && !Climbing)
        {
            if (focus != null)
            {
                FaceTarget();
            }

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            if (isSlowed == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    running = true;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    running = false;
                }
            } else
            {
                running = false;
            }

            Move(inputDir, running);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                soundHandler.PlaySoundByName("Jump");
            }

            FallAnim();
            LandAnim();
            Rolling();

            if (isRolling)
            {
                DodgeForwward();
            }

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
                //int playerLayerMask = LayerMask.GetMask("Player");
                int interactableMask = LayerMask.GetMask("Interactable");
                if (Physics.Raycast(ray, out hit, 100, interactableMask))
                {
                    Debug.Log("ray hit " + hit.transform.name);
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.GetComponent<ItemPickUp>() != null
                        || Input.GetKeyDown(KeyCode.E) && hit.transform.gameObject.GetComponent<Enemy>() != null
                        || Input.GetMouseButtonDown(0) && hit.transform.gameObject.GetComponent<Chest>() != null
                        || Input.GetMouseButtonDown(0) && hit.transform.gameObject.GetComponent<NPC>() != null)
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
        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;


        if (isGrounded && !jumping)
        {
            velocityY = 0;
        }
        // Debug.Log("velocity: " + velocity * Time.deltaTime + " controller y velocity: " + controller.velocity.y);
        // Debug.Log("velocityY: " + velocityY+ ", distance to ground: " + distanceToGround);
    }

    // If grounded, not jumping, falling or landing then animate jump
    void Jump()
    {
        if (isGrounded && !falling
         && !animator.GetCurrentAnimatorStateInfo(0).IsName("Land") 
         && !jumpAnimStart)
        {
            RemoveFocus();
            StartCoroutine(JumpCoroutine());
        }
    }

    // Runs jump animation, sets vert velocity and set jumping to true
    // Stops code inside method for a period of time as jump anim has 
    // squatting down motion before player shld leave the ground
    IEnumerator JumpCoroutine()
    {
        jumpAnimStart = true;
        animator.SetTrigger("jump");
        yield return new WaitForSeconds(0.52f);
        jumping = true;
        if (isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        } 
    }

    // if distance above ground is high enough and is falling but fall animation not running
    // , run fall animation
    void FallAnim()
    {
        if (distanceToGround >= distanceAboveGroundTriggerFallAnim && (velocityY < 0) && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
        {
            jumpAnimStart = false;
            falling = true;
            jumping = false;
            animator.SetBool("fall", true);
        }
    }

    // Animates landing animation when distance to ground is close enough,
    // is in falling anim and falling, but not in jumping anim
    void LandAnim()
    {
        if (distanceToGround <= distanceAboveGroundTriggerLandAnim && 
            (jumping || falling) && velocityY <= 0 )
        {
            animator.SetBool("fall", false);
            animator.SetTrigger("land");
            falling = false;
            jumping = false;
            jumpAnimStart = false;
        }
    }


    float GetModifiedSmoothTime(float smoothTime)
    {
        if (isGrounded)
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
    // In Update method, he can do actions (move, jump, set focus, face target, pick up) if returns true, 
    // else no actions can be done by player
    bool ActionsAllowed()
    {
        return !animator.GetCurrentAnimatorStateInfo(0).IsName("Reaction") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Punching") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Land");
    }

    // Sets distance to mesh collider below player in variable distanceToGround
    void SetDistanceToGround()
    {
        int ignoreRaycastLayerMask = LayerMask.GetMask("Ignore Raycast");
        RaycastHit hit;
        float distance;
        if (Physics.BoxCast(distanceFromGroundReference.transform.position, 
            new Vector3(controller.radius * 0.9f,  0.01f, controller.radius * 0.9f),
            -transform.TransformDirection(Vector3.up), out hit, transform.rotation, 100, ~ignoreRaycastLayerMask))
        {


            // Debug.Log("collided obj: " + hit.transform.name);
            distance = hit.distance;

        } else {
            distance = int.MaxValue;
        }
        //Debug.Log("distance: " + (distance - groundRefOffset));
        distanceToGround =  Mathf.Clamp(distance - groundRefOffset, 0, distance - groundRefOffset);
    }

    // If vertical velocity of player is approximately 0 and not in the air (not falling or jumping),
    // or distance from ground is less than a certain threshold,
    // player is grounded and result is stored in isGrounded variable
    void SetIsGrounded()
    {
        //Debug.Log("vertical velocity of controller " + Mathf.Abs(controller.velocity.y) + " < " + Mathf.Pow(1,-10));
        if ((Mathf.Abs(controller.velocity.y) < Mathf.Pow(10, -5) && !jumping && !falling) || distanceToGround <= groundedDistance)
        {
            //Debug.Log("is grounded set to true");
            isGrounded = true;
        }
        else
        {
           // Debug.Log("isgrounded set to false");
           isGrounded =  false;
        }
    }


    //Rolling and Dodging
    public void Rolling()
    {
        bool Roll = Input.GetButtonDown("Roll");
        if (ActCoolDown <= 0)
        {
            animator.ResetTrigger("Roll");
            if (Roll)
            {
                StartCoroutine(RollDuration());
                Dodge();
            }
        } else
        {
            ActCoolDown -= Time.deltaTime;
        }
    }

    public void Dodge()
    {
        ActCoolDown = DodgeCoolDown;
        playerStats.Invisible(DelayBeforeInvisible, InvisibleDuration);
        animator.SetTrigger("Roll");
       


    }

    public void DodgeForwward()
    {
         //transform.position += transform.forward * Time.deltaTime * 3f;
        //transform.Translate(Vector3.forward * Time.deltaTime * 2f);
        transform.Translate(transform.forward * Time.deltaTime * 2f, Space.World); // transform.forward is vector3.forward(0,0,1) * transform.rotation in world Space. So use Space.World. If using Space.Self, use Vector3.forward as the Z axis of person will be facing in the direction it is facing so dont need to take into account the rotation anymore.
        
        
    }

    IEnumerator RollDuration()
    {
        isRolling = true;
        yield return new WaitForSeconds(0.8f);
        isRolling = false;
    }

    
}