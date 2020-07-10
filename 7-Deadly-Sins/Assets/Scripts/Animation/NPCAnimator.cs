using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : CharacterAnimator
{

    protected override void Update(){

    }

    public void Celebrate() {
        animator.SetTrigger("celebration");
    }

    public void Disappointment() {
        animator.SetTrigger("disappointment");
    }

    public void Converse() {
        animator.SetBool("conversation", true);
    }

    public void StopConversation() {
        animator.SetBool("conversation", false);
    }
}
