using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmplitudeEstimation
{
    private int _numEvalQubits;
    private QuantumCircuit _statePreparation;
    private QuantumCircuit _groverOperator;
    private List<int> _objectiveQubits;
    private Func<float, float> _postProcessing;
    private QuantumInstance _quantumInstance;
    private QuantumCircuit _phaseEstimationCircuit;

    private Dictionary<string, object> _results;

    public AmplitudeEstimation(
        int numEvalQubits,
        QuantumCircuit statePreparation,
        QuantumCircuit groverOperator,
        List<int> objectiveQubits = null,
        Func<float, float> postProcessing = null,
        QuantumInstance quantumInstance = null)
    {
        if (numEvalQubits < 1)
        {
            throw new ArgumentException("Number of evaluation qubits must be at least 1.");
        }

        _numEvalQubits = numEvalQubits;
        _statePreparation = statePreparation ?? throw new ArgumentNullException(nameof(statePreparation), "State preparation circuit is required.");
        _groverOperator = groverOperator ?? throw new ArgumentNullException(nameof(groverOperator), "Grover operator circuit is required.");
        _objectiveQubits = objectiveQubits ?? new List<int>();
        _postProcessing = postProcessing ?? (x => x); // Default to identity function
        _quantumInstance = quantumInstance ?? new QuantumInstance();
        _results = new Dictionary<string, object>();
    }

    public Dictionary<string, object> Run()
    {
        // Construct the amplitude estimation circuit
        var circuit = ConstructCircuit(measurement: true);

        // Execute the circuit
        var executionResult = _quantumInstance.Execute(circuit);

        // Analyze results
        var probabilities = CalculateProbabilities(executionResult);
        var estimation = EstimateAmplitude(probabilities);

        // Save results
        _results["estimation"] = estimation;
        _results["probabilities"] = probabilities;

        return _results;
    }

    private QuantumCircuit ConstructCircuit(bool measurement)
    {
        Debug.Log("Constructing amplitude estimation circuit.");

        var circuit = new QuantumCircuit();

        // Add evaluation qubits
        circuit.AddQubits(_numEvalQubits);

        // Apply state preparation
        circuit.Compose(_statePreparation);

        // Apply the Grover operator in the phase estimation pattern
        for (int i = 0; i < _numEvalQubits; i++)
        {
            var power = (int)Math.Pow(2, i);
            circuit.Compose(_groverOperator.Power(power));
        }

        // Add measurements if requested
        if (measurement)
        {
            circuit.MeasureAll();
        }

        return circuit;
    }

    private Dictionary<string, float> CalculateProbabilities(Dictionary<string, int> counts)
    {
        Debug.Log("Calculating probabilities from execution results.");

        int totalShots = counts.Values.Sum();
        var probabilities = new Dictionary<string, float>();

        foreach (var kvp in counts)
        {
            probabilities[kvp.Key] = (float)kvp.Value / totalShots;
        }

        return probabilities;
    }

    private float EstimateAmplitude(Dictionary<string, float> probabilities)
    {
        Debug.Log("Estimating amplitude based on probabilities.");

        // Calculate the most probable measurement outcome
        var maxProbabilityState = probabilities.OrderByDescending(kvp => kvp.Value).First();
        var y = Convert.ToInt32(maxProbabilityState.Key, 2);

        // Map to [0, 1] range using phase estimation mapping
        float a = (float)Math.Pow(Math.Sin(y * Math.PI / Math.Pow(2, _numEvalQubits)), 2);
        return _postProcessing(a);
    }
}
