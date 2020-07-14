using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    Vector3 oldPlayerPosition;
    public GameObject idleCrossHair;
    [SerializeField]
    EditedTurretPlayerDetection playerDetection;
    [SerializeField]
    EditedTurretReload turretReload;
    [SerializeField]
    EditedTurretShooting turretShooting;
    [SerializeField]

    GameObject turretGraphics;
    [SerializeField]
    GameObject playerGraphics;

    Transform player;

    // true if player is detected, and has ammo. (Can shoot at enemies)
    bool inPosition = false;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection.PlayerShooting() && !turretShooting.OutOfAmmo && !inPosition) 
        {
            player.GetComponent<PlayerController>().ChangeActionsAllowed(false);
            turretGraphics.SetActive(false);
            playerGraphics.SetActive(false);
            oldPlayerPosition = player.position;
            player.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
            player.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
            idleCrossHair.SetActive(true);
            inPosition = true;
        } 
        else if (playerDetection.PlayerShooting() && turretShooting.OutOfAmmo && inPosition) 
        {
            player.GetComponent<PlayerController>().ChangeActionsAllowed(true);
            turretGraphics.SetActive(true);
            playerGraphics.SetActive(true);
            idleCrossHair.SetActive(false);
            playerDetection.SetPlayerShooting(false);

            turretReload.Reload();

            player.GetComponent<CharacterController>().enabled = false;
            player.position = oldPlayerPosition;
            player.GetComponent<CharacterController>().enabled = true;

            inPosition = false;
        } 
        else if (playerDetection.PlayerShooting() && turretShooting.OutOfAmmo && !inPosition) 
        {
            turretReload.Reload();
            playerDetection.SetPlayerShooting(false);
        }
    }


}
