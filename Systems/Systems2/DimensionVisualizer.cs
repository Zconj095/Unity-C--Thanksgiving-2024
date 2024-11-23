using UnityEngine;
using System.Collections.Generic;

public class DimensionVisualizer : MonoBehaviour
{
    public HyperCogniCortex Cortex;

    void OnDrawGizmos()
    {
        if (Cortex == null || Cortex.Dimensions == null) return;

        float y = 0;
        foreach (var dimension in Cortex.Dimensions)
        {
            Gizmos.color = Color.Lerp(Color.red, Color.green, dimension.Value / dimension.MaxValue);
            Gizmos.DrawCube(new Vector3(0, y, 0), new Vector3(dimension.Value, 0.5f, 0.5f));
            y += 1;
        }
    }
}
