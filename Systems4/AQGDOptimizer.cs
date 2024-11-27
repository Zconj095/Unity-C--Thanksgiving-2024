using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Analytic Quantum Gradient Descent (AQGD) optimizer.
/// Includes support for momentum and step size scheduling.
/// </summary>
public class AQGDOptimizer
{
    private List<int> maxIterations;
    private List<float> learningRates;
    private List<float> momenta;
    private float tolerance;
    private float paramTolerance;
    private int averagingWindow;

    private float[] prevParams;
    private List<float> prevObjectiveValues;
    private List<float[]> prevGradients;
    private int evaluationCount;

    /// <summary>
    /// Constructor for the AQGD optimizer.
    /// </summary>
    public AQGDOptimizer(
        int maxIterations = 1000,
        float learningRate = 1.0f,
        float momentum = 0.25f,
        float tolerance = 1e-6f,
        float paramTolerance = 1e-6f,
        int averagingWindow = 10)
    {
        this.maxIterations = new List<int> { maxIterations };
        this.learningRates = new List<float> { learningRate };
        this.momenta = new List<float> { momentum };
        this.tolerance = tolerance;
        this.paramTolerance = paramTolerance;
        this.averagingWindow = averagingWindow;

        ResetState();
    }

    /// <summary>
    /// Resets the optimizer's internal state.
    /// </summary>
    private void ResetState()
    {
        prevParams = null;
        prevObjectiveValues = new List<float>();
        prevGradients = new List<float[]>();
        evaluationCount = 0;
    }

    /// <summary>
    /// Minimizes the provided loss function using AQGD.
    /// </summary>
    public (float[] OptimizedParams, float FinalLoss) Minimize(
        Func<float[], float> lossFunction,
        float[] initialParams,
        Func<float[], float[]> gradientFunction = null)
    {
        ResetState();

        float[] parameters = (float[])initialParams.Clone();
        float[] momentumVector = new float[parameters.Length];
        int iterationCount = 0;
        int epoch = 0;
        bool converged = false;

        foreach (var (eta, momentumCoeff) in Zip(learningRates, momenta))
        {
            Debug.Log($"Epoch {epoch + 1}: Learning Rate = {eta}, Momentum = {momentumCoeff}");

            int currentMaxIterations = maxIterations[Math.Min(epoch, maxIterations.Count - 1)];

            for (int iter = 0; iter < currentMaxIterations; iter++)
            {
                iterationCount++;

                // Compute gradient and loss
                float[] gradient = gradientFunction != null
                    ? gradientFunction(parameters)
                    : ComputeNumericalGradient(lossFunction, parameters);

                float loss = lossFunction(parameters);
                evaluationCount++;

                Debug.Log($"Iteration {iterationCount}: Loss = {loss}, Gradient Norm = {Norm(gradient)}");

                // Check for convergence
                converged = CheckParameterConvergence(parameters) ||
                            CheckObjectiveConvergence(loss);

                if (converged)
                    break;

                // Update parameters using gradient descent with momentum
                UpdateParameters(ref parameters, gradient, ref momentumVector, eta, momentumCoeff);
            }

            if (converged)
                break;

            epoch++;
        }

        float finalLoss = lossFunction(parameters);
        return (parameters, finalLoss);
    }

    /// <summary>
    /// Updates parameters and momentum using gradient descent.
    /// </summary>
    private void UpdateParameters(
        ref float[] parameters,
        float[] gradient,
        ref float[] momentumVector,
        float stepSize,
        float momentumCoeff)
    {
        for (int i = 0; i < parameters.Length; i++)
        {
            momentumVector[i] = (1 - momentumCoeff) * gradient[i] + momentumCoeff * momentumVector[i];
            parameters[i] -= stepSize * momentumVector[i];
        }
    }

    /// <summary>
    /// Checks for convergence based on parameter changes.
    /// </summary>
    private bool CheckParameterConvergence(float[] parameters)
    {
        if (prevParams == null)
        {
            prevParams = (float[])parameters.Clone();
            return false;
        }

        float paramChange = Norm(Difference(parameters, prevParams));
        prevParams = (float[])parameters.Clone();
        return paramChange < paramTolerance;
    }

    /// <summary>
    /// Checks for convergence based on objective function changes.
    /// </summary>
    private bool CheckObjectiveConvergence(float loss)
    {
        if (prevObjectiveValues.Count < averagingWindow)
        {
            prevObjectiveValues.Add(loss);
            return false;
        }

        prevObjectiveValues.Add(loss);
        prevObjectiveValues.RemoveAt(0);

        float prevAverage = Average(prevObjectiveValues.GetRange(0, averagingWindow - 1));
        float currentAverage = Average(prevObjectiveValues);

        return Math.Abs(prevAverage - currentAverage) < tolerance;
    }

    /// <summary>
    /// Computes numerical gradients for the loss function.
    /// </summary>
    private float[] ComputeNumericalGradient(Func<float[], float> lossFunction, float[] parameters)
    {
        float epsilon = 1e-6f;
        float[] gradient = new float[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            float[] paramsPlus = (float[])parameters.Clone();
            float[] paramsMinus = (float[])parameters.Clone();
            paramsPlus[i] += epsilon;
            paramsMinus[i] -= epsilon;

            gradient[i] = (lossFunction(paramsPlus) - lossFunction(paramsMinus)) / (2 * epsilon);
        }

        return gradient;
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

    /// <summary>
    /// Computes the difference between two vectors.
    /// </summary>
    private static float[] Difference(float[] vector1, float[] vector2)
    {
        float[] result = new float[vector1.Length];
        for (int i = 0; i < vector1.Length; i++)
        {
            result[i] = vector1[i] - vector2[i];
        }
        return result;
    }

    /// <summary>
    /// Computes the average of a list of numbers.
    /// </summary>
    private static float Average(List<float> values)
    {
        float sum = 0f;
        foreach (float value in values)
        {
            sum += value;
        }
        return sum / values.Count;
    }

    /// <summary>
    /// Zips two lists together.
    /// </summary>
    private static IEnumerable<(T1, T2)> Zip<T1, T2>(IEnumerable<T1> list1, IEnumerable<T2> list2)
    {
        using var enumerator1 = list1.GetEnumerator();
        using var enumerator2 = list2.GetEnumerator();

        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            yield return (enumerator1.Current, enumerator2.Current);
        }
    }
}
