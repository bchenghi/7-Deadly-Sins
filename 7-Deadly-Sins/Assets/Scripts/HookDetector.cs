using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hookable"))
        {
            GetComponentInParent<GrapplingHooks>().hooked = true;
            GetComponentInParent<GrapplingHooks>().hookedObj = other.gameObject;
        }
    }
}
