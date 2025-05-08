using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float attackRadius = 10f; 
    public LevelManager levelManager;
    bool CanAttack = true;
    float AttackCooldown = 2f;
    Animator anim;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        float distance = Vector3.Distance(transform.position, LevelManager.instance.player.position);
        if (distance < attackRadius)
        {
            agent.SetDestination(LevelManager.instance.player.position);
            if (distance <= agent.stoppingDistance)
            {
               if (CanAttack)
                {
                    StartCoroutine(cooldown());
                    //play attack animation
                    anim.SetTrigger("Attack");

                }
            }
        }
       
    }

   IEnumerator cooldown()
   {
        CanAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

}
//    public NavMeshAgent agent; // Reference to the NavMeshAgent component
//    public Transform player; // Reference to the player's transform
//    public LayerMask whatIsGround, whatIsPlayer; // Layers for ground and player detection

//    // Enemy states
//    public float sightRange, attackRange;
//    public bool playerInSightRange, playerInAttackRange;

//    // Patrol settings
//    public float walkPointRange=10; // Range within which the enemy spawns and patrols
//    private Vector3 walkPoint; // Randomly chosen walk point
//    private bool walkPointSet;

//    // Movement speed
//    public float slowSpeed = 2f; // Slow movement speed

//    // Attack settings
//    public float timeBetweenAttacks;
//    private bool alreadyAttacked;

//    // Health and interaction
//    private int hitCount = 0; // Tracks how many times the enemy has been hit
//    public float fallSpeed = 5f; // Speed at which the enemy falls to the ground
//    private bool isFalling = false; // Tracks if the enemy is falling

//    private void Awake()
//    {
//        player = GameObject.Find("Player").transform; // Find the player in the scene
//        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
//        agent.speed = slowSpeed; // Set the agent's speed to slow
//    }

//    private void Update()
//    {
//        if (isFalling)
//        {
//            // Make the enemy fall to the ground
//            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Time.deltaTime * fallSpeed);
//            if (Mathf.Abs(transform.position.y) < 0.1f) // Check if the enemy is close to the ground
//            {
//                Destroy(gameObject); // Destroy the enemy
//            }
//            return;
//        }

//        // Check if the player is in sight or attack range
//        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
//        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

//        if (!playerInSightRange && !playerInAttackRange)
//            Patrol();
//        else if (playerInSightRange && !playerInAttackRange)
//            ChasePlayer();
//        else if (playerInAttackRange && playerInSightRange)
//            AttackPlayer();
//    }

//    private void Patrol()
//    {
//        if (!walkPointSet) SearchWalkPoint();

//        if (walkPointSet)
//        {
//            // Move to the walk point
//            agent.SetDestination(walkPoint);

//            // Check if the enemy has reached the walk point
//            Vector3 distanceToWalkPoint = transform.position - walkPoint;
//            if (distanceToWalkPoint.magnitude < 1f)
//                walkPointSet = false;
//        }
//    }

//    private void SearchWalkPoint()
//    {
//        // Generate a random point within the walk range
//        float randomZ = Random.Range(-walkPointRange, walkPointRange);
//        float randomX = Random.Range(-walkPointRange, walkPointRange);
//        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

//        // Check if the point is on the ground
//        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
//            walkPointSet = true;
//    }

//    private void ChasePlayer()
//    {
//        // Follow the player
//        if (agent.isActiveAndEnabled)
//            agent.SetDestination(player.position);
//    }

//    private void AttackPlayer()
//    {
//        // Stop moving and face the player
//        agent.SetDestination(transform.position);
//        transform.LookAt(player);

//        if (!alreadyAttacked)
//        {
//            // Attack logic (e.g., shoot a projectile)
//            alreadyAttacked = true;
//            Invoke(nameof(ResetAttack), timeBetweenAttacks);
//        }
//    }

//    private void ResetAttack()
//    {
//        alreadyAttacked = false;
//    }

//    public void TakeHit()
//    {
//        hitCount++;
//        if (hitCount >= 2) // If hit twice, start falling
//        {
//            isFalling = true;
//            agent.enabled = false; // Disable NavMeshAgent to stop movement
//        }
//    }

//    private void OnDrawGizmosSelected()
//    {
//        // Visualize sight and attack ranges in the editor
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, sightRange);
//        Gizmos.color = Color.blue;
//        Gizmos.DrawWireSphere(transform.position, walkPointRange); // Visualize patrol range
//    }
//}

//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyAi : MonoBehaviour
//{
//    public NavMeshAgent agent; // Reference to the NavMeshAgent component
//    public Transform player; // Reference to the player's transform
//    public LayerMask whatIsGround, whatIsPlayer; // Fixed variable name for consistency
//    // Patrolling 
//    public Vector3 walkPoint;
//    bool walkPointSet;
//    public float walkPointRange;
//    // Attacking    
//    public float timeBetweenAttacks;
//    bool alreadyAttacked;
//    // States
//    public float sightRange, attackRange;
//    public bool playerInSightRange, playerInAttackRange;

//    public GameObject projectile;

//    public int damage = 10;
//    public float health;

//    private void Awake()
//    {
//        player = GameObject.Find("Player").transform;
//        agent = GetComponent<NavMeshAgent>();
//    }
//    private void Update()
//    {
//        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
//        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

//        Debug.Log($"Player in sight: {playerInSightRange}, Player in attack range: {playerInAttackRange}");

