using System.Collections;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Common interface for data splits.
    /// </summary>
    /// <typeparam name="TInput">The type of the input being partitioned into splits.</typeparam>
    /// <typeparam name="TOutput">The type of the output being partitioned into splits.</typeparam>
    public interface IDataSplit<TInput, TOutput> : IEnumerable<DataSubset<TInput, TOutput>>
    {
        /// <summary>
        /// The index of the split in relation to the original dataset, if applicable.
        /// </summary>
        int SplitIndex { get; set; }
    }

    /// <summary>
    /// Represents a subset of data used for training, validation, or testing.
    /// </summary>
    /// <typeparam name="TInput">The type of the input data.</typeparam>
    /// <typeparam name="TOutput">The type of the output data.</typeparam>
}
