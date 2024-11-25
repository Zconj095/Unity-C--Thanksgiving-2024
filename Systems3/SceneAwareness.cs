using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SceneAwareness
{
    public List<GameObject> GetNearbyObjects(Vector3 position, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        List<GameObject> objects = new List<GameObject>();

        foreach (var hitCollider in hitColliders)
        {
            objects.Add(hitCollider.gameObject);
        }
        return objects;
    }

    public string DescribeSceneObjects(List<GameObject> objects)
    {
        string description = "Nearby objects: ";
        foreach (var obj in objects)
        {
            description += $"{obj.name}, ";
        }
        return description.TrimEnd(',', ' ');
    }
}
