using UnityEngine;

public class AgentSensor : MonoBehaviour
{
    [Header("Vision Settings")]
    [SerializeField] private float viewDistance = 15f;
    [SerializeField] private float viewAngle = 90f; // Total field of view angle
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask obstacleLayer;

    public Transform VisibleTarget { get; private set; }

    public bool DetectTarget()
    {
        VisibleTarget = null;
        Collider[] hits = Physics.OverlapSphere(transform.position, viewDistance, enemyLayer);

        foreach (Collider col in hits)
        {
            Vector3 dirToTarget = (col.transform.position - transform.position).normalized;

            // 1. Check if target is inside the vision cone angle
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, col.transform.position);

                // 2. Raycast to verify Line of Sight (ensures walls/cover block vision)
                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleLayer))
                {
                    VisibleTarget = col.transform;
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize view cone in the Unity Editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 leftRay = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightRay = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftRay * viewDistance);
        Gizmos.DrawRay(transform.position, rightRay * viewDistance);
    }
}