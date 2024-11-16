using System;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeEstimationAlgorithm
{
    private QuantumCircuit _statePreparation;
    private QuantumCircuit _groverOperator;
    private List<int> _objectiveQubits;
    private Func<float, float> _postProcessing;
    private QuantumInstance _quantumInstance;

    public AmplitudeEstimationAlgorithm(
        QuantumCircuit statePreparation,
        QuantumCircuit groverOperator,
        List<int> objectiveQubits = null,
        Func<float, float> postProcessing = null,
        QuantumInstance quantumInstance = null)
    {
        _statePreparation = statePreparation ?? throw new ArgumentNullException(nameof(statePreparation), "State preparation circuit is required.");
        _groverOperator = groverOperator ?? throw new ArgumentNullException(nameof(groverOperator), "Grover operator circuit is required.");
        _objectiveQubits = objectiveQubits ?? new List<int>();
        _postProcessing = postProcessing ?? (x => x); // Default to identity mapping
        _quantumInstance = quantumInstance ?? new QuantumInstance();
    }

    public bool IsGoodState(string measurement)
    {
        if (_objectiveQubits == null || _objectiveQubits.Count == 0)
        {
            throw new InvalidOperationException("Objective qubits are not set.");
        }

        foreach (var qubitIndex in _objectiveQubits)
        {
            if (measurement[measurement.Length - 1 - qubitIndex] != '1') // Note: index from the end
            {
                return false;
            }
        }
        return true;
    }

    public float PostProcessing(float value)
    {
        return _postProcessing(value);
    }

    public Dictionary<string, object> Run()
    {
        Debug.Log("Running amplitude estimation algorithm.");

        var results = new Dictionary<string, object>();

        var circuit = ConstructCircuit();
        var executionResult = _quantumInstance.Execute(circuit);
        var probabilities = CalculateProbabilities(executionResult);

        var aEstimation = EstimateAmplitude(probabilities);
        var estimation = PostProcessing(aEstimation);

        results["aEstimation"] = aEstimation;
        results["estimation"] = estimation;
        results["probabilities"] = probabilities;

        return results;
    }

    private QuantumCircuit ConstructCircuit()
    {
        Debug.Log("Constructing amplitude estimation circuit.");

        var circuit = new QuantumCircuit();

        // Add state preparation
        circuit.Compose(_statePreparation);

        // Add the Grover operator
        circuit.Compose(_groverOperator);

        // Add measurements
        circuit.MeasureAll();

        return circuit;
    }

    private Dictionary<string, float> CalculateProbabilities(Dictionary<string, int> counts)
    {
        int totalCounts = 0;
        foreach (var count in counts.Values)
        {
            totalCounts += count;
        }

        var probabilities = new Dictionary<string, float>();
        foreach (var kvp in counts)
        {
            probabilities[kvp.Key] = (float)kvp.Value / totalCounts;
        }

        return probabilities;
    }

    private float EstimateAmplitude(Dictionary<string, float> probabilities)
    {
        Debug.Log("Estimating amplitude.");

        var maxProbability = float.MinValue;
        var bestState = "";

        foreach (var kvp in probabilities)
        {
            if (kvp.Value > maxProbability)
            {
                maxProbability = kvp.Value;
                bestState = kvp.Key;
            }
        }

        int y = Convert.ToInt32(bestState, 2);
        return (float)Math.Pow(Math.Sin(y * Math.PI / (1 << _statePreparation.QubitCount)), 2);
    }
}
