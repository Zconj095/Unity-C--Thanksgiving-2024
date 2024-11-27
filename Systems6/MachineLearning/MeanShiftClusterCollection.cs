using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Mean shift cluster collection for Unity.
    /// </summary>
    public class MeanShiftClusterCollection
    {
        private readonly List<Vector3> modes;
        private readonly Dictionary<int, Vector3> clusters;
        private readonly List<float> proportions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeanShiftClusterCollection"/> class.
        /// </summary>
        /// <param name="modes">List of cluster centers.</param>
        /// <param name="data">Original data points.</param>
        public MeanShiftClusterCollection(List<Vector3> modes, List<Vector3> data)
        {
            this.modes = modes;
            clusters = new Dictionary<int, Vector3>();
            proportions = new List<float>();

            AssignClusters(data);
        }

        /// <summary>
        /// Gets the cluster centers.
        /// </summary>
        public List<Vector3> Modes => modes;

        /// <summary>
        /// Gets the number of clusters in the collection.
        /// </summary>
        public int Count => modes.Count;

        /// <summary>
        /// Gets the proportion of points in each cluster.
        /// </summary>
        public List<float> Proportions => proportions;

        /// <summary>
        /// Assigns data points to clusters based on their proximity to the modes.
        /// </summary>
        /// <param name="data">List of data points to assign.</param>
        private void AssignClusters(List<Vector3> data)
        {
            var clusterCounts = new int[modes.Count];

            foreach (var point in data)
            {
                int closestModeIndex = -1;
                float minDistance = float.MaxValue;

                // Find the nearest mode for this point
                for (int i = 0; i < modes.Count; i++)
                {
                    float distance = Vector3.Distance(point, modes[i]);
                    if (distance < minDistance)
                    {
                        closestModeIndex = i;
                        minDistance = distance;
                    }
                }

                // Assign point to the nearest cluster
                if (closestModeIndex >= 0)
                {
                    clusters[closestModeIndex] = modes[closestModeIndex];
                    clusterCounts[closestModeIndex]++;
                }
            }

            // Calculate proportions
            float totalPoints = data.Count;
            for (int i = 0; i < clusterCounts.Length; i++)
            {
                proportions.Add(clusterCounts[i] / totalPoints);
            }
        }

        /// <summary>
        /// Transforms data points into distances to their nearest clusters.
        /// </summary>
        /// <param name="data">The input data points.</param>
        /// <returns>List of distances from points to their respective cluster centers.</returns>
        public List<float> Transform(List<Vector3> data)
        {
            var distances = new List<float>();

            foreach (var point in data)
            {
                float minDistance = float.MaxValue;

                foreach (var mode in modes)
                {
                    float distance = Vector3.Distance(point, mode);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }

                distances.Add(minDistance);
            }

            return distances;
        }

        /// <summary>
        /// Calculates the average distance from points to their nearest cluster centers (distortion).
        /// </summary>
        /// <param name="data">The data points.</param>
        /// <returns>The average distortion.</returns>
        public float Distortion(List<Vector3> data)
        {
            float totalDistance = 0f;

            foreach (var point in data)
            {
                float minDistance = float.MaxValue;

                foreach (var mode in modes)
                {
                    float distance = Vector3.Distance(point, mode);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }

                totalDistance += minDistance;
            }

            return totalDistance / data.Count;
        }
    }
}
