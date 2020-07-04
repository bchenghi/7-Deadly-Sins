using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 GrapplePoint;
    public LayerMask WhatIsGrappleable;
    public Transform gunTip;
    public Transform player;
    public float maxDistance;
    private SpringJoint joint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.gameObject.AddComponent<Rigidbody>();
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<Rigidbody>().mass = 5;
            StartGrapple();
        } else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
            player.GetComponent<CharacterController>().enabled = true;
            Destroy(player.gameObject.GetComponent<Rigidbody>());
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit,maxDistance, WhatIsGrappleable)) {
            GrapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = GrapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, GrapplePoint);
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;

        }

    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, GrapplePoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
