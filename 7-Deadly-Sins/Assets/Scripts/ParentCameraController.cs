using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCameraController : MonoBehaviour
{
    public bool lockCursor;
    bool inventoryUIUsed = false;

    public bool changeTransparency;
    public SkinnedMeshRenderer targetRenderer;

    public Transform target;
    public float dstFromTarget = 2;
    public float dstToFade = 1;

    float yaw;
    float pitch;
    Vector3 currentRotation;
    public float rotationSmoothTime = 8;
    float minPitch = -40;
    float maxPitch = 85;
    public float mouseSensitivity = 10;

    Camera cam;
    EditedThirdPersonCamera cameraController;

    public float distFromWall = 0.7f;

    public LayerMask collisionMask;

    public float moveSpeed = 5;

    public GameObject inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        cam = GetComponentInChildren<Camera>();
        cameraController = cam.GetComponent<EditedThirdPersonCamera>();
    }

    // Update is called once per frame
    // Sets rotationand position of parent camera and CollisionCheck method
    void LateUpdate()
    {
        if (inventoryUIUsed = inventoryUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (!inventoryUIUsed)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
        currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);

        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * dstFromTarget;
        CollisionCheck();

    }

    // Checks for collision, shifts transform of camera
    void CollisionCheck()
    {
        RaycastHit hit;
        if (Physics.Linecast(target.position, transform.position, out hit, collisionMask))
        {
            Vector3 norm = hit.normal * distFromWall;
            Vector3 p = hit.point + norm;
            cameraController.ShiftTransform(p);
        }
        else
        {
            cam.transform.position = transform.position;
        }
        if (changeTransparency)
            cameraController.TransparencyCheck();
    }

    

}
