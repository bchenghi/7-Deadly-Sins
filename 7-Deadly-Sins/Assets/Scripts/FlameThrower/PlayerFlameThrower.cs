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
    public Others flameThrowerObject;
    IUsable flameThrowerItem;
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
        flameThrowerItem = flameThrowerObject as IUsable;
        
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
        Debug.Log("Use FlameThrower");
        if (!inUse)
        {
            inUse = true;
            animator.SetLayerWeight(3, 1);
            flameThrower.SetActive(true);
            if (hasAmmo)
            {
                HotKeyBar.instance.DisableSpecificHotKeyWithItemCheck(flameThrowerItem);
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
            HotKeyBar.instance.DisableSpecificHotKeyWithItemCheck(flameThrowerItem);
            runOutOfTime = false;
            animator.SetLayerWeight(3, 0);
            flameThrower.SetActive(false);
            inUse = false;
            timeChecker = false;
            StartCoroutine(enableHotKey());
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
        HotKeyBar.instance.EnableSpecificHotKeyWithItemCheck(flameThrowerItem);
    }

    IEnumerator enableHotKey()
    {
        yield return new WaitForSeconds(0.1f);
        HotKeyBar.instance.EnableSpecificHotKeyWithItemCheck(flameThrowerItem);

    }
}
