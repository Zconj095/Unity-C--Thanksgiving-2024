using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

public class EmbeddingSimilarity
{
    public static float ComputeCosineSimilarity(float[] vectorA, float[] vectorB)
    {
        float dot = 0, magA = 0, magB = 0;
        for (int i = 0; i < vectorA.Length; i++)
        {
            dot += vectorA[i] * vectorB[i];
            magA += vectorA[i] * vectorA[i];
            magB += vectorB[i] * vectorB[i];
        }

        return dot / (Mathf.Sqrt(magA) * Mathf.Sqrt(magB));
    }

    public static List<List<float[]>> CategorizeData(List<float[]> embeddings, float similarityThreshold)
    {
        var categories = new List<List<float[]>>();

        foreach (var embedding in embeddings)
        {
            bool addedToCategory = false;
            foreach (var category in categories)
            {
                if (ComputeCosineSimilarity(embedding, category[0]) >= similarityThreshold)
                {
                    category.Add(embedding);
                    addedToCategory = true;
                    break;
                }
            }

            if (!addedToCategory)
            {
                categories.Add(new List<float[]> { embedding });
            }
        }

        return categories;
    }
}
