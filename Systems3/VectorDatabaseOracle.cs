using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class VectorDatabaseOracle
{
    private float[][] vectors; // Array of stored vectors
    private float[] queryVector;
    private float similarityThreshold;

    public VectorDatabaseOracle(float[][] vectors, float[] queryVector, float similarityThreshold = 0.9f)
    {
        this.vectors = vectors;
        this.queryVector = queryVector;
        this.similarityThreshold = similarityThreshold;
    }

    public bool IsTargetState(int index)
    {
        return ComputeCosineSimilarity(vectors[index], queryVector) >= similarityThreshold;
    }

    private float ComputeCosineSimilarity(float[] vec1, float[] vec2)
    {
        float dot = 0, mag1 = 0, mag2 = 0;
        for (int i = 0; i < vec1.Length; i++)
        {
            dot += vec1[i] * vec2[i];
            mag1 += vec1[i] * vec1[i];
            mag2 += vec2[i] * vec2[i];
        }
        return dot / (Mathf.Sqrt(mag1) * Mathf.Sqrt(mag2));
    }
}
