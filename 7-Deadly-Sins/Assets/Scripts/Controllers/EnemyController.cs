using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform moveSpot;
    public bool FreeRoamer;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    private float waitTime;
    public float startWaitTime;
    private float StoppingDist;
    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    Animator animator;
    Vector3 originalPos;
   

    // Start is called before the first frame update
     void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        animator = GetComponentInChildren<Animator>();
        originalPos = transform.position;
        
        waitTime = startWaitTime;
        moveSpot.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
        StoppingDist = agent.stoppingDistance;
    }

    // Update is called once per frame
     void Update()
    {
        
        float distance = Vector3.Distance(target.position, transform.position);

        if (!combat.dead)
        {
            if (distance <= agent.stoppingDistance)
            {
                agent.stoppingDistance = StoppingDist;
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                    FaceTarget();
                }
            }
            else if (distance <= lookRadius)
            {
                agent.stoppingDistance = StoppingDist;
                agent.SetDestination(target.position);
                DecideOnChasing();
            }
            else if (distance > lookRadius)
            {
                if (FreeRoamer)
                {
                    agent.stoppingDistance = 0;
                    FreeRoam();

                }
                else
                {
                    agent.stoppingDistance = StoppingDist;
                    agent.SetDestination(originalPos);
                }
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

    

    private void FreeRoam()
    {
        agent.SetDestination(moveSpot.position);
        //Debug.Log(Vector3.Distance(transform.position, moveSpot.position));
        if (Vector3.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
                waitTime = startWaitTime;
                
            } else
            {
                waitTime -= Time.deltaTime;
                
            }
        }
    }

    public bool Approximately(float a, float b, float epsilon)
    {
        return (Mathf.Abs(a - b) < epsilon) || (Mathf.Approximately(Mathf.Abs(a - b), epsilon));
    }

    
}
