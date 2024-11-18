using UnityEngine;
using System.Collections.Generic;

public class FluctuativeHyperstateReasoning : MonoBehaviour
{
    /// <summary>
    /// Represents a hyperstate with a fluctuating probability weight.
    /// </summary>
    [System.Serializable]
    public class Hyperstate
    {
        public string Name; // Name of the hyperstate
        public float ProbabilityWeight; // Current probability weight

        // Constructor
        public Hyperstate(string name, float initialWeight)
        {
            Name = name;
            ProbabilityWeight = initialWeight;
        }
    }

    [SerializeField]
    private List<Hyperstate> hyperstates = new List<Hyperstate>(); // List of hyperstates

    [Header("Fluctuation Parameters")]
    [SerializeField, Range(0f, 0.1f)] private float fluctuationRate = 0.05f; // Max adjustment to probabilities
    [SerializeField, Range(0.01f, 1f)] private float fluctuationFrequency = 0.1f; // Frequency of fluctuation updates (in seconds)

    private void Start()
    {
        // Initialize some default hyperstates if none are provided
        if (hyperstates.Count == 0)
        {
            hyperstates.Add(new Hyperstate("Correlation", 0.3f));
            hyperstates.Add(new Hyperstate("Blessing", 0.2f));
            hyperstates.Add(new Hyperstate("Event Trigger", 0.25f));
            hyperstates.Add(new Hyperstate("Sound Reasoning", 0.25f));
        }

        // Begin fluctuation updates
        InvokeRepeating(nameof(FluctuateHyperstateProbabilities), 0f, fluctuationFrequency);
    }

    /// <summary>
    /// Applies fluctuations to each hyperstate's probability weight.
    /// </summary>
    private void FluctuateHyperstateProbabilities()
    {
        float totalWeight = 0f;

        foreach (var hyperstate in hyperstates)
        {
            // Apply a random fluctuation to the probability weight
            hyperstate.ProbabilityWeight += Random.Range(-fluctuationRate, fluctuationRate);
            hyperstate.ProbabilityWeight = Mathf.Clamp(hyperstate.ProbabilityWeight, 0.05f, 1f); // Ensure reasonable range
            totalWeight += hyperstate.ProbabilityWeight;
        }

        // Normalize probabilities so the total weight equals 1
        foreach (var hyperstate in hyperstates)
        {
            hyperstate.ProbabilityWeight /= totalWeight;
        }
    }

    /// <summary>
    /// Simulates reasoning by selecting a hyperstate based on fluctuating weights.
    /// </summary>
    /// <returns>The selected hyperstate.</returns>
    public Hyperstate Reason()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulativeWeight = 0f;

        foreach (var hyperstate in hyperstates)
        {
            cumulativeWeight += hyperstate.ProbabilityWeight;
            if (randomValue <= cumulativeWeight)
            {
                Debug.Log($"Reasoning led to decision: {hyperstate.Name}");
                return hyperstate;
            }
        }

        // Fallback: Return the last hyperstate if none selected due to rounding errors
        return hyperstates[hyperstates.Count - 1];
    }

    private void Update()
    {
        // Trigger reasoning with the space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reason();
        }
    }
}
