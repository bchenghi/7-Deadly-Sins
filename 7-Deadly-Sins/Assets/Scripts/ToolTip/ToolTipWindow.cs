using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

// Each slot has a tooltip window, displays text when mouse is over the slot
public class ToolTipWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ToolTip toolTip;
    UISlot UISlot;
    string stringToShow = null;
    bool mouseCurrentlyHere = false;

    // Start is called before the first frame update
    void Start()
    {
        toolTip = ToolTip.instance;
        UISlot = GetComponent<UISlot>();
    }

    void Update()
    {
        if (mouseCurrentlyHere)
        {
            if (stringToShow != null)
            {
                toolTip.ShowToolTip(stringToShow);
                CheckSlot();
            }
            else
            {
                toolTip.HideToolTip();
            }
        }
    }

    //  When mouse is over the slot, runs CheckSlot
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        mouseCurrentlyHere = true;
        CheckSlot();
    }

    // Hides tooltip when mouse no longer over object
    public void OnPointerExit(PointerEventData pointerEventData){
        mouseCurrentlyHere = false;
        toolTip.HideToolTip();
    }

    //Hides tooltip when the slot is diabled
    public void OnDisable()
    {
        mouseCurrentlyHere = false;
        toolTip.HideToolTip();
    }

    // Sets the string to be shown on tooltip, sets null if object in slot is null
    public void CheckSlot()
    {
        Item item = null;
        if (UISlot is EquipmentUISlot)
        {
            item = ((EquipmentUISlot)UISlot).equipment;
        }
        else if (UISlot is InventorySlot)
        {
            item = ((InventorySlot)UISlot).item;
        }
        stringToShow = StatsString(item);
    }


    // Returns a string to be displayed on tooltip, null if argument is null, or argument is neither equipment nor item
    string StatsString(Item item)
    {
        if (item is Equipment)
        {
            return EquipmentStatsString((Equipment)item);
        }
        else if (item is Item)
        {
            return ItemStatsString(item);
        }
        else
        {
            return null;
        }
    }

    // Returns string of equipment stats, return null if equipment is null
    string EquipmentStatsString(Equipment equipment)
    {
        if (equipment != null)
        {
            string name = equipment.name;
            int armorModifier = equipment.armorModifier;
            int damageModifier = equipment.damageModifier;
            StringBuilder str = new StringBuilder();
            str.Append(name).AppendLine();
            str.Append("<color=green>Armor Modifier: ").Append(armorModifier).Append("</color>").AppendLine();
            str.Append("<color=red>Damage Modifier: ").Append(damageModifier).Append("</color>");

            return str.ToString();
        }
        else
        {
            return null;
        }
    }

    // Returns name of item, null if item is null
    string ItemStatsString(Item item)
    {
        if (item != null)
            return item.name;

        return null;
    }
}
