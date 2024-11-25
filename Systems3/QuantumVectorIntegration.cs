using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuantumVectorIntegration
{
    private LLMQuantumState LLMQuantumState;

    public QuantumVectorIntegration(int stateSize)
    {
        LLMQuantumState = new LLMQuantumState(stateSize);
    }

    public float ComputeQuantumCorrelation(float[] classicalVector, LLMQuantumState LLMQuantumState)
    {
        float correlation = 0;

        for (int i = 0; i < classicalVector.Length; i++)
        {
            correlation += (float)(classicalVector[i] * LLMQuantumState.Amplitudes[i].Real);
        }

        return correlation;
    }

    public void EnhanceClusterAssignment(float[] classicalVector, List<float[]> clusterCenters)
    {
        foreach (var center in clusterCenters)
        {
            float correlation = ComputeQuantumCorrelation(classicalVector, LLMQuantumState);
            Console.WriteLine($"Correlation with cluster: {correlation}");
        }
    }
}
