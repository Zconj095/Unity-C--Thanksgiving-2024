using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdHocDataGenerator
{
    public static (float[,], int[], float[,], int[]) GenerateData(
        int trainingSize,
        int testSize,
        int n,
        float gap,
        bool oneHot = true,
        bool includeSampleTotal = false
    )
    {
        if (n != 2 && n != 3)
        {
            throw new ArgumentException("Supported values of 'n' are 2 and 3 only.");
        }

        int count = (n == 2) ? 100 : 20;

        // Generate Z matrices
        float[,] z = new float[,] { { 1, 0 }, { 0, -1 } };
        float[,] identity = new float[,] { { 1, 0 }, { 0, 1 } };
        List<float[,]> zMatrices = GenerateZMatrices(n, z, identity);

        // Generate parity operator and random unitary
        float[,] parityMatrix = GenerateParityMatrix(n);
        float[,] randomUnitary = GenerateRandomUnitary(n);

        // Generate data points
        float[] xvals = Enumerable.Range(0, count)
                                   .Select(i => i * 2 * Mathf.PI / count)
                                   .ToArray();
        List<float> sampleTotal = GenerateSamplePoints(xvals, zMatrices, parityMatrix, randomUnitary, n, gap);

        // Extract training and testing samples
        (float[,], int[]) trainingData = ExtractSamples(sampleTotal, xvals, trainingSize, n);
        (float[,], int[]) testData = ExtractSamples(sampleTotal, xvals, testSize, n);

        return (trainingData.Item1, trainingData.Item2, testData.Item1, testData.Item2);
    }

    private static List<float[,]> GenerateZMatrices(int n, float[,] z, float[,] identity)
    {
        var zMatrices = new List<float[,]>();
        for (int i = 0; i < n; i++)
        {
            float[,] currentZ = KroneckerProduct(Enumerable.Repeat(identity, i)
                .Concat(new[] { z })
                .Concat(Enumerable.Repeat(identity, n - i - 1))
                .ToArray());
            zMatrices.Add(currentZ);
        }
        return zMatrices;
    }

    private static float[,] GenerateParityMatrix(int n)
    {
        int size = (int)Math.Pow(2, n);
        float[,] parityMatrix = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            int parity = Convert.ToString(i, 2).Count(c => c == '1') % 2;
            parityMatrix[i, i] = parity == 0 ? 1 : -1;
        }
        return parityMatrix;
    }

    private static float[,] GenerateRandomUnitary(int n)
    {
        int size = (int)Math.Pow(2, n);
        System.Random random = new System.Random();
        float[,] randomMatrix = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                randomMatrix[i, j] = (float)random.NextDouble();
            }
        }
        return NormalizeMatrix(randomMatrix);
    }

    private static float[,] NormalizeMatrix(float[,] matrix)
    {
        float norm = Mathf.Sqrt(matrix.Cast<float>().Sum(v => v * v));
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] /= norm;
            }
        }
        return matrix;
    }

    private static List<float> GenerateSamplePoints(
        float[] xvals,
        List<float[,]> zMatrices,
        float[,] parityMatrix,
        float[,] randomUnitary,
        int n,
        float gap
    )
    {
        List<float> samplePoints = new List<float>();
        int count = xvals.Length;
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                float[] x = { xvals[i], xvals[j] };
                float expectation = ComputeExpectationValue(x, zMatrices, parityMatrix, randomUnitary, n);
                samplePoints.Add(Mathf.Abs(expectation) > gap ? Mathf.Sign(expectation) : 0);
            }
        }
        return samplePoints;
    }

    private static float ComputeExpectationValue(
        float[] x,
        List<float[,]> zMatrices,
        float[,] parityMatrix,
        float[,] randomUnitary,
        int n
    )
    {
        float[,] state = ApplyFeatureMap(x, zMatrices, n);
        float[,] transformedState = ApplyRandomUnitary(state, randomUnitary);
        return ExpectationValue(transformedState, parityMatrix);
    }

    private static float[,] ApplyFeatureMap(float[] x, List<float[,]> zMatrices, int n)
    {
        // Example implementation of feature map
        return zMatrices[0]; // Placeholder logic
    }

    private static float[,] ApplyRandomUnitary(float[,] state, float[,] randomUnitary)
    {
        return state; // Placeholder for applying random unitary
    }

    private static float ExpectationValue(float[,] state, float[,] operatorMatrix)
    {
        return 1.0f; // Placeholder logic
    }

    private static (float[,], int[]) ExtractSamples(List<float> sampleTotal, float[] xvals, int numSamples, int n)
    {
        float[,] samples = new float[numSamples, n];
        int[] labels = new int[numSamples];

        for (int i = 0; i < numSamples; i++)
        {
            // Example extraction logic
            samples[i, 0] = xvals[i % xvals.Length];
            labels[i] = (int)sampleTotal[i];
        }

        return (samples, labels);
    }

    private static float[,] KroneckerProduct(params float[][,] matrices)
    {
        // Implementation of Kronecker product
        return new float[1, 1]; // Placeholder logic
    }
}
