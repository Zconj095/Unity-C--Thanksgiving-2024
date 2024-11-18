using UnityEngine;

public class QuantumVisualizer : MonoBehaviour
{
    public GameObject QubitPrefab;
    public GameObject GatePrefab;

    private GameObject[] qubits;

    public void InitializeQubits(int numQubits)
    {
        qubits = new GameObject[numQubits];
        for (int i = 0; i < numQubits; i++)
        {
            Vector3 position = new Vector3(i * 2.0f, 0, 0);
            qubits[i] = Instantiate(QubitPrefab, position, Quaternion.identity);
            qubits[i].name = $"Qubit {i}";
        }
    }

    public void VisualizePulse(double amplitude, double frequency, double duration)
    {
        Debug.Log($"Visualizing Pulse: Amplitude={amplitude}, Frequency={frequency}, Duration={duration}");
        // Add actual visualization logic here
    }
    public void VisualizeGate(QuantumGate gate)
    {
        Vector3 position = Vector3.zero;
        foreach (int qubit in gate.Qubits)
        {
            position += qubits[qubit].transform.position;
        }
        position /= gate.Qubits.Length;

        Instantiate(GatePrefab, position, Quaternion.identity);
    }

    public void ShowNoiseEffect(int qubitIndex)
    {
        if (qubitIndex < 0 || qubitIndex >= qubits.Length)
            return;

        Renderer renderer = qubits[qubitIndex].GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red; // Change color to indicate noise
            Debug.Log($"Noise applied to Qubit {qubitIndex}");
        }
    }
}
