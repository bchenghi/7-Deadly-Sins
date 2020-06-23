using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForOpening : MonoBehaviour
{
    public GameObject openingManager;
    DialogueOpening dialogue;

    private void Start()
    {
        dialogue = openingManager.GetComponent<DialogueOpening>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<CharacterController>())
        {
            dialogue.NextSentence();
        }
    }
}
