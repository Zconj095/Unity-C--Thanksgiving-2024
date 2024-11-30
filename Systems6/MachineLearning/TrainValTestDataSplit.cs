using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Training-Validation-Testing data split.
    /// </summary>
    /// <typeparam name="TInput">The type of the input being partitioned into splits.</typeparam>
    /// <typeparam name="TOutput">The type of the output being partitioned into splits.</typeparam>
    public class TrainValTestDataSplit<TInput, TOutput>
    {
        /// <summary>
        /// The index of the split in relation to the original dataset, if applicable.
        /// </summary>
        public int SplitIndex { get; set; }

        /// <summary>
        /// The training subset of data.
        /// </summary>
        public DataSubset3<TInput, TOutput> Training { get; private set; }

        /// <summary>
        /// The validation subset of data.
        /// </summary>
        public DataSubset3<TInput, TOutput> Validation { get; private set; }

        /// <summary>
        /// The testing subset of data.
        /// </summary>
        public DataSubset3<TInput, TOutput> Testing { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainValTestDataSplit{TInput, TOutput}" /> class.
        /// </summary>
        public TrainValTestDataSplit() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainValTestDataSplit{TInput, TOutput}" /> class.
        /// </summary>
        /// <param name="index">The index associated with this subset, if any.</param>
        /// <param name="inputs">The input instances in this subset.</param>
        /// <param name="outputs">The output instances in this subset.</param>
        /// <param name="weights">The weights associated with the input instances.</param>
        /// <param name="trainIndices">The indices of the training instances in relation to the original dataset.</param>
        /// <param name="validationIndices">The indices of the validation instances in relation to the original dataset.</param>
        /// <param name="testingIndices">The indices of the testing instances in relation to the original dataset.</param>
        public TrainValTestDataSplit(
            int index,
            TInput[] inputs,
            TOutput[] outputs,
            double[] weights,
            int[] trainIndices,
            int[] validationIndices,
            int[] testingIndices)
        {
            SplitIndex = index;
            Training = new DataSubset3<TInput, TOutput>(index, inputs, outputs, weights, trainIndices);
            Validation = new DataSubset3<TInput, TOutput>(index, inputs, outputs, weights, validationIndices);
            Testing = new DataSubset3<TInput, TOutput>(index, inputs, outputs, weights, testingIndices);
        }
    }

    /// <summary>
    /// Represents a subset of data used for training, validation, or testing.
    /// </summary>
    /// <typeparam name="TInput">The type of the input data.</typeparam>
    /// <typeparam name="TOutput">The type of the output data.</typeparam>
    public class DataSubset3<TInput, TOutput>
    {
        /// <summary>
        /// The index associated with this subset.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// The input instances.
        /// </summary>
        public TInput[] Inputs { get; private set; }

        /// <summary>
        /// The output instances.
        /// </summary>
        public TOutput[] Outputs { get; private set; }

        /// <summary>
        /// The weights associated with the input instances.
        /// </summary>
        public double[] Weights { get; private set; }

        /// <summary>
        /// The indices of the data in relation to the original dataset.
        /// </summary>
        public int[] Indices { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSubset3{TInput, TOutput}" /> class.
        /// </summary>
        /// <param name="index">The index of this subset.</param>
        /// <param name="inputs">The input data.</param>
        /// <param name="outputs">The output data.</param>
        /// <param name="weights">The weights associated with the data.</param>
        /// <param name="indices">The indices of the data in relation to the original dataset.</param>
        public DataSubset3(int index, TInput[] inputs, TOutput[] outputs, double[] weights, int[] indices)
        {
            Index = index;
            Inputs = inputs;
            Outputs = outputs;
            Weights = weights;
            Indices = indices;
        }
    }
}
