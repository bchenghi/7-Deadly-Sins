using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
   

    void Awake()
    {
        
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("Awake: " + this.gameObject);
        
    }
}
