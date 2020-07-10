using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingQuestion : MonoBehaviour
{
    public Vector3 Offset = new Vector3(0, 2, 0);
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition += Offset;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        var cameraToLookAt = Camera.main;
        transform.LookAt(cameraToLookAt.transform);
        


    }
}
