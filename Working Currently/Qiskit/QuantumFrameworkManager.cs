using UnityEngine;

public class QuantumFrameworkManager : MonoBehaviour
{
    public QuantumVisualizer visualizer; // Assign via Unity Inspector

    void Start()
    {
        Debug.Log("QuantumFrameworkManager: Starting...");

        if (visualizer == null)
        {
            Debug.LogError("QuantumFrameworkManager: QuantumVisualizer is not assigned.");
            return;
        }

        // Create and initialize QuantumCircuit
        GameObject circuitObject = new GameObject("QuantumCircuit");
        QuantumCircuit circuit = circuitObject.AddComponent<QuantumCircuit>();
        circuit.NumQubits = 2;

        if (circuit == null)
        {
            Debug.LogError("QuantumFrameworkManager: QuantumCircuit failed to initialize.");
            return;
        }
        Debug.Log("QuantumFrameworkManager: QuantumCircuit initialized successfully.");

        try
        {
            // Initialize EnhancedPulseSimulator
            EnhancedPulseSimulator simulator = new EnhancedPulseSimulator(circuit, visualizer);
            Debug.Log("QuantumFrameworkManager: EnhancedPulseSimulator initialized successfully.");

            // Add noise models
            simulator.AddNoiseModel(new DepolarizingNoise(0.1));
            simulator.AddNoiseModel(new AmplitudeDampingNoise(0.05));
            Debug.Log("QuantumFrameworkManager: Noise models added successfully.");

            // Add gates and simulate
            circuit.AddGate(QuantumGate.Hadamard(0));
            circuit.AddGate(QuantumGate.CNOT(0, 1));
            simulator.Simulate();
            Debug.Log("QuantumFrameworkManager: Simulation completed successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"QuantumFrameworkManager: Exception during EnhancedPulseSimulator setup - {ex.Message}");
        }
    }
}
