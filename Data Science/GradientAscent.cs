using UnityEngine;
using System;

public class GradientAscent : MonoBehaviour
{
    [Header("Gradient Ascent Settings")]
    [SerializeField] private float learningRate = 0.1f;  // Step size
    [SerializeField] private int iterations = 100;       // Number of iterations
    [SerializeField] private float startingPoint = -5.0f; // Initial guess

    // The function to maximize
    private Func<float, float> objectiveFunction;

    private void Start()
    {
        // Define the objective function: f(x) = -(x^2 - 4x + 3) (We aim to maximize this function)
        objectiveFunction = (x) => -(x * x - 4 * x + 3);

        // Perform Gradient Ascent
        float maxPoint = PerformGradientAscent(objectiveFunction, startingPoint, learningRate, iterations);

        // Output the result
        Debug.Log($"The point of maximum value is: {maxPoint}");
        Debug.Log($"The maximum value is: {objectiveFunction(maxPoint)}");
    }

    /// <summary>
    /// Performs Gradient Ascent to find the maximum point of a given function.
    /// </summary>
    /// <param name="func">The function to maximize.</param>
    /// <param name="startPoint">The starting point for the ascent.</param>
    /// <param name="learningRate">The step size for the ascent.</param>
    /// <param name="iterations">The number of iterations to perform.</param>
    /// <returns>The point of maximum value.</returns>
    private float PerformGradientAscent(Func<float, float> func, float startPoint, float learningRate, int iterations)
    {
        float currentPoint = startPoint;

        for (int i = 0; i < iterations; i++)
        {
            // Compute the gradient (derivative of the function)
            float gradient = ComputeGradient(func, currentPoint);

            // Update the current point
            currentPoint += learningRate * gradient;

            // Optionally, output the current point and its value
            Debug.Log($"Iteration {i + 1}: x = {currentPoint:F4}, f(x) = {func(currentPoint):F4}");
        }

        return currentPoint;
    }

    /// <summary>
    /// Computes the gradient of the function using numerical differentiation.
    /// </summary>
    /// <param name="func">The function to differentiate.</param>
    /// <param name="point">The point at which to compute the gradient.</param>
    /// <returns>The computed gradient.</returns>
    private float ComputeGradient(Func<float, float> func, float point)
    {
        float h = 0.0001f; // A small delta for numerical differentiation
        return (func(point + h) - func(point)) / h;
    }
}
