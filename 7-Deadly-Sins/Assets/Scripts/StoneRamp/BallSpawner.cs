using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject[] balls;
    public float time;

    private void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        StartCoroutine(ballSpawn());
    }

    IEnumerator ballSpawn()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            GameObject.Instantiate(balls[i], transform.position, transform.rotation);
            yield return new WaitForSeconds(time);
        }
    }
}
