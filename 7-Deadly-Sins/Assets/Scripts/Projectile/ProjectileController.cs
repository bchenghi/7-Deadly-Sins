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
        Debug.Log("name of collider" + otherCollider.transform.name);
        if (otherCollider.transform.GetComponent<Enemy>() != null && 
        otherCollider.transform.GetComponent<Enemy>().name == "Clown") 
        {
            return;
        }
        else 
        {
            if (otherCollider.transform.name == "Player") 
            {
                PlayerStats playerStats = otherCollider.GetComponent<PlayerStats>();
                playerStats.TakeDamage(damage);
            } 
            if (otherCollider.transform.GetComponent<CharacterStats>()) {
                CharacterStats characterStats = otherCollider.GetComponent<CharacterStats>();
                characterStats.TakeDamage(damage);
            }
            else 
            {
                // explosion effect when hit wall
            }
            AudioManager.instance.Play(otherCollider.transform, "Magic Impact");
            Destroy(this.gameObject); 
        }
        
    } 
}
