using System.Collections.Generic;
using UnityEngine;

public class MultiDatabaseClusterer
{
    public Dictionary<int, List<float[]>> ClusterVectors(
        MultiDatabaseManager dbManager, int numClusters, int maxIterations = 100)
    {
        List<float[]> allVectors = new List<float[]>();

        // Gather vectors from all databases
        foreach (var db in dbManager.GetAllDatabases())
        {
            allVectors.AddRange(db.GetAllVectors().Values);
        }

        // Use KMeans to cluster the vectors
        List<List<float[]>> clusters = KMeans.Cluster(allVectors.ToArray(), numClusters, maxIterations);

        // Convert List<List<float[]>> to Dictionary<int, List<float[]>>
        Dictionary<int, List<float[]>> clusteredData = new Dictionary<int, List<float[]>>();
        for (int i = 0; i < clusters.Count; i++)
        {
            clusteredData[i] = clusters[i];
        }

        return clusteredData;
    }
}
