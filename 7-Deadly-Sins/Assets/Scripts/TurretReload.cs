using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretReload : MonoBehaviour
{
    TurretPlayerDetection PlayerDetector;
    TurretShooting turretShooter;
    public float reloadTime;
    public bool canReload;
    public Item AmmoRequired;
    int AmmoToReload;
    public GameObject ReloadImage;
    private bool isReloading;

    private void Start()
    {
        PlayerDetector = GetComponent<TurretPlayerDetection>();
        turretShooter = GetComponent<TurretShooting>();
        AmmoToReload = turretShooter.ammoCount;
        
        

    }

    private void Update()
    {
        if (turretShooter.OutOfAmmo)
        {
            canReload = true;
        } else
        {
            canReload = false;
        }

        if (canReload && Input.GetKeyDown(KeyCode.E) && !isReloading)
        {
            isReloading = true;
            Reload();
            
            canReload = false;
        }
    }


    public void Reload()
    {
        if (Inventory.instance.getValue(AmmoRequired) >= AmmoToReload)
        {
            StartCoroutine(reloadTimeRoutine(reloadTime));
        } else
        {
            Debug.Log("Not enough Ammo");
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
