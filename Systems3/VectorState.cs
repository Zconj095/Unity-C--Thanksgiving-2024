using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class VectorState
{
    public float[] QuantumVector { get; private set; }
    public float[] HyperdimensionalVector { get; private set; }

    public VectorState(float[] quantumVector, float[] hyperdimensionalVector)
    {
        QuantumVector = quantumVector;
        HyperdimensionalVector = hyperdimensionalVector;
    }

    public float[] CombineStates(Func<float, float, float> combineFunc)
    {
        if (QuantumVector.Length != HyperdimensionalVector.Length)
        {
            throw new Exception("Vector lengths must match.");
        }

        float[] combined = new float[QuantumVector.Length];
        for (int i = 0; i < QuantumVector.Length; i++)
        {
            combined[i] = combineFunc(QuantumVector[i], HyperdimensionalVector[i]);
        }
        return combined;
    }
}
