using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float TimeToDrop;
    public bool PlayerDetected;
    private bool timeUp;
    public float speed;
    float OriginalX;
    float OriginalY;
    float OriginalZ;



    private void Start()
    {
         OriginalX = transform.position.x; 
         OriginalY = transform.position.y;
         OriginalZ = transform.position.z;
    }

    private void Update()
    {
        if (PlayerDetected && !timeUp)
        {
            StartCoroutine(DropTime(TimeToDrop));
            float moveX = Random.Range(0, xOffset) + OriginalX;
            float moveY = Random.Range(0, yOffset) + OriginalY;
            float moveZ = Random.Range(0, zOffset) + OriginalZ;

            transform.position = new Vector3(moveX, moveY, moveZ);
           

        }

        if (timeUp)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);
        }
    }

    IEnumerator DropTime(float Time)
    {
        yield return new WaitForSeconds(Time);
        timeUp = true;

       
        
    }


}
