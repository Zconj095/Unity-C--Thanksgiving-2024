using UnityEngine;

public class QuantumTestSuite : MonoBehaviour
{
    void Start()
    {
        Debug.Log("QuantumTestSuite: Starting test...");

        // Create and initialize QuantumCircuit
        GameObject circuitObject = new GameObject("QuantumCircuit");
        QuantumCircuit circuit = circuitObject.AddComponent<QuantumCircuit>();
        circuit.NumQubits = 1;

        if (circuit == null)
        {
            Debug.LogError("QuantumTestSuite: QuantumCircuit failed to initialize.");
            return;
        }
        Debug.Log("QuantumTestSuite: QuantumCircuit initialized successfully.");

        // Find QuantumVisualizer in the scene
        QuantumVisualizer visualizer = FindObjectOfType<QuantumVisualizer>();
        if (visualizer == null)
        {
            Debug.LogError("QuantumTestSuite: QuantumVisualizer not found.");
            return;
        }
        Debug.Log("QuantumTestSuite: QuantumVisualizer found successfully.");

        try
        {
            // Initialize QuantumSimulator
            QuantumSimulator simulator = new QuantumSimulator(circuit, visualizer);
            Debug.Log("QuantumTestSuite: QuantumSimulator initialized successfully.");

            // Add gates and simulate
            circuit.AddGate(QuantumGate.Hadamard(0));
            simulator.Simulate();
            Debug.Log("QuantumTestSuite: Simulation completed successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"QuantumTestSuite: Exception during QuantumSimulator setup - {ex.Message}");
        }
    }
}
