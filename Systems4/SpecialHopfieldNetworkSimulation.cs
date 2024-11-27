using System;
using UnityEngine;

public class SpecialHopfieldNetworkSimulation : MonoBehaviour
{
    // Parameters
    private const int NumQubits = 5; // High-dimensional representation
    private const float InterjectionThreshold = 0.3f; // Threshold for interjection detection
    private const float LearningRate = 0.1f; // Hebbian learning rate
    private const float RetentionRate = 0.9f; // Retention rate for memory
    private const int TimeSteps = 50; // Number of temporal steps

    private float[,] hopfieldMemory;
    private float[] synapticRelayWeights;
    private float[,] bayesianPrior;

    void Start()
    {
        // Initialize memory, weights, and prior
        hopfieldMemory = new float[NumQubits, NumQubits];
        synapticRelayWeights = new float[NumQubits];
        bayesianPrior = GenerateRandomMatrix(NumQubits, NumQubits);

        NormalizeMatrix(bayesianPrior); // Normalize Bayesian prior
        for (int i = 0; i < NumQubits; i++)
        {
            synapticRelayWeights[i] = 1f; // Initialize weights to 1
        }

        // Run simulation
        for (int t = 0; t < TimeSteps; t++)
        {
            SimulateTimeStep(t);
        }

        // Output final memory matrix
        Debug.Log("Final Hopfield Memory Matrix:");
        for (int i = 0; i < NumQubits; i++)
        {
            Debug.Log(string.Join(", ", GetRow(hopfieldMemory, i)));
        }
    }

    private void SimulateTimeStep(int timeStep)
    {
        System.Random random = new System.Random();

        // Generate random cortical and synaptic states
        float[] corticalState = GenerateRandomVector(NumQubits, random);
        float[] synapticState = GenerateRandomVector(NumQubits, random);

        Normalize(corticalState);
        Normalize(synapticState);

        // Calculate similarities
        float cosSim = CalculateCosineSimilarity(corticalState, synapticState);
        float deltaTangentSim = CalculateDeltaTangentSimilarity(cosSim);

        // Detect and handle interjections
        for (int i = 0; i < NumQubits; i++)
        {
            float interjection = Mathf.Abs(cosSim - deltaTangentSim);
            if (interjection > InterjectionThreshold)
            {
                synapticRelayWeights[i] += LearningRate * interjection;
                for (int j = 0; j < NumQubits; j++)
                {
                    hopfieldMemory[i, j] += synapticRelayWeights[i] * bayesianPrior[i, j];
                }
            }
        }

        // Update Hopfield memory
        UpdateHopfieldMemory(cosSim);

        // Log details
        Debug.Log($"Time step {timeStep} - Cosine Similarity: {cosSim}");
        Debug.Log($"Delta Tangent Similarity: {deltaTangentSim}");
    }

    private void UpdateHopfieldMemory(float cosSim)
    {
        for (int i = 0; i < NumQubits; i++)
        {
            for (int j = 0; j < NumQubits; j++)
            {
                if (i != j)
                {
                    hopfieldMemory[i, j] = RetentionRate * hopfieldMemory[i, j] +
                                           LearningRate * synapticRelayWeights[i] *
                                           synapticRelayWeights[j] * cosSim;
                }
            }
        }
    }

    private float[,] GenerateRandomMatrix(int rows, int cols)
    {
        float[,] matrix = new float[rows, cols];
        System.Random random = new System.Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = (float)random.NextDouble();
            }
        }
        return matrix;
    }

    private void NormalizeMatrix(float[,] matrix)
    {
        float sum = 0f;
        foreach (float value in matrix)
        {
            sum += value;
        }

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] /= sum;
            }
        }
    }

    private float[] GenerateRandomVector(int size, System.Random random)
    {
        float[] vector = new float[size];
        for (int i = 0; i < size; i++)
        {
            vector[i] = (float)random.NextDouble();
        }
        return vector;
    }

    private void Normalize(float[] vector)
    {
        float norm = 0f;
        foreach (float value in vector)
        {
            norm += value * value;
        }
        norm = Mathf.Sqrt(norm);

        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] /= norm;
        }
    }

    private float CalculateCosineSimilarity(float[] vectorA, float[] vectorB)
    {
        float dotProduct = 0f;
        float normA = 0f, normB = 0f;

        for (int i = 0; i < vectorA.Length; i++)
        {
            dotProduct += vectorA[i] * vectorB[i];
            normA += vectorA[i] * vectorA[i];
            normB += vectorB[i] * vectorB[i];
        }

        return dotProduct / (Mathf.Sqrt(normA) * Mathf.Sqrt(normB));
    }

    private float CalculateDeltaTangentSimilarity(float cosSim)
    {
        return Mathf.Tan(Mathf.Acos(cosSim));
    }

    private float[] GetRow(float[,] matrix, int row)
    {
        float[] result = new float[matrix.GetLength(1)];
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            result[j] = matrix[row, j];
        }
        return result;
    }
}
