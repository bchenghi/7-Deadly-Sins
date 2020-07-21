using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditedTurretReload : MonoBehaviour
{
    EditedTurretPlayerDetection PlayerDetector;
    EditedTurretShooting turretShooter;
    public float reloadTime;
    public Item AmmoRequired;
    int AmmoToReload;
    public GameObject ReloadImage;
    private bool isReloading;

    private void Start()
    {
        PlayerDetector = GetComponent<EditedTurretPlayerDetection>();
        turretShooter = GetComponent<EditedTurretShooting>();
        AmmoToReload = turretShooter.maxAmmo;
    }

    


    public bool Reload()
    {
        if (Inventory.instance.getValue(AmmoRequired) >= AmmoToReload)
        {
            StartCoroutine(reloadTimeRoutine(reloadTime));
            return true;
        } else
        {
            Debug.Log("Not enough Ammo");
            DisplayTextManager.instance.Display("Not enough Ammo", 2f);
            return false;
        }
    }


    IEnumerator reloadTimeRoutine(float time)
    {
        ReloadImage.SetActive(true);
        int ammoRemovedFromInventory = 0;
        for (int i = AmmoToReload ; i > 0 ; i--) {
            if (Inventory.instance.Remove(AmmoRequired, i)) {
                ammoRemovedFromInventory = i;
                break;
            }
        }
        turretShooter.ammoCount += ammoRemovedFromInventory;
        yield return new WaitForSeconds(time);
        ReloadImage.SetActive(false);
        if (turretShooter.onAmmoChange != null) {
            turretShooter.onAmmoChange(ammoRemovedFromInventory, AmmoToReload);
        }
        isReloading = false;
    }
}
