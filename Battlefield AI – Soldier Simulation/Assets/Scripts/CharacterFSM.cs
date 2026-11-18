using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AgentSensors))]
public class CharacterFSM : MonoBehaviour
{
    public enum FSMState
    {
        Patrol,
        Chase,
        Attack
    }

    [Header("FSM Settings")]
    public FSMState currentState;
    public float attackRange = 2.5f; // Distance at which the soldier stops and attacks

    [Header("Combat Settings")]
    public GameObject bulletPrefab; // Assign your bullet prefab here
    public Transform firePoint;     // Assign your FirePoint here
    public float fireRate = 1.0f;   // Time in seconds between shots
    private float nextFireTime = 0f;

    [Header("Optional Patrol Waypoints")]
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private NavMeshAgent navMeshAgent;
    private AgentSensors sensors;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        sensors = GetComponent<AgentSensors>();
        currentState = FSMState.Patrol;
    }

    void Update()
    {
        // 1. Determine Current State based on Sensors and Distance
        if (sensors.DetectedTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, sensors.DetectedTarget.position);

            if (distanceToTarget <= attackRange)
            {
                currentState = FSMState.Attack;
            }
            else
            {
                currentState = FSMState.Chase;
            }
        }
        else
        {
            currentState = FSMState.Patrol;
        }

        // 2. Execute Behavior for the Active State
        switch (currentState)
        {
            case FSMState.Patrol:
                HandlePatrolState();
                break;

            case FSMState.Chase:
                HandleChaseState();
                break;

            case FSMState.Attack:
                HandleAttackState();
                break;
        }
    }

    void HandlePatrolState()
    {
        navMeshAgent.isStopped = false;

        // Ensure the array exists, has items, and the current waypoint slot isn't empty (null)
        if (waypoints != null && waypoints.Length > 0)
        {
            if (waypoints[currentWaypointIndex] != null)
            {
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);

                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
                {
                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                }
            }
            else
            {
                Debug.LogWarning("Waypoint at index " + currentWaypointIndex + " is unassigned!", this);
            }
        }
        else
        {
            // If no waypoints are set up yet, just stand idle
            navMeshAgent.isStopped = true;
        }
    }

    void HandleChaseState()
    {
        navMeshAgent.isStopped = false;

        if (sensors.DetectedTarget != null)
        {
            navMeshAgent.SetDestination(sensors.DetectedTarget.position);
        }
    }

    void HandleAttackState()
    {
        navMeshAgent.isStopped = true; // Stop moving so the soldier holds position

        if (sensors.DetectedTarget != null)
        {
            // Keep soldier upright while turning to face the target
            Vector3 lookTarget = new Vector3(
                sensors.DetectedTarget.position.x,
                transform.position.y,
                sensors.DetectedTarget.position.z
            );
            transform.LookAt(lookTarget);

            // Aim the firePoint directly at the target's center mass
            if (firePoint != null)
            {
                firePoint.LookAt(sensors.DetectedTarget.position + Vector3.up * 1.0f);
            }

            ShootAtTarget();
        }
    }

    void ShootAtTarget()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            else
            {
                Debug.LogWarning("Bullet Prefab or FirePoint is unassigned on CharacterFSM!", this);
            }
        }
    }
}