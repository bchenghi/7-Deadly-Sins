using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// changes camera location, and shoots
public class EditedTurretShooting : MonoBehaviour
{
    public bool OutOfAmmo;
    public int ammoCount;
    public int maxAmmo;
    public GameObject changeCamera;
    public Transform effect;
    public float TurretRange;
    public int damage;
    public float zoomin = 0;
    GameObject player;
    public LayerMask TargetMask;
    private float originalZoom;
    public GameObject crossHair;
    [SerializeField]
    EditedTurretPlayerDetection playerDetection;

    [HideInInspector]
    public bool canShoot = false;
    SoundHandler soundHandler;

    

    public delegate void OnAmmoChange(int currentAmmo, int maxAmmo);
    public OnAmmoChange onAmmoChange;
    bool firstFrame = true;
   

    private void Start()
    {
        player = PlayerManager.instance.player;
        originalZoom = changeCamera.GetComponent<ParentCameraController>().dstFromTarget;
        soundHandler = GetComponent<SoundHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame) {
            if (onAmmoChange != null) {
                onAmmoChange(ammoCount, maxAmmo);
            }
            firstFrame = false;
        }

        if (canShoot && ammoCount > 0 )
        {
            changeCamera.GetComponent<ParentCameraController>().dstFromTarget = zoomin;
            if (MouseIsLocked() && Input.GetMouseButtonDown(0))
            {
                ammoCount -= 1;
                if (onAmmoChange != null) {
                    onAmmoChange(ammoCount, maxAmmo);
                }
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, TurretRange, TargetMask))
                {
                    soundHandler.PlaySoundByName(transform, "GunShot");
                    float distance = Vector3.Distance(player.transform.position, hit.point);
                    if (distance <= TurretRange)
                    {
                        if (hit.transform.GetComponent<EnemyStats>() || hit.transform.GetComponent<ClownStats>())
                        {
                            hit.transform.GetComponent<CharacterStats>().TakeDamage(damage);
                            crossHair.SetActive(true);
                            StartCoroutine(crossHairRoutine());
                        } else {
                            var Clone = GameObject.Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));
                            Destroy(Clone.gameObject, 2f);
                        }
                    }
                }
            }
        } else
        {
            changeCamera.GetComponent<ParentCameraController>().dstFromTarget = originalZoom;
            if (ammoCount <= 0)
            {
                OutOfAmmo = true;
            } else
            {
                OutOfAmmo = false;
            }
        }

        FixAmmoCount();
        
        
    }

    IEnumerator crossHairRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        crossHair.SetActive(false);
    }

    void FixAmmoCount() {
        if (ammoCount < 0) {
            ammoCount = 0;
        }

        if (ammoCount > maxAmmo) {
            ammoCount = maxAmmo;
        }
    }

    bool MouseIsLocked() {
        return Cursor.lockState == CursorLockMode.Locked && !Cursor.visible;
    }
          
}
