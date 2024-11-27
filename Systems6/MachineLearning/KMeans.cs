using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
namespace EdgeLoreMachineLearning
{
    [Serializable]
    public class MLKMeans
    {
        public int K { get; private set; }
        public List<double[]> Centroids { get; private set; }
        public int[] Labels { get; private set; }
        public double Tolerance { get; set; } = 1e-5;
        public int MaxIterations { get; set; } = 100;
        public double Error { get; private set; }
        public Func<double[], double[], double> DistanceFunction { get; set; }

        public MLKMeans(int k, Func<double[], double[], double> distanceFunction = null)
        {
            if (k <= 0)
                throw new ArgumentOutOfRangeException(nameof(k), "Number of clusters must be greater than zero.");

            K = k;
            DistanceFunction = distanceFunction ?? EuclideanDistance;
        }

        public void Fit(double[][] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentException("Input data cannot be null or empty.");
            if (data.Length < K)
                throw new ArgumentException("Number of data points must be greater than or equal to the number of clusters.");

            // Initialize centroids randomly
            var random = new System.Random();
            Centroids = data.OrderBy(_ => random.Next()).Take(K).ToList();
            Labels = new int[data.Length];

            for (int iter = 0; iter < MaxIterations; iter++)
            {
                // Step 1: Assign data points to the nearest centroid
                AssignClusters(data);

                // Step 2: Update centroids based on cluster assignments
                var newCentroids = UpdateCentroids(data);

                // Step 3: Check for convergence
                if (HasConverged(Centroids, newCentroids))
                    break;

                Centroids = newCentroids;
            }

            // Calculate final error
            Error = CalculateError(data);
        }

        private void AssignClusters(double[][] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Labels[i] = GetNearestCentroid(data[i]);
            }
        }

        private int GetNearestCentroid(double[] point)
        {
            int nearest = 0;
            double minDistance = double.MaxValue;

            for (int i = 0; i < Centroids.Count; i++)
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

        private List<double[]> UpdateCentroids(double[][] data)
        {
            var newCentroids = new List<double[]>(K);
            for (int i = 0; i < K; i++)
            {
                var clusterPoints = data.Where((_, idx) => Labels[idx] == i).ToArray();

                if (clusterPoints.Length == 0)
                {
                    // If a cluster is empty, retain the previous centroid
                    newCentroids.Add(Centroids[i]);
                }
                else
                {
                    // Compute the mean of the cluster points
                    var newCentroid = new double[data[0].Length];
                    foreach (var point in clusterPoints)
                    {
                        for (int d = 0; d < point.Length; d++)
                        {
                            newCentroid[d] += point[d];
                        }
                    }

                    for (int d = 0; d < newCentroid.Length; d++)
                    {
                        newCentroid[d] /= clusterPoints.Length;
                    }

                    newCentroids.Add(newCentroid);
                }
            }

            return newCentroids;
        }

        private bool HasConverged(List<double[]> oldCentroids, List<double[]> newCentroids)
        {
            for (int i = 0; i < K; i++)
            {
                if (DistanceFunction(oldCentroids[i], newCentroids[i]) > Tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        private double CalculateError(double[][] data)
        {
            double totalError = 0;
            for (int i = 0; i < data.Length; i++)
            {
                totalError += DistanceFunction(data[i], Centroids[Labels[i]]);
            }
            return totalError / data.Length;
        }

        private double EuclideanDistance(double[] pointA, double[] pointB)
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
