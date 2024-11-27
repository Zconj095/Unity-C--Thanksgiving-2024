using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Sampler-based Quantum Neural Network implementation in Unity.
/// This class simulates a neural network leveraging a parameterized quantum circuit.
/// </summary>
public class SamplerQNN : MonoBehaviour
{
    private int numInputs; // Number of input features
    private int numWeights; // Number of trainable weights
    private bool sparse; // Determines if the output is sparse
    private int[] outputShape; // Shape of the network output
    private Func<int, int> interpret; // Interpretation function for sampler output
    private float[] weights; // Trainable weights
    private Func<float[], float[], float[]> forwardFunction; // Function for forward computation
    private Func<float[], float[], (float[], float[])> backwardFunction; // Function for backward computation

    /// <summary>
    /// Initializes the SamplerQNN with a parameterized circuit and neural network specifications.
    /// </summary>
    public void Initialize(
        object quantumCircuit,
        int? numInputs = null,
        int? numWeights = null,
        bool sparseOutput = false,
        Func<int, int> interpretFunction = null,
        int[] customOutputShape = null
    )
    {
        // Set inputs and weights
        this.numInputs = numInputs ?? GetPropertyValue<int>(quantumCircuit, "NumInputs");
        this.numWeights = numWeights ?? GetPropertyValue<int>(quantumCircuit, "NumWeights");
        this.sparse = sparseOutput;

        // Set interpretation and output shape
        this.interpret = interpretFunction ?? (x => x);
        this.outputShape = customOutputShape ?? new[] { (int)Math.Pow(2, GetPropertyValue<int>(quantumCircuit, "NumQubits")) };

        // Set forward and backward computations
        this.forwardFunction = CreateMethodDelegate<float[], float[], float[]>(quantumCircuit, "Forward");
        this.backwardFunction = CreateMethodDelegate<float[], float[], (float[], float[])>(quantumCircuit, "Backward");

        // Initialize weights
        weights = new float[this.numWeights];
        RandomizeWeights();
    }

    /// <summary>
    /// Performs a forward pass on the quantum circuit using the provided input data and weights.
    /// </summary>
    public float[] Forward(float[] inputData)
    {
        ValidateInputs(inputData, weights);
        float[] rawOutput = forwardFunction(inputData, weights);
        return ProcessOutput(rawOutput, inputData.Length / numInputs);
    }

    /// <summary>
    /// Performs a backward pass to compute gradients with respect to inputs and weights.
    /// </summary>
    public (float[] inputGradients, float[] weightGradients) Backward(float[] inputData)
    {
        ValidateInputs(inputData, weights);
        var (inputGrad, weightGrad) = backwardFunction(inputData, weights);
        return (ReshapeGradients(inputGrad, inputData.Length / numInputs, numInputs),
                ReshapeGradients(weightGrad, inputData.Length / numInputs, numWeights));
    }

    /// <summary>
    /// Randomly initializes weights between -1 and 1.
    /// </summary>
    private void RandomizeWeights()
    {
        var random = new System.Random();
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = (float)(random.NextDouble() * 2 - 1);
        }
    }

    /// <summary>
    /// Validates the input data and weights for compatibility with the network.
    /// </summary>
    private void ValidateInputs(float[] inputData, float[] weights)
    {
        if (inputData.Length % numInputs != 0)
        {
            throw new ArgumentException("Input data length must be a multiple of the number of inputs.");
        }
        if (weights.Length != numWeights)
        {
            throw new ArgumentException("Weights length must match the number of trainable weights.");
        }
    }

    /// <summary>
    /// Processes raw output from the forward pass to match expected batch and output dimensions.
    /// </summary>
    private float[] ProcessOutput(float[] rawOutput, int batchSize)
    {
        int expectedElements = batchSize * outputShape.Aggregate(1, (a, b) => a * b);
        if (rawOutput.Length != expectedElements)
        {
            throw new ArgumentException("Raw output data does not match expected output dimensions.");
        }
        return rawOutput;
    }

    /// <summary>
    /// Reshapes gradients to match batch and parameter dimensions.
    /// </summary>
    private float[] ReshapeGradients(float[] gradients, int batchSize, int parameterCount)
    {
        if (gradients.Length != batchSize * parameterCount)
        {
            throw new ArgumentException("Gradient dimensions do not match batch size and parameter count.");
        }
        return gradients;
    }

    /// <summary>
    /// Dynamically retrieves a property value using reflection.
    /// </summary>
    private T GetPropertyValue<T>(object target, string propertyName)
    {
        PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
            throw new MissingMemberException($"Property '{propertyName}' not found on type '{target.GetType()}'.");
        }
        return (T)property.GetValue(target);
    }

    /// <summary>
    /// Dynamically creates a delegate for a method with two input parameters.
    /// </summary>
    private static Func<TInput1, TInput2, TOutput> CreateMethodDelegate<TInput1, TInput2, TOutput>(object target, string methodName)
    {
        MethodInfo method = target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        if (method == null)
        {
            throw new MissingMethodException($"Method '{methodName}' not found on type '{target.GetType()}'.");
        }
        return (input1, input2) => (TOutput)method.Invoke(target, new object[] { input1, input2 });
    }
}
