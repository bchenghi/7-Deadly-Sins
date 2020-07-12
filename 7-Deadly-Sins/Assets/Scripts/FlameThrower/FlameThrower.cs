using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public int damage;
    public float timeBetweenDamage;
    private bool hasTakenDamage;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<EnemyStats>())
        {
            if (!hasTakenDamage)
            {
                hasTakenDamage = true;
                other.transform.GetComponent<EnemyStats>().TakeDamage(damage);
                StartCoroutine(damageCooldown(timeBetweenDamage));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyStats>())
        {
            hasTakenDamage = false;
        }
    }

    IEnumerator damageCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        hasTakenDamage = false;
    }
}
