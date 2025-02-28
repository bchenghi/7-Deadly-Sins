﻿using System.Collections;


using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    public int Damage;
    public bool dealsDamageOverTime;
    public bool RandomPusher;
    private bool TakingDamage;
    private bool intervalOver = true;
    public float pushX;
    public float pushY;
    public float pushZ;
    private bool playerTakenDamage;
   

    private void OnTriggerEnter(Collider other)
    {
        if (dealsDamageOverTime && other.GetComponent<PlayerStats>())
        {
            TakingDamage = true;
        }

        else if (RandomPusher && other.GetComponent<PlayerStats>())
        {
            other.transform.GetComponent<CharacterController>().enabled = false;
            pushX = Random.Range(-pushX, pushX);
            pushY = Random.Range(-pushY, pushY);
            pushZ = Random.Range(-pushZ, pushZ);
            Debug.Log(pushX + " " + pushY + " " + pushZ);
            Vector3 newPos = other.transform.position + new Vector3(pushX, pushY, pushZ);
            Debug.Log(other.transform);
            other.transform.position = newPos;
            other.transform.GetComponent<CharacterController>().enabled = true;
            Debug.Log(other.transform.position);

            other.GetComponent<PlayerStats>().TakeDamage(Damage);
        }
        else if (other.GetComponent<PlayerStats>())
        {
            if (!playerTakenDamage)
            {
                other.GetComponent<PlayerStats>().TakeDamage(Damage);
                playerTakenDamage = true;
            }
        }
        
    }

    IEnumerator takenDamgeRoutine()
    {
        yield return new WaitForSeconds(1f);
        playerTakenDamage = false;
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakingDamage = false;
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
