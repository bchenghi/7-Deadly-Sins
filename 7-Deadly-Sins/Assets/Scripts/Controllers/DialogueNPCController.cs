using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPCController : NPCController
{
    [SerializeField]
    NPCMissionDialogue missionDialogue;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //sets dialogue to be shown based on state of npc
    public override void InteractWithNpc(){
        missionDialogue.StartDialogue();
    }
}

