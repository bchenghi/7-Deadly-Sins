using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOECollider : MonoBehaviour
{
    public float damage;
    public float stunTime;
    public bool isActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyStats>() && isActive && other != null)
        {
            other.transform.GetComponent<EnemyStats>().TakeDamage((int)damage);
            if (other.transform.GetComponent<EnemyStats>().currentHealth != 0)
            {
                StartCoroutine(StunCoolDown(stunTime, other));
            }








        }
    }


    IEnumerator StunCoolDown(float time, Collider other)
    {
        if (other.transform.GetComponent<EnemyController>())
        {
            other.transform.GetComponent<EnemyController>().StopMovement();
            yield return new WaitForSeconds(time);
            other.transform.GetComponent<EnemyController>().StartMovement();
        } else
        {
           
            other.transform.GetComponent<FinalEnemyController>().StopMovement();
            yield return new WaitForSeconds(time);
            other.transform.GetComponent<FinalEnemyController>().StartMovement();
            
            
        }
    }
}
