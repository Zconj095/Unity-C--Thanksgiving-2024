using System.Collections;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Represents a training-validation split of data.
    /// </summary>
    /// <typeparam name="T">The type being separated into training and validation splits.</typeparam>
    public class TrainValSplit<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets or sets the training split.
        /// </summary>
        public T Training { get; set; }

        /// <summary>
        /// Gets or sets the validation split.
        /// </summary>
        public T Validation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainValSplit{T}"/> class.
        /// </summary>
        public TrainValSplit() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainValSplit{T}"/> class with specified training and validation splits.
        /// </summary>
        /// <param name="training">The training split.</param>
        /// <param name="validation">The validation split.</param>
        public TrainValSplit(T training, T validation)
        {
            Training = training;
            Validation = validation;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            yield return Training;
            yield return Validation;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
