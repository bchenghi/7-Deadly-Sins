using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTexts : MonoBehaviour
{
    public float DestroyTime = 3f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public Vector3 RandomizeIntensity = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);

        transform.localPosition += Offset;
        /*transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            (Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            (Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
        */
        // use for enemy damage
    }

    private void LateUpdate()
    {
        var cameraToLookAt = Camera.main;
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);


    }


}
