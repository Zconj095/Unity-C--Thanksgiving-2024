using System;
using System.Collections.Generic;
using UnityEngine;

public class CozniCortexSystem : MonoBehaviour
{
    // Data structures for celestial vectors and cognitive feedback
    private class CelestialVector
    {
        public Vector3 Position { get; set; }
        public float ActivationLevel { get; set; }

        public CelestialVector(Vector3 position)
        {
            Position = position;
            ActivationLevel = 0.0f;
        }
    }

    private List<CelestialVector> celestialArray = new List<CelestialVector>();
    private const int arraySize = 50; // Number of celestial vectors

    // Initialize the celestial vector array
    private void InitializeCelestialArray()
    {
        for (int i = 0; i < arraySize; i++)
        {
            Vector3 position = new Vector3(
                UnityEngine.Random.Range(-20.0f, 20.0f),
                UnityEngine.Random.Range(-20.0f, 20.0f),
                UnityEngine.Random.Range(-20.0f, 20.0f)
            );
            celestialArray.Add(new CelestialVector(position));
        }

        Debug.Log("Celestial Array Initialized with " + arraySize + " vectors.");
    }

    // Zellia Permutation Algorithm
    private List<Vector3> ZelliaPermutation(Vector3 start, int depth, float scale)
    {
        if (depth == 0)
            return new List<Vector3> { start };

        List<Vector3> permutations = new List<Vector3>();
        Vector3[] offsets = {
            new Vector3(scale, scale, 0),
            new Vector3(-scale, scale, 0),
            new Vector3(scale, -scale, 0),
            new Vector3(-scale, -scale, 0),
            new Vector3(0, scale, scale),
            new Vector3(0, -scale, scale),
            new Vector3(0, scale, -scale),
            new Vector3(0, -scale, -scale)
        };

        foreach (var offset in offsets)
        {
            permutations.AddRange(ZelliaPermutation(start + offset, depth - 1, scale * 0.5f));
        }

        return permutations;
    }

    // Vector Means Transformation
    private Vector3 TransformVectorMeans()
    {
        Vector3 mean = Vector3.zero;
        foreach (var vector in celestialArray)
        {
            mean += vector.Position;
        }
        mean /= celestialArray.Count;

        Debug.Log("Transformed Vector Mean: " + mean);
        return mean;
    }

    // Hypercorrelated Response Calculation
    private float CalculateHypercorrelation(Vector3 stimulus, Vector3 response)
    {
        float dotProduct = Vector3.Dot(stimulus.normalized, response.normalized);
        float distance = Vector3.Distance(stimulus, response);
        float hypercorrelation = dotProduct / (1.0f + distance);

        Debug.Log($"Hypercorrelation: {hypercorrelation} for Stimulus: {stimulus} and Response: {response}");
        return hypercorrelation;
    }

    // Cozni Cortex Feedback and Synchrony
    private void GenerateCognitiveFeedback(Vector3 stimulus)
    {
        Vector3 transformedMean = TransformVectorMeans();
        foreach (var vector in celestialArray)
        {
            float response = CalculateHypercorrelation(stimulus, vector.Position);
            vector.ActivationLevel += response;

            Debug.Log($"Vector at {vector.Position} Activation Level: {vector.ActivationLevel}");
        }

        Debug.Log("Cognitive Feedback Completed.");
    }

    void Start()
    {
        // Initialize celestial array and cognitive cortex
        InitializeCelestialArray();

        // Generate Zellia Permutations
        Vector3 start = Vector3.zero;
        var permutations = ZelliaPermutation(start, 4, 10.0f);
        Debug.Log("Generated Zellia Permutations with " + permutations.Count + " points.");

        // Simulate Cozni Cortex with Stimulus
        Vector3 stimulus = new Vector3(5.0f, -3.0f, 2.0f);
        GenerateCognitiveFeedback(stimulus);
    }
}
