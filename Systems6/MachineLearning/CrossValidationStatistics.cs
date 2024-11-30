using System;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Summary statistics for a cross-validation trial in Unity.
    /// </summary>
    [Serializable]
    public class CrossValidationStatistics
    {
        /// <summary>
        /// Gets the values acquired during the cross-validation.
        /// Most often these will be the errors for each fold.
        /// </summary>
        public double[] Values { get; private set; }

        /// <summary>
        /// Gets the variance for each value acquired during the cross-validation.
        /// Most often these will be the error variances for each fold.
        /// </summary>
        public double[] Variances { get; private set; }

        /// <summary>
        /// Gets the number of samples used to compute the variance
        /// of the values acquired during the cross-validation.
        /// </summary>
        public int[] Sizes { get; private set; }

        /// <summary>
        /// Gets the mean of the performance statistics.
        /// </summary>
        public double Mean { get; private set; }

        /// <summary>
        /// Gets the variance of the performance statistics.
        /// </summary>
        public double Variance { get; private set; }

        /// <summary>
        /// Gets the standard deviation of the performance statistics.
        /// </summary>
        public double StandardDeviation => Math.Sqrt(Variance);

        /// <summary>
        /// Gets the pooled variance of the performance statistics.
        /// </summary>
        public double PooledVariance { get; private set; }

        /// <summary>
        /// Gets the pooled standard deviation of the performance statistics.
        /// </summary>
        public double PooledStandardDeviation => Math.Sqrt(PooledVariance);

        /// <summary>
        /// Tag for user-defined information.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Creates a new instance of CrossValidationStatistics.
        /// </summary>
        /// <param name="sizes">The number of samples used to compute the statistics.</param>
        /// <param name="values">The performance statistics gathered during the run.</param>
        /// <param name="variances">The variance of the statistics, if available.</param>
        public CrossValidationStatistics(int[] sizes, double[] values, double[] variances = null)
        {
            if (sizes.Length != values.Length)
                throw new ArgumentException("Sizes and values must have the same length.");

            Sizes = sizes;
            Values = values;
            Variances = variances;

            Mean = ComputeMean(values);
            Variance = ComputeVariance(values, Mean);

            if (variances != null)
                PooledVariance = ComputePooledVariance(sizes, variances);
        }

        private static double ComputeMean(double[] values)
        {
            return values.Average();
        }

        private static double ComputeVariance(double[] values, double mean)
        {
            return values.Sum(x => Math.Pow(x - mean, 2)) / values.Length;
        }

        private static double ComputePooledVariance(int[] sizes, double[] variances)
        {
            int totalSize = sizes.Sum();
            double pooledVariance = 0;

            for (int i = 0; i < sizes.Length; i++)
            {
                pooledVariance += (sizes[i] - 1) * variances[i];
            }

            return pooledVariance / (totalSize - sizes.Length);
        }
    }
}
