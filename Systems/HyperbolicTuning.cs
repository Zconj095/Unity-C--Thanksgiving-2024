using System;
using System.Collections.Generic;
using UnityEngine;
public class HyperbolicTuning : MonoBehaviour
{
    // Hyperbolic distance function
    public static float HyperbolicDistance(float[] x, float[] y)
    {
        float normX = VectorMagnitude(x);
        float normY = VectorMagnitude(y);

        if (normX >= 1 || normY >= 1)
            throw new ArgumentException("Input vectors must lie within the unit hyperbolic space.");

        float normDiff = VectorSquaredDifference(x, y);
        return MathF.Acosh(1 + (2 * normDiff) / ((1 - normX * normX) * (1 - normY * normY)));
    }

    // Loss function for fine-tuning
    public static float HyperbolicLoss(float[] prediction, float[] target, float regularization, float[] parameters)
    {
        float distance = HyperbolicDistance(prediction, target);
        float regTerm = regularization * VectorMagnitude(parameters);
        return distance * distance + regTerm;
    }

    // Helper methods
    private static float VectorMagnitude(float[] v)
    {
        float sum = 0;
        foreach (var val in v) sum += val * val;
        return MathF.Sqrt(sum);
    }

    private static float VectorSquaredDifference(float[] x, float[] y)
    {
        float sum = 0;
        for (int i = 0; i < x.Length; i++)
        {
            float diff = x[i] - y[i];
            sum += diff * diff;
        }
        return sum;
    }

    void Start()
    {
        // Define fractal deep learning network
        var fractalNetwork = new FractalDeepLearning(new int[] { 3, 5, 3 });

        // Example input and target
        float[] input = { 0.2f, 0.5f, 0.8f };
        float[] target = { 0.1f, 0.7f, 0.3f };

        // Forward pass
        float[] prediction = fractalNetwork.Forward(input);

        // Hyperbolic loss calculation
        float loss = HyperbolicTuning.HyperbolicLoss(prediction, target, 0.01f, new float[] { 0.5f, 0.3f, 0.2f });

        Debug.Log($"Prediction: [{string.Join(", ", prediction)}]");
        Debug.Log($"Hyperbolic Loss: {loss}");
    }

}
