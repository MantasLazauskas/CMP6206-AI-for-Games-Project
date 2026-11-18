using UnityEngine;
using UnityEngine.AI;

public static class TacticalPathfinding
{
    /// <summary>
    /// Calculates a flanking position relative to the target's position.
    /// </summary>
    public static bool GetFlankingPosition(Vector3 agentPosition, Vector3 targetPosition, float flankDistance, out Vector3 resultPosition)
    {
        Vector3 directionToTarget = (targetPosition - agentPosition).normalized;

        // Perpendicular vector for flanking (left or right side)
        Vector3 flankDirection = Vector3.Cross(Vector3.up, directionToTarget);
        if (Random.value > 0.5f) flankDirection = -flankDirection;

        Vector3 candidatePosition = targetPosition + (flankDirection * flankDistance);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(candidatePosition, out hit, 3.0f, NavMesh.AllAreas))
        {
            resultPosition = hit.position;
            return true;
        }

        resultPosition = agentPosition;
        return false;
    }

    /// <summary>
    /// Calculates a safe tactical retreat position away from the threat.
    /// </summary>
    public static bool GetRetreatPosition(Vector3 agentPosition, Vector3 threatPosition, float retreatDistance, out Vector3 resultPosition)
    {
        Vector3 awayFromThreat = (agentPosition - threatPosition).normalized;
        Vector3 candidatePosition = agentPosition + (awayFromThreat * retreatDistance);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(candidatePosition, out hit, 5.0f, NavMesh.AllAreas))
        {
            resultPosition = hit.position;
            return true;
        }

        resultPosition = agentPosition;
        return false;
    }
}