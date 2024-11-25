using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

public class HyperdimensionalLogic
{
    public static float[] Bind(float[] vectorA, float[] vectorB)
    {
        // Element-wise multiplication (binding operation)
        float[] result = new float[vectorA.Length];
        for (int i = 0; i < vectorA.Length; i++)
        {
            result[i] = vectorA[i] * vectorB[i];
        }
        return result;
    }

    public static float[] Superpose(float[] vectorA, float[] vectorB)
    {
        // Element-wise addition (superposition operation)
        float[] result = new float[vectorA.Length];
        for (int i = 0; i < vectorA.Length; i++)
        {
            result[i] = vectorA[i] + vectorB[i];
        }
        return result;
    }

    public static float[] Normalize(float[] vector)
    {
        // Normalize vector to unit length
        float magnitude = (float)Math.Sqrt(vector.Sum(x => x * x));
        return vector.Select(x => x / magnitude).ToArray();
    }
}
