using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTurret : MonoBehaviour
{
    private GameObject target;
    private bool targetLocked;
    public GameObject[] bulletSpawnPoint;
    public GameObject turretHead;
    public GameObject bullet;
    public float fireTimer;
    private bool shotReady;
    public float range;
    private bool isUpgrading;
    private List<Collider> CollidersInRange;
    private GameObject previousTarget;

    


    private void Start()
    {
        shotReady = true;
        previousTarget = null;
        CollidersInRange = new List<Collider>();
        
    }

    private void Update()
    {
        Debug.Log(target);
        if (!GetComponent<AutomaticTurretUpgrading>())
        {
            isUpgrading = false;
        }
        else
        {
            isUpgrading = GetComponent<AutomaticTurretUpgrading>().isUpgrading;
        }
        if (target == null)
        {
            targetLocked = false;
        }
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
            Vector3 pos = target.transform.position;
            pos.y += 0.7f;
            turretHead.transform.LookAt(pos);
            turretHead.transform.Rotate(new Vector3(0, -90, 0));

            if (shotReady && isUpgrading == false)
            {
                Shoot();
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!CollidersInRange.Contains(other))
        {
            CollidersInRange.Add(other);
        }

        for (int i = 0; i < CollidersInRange.Count; i++) {
            if (CollidersInRange[i] != null)
            {
                if (CollidersInRange[i].CompareTag("Enemy"))
                {
                    if (target != null)
                    {
                        targetLocked = true;
                    }
                    else
                    {
                        if (CollidersInRange[i].gameObject != previousTarget)
                        {
                            targetLocked = false;
                            target = CollidersInRange[i].gameObject;
                            previousTarget = CollidersInRange[i].gameObject;
                           
                            break;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CollidersInRange.Contains(other))
        {
            if (other.gameObject == previousTarget)
            {
                previousTarget = null;
            }
            CollidersInRange.Remove(other);
        }
    }

    public void Shoot()
    {
        for (int i = 0; i < bulletSpawnPoint.Length; i++)
        {
            Transform _bullet = Instantiate(bullet.transform, bulletSpawnPoint[i].transform.position, Quaternion.identity);
            _bullet.transform.rotation = bulletSpawnPoint[i].transform.rotation;
            shotReady = false;
            StartCoroutine(FireRate());
        }

    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        shotReady = true;
    }

    public void SetTargetNull()
    {
        
        target = null;
        targetLocked = false;
        
    }
}
