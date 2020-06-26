using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [SerializeField]
    GameObject[] lootDrops;

    [SerializeField]
    float maxDistanceFromTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropLoot() {
        foreach(GameObject loot in lootDrops) {
            Vector3 locationToSpawn = LocationToSpawnLoot(loot);
            if (!locationToSpawn.Equals(Vector3.zero)) {
                Instantiate(loot, locationToSpawn, Quaternion.identity);
            } else {
                continue;
            }
        }
    }

    // Returns a random location to spawn the loot
    Vector3 SpawnLocation(GameObject loot) {
        BoxCollider collider = loot.GetComponent<BoxCollider>();
        Debug.Log("box collider of loot is " + collider);
        Vector3 result = transform.position;
        result += new Vector3(0, collider.size.y/2, 0);
        float x = Random.Range(0, maxDistanceFromTransform);
        float z = Mathf.Sqrt(Mathf.Pow(maxDistanceFromTransform,2) - Mathf.Pow(x, 2));
        x *= (Random.Range(0,2)*2-1);
        z *= (Random.Range(0,2)*2-1);
        result += new Vector3(x, 0 ,z);
        return result;
    }

    // Return true if loot can be spawned at random location without colliding with other objects, 
    // otherwise false.
    bool TestSpawnLocation(GameObject loot, Vector3 location) {
        BoxCollider collider = loot.GetComponent<BoxCollider>();
        Vector3 overlapBoxScale = new Vector3(collider.size.x, collider.size.y, collider.size.z);
        Collider[] collidersInBox = new Collider[1];
        int ignoreRaycastLayer = LayerMask.NameToLayer("ignore Raycast");
        int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(location, overlapBoxScale, collidersInBox, 
        transform.rotation, ~ignoreRaycastLayer);
        if (numberOfCollidersFound == 0) {
            return true;
        } 
        else 
        {
            Debug.Log("Collided with " + collidersInBox[0].ToString());
            return false;
        }
    }

    // Randomly generates locations to spawn a loot, and tests if it can be spawned. 
    // Will give up after a given number of tests. Will return a location that can spawn loot.
    Vector3 LocationToSpawnLoot(GameObject loot) {
        BoxCollider collider = loot.GetComponent<BoxCollider>();
        bool found = false;
        int maxTries = 5;
        int numOfTries = 0;
        Vector3 location = Vector3.zero;
        while (!found && numOfTries < maxTries) {
            location = SpawnLocation(loot);
            found = TestSpawnLocation(loot, location);
            numOfTries++;
        }
        if (!found) {
            Debug.Log("max tries to find location reached");
        }
        return location - new Vector3(0, collider.size.y / 2, 0);
    }

}
