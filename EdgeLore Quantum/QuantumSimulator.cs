using UnityEngine;
using System.Collections.Generic;

public class QuantumSimulator : MonoBehaviour
{
    public QuantumCircuit Circuit { get; private set; }
    protected QuantumVisualizer Visualizer => visualizer; // Protected property for derived classes
    private QuantumVisualizer visualizer;
    private QuantumState state;
    private List<NoiseModel> noiseModels;

    protected QuantumState State => state; // Protected access to state

    public QuantumSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)
    {
        Circuit = circuit ?? throw new System.ArgumentNullException(nameof(circuit));
        this.visualizer = visualizer ?? throw new System.ArgumentNullException(nameof(visualizer));
        state = new QuantumState(circuit.NumQubits);
        noiseModels = new List<NoiseModel>();
    }

    public void Simulate()
    {
        Debug.Log("Simulating circuit...");
        foreach (var gate in Circuit.Gates)
        {
            Debug.Log($"Applying gate: {gate.Name}");
            visualizer.VisualizeGate(gate);
        }
        Debug.Log("Simulation complete.");
    }

    public void AddNoiseModel(NoiseModel noiseModel)
    {
        noiseModels.Add(noiseModel);
    }
}
