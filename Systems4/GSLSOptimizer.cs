using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Gaussian-Smoothed Line Search (GSLS) Optimizer.
/// Optimizes an objective function using gradient approximations via Gaussian-smoothed sampling.
/// </summary>
public class GSLSOptimizer
{
    // Configuration options
    public int MaxIterations { get; private set; }
    public int MaxEvaluations { get; private set; }
    public float SamplingRadius { get; private set; }
    public int SampleSizeFactor { get; private set; }
    public float InitialStepSize { get; private set; }
    public float MinStepSize { get; private set; }
    public float StepSizeMultiplier { get; private set; }
    public float ArmijoParameter { get; private set; }
    public float MinGradientNorm { get; private set; }

    // State
    private int evaluationCount;

    /// <summary>
    /// Initializes the GSLS optimizer with default or user-specified parameters.
    /// </summary>
    public GSLSOptimizer(
        int maxIterations = 1000,
        int maxEvaluations = 10000,
        float samplingRadius = 1e-6f,
        int sampleSizeFactor = 1,
        float initialStepSize = 1e-2f,
        float minStepSize = 1e-10f,
        float stepSizeMultiplier = 0.4f,
        float armijoParameter = 1e-1f,
        float minGradientNorm = 1e-8f)
    {
        MaxIterations = maxIterations;
        MaxEvaluations = maxEvaluations;
        SamplingRadius = samplingRadius;
        SampleSizeFactor = sampleSizeFactor;
        InitialStepSize = initialStepSize;
        MinStepSize = minStepSize;
        StepSizeMultiplier = stepSizeMultiplier;
        ArmijoParameter = armijoParameter;
        MinGradientNorm = minGradientNorm;
    }

    /// <summary>
    /// Minimizes the provided objective function.
    /// </summary>
    /// <param name="objectiveFunction">Objective function to minimize.</param>
    /// <param name="initialPoint">Initial point for optimization.</param>
    /// <param name="bounds">Bounds for the decision variables.</param>
    /// <returns>Optimized parameters and final objective value.</returns>
    public (float[] OptimizedParams, float FinalValue) Minimize(
        Func<float[], float> objectiveFunction,
        float[] initialPoint,
        (float[] lower, float[] upper) bounds)
    {
        int dimensions = initialPoint.Length;
        evaluationCount = 0;

        // Initialize variables
        float[] currentPoint = initialPoint.ToArray();
        float currentStepSize = InitialStepSize;
        float currentObjectiveValue = objectiveFunction(currentPoint);
        evaluationCount++;

        for (int iteration = 0; iteration < MaxIterations; iteration++)
        {
            if (evaluationCount >= MaxEvaluations)
                break;

            // Generate sample points
            int sampleSize = SampleSizeFactor * dimensions;
            var (directions, samplePoints) = GenerateSampleSet(dimensions, currentPoint, bounds, sampleSize);

            // Evaluate samples
            float[] sampleValues = samplePoints.Select(objectiveFunction).ToArray();
            evaluationCount += sampleValues.Length;

            // Approximate gradient
            float[] gradient = ApproximateGradient(
                dimensions, currentPoint, currentObjectiveValue, directions, sampleValues);

            // Gradient norm
            float gradientNorm = gradient.Select(x => x * x).Sum();
            if (gradientNorm < MinGradientNorm || currentStepSize < MinStepSize)
                break;

            // Perform line search
            float[] nextPoint = new float[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                nextPoint[i] = Mathf.Clamp(
                    currentPoint[i] - currentStepSize * gradient[i],
                    bounds.lower[i], bounds.upper[i]);
            }

            float nextObjectiveValue = objectiveFunction(nextPoint);
            evaluationCount++;

            // Armijo condition
            if (nextObjectiveValue <= currentObjectiveValue - ArmijoParameter * currentStepSize * gradientNorm)
            {
                // Accept the step
                currentPoint = nextPoint;
                currentObjectiveValue = nextObjectiveValue;
                currentStepSize /= 2f * StepSizeMultiplier;
            }
            else
            {
                // Reject the step
                currentStepSize *= StepSizeMultiplier;
            }

            Debug.Log($"Iteration {iteration}, Objective: {currentObjectiveValue}, StepSize: {currentStepSize}");
        }

        return (currentPoint, currentObjectiveValue);
    }

    /// <summary>
    /// Generates a set of sample points around the current point on a sphere.
    /// </summary>
    private (float[][] directions, float[][] points) GenerateSampleSet(
        int dimensions,
        float[] center,
        (float[] lower, float[] upper) bounds,
        int sampleSize)
    {
        List<float[]> directions = new();
        List<float[]> points = new();

        while (directions.Count < sampleSize)
        {
            float[] direction = RandomOnSphere(dimensions);
            float[] point = new float[dimensions];

            for (int i = 0; i < dimensions; i++)
            {
                point[i] = center[i] + SamplingRadius * direction[i];
                if (point[i] < bounds.lower[i] || point[i] > bounds.upper[i])
                    continue;
            }

            directions.Add(direction);
            points.Add(point);
        }

        return (directions.ToArray(), points.ToArray());
    }

    /// <summary>
    /// Generates a random point on the surface of a unit sphere in n dimensions.
    /// </summary>
    private float[] RandomOnSphere(int dimensions)
    {
        float[] randomPoint = new float[dimensions];
        float magnitude = 0;

        for (int i = 0; i < dimensions; i++)
        {
            randomPoint[i] = UnityEngine.Random.Range(-1f, 1f);
            magnitude += randomPoint[i] * randomPoint[i];
        }

        magnitude = Mathf.Sqrt(magnitude);
        for (int i = 0; i < dimensions; i++)
        {
            randomPoint[i] /= magnitude;
        }

        return randomPoint;
    }

    /// <summary>
    /// Approximates the gradient based on sampled points and their objective values.
    /// </summary>
    private float[] ApproximateGradient(int dimensions, float[] center, float centerValue, float[][] directions, float[] values)
    {
        float[] gradient = new float[dimensions];

        for (int i = 0; i < directions.Length; i++)
        {
            for (int j = 0; j < dimensions; j++)
            {
                gradient[j] += (values[i] - centerValue) * directions[i][j];
            }
        }

        for (int j = 0; j < dimensions; j++)
        {
            gradient[j] /= SamplingRadius * directions.Length;
        }

        return gradient;
    }
}
