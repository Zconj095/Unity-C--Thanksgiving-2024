using System;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Represents a subset of a larger dataset.
    /// </summary>
    /// <typeparam name="TInput">The type of the input data in this dataset.</typeparam>
    public class DataSubset<TInput>
    {
        /// <summary>
        /// Gets or sets the input data in the dataset.
        /// </summary>
        public TInput[] Inputs { get; set; }

        /// <summary>
        /// Gets or sets the weights associated with each input sample in the dataset.
        /// </summary>
        public double[] Weights { get; set; }

        /// <summary>
        /// Gets or sets the indices of the samples of this subset
        /// in relation to the original dataset they belong to.
        /// </summary>
        public int[] Indices { get; set; }

        /// <summary>
        /// Gets or sets the size of this subset as a proportion in
        /// relation to the original dataset this subset comes from.
        /// </summary>
        public double Proportion { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSubset{TInput}"/> class.
        /// </summary>
        public DataSubset() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSubset{TInput}"/> class.
        /// </summary>
        /// <param name="subsetSize">The size of the data subset.</param>
        /// <param name="totalSize">The total size of the dataset that contains this subset.</param>
        public DataSubset(int subsetSize, int totalSize)
        {
            Proportion = subsetSize / (double)totalSize;
            Indices = new int[subsetSize];
            Inputs = new TInput[subsetSize];
            Weights = Enumerable.Repeat(1.0, subsetSize).ToArray();
        }
    }

    /// <summary>
    /// Represents a subset of a larger dataset with both inputs and outputs.
    /// </summary>
    /// <typeparam name="TInput">The type of the input data in this dataset.</typeparam>
    /// <typeparam name="TOutput">The type of the output data in this dataset.</typeparam>
    public class DataSubset<TInput, TOutput> : DataSubset<TInput>
    {
        /// <summary>
        /// Gets or sets the output data in the dataset.
        /// </summary>
        public TOutput[] Outputs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSubset{TInput, TOutput}"/> class.
        /// </summary>
        public DataSubset() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSubset{TInput, TOutput}"/> class.
        /// </summary>
        /// <param name="subsetSize">The size of the data subset.</param>
        /// <param name="totalSize">The total size of the dataset that contains this subset.</param>
        public DataSubset(int subsetSize, int totalSize)
            : base(subsetSize, totalSize)
        {
            Outputs = new TOutput[subsetSize];
        }
    }
}
