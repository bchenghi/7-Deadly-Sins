using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public float LookRadius = 10f;
    public float RangeCombatRadius = 10f;
    public bool withinRange = false;

    Transform target;
    NavMeshAgent agent;
    BossCombat combat;
    Animator animator;
    Vector3 originalPos;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<BossCombat>();
        animator = GetComponentInChildren<Animator>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (!combat.dead)
        {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if (distance <= agent.stoppingDistance) //Close Combat 
            {
                
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                    FaceTarget();
                }
            }
            else if (distance <= LookRadius) //Chase Player
            {
                withinRange = false;
                agent.SetDestination(target.position);
                DecideOnChasing();
            }
            else if (distance > LookRadius) //Goes back to original Position
            {
                agent.SetDestination(originalPos);
            }
            else if (distance <= RangeCombatRadius && distance > LookRadius)
            {
                withinRange = true;
                FaceTarget();
                combat.RangedAttack(targetStats);
            }
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
        Gizmos.DrawWireSphere(transform.position, RangeCombatRadius);
    }

    // Stops or starts chasing player based on whether attack and reaction animation is playing
    public void DecideOnChasing()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punching") || animator.GetCurrentAnimatorStateInfo(0).IsName("Reaction"))
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }


}
