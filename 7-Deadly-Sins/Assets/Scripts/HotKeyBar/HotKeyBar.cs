using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HotKeyBar : MonoBehaviour
{

   
    public static HotKeyBar instance;
    public GameObject hotKeysParent;
    public HotKey[] Hotkeys;
    public string HotKeyString = "123456ty";

    // memorises the IUsables in the hotkeys for scene transitions
    IUsable[] hotKeyMemory;
    bool doNotChangeEnableOrDisable = false;

    // Delegate used by 
    public delegate void OnHotKeyChange();

    public OnHotKeyChange onHotKeyChange;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Hotkeys = hotKeysParent.GetComponentsInChildren<HotKey>();
        hotKeyMemory = HotKeyBarManager.instance.hotKeyMemory;
        Inventory.instance.onItemChangedCallback += RefreshHotkeys;
        UpdateHotKeysFromMemory();

        EnableAll();

        if (SceneManager.GetActiveScene().name == "Shop-CH") {
            DisableAllSkills();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Shop-CH") {
            DisableAllSkills();
        }
    }

    public void UseHotKey(int index)
    {
        if (index >= 0 && index < HotKeyString.Length)
        {
            Debug.Log("index in usehotkey: " + index);
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
        if (!doNotChangeEnableOrDisable) {
            foreach(HotKey hotkey in Hotkeys)
            {
                hotkey.EnableHotKey();
            }
        }
        else 
        {
            Debug.Log("Enable all locked, Use enableAllMaster");
        }
    }

    public void DisableAll()
    {
        if (!doNotChangeEnableOrDisable) {
            foreach (HotKey hotkey in Hotkeys)
            {
                hotkey.DisableHotKey();
            }
        }
        else 
        {
            Debug.Log("Disable all locked, Use enableAllMaster to unlock first");
        }
    }

    public void DisableSpecificHotKeyWithItemCheck(IUsable itemInSlot)
    {
        for (int i = 0; i < Hotkeys.Length; i++)
        {
            if (Hotkeys[i]._usable.Equals(itemInSlot))
            {
                Hotkeys[i].DisableHotKey();
                break;
            }
        }
    }

    public void EnableSpecificHotKeyWithItemCheck(IUsable itemInSlot)
    {
        for (int i = 0; i < Hotkeys.Length; i++)
        {
            if (Hotkeys[i]._usable.Equals(itemInSlot))
            {
                Hotkeys[i].EnableHotKey();
                break;
            }
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
//                        Debug.Log("skill description" + ((Skill)hotKeyMemory[i]).Description);
//                        Debug.Log("Skill : " + skill.Description);
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

    public void DisableAllSkills() {
        if (!doNotChangeEnableOrDisable) {
            foreach (HotKey hotkey in Hotkeys)
            {
                if (hotkey._usable is Skill && hotkey.enabled)
                {
                    hotkey.DisableHotKey();
                }
                else 
                {
                    hotkey.EnableHotKey();
                }
            }
        } else {
            Debug.Log("Disable all skills locked, use enable all master to unlock first");
        }
    }

    public void DisableAllMaster() {
        doNotChangeEnableOrDisable = true;
        foreach (HotKey hotkey in Hotkeys)
        {
            hotkey.DisableHotKey();
        }
    }

    public void EnableAllMaster() {
        foreach (HotKey hotkey in Hotkeys)
        {
            hotkey.EnableHotKey();
        }
        doNotChangeEnableOrDisable = false;
    }


    // Used only for shopscene, if hotkey is rearranged, will need to check hotkeys with skills to diable
    public void HotKeyBarRearranged() {
        if (SceneManager.GetActiveScene().name == "Shop-CH") {
            DisableAllSkills();
        }
    }


}
