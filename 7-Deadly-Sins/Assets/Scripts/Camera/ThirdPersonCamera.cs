﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//test

public class ThirdPersonCamera : MonoBehaviour
{

    public bool lockCursor;
    public float mouseSensitivity = 5;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 8f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    //Collisions
    public bool changeTransparency = true;
    public MeshRenderer targetRenderer;

    //Collision Speeds
    public float moveSpeed = 5;
    public float returnSpeed = 9;
    public float wallPush = 0.7f;

    //Distances
    public float closestDistanceToPlayer = 2;
    public float evenCloserDistanceToPlayer = 1;

    //Mask
    public LayerMask collisionMask;

    private bool pitchLock = false;






    private void Start()
    {

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }



    }
    // Update is called once per frame
    void LateUpdate()
    {
        CollisionCheck(target.position - transform.forward * dstFromTarget);
        WallCheck();


        if (!pitchLock)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);
        }
        else
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch = pitchMinMax.y;
            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);
        }




        transform.eulerAngles = currentRotation;





    }

    private void WallCheck()
    {
        Ray ray = new Ray(target.position, -target.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.2f, out hit, 0.7f, collisionMask))
        {
            pitchLock = true;
        }
        else
        {
            pitchLock = false;
        }
    }

    private void CollisionCheck(Vector3 retPoint)
    {
        RaycastHit hit;

        if (Physics.Linecast(target.position, retPoint, out hit, collisionMask))
        {
            Vector3 norm = hit.normal * wallPush;
            Vector3 p = hit.point + norm;

            TransparencyCheck();

            if (Vector3.Distance(Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime), target.position) <= evenCloserDistanceToPlayer)
            {

            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime);
            }

            return;
        }

        FullTransparency();

        transform.position = Vector3.Lerp(transform.position, retPoint, returnSpeed * Time.deltaTime);
        pitchLock = false;
    }

    private void TransparencyCheck()
    {
        if (changeTransparency)
        {
            if (Vector3.Distance(transform.position, target.position) <= closestDistanceToPlayer)
            {
                foreach (Material element in targetRenderer.sharedMaterials)
                {
                    Color temp = element.color;
                    temp.a = Mathf.Lerp(temp.a, 0.2f, moveSpeed * Time.deltaTime);

                    targetRenderer.sharedMaterial.color = temp;

                }
            }
            else
            {
                foreach (Material element in targetRenderer.sharedMaterials)
                {
                    if (element.color.a <= 0.99f)
                    {
                        Color temp = element.color;
                        temp.a = Mathf.Lerp(temp.a, 1, moveSpeed * Time.deltaTime);

                        targetRenderer.sharedMaterial.color = temp;
                    }
                }
            }
        }
    }

    private void FullTransparency()
    {
        if (changeTransparency)
        {
            foreach (Material element in targetRenderer.sharedMaterials)
            {
                if (element.color.a <= 0.99f)
                {
                    Color temp = element.color;
                    temp.a = Mathf.Lerp(temp.a, 1, moveSpeed * Time.deltaTime);

                    targetRenderer.sharedMaterial.color = temp;
                }
            }
        }
    }







}