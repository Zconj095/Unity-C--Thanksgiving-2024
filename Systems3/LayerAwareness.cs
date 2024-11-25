using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LayerAwareness
{
    public List<GameObject> GetObjectsByLayer(int layer, float radius, Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        List<GameObject> objects = new List<GameObject>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == layer)
            {
                objects.Add(hitCollider.gameObject);
            }
        }
        return objects;
    }

    public string DescribeLayerObjects(int layer, List<GameObject> objects)
    {
        string layerName = LayerMask.LayerToName(layer);
        string description = $"Objects in layer {layerName}: ";

        foreach (var obj in objects)
        {
            description += $"{obj.name}, ";
        }
        return description.TrimEnd(',', ' ');
    }
}
