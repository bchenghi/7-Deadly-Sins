using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

// Tooltip windows for shop tab
public class ToolTipWindowTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ToolTip toolTip;
    string stringToShow = null;
    bool mouseCurrentlyHere = false;

    // Start is called before the first frame update
    void Start()
    {
        toolTip = ToolTip.instance;
        SetStringToShow();
    }

    void Update()
    {
        if (mouseCurrentlyHere)
        {
            if (stringToShow != "")
            {
                toolTip.ShowToolTip(stringToShow);
            }
            else
            {
                toolTip.HideToolTip();
            }
        }
    }

    // Gameobject name is 'Tab <tab description>', 
    // sets stringToShow as the <tab description>
    void SetStringToShow() {
        string newStringToShow = this.gameObject.name;
        stringToShow = newStringToShow.Substring(4);
    }

    //  When mouse is over the slot, runs CheckSlot
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        mouseCurrentlyHere = true;
    }

    // Hides tooltip when mouse no longer over object
    public void OnPointerExit(PointerEventData pointerEventData){
        mouseCurrentlyHere = false;
        toolTip.HideToolTip();
    }
}
