using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Abstract Neural Network class for Unity.
/// Provides forward and backward passes for batched inputs. To be extended by specific (quantum) neural networks.
/// </summary>
public abstract class NeuralNetworkSystem : MonoBehaviour
{
    private int numInputs; // Number of input features
    private int numWeights; // Number of trainable weights
    private bool sparse; // Determines if the output is sparse
    private int[] outputShape; // Shape of the output
    private bool inputGradients; // Whether to compute gradients with respect to inputs

    /// <summary>
    /// Initializes the NeuralNetwork with input, weight, and output specifications.
    /// </summary>
    public void Initialize(
        int numInputs,
        int numWeights,
        bool sparse,
        int[] outputShape,
        bool inputGradients = false
    )
    {
        if (numInputs < 0) throw new ArgumentException("Number of inputs cannot be negative.");
        if (numWeights < 0) throw new ArgumentException("Number of weights cannot be negative.");
        if (outputShape == null || outputShape.Length == 0 || outputShape.Any(x => x <= 0))
        {
            throw new ArgumentException("Output shape must be a positive integer array.");
        }

        this.numInputs = numInputs;
        this.numWeights = numWeights;
        this.sparse = sparse;
        this.outputShape = outputShape;
        this.inputGradients = inputGradients;
    }

    /// <summary>
    /// Executes the forward pass for the neural network.
    /// </summary>
    public float[] Forward(float[] inputData, float[] weights)
    {
        ValidateInputs(inputData, weights);
        var outputData = PerformForward(inputData, weights);
        return ReshapeOutput(outputData, inputData.Length / numInputs);
    }

    /// <summary>
    /// Executes the backward pass for the neural network, calculating gradients.
    /// </summary>
    public (float[] inputGradients, float[] weightGradients) Backward(float[] inputData, float[] weights)
    {
        ValidateInputs(inputData, weights);
        var (inputGrad, weightGrad) = PerformBackward(inputData, weights);

        var reshapedInputGrad = ReshapeGradients(inputGrad, inputData.Length / numInputs, numInputs);
        var reshapedWeightGrad = ReshapeGradients(weightGrad, inputData.Length / numInputs, numWeights);

        return (reshapedInputGrad, reshapedWeightGrad);
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
    /// Reshapes the forward pass output to match the expected batch and output dimensions.
    /// </summary>
    private float[] ReshapeOutput(float[] outputData, int batchSize)
    {
        if (batchSize == 1) return outputData;

        int totalOutputElements = batchSize * outputShape.Aggregate(1, (a, b) => a * b);
        if (outputData.Length != totalOutputElements)
        {
            throw new ArgumentException("Output data does not match the expected output shape.");
        }
        return outputData;
    }

    /// <summary>
    /// Reshapes gradients to match batch and respective parameter dimensions.
    /// </summary>
    private float[] ReshapeGradients(float[] gradients, int batchSize, int parameterCount)
    {
        if (gradients.Length != batchSize * parameterCount)
        {
            throw new ArgumentException("Gradient dimensions do not match expected batch size and parameter count.");
        }
        return gradients;
    }

    /// <summary>
    /// Abstract method for performing the forward pass.
    /// Must be implemented by derived classes.
    /// </summary>
    protected abstract float[] PerformForward(float[] inputData, float[] weights);

    /// <summary>
    /// Abstract method for performing the backward pass.
    /// Must be implemented by derived classes.
    /// </summary>
    protected abstract (float[] inputGrad, float[] weightGrad) PerformBackward(float[] inputData, float[] weights);

    /// <summary>
    /// Dynamically retrieves a property value using Unity reflection.
    /// </summary>
    protected T GetPropertyValue<T>(object target, string propertyName)
    {
        var property = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
            throw new MissingMemberException($"Property '{propertyName}' not found on type '{target.GetType()}'.");
        }
        return (T)property.GetValue(target);
    }

    /// <summary>
    /// Dynamically invokes a method using Unity reflection.
    /// </summary>
    protected T InvokeMethod<T>(object target, string methodName, params object[] args)
    {
        var method = target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        if (method == null)
        {
            throw new MissingMethodException($"Method '{methodName}' not found on type '{target.GetType()}'.");
        }
        return (T)method.Invoke(target, args);
    }
}
