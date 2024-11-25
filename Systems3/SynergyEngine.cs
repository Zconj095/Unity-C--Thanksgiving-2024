using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SynergyEngine
{
    public float ComputeCorrelation(float[] vec1, float[] vec2)
    {
        // Use cosine similarity for correlation
        float dot = 0, mag1 = 0, mag2 = 0;
        for (int i = 0; i < vec1.Length; i++)
        {
            dot += vec1[i] * vec2[i];
            mag1 += vec1[i] * vec1[i];
            mag2 += vec2[i] * vec2[i];
        }
        return dot / (Mathf.Sqrt(mag1) * Mathf.Sqrt(mag2));
    }

    public List<(int dbIndex, int vectorId, float score)> FindSimilarVectors(
        float[] query,
        MultiDatabaseManager dbManager,
        float threshold = 0.8f)
    {
        List<(int dbIndex, int vectorId, float score)> results = new List<(int, int, float)>();

        foreach (var db in dbManager.GetAllDatabases())
        {
            foreach (var entry in db.GetAllVectors())
            {
                float correlation = ComputeCorrelation(query, entry.Value);
                if (correlation > threshold)
                {
                    results.Add((dbManager.GetAllDatabases().IndexOf(db), entry.Key, correlation));
                }
            }
        }

        results.Sort((a, b) => b.score.CompareTo(a.score)); // Sort by correlation score
        return results;
    }
}
