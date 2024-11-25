using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class KMeans
{
    public static List<List<float[]>> Cluster(float[][] data, int k, int maxIterations = 100)
    {
        // Initialize centroids randomly
        float[][] centroids = new float[k][];
        System.Random rand = new System.Random();
        for (int i = 0; i < k; i++)
        {
            centroids[i] = data[rand.Next(data.Length)];
        }

        List<List<float[]>> clusters = null;

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            clusters = new List<List<float[]>>(new List<float[]>[k]);
            for (int i = 0; i < k; i++) clusters[i] = new List<float[]>();

            // Assign data points to nearest centroid
            foreach (float[] point in data)
            {
                int nearestCentroid = 0;
                float minDistance = float.MaxValue;

                for (int i = 0; i < k; i++)
                {
                    float distance = EuclideanDistance(point, centroids[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestCentroid = i;
                    }
                }
                clusters[nearestCentroid].Add(point);
            }

            // Recalculate centroids
            for (int i = 0; i < k; i++)
            {
                centroids[i] = CalculateMean(clusters[i]);
            }
        }

        return clusters;
    }

    private static float[] CalculateMean(List<float[]> points)
    {
        int dimensions = points[0].Length;
        float[] mean = new float[dimensions];

        foreach (float[] point in points)
        {
            for (int i = 0; i < dimensions; i++)
            {
                mean[i] += point[i];
            }
        }

        for (int i = 0; i < dimensions; i++)
        {
            mean[i] /= points.Count;
        }

        return mean;
    }

    private static float EuclideanDistance(float[] a, float[] b)
    {
        float sum = 0;
        for (int i = 0; i < a.Length; i++)
        {
            sum += (a[i] - b[i]) * (a[i] - b[i]);
        }
        return Mathf.Sqrt(sum);
    }
}
