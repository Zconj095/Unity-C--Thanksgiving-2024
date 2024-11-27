using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    [Serializable]
    public class KMeansClusterCollection : IEnumerable<KMeansClusterCollection.KMeansCluster>
    {
        private readonly List<KMeansCluster> clusters;
        private readonly Func<double[], double[], double> distanceFunction;
        private readonly double[][][] covariances;

        public KMeansClusterCollection(int k, Func<double[], double[], double> distanceFunction)
        {
            if (k <= 0)
                throw new ArgumentOutOfRangeException(nameof(k), "Number of clusters must be greater than zero.");

            this.distanceFunction = distanceFunction ?? EuclideanDistance;
            clusters = new List<KMeansCluster>(k);
            covariances = new double[k][][];

            for (int i = 0; i < k; i++)
            {
                clusters.Add(new KMeansCluster(this, i));
                covariances[i] = null; // Covariance will be calculated when needed
            }
        }

        public double[][] Centroids
        {
            get => clusters.Select(c => c.Centroid).ToArray();
            set
            {
                if (value.Length != clusters.Count)
                    throw new ArgumentException("Number of centroids must match the number of clusters.");

                for (int i = 0; i < clusters.Count; i++)
                {
                    clusters[i].Centroid = value[i];
                }
            }
        }

        public double[] Proportions => clusters.Select(c => c.Proportion).ToArray();

        public double[][][] Covariances => covariances;

        public int Count => clusters.Count;

        public KMeansCluster this[int index] => clusters[index];

        public double Distortion(double[][] data, int[] labels = null, double[] weights = null)
        {
            if (labels == null)
            {
                labels = data.Select(FindNearestCluster).ToArray();
            }

            double distortion = 0;
            for (int i = 0; i < data.Length; i++)
            {
                var clusterIndex = labels[i];
                distortion += Math.Pow(distanceFunction(data[i], clusters[clusterIndex].Centroid), 2);
            }

            return distortion / data.Length;
        }

        public double[][] Transform(double[][] data, double[] weights = null, double[][] result = null)
        {
            var transformed = new double[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                transformed[i] = clusters.Select(c => distanceFunction(data[i], c.Centroid)).ToArray();
            }
            return transformed;
        }

        public void Randomize(double[][] points, Seeding strategy)
        {
            var random = new System.Random();

            switch (strategy)
            {
                case Seeding.Fixed:
                    // Do nothing for fixed seeding
                    break;

                case Seeding.Uniform:
                    for (int i = 0; i < clusters.Count; i++)
                    {
                        clusters[i].Centroid = points[random.Next(points.Length)];
                    }
                    break;

                case Seeding.KMeansPlusPlus:
                    InitializeKMeansPlusPlus(points, random);
                    break;

                case Seeding.PamBuild:
                    throw new NotImplementedException("PamBuild seeding is not implemented.");
            }
        }

        private void InitializeKMeansPlusPlus(double[][] points, Random random)
        {
            // Pick the first centroid randomly
            clusters[0].Centroid = points[random.Next(points.Length)];

            for (int i = 1; i < clusters.Count; i++)
            {
                // Compute distances from each point to the nearest centroid
                var distances = points.Select(point =>
                    clusters.Take(i).Min(c => distanceFunction(point, c.Centroid))
                ).ToArray();

                // Select the next centroid with probability proportional to the square of the distance
                double total = distances.Sum(d => d * d);
                double threshold = random.NextDouble() * total;

                for (int j = 0; j < distances.Length; j++)
                {
                    threshold -= distances[j] * distances[j];
                    if (threshold <= 0)
                    {
                        clusters[i].Centroid = points[j];
                        break;
                    }
                }
            }
        }

        private int FindNearestCluster(double[] point)
        {
            int nearest = 0;
            double minDistance = double.MaxValue;

            for (int i = 0; i < clusters.Count; i++)
            {
                double distance = distanceFunction(point, clusters[i].Centroid);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = i;
                }
            }

            return nearest;
        }

        public IEnumerator<KMeansCluster> GetEnumerator() => clusters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => clusters.GetEnumerator();

        private static double EuclideanDistance(double[] pointA, double[] pointB)
        {
            double sum = 0;
            for (int i = 0; i < pointA.Length; i++)
            {
                sum += Math.Pow(pointA[i] - pointB[i], 2);
            }
            return Math.Sqrt(sum);
        }

        [Serializable]
        public class KMeansCluster
        {
            public KMeansClusterCollection Owner { get; }
            public int Index { get; }
            public double[] Centroid { get; set; }
            public double Proportion { get; set; }

            public KMeansCluster(KMeansClusterCollection owner, int index)
            {
                Owner = owner;
                Index = index;
                Centroid = null;
                Proportion = 0;
            }

            public double[][] Covariance
            {
                get
                {
                    if (Owner.Covariances[Index] == null)
                    {
                        throw new InvalidOperationException("Covariance has not been computed.");
                    }
                    return Owner.Covariances[Index];
                }
            }
        }
    }

    public enum Seeding
    {
        Fixed,
        Uniform,
        KMeansPlusPlus,
        PamBuild
    }
}
