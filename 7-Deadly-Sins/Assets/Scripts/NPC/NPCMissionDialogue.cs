using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Checks with NPCMission for status of mission, adjusts dialogue accordingly and animates npc
public class NPCMissionDialogue : NPCDialogue
{
    [SerializeField]
    NPCMission mission;

    // Dialogue triggers with tags to identify which state to use which dialogue
    [SerializeField]
    public DialogueWithTag[] dialogueTriggersWithTag;

    DialogueTrigger dialogueTriggerToUse;

    bool interactCalled = false;

    NPCAnimator animator;


    bool conversingWithThisNPC = false;
    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<NPCAnimator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (interactCalled) {
            dialogueTriggerToUse.Trigger();
            interactCalled = false;
        }

        if (!ConversingWithThisNPC() && conversingWithThisNPC) {
            conversingWithThisNPC = false;
            animator.StopConversation();
        }
    }

    public override void StartDialogue() {
        SetMissionState();
        conversingWithThisNPC = true;
        if (mission.celebration) {
            dialogueTriggerToUse = GetDialogueTriggerByTag("celebration");
            if (animator != null) {
                animator.Celebrate();
            }
            // animation
        }
        else if (mission.disappointment) {
            dialogueTriggerToUse = GetDialogueTriggerByTag("disappointment");
            if (animator != null) {
                animator.Disappointment();
            }
            // animation
        } else if (mission.done) {
            dialogueTriggerToUse = GetDialogueTriggerByTag("done");
            if (animator != null) {
                animator.Converse();
            }
            //animation
        } else if (mission.opening){
            dialogueTriggerToUse = GetDialogueTriggerByTag("opening");
            if (animator != null) {
                animator.Converse();
            }
            //animation
        }
        interactCalled = true;
        ResetMissionState();
    }

    DialogueTrigger GetDialogueTriggerByTag(string tag) {
        DialogueTrigger result = null;
        foreach (DialogueWithTag dialogueTriggerWithTag in dialogueTriggersWithTag) {
            if (dialogueTriggerWithTag.tag == tag) {
                result = dialogueTriggerWithTag.dialogueTrigger;
                break;
            }
        }
        return result;
    }

    bool ConversingWithThisNPC() {
        return conversingWithThisNPC && DialogueManager.instance.currentlyInDialogue;
    }

    void SetMissionState() {
        mission.SetState();
    }

    void ResetMissionState() {
        mission.ResetState();
    }



    
}
