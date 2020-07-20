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
    /*
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (KeyPressDown != null) KeyPressDown();
            string inputKey = "";
            Debug.Log("inputstring is "+ Input.inputString);
            if (Input.inputString != "")
            {
                int length = Input.inputString.Length;
                inputKey = Input.inputString[length - 1].ToString();
                Debug.Log("input key is " + inputKey);

            }
        }

        
    }
    */


    void OnGUI() {
        Event e = Event.current;
        if (e.isKey) {
            Debug.Log("input key tostring is: " + e.keyCode.ToString());
            string inputString = e.keyCode.ToString();
            string inputForHotKey = null;
            if (inputString.Length > 5 && inputString.Substring(0,5) == "Alpha") {
                inputForHotKey = inputString.Substring(5);
                Debug.Log("inputforhotkey set: " + inputForHotKey);
            } else {
                inputForHotKey = e.keyCode.ToString().ToLower();
            }
            Debug.Log("inputForHotKey " + inputForHotKey);
            int hotkeyIndex = HotKeyBar.instance.HotKeyString.IndexOf(inputForHotKey);
            Debug.Log("hotkeyindex: " + hotkeyIndex);
            HotKeyBar.instance.UseHotKey(hotkeyIndex);
        }
    }
}
