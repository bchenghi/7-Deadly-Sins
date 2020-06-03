using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUIManager : MonoBehaviour
{
    GameObject inventoryUI;
    GameObject equipmentUI;

    void Start()
    {
        inventoryUI = GetComponent<InventoryUI>().inventoryUI;
        equipmentUI = GetComponent<EquipmentUI>().equipmentUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipmentUI.SetActive(false);
        }
        if (Input.GetButtonDown("EquipmentUI"))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
            inventoryUI.SetActive(false);
        }
    }
}
