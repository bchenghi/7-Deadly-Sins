using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [HideInInspector]

    // true if player is in turret
    public bool inPosition = false;

    [SerializeField]
    GameObject counterTextObject;
    TextMeshProUGUI counterText;
    bool reloadCalled = false;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
        turretShooting.onAmmoChange += UpdateCounterText;
        counterText = counterTextObject.GetComponent<TextMeshProUGUI>();
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
            counterText.gameObject.SetActive(true);
            reloadCalled = false;
            turretShooting.canShoot = true;
        } 
        else if (playerDetection.PlayerShooting() && turretShooting.OutOfAmmo && inPosition
         && !reloadCalled) 
        {
            player.GetComponent<PlayerController>().ChangeActionsAllowed(true);
            turretGraphics.SetActive(true);
            playerGraphics.SetActive(true);
            idleCrossHair.SetActive(false);
            playerDetection.SetPlayerShooting(false);

            if (turretReload.Reload()) {
                reloadCalled = true;
            }

            player.GetComponent<CharacterController>().enabled = false;
            player.position = oldPlayerPosition;
            player.GetComponent<CharacterController>().enabled = true;

            inPosition = false;
            counterText.gameObject.SetActive(false);
            turretShooting.canShoot = false;
        } 
        else if (playerDetection.PlayerShooting() && turretShooting.OutOfAmmo 
        && !inPosition && !reloadCalled) 
        {
            if (turretReload.Reload()) {
                reloadCalled = true;
            }
            
            playerDetection.SetPlayerShooting(false);
            turretShooting.canShoot = false;
        } 
        else if (!playerDetection.PlayerShooting() && inPosition) 
        {
            player.GetComponent<PlayerController>().ChangeActionsAllowed(true);
            turretGraphics.SetActive(true);
            playerGraphics.SetActive(true);
            idleCrossHair.SetActive(false);
            inPosition = false;
            counterText.gameObject.SetActive(false);

            player.GetComponent<CharacterController>().enabled = false;
            player.position = oldPlayerPosition;
            player.GetComponent<CharacterController>().enabled = true;
            reloadCalled = false;
            turretShooting.canShoot = false;
        }
    }

    void UpdateCounterText(int currentAmmo, int maxAmmo) {
        counterText.text = currentAmmo + "/" + maxAmmo;
    }

    void EnableTurretAndPlayerGraphics(bool boolean) {

    }


}
