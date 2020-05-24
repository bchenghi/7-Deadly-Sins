using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singelton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion
    public Transform Shield;
    public Transform Sword;

    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
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


public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = Unequip(slotIndex);


        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, null);
        }

        SetEquipmentBlendShapes(newItem, 100);
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        Debug.Log("New mesh is " + newMesh);
        currentMeshes[slotIndex] = newMesh;

        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            newMesh.rootBone = Sword;
        }
        else if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Shield)
        {
            newMesh.rootBone = Shield;
        }
        else
        {
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
        }


    }

    /*
    public void Equip(Equipment newEquipment)
    {
        int slotIndex = (int)newEquipment.equipmentSlot;
        Equipment oldEquipment = Unequip(slotIndex);
        currentEquipment[slotIndex] = newEquipment;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }

        SetEquipmentBlendShapes(newEquipment, 100);

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newEquipment.mesh);
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }
    */

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}

