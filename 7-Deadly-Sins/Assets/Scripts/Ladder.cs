﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    bool Climbing;
    public float speed;
    Transform target;
    float intitalGravity;
    float initialStepOffset;

    private void Start()
    {
        intitalGravity = PlayerManager.instance.player.GetComponent<PlayerController>().gravity;
        initialStepOffset = PlayerManager.instance.player.GetComponent<CharacterController>().stepOffset;
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp(KeyCode.W) && !Climbing)
        {
            target = other.transform;
            Climbing = true;
        }

        if (Climbing)
        {
            target.GetComponent<PlayerController>().Climbing = true;
            target.GetComponent<PlayerController>().gravity = 0;
            target.GetComponent<CharacterController>().stepOffset = 0;
            target.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            target.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
            //Vector3 direction = (transform.position - target.position).normalized;
            //target.rotation = Quaternion.LookRotation(target.forward);
            if (Input.GetKey(KeyCode.W))
            {
                target.GetComponent<PlayerController>().ClimbingButStop = false;
                target.Translate(Vector3.up * Time.deltaTime * speed);
            } else if (Input.GetKey(KeyCode.S))
            {
                target.GetComponent<PlayerController>().ClimbingButStop = false;
                target.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
            } else
            {
                target.GetComponent<PlayerController>().ClimbingButStop = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (target && other.GetComponent<PlayerController>())
        {
            target.GetComponent<PlayerController>().Climbing = false;
            target.GetComponent<PlayerController>().gravity = intitalGravity;
            target.GetComponent<CharacterController>().stepOffset = initialStepOffset;
            target.Translate(Vector3.forward * Time.deltaTime * 20);
            Climbing = false;
            target = null;
        }
    }
}
