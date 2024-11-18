using UnityEngine;
using System.Collections.Generic;

public class CoordinalInferentialMeans : MonoBehaviour
{
    // Represents a point in 3D space with a likelihood of an event
    public struct SpatialPoint
    {
        public Vector3 Position; // 3D position in space
        public float Likelihood; // Likelihood of an event at this coordinate

        public SpatialPoint(Vector3 position, float likelihood)
        {
            Position = position;
            Likelihood = likelihood;
        }
    }

    private List<SpatialPoint> spatialPoints;

    // Thresholds for inference
    [SerializeField] private float maxDistance = 10f; // Maximum distance to consider in inference
    [SerializeField] private float likelihoodThreshold = 0.5f; // Threshold for event likelihood

    void Start()
    {
        // Initialize spatial points (you can modify or populate these dynamically as needed)
        spatialPoints = new List<SpatialPoint>
        {
            new SpatialPoint(new Vector3(1, 2, 3), 0.3f),
            new SpatialPoint(new Vector3(5, 1, 4), 0.8f),
            new SpatialPoint(new Vector3(10, 0, 0), 0.2f)
        };
    }

    void Update()
    {
        Vector3 playerPosition = transform.position; // Current position of the GameObject

        // Process each spatial point for inference
        foreach (var point in spatialPoints)
        {
            float distance = Vector3.Distance(playerPosition, point.Position);

            // Check if the point is within the inference range
            if (distance <= maxDistance)
            {
                float inferredLikelihood = InferLikelihood(distance, point.Likelihood);

                Debug.Log($"Inferred likelihood for point at {point.Position} is: {inferredLikelihood}");

                if (inferredLikelihood >= likelihoodThreshold)
                {
                    Debug.Log($"High likelihood event detected at point {point.Position}!");
                    // Add event logic or responses here
                }
            }
        }
    }

    // Function to infer likelihood based on distance and the base likelihood
    private float InferLikelihood(float distance, float baseLikelihood)
    {
        // Inverse distance weighting for likelihood adjustment
        float distanceFactor = Mathf.Max(0, 1 - (distance / maxDistance));
        float inferredLikelihood = baseLikelihood * distanceFactor;

        // Clamp the likelihood between 0 and 1 for consistency
        return Mathf.Clamp01(inferredLikelihood);
    }
}
