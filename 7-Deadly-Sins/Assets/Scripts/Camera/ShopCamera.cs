using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopCamera : MonoBehaviour
{
    Camera camera;
    [SerializeField]
    GameObject targetLook;
    [SerializeField]
    float offset;

    EquipmentManager equipmentManager;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        transform.position = targetLook.transform.position -
        targetLook.transform.forward * offset;
        transform.LookAt(targetLook.transform);
        equipmentManager = EquipmentManager.instance;
    }

    
    void Update() {
        transform.position = targetLook.transform.position -
        targetLook.transform.forward * offset;
        transform.LookAt(targetLook.transform);
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Mouse clicked");
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1f, false);
            int UILayer = LayerMask.NameToLayer("UI");
            if (Physics.Raycast(ray, out hit, 10000, ~UILayer)) {
                Debug.Log(hit.transform.name);
                int indexOfMesh = IndexOfMesh(hit.transform.name);
                Debug.Log("name: " + hit.transform.name + " index of mesh: " + indexOfMesh);
                if (indexOfMesh > -1) 
                {
                    equipmentManager.Unequip(indexOfMesh);
                }
            }
        }
    }

    int IndexOfMesh(string name) {
        string[] equipmentSlotTypes = (string[]) Enum.GetNames(typeof(EquipmentSlot));
        for (int i = 0 ; i < equipmentSlotTypes.Length; i++) {
            if (equipmentSlotTypes[i] == name) {
                return i;
                            }
        }
        return -1;
    }
}
