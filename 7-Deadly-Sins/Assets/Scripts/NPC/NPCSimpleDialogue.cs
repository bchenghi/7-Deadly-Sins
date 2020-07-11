using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSimpleDialogue : NPCDialogue
{
    [SerializeField]
    DialogueTrigger dialogueTrigger;
    NPCAnimator animator;
    bool conversingWithThisNPC = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<NPCAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ConversingWithThisNPC() && conversingWithThisNPC) {
            conversingWithThisNPC = false;
            animator.StopConversation();
        }
    }

    public override void StartDialogue() {
        conversingWithThisNPC = true;
        animator.Converse();
        dialogueTrigger.Trigger();
    }

    bool ConversingWithThisNPC() {
        return conversingWithThisNPC && DialogueManager.instance.currentlyInDialogue;
    }
}
