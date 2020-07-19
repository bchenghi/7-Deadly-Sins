using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null) {
            Debug.Log("more than one instance of equipment managers found!");
            return;
        }
        instance = this;
    }
    #endregion

    public Transform Shield;
    public Transform Sword;

    public SkinnedMeshRenderer targetMesh;
    public Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    public Equipment[] defaultItems;

    public delegate void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChanged;
    
    private void Start()
    {
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numberOfSlots];
        currentMeshes = new SkinnedMeshRenderer[numberOfSlots];
        EquipDefaultItems();
        
    }

    public int GetWeaponIndex()
    {
        return Array.IndexOf(Enum.GetValues(typeof(EquipmentSlot)), EquipmentSlot.Weapon);
    }


    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = Unequip(slotIndex);


        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, null);
        }

        
        currentEquipment[slotIndex] = newItem;
        SetEquipmentMesh(newItem);


        /*if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            newMesh.rootBone = Sword;
        }
        //else if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Shield)
        {
            newMesh.rootBone = Shield;
        }
        else
        {
        */

        
    }



    public Equipment Unequip(int slotIndex)
    {
        Equipment equipment = currentEquipment[slotIndex];
        if (equipment != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            SetEquipmentBlendShapes(equipment, 0);
            Inventory.instance.Add(equipment);
            currentEquipment[slotIndex] = null;


            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, equipment);
            }
        }

        return equipment;
    }

    public void SetEquipmentMesh(Equipment equipment) {
        int slotIndex = (int) equipment.equipmentSlot;
        SetEquipmentBlendShapes(equipment, 100);
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(equipment.mesh);
        Debug.Log("New mesh is " + newMesh);
        currentMeshes[slotIndex] = newMesh;
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
    }

    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++) 
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment equipment, int weight)
    {
        foreach(EquipmentMeshRegion blendShape in equipment.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach(Equipment equipment in defaultItems)
        {
            Equip(equipment);
        }
    }


    // Removes from current equipment, doesnt add to inventory, then requips the equipment
    public Equipment UnequipWithoutAddingToInventory(int slotIndex) {
        Equipment equipment = currentEquipment[slotIndex];
        if (equipment != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            SetEquipmentBlendShapes(equipment, 0);
            //Inventory.instance.Add(equipment);
            currentEquipment[slotIndex] = null;


            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, equipment);
            }

            
        }
        return equipment;

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    public void EquipCurrentItems() {
        foreach(Equipment equipment in currentEquipment) {
            if (equipment != null)
                Equip(equipment);
        }
    }

    public bool IsEquipped(Equipment equipment) {
        bool result = false;
        foreach(Equipment wornEquipment in currentEquipment) {
            if (wornEquipment != null && wornEquipment.Equals(equipment)) {
                result = true;
                break;
            } 
        }
        return result;
    }

    public Equipment ReferenceToEquipment(Equipment equipment) {
        Equipment reference = null;
        foreach(Equipment wornEquipment in currentEquipment) {
            if (wornEquipment != null && wornEquipment.Equals(equipment)) {
                reference = wornEquipment;
                break;
            }
        }
        return reference;
    }
}

