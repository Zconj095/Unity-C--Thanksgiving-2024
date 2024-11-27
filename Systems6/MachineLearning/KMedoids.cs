using System;
using System.Collections.Generic;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    public class KMedoids<T>
    {
        private List<List<T>> _clusters;
        private Func<T, T, double> _distanceFunction;
        private int _maxIterations;
        private double _tolerance;
        private int _iterations;
        private List<T> _medoids;

        public KMedoids(int k, Func<T, T, double> distanceFunction = null, int maxIterations = 100, double tolerance = 1e-5)
        {
            if (k <= 0)
                throw new ArgumentException("Number of clusters must be greater than zero.", nameof(k));

            _clusters = new List<List<T>>(k);
            for (int i = 0; i < k; i++)
            {
                _clusters.Add(new List<T>());
            }

            _distanceFunction = distanceFunction ?? DefaultDistanceFunction;
            _maxIterations = maxIterations;
            _tolerance = tolerance;
            _medoids = new List<T>(k);
        }

        public List<T> Medoids => _medoids;

        public int Iterations => _iterations;

        public void Fit(List<T> data)
        {
            if (data.Count < _medoids.Capacity)
                throw new ArgumentException("Not enough data points for the number of clusters.", nameof(data));

            InitializeMedoids(data);
            double previousCost = double.MaxValue;

            for (_iterations = 0; _iterations < _maxIterations; _iterations++)
            {
                AssignClusters(data);
                double cost = ComputeTotalCost(data);

                if (Math.Abs(previousCost - cost) <= _tolerance)
                    break;

                previousCost = cost;
                UpdateMedoids();
            }
        }

        private void InitializeMedoids(List<T> data)
        {
            var random = new Random();
            var selectedIndices = new HashSet<int>();

            for (int i = 0; i < _medoids.Capacity; i++)
            {
                int index;

                do
                {
                    index = random.Next(data.Count);
                } while (selectedIndices.Contains(index));

                selectedIndices.Add(index);
                _medoids.Add(data[index]);
            }
        }

        private void AssignClusters(List<T> data)
        {
            foreach (var cluster in _clusters)
            {
                cluster.Clear();
            }

            foreach (var point in data)
            {
                int bestClusterIndex = FindNearestMedoid(point);
                _clusters[bestClusterIndex].Add(point);
            }
        }

        private int FindNearestMedoid(T point)
        {
            double minDistance = double.MaxValue;
            int nearestIndex = 0;

            for (int i = 0; i < _medoids.Count; i++)
            {
                double distance = _distanceFunction(point, _medoids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestIndex = i;
                }
            }

            return nearestIndex;
        }

        private double ComputeTotalCost(List<T> data)
        {
            double totalCost = 0;

            for (int i = 0; i < _clusters.Count; i++)
            {
                foreach (var point in _clusters[i])
                {
                    totalCost += _distanceFunction(point, _medoids[i]);
                }
            }

            return totalCost;
        }

        private void UpdateMedoids()
        {
            for (int i = 0; i < _clusters.Count; i++)
            {
                double minCost = double.MaxValue;
                T bestMedoid = default;

                foreach (var candidate in _clusters[i])
                {
                    double cost = ComputeClusterCost(candidate, _clusters[i]);

                    if (cost < minCost)
                    {
                        minCost = cost;
                        bestMedoid = candidate;
                    }
                }

                _medoids[i] = bestMedoid;
            }
        }

        private double ComputeClusterCost(T candidate, List<T> cluster)
        {
            double cost = 0;

            foreach (var point in cluster)
            {
                cost += _distanceFunction(candidate, point);
            }

            return cost;
        }

        private static double DefaultDistanceFunction(T a, T b)
        {
            if (typeof(T) == typeof(double))
            {
                return Math.Abs(Convert.ToDouble(a) - Convert.ToDouble(b));
            }

            throw new InvalidOperationException("Default distance function supports only numerical types. Provide a custom distance function.");
        }
    }

    public class KMedoids : KMedoids<double>
    {
        public KMedoids(int k, int maxIterations = 100, double tolerance = 1e-5)
            : base(k, null, maxIterations, tolerance)
        {
        }
    }
}
