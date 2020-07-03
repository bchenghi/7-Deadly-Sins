using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public GameObject TurretHeadObject;
    Transform TurretHead;
    public float turretRotationSpeed;
    public float maxX;
    public float maxY;
    

    // Start is called before the first frame update
    void Start()
    {
        TurretHead = TurretHeadObject.transform;
        TurretHead.rotation = Quaternion.Euler(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        TurretHead.eulerAngles = new Vector3(0, (TurretHead.localEulerAngles.y + mouseX), (TurretHead.localEulerAngles.z + mouseY));

        Vector3 newRotation = TurretHead.localEulerAngles;
        newRotation.y += mouseX * turretRotationSpeed;
        newRotation.z += mouseY * turretRotationSpeed;
        newRotation.y = ClampAngle(newRotation.y, -maxX, maxX);
        newRotation.z = ClampAngle(newRotation.z, -maxY, maxY);
        TurretHead.eulerAngles = newRotation;
        
    }


    public float ClampAngle(float angle, float min, float max) {
 
     if (angle<90 || angle>270){       // if angle in the critic region...
         if (angle>180) angle -= 360;  // convert all angles to -180..+180
         if (max>180) max -= 360;
         if (min>180) min -= 360;
     }
     angle = Mathf.Clamp(angle, min, max);
     if (angle<0) angle += 360;  // if angle negative, convert to 0..360
     return angle;
 }
}
