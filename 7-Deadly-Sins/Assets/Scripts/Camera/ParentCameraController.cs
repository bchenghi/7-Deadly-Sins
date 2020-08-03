using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCameraController : MonoBehaviour
{
    public bool lockCursor;
    bool inventoryUIUsed = false;
    bool equipmentUIUsed = false;
    bool ChestUIUsed = false;
    bool dialogueBoxOn = false;
    bool skillTreeOn = false;
    bool pauseMenuOn = false;
    bool GameOverOn = false;

    bool controlsOn = false;
    
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
    public GameObject equipmentUI;
    public GameObject chestInventory;
    public GameObject SkillTreeUI;
    public GameObject dialogueBox;
    public GameObject pauseMenu;
    GameObject pauseMenuUI;
    GameObject controls;
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        pauseMenuUI = pauseMenu.transform.Find("Menu").gameObject;
        controls = pauseMenu.transform.Find("Controls").gameObject;
        cam = GetComponentInChildren<Camera>();
        cameraController = cam.GetComponent<EditedThirdPersonCamera>();
        //dstFromTarget += distFromWall;
    }

    // Update is called once per frame
    // Sets rotationand position of parent camera and CollisionCheck method
    void LateUpdate()
    {
        if ((inventoryUIUsed = inventoryUI.activeSelf) || (equipmentUIUsed = equipmentUI.activeSelf)
            || (ChestUIUsed = chestInventory.activeSelf) || (SkillTreeUI.transform.localScale != new Vector3(0, 0, 0))
            || (dialogueBoxOn = dialogueBox.activeSelf) || (pauseMenuOn = pauseMenuUI.activeSelf) || (GameOverOn = gameOver.activeSelf)
            || (controlsOn = controls.activeSelf))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (!inventoryUIUsed && !equipmentUIUsed && !dialogueBoxOn && !ChestUIUsed
        && (SkillTreeUI.transform.localScale == new Vector3(0, 0, 0)) && !pauseMenuOn && !GameOverOn && !controlsOn)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
        currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);

        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * dstFromTarget;
        

    }

    void FixedUpdate() {
        CollisionCheck();
    }

    // Checks for collision, shifts transform of camera
    void CollisionCheck()
    {
        RaycastHit hit;
        //if (Physics.SphereCast(target.position, distFromWall, transform.position - target.position, out hit, dstFromTarget, collisionMask))
        if (Physics.Linecast(target.position, transform.position, out hit, collisionMask))
        {
            //Debug.Log("cam spherecast hit.name " + hit.transform.name);
            Vector3 norm = hit.normal * distFromWall;
            Vector3 p = hit.point + norm;
            cameraController.ShiftTransform(p);
        }
        else
        {
            /*
            //Debug.Log("cam no spherecast hit");
            if (Physics.Linecast(target.position, transform.position, out hit, collisionMask)) {
                //Debug.Log("cam line cast hit.name " + hit.transform.name);
                Vector3 norm = hit.normal * distFromWall;
                Vector3 p = hit.point + norm;
                cameraController.ShiftTransform(p);
            } else {
                */
                //Debug.Log("cam no hit");
            Vector3 direction = (target.position - transform.position).normalized;
            cameraController.ShiftTransform(transform.position + direction * distFromWall);
            //}

        }
        if (changeTransparency)
            cameraController.TransparencyCheck();
    }

    

}