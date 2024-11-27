using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class FeatureVectorCircuit : MonoBehaviour
{
    private int featureDimension;
    private int numQubits;
    private float[] parameters;

    public FeatureVectorCircuit()
    {
        // Default constructor
    }

    /// <summary>
    /// Initializes the circuit with parameters using reflection.
    /// </summary>
    /// <param name="inputObject">Object containing the initialization data.</param>
    /// <param name="methodName">Method name for setting amplitudes dynamically.</param>
    public void Initialize(object inputObject, string methodName)
    {
        // Reflectively invoke the specified method on the input object
        MethodInfo method = inputObject.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        if (method == null)
        {
            throw new MissingMethodException($"Method {methodName} not found on type {inputObject.GetType()}.");
        }

        // Invoke the method to retrieve amplitudes
        float[] amplitudes = (float[])method.Invoke(inputObject, null);

        // Validate and set the feature dimension and parameters
        SetAmplitudes(amplitudes);
    }

    /// <summary>
    /// Sets the amplitudes, validates feature dimensions, and normalizes the input.
    /// </summary>
    /// <param name="amplitudes">Input amplitudes to set.</param>
    private void SetAmplitudes(float[] amplitudes)
    {
        if (!IsPowerOfTwo(amplitudes.Length))
        {
            throw new ArgumentException("Feature dimension must be a power of 2.");
        }

        featureDimension = amplitudes.Length;
        numQubits = Log2(featureDimension);

        float norm = Normalize(amplitudes);
        if (!Mathf.Approximately(norm, 1f))
        {
            throw new ArgumentException("Amplitudes must be normalized.");
        }

        parameters = amplitudes;
    }

    /// <summary>
    /// Displays the circuit configuration and parameters using Unity's Debug.Log.
    /// </summary>
    public void DisplayCircuit()
    {
        Debug.Log($"FeatureVectorCircuit with {numQubits} Qubits and {featureDimension} Feature Dimension:");
        Debug.Log($"Parameters: {string.Join(", ", parameters)}");
    }

    /// <summary>
    /// Returns the feature dimension.
    /// </summary>
    public int GetFeatureDimension()
    {
        return featureDimension;
    }

    /// <summary>
    /// Returns the number of qubits.
    /// </summary>
    public int GetNumQubits()
    {
        return numQubits;
    }

    /// <summary>
    /// Returns the normalized parameters.
    /// </summary>
    public float[] GetParameters()
    {
        return parameters;
    }

    /// <summary>
    /// Validates if a given number is a power of two.
    /// </summary>
    private bool IsPowerOfTwo(int value)
    {
        return (value & (value - 1)) == 0 && value > 0;
    }

    /// <summary>
    /// Calculates the base-2 logarithm of a given value.
    /// </summary>
    private static int Log2(int value)
    {
        return (int)(Math.Log(value) / Math.Log(2));
    }

    /// <summary>
    /// Normalizes the input array so that its L2 norm equals 1.
    /// </summary>
    /// <param name="input">Input array to normalize.</param>
    /// <returns>The computed norm of the array.</returns>
    private float Normalize(float[] input)
    {
        float norm = 0f;
        for (int i = 0; i < input.Length; i++)
        {
            norm += input[i] * input[i];
        }

        norm = Mathf.Sqrt(norm);

        for (int i = 0; i < input.Length; i++)
        {
            input[i] /= norm;
        }

        return norm;
    }
}
