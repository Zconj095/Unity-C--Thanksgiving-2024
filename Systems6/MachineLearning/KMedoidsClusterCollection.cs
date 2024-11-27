using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class KMedoidsClusterCollection<T>
    {
        private List<T[]> _centroids;
        private Func<T[], T[], double> _distanceFunction;
        private List<List<T[]>> _clusters;

        public KMedoidsClusterCollection(int k, Func<T[], T[], double> distanceFunction)
        {
            if (k <= 0)
                throw new ArgumentException("Number of clusters must be greater than zero.", nameof(k));

            _centroids = new List<T[]>(k);
            _clusters = new List<List<T[]>>(k);
            for (int i = 0; i < k; i++)
            {
                _clusters.Add(new List<T[]>());
            }

            _distanceFunction = distanceFunction ?? DefaultDistanceFunction;
        }

        public List<T[]> Centroids => _centroids;

        public List<List<T[]>> Clusters => _clusters;

        public int Count => _centroids.Count;

        public void Randomize(IEnumerable<T[]> data)
        {
            var random = new System.Random();
            _centroids.Clear();
            foreach (var point in data.OrderBy(x => random.Next()).Take(_centroids.Capacity))
            {
                _centroids.Add(point);
            }
        }

        public void AssignClusters(IEnumerable<T[]> data)
        {
            foreach (var cluster in _clusters)
            {
                cluster.Clear();
            }

            foreach (var point in data)
            {
                int bestClusterIndex = FindNearestCentroid(point);
                _clusters[bestClusterIndex].Add(point);
            }
        }

        public double ComputeDistortion()
        {
            double totalDistortion = 0;
            for (int i = 0; i < _clusters.Count; i++)
            {
                foreach (var point in _clusters[i])
                {
                    totalDistortion += _distanceFunction(point, _centroids[i]);
                }
            }

            return totalDistortion;
        }

        public void UpdateCentroids()
        {
            for (int i = 0; i < _clusters.Count; i++)
            {
                if (_clusters[i].Count == 0)
                    continue;

                var newCentroid = ComputeCentroid(_clusters[i]);
                _centroids[i] = newCentroid;
            }
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

        private T[] ComputeCentroid(List<T[]> cluster)
        {
            if (typeof(T) == typeof(double))
            {
                int dimensions = cluster[0].Length;
                var centroid = new double[dimensions];

                foreach (var point in cluster)
                {
                    for (int i = 0; i < dimensions; i++)
                    {
                        centroid[i] += Convert.ToDouble(point[i]);
                    }
                }

                for (int i = 0; i < dimensions; i++)
                {
                    centroid[i] /= cluster.Count;
                }

                return centroid.Cast<T>().ToArray();
            }

            throw new InvalidOperationException("Centroid computation is only implemented for numerical types.");
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
}
