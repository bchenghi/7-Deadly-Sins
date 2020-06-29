using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monobehaviour : MonoBehaviour
{
    public static Monobehaviour instance;

    void Start()
    {
        Monobehaviour.instance = this;
    }
}
