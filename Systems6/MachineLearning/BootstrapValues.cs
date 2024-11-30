using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Represents the results of training and validation for a single bootstrap iteration.
    /// </summary>
    [Serializable]
    public class BootstrapValues
    {
        /// <summary>
        /// The training value for the model.
        /// </summary>
        public double TrainingValue { get; private set; }

        /// <summary>
        /// The variance of the training value, if available.
        /// </summary>
        public double TrainingVariance { get; private set; }

        /// <summary>
        /// The validation value for the model.
        /// </summary>
        public double ValidationValue { get; private set; }

        /// <summary>
        /// The variance of the validation value, if available.
        /// </summary>
        public double ValidationVariance { get; private set; }

        /// <summary>
        /// A tag for user-defined information.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Constructor to create a new instance of BootstrapValues.
        /// </summary>
        /// <param name="trainingValue">The training value for the model.</param>
        /// <param name="validationValue">The validation value for the model.</param>
        public BootstrapValues(double trainingValue, double validationValue)
        {
            TrainingValue = trainingValue;
            ValidationValue = validationValue;
        }

        /// <summary>
        /// Constructor to create a new instance of BootstrapValues with variances.
        /// </summary>
        /// <param name="trainingValue">The training value for the model.</param>
        /// <param name="trainingVariance">The variance of the training value.</param>
        /// <param name="validationValue">The validation value for the model.</param>
        /// <param name="validationVariance">The variance of the validation value.</param>
        public BootstrapValues(double trainingValue, double trainingVariance, double validationValue, double validationVariance)
        {
            TrainingValue = trainingValue;
            TrainingVariance = trainingVariance;
            ValidationValue = validationValue;
            ValidationVariance = validationVariance;
        }
    }
}
