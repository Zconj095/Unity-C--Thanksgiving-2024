using System.Collections;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Represents a training-validation-test split of data.
    /// </summary>
    /// <typeparam name="T">The type being separated into training, validation, and test splits.</typeparam>
    public class TrainValTestSplit<T> : IEnumerable<T>
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
        /// Gets or sets the testing split.
        /// </summary>
        public T Testing { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainValTestSplit{T}"/> class.
        /// </summary>
        public TrainValTestSplit() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainValTestSplit{T}"/> class with specified splits.
        /// </summary>
        /// <param name="training">The training split.</param>
        /// <param name="validation">The validation split.</param>
        /// <param name="testing">The testing split.</param>
        public TrainValTestSplit(T training, T validation, T testing)
        {
            Training = training;
            Validation = validation;
            Testing = testing;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            yield return Training;
            yield return Validation;
            yield return Testing;
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
