using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GrapplingHooks : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public bool fired;
    public bool hooked;
    public GameObject hookedObj;

    public float maxDistance;
    private float currentDistance;
    public LayerMask targetMask;
    Vector3 directionOfTravel;

    private bool grounded;
    private LineRenderer rope;
    private PlayerAnimator animator;
    public Item hooks;
    private SphereCollider hookCollider;

    private void Start()
    {
        rope = hook.GetComponent<LineRenderer>();
        animator = GetComponent<PlayerAnimator>();
        hookCollider = hook.GetComponent<SphereCollider>();
    }


    private void Update()
    {
        

        if (Inventory.instance.getValue(hooks) != -1)
        {
            rope.enabled = true;
            //Firing the hook
            if (Input.GetMouseButtonDown(1) && !fired)
            {
                Inventory.instance.Remove(hooks, 1);
                hookCollider.enabled = true;
                fired = true;
                animator.Hook();
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, targetMask))
                {
                    directionOfTravel = hit.point - hook.transform.position;
                    directionOfTravel.Normalize();


                }





            }

            if (fired)
            {
                rope.positionCount = 2;
                rope.SetPosition(0, hookHolder.transform.position);
                rope.SetPosition(1, hook.transform.position);
            }

            if (fired && !hooked)
            {

                //hook.transform.Translate(Vector3.forward * hookTravelSpeed * Time.deltaTime);

                hook.transform.Translate((directionOfTravel.x * hookTravelSpeed * Time.deltaTime),
             (directionOfTravel.y * hookTravelSpeed * Time.deltaTime),
             (directionOfTravel.z * hookTravelSpeed * Time.deltaTime),
             Space.World);

                //Debug.Log(hook.transform.position);

                currentDistance = Vector3.Distance(transform.position, hook.transform.position);

                if (currentDistance >= maxDistance)
                {
                    ReturnHook();
                }


            }
            if (hooked && fired)
            {
                hook.transform.parent = hookedObj.transform;
                GetComponent<CharacterController>().enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.deltaTime);
                float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);
                //Debug.Log(distanceToHook);
                if (distanceToHook <= 1)
                {
                    CheckIfGrounded();
                    if (grounded == false)
                    {
                        this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                        this.transform.Translate(Vector3.up * Time.deltaTime * 18f);


                    }
                    StartCoroutine(Climb());


                }
            }
            else
            {
                hook.transform.parent = hookHolder.transform;
                GetComponent<CharacterController>().enabled = true;
                animator.Hook();

            }
        } else
        {
            rope.enabled = false;
        }
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    public void ReturnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
        hookCollider.enabled = false;
        rope.positionCount = 0;
        
    }


    public void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1f;

        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, 100))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }




}
