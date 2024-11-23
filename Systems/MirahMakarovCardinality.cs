using System.Collections.Generic;
using UnityEngine;

public class MirahMakarovCardinality : MonoBehaviour
{
    public int clusterCount = 3;
    public List<Vector3> dataPoints; // Example: 3D points
    private List<Vector3> centroids;
    private List<List<Vector3>> clusters;
    private float[] radii;

    void Start()
    {
        InitializeClusters();
        PerformClustering();
    }

    void InitializeClusters()
    {
        centroids = new List<Vector3>();
        clusters = new List<List<Vector3>>();
        radii = new float[clusterCount];

        for (int i = 0; i < clusterCount; i++)
        {
            centroids.Add(dataPoints[Random.Range(0, dataPoints.Count)]);
            clusters.Add(new List<Vector3>());
        }
    }

    void PerformClustering()
    {
        for (int iteration = 0; iteration < 10; iteration++) // Example: 10 iterations
        {
            AssignPointsToClusters();
            UpdateCentroidsAndRadii();
        }
    }

    void AssignPointsToClusters()
    {
        foreach (var cluster in clusters)
        {
            cluster.Clear();
        }

        foreach (var point in dataPoints)
        {
            int bestCluster = 0;
            float minDistance = float.MaxValue;

            for (int i = 0; i < clusterCount; i++)
            {
                float distance = Vector3.Distance(point, centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestCluster = i;
                }
            }

            clusters[bestCluster].Add(point);
        }
    }

    void UpdateCentroidsAndRadii()
    {
        for (int i = 0; i < clusterCount; i++)
        {
            if (clusters[i].Count == 0) continue;

            Vector3 newCentroid = Vector3.zero;
            foreach (var point in clusters[i])
            {
                newCentroid += point;
            }
            centroids[i] = newCentroid / clusters[i].Count;

            float sumDistances = 0f;
            foreach (var point in clusters[i])
            {
                sumDistances += Vector3.Distance(point, centroids[i]);
            }
            radii[i] = sumDistances / clusters[i].Count;
        }
    }
}