//        if (!playerInSightRange && !playerInAttackRange)
//            Patrol();
//        else if (playerInSightRange && !playerInAttackRange)
//            ChasePlayer();
//        else if (playerInAttackRange && playerInSightRange)
//            AttackPlayer();
//    }

//    private void Patrol()
//    {
//        if (!walkPointSet)
//            SearchWalkPoint();
//        if (walkPointSet)
//        {
//            // Set destination only if agent is active
//            if (agent.isActiveAndEnabled)
//                agent.SetDestination(walkPoint);
//        }

//        Vector3 distanceToWalkPoint = transform.position - walkPoint;
//        // Walkpoint reached
//        if (distanceToWalkPoint.magnitude < 1f)
//            walkPointSet = false;
//    }

//    private void SearchWalkPoint()
//    {
//        // Calculate random point in range
//        float randomZ = Random.Range(-walkPointRange, walkPointRange);
//        float randomX = Random.Range(-walkPointRange, walkPointRange);
//        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

//        // Check if the walk point is valid
//        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
//            walkPointSet = true;
//    }

//    private void ChasePlayer()
//    {
//        // Set destination only if agent is active
//        if (agent.isActiveAndEnabled)
//            agent.SetDestination(player.position);
//    }

//    private void AttackPlayer()
//    {
//        // Make sure enemy doesn't move
//        agent.SetDestination(transform.position);
//        transform.LookAt(player);

//        if (!alreadyAttacked)
//        {
//            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
//            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
//            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

//            alreadyAttacked = true;
//            Invoke(nameof(ResetAttack), timeBetweenAttacks);
//        }
//    }

//    private void ResetAttack()
//    {
//        alreadyAttacked = false;
//    }

//    public void TakeDamage(int damage)
//    {
//        health -= damage;
//        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
//    }

//    private void DestroyEnemy()
//    {
//        // Handle enemy death (e.g., play animation, destroy object)
//        Destroy(gameObject);
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, sightRange);
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//    }
//}



////public class EnemyPatrolAndFlee : MonoBehaviour
////{
////    public Transform player;              // Assign player in Inspector
////    public float fleeDistance = 5f;       // Distance to trigger fleeing
////    public float moveSpeed = 3f;          // Speed of enemy movement
////    public Transform[] patrolPoints;      // Assign patrol points in Inspector
////    public float patrolWaitTime = 2f;     // Time to wait at each patrol point

////    private int currentPatrolIndex = 0;
////    private float waitTimer = 0f;

////    void Update()
////    {
////        if (player == null || patrolPoints.Length == 0) return;

////        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

////        if (distanceToPlayer < fleeDistance)
////        {
////            FleeFromPlayer();
////        }
////        else
////        {
////            Patrol();
////        }
////    }

////    void FleeFromPlayer()
////    {
////        Vector3 fleeDirection = transform.position - player.position;
////        fleeDirection.y = 0f; // Prevent downward movement
////        fleeDirection.Normalize();

////        transform.position += fleeDirection * moveSpeed * Time.deltaTime;

////        // Optional: rotate to face away from player
////        if (fleeDirection != Vector3.zero)
////        {
////            Quaternion lookRotation = Quaternion.LookRotation(fleeDirection);
////            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
////        }
////    }

////    void Patrol()
////    {
////        Transform targetPoint = patrolPoints[currentPatrolIndex];
////        Vector3 direction = targetPoint.position - transform.position;
////        direction.y = 0f;

////        if (direction.magnitude < 0.2f)
////        {
////            waitTimer += Time.deltaTime;
////            if (waitTimer >= patrolWaitTime)
////            {
////                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
////                waitTimer = 0f;
////            }
////        }
////        else
////        {
////            direction.Normalize();
////            transform.position += direction * moveSpeed * Time.deltaTime;

////            // Rotate towards patrol point
////            if (direction != Vector3.zero)
////            {
////                Quaternion lookRotation = Quaternion.LookRotation(direction);
////                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
////            }
////        }
////    }
////}

////using UnityEngine;

////public class EnemyAutoDisappear : MonoBehaviour
////{
////    public Transform player;         // Assign the player's Transform in the Inspector
////    public float disappearDistance = 3f; // Distance at which the enemy disappears

////    void Update()
////    {
////        if (player == null) return;

////        float distance = Vector3.Distance(transform.position, player.position);

////        if (distance < disappearDistance)
////        {
////            // You can add effects here before destroying
////            Destroy(gameObject);
////        }
////    }


////}


////using UnityEngine;

////public class EnemyFleeFromPlayer : MonoBehaviour
////{
////    public Transform player;             // Assign in Inspector
////    public float fleeRange = 8f;         // Distance to start fleeing
////    public float fleeSpeed = 4f;         // Speed of fleeing

////    void Update()
////    {
////        if (player == null) return;

////        float distance = Vector3.Distance(transform.position, player.position);

////        if (distance <= fleeRange)
////        {
////            // Get direction away from the player
////            Vector3 fleeDirection = (transform.position - player.position).normalized;

////            // Move away
////            transform.position += fleeDirection * fleeSpeed * Time.deltaTime;

////            // Optionally: face the fleeing direction
////            if (fleeDirection != Vector3.zero)
////            {
////                Quaternion toRotation = Quaternion.LookRotation(fleeDirection, Vector3.up);
////                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
////            }
////        }
////    }
////}
