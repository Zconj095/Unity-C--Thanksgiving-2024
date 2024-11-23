using System;
using System.Collections.Generic;
using System.Linq; // Required for LINQ methods like Select
using UnityEngine;

public class HinizagiTransform : MonoBehaviour
{
    // Core data structure for events
    private class GalacticEvent
    {
        public string EventId { get; set; }
        public Vector3 SpatialOrigin { get; set; } // 3D space coordinates
        public DateTime TimeStamp { get; set; }
        public Dictionary<string, float> Attributes { get; set; } // Custom properties

        public GalacticEvent(string id, Vector3 origin, DateTime timeStamp, Dictionary<string, float> attributes)
        {
            EventId = id;
            SpatialOrigin = origin;
            TimeStamp = timeStamp;
            Attributes = attributes;
        }
    }

    // Clustering system
    private class KMeansCluster
    {
        public List<Vector3> Centroids { get; private set; }
        public Dictionary<int, List<GalacticEvent>> Clusters { get; private set; }

        public KMeansCluster(int k)
        {
            Centroids = new List<Vector3>(k);
            Clusters = new Dictionary<int, List<GalacticEvent>>();
        }

        public void Fit(List<GalacticEvent> events, int iterations = 100)
        {
            System.Random rand = new System.Random(); // Explicit namespace for System.Random
            for (int i = 0; i < Centroids.Capacity; i++)
            {
                Centroids.Add(events[rand.Next(events.Count)].SpatialOrigin);
            }

            for (int it = 0; it < iterations; it++)
            {
                Clusters.Clear();

                foreach (var e in events)
                {
                    int closest = FindClosestCentroid(e.SpatialOrigin);
                    if (!Clusters.ContainsKey(closest))
                        Clusters[closest] = new List<GalacticEvent>();
                    Clusters[closest].Add(e);
                }

                for (int i = 0; i < Centroids.Count; i++)
                {
                    if (Clusters.ContainsKey(i))
                    {
                        Centroids[i] = CalculateMean(Clusters[i]);
                    }
                }
            }
        }

        private int FindClosestCentroid(Vector3 point)
        {
            int index = -1;
            float minDistance = float.MaxValue;
            for (int i = 0; i < Centroids.Count; i++)
            {
                float distance = Vector3.Distance(point, Centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = i;
                }
            }
            return index;
        }

        private Vector3 CalculateMean(List<GalacticEvent> events)
        {
            Vector3 mean = Vector3.zero;
            foreach (var e in events)
            {
                mean += e.SpatialOrigin;
            }
            return mean / events.Count;
        }
    }

    // Temporal correlation methods
    private static class TemporalCorrelation
    {
        /// <summary>
        /// Computes the cosine similarity between two events based on their spatial origins.
        /// </summary>
        /// <param name="e1">First GalacticEvent</param>
        /// <param name="e2">Second GalacticEvent</param>
        /// <returns>Cosine similarity value</returns>
        public static float CosineSimilarity(GalacticEvent e1, GalacticEvent e2)
        {
            Vector3 v1 = e1.SpatialOrigin;
            Vector3 v2 = e2.SpatialOrigin;

            // Ensure valid vectors to avoid division by zero
            if (v1.magnitude == 0 || v2.magnitude == 0)
                return 0f;

            return Vector3.Dot(v1, v2) / (v1.magnitude * v2.magnitude);
        }

        /// <summary>
        /// Computes the tangent-based similarity between two events based on their spatial distance.
        /// </summary>
        /// <param name="e1">First GalacticEvent</param>
        /// <param name="e2">Second GalacticEvent</param>
        /// <returns>Tangent similarity value</returns>
        public static float TangentSimilarity(GalacticEvent e1, GalacticEvent e2)
        {
            float distance = Vector3.Distance(e1.SpatialOrigin, e2.SpatialOrigin);
            return Mathf.Tan(distance);
        }

        /// <summary>
        /// Computes a time-weighted similarity score between two events, incorporating both time and spatial factors.
        /// </summary>
        /// <param name="e1">First GalacticEvent</param>
        /// <param name="e2">Second GalacticEvent</param>
        /// <param name="timeWeight">Weight given to the time factor</param>
        /// <returns>Time-weighted similarity value</returns>
        public static float TimeWeightedSimilarity(GalacticEvent e1, GalacticEvent e2, float timeWeight = 0.5f)
        {
            // Spatial similarity (cosine)
            float spatialSimilarity = CosineSimilarity(e1, e2);

            // Time difference in seconds
            double timeDifference = Math.Abs((e1.TimeStamp - e2.TimeStamp).TotalSeconds);

            // Normalize time difference to a similarity score (inverse relationship)
            float timeSimilarity = Mathf.Exp(-0.001f * (float)timeDifference); // Decay factor adjusts sensitivity

            // Combine spatial and temporal similarity
            return timeWeight * spatialSimilarity + (1 - timeWeight) * timeSimilarity;
        }

        /// <summary>
        /// Computes the Euclidean distance between two events in space.
        /// </summary>
        /// <param name="e1">First GalacticEvent</param>
        /// <param name="e2">Second GalacticEvent</param>
        /// <returns>Euclidean distance</returns>
        public static float EuclideanDistance(GalacticEvent e1, GalacticEvent e2)
        {
            return Vector3.Distance(e1.SpatialOrigin, e2.SpatialOrigin);
        }

