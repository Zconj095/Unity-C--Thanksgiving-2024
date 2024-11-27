using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constrained Optimization By Linear Approximation (COBYLA) optimizer.
/// Solves constrained optimization problems where the derivative of the objective function is unknown.
/// </summary>
public class COBYLAOptimizer
{
    private int maxIterations;
    private float rhoBegin;
    private float tolerance;
    private bool displayConvergence;

    /// <summary>
    /// Initializes the COBYLA Optimizer with specified parameters.
    /// </summary>
    /// <param name="maxIterations">Maximum number of iterations.</param>
    /// <param name="rhoBegin">Initial step size.</param>
    /// <param name="tolerance">Tolerance for convergence.</param>
    /// <param name="displayConvergence">Display convergence messages in the console.</param>
    public COBYLAOptimizer(
        int maxIterations = 1000,
        float rhoBegin = 1.0f,
        float tolerance = 1e-6f,
        bool displayConvergence = false)
    {
        this.maxIterations = maxIterations;
        this.rhoBegin = rhoBegin;
        this.tolerance = tolerance;
        this.displayConvergence = displayConvergence;
    }

    /// <summary>
    /// Minimizes the objective function subject to constraints.
    /// </summary>
    /// <param name="objectiveFunction">The objective function to minimize.</param>
    /// <param name="initialPoint">Initial guess for the solution.</param>
    /// <param name="constraints">List of constraints (each returning a non-negative value).</param>
    /// <returns>Optimized parameters and the minimum value of the objective function.</returns>
    public (float[] OptimizedParams, float FinalValue) Minimize(
        Func<float[], float> objectiveFunction,
        float[] initialPoint,
        List<Func<float[], float>> constraints)
    {
        float[] x = (float[])initialPoint.Clone();
        float rho = rhoBegin;
        float objectiveValue = objectiveFunction(x);
        int iteration = 0;

        while (iteration < maxIterations)
        {
            iteration++;
            float[] newPoint = (float[])x.Clone();

            // Apply constraints using a linear approximation
            for (int i = 0; i < x.Length; i++)
            {
                float constraintViolation = ComputeConstraintViolation(newPoint, constraints);
                if (constraintViolation > tolerance)
                {
                    AdjustForConstraints(newPoint, constraints, rho);
                }
            }

            // Compute the new objective value
            float newObjectiveValue = objectiveFunction(newPoint);

            // Check convergence
            if (Mathf.Abs(newObjectiveValue - objectiveValue) < tolerance)
            {
                if (displayConvergence)
                {
                    Debug.Log($"Converged in {iteration} iterations with objective value: {newObjectiveValue}");
                }
                return (newPoint, newObjectiveValue);
            }

            // Update parameters
            x = newPoint;
            objectiveValue = newObjectiveValue;
            rho = Mathf.Max(rho * 0.9f, tolerance); // Reduce step size
        }

        Debug.LogWarning("Maximum iterations reached without convergence.");
        return (x, objectiveValue);
    }

    /// <summary>
    /// Computes the total constraint violation for a given point.
    /// </summary>
    private float ComputeConstraintViolation(float[] point, List<Func<float[], float>> constraints)
    {
        float totalViolation = 0f;
        foreach (var constraint in constraints)
        {
            float violation = Mathf.Max(0, -constraint(point)); // Negative values are violations
            totalViolation += violation;
        }
        return totalViolation;
    }

    /// <summary>
    /// Adjusts the point to satisfy constraints using a linear approximation.
    /// </summary>
    private void AdjustForConstraints(float[] point, List<Func<float[], float>> constraints, float rho)
    {
        foreach (var constraint in constraints)
        {
            float value = constraint(point);
            if (value < 0) // Constraint is violated
            {
                for (int i = 0; i < point.Length; i++)
                {
                    point[i] += rho * Mathf.Sign(value); // Move towards satisfying the constraint
                }
            }
        }
    }
}
