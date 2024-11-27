using System;
using System.Collections.Generic;
using UnityEngine;

public class QNNCircuit
{
    private int numQubits;
    private Func<float[], float[]> featureMap;
    private Func<float[], float[]> ansatz;
    private Func<float[], float[]> circuit;

    public QNNCircuit(int? numQubits = null, Func<float[], float[]> featureMap = null, Func<float[], float[]> ansatz = null)
    {
        // Derive number of qubits
        this.numQubits = numQubits ?? 2;

        // Assign feature map and ansatz, using defaults if necessary
        this.featureMap = featureMap ?? DefaultFeatureMap;
        this.ansatz = ansatz ?? DefaultAnsatz;

        // Compose the feature map and ansatz into a single circuit
        ComposeCircuit();
    }

    private void ComposeCircuit()
    {
        this.circuit = input =>
        {
            float[] mappedInput = featureMap(input);
            return ansatz(mappedInput);
        };
    }

    public void UpdateNumQubits(int numQubits)
    {
        if (numQubits <= 0)
        {
            throw new ArgumentException("Number of qubits must be a positive integer.");
        }

        this.numQubits = numQubits;
        // Adjust the feature map and ansatz to the new number of qubits
        this.featureMap = DefaultFeatureMap;
        this.ansatz = DefaultAnsatz;

        ComposeCircuit();
    }

    public void UpdateFeatureMap(Func<float[], float[]> newFeatureMap)
    {
        if (newFeatureMap == null)
        {
            throw new ArgumentNullException(nameof(newFeatureMap), "Feature map cannot be null.");
        }

        this.featureMap = newFeatureMap;

        // Adjust the ansatz to match the feature map's qubit configuration
        ComposeCircuit();
    }

    public void UpdateAnsatz(Func<float[], float[]> newAnsatz)
    {
        if (newAnsatz == null)
        {
            throw new ArgumentNullException(nameof(newAnsatz), "Ansatz cannot be null.");
        }

        this.ansatz = newAnsatz;

        // Adjust the feature map to match the ansatz's qubit configuration
        ComposeCircuit();
    }

    public float[] Evaluate(float[] input)
    {
        if (input.Length != numQubits)
        {
            throw new ArgumentException($"Input size must match the number of qubits ({numQubits}).");
        }

        return circuit(input);
    }

    public int GetNumQubits()
    {
        return numQubits;
    }

    private float[] DefaultFeatureMap(float[] input)
    {
        // Example: Identity mapping
        return input;
    }

    private float[] DefaultAnsatz(float[] input)
    {
        // Example: Sine transformation
        float[] output = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            output[i] = Mathf.Sin(input[i]);
        }
        return output;
    }

    public int NumInputParameters => numQubits; // Example mapping for feature map parameters

    public int NumWeightParameters => numQubits; // Example mapping for ansatz parameters
}
