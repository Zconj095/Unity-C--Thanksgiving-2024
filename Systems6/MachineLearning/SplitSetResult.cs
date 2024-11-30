using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Represents the result of a split set validation.
    /// </summary>
    /// <typeparam name="TModel">The type of the model being validated.</typeparam>
    public class SplitSetResult<TModel> where TModel : class
    {
        /// <summary>
        /// Gets the validation settings used to generate this result.
        /// </summary>
        public SplitSetValidation<TModel> Settings { get; private set; }

        /// <summary>
        /// Gets the performance statistics for the training set.
        /// </summary>
        public SplitSetResultsSplitSetStatistics<TModel> Training { get; private set; }

        /// <summary>
        /// Gets the performance statistics for the validation set.
        /// </summary>
        public SplitSetResultsSplitSetStatistics<TModel> Validation { get; private set; }

        /// <summary>
        /// Gets or sets a tag for user-defined information.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitSetResult{TModel}" /> class.
        /// </summary>
        /// <param name="settings">The settings used to create this result.</param>
        /// <param name="training">The training set statistics.</param>
        /// <param name="validation">The validation set statistics.</param>
        public SplitSetResult(
            SplitSetValidation<TModel> settings,
            SplitSetResultsSplitSetStatistics<TModel> training,
            SplitSetResultsSplitSetStatistics<TModel> validation)
        {
            Settings = settings;
            Training = training;
            Validation = validation;
        }
    }

    /// <summary>
    /// Represents settings for split set validation.
    /// </summary>
    /// <typeparam name="TModel">The type of the model being validated.</typeparam>
    public class SplitSetValidation<TModel> where TModel : class
    {
        // Add necessary properties or methods for split set validation settings.
    }

    /// <summary>
    /// Represents statistics for a specific data set during validation.
    /// </summary>
    /// <typeparam name="TModel">The type of the model being evaluated.</typeparam>
    public class SplitSetResultsSplitSetStatistics<TModel> where TModel : class
    {
        /// <summary>
        /// Gets or sets the performance metric value.
        /// </summary>
        public double MetricValue { get; set; }

        /// <summary>
        /// Gets or sets the variance of the metric.
        /// </summary>
        public double Variance { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitSetResultsSplitSetStatistics{TModel}" /> class.
        /// </summary>
        /// <param name="metricValue">The metric value.</param>
        /// <param name="variance">The variance of the metric.</param>
        public SplitSetResultsSplitSetStatistics(double metricValue, double variance)
        {
            MetricValue = metricValue;
            Variance = variance;
        }
    }
}
