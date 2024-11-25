using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IndependentThoughtProcessor
{
    private List<float[]> memoryEmbeddings = new List<float[]>();

    public void ExpandClusters(float[][] baseClusters, int numNewClusters)
    {
        // Generate random perturbations to existing centroids
        List<float[]> newCentroids = new List<float[]>();
        foreach (var cluster in baseClusters)
        {
            for (int i = 0; i < numNewClusters; i++)
            {
                var newCentroid = new float[cluster.Length];
                for (int j = 0; j < cluster.Length; j++)
                {
                    newCentroid[j] = cluster[j] + Random.Range(-0.1f, 0.1f);
                }
                newCentroids.Add(newCentroid);
            }
        }

        // Re-cluster with expanded centroids
        memoryEmbeddings.AddRange(newCentroids);

        // Call the static method KMeans.Cluster
        var newClusters = KMeans.Cluster(memoryEmbeddings.ToArray(), numNewClusters);
        Debug.Log($"Expanded into {newClusters.Count} clusters.");
    }
}
