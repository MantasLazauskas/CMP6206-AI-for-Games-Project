using UnityEngine;

public class AgentSensors : MonoBehaviour
{
    [Header("Vision Settings")]
    public float viewRadius = 15f;
    [Range(0, 360)] public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Hearing Settings")]
    public float hearingRadius = 20f;

    [HideInInspector] public Transform detectedTarget;

    void Update()
    {
        FieldOfViewCheck();
    }

    void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // Check if line of sight is blocked by an obstacle
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    detectedTarget = target;
                    Debug.Log("Enemy Spotted!");
                }
                else
                {
                    detectedTarget = null;
                }
            }

            else
            {
                detectedTarget = null;
            }
        }

        else
        {
            detectedTarget = null;
        }
    }

    // Call this method when a loud sound occurs nearby (e.g., gunfire)
    public void ListenForSound(Vector3 soundPosition, float soundVolume)
    {
        if (Vector3.Distance(transform.position, soundPosition) <= hearingRadius * soundVolume)
        {
            Debug.Log("Sound heard at position: " + soundPosition);
            // Trigger investigate state here
        }
    }
}