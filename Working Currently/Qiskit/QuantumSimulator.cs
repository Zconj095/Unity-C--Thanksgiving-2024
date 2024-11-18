using UnityEngine;
using System.Collections.Generic;

public class QuantumSimulator : MonoBehaviour
{
    public QuantumCircuit Circuit { get; private set; }
    protected QuantumVisualizer visualizer;
    protected QuantumState state; // Changed from private to protected
    private List<NoiseModel> noiseModels;

    public QuantumSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)
    {
        if (circuit == null)
        {
            Debug.LogError("QuantumSimulator: QuantumCircuit is null in constructor.");
            throw new System.ArgumentNullException(nameof(circuit));
        }
        if (visualizer == null)
        {
            Debug.LogError("QuantumSimulator: QuantumVisualizer is null in constructor.");
            throw new System.ArgumentNullException(nameof(visualizer));
        }

        this.Circuit = circuit;
        this.visualizer = visualizer;
        this.state = new QuantumState(circuit.NumQubits);
        this.noiseModels = new List<NoiseModel>();

        Debug.Log($"QuantumSimulator initialized with {circuit.NumQubits} qubits.");
    }

    public void AddNoiseModel(NoiseModel noiseModel)
    {
        noiseModels.Add(noiseModel);
    }

    public virtual void Simulate()
    {
        foreach (var gate in Circuit.Gates)
        {
            Debug.Log($"Applying {gate.Name}");
            state.ApplyGate(gate);

            foreach (var noise in noiseModels)
            {
                noise.ApplyNoise(state);
            }

            visualizer.VisualizeGate(gate);
        }

        Debug.Log("Simulation complete.");
    }
}
