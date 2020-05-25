using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUISlot : MonoBehaviour
{

    public Image icon;
    public Button removeButton;
    Equipment equipment;

    public void AddEquipment(Equipment equipment)
    {
        this.equipment = equipment;

        icon.sprite = equipment.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        equipment = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnEquipmentRemoveButton()
    {
        EquipmentManager.instance.Unequip((int) equipment.equipmentSlot);
    }

    public void DoSomething()
    {
        //Click on Equipment to do sth.. show stats etc.
    }
}
