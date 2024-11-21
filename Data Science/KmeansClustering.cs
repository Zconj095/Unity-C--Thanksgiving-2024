using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class KMeansClustering : MonoBehaviour
{
    [Header("K-Means Settings")]
    [SerializeField] private int k = 3;  // Number of clusters
    [SerializeField] private List<KmeansDataPoint> dataPoints; // Data points to cluster

    private List<KmeansDataPoint> centroids; // Centroids of the clusters

    private void Start()
    {
        // Example data points initialization (2D points for simplicity)
        InitializeExampleData();

        // Perform K-Means clustering
        centroids = PerformKMeansClustering(dataPoints, k);

        // Output the centroids to Unity's console
        Debug.Log("Final Centroids:");
        foreach (var centroid in centroids)
        {
            Debug.Log(centroid.ToString());
        }
    }

    /// <summary>
    /// Performs K-Means clustering.
    /// </summary>
    private List<KmeansDataPoint> PerformKMeansClustering(List<KmeansDataPoint> data, int k)
    {
        // Initialize centroids randomly
        List<KmeansDataPoint> centroids = InitializeCentroids(data, k);

        bool centroidsChanged = true;

        while (centroidsChanged)
        {
            centroidsChanged = false;

            // Step 1: Assign each data point to the nearest centroid
            foreach (var point in data)
            {
                point.AssignedCluster = GetClosestCentroid(point, centroids);
            }

            // Step 2: Recompute centroids as the mean of assigned points
            for (int i = 0; i < k; i++)
            {
                var clusterPoints = data.Where(p => p.AssignedCluster == i).ToList();
                if (clusterPoints.Count > 0)
                {
                    KmeansDataPoint newCentroid = CalculateCentroid(clusterPoints);
                    if (!newCentroid.Equals(centroids[i]))
                    {
                        centroidsChanged = true;
                    }
                    centroids[i] = newCentroid;
                }
            }
        }

        return centroids;
    }

    /// <summary>
    /// Initializes centroids randomly from the data points.
    /// </summary>
    private List<KmeansDataPoint> InitializeCentroids(List<KmeansDataPoint> data, int k)
    {
        List<KmeansDataPoint> centroids = new List<KmeansDataPoint>();
        System.Random rand = new System.Random();

        while (centroids.Count < k)
        {
            int index = rand.Next(data.Count);
            KmeansDataPoint candidate = data[index];
            if (!centroids.Contains(candidate))
            {
                centroids.Add(candidate);
            }
        }

        return centroids;
    }

    /// <summary>
    /// Gets the index of the closest centroid to a given point.
    /// </summary>
    private int GetClosestCentroid(KmeansDataPoint point, List<KmeansDataPoint> centroids)
    {
        float minDistance = float.MaxValue;
        int closestCentroid = 0;

        for (int i = 0; i < centroids.Count; i++)
        {
            float distance = CalculateEuclideanDistance(point, centroids[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCentroid = i;
            }
        }

        return closestCentroid;
    }

    /// <summary>
    /// Calculates the Euclidean distance between two points.
    /// </summary>
    private float CalculateEuclideanDistance(KmeansDataPoint point1, KmeansDataPoint point2)
    {
        float sum = 0;
        for (int i = 0; i < point1.Features.Length; i++)
        {
            sum += Mathf.Pow(point1.Features[i] - point2.Features[i], 2);
        }

        return Mathf.Sqrt(sum);
    }

    /// <summary>
    /// Calculates the centroid of a cluster of points.
    /// </summary>
    private KmeansDataPoint CalculateCentroid(List<KmeansDataPoint> points)
    {
        int numFeatures = points[0].Features.Length;
        float[] centroidFeatures = new float[numFeatures];

        foreach (var point in points)
        {
            for (int i = 0; i < numFeatures; i++)
            {
                centroidFeatures[i] += point.Features[i];
            }
        }

        for (int i = 0; i < numFeatures; i++)
        {
            centroidFeatures[i] /= points.Count;
        }

        return new KmeansDataPoint(centroidFeatures);
    }

    /// <summary>
    /// Initializes example 2D data points for clustering.
    /// </summary>
    private void InitializeExampleData()
    {
        dataPoints = new List<KmeansDataPoint>
        {
            new KmeansDataPoint(new float[] { 1.0f, 2.0f }),
            new KmeansDataPoint(new float[] { 1.5f, 1.8f }),
            new KmeansDataPoint(new float[] { 5.0f, 8.0f }),
            new KmeansDataPoint(new float[] { 8.0f, 8.0f }),
            new KmeansDataPoint(new float[] { 1.0f, 0.6f }),
            new KmeansDataPoint(new float[] { 9.0f, 11.0f }),
            new KmeansDataPoint(new float[] { 8.0f, 4.0f }),
            new KmeansDataPoint(new float[] { 6.0f, 4.0f }),
            new KmeansDataPoint(new float[] { 3.0f, 7.0f }),
            new KmeansDataPoint(new float[] { 3.5f, 6.5f })
        };
    }
}


