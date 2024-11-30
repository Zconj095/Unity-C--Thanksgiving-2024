using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Non-generic interface for parameter ranges in grid search.
    /// </summary>
    /// <seealso cref="GridSearchRange{T}"/>
    public interface IGridSearchRange : ICloneable
    {
        /// <summary>
        /// Gets or sets the index of the current value in the search.
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Gets the number of values in the parameter range.
        /// </summary>
        int Length { get; }
    }
}
