using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    float speed;

    Rigidbody rigidBody;
    int damage;

    bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (move) {
            rigidBody.AddForce(transform.forward * speed);
        }
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public void FaceTarget(Transform targetTransform) {
        CharacterController controller = targetTransform.GetComponent<CharacterController>();
        Vector3 newTargetPosition = targetTransform.position + controller.center;
        transform.LookAt(newTargetPosition);
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public void StartMoving() {
        move = true;
    }

    public void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.transform.name == "Player") {
            PlayerStats playerStats = otherCollider.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
            Destroy(this.gameObject);
        } else {
            // explosion effect
        }
        // play explosion sound
    } 
}
