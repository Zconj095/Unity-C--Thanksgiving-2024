using System.Collections.Generic;
using UnityEngine;

public class CognitiveSynchronization : MonoBehaviour
{
    [Header("Neural Network Settings")]
    public int numberOfNeurons = 10;
    public float couplingStrength = 0.5f; // Strength of neuron coupling
    public float learningRate = 0.1f; // Rate for Hebbian updates
    public float phaseUpdateInterval = 0.1f; // Interval for phase updates

    private List<Neuron> neurons = new List<Neuron>();
    private float[,] connectionWeights;

    void Start()
    {
        InitializeNeurons();
        InitializeConnections();
        InvokeRepeating(nameof(UpdateNeuronStates), 0f, phaseUpdateInterval);
    }

    void InitializeNeurons()
    {
        neurons.Clear();
        for (int i = 0; i < numberOfNeurons; i++)
        {
            neurons.Add(new Neuron
            {
                Phase = Random.Range(0f, 2 * Mathf.PI), // Random initial phase
                FiringRate = Random.Range(0.1f, 1f) // Random firing rate
            });
        }
        Debug.Log($"Initialized {numberOfNeurons} neurons.");
    }

    void InitializeConnections()
    {
        connectionWeights = new float[numberOfNeurons, numberOfNeurons];

        for (int i = 0; i < numberOfNeurons; i++)
        {
            for (int j = 0; j < numberOfNeurons; j++)
            {
                if (i != j)
                {
                    connectionWeights[i, j] = Random.Range(0.1f, 1f); // Random connection strength
                }
                else
                {
                    connectionWeights[i, j] = 0f; // No self-connection
                }
            }
        }
        Debug.Log("Connections initialized.");
    }

    void UpdateNeuronStates()
    {
        float[] newPhases = new float[numberOfNeurons];

        // Update neuron phases based on synchronization dynamics
        for (int i = 0; i < numberOfNeurons; i++)
        {
            float phaseSum = 0f;

            for (int j = 0; j < numberOfNeurons; j++)
            {
                if (i != j)
                {
                    float phaseDifference = neurons[j].Phase - neurons[i].Phase;
                    phaseSum += connectionWeights[i, j] * Mathf.Sin(phaseDifference);
                }
            }

            newPhases[i] = neurons[i].Phase + couplingStrength * phaseSum;
        }

        // Apply updated phases
        for (int i = 0; i < numberOfNeurons; i++)
        {
            neurons[i].Phase = NormalizePhase(newPhases[i]);
        }

        DebugNeuronStates();
    }

    void DebugNeuronStates()
    {
        for (int i = 0; i < numberOfNeurons; i++)
        {
            Debug.Log($"Neuron {i}: Phase = {neurons[i].Phase:F2}, Firing Rate = {neurons[i].FiringRate:F2}");
        }
    }

    float NormalizePhase(float phase)
    {
        // Keep the phase within [0, 2π]
        return (phase + 2 * Mathf.PI) % (2 * Mathf.PI);
    }
}

public class Neuron
{
    public float Phase; // Neuron's phase
    public float FiringRate; // Neuron's firing rate
}
