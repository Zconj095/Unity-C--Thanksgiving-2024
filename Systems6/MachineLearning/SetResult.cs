using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Represents the training and validation results of a model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    [Serializable]
    public class SetResult<TModel>
    {
        /// <summary>
        /// Gets or sets the name of the set (e.g., "Training" or "Testing").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the model associated with this result.
        /// </summary>
        public TModel Model { get; private set; }

        /// <summary>
        /// Gets the indices of the samples in this subset in the original dataset.
        /// </summary>
        public int[] Indices { get; private set; }

        /// <summary>
        /// Gets the number of samples in this subset.
        /// </summary>
        public int NumberOfSamples { get; private set; }

        /// <summary>
        /// Gets the proportion of this subset compared to the original dataset.
        /// </summary>
        public double Proportion { get; private set; }

        /// <summary>
        /// Gets or sets the metric value for the model in the current set.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the variance of the validation value for the model, if available.
        /// </summary>
        public double Variance { get; set; }

        /// <summary>
        /// Gets the standard deviation of the validation value for the model, if available.
        /// </summary>
        public double StandardDeviation => Math.Sqrt(Variance);

        /// <summary>
        /// Gets or sets a tag for user-defined information.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetResult{TModel}"/> class.
        /// </summary>
        /// <param name="model">The model computed in this subset.</param>
        /// <param name="indices">The indices of the samples in this subset.</param>
        /// <param name="name">The name of this set.</param>
        /// <param name="proportion">The proportion of samples in this subset compared to the full dataset.</param>
        public SetResult(TModel model, int[] indices, string name, double proportion)
        {
            Name = name;
            Model = model;
            Indices = indices;
            NumberOfSamples = indices.Length;
            Proportion = proportion;
        }
    }
}
