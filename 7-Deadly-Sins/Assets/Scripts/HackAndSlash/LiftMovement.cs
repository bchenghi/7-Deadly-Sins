using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftMovement : MonoBehaviour
{
    public bool level1Done;
    public bool level2Done;
    public Transform lift;
    public Vector3 level2Position;
    public Vector3 level3Position;
    private bool PlayerEntered;
    public Animator animator;
    private bool doorClosed;
    public bool reachedLevel2;
    public bool reachedLevel3;



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PlayerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            GetComponent<BoxCollider>().enabled = false;
            PlayerEntered = false;
        }
    }

    private void Update()
    {
        if (level1Done && PlayerEntered && !reachedLevel2)
        {
           
            animator.SetBool("DoorOpen", false);
            
            StartCoroutine(WaitForDoorClose());
            if (doorClosed)
            {
                lift.position = Vector3.MoveTowards(lift.position, level2Position, 3f * Time.deltaTime);
                float distance = Vector3.Distance(lift.position, level2Position);
                if (distance < 0.2f)
                {
                    reachedLevel2 = true;
                    doorClosed = false;
                }
               
            }
            
        }
        else if (level2Done && PlayerEntered && !reachedLevel3)
        {
            
            animator.SetBool("DoorOpen", false);
            
            StartCoroutine(WaitForDoorClose());
            if (doorClosed)
            {
                lift.position = Vector3.MoveTowards(lift.position, level3Position, 3f * Time.deltaTime);
                float distance = Vector3.Distance(lift.position, level3Position);
                if (distance < 0.2f)
                {
                    reachedLevel3 = true;
                    doorClosed = false;
                }
            }
        }
    }

    IEnumerator WaitForDoorClose()
    {
        yield return new WaitForSeconds(3f);
        doorClosed = true;

    }
}
