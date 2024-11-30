using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class KNearestNeighbors :
        BaseKNearestNeighbors<KNearestNeighbors, double[], Func<double[], double[], double>>
    {
        private List<(double[] Position, int Label)> data;

        /// <summary>
        ///   Creates a new KNearestNeighbors instance.
        /// </summary>
        public KNearestNeighbors()
        {
            data = new List<(double[], int)>();
        }

        /// <summary>
        ///   Creates a new KNearestNeighbors instance with a specific number of neighbors.
        /// </summary>
        public KNearestNeighbors(int k) : this()
        {
            K = k;
            Distance = EuclideanDistance;
        }

        /// <summary>
        ///   Creates a new KNearestNeighbors instance with a specific number of neighbors and custom distance metric.
        /// </summary>
        public KNearestNeighbors(int k, Func<double[], double[], double> distance) : this(k)
        {
            Distance = distance;
        }

        /// <summary>
        ///   Computes the Euclidean distance between two points.
        /// </summary>
        private double EuclideanDistance(double[] point1, double[] point2)
        {
            double sum = 0;
            for (int i = 0; i < point1.Length; i++)
            {
                double diff = point1[i] - point2[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }

        /// <summary>
        ///   Adds a training point to the dataset.
        /// </summary>
        public void AddTrainingData(double[][] inputs, int[] outputs)
        {
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("Inputs and outputs must have the same length.");

            for (int i = 0; i < inputs.Length; i++)
                data.Add((inputs[i], outputs[i]));
        }

        /// <summary>
        ///   Scores the association between a point and each class.
        /// </summary>
        public override double[] Scores(double[] input)
        {
            var neighbors = GetNearestNeighbors(input, out var labels);

            var scores = new double[labels.Max() + 1];
            for (int i = 0; i < neighbors.Length; i++)
            {
                var distance = Distance(input, neighbors[i]);
                scores[labels[i]] += 1.0 / (1.0 + distance); // Similarity
            }
            return scores;
        }

        /// <summary>
        ///   Retrieves the k-nearest neighbors to a given input point.
        /// </summary>
        public override double[][] GetNearestNeighbors(double[] input, out int[] labels)
        {
            var sortedNeighbors = data
                .Select(entry => (Distance: Distance(input, entry.Position), entry.Label, entry.Position))
                .OrderBy(entry => entry.Distance)
                .Take(K)
                .ToList();

            labels = sortedNeighbors.Select(neighbor => neighbor.Label).ToArray();
            return sortedNeighbors.Select(neighbor => neighbor.Position).ToArray();
        }

        /// <summary>
        ///   Trains the kNN model with the provided dataset.
        /// </summary>
        public override KNearestNeighbors Learn(double[][] inputs, int[] outputs, double[] weights = null)
        {
            CheckArgs(K, inputs, outputs, Distance, weights);

            AddTrainingData(inputs, outputs);
            return this;
        }

        #region From Existing Data
        /// <summary>
        ///   Creates a new KNearestNeighbors instance from existing data.
        /// </summary>
        public static KNearestNeighbors FromData(int k, Func<double[], double[], double> distance, double[][] inputs, int[] outputs)
        {
            var knn = new KNearestNeighbors(k, distance);
            knn.AddTrainingData(inputs, outputs);
            return knn;
        }
        #endregion
    }
}
