using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Mean Shift clustering algorithm implementation for Unity.
    /// </summary>
    public class MeanShift
    {
        private readonly float bandwidth;
        private readonly Func<Vector3, Vector3, float> distanceFunc;

        private readonly List<Vector3> points;
        private readonly List<Vector3> modes;

        /// <summary>
        ///   Constructor for the Mean Shift algorithm.
        /// </summary>
        /// <param name="bandwidth">The radius for clustering.</param>
        /// <param name="distanceFunc">Distance function (e.g., Euclidean distance).</param>
        public MeanShift(float bandwidth, Func<Vector3, Vector3, float> distanceFunc)
        {
            if (bandwidth <= 0)
                throw new ArgumentException("Bandwidth must be positive.", nameof(bandwidth));

            this.bandwidth = bandwidth;
            this.distanceFunc = distanceFunc ?? throw new ArgumentNullException(nameof(distanceFunc));

            points = new List<Vector3>();
            modes = new List<Vector3>();
        }

        /// <summary>
        /// Adds data points to the Mean Shift algorithm.
        /// </summary>
        /// <param name="dataPoints">Points to cluster.</param>
        public void AddPoints(IEnumerable<Vector3> dataPoints)
        {
            points.AddRange(dataPoints);
        }

        /// <summary>
        ///   Runs the clustering algorithm and identifies cluster centers (modes).
        /// </summary>
        /// <param name="maxIterations">Maximum number of iterations for convergence.</param>
        /// <param name="tolerance">Convergence tolerance.</param>
        public void Run(int maxIterations = 100, float tolerance = 0.01f)
        {
            if (points.Count == 0)
                throw new InvalidOperationException("No data points added to cluster.");

            var currentPoints = new List<Vector3>(points);

            for (int iter = 0; iter < maxIterations; iter++)
            {
                var newPoints = new List<Vector3>();

                foreach (var point in currentPoints)
                {
                    var neighbors = points.Where(p => distanceFunc(point, p) <= bandwidth).ToList();

                    // Compute mean of neighbors
                    var mean = Vector3.zero;
                    foreach (var neighbor in neighbors)
                        mean += neighbor;

                    mean /= neighbors.Count;

                    newPoints.Add(mean);
                }

                // Check for convergence
                float maxShift = 0f;
                for (int i = 0; i < currentPoints.Count; i++)
                {
                    float shift = distanceFunc(currentPoints[i], newPoints[i]);
                    if (shift > maxShift)
                        maxShift = shift;
                }

                currentPoints = newPoints;

                if (maxShift < tolerance)
                    break;
            }

            // Identify unique modes
            modes.Clear();
            foreach (var point in currentPoints)
            {
                if (!modes.Any(mode => distanceFunc(mode, point) <= tolerance))
                {
                    modes.Add(point);
                }
            }
        }

        /// <summary>
        ///   Gets the cluster centers (modes) identified by the algorithm.
        /// </summary>
        public IEnumerable<Vector3> GetModes()
        {
            return modes;
        }

        /// <summary>
        ///   Assigns each point to a cluster based on proximity to a mode.
        /// </summary>
        public Dictionary<Vector3, int> AssignClusters()
        {
            var clusterAssignments = new Dictionary<Vector3, int>();

            for (int i = 0; i < points.Count; i++)
            {
                Vector3 point = points[i];
                int clusterIndex = -1;
                float minDistance = float.MaxValue;

                for (int j = 0; j < modes.Count; j++)
                {
                    float distance = distanceFunc(point, modes[j]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        clusterIndex = j;
                    }
                }

                clusterAssignments[point] = clusterIndex;
            }

            return clusterAssignments;
        }
    }
}
