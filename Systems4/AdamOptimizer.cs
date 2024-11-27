using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Adam optimizer implementation in Unity.
/// Includes AMSGrad variant for improved convergence properties.
/// </summary>
public class AdamOptimizer
{
    private int maxIterations;
    private float tolerance;
    private float learningRate;
    private float beta1;
    private float beta2;
    private float noiseFactor;
    private float epsilon;
    private bool useAMSGrad;
    private string snapshotDirectory;

    private int timeStep;
    private float[] firstMoment;
    private float[] secondMoment;
    private float[] maxSecondMoment; // For AMSGrad

    /// <summary>
    /// Constructor for the Adam optimizer.
    /// </summary>
    public AdamOptimizer(
        int maxIterations = 10000,
        float tolerance = 1e-6f,
        float learningRate = 1e-3f,
        float beta1 = 0.9f,
        float beta2 = 0.99f,
        float noiseFactor = 1e-8f,
        float epsilon = 1e-10f,
        bool useAMSGrad = false,
        string snapshotDirectory = null)
    {
        this.maxIterations = maxIterations;
        this.tolerance = tolerance;
        this.learningRate = learningRate;
        this.beta1 = beta1;
        this.beta2 = beta2;
        this.noiseFactor = noiseFactor;
        this.epsilon = epsilon;
        this.useAMSGrad = useAMSGrad;
        this.snapshotDirectory = snapshotDirectory;

        Reset();
    }

    /// <summary>
    /// Resets the optimizer's internal state.
    /// </summary>
    public void Reset()
    {
        timeStep = 0;
        firstMoment = null;
        secondMoment = null;
        maxSecondMoment = useAMSGrad ? null : null;
    }

    /// <summary>
    /// Minimizes the given function using Adam optimization.
    /// </summary>
    public (float[] Parameters, float Loss) Minimize(
        Func<float[], float> lossFunction,
        float[] initialParams,
        Func<float[], float[]> gradientFunction)
    {
        int paramCount = initialParams.Length;
        firstMoment = new float[paramCount];
        secondMoment = new float[paramCount];
        if (useAMSGrad) maxSecondMoment = new float[paramCount];

        float[] parameters = (float[])initialParams.Clone();
        float[] gradients = gradientFunction(parameters);

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            timeStep++;

            // Update first and second moments
            for (int i = 0; i < paramCount; i++)
            {
                firstMoment[i] = beta1 * firstMoment[i] + (1 - beta1) * gradients[i];
                secondMoment[i] = beta2 * secondMoment[i] + (1 - beta2) * gradients[i] * gradients[i];

                if (useAMSGrad)
                {
                    maxSecondMoment[i] = Math.Max(maxSecondMoment[i], secondMoment[i]);
                }
            }

            // Compute bias-corrected moments
            float correctedLearningRate = learningRate * Mathf.Sqrt(1 - Mathf.Pow(beta2, timeStep)) /
                                          (1 - Mathf.Pow(beta1, timeStep));

            // Update parameters
            for (int i = 0; i < paramCount; i++)
            {
                float denominator = Mathf.Sqrt(useAMSGrad ? maxSecondMoment[i] : secondMoment[i]) + noiseFactor;
                parameters[i] -= correctedLearningRate * firstMoment[i] / denominator;
            }

            // Save state if snapshot directory is specified
            if (!string.IsNullOrEmpty(snapshotDirectory))
            {
                SaveSnapshot();
            }

            // Check for convergence
            if (Norm(gradients) < tolerance)
            {
                break;
            }

            // Recompute gradients
            gradients = gradientFunction(parameters);
        }

        float finalLoss = lossFunction(parameters);
        return (parameters, finalLoss);
    }

    /// <summary>
    /// Saves the optimizer's internal state to a file.
    /// </summary>
    private void SaveSnapshot()
    {
        string filePath = Path.Combine(snapshotDirectory, "adam_params.csv");

        using (var writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(string.Join(",", firstMoment));
            writer.WriteLine(string.Join(",", secondMoment));
            if (useAMSGrad)
            {
                writer.WriteLine(string.Join(",", maxSecondMoment));
            }
            writer.WriteLine(timeStep);
        }
    }

    /// <summary>
    /// Loads the optimizer's internal state from a file.
    /// </summary>
    public void LoadSnapshot(string snapshotDirectory)
    {
        string filePath = Path.Combine(snapshotDirectory, "adam_params.csv");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Snapshot file not found at {filePath}");
        }

        using (var reader = new StreamReader(filePath))
        {
            firstMoment = Array.ConvertAll(reader.ReadLine().Split(','), float.Parse);
            secondMoment = Array.ConvertAll(reader.ReadLine().Split(','), float.Parse);
            if (useAMSGrad)
            {
                maxSecondMoment = Array.ConvertAll(reader.ReadLine().Split(','), float.Parse);
            }
            timeStep = int.Parse(reader.ReadLine());
        }
    }

    /// <summary>
    /// Computes the Euclidean norm of a vector.
    /// </summary>
    private static float Norm(float[] vector)
    {
        float sum = 0f;
        foreach (float value in vector)
        {
            sum += value * value;
        }
        return Mathf.Sqrt(sum);
    }
}
