using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Summary statistics for a split-set validation trial.
    /// </summary>
    /// <typeparam name="TModel">The type of the model being evaluated.</typeparam>
    public class SplitSetStatistics<TModel> where TModel : class
    {
        /// <summary>
        /// The model generated during the validation trial.
        /// </summary>
        public TModel Model { get; private set; }

        /// <summary>
        /// The performance statistic value gathered during the validation run.
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// The variance of the performance statistic during the validation run.
        /// </summary>
        public double Variance { get; private set; }

        /// <summary>
        /// The number of samples used to compute the statistic.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// The standard deviation of the performance statistic.
        /// </summary>
        public double StandardDeviation => Math.Sqrt(Variance);

        /// <summary>
        /// A tag for user-defined information.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="SplitSetStatistics{TModel}"/> class.
        /// </summary>
        /// <param name="model">The generated model.</param>
        /// <param name="size">The number of samples used to compute the statistic.</param>
        /// <param name="value">The performance statistic gathered during the run.</param>
        /// <param name="variance">The variance of the performance statistic during the run.</param>
        public SplitSetStatistics(TModel model, int size, double value, double variance)
        {
            Model = model;
            Size = size;
            Value = value;
            Variance = variance;
        }
    }

    /// <summary>
    /// Represents a non-generic version of <see cref="SplitSetStatistics{TModel}"/>.
    /// </summary>
    public class SplitSetStatistics2 : SplitSetStatistics<object>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SplitSetStatistics"/> class.
        /// </summary>
        /// <param name="model">The generated model.</param>
        /// <param name="size">The number of samples used to compute the statistic.</param>
        /// <param name="value">The performance statistic gathered during the run.</param>
        /// <param name="variance">The variance of the performance statistic during the run.</param>
        public SplitSetStatistics2(object model, int size, double value, double variance)
            : base(model, size, value, variance) { }

        /// <summary>
        /// Creates a new instance of the <see cref="SplitSetStatistics{TModel}"/> class.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The generated model.</param>
        /// <param name="size">The number of samples used to compute the statistic.</param>
        /// <param name="value">The performance statistic gathered during the run.</param>
        /// <param name="variance">The variance of the performance statistic during the run.</param>
        /// <returns>A new instance of <see cref="SplitSetStatistics{TModel}"/>.</returns>
        public static SplitSetStatistics<TModel> Create<TModel>(TModel model, int size, double value, double variance) where TModel : class
        {
            return new SplitSetStatistics<TModel>(model, size, value, variance);
        }
    }
}
