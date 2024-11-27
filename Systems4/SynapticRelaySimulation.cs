using System;
using UnityEngine;

public class SynapticRelaySimulation : MonoBehaviour
{
    // Parameters
    private const int NumQubits = 5;  // High-dimensional representation
    private const float InterjectionThreshold = 0.3f;  // Threshold for wild interjections
    private const float LearningRate = 0.1f;  // Hebbian learning rate
    private const int TimeSteps = 50;  // Number of temporal steps

    private float[] synapticRelayWeights;

    void Start()
    {
        // Initialize synaptic relay weights
        synapticRelayWeights = new float[NumQubits];
        for (int i = 0; i < NumQubits; i++)
        {
            synapticRelayWeights[i] = 1f;  // Initialize all weights to 1
        }

        // Run simulation
        for (int t = 0; t < TimeSteps; t++)
        {
            SimulateTimeStep(t);
        }

        // Output final weights
        Debug.Log("Final Synaptic Relay Weights: " + string.Join(", ", synapticRelayWeights));
    }

    private void SimulateTimeStep(int t)
    {
        System.Random random = new System.Random();

        // Randomly initialize cortical and synaptic states
        float[] corticalState = GenerateRandomState(NumQubits, random);
        float[] synapticState = GenerateRandomState(NumQubits, random);

        // Normalize states
        Normalize(corticalState);
        Normalize(synapticState);

        // Calculate similarities
        float cosSim = CalculateCosineSimilarity(corticalState, synapticState);
        float deltaTangentSim = CalculateDeltaTangentSimilarity(cosSim);

        // Calculate interjection
        float[] interjection = CalculateInterjection(cosSim, deltaTangentSim);

        // Detect interjection indices
        for (int i = 0; i < interjection.Length; i++)
        {
            if (interjection[i] > InterjectionThreshold)
            {
                // Update synaptic relay weights
                synapticRelayWeights[i] += LearningRate * interjection[i];
            }
        }

        // Log details
        Debug.Log($"Time step {t} - Cosine Similarity: {cosSim}");
        Debug.Log($"Delta Tangent Similarity: {deltaTangentSim}");
        Debug.Log($"Updated Synaptic Relay Weights: {string.Join(", ", synapticRelayWeights)}");
    }

    private float[] GenerateRandomState(int size, System.Random random)
    {
        float[] state = new float[size];
        for (int i = 0; i < size; i++)
        {
            state[i] = (float)random.NextDouble();  // Random values between 0 and 1
        }
        return state;
    }

    private void Normalize(float[] state)
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

    private float CalculateDeltaTangentSimilarity(float cosSim)
    {
        return Mathf.Tan(Mathf.Acos(cosSim));
    }

    private float[] CalculateInterjection(float cosSim, float deltaTangentSim)
    {
        float[] interjection = new float[NumQubits];
        for (int i = 0; i < NumQubits; i++)
        {
            interjection[i] = Mathf.Abs(cosSim - deltaTangentSim);
        }
        return interjection;
    }
}
