using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAOpen : MonoBehaviour
{
    // enemies to be killed before opening door
    [SerializeField]
    GameObject[] enemiesToKill;


    void Start()
    {

    }

    void Update()
    {
        if (CheckOpenCondition()) {
            DoorOpen();
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
}