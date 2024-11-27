using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Conjugate Gradient (CG) Optimizer for solving optimization problems iteratively.
/// </summary>
public class ConjugateGradientOptimizer
{
    private int maxIterations;
    private float gradientTolerance;
    private float epsilon;
    private bool displayConvergence;

    /// <summary>
    /// Constructor to initialize the Conjugate Gradient Optimizer.
    /// </summary>
    /// <param name="maxIterations">Maximum number of iterations to perform.</param>
    /// <param name="gradientTolerance">Gradient norm tolerance for convergence.</param>
    /// <param name="epsilon">Step size for approximated gradient calculations.</param>
    /// <param name="displayConvergence">Display convergence messages in the console.</param>
    public ConjugateGradientOptimizer(
        int maxIterations = 20,
        float gradientTolerance = 1e-5f,
        float epsilon = 1.49e-8f,
        bool displayConvergence = false)
    {
        this.maxIterations = maxIterations;
        this.gradientTolerance = gradientTolerance;
        this.epsilon = epsilon;
        this.displayConvergence = displayConvergence;
    }

    /// <summary>
    /// Minimizes the objective function using the Conjugate Gradient method.
    /// </summary>
    /// <param name="objectiveFunction">The objective function to minimize.</param>
    /// <param name="initialPoint">Initial guess for the solution.</param>
    /// <param name="gradientFunction">Gradient function for the objective (optional).</param>
    /// <returns>Optimized parameters and the minimum value of the objective function.</returns>
    public (float[] OptimizedParams, float FinalValue) Minimize(
        Func<float[], float> objectiveFunction,
        float[] initialPoint,
        Func<float[], float[]> gradientFunction = null)
    {
        float[] x = (float[])initialPoint.Clone();
        float[] gradient = gradientFunction != null ? gradientFunction(x) : ComputeNumericalGradient(objectiveFunction, x);
        float[] direction = NegateVector(gradient);

        int iteration = 0;
        float gradientNorm = Norm(gradient);

        while (gradientNorm > gradientTolerance && iteration < maxIterations)
        {
            // Line search to find optimal step size (alpha)
            float alpha = LineSearch(objectiveFunction, x, direction);

            // Update parameters
            for (int i = 0; i < x.Length; i++)
            {
                x[i] += alpha * direction[i];
            }

            // Compute new gradient
            float[] newGradient = gradientFunction != null ? gradientFunction(x) : ComputeNumericalGradient(objectiveFunction, x);

            // Conjugate gradient update
            float beta = DotProduct(newGradient, newGradient) / DotProduct(gradient, gradient);
            float[] newDirection = AddVectors(NegateVector(newGradient), ScaleVector(direction, beta));

            // Update state
            gradient = newGradient;
            direction = newDirection;
            gradientNorm = Norm(gradient);
            iteration++;

            if (displayConvergence)
            {
                Debug.Log($"Iteration {iteration}: Gradient Norm = {gradientNorm}");
            }
        }

        float finalValue = objectiveFunction(x);
        return (x, finalValue);
    }

    /// <summary>
    /// Performs a simple line search to find the optimal step size (alpha).
    /// </summary>
    private float LineSearch(Func<float[], float> objectiveFunction, float[] x, float[] direction)
    {
        float alpha = 1.0f;
        float c = 1e-4f;
        float rho = 0.9f;

        float initialObjective = objectiveFunction(x);
        float[] newX = (float[])x.Clone();

        // Backtracking line search
        while (true)
        {
            for (int i = 0; i < x.Length; i++)
            {
                newX[i] = x[i] + alpha * direction[i];
            }

            if (objectiveFunction(newX) <= initialObjective + c * alpha * DotProduct(direction, direction))
            {
                break;
            }

            alpha *= rho;
        }

        return alpha;
    }

    /// <summary>
    /// Computes the numerical gradient of the objective function.
    /// </summary>
    private float[] ComputeNumericalGradient(Func<float[], float> objectiveFunction, float[] x)
    {
        float[] gradient = new float[x.Length];
        float[] xPlus = (float[])x.Clone();
        float[] xMinus = (float[])x.Clone();

        for (int i = 0; i < x.Length; i++)
        {
            xPlus[i] += epsilon;
            xMinus[i] -= epsilon;

            gradient[i] = (objectiveFunction(xPlus) - objectiveFunction(xMinus)) / (2 * epsilon);

            xPlus[i] = x[i];
            xMinus[i] = x[i];
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
    /// Computes the dot product of two vectors.
    /// </summary>
    private static float DotProduct(float[] vector1, float[] vector2)
    {
        float sum = 0f;
        for (int i = 0; i < vector1.Length; i++)
        {
            sum += vector1[i] * vector2[i];
        }
        return sum;
    }

    /// <summary>
    /// Negates a vector.
    /// </summary>
    private static float[] NegateVector(float[] vector)
    {
        float[] result = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            result[i] = -vector[i];
        }
        return result;
    }

    /// <summary>
    /// Scales a vector by a scalar.
    /// </summary>
    private static float[] ScaleVector(float[] vector, float scalar)
    {
        float[] result = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            result[i] = vector[i] * scalar;
        }
        return result;
    }

    /// <summary>
    /// Adds two vectors element-wise.
    /// </summary>
    private static float[] AddVectors(float[] vector1, float[] vector2)
    {
        float[] result = new float[vector1.Length];
        for (int i = 0; i < vector1.Length; i++)
        {
            result[i] = vector1[i] + vector2[i];
        }
        return result;
    }
}
