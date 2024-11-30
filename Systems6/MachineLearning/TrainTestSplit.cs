using System.Collections;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Represents a training-test split of data.
    /// </summary>
    /// <typeparam name="T">The type being separated into training and test splits.</typeparam>
    public class TrainTestSplit<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets or sets the training split.
        /// </summary>
        public T Training { get; set; }

        /// <summary>
        /// Gets or sets the testing split.
        /// </summary>
        public T Testing { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainTestSplit{T}"/> class.
        /// </summary>
        public TrainTestSplit() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainTestSplit{T}"/> class with specified training and testing splits.
        /// </summary>
        /// <param name="training">The training split.</param>
        /// <param name="testing">The testing split.</param>
        public TrainTestSplit(T training, T testing)
        {
            Training = training;
            Testing = testing;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            yield return Training;
            yield return Testing;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
