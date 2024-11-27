using System;
using UnityEngine;

public class NFTOptimizer
{
    public int MaxIterations { get; private set; }
    public int MaxFunctionEvaluations { get; private set; }
    public int ResetInterval { get; private set; }
    public float Epsilon { get; private set; }
    public bool Verbose { get; private set; }

    /// <summary>
    /// Initializes the NFT optimizer.
    /// </summary>
    /// <param name="maxIterations">Maximum number of iterations to perform.</param>
    /// <param name="maxFunctionEvaluations">Maximum number of function evaluations to perform.</param>
    /// <param name="resetInterval">Reset interval for recalculating z0.</param>
    /// <param name="epsilon">Small value to avoid division by zero.</param>
    /// <param name="verbose">Enable verbose logging for debugging purposes.</param>
    public NFTOptimizer(
        int maxIterations = 1000,
        int maxFunctionEvaluations = 1024,
        int resetInterval = 32,
        float epsilon = 1e-32f,
        bool verbose = false)
    {
        MaxIterations = maxIterations;
        MaxFunctionEvaluations = maxFunctionEvaluations;
        ResetInterval = resetInterval;
        Epsilon = epsilon;
        Verbose = verbose;
    }

    /// <summary>
    /// Minimizes the provided objective function.
    /// </summary>
    /// <param name="objectiveFunction">Objective function to minimize.</param>
    /// <param name="initialGuess">Initial guess for the parameters.</param>
    /// <returns>Tuple containing the optimized parameters and final function value.</returns>
    public (float[] OptimizedParams, float FinalValue) Minimize(
        Func<float[], float> objectiveFunction,
        float[] initialGuess)
    {
        float[] x = (float[])initialGuess.Clone();
        float? recycleZ0 = null;

        int nIterations = 0;
        int nFunctionCalls = 0;

        while (true)
        {
            int idx = nIterations % x.Length;

            // Reset Z0 at intervals
            if (ResetInterval > 0 && nIterations % ResetInterval == 0)
                recycleZ0 = null;

            float z0;
            if (recycleZ0.HasValue)
            {
                z0 = recycleZ0.Value;
            }
            else
            {
                z0 = objectiveFunction(x);
                nFunctionCalls++;
            }

            float[] p = (float[])x.Clone();
            p[idx] += Mathf.PI / 2;
            float z1 = objectiveFunction(p);
            nFunctionCalls++;

            p[idx] -= Mathf.PI; // Subtract π to get x - π/2
            float z3 = objectiveFunction(p);
            nFunctionCalls++;

            float z2 = z1 + z3 - z0;
            float c = (z1 + z3) / 2;
            float a = Mathf.Sqrt((z0 - z2) * (z0 - z2) + (z1 - z3) * (z1 - z3)) / 2;
            float b = Mathf.Atan((z1 - z3) / ((z0 - z2) + Epsilon * (z0 == z2 ? 1 : 0))) + x[idx];
            b += 0.5f * Mathf.PI + 0.5f * Mathf.PI * Mathf.Sign((z0 - z2) + Epsilon * (z0 == z2 ? 1 : 0));

            x[idx] = b;
            recycleZ0 = c - a;

            nIterations++;

            if (Verbose)
            {
                Debug.Log($"Iteration {nIterations}: x = [{string.Join(", ", x)}], f(x) = {z0}, Function Calls = {nFunctionCalls}");
            }

            if (nFunctionCalls >= MaxFunctionEvaluations || nIterations >= MaxIterations)
                break;
        }

        return (x, objectiveFunction(x));
    }
}
