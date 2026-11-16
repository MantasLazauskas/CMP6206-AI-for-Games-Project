using UnityEngine;
using UnityEngine.AI;

public static class TacticalPathfinding
{
    // 1. Calculate a tactical flanking position around a target
    public static Vector3 GetFlankPosition(Vector3 agentPos, Vector3 targetPos, float flankDistance = 6f)
    {
        Vector3 dirToTarget = (targetPos - agentPos).normalized;
        Vector3 flankDir = Vector3.Cross(dirToTarget, Vector3.up); // Perpendicular direction
        if (Random.value < 0.5f) flankDir = -flankDir; // Randomize left/right flank

        Vector3 rawFlankPos = targetPos + (flankDir * flankDistance);

        // Snap to nearest valid point on the NavMesh
        if (NavMesh.SamplePosition(rawFlankPos, out NavMeshHit hit, 4f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return targetPos;
    }

    // 2. Calculate a tactical retreat position away from a threat
    public static Vector3 GetRetreatPosition(Vector3 agentPos, Vector3 threatPos, float retreatDistance = 8f)
    {
        Vector3 awayFromThreat = (agentPos - threatPos).normalized;
        Vector3 rawRetreatPos = agentPos + (awayFromThreat * retreatDistance);

        // Snap to nearest valid point on the NavMesh
        if (NavMesh.SamplePosition(rawRetreatPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return agentPos;
    }
}