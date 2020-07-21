using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField]
    GameObject idleHandEffects;
    float timePassedSinceTeleport = 0;
    EffectHandler effectHandler;
    SoundHandler soundHandler;
    Animator animator;
    Transform target;

    // teleporting is true if teleport coroutine has started, false if coroutine ended
    [HideInInspector]
    public bool teleporting = false;

    bool firstFrame = true;

    ClownCombat clownCombat;
    ClownAnimator clownAnimator;
    ClownStats clownStats;

    Coroutine currentTeleportCoroutine;

    // Tracks previous health to calculate percentage of max health clown lost. 
    // Used for deciding to teleport
    int previousHealth;


    // A condition to teleport is if clown loses a percentage of his max health.
    float percentageOfHealthLostTeleport = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        effectHandler = GetComponent<EffectHandler>();
        animator = GetComponentInChildren<Animator>();
        soundHandler = GetComponent<SoundHandler>();
        clownCombat = GetComponent<ClownCombat>();
        clownStats = GetComponent<ClownStats>();

        if (regionsAvailableForTeleport.Diameter() 
        < distanceAwayFromTargetToTeleport) {
            Debug.LogWarning("distance to teleport away from target is too large");
        }

        previousHealth = clownStats.currentHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame) {
            effectHandler.SmokeEffectEvent(transform, 5, 2f);
            firstFrame = false;
        }

        if (clownCombat.dead && idleHandEffects.activeSelf)
        {
            idleHandEffects.SetActive(false);
        }

        if (ActionsAllowed()) {
            timePassedSinceTeleport += Time.deltaTime;
            if (CanTeleport()) 
             {
                Teleport();
                previousHealth = clownStats.currentHealth;
                timePassedSinceTeleport = 0;
            }
            FaceTarget();
            //Debug.Log("CheckIfWillHitTarget()" + CheckIfWillHitTarget());
            if (!clownCombat.dead && !teleporting && CheckIfWillHitTarget()) {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    clownCombat.Attack(targetStats);
                }
            }
        }

        if (clownCombat.dead && currentTeleportCoroutine != null) {
            StopCoroutine(currentTeleportCoroutine);
        }
        
    }

    void Teleport() {
        bool teleported = false;
        int tries = 0;
        int maxTries = 5;
        while (!teleported && tries < maxTries) {
            Vector3 randomPosition = regionsAvailableForTeleport.RandomPosition();
            if (Vector3.Distance(target.position, randomPosition) >= distanceAwayFromTargetToTeleport) {
                currentTeleportCoroutine = StartCoroutine(TeleportCoroutine(randomPosition));
                teleported = true;
            } 
            else 
            {  
                tries++;
            }
        }
        
    }


    IEnumerator TeleportCoroutine(Vector3 teleportPosition) {
        teleporting = true;

        idleHandEffects.SetActive(false);

        animator.SetTrigger("teleport");

        string[] teleportSounds = new string[] {"Whoosh", "Whoosh1"};
        soundHandler.PlaySoundRandomly(teleportSounds, transform);

        yield return new WaitForSeconds(1.3f);

        effectHandler.SmokeEffectEvent(transform, 6, 0.5f);
        GetComponent<NavMeshAgent>().Warp(teleportPosition);
        effectHandler.SmokeEffectEvent(transform, 6, 0.5f);
        teleporting = false;


        idleHandEffects.SetActive(true);
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Checks for obstacles between firing point and player. Returns true if no obstacles, false if there is
    bool CheckIfWillHitTarget() {
        ProjectileHandler projectileHandler = GetComponent<ProjectileHandler>();
        Ray ray = new Ray(projectileHandler.projectileFirePoint.position, (target.position - transform.position));
        RaycastHit hit;
        int ignoreRaycastLayerMask = LayerMask.GetMask("Ignore Raycast");
        //int playerLayerMask = LayerMask.GetMask("Player");
        if (Physics.Raycast(ray,out hit, 100, ~ignoreRaycastLayerMask)) {
            Debug.Log("Clown check if will hit target hit: " + hit.transform.name);
            if (hit.transform.name == "Player") {
                return true;
            }
        } 
        return false;
    }

    bool ActionsAllowed() {
        return !clownCombat.dead;
    }

    // Current conditions to teleport are (if teleport cooldown is over, or if distance to player is 
    // close enough, or a percentage of max health was lost), and is not already teleporting
    bool CanTeleport() {
        //Debug.Log("health lost: " + ((float) (previousHealth - clownStats.currentHealth)) / clownStats.maxHealth);
        return (timePassedSinceTeleport >= teleportCooldown || 
            Vector3.Distance(target.position, transform.position) < distanceFromTargetStartTeleport || 
            ((float)(previousHealth - clownStats.currentHealth)) / clownStats.maxHealth > percentageOfHealthLostTeleport)
             && !teleporting;
    }


}
