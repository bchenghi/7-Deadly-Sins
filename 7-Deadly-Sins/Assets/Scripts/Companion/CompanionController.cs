using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    
    public float lookRadius = 10f;
    private float StoppingDist;
    public Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    Animator animator;
    bool hasEnemyTarget;
    Transform player;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        animator = GetComponentInChildren<Animator>();
        StoppingDist = agent.stoppingDistance;
        hasEnemyTarget = false;
        player = PlayerManager.instance.player.transform;
        target = null;
    }

    private void Update()
    {
        CheckForEnemy();

        

        if (!combat.dead)
        {
            if (hasEnemyTarget)
            {
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance <= agent.stoppingDistance)
                {
                    agent.stoppingDistance = StoppingDist;
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if (targetStats != null)
                    {
                        combat.Attack(targetStats);
                        FaceTarget();
                        if (targetStats.currentHealth <= 0)
                        {
                            target = null;
                        }
                    }
                }
                else if (distance <= lookRadius)
                {
                    agent.stoppingDistance = StoppingDist;
                    agent.SetDestination(target.position);
                    DecideOnChasing();
                   
                }
                
            } else
            {

               
                //Follow Player
                agent.SetDestination(player.position);
                
            }
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
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

    private bool CheckForEnemy()
    {
        if (target != null)
        {
            hasEnemyTarget = true;
        } else
        {
            hasEnemyTarget = false;
        }
        return hasEnemyTarget;
    }
}

