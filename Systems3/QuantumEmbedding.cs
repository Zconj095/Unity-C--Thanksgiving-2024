using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class QuantumEmbedding
{
    public float[] GenerateQuantumState(float[] vector, float amplitude)
    {
        float[] quantumState = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            quantumState[i] = (float)(vector[i] * Math.Cos(amplitude) + Math.Sin(amplitude));
        }
        return quantumState;
    }
}
