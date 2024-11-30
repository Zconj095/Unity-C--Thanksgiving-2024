using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Stores training and validation errors of a model and provides transformation capabilities.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TInput">The type of the input data.</typeparam>
    /// <typeparam name="TOutput">The type of the output data.</typeparam>
    public interface ITransform<TInput, TOutput>
    {
        TOutput Transform(TInput input);
        TOutput[] Transform(TInput[] input);
        TOutput[] Transform(TInput[] input, TOutput[] result);
    }
    [Serializable]

    public class SplitResult<TModel, TInput, TOutput> : ITransform<TInput, TOutput>
        where TModel : ITransform<TInput, TOutput>
    {
        /// <summary>
        /// Gets the model associated with this result.
        /// </summary>
        public TModel Model { get; private set; }

        /// <summary>
        /// Gets or sets the index of this split.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets a tag for user-defined information.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Gets or sets the training results.
        /// </summary>
        public SetResult<TModel> Training { get; set; }

        /// <summary>
        /// Gets or sets the validation results.
        /// </summary>
        public SetResult<TModel> Validation { get; set; }

        /// <summary>
        /// Gets the total number of samples in training and validation sets.
        /// </summary>
        public int NumberOfSamples => Training.NumberOfSamples + Validation.NumberOfSamples;

        /// <summary>
        /// Gets the average number of samples between the training and validation sets.
        /// </summary>
        public double AverageNumberOfSamples => NumberOfSamples / 2.0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitResult{TModel, TInput, TOutput}"/> class.
        /// </summary>
        /// <param name="model">The model associated with this split result.</param>
        /// <param name="index">The index of this split in relation to the dataset.</param>
        public SplitResult(TModel model, int index)
        {
            Model = model;
            Index = index;
        }

        /// <summary>
        /// Applies the transformation to a single input, producing an output.
        /// </summary>
        /// <param name="input">The input to transform.</param>
        /// <returns>The transformed output.</returns>
        public TOutput Transform(TInput input)
        {
            return Model.Transform(input);
        }

        /// <summary>
        /// Applies the transformation to an array of inputs, producing an array of outputs.
        /// </summary>
        /// <param name="input">The array of inputs to transform.</param>
        /// <returns>The array of transformed outputs.</returns>
        public TOutput[] Transform(TInput[] input)
        {
            return Model.Transform(input);
        }

        /// <summary>
        /// Applies the transformation to an array of inputs, storing the results in the provided array.
        /// </summary>
        /// <param name="input">The array of inputs to transform.</param>
        /// <param name="result">The array where the results will be stored.</param>
        /// <returns>The array of transformed outputs.</returns>
        public TOutput[] Transform(TInput[] input, TOutput[] result)
        {
            return Model.Transform(input, result);
        }
    }
}
