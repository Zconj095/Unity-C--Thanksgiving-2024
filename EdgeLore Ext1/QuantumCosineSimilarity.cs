using System;
using System.Linq;
using UnityEngine;

public class QuantumCosineSimilarity : MonoBehaviour
{
    // Parameters
    private int numQubits = 5; // High-dimensional representation in quantum
    private float similarityThreshold = 0.5f; // Cosine similarity threshold for dip detection
    private int timeSteps = 50; // Number of temporal steps

    public void RunQuantumSimulation()
    {
        // Initialize Quantum Circuit Logic
        for (int t = 0; t < timeSteps; t++)
        {
            // Create Quantum States for Cortical Flex Relay and Synaptic Flux Intermittence
            float[] corticalState = GenerateRandomState(numQubits);
            float[] synapticState = GenerateRandomState(numQubits);

            // Normalize the states
            Normalize(corticalState);
            Normalize(synapticState);

            // Measure cosine similarity
            float similarity = CosineSimilarity(corticalState, synapticState);

            // Detect "dip" if similarity is below threshold
            if (similarity < similarityThreshold)
            {
                Console.WriteLine($"Time step {t}: Dip detected. Cosine Similarity = {similarity}");
            }
            else
            {
                Console.WriteLine($"Time step {t}: No dip. Cosine Similarity = {similarity}");
            }
        }
    }

    // Generate random state for qubits
    private float[] GenerateRandomState(int size)
    {
        System.Random rand = new System.Random();
        return Enumerable.Range(0, size).Select(_ => (float)rand.NextDouble()).ToArray();
    }

    // Normalize a vector
    private void Normalize(float[] vector)
    {
        float magnitude = (float)Math.Sqrt(vector.Sum(x => x * x));
        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] /= magnitude;
        }
    }

    // Calculate cosine similarity between two vectors
    private float CosineSimilarity(float[] vectorA, float[] vectorB)
    {
        float dotProduct = 0;
        float magnitudeA = 0;
        float magnitudeB = 0;

        for (int i = 0; i < vectorA.Length; i++)
        {
            dotProduct += vectorA[i] * vectorB[i];
            magnitudeA += vectorA[i] * vectorA[i];
            magnitudeB += vectorB[i] * vectorB[i];
        }

        return dotProduct / (float)(Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
    }
}
