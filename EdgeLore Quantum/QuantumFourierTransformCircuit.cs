using UnityEngine;

public class QuantumFourierTransformCircuit : MonoBehaviour
{
    public void ApplyQFT(int numQubits)
    {
        Debug.Log($"Applying Quantum Fourier Transform to {numQubits} qubits...");

        for (int i = 0; i < numQubits; i++)
        {
            Debug.Log($"Applying H Gate to Qubit {i}...");
            for (int j = i + 1; j < numQubits; j++)
            {
                float angle = Mathf.PI / Mathf.Pow(2, j - i);
                Debug.Log($"Applying Controlled R({angle}) Gate between Qubit {i} and Qubit {j}...");
            }
        }

        Debug.Log("Quantum Fourier Transform completed.");
    }
}
