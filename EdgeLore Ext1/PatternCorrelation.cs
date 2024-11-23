using System;
using System.Collections.Generic;
using UnityEngine;

public static class PatternCorrelation
{
    // Compute cosine similarity between two vectors
    public static float ComputeCosineSimilarity(List<float> pattern, List<float> template)
    {
        if (pattern.Count != template.Count)
            throw new ArgumentException("Pattern and template must have the same dimensions.");

        float dotProduct = 0f;
        float magnitudePattern = 0f;
        float magnitudeTemplate = 0f;

        for (int i = 0; i < pattern.Count; i++)
        {
            dotProduct += pattern[i] * template[i];
            magnitudePattern += pattern[i] * pattern[i];
            magnitudeTemplate += template[i] * template[i];
        }

        if (magnitudePattern == 0 || magnitudeTemplate == 0)
            return 0; // Avoid division by zero

        return dotProduct / (Mathf.Sqrt(magnitudePattern) * Mathf.Sqrt(magnitudeTemplate));
    }
}
