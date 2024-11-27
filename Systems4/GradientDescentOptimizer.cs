using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gradient Descent Optimizer with support for learning rate scheduling and callbacks.
/// </summary>
public class GradientDescentOptimizer
{
    public int MaxIterations { get; private set; }
    public float Tolerance { get; private set; }
    public Func<int, float> LearningRateSchedule { get; private set; }
    public Action<int, float[], float, float> Callback { get; private set; }

    private float[] currentParams;
    private float currentStepSize;
    private int iterationCount;

    /// <summary>
    /// Initializes the Gradient Descent Optimizer.
    /// </summary>
    /// <param name="maxIterations">Maximum number of iterations.</param>
    /// <param name="initialLearningRate">Initial learning rate (constant if no schedule is provided).</param>
    /// <param name="tolerance">Convergence tolerance.</param>
    /// <param name="callback">Optional callback for monitoring progress.</param>
    public GradientDescentOptimizer(
        int maxIterations = 100,
        float initialLearningRate = 0.01f,
        float tolerance = 1e-6f,
        Action<int, float[], float, float> callback = null)
    {
        MaxIterations = maxIterations;
        Tolerance = tolerance;
        Callback = callback;

        // Default to constant learning rate if no schedule is provided
        LearningRateSchedule = (iteration) => initialLearningRate;
    }

    /// <summary>
    /// Sets a custom learning rate schedule.
    /// </summary>
    /// <param name="schedule">A function that determines the learning rate for each iteration.</param>
    public void SetLearningRateSchedule(Func<int, float> schedule)
    {
        LearningRateSchedule = schedule;
    }

    /// <summary>
    /// Minimizes the provided objective function.
    /// </summary>
    /// <param name="objectiveFunction">The objective function to minimize.</param>
    /// <param name="initialParams">Initial parameters for optimization.</param>
    /// <param name="gradientFunction">The gradient function of the objective function.</param>
    /// <returns>Optimized parameters and the final objective value.</returns>
    public (float[] OptimizedParams, float FinalValue) Minimize(
        Func<float[], float> objectiveFunction,
        float[] initialParams,
        Func<float[], float[]> gradientFunction)
    {
        currentParams = (float[])initialParams.Clone();
        currentStepSize = float.MaxValue;
        iterationCount = 0;

        while (iterationCount < MaxIterations && currentStepSize > Tolerance)
        {
            iterationCount++;

            // Compute objective value and gradient
            float currentValue = objectiveFunction(currentParams);
            float[] gradient = gradientFunction(currentParams);

            // Compute the learning rate for this iteration
            float learningRate = LearningRateSchedule(iterationCount);

            // Update parameters
            float[] newParams = new float[currentParams.Length];
            for (int i = 0; i < currentParams.Length; i++)
            {
                newParams[i] = currentParams[i] - learningRate * gradient[i];
            }

            // Calculate step size (parameter change)
            currentStepSize = 0f;
            for (int i = 0; i < currentParams.Length; i++)
            {
                currentStepSize += Mathf.Pow(newParams[i] - currentParams[i], 2);
            }
            currentStepSize = Mathf.Sqrt(currentStepSize);

            // Update current parameters
            currentParams = newParams;

            // Invoke callback if provided
            Callback?.Invoke(iterationCount, currentParams, currentValue, currentStepSize);
        }

        float finalValue = objectiveFunction(currentParams);
        return (currentParams, finalValue);
    }
}
