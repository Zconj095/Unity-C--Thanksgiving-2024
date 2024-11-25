using System.Collections.Generic;
using UnityEngine;

public class CognizantCortexReaderKMeansClustering
{
    public int ClusterCount { get; private set; }
    private List<float[]> dataPoints;
    private List<float[]> centroids;

    public CognizantCortexReaderKMeansClustering(int clusterCount)
    {
        ClusterCount = clusterCount;
        dataPoints = new List<float[]>();
        centroids = new List<float[]>();
    }

    public void AddDataPoint(float[] point)
    {
        dataPoints.Add(point);
    }

    public void InitializeCentroids()
    {
        centroids.Clear();
        for (int i = 0; i < ClusterCount; i++)
        {
            centroids.Add(dataPoints[Random.Range(0, dataPoints.Count)]);
        }
    }

    public List<int> PerformClustering(int maxIterations = 10)
    {
        List<int> assignments = new List<int>();
        for (int i = 0; i < dataPoints.Count; i++)
        {
            assignments.Add(-1);
        }

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            // Assign points to nearest centroid
            for (int i = 0; i < dataPoints.Count; i++)
            {
                int closestCentroid = -1;
                float closestDistance = float.MaxValue;

                for (int j = 0; j < centroids.Count; j++)
                {
                    float distance = ComputeDistance(dataPoints[i], centroids[j]);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCentroid = j;
                    }
                }

                assignments[i] = closestCentroid;
            }

            // Update centroids
            List<float[]> newCentroids = new List<float[]>(centroids.Count);
            for (int i = 0; i < centroids.Count; i++)
            {
                newCentroids.Add(new float[centroids[i].Length]);
            }

            int[] clusterSizes = new int[centroids.Count];

            for (int i = 0; i < dataPoints.Count; i++)
            {
                int cluster = assignments[i];
                for (int j = 0; j < dataPoints[i].Length; j++)
                {
                    newCentroids[cluster][j] += dataPoints[i][j];
                }
                clusterSizes[cluster]++;
            }

            for (int i = 0; i < centroids.Count; i++)
            {
                for (int j = 0; j < newCentroids[i].Length; j++)
                {
                    newCentroids[i][j] /= clusterSizes[i] > 0 ? clusterSizes[i] : 1;
                }
                centroids[i] = newCentroids[i];
            }
        }

        return assignments;
    }

    private float ComputeDistance(float[] a, float[] b)
    {
        float sum = 0f;
        for (int i = 0; i < a.Length; i++)
        {
            sum += Mathf.Pow(a[i] - b[i], 2);
        }
        return Mathf.Sqrt(sum);
    }
}
