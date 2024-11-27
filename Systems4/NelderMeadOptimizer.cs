using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Nelder-Mead optimizer for Unity.
/// Performs unconstrained optimization using the simplex algorithm.
/// </summary>
public class NelderMeadOptimizer
{
    public int MaxIterations { get; private set; }
    public int MaxFunctionEvaluations { get; private set; }
    public float Tolerance { get; private set; }
    public bool Verbose { get; private set; }

    /// <summary>
    /// Initializes the Nelder-Mead optimizer.
    /// </summary>
    /// <param name="maxIterations">Maximum number of iterations allowed.</param>
    /// <param name="maxFunctionEvaluations">Maximum number of function evaluations allowed.</param>
    /// <param name="tolerance">Tolerance for convergence.</param>
    /// <param name="verbose">Enables verbose output for debugging purposes.</param>
    public NelderMeadOptimizer(
        int maxIterations = 1000,
        int maxFunctionEvaluations = 1000,
        float tolerance = 1e-4f,
        bool verbose = false)
    {
        MaxIterations = maxIterations;
        MaxFunctionEvaluations = maxFunctionEvaluations;
        Tolerance = tolerance;
        Verbose = verbose;
    }

    /// <summary>
    /// Minimizes the provided objective function.
    /// </summary>
    /// <param name="objectiveFunction">Objective function to minimize.</param>
    /// <param name="initialGuess">Initial guess for the parameters.</param>
    /// <param name="stepSize">Step size for the initial simplex.</param>
    /// <returns>Tuple containing optimized parameters and final objective value.</returns>
    public (float[] OptimizedParams, float FinalValue) Minimize(
        Func<float[], float> objectiveFunction,
        float[] initialGuess,
        float stepSize = 1.0f)
    {
        int dimensions = initialGuess.Length;

        // Initialize the simplex
        float[][] simplex = InitializeSimplex(initialGuess, stepSize);
        float[] functionValues = simplex.Select(objectiveFunction).ToArray();

        int iteration = 0;
        int functionEvaluations = dimensions + 1;

        while (iteration < MaxIterations && functionEvaluations < MaxFunctionEvaluations)
        {
            // Sort simplex by function value
            Array.Sort(functionValues, simplex);

            // Check convergence
            if (Converged(simplex, functionValues))
            {
                if (Verbose)
                    Debug.Log($"Converged after {iteration} iterations with value {functionValues[0]}");
                break;
            }

            // Compute centroid of all points except the worst
            float[] centroid = ComputeCentroid(simplex, dimensions);

            // Reflect the worst point
            float[] reflected = Reflect(simplex[dimensions], centroid, 1.0f);
            float reflectedValue = objectiveFunction(reflected);
            functionEvaluations++;

            if (reflectedValue < functionValues[0])
            {
                // Expansion
                float[] expanded = Reflect(simplex[dimensions], centroid, 2.0f);
                float expandedValue = objectiveFunction(expanded);
                functionEvaluations++;

                if (expandedValue < reflectedValue)
                {
                    simplex[dimensions] = expanded;
                    functionValues[dimensions] = expandedValue;
                }
                else
                {
                    simplex[dimensions] = reflected;
                    functionValues[dimensions] = reflectedValue;
                }
            }
            else if (reflectedValue < functionValues[dimensions - 1])
            {
                // Accept reflection
                simplex[dimensions] = reflected;
                functionValues[dimensions] = reflectedValue;
            }
            else
            {
                // Contraction
                float[] contracted = Reflect(simplex[dimensions], centroid, 0.5f);
                float contractedValue = objectiveFunction(contracted);
                functionEvaluations++;

                if (contractedValue < functionValues[dimensions])
                {
                    simplex[dimensions] = contracted;
                    functionValues[dimensions] = contractedValue;
                }
                else
                {
                    // Shrink the simplex
                    for (int i = 1; i < simplex.Length; i++)
                    {
                        simplex[i] = simplex[0].Zip(simplex[i], (x0, xi) => x0 + 0.5f * (xi - x0)).ToArray();
                        functionValues[i] = objectiveFunction(simplex[i]);
                    }
                    functionEvaluations += dimensions;
                }
            }

            iteration++;
            if (Verbose)
                Debug.Log($"Iteration {iteration}: Best Value={functionValues[0]}");
        }

        return (simplex[0], functionValues[0]);
    }

    /// <summary>
    /// Initializes the simplex around the initial guess.
    /// </summary>
    private float[][] InitializeSimplex(float[] initialGuess, float stepSize)
    {
        int dimensions = initialGuess.Length;
        float[][] simplex = new float[dimensions + 1][];
        simplex[0] = initialGuess;

        for (int i = 0; i < dimensions; i++)
        {
            float[] vertex = initialGuess.ToArray();
            vertex[i] += stepSize;
            simplex[i + 1] = vertex;
        }

        return simplex;
    }

    /// <summary>
    /// Computes the centroid of all points except the worst.
    /// </summary>
    private float[] ComputeCentroid(float[][] simplex, int dimensions)
    {
        float[] centroid = new float[dimensions];

        for (int i = 0; i < simplex.Length - 1; i++)
        {
            for (int j = 0; j < dimensions; j++)
            {
                centroid[j] += simplex[i][j];
            }
        }

        for (int j = 0; j < dimensions; j++)
        {
            centroid[j] /= dimensions;
        }

        return centroid;
    }

    /// <summary>
    /// Reflects a point around the centroid with a given coefficient.
    /// </summary>
    private float[] Reflect(float[] worst, float[] centroid, float coefficient)
    {
        return centroid.Zip(worst, (c, w) => c + coefficient * (c - w)).ToArray();
    }

    /// <summary>
    /// Checks if the simplex has converged.
    /// </summary>
    private bool Converged(float[][] simplex, float[] functionValues)
    {
        float average = functionValues.Average();
        float variance = functionValues.Select(f => Mathf.Pow(f - average, 2)).Sum();
        return Mathf.Sqrt(variance) < Tolerance;
    }
}
