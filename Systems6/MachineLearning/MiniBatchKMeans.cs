using System;
using System.Collections.Generic;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    [Serializable]
    public class MiniBatchKMeans
    {
        public int K { get; private set; }
        public int BatchSize { get; private set; }
        public int? InitializationBatchSize { get; private set; }
        public int NumberOfInitializations { get; private set; } = 1;
        public double[][] Centroids { get; private set; }
        public int[] Labels { get; private set; }
        public Func<double[], double[], double> DistanceFunction { get; private set; }
        public double Tolerance { get; set; } = 1e-5;

        public MiniBatchKMeans(int k, int batchSize, Func<double[], double[], double> distanceFunction = null)
        {
            if (k <= 0)
                throw new ArgumentOutOfRangeException(nameof(k), "Number of clusters must be greater than zero.");
            if (batchSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(batchSize), "Batch size must be greater than zero.");

            K = k;
            BatchSize = batchSize;
            DistanceFunction = distanceFunction ?? EuclideanDistance;
            Centroids = new double[k][];
        }

        public void SetInitializationBatchSize(int? size)
        {
            if (size.HasValue && size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Initialization batch size must be positive.");
            InitializationBatchSize = size;
        }

        public void SetNumberOfInitializations(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Number of initializations must be positive.");
            NumberOfInitializations = value;
        }

        public void Fit(double[][] data)
        {
            if (data == null || data.Length < K)
                throw new ArgumentException("Insufficient data points. There should be more data points than the number of clusters.");

            InitializationBatchSize ??= Math.Min(3 * K, data.Length);

            if (InitializationBatchSize.Value < K)
                throw new ArgumentException("Initialization batch size must be at least as large as the number of clusters.");

            Labels = new int[data.Length];

            InitializeCentroids(data);

            bool shouldStop = false;
            var count = new double[K];
            var newCentroids = Centroids.Select(c => c.ToArray()).ToArray();

            while (!shouldStop)
            {
                // Randomly select a mini-batch
                var batchIndices = CreateBatch(data.Length, BatchSize);

                // Assign points in the batch to the nearest centroids
                foreach (var idx in batchIndices)
                {
                    Labels[idx] = GetNearestCentroid(data[idx]);
                }

                // Update centroids using the mini-batch
                foreach (var idx in batchIndices)
                {
                    int clusterIndex = Labels[idx];
                    count[clusterIndex]++;
                    double learningRate = 1.0 / count[clusterIndex];

                    for (int d = 0; d < data[idx].Length; d++)
                    {
                        newCentroids[clusterIndex][d] =
                            (1 - learningRate) * newCentroids[clusterIndex][d] +
                            learningRate * data[idx][d];
                    }
                }

                // Check for convergence
                shouldStop = HasConverged(Centroids, newCentroids);

                // Update centroids
                for (int i = 0; i < K; i++)
                {
                    Centroids[i] = newCentroids[i].ToArray();
                }
            }
        }

        private void InitializeCentroids(double[][] data)
        {
            var bestCentroids = new double[K][];
            double bestDistortion = double.MaxValue;

            for (int init = 0; init < NumberOfInitializations; init++)
            {
                // Generate initial centroids randomly
                var indices = CreateBatch(data.Length, K);
                for (int i = 0; i < K; i++)
                {
                    Centroids[i] = data[indices[i]].ToArray();
                }

                // Calculate distortion
                double distortion = CalculateDistortion(data);
                if (distortion < bestDistortion)
                {
                    bestDistortion = distortion;
                    bestCentroids = Centroids.Select(c => c.ToArray()).ToArray();
                }
            }

            Centroids = bestCentroids;
        }

        private int[] CreateBatch(int totalPoints, int batchSize)
        {
            var indices = new HashSet<int>();
            var random = new System.Random();

            while (indices.Count < batchSize)
            {
                indices.Add(random.Next(totalPoints));
            }

            return indices.ToArray();
        }

        private double CalculateDistortion(double[][] data)
        {
            double distortion = 0;
            foreach (var point in data)
            {
                int clusterIndex = GetNearestCentroid(point);
                distortion += Math.Pow(DistanceFunction(point, Centroids[clusterIndex]), 2);
            }
            return distortion;
        }

        private int GetNearestCentroid(double[] point)
        {
            int nearest = 0;
            double minDistance = double.MaxValue;

            for (int i = 0; i < Centroids.Length; i++)
            {
                double distance = DistanceFunction(point, Centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = i;
                }
            }

            return nearest;
        }

        private bool HasConverged(double[][] oldCentroids, double[][] newCentroids)
        {
            for (int i = 0; i < oldCentroids.Length; i++)
            {
                if (DistanceFunction(oldCentroids[i], newCentroids[i]) > Tolerance)
                {
                    return false;
                }
            }
            return true;
        }

        private static double EuclideanDistance(double[] pointA, double[] pointB)
        {
            double sum = 0;
            for (int i = 0; i < pointA.Length; i++)
            {
                sum += Math.Pow(pointA[i] - pointB[i], 2);
            }
            return Math.Sqrt(sum);
        }
    }
}
