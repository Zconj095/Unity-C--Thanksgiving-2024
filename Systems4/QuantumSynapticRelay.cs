using System;
using System.Collections.Generic;
using UnityEngine;

public class QuantumSynapticRelay : MonoBehaviour
{
    // Parameters
    private const float SynapticFluxFactor = 0.7f;  // Representing relay probability
    private const float CorticalRelapseFactor = 0.3f;  // Representing cortical inhibition
    private const int TimeSteps = 50;  // Number of temporal steps

    // Quantum State Simulation
    private float[] probabilities = new float[3];  // Relay, Delay, Cortical states

    void Start()
    {
        // Initialize probabilities
        probabilities[0] = 0.5f; // Relay
        probabilities[1] = 0.3f; // Delay
        probabilities[2] = 0.2f; // Cortical

        // Simulate Quantum Circuit Dynamics
        for (int t = 0; t < TimeSteps; t++)
        {
            SimulateTimeStep(t);
        }

        // Output results
        Debug.Log($"Relay Probability: {probabilities[0] * SynapticFluxFactor}");
        Debug.Log($"Delay Probability: {probabilities[1] * (1 - SynapticFluxFactor)}");
        Debug.Log($"Cortical Influence Probability: {probabilities[2] * CorticalRelapseFactor}");
    }

    void SimulateTimeStep(int t)
    {
        // Synaptic flux relay state
        if (UnityEngine.Random.value < SynapticFluxFactor)
        {
            probabilities[0] += 0.1f; // Increase relay probability
        }
        else
        {
            probabilities[0] -= 0.1f; // Decrease relay probability
        }

        // Cortical relapse adjustment
        probabilities[2] = Mathf.Clamp(
            probabilities[2] + CorticalRelapseFactor * Mathf.PI / 2f,
            0f, 1f
        );

        // Relay or delay adjustment
        if (UnityEngine.Random.value > SynapticFluxFactor)
        {
            probabilities[1] += 0.1f; // Increase delay probability
        }
        else
        {
            probabilities[1] = Mathf.Clamp(
                probabilities[1] + probabilities[0] * probabilities[2],
                0f, 1f
            );
        }

        // Normalize probabilities
        NormalizeProbabilities();
    }

    void NormalizeProbabilities()
    {
        float sum = 0f;
        foreach (float p in probabilities)
        {
            sum += p;
        }

        for (int i = 0; i < probabilities.Length; i++)
        {
            probabilities[i] /= sum;
        }
    }
}
