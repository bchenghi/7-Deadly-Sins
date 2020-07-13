using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlameThrower : MonoBehaviour
{
    public GameObject flameThrower;
    ParticleSystem[] ps;
    Animator animator;
    public bool inUse;
    public Item ammo;
    private bool hasAmmo;
    public float UseTimePerAmmo;
    private bool runOutOfTime;
    private bool timeChecker;


    private void Start()
    {
        flameThrower.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        inUse = false;
        ps = flameThrower.GetComponentsInChildren<ParticleSystem>();
        runOutOfTime = false;
        
    }

    private void Update()
    {
        if (inUse && !timeChecker)
        {
            timeChecker = true;
            StartCoroutine(timeCheck(UseTimePerAmmo));
        }
        if (runOutOfTime)
        {
            foreach (ParticleSystem ps in ps)
            {
                ps.Stop();
            }
            runOutOfTime = false;
            flameThrower.GetComponent<BoxCollider>().enabled = false;
        }
        //Debug.Log(runOutOfTime);
    }


    public void UseFlameThrower()
    {
        checkForAmmo();
        if (!inUse)
        {
            inUse = true;
            animator.SetLayerWeight(3, 1);
            flameThrower.SetActive(true);
            if (hasAmmo)
            {
                Inventory.instance.Remove(ammo, 1);
                flameThrower.GetComponent<BoxCollider>().enabled = true;
                foreach (ParticleSystem ps in ps)
                {
                    ps.Play();
                }
                
                
            } else
            {
                flameThrower.GetComponent<BoxCollider>().enabled = false;
                foreach(ParticleSystem ps in ps)
                {
                    ps.Stop();
                }
            }
            
        } else
        {
            runOutOfTime = false;
            animator.SetLayerWeight(3, 0);
            flameThrower.SetActive(false);
            inUse = false;
            timeChecker = false;
        }
    }

    private void checkForAmmo()
    {
        if (Inventory.instance.getValue(ammo) > 0)
        {
            hasAmmo = true;
        } else
        {
            hasAmmo = false;
        }
    }

    IEnumerator timeCheck(float time)
    {
        yield return new WaitForSeconds(time);
        runOutOfTime = true;
    }
}
