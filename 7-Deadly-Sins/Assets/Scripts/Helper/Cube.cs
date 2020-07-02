using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Represents a cube using the two positions
[System.Serializable]
public class Cube
{
    // e.g. (0,0,0)
    public Vector3 lowerXYZValues;
    // e.g. (1,1,1)
    public Vector3 higherXYZValues;
}
