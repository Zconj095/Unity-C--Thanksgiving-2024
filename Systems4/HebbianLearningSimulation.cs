using System;
using UnityEngine;

public class HebbianLearningSimulation : MonoBehaviour
{
    // Parameters
    private const int NumQubits = 5;  // Number of dimensions in the representation
    private const float LearningRate = 0.1f;  // Hebbian learning rate
    private const float RetentionRate = 0.9f;  // Retention rate to maintain prior learning
    private const float AttentionThreshold = 0.6f;  // Attention mechanism threshold
    private const float InstructionWeight = 1.5f;  // Extra weight for instruction-following states
    private const int TimeSteps = 50;  // Number of time steps

    private float[] hebbianWeights;

    void Start()
    {
        // Initialize Hebbian Weights
        hebbianWeights = new float[NumQubits];
        for (int i = 0; i < NumQubits; i++)
        {
            hebbianWeights[i] = 1f;  // Initialize all weights to 1
        }

        // Run simulation
        for (int t = 0; t < TimeSteps; t++)
        {
            SimulateTimeStep(t);
        }

        // Output final state and weights
        Debug.Log("Final Hebbian Weights: " + string.Join(", ", hebbianWeights));
    }

    private void SimulateTimeStep(int timeStep)
    {
        System.Random random = new System.Random();

        // Randomly initialize cortical and synaptic states
        float[] corticalState = GenerateRandomState(NumQubits, random);
        float[] synapticState = GenerateRandomState(NumQubits, random);

        // Normalize states
        Normalize(corticalState);
        Normalize(synapticState);

        // Calculate delta cosine tangent intermittance
        float[] deltaIntermit = CalculateDeltaIntermit(corticalState, synapticState);

        // Apply attention mechanism
        for (int i = 0; i < NumQubits; i++)
        {
            if (deltaIntermit[i] < AttentionThreshold)
            {
                // Determine instruction-following weight
                float instructionFollow = (i % 2 == 0) ? InstructionWeight : 1f;

                // Hebbian update
                hebbianWeights[i] = hebbianWeights[i] * RetentionRate +
                                    LearningRate * deltaIntermit[i] * instructionFollow;
            }
        }

        // Log progress for this time step
        Debug.Log($"Time step {timeStep} - Hebbian Weights: {string.Join(", ", hebbianWeights)}");
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

    private float[] CalculateDeltaIntermit(float[] corticalState, float[] synapticState)
    {
        float[] deltaIntermit = new float[NumQubits];

        for (int i = 0; i < NumQubits; i++)
        {
            float cosSim = (corticalState[i] * synapticState[i]) /
                           (Mathf.Sqrt(corticalState[i] * corticalState[i]) *
                            Mathf.Sqrt(synapticState[i] * synapticState[i]));

            deltaIntermit[i] = Mathf.Tan(Mathf.Acos(cosSim));
        }

        return deltaIntermit;
    }
}
