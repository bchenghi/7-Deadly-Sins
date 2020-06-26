using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A ITrigger that sets the active state of specified gameObject to the specified boolean
public class ChangeActiveTrigger :  MonoBehaviour, ITrigger
{
    [SerializeField]
    bool setActiveTo;

    [SerializeField]
    GameObject objectToChangeActiveState;
    // Start is called before the first frame update

    public void Trigger() {
        objectToChangeActiveState.SetActive(setActiveTo);
    }
}
