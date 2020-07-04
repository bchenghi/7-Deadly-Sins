using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController : MonoBehaviour
{
    [SerializeField]
    Regions regionsAvailableForTeleport;
    [SerializeField]
    float distanceAwayFromTargetToTeleport;
    
    [SerializeField]
    float distanceFromTargetStartTeleport;
    [SerializeField]
    
    float teleportCooldown;
    float timePassedSinceTeleport = 0;
    EffectHandler effectHandler;
    Animator animator;
    Transform target;
    CharacterCombat combat;
    bool teleporting = false;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        effectHandler = GetComponent<EffectHandler>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if (regionsAvailableForTeleport.Diameter() 
        < distanceAwayFromTargetToTeleport) {
            Debug.LogWarning("distance to teleport away from target is too large");
        }

        effectHandler.SmokeEffectEvent(transform, 5, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        timePassedSinceTeleport += Time.deltaTime;
        if (timePassedSinceTeleport >= teleportCooldown) {
            if (Vector3.Distance(target.position, transform.position) < distanceFromTargetStartTeleport
             && !teleporting) {
            Teleport();
            teleporting = true;
            timePassedSinceTeleport = 0;
            }
        }
        FaceTarget();

        if (!combat.dead) {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if (targetStats != null)
            {
                //combat.Attack(targetStats);
            }
        }
    }

    void Teleport() {
        bool teleported = false;
        int tries = 0;
        int maxTries = 5;
        while (!teleported && tries < maxTries) {
            Vector3 randomPosition = regionsAvailableForTeleport.RandomPosition();
            if (Vector3.Distance(target.position, randomPosition) >= distanceAwayFromTargetToTeleport) {
                StartCoroutine(TeleportCoroutine(randomPosition));
                teleported = true;
            } else {  
                tries++;
            }
        }
    }


    IEnumerator TeleportCoroutine(Vector3 teleportPosition) {
        animator.SetTrigger("teleport");    
        yield return new WaitForSeconds(1.3f);
        effectHandler.SmokeEffectEvent(transform, 6, 0.5f);
        this.gameObject.transform.position = teleportPosition;
        effectHandler.SmokeEffectEvent(transform, 6, 0.5f);
        teleporting = false;
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


}
