using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    NPCController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<NPCController>();
    }

    public override void Interact() {
        base.Interact();
        controller.InteractWithNpc();
    }
}
