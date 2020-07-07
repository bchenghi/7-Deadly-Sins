using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDamage : MonoBehaviour
{
    public int Damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerStats>())
        {
            other.transform.GetComponent<PlayerStats>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}
