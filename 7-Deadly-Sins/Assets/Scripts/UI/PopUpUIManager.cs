using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpUIManager : MonoBehaviour
{
    GameObject inventoryUI;
    GameObject equipmentUI;
    public GameObject skillTreeUI;
    Vector3 ResetSize;
    Vector3 currentSize;
    void Start()
    {
        inventoryUI = GetComponent<InventoryUI>().inventoryUI;
        equipmentUI = GetComponent<EquipmentUI>().equipmentUI;
        ResetSize = new Vector3(0,0,0);
        currentSize = new Vector3(0.7f ,0.8f, 1.0f);
        skillTreeUI.transform.localScale = ResetSize;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Shop-CH") {
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

        if (Input.GetButtonDown("skillTree"))
        {

            if (skillTreeUI.transform.localScale != currentSize)
            {
               
                skillTreeUI.transform.localScale = currentSize;
            } else
            {
                skillTreeUI.transform.localScale = ResetSize;
            }
            
            
        }
    }
}
