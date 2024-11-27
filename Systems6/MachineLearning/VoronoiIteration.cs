using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class VoronoiIteration<T>
    {
        private List<T[]> _centroids;
        private Func<T[], T[], double> _distanceFunction;
        private int _maxIterations;
        private double _tolerance;
        private int _k;

        public VoronoiIteration(int k, Func<T[], T[], double> distanceFunction)
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

                var newCentroid = ComputeMedoid(clusters[i]);
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

        private T[] ComputeMedoid(List<T[]> cluster)
        {
            double minTotalDistance = double.MaxValue;
            T[] medoid = null;

            foreach (var candidate in cluster)
            {
                double totalDistance = cluster.Sum(point => _distanceFunction(candidate, point));

                if (totalDistance < minTotalDistance)
                {
                    minTotalDistance = totalDistance;
                    medoid = candidate;
                }
            }

            return medoid;
        }

        private static double DefaultDistanceFunction(T[] a, T[] b)
        {
            if (typeof(T) == typeof(double))
            {
                double distance = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    distance += Math.Pow(Convert.ToDouble(a[i]) - Convert.ToDouble(b[i]), 2);
                }

                return Math.Sqrt(distance);
            }

            throw new InvalidOperationException("Default distance function supports only numerical types. Provide a custom distance function.");
        }
    }

    public class VoronoiIteration : VoronoiIteration<double>
    {
        public VoronoiIteration(int k) : base(k, DefaultDistanceFunction)
        {
        }

        private static double DefaultDistanceFunction(double[] a, double[] b)
        {
            double distance = 0;
            for (int i = 0; i < a.Length; i++)
            {
                distance += (a[i] - b[i]) * (a[i] - b[i]);
            }

            return Math.Sqrt(distance);
        }
    }
}
