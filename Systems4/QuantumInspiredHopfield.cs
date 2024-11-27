using System;
using System.Collections.Generic;
using UnityEngine;

public class QuantumInspiredHopfield : MonoBehaviour
{
    // Parameters
    private const int numQubits = 5;
    private const int numNetworks = 20;
    private const float interjectionThreshold = 0.3f;
    private const float learningRate = 0.1f;
    private const float retentionRate = 0.9f;
    private const float synchronizationRate = 0.05f;
    private const int timeSteps = 50;
    private const int syncInterval = 5;

    // Hopfield Network Memory and Weights
    private List<float[,]> hopfieldMemory;
    private List<float[]> synapticRelayWeights;
    private float[,] bayesianPrior;

    void Start()
    {
        Initialize();
        RunSimulation();
    }

    private void Initialize()
    {
        hopfieldMemory = new List<float[,]>();
        synapticRelayWeights = new List<float[]>();

        for (int i = 0; i < numNetworks; i++)
        {
            hopfieldMemory.Add(new float[numQubits, numQubits]);
            synapticRelayWeights.Add(new float[numQubits]);
            for (int j = 0; j < numQubits; j++)
            {
                synapticRelayWeights[i][j] = 1.0f;
            }
        }

        // Initialize Bayesian Prior
        bayesianPrior = new float[numQubits, numQubits];
        for (int i = 0; i < numQubits; i++)
        {
            for (int j = 0; j < numQubits; j++)
            {
                bayesianPrior[i, j] = UnityEngine.Random.value;
            }
        }

        NormalizeBayesianPrior();
    }

    private void NormalizeBayesianPrior()
    {
        float sum = 0f;
        foreach (var value in bayesianPrior)
            sum += value;

        for (int i = 0; i < numQubits; i++)
        {
            for (int j = 0; j < numQubits; j++)
            {
                bayesianPrior[i, j] /= sum;
            }
        }
    }

    private void RunSimulation()
    {
        for (int t = 0; t < timeSteps; t++)
        {
            for (int net = 0; net < numNetworks; net++)
            {
                // Randomly initialize cortical and synaptic states
                float[] corticalState = RandomNormalizedVector(numQubits);
                float[] synapticState = RandomNormalizedVector(numQubits);

                // Calculate similarities
                float cosSim = CosineSimilarity(corticalState, synapticState);
                float deltaTangentSim = DeltaTangentSimilarity(corticalState, synapticState);

                // Interjection Detection
                float interjection = Mathf.Abs(cosSim - deltaTangentSim);
                if (interjection > interjectionThreshold)
                {
                    for (int idx = 0; idx < numQubits; idx++)
                    {
                        float adjustment = learningRate * interjection;
                        synapticRelayWeights[net][idx] += adjustment;
                        for (int j = 0; j < numQubits; j++)
                        {
                            hopfieldMemory[net][idx, j] += synapticRelayWeights[net][idx] * bayesianPrior[idx, j];
                        }
                    }
                }

                // Recurrent Memory Update
                for (int i = 0; i < numQubits; i++)
                {
                    for (int j = 0; j < numQubits; j++)
                    {
                        if (i != j)
                        {
                            hopfieldMemory[net][i, j] = retentionRate * hopfieldMemory[net][i, j] +
                                                        learningRate * synapticRelayWeights[net][i] * synapticRelayWeights[net][j] * cosSim;
                        }
                    }
                }
            }

            // Synchronization
            if (t % syncInterval == 0)
            {
                SynchronizeNetworks();
                Debug.Log($"Synchronization at time step {t}");
            }
        }

        PrintFinalStates();
    }

    private void SynchronizeNetworks()
    {
        float[,] synchronizedMemory = new float[numQubits, numQubits];
        foreach (var memory in hopfieldMemory)
        {
            for (int i = 0; i < numQubits; i++)
            {
                for (int j = 0; j < numQubits; j++)
                {
                    synchronizedMemory[i, j] += memory[i, j];
                }
            }
        }

        for (int i = 0; i < numQubits; i++)
        {
            for (int j = 0; j < numQubits; j++)
            {
                synchronizedMemory[i, j] /= numNetworks;
            }
        }

        for (int net = 0; net < numNetworks; net++)
        {
            for (int i = 0; i < numQubits; i++)
            {
                for (int j = 0; j < numQubits; j++)
                {
                    hopfieldMemory[net][i, j] = hopfieldMemory[net][i, j] * (1 - synchronizationRate) +
                                                synchronizedMemory[i, j] * synchronizationRate;
                }
            }
        }
    }

    private void PrintFinalStates()
    {
        for (int net = 0; net < numNetworks; net++)
        {
            Debug.Log($"Final Hopfield Memory for Network {net}:");
            for (int i = 0; i < numQubits; i++)
            {
                string row = "";
                for (int j = 0; j < numQubits; j++)
                {
                    row += $"{hopfieldMemory[net][i, j]:F2} ";
                }
                Debug.Log(row);
            }
        }
    }

    private float[] RandomNormalizedVector(int size)
    {
        float[] vector = new float[size];
        float magnitude = 0f;

        for (int i = 0; i < size; i++)
        {
            vector[i] = UnityEngine.Random.value;
            magnitude += vector[i] * vector[i];
        }

        magnitude = Mathf.Sqrt(magnitude);

        for (int i = 0; i < size; i++)
        {
            vector[i] /= magnitude;
        }

        return vector;
    }

    private float CosineSimilarity(float[] a, float[] b)
    {
        float dot = 0f, normA = 0f, normB = 0f;

        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }

        return dot / (Mathf.Sqrt(normA) * Mathf.Sqrt(normB));
    }

    private float DeltaTangentSimilarity(float[] a, float[] b)
    {
        float cosSim = CosineSimilarity(a, b);
        return Mathf.Tan(Mathf.Acos(cosSim));
    }
}
