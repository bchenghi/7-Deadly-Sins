using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeyBarManager : MonoBehaviour
{
    #region Singleton
    public static HotKeyBarManager instance;
    void Awake() {
        if (HotKeyBarManager.instance == null) {
            HotKeyBarManager.instance = this;
        } else {
            Destroy(this);
        }
        
        hotKeyMemory = new IUsable[8];
    }

    #endregion

    // memorises the IUsables in the hotkeys for scene transitions
    [HideInInspector]
    public IUsable[] hotKeyMemory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
