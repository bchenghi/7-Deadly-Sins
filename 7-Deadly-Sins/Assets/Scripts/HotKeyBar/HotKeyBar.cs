using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class HotKeyBar : MonoBehaviour
{

   
    public static HotKeyBar instance;
    public HotKey[] Hotkeys;
    public string HotKeyString = "123456ty";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Hotkeys = GetComponentsInChildren<HotKey>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseHotKey(int index)
    {
        if (index >= 0 && index < HotKeyString.Length)
        {
            Hotkeys[index].UseUsable();
        }
    }

    public void RefreshHotkeys()
    {
        foreach(HotKey hotkey in Hotkeys)
        {
            hotkey.Refresh();
        }
    }

    public void EnableAll()
    {
        foreach(HotKey hotkey in Hotkeys)
        {
            hotkey.EnableHotKey();
        }
    }

    public void DisableAll()
    {
        foreach (HotKey hotkey in Hotkeys)
        {
            hotkey.DisableHotKey();
        }
    }


}
