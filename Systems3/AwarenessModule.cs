using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AwarenessModule : MonoBehaviour
{
    public bool IsPlayerInSight(Transform player)
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        return angle < 45f; // 45-degree field of view
    }
}
