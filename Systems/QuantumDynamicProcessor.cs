using System;
using UnityEngine;

public class QuantumDynamicProcessor : MonoBehaviour
{
    // Compute Quantum State Dynamic Variance
    public static float ComputeDynamicVariance(float[,] quantumStates, bool rowWise = true)
    {
        int rows = quantumStates.GetLength(0);
        int cols = quantumStates.GetLength(1);

        float[] means = CorticalMatrixProcessor.ComputeCorticalMeans(quantumStates, rowWise);

        float variance = 0f;
        for (int i = 0; i < means.Length; i++)
        {
            float diffSum = 0f;
            for (int j = 0; j < (rowWise ? cols : rows); j++)
            {
                float value = rowWise ? quantumStates[i, j] : quantumStates[j, i];
                diffSum += Mathf.Pow(value - means[i], 2);
            }
            variance += diffSum / (rowWise ? cols : rows);
        }

        return variance / means.Length; // Normalize over the number of means
    }

    // Compute Cross-Correlation Between Means
    public static float[] ComputeCrossCorrelation(float[,] matrixA, float[,] matrixB, bool rowWise = true)
    {
        float[] meansA = CorticalMatrixProcessor.ComputeCorticalMeans(matrixA, rowWise);
        float[] meansB = CorticalMatrixProcessor.ComputeCorticalMeans(matrixB, rowWise);

        int length = Math.Min(meansA.Length, meansB.Length);
        float[] crossCorrelation = new float[length];

        for (int i = 0; i < length; i++)
        {
            crossCorrelation[i] = meansA[i] * meansB[i]; // Simple cross-product
        }

        return crossCorrelation;
    }

    // Compute Flux Dynamic Cross Interjections
    public static float ComputeFluxInterjection(float[,] fluxA, float[,] fluxB)
    {
        int rows = fluxA.GetLength(0);
        int cols = fluxA.GetLength(1);

        float interjectionSum = 0f;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                interjectionSum += fluxA[i, j] * fluxB[i, j]; // Overlap of flux components
            }
        }

        return interjectionSum / (rows * cols); // Normalize by the number of elements
    }

    void Start()
    {
        // Example Matrices
        float[,] quantumStatesA = {
            { 1.0f, 2.0f, 3.0f },
            { 4.0f, 5.0f, 6.0f },
            { 7.0f, 8.0f, 9.0f }
        };

        float[,] quantumStatesB = {
            { 0.5f, 1.5f, 2.5f },
            { 3.5f, 4.5f, 5.5f },
            { 6.5f, 7.5f, 8.5f }
        };

        // Compute Dynamic Variance
        float varianceA = ComputeDynamicVariance(quantumStatesA);
        Debug.Log($"Quantum State Dynamic Variance (A): {varianceA}");

        // Compute Cross-Correlation
        float[] crossCorrelation = ComputeCrossCorrelation(quantumStatesA, quantumStatesB);
        Debug.Log("Cross-Correlation of Means: " + string.Join(", ", crossCorrelation));

        // Compute Flux Dynamic Cross Interjections
        float fluxInterjection = ComputeFluxInterjection(quantumStatesA, quantumStatesB);
        Debug.Log($"Flux Dynamic Cross Interjection: {fluxInterjection}");
    }
}
