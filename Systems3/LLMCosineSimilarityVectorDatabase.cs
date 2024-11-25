using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LLMCosineSimilarityVectorDatabase
{
    private Dictionary<int, float[]> vectorStore = new Dictionary<int, float[]>();

    public void AddVector(int id, float[] vector)
    {
        vectorStore[id] = vector;
    }

    public float[] GetVector(int id)
    {
        return vectorStore.ContainsKey(id) ? vectorStore[id] : null;
    }

    public List<int> QueryNearestNeighbors(float[] query, int topN)
    {
        return vectorStore.Keys.OrderBy(id => CosineSimilarity(query, vectorStore[id]))
                               .Take(topN)
                               .ToList();
    }

    private float CosineSimilarity(float[] a, float[] b)
    {
        float dotProduct = 0, magA = 0, magB = 0;
        for (int i = 0; i < a.Length; i++)
        {
            dotProduct += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }
        return dotProduct / (Mathf.Sqrt(magA) * Mathf.Sqrt(magB));
    }
}
