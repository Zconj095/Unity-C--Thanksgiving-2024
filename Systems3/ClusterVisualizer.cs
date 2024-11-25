using System.Collections.Generic;
using UnityEngine;

public class ClusterVisualizer : MonoBehaviour
{
    public List<List<Vector3>> Clusters = new List<List<Vector3>>();

    private void OnDrawGizmos()
    {
        if (Clusters == null || Clusters.Count == 0) return;

        Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta };

        for (int i = 0; i < Clusters.Count; i++)
        {
            Gizmos.color = colors[i % colors.Length];
            foreach (var point in Clusters[i])
            {
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }

    public void UpdateClusters(List<List<float[]>> clusterData)
    {
        Clusters = new List<List<Vector3>>();

        foreach (var cluster in clusterData)
        {
            var points = new List<Vector3>();
            foreach (var vec in cluster)
            {
                points.Add(new Vector3(vec[0], vec[1], vec[2]));
            }
            Clusters.Add(points);
        }
    }
}
