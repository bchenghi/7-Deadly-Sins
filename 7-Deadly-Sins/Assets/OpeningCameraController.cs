using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCameraController : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    float distanceFromPlayer;
    float heightOfPlayer;
    Vector3 offsetVector;
    // Start is called before the first frame update
    void Start()
    {
        heightOfPlayer = playerTransform.gameObject.GetComponent<CharacterController>().height;
        offsetVector = new Vector3(0, 0, -distanceFromPlayer) + new Vector3(0, heightOfPlayer/2, 0);
        transform.position = playerTransform.position + offsetVector;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offsetVector;
    }
}
