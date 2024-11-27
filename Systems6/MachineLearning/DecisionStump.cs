using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
namespace EdgeLoreMachineLearning.Boosting.Learners
{
    /// <summary>
    ///   Simple classifier based on decision thresholds for one-dimensional features.
    /// </summary>
    public class DecisionStump
    {
        private double threshold;
        private int featureIndex;
        private ComparisonKind comparison;

        /// <summary>
        ///   Initializes a new instance of the <see cref="DecisionStump"/> class.
        /// </summary>
        public DecisionStump()
        {
        }

        /// <summary>
        ///   Gets or sets the decision threshold for this classifier.
        /// </summary>
        public double Threshold
        {
            get => threshold;
            set => threshold = value;
        }

        /// <summary>
        ///   Gets or sets the index of the feature used for the decision.
        /// </summary>
        public int Index
        {
            get => featureIndex;
            set => featureIndex = value;
        }

        /// <summary>
        ///   Gets or sets the type of comparison performed.
        /// </summary>
        public ComparisonKind Comparison
        {
            get => comparison;
            set => comparison = value;
        }

        /// <summary>
        ///   Decides the classification result for a given input.
        /// </summary>
        /// <param name="input">Input vector.</param>
        /// <returns>True if the input satisfies the threshold condition; otherwise, false.</returns>
        public bool Decide(double[] input)
        {
            if (featureIndex < 0 || featureIndex >= input.Length)
                throw new IndexOutOfRangeException("Feature index is out of range for the input vector.");

            double value = input[featureIndex];

            return comparison switch
            {
                ComparisonKind.Equal => value == threshold,
                ComparisonKind.NotEqual => value != threshold,
                ComparisonKind.GreaterThan => value > threshold,
                ComparisonKind.GreaterThanOrEqual => value >= threshold,
                ComparisonKind.LessThan => value < threshold,
                ComparisonKind.LessThanOrEqual => value <= threshold,
                _ => throw new InvalidOperationException("Invalid comparison type."),
            };
        }

        /// <summary>
        ///   Trains the decision stump based on given inputs and outputs.
        /// </summary>
        /// <param name="inputs">Input vectors.</param>
        /// <param name="outputs">Expected class labels (binary: 0 or 1).</param>
        /// <param name="weights">Weights for each input.</param>
        public void Learn(double[][] inputs, int[] outputs, double[] weights = null)
        {
            if (inputs == null || outputs == null || inputs.Length != outputs.Length)
                throw new ArgumentException("Inputs and outputs must be non-null and of the same length.");

            int numSamples = inputs.Length;
            int numFeatures = inputs[0].Length;
            weights ??= new double[numSamples];

            // Initialize weights uniformly if not provided
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] == 0)
                    weights[i] = 1.0 / numSamples;
            }

            double minError = double.MaxValue;

            for (int feature = 0; feature < numFeatures; feature++)
            {
                // Get all unique values for the current feature
                var uniqueValues = new HashSet<double>(inputs.Select(x => x[feature]));

                foreach (double candidateThreshold in uniqueValues)
                {
                    foreach (ComparisonKind comparisonType in Enum.GetValues(typeof(ComparisonKind)))
                    {
                        // Evaluate error for this combination
                        double error = EvaluateError(inputs, outputs, weights, feature, candidateThreshold, comparisonType);

                        // Update the best stump parameters
                        if (error < minError)
                        {
                            minError = error;
                            featureIndex = feature;
                            threshold = candidateThreshold;
                            comparison = comparisonType;
                        }
                    }
                }
            }
        }

        private double EvaluateError(double[][] inputs, int[] outputs, double[] weights, int feature, double threshold, ComparisonKind comparisonType)
        {
            double error = 0.0;

            for (int i = 0; i < inputs.Length; i++)
            {
                bool prediction = Compare(inputs[i][feature], threshold, comparisonType);
                bool isIncorrect = (prediction ? 1 : 0) != outputs[i];
                if (isIncorrect)
                    error += weights[i];
            }

            return error;
        }

        private bool Compare(double value, double threshold, ComparisonKind comparisonType)
        {
            return comparisonType switch
            {
                ComparisonKind.Equal => value == threshold,
                ComparisonKind.NotEqual => value != threshold,
                ComparisonKind.GreaterThan => value > threshold,
                ComparisonKind.GreaterThanOrEqual => value >= threshold,
                ComparisonKind.LessThan => value < threshold,
                ComparisonKind.LessThanOrEqual => value <= threshold,
                _ => throw new InvalidOperationException("Invalid comparison type."),
            };
        }
    }

    /// <summary>
    ///   Comparison types for threshold-based classifiers.
    /// </summary>
    public enum ComparisonKind
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }
}
