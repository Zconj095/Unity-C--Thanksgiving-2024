using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;
public class FractalDatabase
{
    private List<float[]> fractalPatterns;

    public FractalDatabase()
    {
        fractalPatterns = new List<float[]>();
    }

    public void AddPattern(float[] embedding)
    {
        fractalPatterns.Add(embedding);
    }

    public float ComputeFractalDistortion(float[] embedding)
    {
        float distortionScore = 0;
        foreach (var pattern in fractalPatterns)
        {
            distortionScore += EmbeddingSimilarity.ComputeCosineSimilarity(embedding, pattern);
        }

        return 1 - (distortionScore / fractalPatterns.Count); // Normalize distortion
    }
}
