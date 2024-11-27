using System;
using UnityEngine;

public class QuantumRelaySimulation : MonoBehaviour
{
    // Parameters
    private const float SynapticFluxFactor = 0.7f;  // Relay probability
    private const float CorticalRelapseFactor = 0.3f;  // Cortical inhibition factor
    private const int TimeSteps = 50;  // Number of temporal steps
    private const int NumQubits = 3;  // Number of states: relay, delay, cortical

    // Probabilities for the system
    private float[] probabilities = new float[NumQubits];

    void Start()
    {
        // Initialize quantum-like probabilities
        probabilities[0] = SynapticFluxFactor;  // Relay
        probabilities[1] = 1 - SynapticFluxFactor;  // Delay
        probabilities[2] = CorticalRelapseFactor;  // Cortical

        // Simulate time steps
        for (int t = 0; t < TimeSteps; t++)
        {
            SimulateTimeStep(t);
        }

        // Output final probabilities
        Debug.Log($"Relay Probability: {probabilities[0] * SynapticFluxFactor}");
        Debug.Log($"Delay Probability: {probabilities[1] * (1 - SynapticFluxFactor)}");
        Debug.Log($"Cortical Influence Probability: {probabilities[2] * CorticalRelapseFactor}");
    }

    private void SimulateTimeStep(int t)
    {
        System.Random random = new System.Random();

        // Step 1: Synaptic flux relay state
        if (random.NextDouble() < SynapticFluxFactor)
        {
            probabilities[0] += 0.1f;  // Increase relay probability
        }
        else
        {
            probabilities[0] -= 0.1f;  // Decrease relay probability
        }

        // Step 2: Cortical relapse adjustment
        probabilities[2] = Mathf.Clamp(
            probabilities[2] + CorticalRelapseFactor * Mathf.PI,
            0f, 1f
        );

        // Step 3: Relay or delay adjustment
        if (random.NextDouble() > SynapticFluxFactor)
        {
            probabilities[1] += 0.1f;  // Increase delay probability
        }
        else
        {
            probabilities[1] = Mathf.Clamp(
                probabilities[1] + probabilities[0] * probabilities[2],
                0f, 1f
            );
        }

        // Normalize probabilities to simulate quantum state vector behavior
        NormalizeProbabilities();
    }

    private void NormalizeProbabilities()
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
