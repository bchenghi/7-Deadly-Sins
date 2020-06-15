using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUIManager : MonoBehaviour
{
    [SerializeField]
    StatUI[] statUis;

    public void UpdateStatUIs() {
        foreach(StatUI ui in statUis) {
            ui.UpdateStatUI();
        }
    }
}
