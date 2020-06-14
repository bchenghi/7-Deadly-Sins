using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{

    public Transform equipmentParent;
    public GameObject equipmentUI;

    EquipmentManager equipmentManager;
    EquipmentUISlot[] equipmentSlots;

    void Awake() {

    }
    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateUI;
        equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentUISlot>();
    }

    // Clears slot that belonged to oldEquipment and adds newEquipment to the Equipment UI
    void UpdateUI(Equipment newEquipment, Equipment oldEquipment)
    {
        if (oldEquipment != null)
            equipmentSlots[(int) oldEquipment.equipmentSlot].ClearSlot();
        if (newEquipment != null)
            equipmentSlots[(int) newEquipment.equipmentSlot].AddEquipment(newEquipment);
    }

}
