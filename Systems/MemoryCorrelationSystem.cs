using System;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCorrelationSystem : MonoBehaviour
{
    // Data structure for memory states
    private class MemoryState
    {
        public Vector3 Position { get; set; }
        public float Intensity { get; set; }
        public string Tags { get; set; } // Tagging for memory classification

        public MemoryState(Vector3 position, float intensity, string tags)
        {
            Position = position;
            Intensity = intensity;
            Tags = tags;
        }
    }

    private List<MemoryState> memoryStates = new List<MemoryState>();
    private const int memorySize = 30; // Number of memories

    private float[,] correlationMatrix; // Correlation matrix
    private float fluxAmbience; // Ambient memory flux

    // Initialize memory states
    private void InitializeMemory()
    {
        for (int i = 0; i < memorySize; i++)
        {
            Vector3 position = new Vector3(
                UnityEngine.Random.Range(-10.0f, 10.0f),
                UnityEngine.Random.Range(-10.0f, 10.0f),
                UnityEngine.Random.Range(-10.0f, 10.0f)
            );
            float intensity = UnityEngine.Random.Range(0.5f, 1.5f);
            string tags = $"Tag-{UnityEngine.Random.Range(1, 5)}";

            memoryStates.Add(new MemoryState(position, intensity, tags));
        }

        Debug.Log("Memory States Initialized with " + memorySize + " memories.");
    }

    // Build the correlation matrix
    private void BuildCorrelationMatrix()
    {
        int size = memoryStates.Count;
        correlationMatrix = new float[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float similarity = Vector3.Dot(memoryStates[i].Position.normalized, memoryStates[j].Position.normalized);
                correlationMatrix[i, j] = similarity;
            }
        }

        Debug.Log("Correlation Matrix Built.");
    }

    // Calculate flux ambience
    private void CalculateFluxAmbience()
    {
        fluxAmbience = 0.0f;

        for (int i = 0; i < memoryStates.Count; i++)
        {
            for (int j = 0; j < memoryStates.Count; j++)
            {
                fluxAmbience += correlationMatrix[i, j] * memoryStates[i].Intensity * memoryStates[j].Intensity;
            }
        }

        fluxAmbience /= memoryStates.Count * memoryStates.Count;
        Debug.Log("Flux Ambience Calculated: " + fluxAmbience);
    }

    // Retrieve memory based on a stimulus vector
    private MemoryState RetrieveMemory(Vector3 stimulus)
    {
        float maxCorrelation = float.MinValue;
        MemoryState bestMatch = null;

        foreach (var memory in memoryStates)
        {
            float correlation = Vector3.Dot(memory.Position.normalized, stimulus.normalized);
            if (correlation > maxCorrelation)
            {
                maxCorrelation = correlation;
                bestMatch = memory;
            }
        }

        Debug.Log($"Retrieved Memory: Position={bestMatch.Position}, Intensity={bestMatch.Intensity}, Tags={bestMatch.Tags}");
        return bestMatch;
    }

    // Generate feedback for retrieved memory
    private void FeedbackMemory(MemoryState memory)
    {
        foreach (var state in memoryStates)
        {
            float correlation = Vector3.Dot(memory.Position.normalized, state.Position.normalized);
            state.Intensity += correlation * fluxAmbience * 0.1f;
        }

        Debug.Log("Memory Feedback Applied.");
    }

    void Start()
    {
        // Initialize memory states
        InitializeMemory();

        // Build correlation matrix
        BuildCorrelationMatrix();

        // Calculate flux ambience
        CalculateFluxAmbience();

        // Retrieve memory based on stimulus
        Vector3 stimulus = new Vector3(5.0f, -3.0f, 2.0f);
        MemoryState retrievedMemory = RetrieveMemory(stimulus);

        // Apply feedback
        FeedbackMemory(retrievedMemory);
    }
}
