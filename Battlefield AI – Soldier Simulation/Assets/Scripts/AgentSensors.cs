using UnityEngine;

public class AgentSensors : MonoBehaviour
{
    [Header("Vision Settings")]
    public float viewDistance = 6f;
    [Range(0, 360)] public float viewAngle = 90f;
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public Transform DetectedTarget { get; private set; }

    void Update()
    {
        ScanForEnemies();
    }

    void ScanForEnemies()
    {
        DetectedTarget = null;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewDistance, enemyLayer);

        foreach (Collider target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector3 dirToTarget = (targetTransform.position - transform.position).normalized;

            // Check if target is within the field of view angle
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2f)
            {
                float dstToTarget = Vector3.Distance(transform.position, targetTransform.position);

                // Raycast to ensure no walls/obstacles are blocking line of sight
                if (!Physics.Raycast(transform.position + Vector3.up, dirToTarget, dstToTarget, obstacleLayer))
                {
                    DetectedTarget = targetTransform; // Target acquired and visible!
                    break;
                }
            }
        }
    }

    // Visualize FOV cone in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftBoundary * viewDistance);
        Gizmos.DrawRay(transform.position, rightBoundary * viewDistance);
    }
}