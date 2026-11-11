using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterFSM : MonoBehaviour
{
    [Header("FSM Tuning")]
    [SerializeField] private int startingAmmo = 30;
    [SerializeField] private float secondsPerShot = 0.2f;
    [SerializeField] private float reloadSeconds = 2.5f;

    [Header("Patrol Settings")]
    [SerializeField] public Transform[] waypoints; // Public so states can access it

    [Header("Threat Detection")]
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private LayerMask enemyLayer;

    // Public property so inner states can access the NavMeshAgent
    public NavMeshAgent agent { get; private set; }

    private ICharacterState currentState;
    private ICharacterState pendingState;
    private int ammo;

    public int Ammo => ammo;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        ammo = startingAmmo;
        if (waypoints != null && waypoints.Length > 0)
            ChangeState(new PatrolState(this, waypoints));
        else
            ChangeState(new IdleState(this));
    }

    private void Update()
    {
        ApplyPendingState();

        if (currentState != null)
            currentState.Tick(Time.deltaTime);

        ApplyPendingState();
    }

    public void ChangeState(ICharacterState next)
    {
        pendingState = next;
    }

    private void ApplyPendingState()
    {
        if (pendingState == null) return;

        currentState?.Exit();
        currentState = pendingState;
        pendingState = null;
        currentState.Enter();
    }

    public bool IsThreatDetected()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        return hits.Length > 0;
    }

    public void Fire()
    {
        if (ammo <= 0) return;
        ammo--;
        Debug.Log($"[ATTACK] Soldier fires weapon! Ammo remaining: {ammo}");
    }

    public void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void StopMoving()
    {
        if (agent.isOnNavMesh)
            agent.ResetPath();
    }

    public void RefillAmmo()
    {
        ammo = startingAmmo;
        Debug.Log("[RELOAD] Magazine refilled.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // ==========================================
    // FSM INTERFACE & STATES
    // ==========================================

    public interface ICharacterState
    {
        void Enter();
        void Tick(float deltaTime);
        void Exit();
    }

    private abstract class CharacterState : ICharacterState
    {
        protected readonly CharacterFSM fsm;
        protected float elapsed;

        protected CharacterState(CharacterFSM owner) { fsm = owner; }
        public virtual void Enter() { elapsed = 0f; }
        public abstract void Tick(float deltaTime);
        public virtual void Exit() { }
    }

    private sealed class PatrolState : CharacterState
    {
        private readonly Transform[] waypoints;
        private int currentWaypointIndex = 0;

        public PatrolState(CharacterFSM owner, Transform[] waypoints) : base(owner)
        {
            this.waypoints = waypoints;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("[PATROL] Moving to next waypoint.");
            if (waypoints != null && waypoints.Length > 0)
            {
                fsm.MoveTo(waypoints[currentWaypointIndex].position);
            }
        }

        public override void Tick(float dt)
        {
            if (fsm.IsThreatDetected())
            {
                fsm.ChangeState(new AttackState(fsm));
                return;
            }

            if (waypoints != null && waypoints.Length > 0 && !fsm.agent.pathPending && fsm.agent.remainingDistance <= fsm.agent.stoppingDistance)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                fsm.MoveTo(waypoints[currentWaypointIndex].position);
                Debug.Log($"[PATROL] Reached waypoint. Moving to index {currentWaypointIndex}");
            }
        }
    }

    private sealed class IdleState : CharacterState
    {
        public IdleState(CharacterFSM owner) : base(owner) { }
        public override void Enter()
        {
            base.Enter();
            fsm.StopMoving();
            Debug.Log("[IDLE] Soldier standing ground...");
        }
        public override void Tick(float dt)
        {
            if (fsm.IsThreatDetected())
            {
                fsm.ChangeState(new AttackState(fsm));
            }
        }
    }

    private sealed class AttackState : CharacterState
    {
        private float shotTimer;
        public AttackState(CharacterFSM owner) : base(owner) { }

        public override void Enter()
        {
            base.Enter();
            shotTimer = 0f;
            fsm.StopMoving();
            Debug.Log($"[ATTACK] Engaging target. Current ammo: {fsm.ammo}");
        }

        public override void Tick(float dt)
        {
            shotTimer += dt;
            if (shotTimer >= fsm.secondsPerShot)
            {
                shotTimer = 0f;
                fsm.Fire();

                if (fsm.ammo <= 0)
                {
                    fsm.ChangeState(new ReloadState(fsm));
                }
            }
        }
    }

    private sealed class ReloadState : CharacterState
    {
        public ReloadState(CharacterFSM owner) : base(owner) { }

        public override void Enter()
        {
            base.Enter();
            fsm.StopMoving();
            Debug.Log("[RELOAD] Reloading weapon...");
        }

        public override void Tick(float dt)
        {
            elapsed += dt;
            if (elapsed >= fsm.reloadSeconds)
            {
                fsm.RefillAmmo();

                // Fixed: Accessing waypoints through fsm.waypoints
                if (fsm.waypoints != null && fsm.waypoints.Length > 0)
                    fsm.ChangeState(new PatrolState(fsm, fsm.waypoints));
                else
                    fsm.ChangeState(new IdleState(fsm));
            }
        }
    }
}