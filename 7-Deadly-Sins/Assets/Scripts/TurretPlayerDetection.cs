

using UnityEngine;

public class TurretPlayerDetection : MonoBehaviour
{
    public GameObject crossHair;
    Transform target;
    private bool Shooting;
    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !Shooting)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                crossHair.SetActive(true);
                target = other.transform;
                transform.GetComponentInParent<TurretShooting>().playerDetected = true;
                Shooting = true;

            }

        } else if (Input.GetKeyUp(KeyCode.E) && Shooting) 
        {
            crossHair.SetActive(false);
            Shooting = false;
            transform.GetComponentInParent<TurretShooting>().playerDetected = false;
        }
               

            
        
        
        if (transform.GetComponentInParent<TurretShooting>().playerDetected)
        {
            target.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            target.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        }

       
    }

    public void OffCrossHair()
    {
        crossHair.SetActive(false);
    }

    private void Update()
    {
        
    }

}
