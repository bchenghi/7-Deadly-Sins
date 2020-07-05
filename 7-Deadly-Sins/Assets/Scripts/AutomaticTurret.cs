using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTurret : MonoBehaviour
{
    private GameObject target;
    private bool targetLocked;
    public GameObject bulletSpawnPoint;
    public GameObject turretHead;
    public GameObject bullet;
    public float fireTimer;
    private bool shotReady;
    public float range;


    private void Start()
    {
        shotReady = true;
    }

    private void Update()
    {
        //Shooting and detecting enemies
        if (targetLocked)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > range)
            {
                target = null;
                targetLocked = false;
                return;
            }
            turretHead.transform.LookAt(target.transform);
            turretHead.transform.Rotate(new Vector3(0, -90, 0));

            if (shotReady)
            {
                Shoot();
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (target != null)
            {
                targetLocked = true;
            }
            else
            {
                targetLocked = false;
                target = other.gameObject;
            }
        }
    }

    public void Shoot()
    {
        Transform _bullet = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        _bullet.transform.rotation = bulletSpawnPoint.transform.rotation;
        shotReady = false;
        StartCoroutine(FireRate());

    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        shotReady = true;
    }

    public void SetTargetNull()
    {
        target = null;
    }
}
