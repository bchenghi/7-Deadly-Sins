using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// changes camera location, and shoots
public class EditedTurretShooting : MonoBehaviour
{
    public bool OutOfAmmo;
    public int ammoCount;
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
    
   

    private void Start()
    {
        player = PlayerManager.instance.player;
        originalZoom = changeCamera.GetComponent<ParentCameraController>().dstFromTarget;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection.PlayerShooting() && ammoCount > 0)
        {
            changeCamera.GetComponent<ParentCameraController>().dstFromTarget = zoomin;
            if (Input.GetMouseButtonDown(0))
            {
                ammoCount -= 1;
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, TurretRange, TargetMask))
                {
                    float distance = Vector3.Distance(player.transform.position, hit.point);
                    if (distance <= TurretRange)
                    {
                        var Clone = GameObject.Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(Clone.gameObject, 2f);
                        if (hit.transform.GetComponent<EnemyStats>() || hit.transform.GetComponent<ClownStats>())
                        {
                            hit.transform.GetComponent<CharacterStats>().TakeDamage(damage);
                            crossHair.SetActive(true);
                            StartCoroutine(crossHairRoutine());
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
    }

    IEnumerator crossHairRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        crossHair.SetActive(false);
    }
          
}
