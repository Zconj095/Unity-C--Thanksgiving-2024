using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Variational Quantum Circuit (VQC) class.
/// Simulates a quantum circuit that processes input data through a feature map and ansatz, 
/// computes loss, and trains weights using gradient descent.
/// </summary>
public class VQC : MonoBehaviour
{
    private int numQubits; // Number of qubits in the circuit
    private Func<float[], float[]> featureMap; // Feature map function
    private Func<float[], float[]> ansatz; // Ansatz function
    private Func<float[], float[]> circuit; // Full quantum circuit function
    private Func<float[], float, float> lossFunction; // Loss function
    private float[] weights; // Weights for the quantum circuit
    private Action<float[], float> callback; // Optional callback for monitoring training progress

    /// <summary>
    /// Initializes the VQC model with specified parameters and methods using reflection.
    /// </summary>
    /// <param name="model">Target object containing methods for feature mapping, ansatz, loss, and callback.</param>
    /// <param name="numQubits">Number of qubits (optional). Defaults to 2.</param>
    /// <param name="featureMapMethod">Name of the method for the feature map.</param>
    /// <param name="ansatzMethod">Name of the method for the ansatz.</param>
    /// <param name="lossFunctionMethod">Name of the method for computing loss.</param>
    /// <param name="callbackMethod">Optional name of the method for the callback.</param>
    public void Initialize(
        object model,
        int? numQubits = null,
        string featureMapMethod = "DefaultFeatureMap",
        string ansatzMethod = "DefaultAnsatz",
        string lossFunctionMethod = "DefaultLossFunction",
        string callbackMethod = null
    )
    {
        // Dynamically retrieve the specified methods using reflection
        featureMap = CreateSingleInputDelegate<float[], float[]>(model, featureMapMethod);
        ansatz = CreateSingleInputDelegate<float[], float[]>(model, ansatzMethod);
        lossFunction = CreateDoubleInputDelegate<float[], float, float>(model, lossFunctionMethod);
        callback = callbackMethod != null
            ? CreateVoidDelegate<float[], float>(model, callbackMethod)
            : null;

        // Set the number of qubits
        this.numQubits = numQubits ?? 2;

        // Construct the quantum circuit as a composition of the feature map and ansatz
        this.circuit = input =>
        {
            float[] mappedInput = featureMap(input);
            float[] ansatzOutput = ansatz(mappedInput);
            return ansatzOutput;
        };

        // Initialize weights
        weights = new float[this.numQubits];
        RandomizeWeights();
    }

    /// <summary>
    /// Randomly initializes weights between -1 and 1.
    /// </summary>
    private void RandomizeWeights()
    {
        var random = new System.Random();
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = (float)(random.NextDouble() * 2 - 1); // Values in range [-1, 1]
        }
    }

    /// <summary>
    /// Trains the VQC model using gradient descent.
    /// </summary>
    public void Train(float[,] X, float[,] y, int epochs, float learningRate)
    {
        if (X.GetLength(0) != y.GetLength(0))
        {
            throw new ArgumentException("Number of samples in X and y must match.");
        }

        for (int epoch = 0; epoch < epochs; epoch++)
        {
            float epochLoss = 0f;

            for (int i = 0; i < X.GetLength(0); i++)
            {
                float[] input = GetRow(X, i);
                float[] target = GetRow(y, i);
                float[] output = PredictSingle(input);

                // Compute the loss for this sample
                epochLoss += lossFunction(output, target[0]);

                // Update weights using gradient descent
                for (int j = 0; j < weights.Length; j++)
                {
                    weights[j] -= learningRate * (output[j] - target[j]) * input[j];
                }
            }

            // Invoke the callback after each epoch, if provided
            callback?.Invoke(weights, epochLoss / X.GetLength(0)); // Average loss
        }
    }

    /// <summary>
    /// Predicts the output for a single input sample.
    /// </summary>
    public float[] PredictSingle(float[] input)
    {
        float[] circuitOutput = circuit(input);

        // Apply softmax transformation to obtain probabilities
        return Softmax(circuitOutput);
    }

    /// <summary>
    /// Computes the softmax transformation for a vector.
    /// </summary>
    private float[] Softmax(float[] input)
    {
        float maxVal = input.Max(); // Stabilize computation by subtracting the max value
        float[] expInput = input.Select(x => Mathf.Exp(x - maxVal)).ToArray();
        float sumExp = expInput.Sum();
        return expInput.Select(x => x / sumExp).ToArray();
    }

    /// <summary>
    /// Retrieves a specific row from a 2D array.
    /// </summary>
    private float[] GetRow(float[,] matrix, int row)
    {
        int cols = matrix.GetLength(1);
        float[] rowData = new float[cols];
        for (int i = 0; i < cols; i++)
        {
            rowData[i] = matrix[row, i];
        }
        return rowData;
    }

    /// <summary>
    /// Dynamically creates a delegate for a method with one input parameter.
    /// </summary>
    private static Func<TInput, TOutput> CreateSingleInputDelegate<TInput, TOutput>(object target, string methodName)
    {
        MethodInfo method = target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        if (method == null)
        {
            throw new MissingMethodException($"Method {methodName} not found on type {target.GetType()}.");
        }

        return input => (TOutput)method.Invoke(target, new object[] { input });
    }

    /// <summary>
    /// Dynamically creates a delegate for a method with two input parameters.
    /// </summary>
    private static Func<TInput1, TInput2, TOutput> CreateDoubleInputDelegate<TInput1, TInput2, TOutput>(object target, string methodName)
    {
        MethodInfo method = target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        if (method == null)
        {
            throw new MissingMethodException($"Method {methodName} not found on type {target.GetType()}.");
        }

        return (input1, input2) => (TOutput)method.Invoke(target, new object[] { input1, input2 });
    }

    /// <summary>
    /// Dynamically creates a delegate for a void method with two input parameters.
    /// </summary>
    private static Action<TInput1, TInput2> CreateVoidDelegate<TInput1, TInput2>(object target, string methodName)
    {
        MethodInfo method = target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        if (method == null)
        {
            throw new MissingMethodException($"Method {methodName} not found on type {target.GetType()}.");
        }

        return (input1, input2) => method.Invoke(target, new object[] { input1, input2 });
    }
}
