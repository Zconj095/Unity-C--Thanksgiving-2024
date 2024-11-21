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

        Debug.Log("QuantumFrameworkManager: QuantumCircuit initialized successfully.");

    }
}
