using System;
using UnityEngine;

public class HiddenMarkovDistribution : MonoBehaviour
{
    [Header("HMM Configuration")]
    [SerializeField] private int numStates = 3;          // Number of hidden states
    [SerializeField] private int numObservations = 4;    // Number of possible observations

    private float[,] transitionProbabilities;  // Transition probabilities (states x states)
    private float[,] emissionProbabilities;    // Emission probabilities (states x observations)
    private float[] initialStateProbabilities; // Initial state probabilities

    private void Start()
    {
        // Initialize the HMM with random probabilities
        InitializeHMM();

        // Generate a random sequence of observations
        int[] observations = GenerateRandomObservations(10);

        // Decode the most likely state sequence using the Viterbi algorithm
        int[] mostLikelyStates = Viterbi(observations);

        // Log the results
        Debug.Log("Observations: " + string.Join(", ", observations));
        Debug.Log("Most Likely States: " + string.Join(", ", mostLikelyStates));
    }

    /// <summary>
    /// Initializes the HMM parameters with uniform probabilities.
    /// </summary>
    private void InitializeHMM()
    {
        transitionProbabilities = new float[numStates, numStates];
        emissionProbabilities = new float[numStates, numObservations];
        initialStateProbabilities = new float[numStates];

        // Initialize uniform probabilities
        for (int i = 0; i < numStates; i++)
        {
            initialStateProbabilities[i] = 1.0f / numStates;

            for (int j = 0; j < numStates; j++)
                transitionProbabilities[i, j] = 1.0f / numStates;

            for (int k = 0; k < numObservations; k++)
                emissionProbabilities[i, k] = 1.0f / numObservations;
        }
    }

    /// <summary>
    /// Viterbi Algorithm: Decodes the most likely state sequence for a given observation sequence.
    /// </summary>
    /// <param name="observations">Array of observations.</param>
    /// <returns>The most likely state sequence.</returns>
    public int[] Viterbi(int[] observations)
    {
        int numObservations = observations.Length;

        // Dynamic programming matrices
        float[,] dp = new float[numStates, numObservations];
        int[,] backpointer = new int[numStates, numObservations];

        // Initialization step
        for (int state = 0; state < numStates; state++)
        {
            dp[state, 0] = initialStateProbabilities[state] * emissionProbabilities[state, observations[0]];
            backpointer[state, 0] = 0;
        }

        // Recursion step
        for (int t = 1; t < numObservations; t++)
        {
            for (int currentState = 0; currentState < numStates; currentState++)
            {
                float maxProb = -1;
                int bestPrevState = 0;

                for (int prevState = 0; prevState < numStates; prevState++)
                {
                    float prob = dp[prevState, t - 1] * transitionProbabilities[prevState, currentState] * emissionProbabilities[currentState, observations[t]];
                    if (prob > maxProb)
                    {
                        maxProb = prob;
                        bestPrevState = prevState;
                    }
                }

                dp[currentState, t] = maxProb;
                backpointer[currentState, t] = bestPrevState;
            }
        }

        // Backtracking to find the most likely state sequence
        int[] mostLikelyStates = new int[numObservations];
        float maxFinalProb = -1;
        int lastState = 0;

        for (int state = 0; state < numStates; state++)
        {
            if (dp[state, numObservations - 1] > maxFinalProb)
            {
                maxFinalProb = dp[state, numObservations - 1];
                lastState = state;
            }
        }

        mostLikelyStates[numObservations - 1] = lastState;

        for (int t = numObservations - 2; t >= 0; t--)
        {
            mostLikelyStates[t] = backpointer[mostLikelyStates[t + 1], t + 1];
        }

        return mostLikelyStates;
    }

    /// <summary>
    /// Generates a random sequence of observations.
    /// </summary>
    /// <param name="length">Length of the observation sequence.</param>
    /// <returns>An array of random observations.</returns>
    public int[] GenerateRandomObservations(int length)
    {
        int[] observations = new int[length];
        System.Random rand = new System.Random();

        for (int i = 0; i < length; i++)
        {
            observations[i] = rand.Next(numObservations);
        }

        return observations;
    }
}
