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
        AmmoToReload = turretShooter.ammoCount;
    }

    


    public void Reload()
    {
        if (Inventory.instance.getValue(AmmoRequired) >= AmmoToReload)
        {
            StartCoroutine(reloadTimeRoutine(reloadTime));
        } else
        {
            Debug.Log("Not enough Ammo");
            DisplayTextManager.instance.Display("Not enough Ammo", 2f);
        }
    }


    IEnumerator reloadTimeRoutine(float time)
    {
        ReloadImage.SetActive(true);
        yield return new WaitForSeconds(time);
        Inventory.instance.Remove(AmmoRequired, AmmoToReload);
        ReloadImage.SetActive(false);
        turretShooter.ammoCount += AmmoToReload;
        isReloading = false;
    }
}
