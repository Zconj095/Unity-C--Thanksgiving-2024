using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Cluster collection for KModes algorithm.
    /// </summary>
    public class KModesClusterCollection<T>
    {
        private readonly List<T[]> _centroids;
        private readonly Func<T[], T[], double> _distanceFunction;

        public KModesClusterCollection(int k, Func<T[], T[], double> distanceFunction)
        {
            if (k <= 0)
                throw new ArgumentException("Number of clusters must be greater than zero.", nameof(k));
            if (distanceFunction == null)
                throw new ArgumentNullException(nameof(distanceFunction));

            _centroids = new List<T[]>(k);
            _distanceFunction = distanceFunction;
        }

        /// <summary>
        /// Gets or sets the clusters' centroids.
        /// </summary>
        public List<T[]> Centroids => _centroids;

        /// <summary>
        /// Gets or sets the distance function used to measure the distance
        /// between a point and the cluster centroid in this clustering definition.
        /// </summary>
        public Func<T[], T[], double> DistanceFunction => _distanceFunction;

        /// <summary>
        /// Assigns data points to their nearest cluster centroid.
        /// </summary>
        public int[] AssignClusters(T[][] data)
        {
            var labels = new int[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                labels[i] = FindNearestCentroid(data[i]);
            }

            return labels;
        }

        /// <summary>
        /// Calculates the average distance from the data points to their assigned clusters.
        /// </summary>
        public double CalculateDistortion(T[][] data, int[] labels)
        {
            double distortion = 0.0;

            for (int i = 0; i < data.Length; i++)
            {
                var centroid = _centroids[labels[i]];
                distortion += _distanceFunction(data[i], centroid);
            }

            return distortion / data.Length;
        }

        /// <summary>
        /// Randomly initializes centroids from the dataset.
        /// </summary>
        public void InitializeCentroids(T[][] data, int k)
        {
            var random = new System.Random();
            _centroids.Clear();

            foreach (var point in data.OrderBy(x => random.Next()).Take(k))
            {
                _centroids.Add(point);
            }
        }

        /// <summary>
        /// Computes a score for the association between an input vector and a cluster centroid.
        /// </summary>
        public double Score(T[] input, int clusterIndex)
        {
            return -_distanceFunction(input, _centroids[clusterIndex]);
        }

        /// <summary>
        /// Finds the nearest centroid to the given data point.
        /// </summary>
        private int FindNearestCentroid(T[] point)
        {
            double minDistance = double.MaxValue;
            int nearestIndex = 0;

            for (int i = 0; i < _centroids.Count; i++)
            {
                double distance = _distanceFunction(point, _centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestIndex = i;
                }
            }

            return nearestIndex;
        }
    }
}
