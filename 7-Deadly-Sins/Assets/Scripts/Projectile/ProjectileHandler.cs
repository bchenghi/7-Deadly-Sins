using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    public Transform projectileFirePoint;
    

    public void ShootProjectile(int damage, float speed, Transform targetTransform) {
        GameObject newProjectile = Instantiate(projectile, projectileFirePoint);
        newProjectile.SetActive(true);
        ProjectileController projectileController = newProjectile.GetComponent<ProjectileController>();
        projectileController.FaceTarget(targetTransform);
        projectileController.SetSpeed(speed);
        projectileController.SetDamage(damage);
        projectileController.StartMoving();
    }
}
