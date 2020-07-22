using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOECollider : MonoBehaviour
{
    public GameObject StunCircle;
    public float damage;
    public float stunTime;
    public bool isActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyStats>() && isActive && other != null)
        {
            other.transform.GetComponent<EnemyStats>().TakeDamage((int)damage);
            Debug.Log(other.transform.GetComponent<EnemyStats>().currentHealth);
            if (other.transform.GetComponent<EnemyStats>().currentHealth > 0)
            {
                StartCoroutine(StunCoolDown(stunTime, other));
            }








        }
    }


    IEnumerator StunCoolDown(float time, Collider other)
    {
       
        if (other.transform.GetComponent<EnemyController>())
        {
            GameObject Go = GameObject.Instantiate(StunCircle, other.transform.position, Quaternion.identity);
            Go.transform.position = new Vector3(Go.transform.position.x, Go.transform.position.y + other.transform.GetComponent<CapsuleCollider>().height + 0.2f, Go.transform.position.z);
            other.transform.GetComponent<EnemyController>().StopMovement();
            yield return new WaitForSeconds(time);
            Destroy(Go);
            other.transform.GetComponent<EnemyController>().StartMovement();
            
        } else
        {
            GameObject Go = GameObject.Instantiate(StunCircle, other.transform.position, Quaternion.identity);
            Go.transform.position = new Vector3(Go.transform.position.x, Go.transform.position.y + Go.transform.GetComponent<CapsuleCollider>().height, Go.transform.position.z);
            other.transform.GetComponent<FinalEnemyController>().StopMovement();
            yield return new WaitForSeconds(time);
            other.transform.GetComponent<FinalEnemyController>().StartMovement();
            Destroy(Go);
            
            
        }
    }
}
