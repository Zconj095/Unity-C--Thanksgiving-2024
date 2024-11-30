using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Common interface for Bag of Words objects in Unity.
    /// </summary>
    /// <typeparam name="T">The type of the element to be 
    /// converted to a fixed-length vector representation.</typeparam>
    public interface IBagOfWords<T>
    {
        /// <summary>
        /// Gets the number of words in this codebook.
        /// </summary>
        int NumberOfWords { get; }

        /// <summary>
        /// Converts a given value to a double vector representation.
        /// </summary>
        /// <param name="value">The value to be processed.</param>
        /// <returns>A double vector with the same length as the words in the codebook.</returns>
        double[] TransformToDouble(T value);

        /// <summary>
        /// Converts a given value to an integer vector representation.
        /// </summary>
        /// <param name="value">The value to be processed.</param>
        /// <returns>An integer vector with the same length as the words in the codebook.</returns>
        int[] TransformToInt(T value);

        /// <summary>
        /// Gets the codeword representation of a given value.
        /// </summary>
        /// <param name="value">The value to be processed.</param>
        /// <returns>A double vector with the same length as the words in the codebook.</returns>
        [Obsolete("Please use the TransformToDouble method instead.")]
        double[] GetFeatureVector(T value);
    }
}
