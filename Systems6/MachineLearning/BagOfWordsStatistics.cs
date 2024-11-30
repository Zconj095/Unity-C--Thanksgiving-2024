using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Codebook learning statistics for BagOfWords models, adapted for Unity.
    /// </summary>
    [Serializable]
    public class BagOfWordsStatistics
    {
        /// <summary>
        /// Gets or sets the number of instances (e.g., images or audio signals) in the training set.
        /// </summary>
        public int TotalNumberOfInstances { get; set; }

        /// <summary>
        /// Gets or sets the total number of descriptors seen in the training set.
        /// </summary>
        public int TotalNumberOfDescriptors { get; set; }

        /// <summary>
        /// Gets or sets the average and variance of descriptors per instance.
        /// Replaces the NormalDistribution with Unity-compatible statistics.
        /// </summary>
        public Vector2 TotalNumberOfDescriptorsPerInstance { get; set; } // X = Mean, Y = Variance

        /// <summary>
        /// Gets or sets the minimum and maximum number of descriptors per instance seen in the training set.
        /// </summary>
        public Vector2Int TotalNumberOfDescriptorsPerInstanceRange { get; set; } // X = Min, Y = Max

        /// <summary>
        /// Gets or sets the number of instances actually used in the learning process.
        /// </summary>
        public int NumberOfInstancesTaken { get; set; }

        /// <summary>
        /// Gets or sets the number of descriptors actually used in the learning process.
        /// </summary>
        public int NumberOfDescriptorsTaken { get; set; }

        /// <summary>
        /// Gets or sets the average and variance of descriptors actually used per instance.
        /// </summary>
        public Vector2 NumberOfDescriptorsTakenPerInstance { get; set; } // X = Mean, Y = Variance

        /// <summary>
        /// Gets or sets the minimum and maximum number of descriptors per instance
        /// actually used in the learning process.
        /// </summary>
        public Vector2Int NumberOfDescriptorsTakenPerInstanceRange { get; set; } // X = Min, Y = Max

        /// <summary>
        /// Calculates the mean and variance of a set of data points.
        /// </summary>
        /// <param name="data">The data set to calculate statistics for.</param>
        /// <returns>A Vector2 where X is the mean and Y is the variance.</returns>
        public static Vector2 CalculateMeanAndVariance(int[] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data cannot be null or empty.");

            float mean = 0f;
            foreach (var value in data)
                mean += value;
            mean /= data.Length;

            float variance = 0f;
            foreach (var value in data)
                variance += Mathf.Pow(value - mean, 2);
            variance /= data.Length;

            return new Vector2(mean, variance);
        }

        /// <summary>
        /// Calculates the minimum and maximum values in a data set.
        /// </summary>
        /// <param name="data">The data set to calculate the range for.</param>
        /// <returns>A Vector2Int where X is the minimum and Y is the maximum.</returns>
        public static Vector2Int CalculateMinMax(int[] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data cannot be null or empty.");

            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (var value in data)
            {
                if (value < min) min = value;
                if (value > max) max = value;
            }

            return new Vector2Int(min, max);
        }
    }
}
