using UnityEngine;
using System.Collections.Generic;

public class CognizantCortexReader : MonoBehaviour
{
    private CognizantCortexReaderKMeansClustering kmeans;
    private CognizantCortexReaderGroversAlgorithm grovers;
    private List<float[]> memoryStates;

    void Start()
    {
        int clusterCount = 3;
        int searchSpaceSize = 100;

        kmeans = new CognizantCortexReaderKMeansClustering(clusterCount);
        grovers = new CognizantCortexReaderGroversAlgorithm(searchSpaceSize);
        memoryStates = new List<float[]>();

        // Example data points
        AddMemoryState(new float[] { 0.1f, 0.2f, 0.3f });
        AddMemoryState(new float[] { 0.4f, 0.5f, 0.6f });
        AddMemoryState(new float[] { 0.9f, 0.8f, 0.7f });

        // Perform clustering
        kmeans.InitializeCentroids();
        List<int> assignments = kmeans.PerformClustering();
        Debug.Log($"Cluster Assignments: {string.Join(", ", assignments)}");

        // Mark interesting states for Grover's Algorithm
        grovers.MarkState(1);
        int bestState = grovers.PerformSearch();
        Debug.Log($"Best State Found by Grover's Algorithm: {bestState}");
    }

    public void AddMemoryState(float[] state)
    {
        memoryStates.Add(state);
        kmeans.AddDataPoint(state);
    }
}
