using System;
using System.Collections.Generic;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    public class BinarySplit
    {
        public int K { get; private set; }
        public List<double[]> Centroids { get; private set; }
        public int MaxIterations { get; set; } = 100;
        public double Tolerance { get; set; } = 1e-3;
        public Func<double[], double[], double> Distance { get; set; }
        public bool ComputeProportions { get; set; }

        private List<double[][]> clusters;
        private double[] distortions;

        public BinarySplit(int k, Func<double[], double[], double> distance = null)
        {
            if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k), "Number of clusters must be greater than zero.");
            K = k;
            Distance = distance ?? DefaultDistance;
        }

        public void Fit(double[][] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Input data cannot be null or empty.");
            if (data.Length < K) throw new ArgumentException("Number of data points must be greater than or equal to the number of clusters.");

            clusters = new List<double[][]>(K) { data };
            distortions = new double[K];
            Centroids = new List<double[]>(K);

            for (int i = 0; i < K; i++)
            {
                Centroids.Add(new double[data[0].Length]);
            }

            // Step 1: Start with all points in one cluster
            distortions[0] = ComputeClusterDistortion(data, Centroids[0]);

            // Step 2: Repeat (K-1) times
            for (int current = 1; current < K; current++)
            {
                // Step 3: Choose cluster with largest distortion
                int chosen = Array.IndexOf(distortions, distortions.Max());

                // Step 4: Split the cluster
                var (leftCluster, rightCluster) = SplitCluster(clusters[chosen]);

                // Replace the old cluster and add the new one
                clusters[chosen] = leftCluster;
                clusters.Add(rightCluster);

                // Update centroids
                Centroids[chosen] = ComputeCentroid(leftCluster);
                Centroids[current] = ComputeCentroid(rightCluster);

                // Update distortions
                distortions[chosen] = ComputeClusterDistortion(leftCluster, Centroids[chosen]);
                distortions[current] = ComputeClusterDistortion(rightCluster, Centroids[current]);
            }
        }

        private (double[][], double[][]) SplitCluster(double[][] cluster)
        {
            // Use simple k-means with k=2 for splitting
            var kmeans = new KMeans(2, Distance);
            kmeans.Fit(cluster);

            var leftCluster = cluster.Where((_, i) => kmeans.Labels[i] == 0).ToArray();
            var rightCluster = cluster.Where((_, i) => kmeans.Labels[i] == 1).ToArray();

            return (leftCluster, rightCluster);
        }

        private double ComputeClusterDistortion(double[][] cluster, double[] centroid)
        {
            double distortion = 0;
            foreach (var point in cluster)
            {
                distortion += Math.Pow(Distance(point, centroid), 2);
            }
            return distortion;
        }

        private double[] ComputeCentroid(double[][] cluster)
        {
            int dimensions = cluster[0].Length;
            var centroid = new double[dimensions];

            foreach (var point in cluster)
            {
                for (int d = 0; d < dimensions; d++)
                {
                    centroid[d] += point[d];
                }
            }

            for (int d = 0; d < dimensions; d++)
            {
                centroid[d] /= cluster.Length;
            }

            return centroid;
        }

        private double DefaultDistance(double[] pointA, double[] pointB)
        {
            double sum = 0;
            for (int i = 0; i < pointA.Length; i++)
            {
                sum += Math.Pow(pointA[i] - pointB[i], 2);
            }
            return Math.Sqrt(sum);
        }
    }

    public class KMeans
    {
        public int K { get; }
        public List<double[]> Centroids { get; private set; }
        public int[] Labels { get; private set; }
        public int MaxIterations { get; set; } = 100;
        public Func<double[], double[], double> Distance { get; set; }

        public KMeans(int k, Func<double[], double[], double> distance = null)
        {
            if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k), "Number of clusters must be greater than zero.");
            K = k;
            Distance = distance ?? DefaultDistance;
        }

        public void Fit(double[][] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Input data cannot be null or empty.");
            if (data.Length < K) throw new ArgumentException("Number of data points must be greater than or equal to the number of clusters.");

            var random = new Random();
            Centroids = data.OrderBy(_ => random.Next()).Take(K).ToList();
            Labels = new int[data.Length];

            for (int iter = 0; iter < MaxIterations; iter++)
            {
                // Assign points to the nearest centroid
                for (int i = 0; i < data.Length; i++)
                {
                    Labels[i] = GetNearestCentroid(data[i]);
                }

                // Update centroids
                var newCentroids = new List<double[]>(K);
                for (int k = 0; k < K; k++)
                {
                    var clusterPoints = data.Where((_, index) => Labels[index] == k).ToArray();
                    newCentroids.Add(ComputeCentroid(clusterPoints));
                }

                if (HasConverged(Centroids, newCentroids))
                {
                    break;
                }

                Centroids = newCentroids;
            }
        }

        private int GetNearestCentroid(double[] point)
        {
            int nearest = 0;
            double minDistance = double.MaxValue;

            for (int i = 0; i < Centroids.Count; i++)
            {
                double dist = Distance(point, Centroids[i]);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    nearest = i;
                }
            }

            return nearest;
        }

        private double[] ComputeCentroid(double[][] cluster)
        {
            int dimensions = cluster[0].Length;
            var centroid = new double[dimensions];

            foreach (var point in cluster)
            {
                for (int d = 0; d < dimensions; d++)
                {
                    centroid[d] += point[d];
                }
            }

            for (int d = 0; d < dimensions; d++)
            {
                centroid[d] /= cluster.Length;
            }

            return centroid;
        }

        private bool HasConverged(List<double[]> oldCentroids, List<double[]> newCentroids)
        {
            for (int i = 0; i < K; i++)
            {
                if (Distance(oldCentroids[i], newCentroids[i]) > 1e-6)
                {
                    return false;
                }
            }
            return true;
        }

        private double DefaultDistance(double[] pointA, double[] pointB)
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
