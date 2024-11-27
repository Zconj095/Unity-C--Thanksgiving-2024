using System;
using UnityEngine;

public class SpecialQuantumSimulation : MonoBehaviour
{
    // Parameters
    private const int NumQubits = 5;  // High-dimensional representation
    private const float SimilarityThreshold = 0.5f;  // Cosine similarity threshold
    private const int TimeSteps = 50;  // Number of temporal steps

    void Start()
    {
        // Initialize states for simulation
        float[] corticalState = new float[NumQubits];
        float[] synapticState = new float[NumQubits];

        for (int t = 0; t < TimeSteps; t++)
        {
            // Generate random cortical and synaptic states
            corticalState = GenerateRandomState(NumQubits);
            synapticState = GenerateRandomState(NumQubits);

            // Normalize states
            NormalizeState(corticalState);
            NormalizeState(synapticState);

            // Calculate cosine similarity
            float similarity = CalculateCosineSimilarity(corticalState, synapticState);

            // Log similarity
            Debug.Log($"Time step {t} - Cosine Similarity: {similarity}");

            // Detect dip and apply operation
            if (similarity < SimilarityThreshold)
            {
                Debug.Log("Dip Detected in Quantum Cosine Similarity");
                // Simulate phase shift or other operations as needed
                ApplyDipOperation(synapticState);
            }
        }

        // Output the final simulated states for verification
        Debug.Log("Final Cortical State: " + string.Join(", ", corticalState));
        Debug.Log("Final Synaptic State: " + string.Join(", ", synapticState));
    }

    private float[] GenerateRandomState(int size)
    {
        float[] state = new float[size];
        System.Random random = new System.Random();
        for (int i = 0; i < size; i++)
        {
            state[i] = (float)random.NextDouble();  // Random values between 0 and 1
        }
        return state;
    }

    private void NormalizeState(float[] state)
    {
        float norm = 0f;
        foreach (float value in state)
        {
            norm += value * value;
        }
        norm = Mathf.Sqrt(norm);

        for (int i = 0; i < state.Length; i++)
        {
            state[i] /= norm;  // Normalize each component
        }
    }

    private float CalculateCosineSimilarity(float[] stateA, float[] stateB)
    {
        float dotProduct = 0f;
        float normA = 0f, normB = 0f;

        for (int i = 0; i < stateA.Length; i++)
        {
            dotProduct += stateA[i] * stateB[i];
            normA += stateA[i] * stateA[i];
            normB += stateB[i] * stateB[i];
        }

        return dotProduct / (Mathf.Sqrt(normA) * Mathf.Sqrt(normB));
    }

    private void ApplyDipOperation(float[] state)
    {
        // Example operation for dip detection (e.g., scaling state values)
        for (int i = 0; i < state.Length; i++)
        {
            state[i] *= -1;  // Invert values as a simulated "phase shift"
        }
    }
}
