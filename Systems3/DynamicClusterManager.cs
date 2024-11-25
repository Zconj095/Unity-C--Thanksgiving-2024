using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DynamicClusterManager
{
    private TemperatureControl tempControl = new TemperatureControl();
    private KMeans kMeans = new KMeans();
    private List<float[]> termEmbeddings = new List<float[]>();

    public void AddTerm(float[] embedding)
    {
        termEmbeddings.Add(embedding);

        // Re-cluster terms based on temperature-controlled granularity
        int numClusters = Mathf.RoundToInt(10 * tempControl.GetTemperature());
        var clusters = KMeans.Cluster(termEmbeddings.ToArray(), numClusters);

        Debug.Log($"Clustered into {clusters.Count} groups.");
    }
}
