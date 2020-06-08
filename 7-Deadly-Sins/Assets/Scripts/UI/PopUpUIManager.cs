using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUIManager : MonoBehaviour
{
    GameObject inventoryUI;
    GameObject equipmentUI;
    public GameObject skillTreeUI;
    Vector3 SkillTreeCurrentPosition;
    void Start()
    {
        inventoryUI = GetComponent<InventoryUI>().inventoryUI;
        equipmentUI = GetComponent<EquipmentUI>().equipmentUI;
        SkillTreeCurrentPosition= skillTreeUI.transform.position;
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
        if (Input.GetButtonDown("skillTree"))
        {
            

            if (skillTreeUI.transform.position != SkillTreeCurrentPosition)
            {
                skillTreeUI.transform.position = SkillTreeCurrentPosition;
            } else
            {
                skillTreeUI.transform.position = new Vector3(100, skillTreeUI.transform.position.y, skillTreeUI.transform.position.z);
            }
            
            
        }
    }
}
