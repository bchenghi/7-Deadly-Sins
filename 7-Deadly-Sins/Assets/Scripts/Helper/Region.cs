using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    public Cube[] regions;

    public Vector3 RandomPosition() {
        int indexOfSpawnRegion = Random.Range(0, regions.Length);

        Vector3 lowerXYZ = regions[indexOfSpawnRegion].lowerXYZValues;
        Vector3 higherXYZ = regions[indexOfSpawnRegion].higherXYZValues;
        float x = Random.Range(lowerXYZ[0], higherXYZ[0]);
        float y = Random.Range(lowerXYZ[1], higherXYZ[1]);
        float z = Random.Range(lowerXYZ[2], higherXYZ[2]);
        return new Vector3(x, y, z);
    }

    // returns distance of furthest two points in the regions
    public float Diameter() {
        float distance = -1;
        if (regions.Length > 0) {
            Vector3 minLowerXYZ = regions[0].lowerXYZValues;
            Vector3 maxHigherXYZ = regions[0].higherXYZValues;
            foreach (Cube cube in regions) {
                Vector3 lower = cube.lowerXYZValues;
                Vector3 higher = cube.higherXYZValues;
                if (lower.x + lower.y + lower.z < minLowerXYZ.x + minLowerXYZ.y + minLowerXYZ.z) {
                    minLowerXYZ = lower;
                }
                if (higher.x + higher.y + higher.z > maxHigherXYZ.x + maxHigherXYZ.y + maxHigherXYZ.z) {
                    maxHigherXYZ = higher;
                }
            }
            distance = Vector3.Distance(minLowerXYZ, maxHigherXYZ);
        }

        return distance;
        
    }

}
