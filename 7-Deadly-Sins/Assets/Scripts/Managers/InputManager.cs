using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void InputEvent();
    //public static event InputEvent OnPressDown;
    //public static event InputEvent OnPressUp;
    //public static event InputEvent OnTap;
    public static event InputEvent KeyPressDown;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (KeyPressDown != null) KeyPressDown();
            print("Ïnput Key : " + Input.inputString);
            string inputKey = "";
            if (Input.inputString != "")
            {
                inputKey = Input.inputString[0].ToString();
                int hotkeyIndex = HotKeyBar.instance.HotKeyString.IndexOf(inputKey);
                HotKeyBar.instance.UseHotKey(hotkeyIndex);
            }
            
            
        }
    }
}
