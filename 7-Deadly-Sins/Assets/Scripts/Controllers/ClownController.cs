using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController : MonoBehaviour
{
    [SerializeField]
    Region regionsAvailableForTeleport;
    [SerializeField]
    float distanceAwayFromTargetToTeleport;
    EffectHandler effectHandler;
    Animator animator;
    Transform target;
    CharacterCombat combat;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        effectHandler = GetComponent<EffectHandler>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if (regionsAvailableForTeleport.Diameter() < distanceAwayFromTargetToTeleport) {
            Debug.LogWarning("distance to teleport away from target is too large");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Teleport() {
        bool teleported = false;
        int tries = 0;
        int maxTries = 5;
        while (!teleported && tries < maxTries) {
            Vector3 randomPosition = regionsAvailableForTeleport.RandomPosition();
            if (Vector3.Distance(target.position, randomPosition) >= distanceAwayFromTargetToTeleport) {
            this.gameObject.transform.position = randomPosition;
            teleported = true;
            } else {  
                tries++;
            }
        }
    }
}
