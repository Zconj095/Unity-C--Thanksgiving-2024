using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class FeedbackClustering
{
    public static Dictionary<int, List<float[]>> KMeansClustering(List<float[]> embeddings, int k)
    {
        // Placeholder K-Means clustering
        var clusters = new Dictionary<int, List<float[]>>();
        for (int i = 0; i < k; i++) clusters[i] = new List<float[]>();

        foreach (var embedding in embeddings)
        {
            int clusterIndex = (int)(embedding.Sum() % k); // Assign cluster (example logic)
            clusters[clusterIndex].Add(embedding);
        }

        return clusters;
    }

    public static void FeedbackAdjust(List<float[]> embeddings, float learningRate)
    {
        foreach (var embedding in embeddings)
        {
            for (int i = 0; i < embedding.Length; i++)
            {
                embedding[i] += learningRate * (float)Math.Sin(embedding[i]); // Feedback adjustment
            }
        }
    }
}
