using UnityEngine;

public class EntangledGemSystem : MonoBehaviour
{
    // Quantum state for Opal (3D vector for simplicity)
    private float[] opalState;

    // Quantum state for Emerald (3D vector)
    private float[] emeraldState;

    // Entanglement matrix: links Opal and Emerald
    private float[,] entanglementMatrix;

    // Entangled shared state
    private float[] sharedState;

    void Start()
    {
        // Initialize Opal's quantum state
        opalState = new float[] { 1.0f, 0.5f, 0.2f };

        // Initialize Emerald's quantum state
        emeraldState = new float[] { 0.7f, 0.3f, 0.9f };

        // Initialize entanglement matrix
        entanglementMatrix = new float[3, 3]
        {
            { 0.8f, 0.2f, 0.1f },
            { 0.1f, 0.9f, 0.2f },
            { 0.2f, 0.1f, 0.7f }
        };

        // Initialize shared state
        sharedState = new float[3];
    }

    void Update()
    {
        // Perform the entanglement process
        PerformEntanglement();
    }

    private void PerformEntanglement()
    {
        // Combine Opal and Emerald states into a shared state
        float[] combinedState = new float[3];
        for (int i = 0; i < combinedState.Length; i++)
        {
            combinedState[i] = opalState[i] + emeraldState[i];
        }

        // Apply the entanglement matrix
        sharedState = MatrixMultiply(entanglementMatrix, combinedState);

        // Propagate the shared state back to Opal and Emerald
        for (int i = 0; i < opalState.Length; i++)
        {
            opalState[i] = sharedState[i] * 0.5f; // Influence on Opal
            emeraldState[i] = sharedState[i] * 0.5f; // Influence on Emerald
        }
    }

    // Matrix-vector multiplication utility
    private float[] MatrixMultiply(float[,] matrix, float[] vector)
    {
        float[] result = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            result[i] = 0.0f;
            for (int j = 0; j < vector.Length; j++)
            {
                result[i] += matrix[i, j] * vector[j];
            }
        }
        return result;
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            // Visualize Opal's state as a yellow sphere
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector3(opalState[0], opalState[1], opalState[2]), 0.2f);

            // Visualize Emerald's state as a green sphere
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(emeraldState[0], emeraldState[1], emeraldState[2]), 0.2f);

            // Visualize the shared state as a cyan sphere
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(new Vector3(sharedState[0], sharedState[1], sharedState[2]), 0.3f);
        }
    }

    void OnGUI()
    {
        // Display the entangled states
        GUI.Label(new Rect(10, 10, 300, 20), $"Opal State: [{string.Join(", ", opalState)}]");
        GUI.Label(new Rect(10, 30, 300, 20), $"Emerald State: [{string.Join(", ", emeraldState)}]");
        GUI.Label(new Rect(10, 50, 300, 20), $"Shared State: [{string.Join(", ", sharedState)}]");
    }
}
