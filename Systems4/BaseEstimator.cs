using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEstimator
{
    // Properties
    public string Name { get; set; } // Name of the estimator
    public int MaxIterations { get; set; } // Maximum number of iterations for optimization
    public float LearningRate { get; set; } // Learning rate for gradient updates

    // Constructor
    public BaseEstimator(string name, int maxIterations, float learningRate)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), "Estimator name cannot be null or empty.");
        }

        if (maxIterations <= 0)
        {
            throw new ArgumentException("MaxIterations must be a positive number.");
        }

        if (learningRate <= 0)
        {
            throw new ArgumentException("LearningRate must be a positive number.");
        }

        Name = name;
        MaxIterations = maxIterations;
        LearningRate = learningRate;
    }

    // Method to compute loss given parameters and data
    public float ComputeLoss(List<float> parameters, List<float> data)
    {
        if (parameters == null || data == null)
        {
            throw new ArgumentNullException("Parameters and data cannot be null.");
        }

        // Placeholder for a typical loss function, e.g., Mean Squared Error
        float loss = 0.0f;
        for (int i = 0; i < data.Count; i++)
        {
            float prediction = parameters[0] * data[i]; // Simple linear model: prediction = weight * input
            loss += (prediction - data[i]) * (prediction - data[i]);
        }

        return loss / data.Count; // Mean loss
    }

    // Method to compute gradients
    public List<float> ComputeGradients(List<float> parameters, List<float> data)
    {
        if (parameters == null || data == null)
        {
            throw new ArgumentNullException("Parameters and data cannot be null.");
        }

        var gradients = new List<float>();
        foreach (var param in parameters)
        {
            float gradient = 0.0f;

            // Placeholder gradient computation (example: derivative of loss wrt parameter)
            foreach (var input in data)
            {
                float prediction = param * input;
                gradient += 2 * (prediction - input) * input; // Example: Gradient of MSE for a linear model
            }

            gradients.Add(gradient / data.Count); // Normalize by the number of data points
        }

        return gradients;
    }

    // Method to update parameters using gradients
    public List<float> UpdateParameters(List<float> parameters, List<float> gradients)
    {
        if (parameters == null || gradients == null || parameters.Count != gradients.Count)
        {
            throw new ArgumentException("Parameters and gradients must be non-null and have the same length.");
        }

        for (int i = 0; i < parameters.Count; i++)
        {
            parameters[i] -= LearningRate * gradients[i]; // Gradient descent update rule
        }

        return parameters;
    }

    // Method to perform optimization
    public List<float> Optimize(List<float> parameters, List<float> data)
    {
        if (parameters == null || data == null)
        {
            throw new ArgumentNullException("Parameters and data cannot be null.");
        }

        for (int iteration = 0; iteration < MaxIterations; iteration++)
        {
            var gradients = ComputeGradients(parameters, data);
            parameters = UpdateParameters(parameters, gradients);

            float loss = ComputeLoss(parameters, data);
            Console.WriteLine($"Iteration {iteration + 1}/{MaxIterations}, Loss: {loss}");

            if (loss < 1e-6) // Convergence threshold
            {
                break;
            }
        }

        return parameters;
    }
}
