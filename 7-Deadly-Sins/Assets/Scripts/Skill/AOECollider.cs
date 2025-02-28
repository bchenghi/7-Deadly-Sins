﻿using System.Collections;
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
            
            //Debug.Log(other.transform.GetComponent<EnemyStats>().currentHealth);
            if (other.transform.GetComponent<EnemyStats>().currentHealth > 0)
            {
                StartCoroutine(StunCoolDown(stunTime, other));
            }

            Debug.Log(other.transform.GetComponent<CharacterAnimator>());
            if (other.transform.GetComponent<CharacterAnimator>().reactsToDamage == false)
            {
                other.transform.GetComponentInChildren<Animator>().SetTrigger("Hurt");
            }








        } else if (other.GetComponent<ClownStats>() && isActive && other != null){
            other.transform.GetComponent<ClownStats>().TakeDamage((int) damage);
        }
    }


    IEnumerator StunCoolDown(float time, Collider other)
    {
        if (other != null)
        {
            if (other.transform.GetComponent<EnemyController>())
            {
                GameObject Go = GameObject.Instantiate(StunCircle, other.transform.position, Quaternion.identity);
                Go.transform.position = new Vector3(Go.transform.position.x, Go.transform.position.y + other.transform.GetComponent<CapsuleCollider>().height + 0.2f, Go.transform.position.z);
                other.transform.GetComponent<EnemyController>().StopMovement();
                yield return new WaitForSeconds(time);
                Destroy(Go);
                if (other != null)
                {
                    other.transform.GetComponent<EnemyController>().StartMovement();
                }

            }
            else
            {
                GameObject Go = GameObject.Instantiate(StunCircle, other.transform.position, Quaternion.identity);
                Go.transform.position = new Vector3(Go.transform.position.x, Go.transform.position.y + other.transform.GetComponent<CapsuleCollider>().height + 0.2f, Go.transform.position.z);
                other.transform.GetComponent<FinalEnemyController>().StopMovement();
                yield return new WaitForSeconds(time);
                Destroy(Go);
                other.transform.GetComponent<FinalEnemyController>().StartMovement();



            }
        }
    }
}
