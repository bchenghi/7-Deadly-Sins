using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public int SwitchNumber;
    public bool engaged;
    Animator animator;
    public PuzzleToSwitchBridge bridge;

    private void Start()
    {
        animator = GetComponent<Animator>();
        engaged = false;
    }

    private void Update()
    {
        if (bridge.AllPiecesPieced)
        {
            if (engaged)
            {
                animator.SetBool("SwitchOn", true);
            }
            else
            {
                animator.SetBool("SwitchOn", false);
            }
        }
    }

    public void SetToEngage()
    {
        engaged = true;
    }

}
