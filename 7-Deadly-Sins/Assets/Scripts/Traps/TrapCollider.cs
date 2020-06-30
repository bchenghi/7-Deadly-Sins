using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TrapCollider : MonoBehaviour
{
    public int Damage;
    public bool dealsDamageOverTime;
    private bool TakingDamage;
    private bool intervalOver = true;


    private void OnTriggerEnter(Collider other)
    {
        if (dealsDamageOverTime)
        {
            TakingDamage = true;
        }
        else if (other.GetComponent<PlayerStats>())
        {
            other.GetComponent<PlayerStats>().TakeDamage(Damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerStats>())
        {
            TakingDamage = false;
        }
    }

    private void Update()
    {
        if (TakingDamage && dealsDamageOverTime)
        {
            if (intervalOver)
            {
                StartCoroutine(TakeDamageInterval());
            }

        }
    }

    IEnumerator TakeDamageInterval()
    {
        intervalOver = false;
        PlayerManager.instance.player.GetComponent<PlayerStats>().TakeDamage(Damage);
        yield return new WaitForSeconds(4f);
        intervalOver = true;

    }

    


}
