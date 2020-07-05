using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTurretBullet : MonoBehaviour
{
    public float movementSpeed;
    public int Damage;
    public AutomaticTurret turret;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyStats>())
        {
            other.GetComponent<EnemyStats>().TakeDamage(Damage);
            if (other.GetComponent<EnemyStats>().currentHealth <= 0)
            {
                turret.SetTargetNull();
            }
            Destroy(this.gameObject);

        }
    }
}
