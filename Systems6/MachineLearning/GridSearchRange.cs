using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Represents a range of parameter values for grid search.
    /// </summary>
    /// <typeparam name="T">The type of the parameter values.</typeparam>
    public class GridSearchRange2<T> : ICloneable
    {
        /// <summary>
        /// The range of values to be tested for this parameter.
        /// </summary>
        public T[] Values { get; set; }

        /// <summary>
        /// The index of the current value being tested.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets the current value in the range being tested.
        /// </summary>
        public T Value => Values[Index];

        /// <summary>
        /// Gets the number of values in the parameter range.
        /// </summary>
        public int Length => Values.Length;

        /// <summary>
        /// Creates a shallow copy of the current range.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Implicit conversion from <see cref="GridSearchRange2{T}"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="range">The range to be converted.</param>
        public static implicit operator T(GridSearchRange2<T> range)
        {
            return range.Value;
        }
    }
}
