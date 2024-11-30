using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Training-Validation-Testing data split.
    /// </summary>
    /// <typeparam name="TInput">The type of the input being partitioned into splits.</typeparam>
    /// <typeparam name="TOutput">The type of the output being partitioned into splits.</typeparam>
    public class TrainTestDataSplit<TInput, TOutput>
    {
        /// <summary>
        /// The index of the split in relation to the original dataset, if applicable.
        /// </summary>
        public int SplitIndex { get; set; }

        /// <summary>
        /// The training subset of data.
        /// </summary>
        public DataSubset2<TInput, TOutput> Training { get; private set; }

        /// <summary>
        /// The testing subset of data.
        /// </summary>
        public DataSubset2<TInput, TOutput> Testing { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainTestDataSplit{TInput, TOutput}" /> class.
        /// </summary>
        public TrainTestDataSplit() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainTestDataSplit{TInput, TOutput}" /> class.
        /// </summary>
        /// <param name="index">The index associated with this subset, if any.</param>
        /// <param name="inputs">The input instances in this subset.</param>
        /// <param name="outputs">The output instances in this subset.</param>
        /// <param name="weights">The weights associated with the input instances.</param>
        /// <param name="trainIndices">The indices of the training instances in relation to the original dataset.</param>
        /// <param name="testIndices">The indices of the testing instances in relation to the original dataset.</param>
        public TrainTestDataSplit(
            int index,
            TInput[] inputs,
            TOutput[] outputs,
            double[] weights,
            int[] trainIndices,
            int[] testIndices)
        {
            SplitIndex = index;
            Training = new DataSubset2<TInput, TOutput>(index, inputs, outputs, weights, trainIndices);
            Testing = new DataSubset2<TInput, TOutput>(index, inputs, outputs, weights, testIndices);
        }
    }

    /// <summary>
    /// Represents a subset of data used for training or testing.
    /// </summary>
    /// <typeparam name="TInput">The type of the input data.</typeparam>
    /// <typeparam name="TOutput">The type of the output data.</typeparam>
    public class DataSubset2<TInput, TOutput>
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
        /// Initializes a new instance of the <see cref="DataSubset2{TInput, TOutput}" /> class.
        /// </summary>
        /// <param name="index">The index of this subset.</param>
        /// <param name="inputs">The input data.</param>
        /// <param name="outputs">The output data.</param>
        /// <param name="weights">The weights associated with the data.</param>
        /// <param name="indices">The indices of the data in relation to the original dataset.</param>
        public DataSubset2(int index, TInput[] inputs, TOutput[] outputs, double[] weights, int[] indices)
        {
            Index = index;
            Inputs = inputs;
            Outputs = outputs;
            Weights = weights;
            Indices = indices;
        }
    }
}