        /// <summary>
        /// Combines all similarity metrics for a comprehensive correlation analysis.
        /// </summary>
        /// <param name="e1">First GalacticEvent</param>
        /// <param name="e2">Second GalacticEvent</param>
        /// <returns>Dictionary of similarity scores</returns>
        public static Dictionary<string, float> ComputeAllSimilarities(GalacticEvent e1, GalacticEvent e2)
        {
            return new Dictionary<string, float>
            {
                { "CosineSimilarity", CosineSimilarity(e1, e2) },
                { "TangentSimilarity", TangentSimilarity(e1, e2) },
                { "TimeWeightedSimilarity", TimeWeightedSimilarity(e1, e2) },
                { "EuclideanDistance", EuclideanDistance(e1, e2) }
            };
        }
    }


    // Time series prediction placeholder
    private class TimeSeriesPredictor
    {
        // Predicts the next value using Weighted Moving Average
        public float PredictNextValue(List<float> timeSeries)
        {
            if (timeSeries == null || timeSeries.Count < 2)
            {
                throw new ArgumentException("Time series must contain at least two data points for prediction.");
            }

            // Weighted moving average: recent data points have more influence
            int windowSize = Math.Min(5, timeSeries.Count); // Use a window of up to 5 elements
            float weightSum = 0;
            float weightedSum = 0;

            for (int i = 0; i < windowSize; i++)
            {
                int index = timeSeries.Count - 1 - i; // Start from the most recent data point
                float weight = windowSize - i;       // Assign higher weights to recent points
                weightSum += weight;
                weightedSum += timeSeries[index] * weight;
            }

            return weightedSum / weightSum;
        }

        // Predicts the next n values using the weighted moving average
        public List<float> PredictNextValues(List<float> timeSeries, int numberOfPredictions)
        {
            List<float> predictions = new List<float>();

            for (int i = 0; i < numberOfPredictions; i++)
            {
                float nextValue = PredictNextValue(timeSeries);
                predictions.Add(nextValue);
                timeSeries.Add(nextValue); // Simulate updating the series with the predicted value
            }

            return predictions;
        }
    }

    // Fields for the Hinizagi Transform system
    private List<GalacticEvent> _events;
    private KMeansCluster _kMeansCluster;
    private TimeSeriesPredictor _predictor;

    // Unity lifecycle methods
    void Start()
    {
        _events = new List<GalacticEvent>();
        _kMeansCluster = new KMeansCluster(3); // Example: 3 clusters
        _predictor = new TimeSeriesPredictor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddSampleEvent();
            PerformHinizagiTransform();
        }
    }

    // Core Hinizagi Transform functionality
    private void PerformHinizagiTransform()
    {
        ClusterEvents();
        PerformTemporalCorrelations();
        PredictFutureOutcomes();
    }

    // Add a sample event
    private void AddSampleEvent()
    {
        var eventAttributes = new Dictionary<string, float> { { "Energy", UnityEngine.Random.Range(10f, 100f) } }; // Use UnityEngine.Random
        _events.Add(new GalacticEvent(
            "Event_" + _events.Count,
            new Vector3(UnityEngine.Random.Range(0f, 10f), UnityEngine.Random.Range(0f, 10f), UnityEngine.Random.Range(0f, 10f)),
            DateTime.Now,
            eventAttributes
        ));
    }

    private void ClusterEvents()
    {
        // Ensure we have events to cluster
        if (_events == null || _events.Count == 0)
        {
            Debug.LogWarning("No events available for clustering.");
            return;
        }

        // Perform KMeans clustering on the events
        _kMeansCluster.Fit(_events);

        // Log cluster details
        Debug.Log("Clusters Updated:");
        foreach (var cluster in _kMeansCluster.Clusters)
        {
            Debug.Log($"Cluster {cluster.Key}: {cluster.Value.Count} events");

            // Log detailed information for each event in the cluster
            foreach (var galacticEvent in cluster.Value)
            {
                string attributes = string.Join(", ", galacticEvent.Attributes.Select(a => $"{a.Key}: {a.Value}"));
                Debug.Log($"  Event ID: {galacticEvent.EventId}, Origin: {galacticEvent.SpatialOrigin}, Time: {galacticEvent.TimeStamp}, Attributes: {attributes}");
            }
        }

        // Optional visualization for debugging in Unity (e.g., draw cluster centers in the scene)
        VisualizeClusters();
    }

    // Visualize cluster centroids in the Unity scene
    private void VisualizeClusters()
    {
        foreach (var centroid in _kMeansCluster.Centroids)
        {
            // Draw a sphere at the centroid position for visualization
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = centroid;
            sphere.transform.localScale = Vector3.one * 0.5f; // Adjust size
            sphere.GetComponent<Renderer>().material.color = Color.red; // Set color for cluster centroids
        }

        Debug.Log("Cluster centroids visualized in the scene.");
    }


    // Perform temporal correlations
    private void PerformTemporalCorrelations()
    {
        for (int i = 0; i < _events.Count; i++)
        {
            for (int j = i + 1; j < _events.Count; j++)
            {
                float cosine = TemporalCorrelation.CosineSimilarity(_events[i], _events[j]);
                float tangent = TemporalCorrelation.TangentSimilarity(_events[i], _events[j]);
                Debug.Log($"Events {i} and {j} - Cosine: {cosine}, Tangent: {tangent}");
            }
        }
    }

    // Predict future outcomes
    private void PredictFutureOutcomes()
    {
        var timeSeries = new List<float> { 1, 2, 3, 4, 5 }; // Example data
        int predictionsCount = 3;

        // Predict the next values in the time series
        List<float> predictions = _predictor.PredictNextValues(timeSeries, predictionsCount);

        Debug.Log("Time Series Predictions:");
        foreach (var prediction in predictions)
        {
            Debug.Log($"Predicted Value: {prediction}");
        }
    }
}
