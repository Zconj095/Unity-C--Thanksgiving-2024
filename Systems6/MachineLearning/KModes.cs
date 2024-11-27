using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   K-Modes algorithm for clustering categorical data.
    /// </summary>
    public class KModes<T>
    {
        private int _k; // Number of clusters
        private Func<T[], T[], double> _distanceFunction;
        private int _maxIterations;
        private double _tolerance;

        private List<T[]> _centroids;

        public KModes(int k, Func<T[], T[], double> distanceFunction)
        {
            if (k <= 0)
                throw new ArgumentException("Number of clusters must be greater than zero.", nameof(k));

            _k = k;
            _distanceFunction = distanceFunction ?? DefaultDistanceFunction;
            _maxIterations = 100;
            _tolerance = 1e-5;
            _centroids = new List<T[]>(k);
        }

        public List<T[]> Centroids => _centroids;

        public void Fit(T[][] data)
        {
            InitializeCentroids(data);

            for (int iteration = 0; iteration < _maxIterations; iteration++)
            {
                var clusters = AssignClusters(data);

                bool centroidsChanged = UpdateCentroids(clusters);

                if (!centroidsChanged)
                    break;
            }
        }

        private void InitializeCentroids(T[][] data)
        {
            var random = new System.Random();
            _centroids.Clear();

            foreach (var point in data.OrderBy(x => random.Next()).Take(_k))
            {
                _centroids.Add(point);
            }
        }

        private List<List<T[]>> AssignClusters(T[][] data)
        {
            var clusters = new List<List<T[]>>(_k);
            for (int i = 0; i < _k; i++)
            {
                clusters.Add(new List<T[]>());
            }

            foreach (var point in data)
            {
                int bestClusterIndex = FindNearestCentroid(point);
                clusters[bestClusterIndex].Add(point);
            }

            return clusters;
        }

        private bool UpdateCentroids(List<List<T[]>> clusters)
        {
            bool centroidsChanged = false;

            for (int i = 0; i < _k; i++)
            {
                if (clusters[i].Count == 0)
                    continue;

                var newCentroid = ComputeMode(clusters[i]);
                if (!newCentroid.SequenceEqual(_centroids[i]))
                {
                    _centroids[i] = newCentroid;
                    centroidsChanged = true;
                }
            }

            return centroidsChanged;
        }

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

        private T[] ComputeMode(List<T[]> cluster)
        {
            int dimension = cluster[0].Length;
            var mode = new T[dimension];

            for (int i = 0; i < dimension; i++)
            {
                var column = cluster.Select(point => point[i]).ToArray();
                mode[i] = FindMode(column);
            }

            return mode;
        }

        private T FindMode(T[] values)
        {
            return values
                .GroupBy(v => v)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }

        private static double DefaultDistanceFunction(T[] a, T[] b)
        {
            if (typeof(T) == typeof(string) || typeof(T) == typeof(int))
            {
                double distance = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (!EqualityComparer<T>.Default.Equals(a[i], b[i]))
                    {
                        distance += 1;
                    }
                }

                return distance;
            }

            throw new InvalidOperationException("Default distance function supports only categorical types. Provide a custom distance function.");
        }
    }

    /// <summary>
    /// Specialized KModes implementation for integer arrays.
    /// </summary>
    public class KModes : KModes<int>
    {
        public KModes(int k) : base(k, DefaultDistanceFunction) { }

        private static double DefaultDistanceFunction(int[] a, int[] b)
        {
            double distance = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    distance += 1;
                }
            }
            return distance;
        }
    }
}
