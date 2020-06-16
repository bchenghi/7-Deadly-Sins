using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor: MonoBehaviour
{
    // enemies to be killed before opening door
    [SerializeField]
    GameObject[] enemiesToKill;
    bool opened = false;

    void Start()
    {

    }

    void Update()
    {
        if (CheckOpenCondition()) {
            DoorOpen();
            opened = true;
        } 
    }

    // Opens door
    void DoorOpen()
    {
        GetComponent<Animator>().SetTrigger("DoorOpenTrigger");
    }

    // returns true if condition to open is met
    bool CheckOpenCondition() {
        bool open = true;
        foreach (GameObject enemy in enemiesToKill) {
            if (enemy != null) {
                open = false;
                break;
            }
        }
        return open;
    }



    void DoorClose() {
        GetComponent<Animator>().SetTrigger("DoorCloseTrigger");
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && opened) {
            DoorClose();
        }
    }
}
