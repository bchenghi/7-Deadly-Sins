using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class HotKeyBar : MonoBehaviour
{

   
    public static HotKeyBar instance;
    public GameObject hotKeysParent;
    public HotKey[] Hotkeys;
    public string HotKeyString = "123456ty";

    // memorises the IUsables in the hotkeys for scene transitions
    IUsable[] hotKeyMemory;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Hotkeys = hotKeysParent.GetComponentsInChildren<HotKey>();
        hotKeyMemory = HotKeyBarManager.instance.hotKeyMemory;
        Inventory.instance.onItemChangedCallback += RefreshHotkeys;
        UpdateHotKeysFromMemory();
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



    public void ClearIUsableInMemory(HotKey inputHotKey) {
        for (int i = 0; i < Hotkeys.Length ; i++)
        {
            if (Hotkeys[i].Equals(inputHotKey)) {
                hotKeyMemory[i] = null;
                break;
            }
        }
    }

    public void AddIUsableInMemory(HotKey inputHotKey) {
        for (int i = 0; i < Hotkeys.Length ; i++)
        {
            if (Hotkeys[i].Equals(inputHotKey)) {
                Debug.Log(hotKeyMemory.Length);
                if (hotKeyMemory[i] != null) {
                    hotKeyMemory[i] = null;
                }
                hotKeyMemory[i] = inputHotKey._usable;
                break;
            }
        }
    }


    public void UpdateHotKeysFromMemory() {
        Hotkeys = hotKeysParent.GetComponentsInChildren<HotKey>();
        for (int i = 0; i < hotKeyMemory.Length; i++) {
            //Debug.Log(i);
            if (hotKeyMemory[i] != null) {
                //Debug.Log((hotKeyMemory[i] is Skill) + " memory is skill");
                if (hotKeyMemory[i] is Skill) {
                    foreach (Skill skill in HotKeyBarManager.instance.GetIUsableSkills()) {
                        if (skill.Description == ((Skill) hotKeyMemory[i]).Description) {
                            hotKeyMemory[i] = ((IUsable) skill);
                            break;
                        }
                    }
                }
                
                SetUpIUsable(hotKeyMemory[i]);
                Hotkeys[i].SetUsable(hotKeyMemory[i]);
            }

                
        }
    }

    public void SetUpIUsable(IUsable usable) {
        usable.Start();
    } 


}
