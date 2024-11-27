using System;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Learning algorithm for creating <see cref="DecisionStump"/> classifiers.
    /// </summary>
    public class ThresholdLearning
    {
        /// <summary>
        ///   Gets or sets the model being trained.
        /// </summary>
        public DecisionStump Model { get; set; }

        /// <summary>
        ///   Learns a model that can map the given inputs to the given outputs.
        /// </summary>
        /// <param name="x">The model inputs.</param>
        /// <param name="y">The desired outputs associated with each <paramref name="x">inputs</paramref>.</param>
        /// <param name="weights">The weight of importance for each input-output pair (optional).</param>
        /// <returns>A model that has learned how to produce <paramref name="y"/> given <paramref name="x"/>.</returns>
        public DecisionStump Learn(double[][] x, bool[] y, double[] weights = null)
        {
            if (weights == null)
            {
                weights = Enumerable.Repeat(1.0 / x.Length, x.Length).ToArray();
            }

            if (Model == null)
            {
                Model = new DecisionStump();
            }

            double errorMinimum = double.MaxValue;
            int numberOfVariables = x[0].Length;

            for (int featureIndex = 0; featureIndex < numberOfVariables; featureIndex++)
            {
                // Sort indices by the feature values
                int[] indices = Enumerable.Range(0, x.Length).OrderBy(idx => x[idx][featureIndex]).ToArray();

                double error = weights.Zip(y, (weight, label) => label ? weight : 0.0).Sum();

                for (int j = 0; j < y.Length - 1; j++)
                {
                    int currentIdx = indices[j];
                    int nextIdx = indices[j + 1];

                    if (y[currentIdx])
                        error -= weights[currentIdx];
                    else
                        error += weights[currentIdx];

                    double midpoint = (x[currentIdx][featureIndex] + x[nextIdx][featureIndex]) / 2.0;

                    // Check if this split minimizes the error
                    if (error < errorMinimum)
                    {
                        errorMinimum = error;
                        Model.Index = featureIndex;
                        Model.Threshold = midpoint;
                        Model.Comparison = ComparisonKind.LessThan;
                    }

                    // Check the complementary split
                    if ((1.0 - error) < errorMinimum)
                    {
                        errorMinimum = 1.0 - error;
                        Model.Index = featureIndex;
                        Model.Threshold = midpoint;
                        Model.Comparison = ComparisonKind.GreaterThan;
                    }
                }
            }

            return Model;
        }
    }

    /// <summary>
    ///   A simple decision stump classifier for threshold-based classification.
    /// </summary>
    public class DecisionStump
    {
        public int Index { get; set; }
        public double Threshold { get; set; }
        public ComparisonKind Comparison { get; set; }

        /// <summary>
        ///   Decides the classification result for a given input.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>True if the input satisfies the threshold condition; otherwise, false.</returns>
        public bool Decide(double[] input)
        {
            double value = input[Index];
            return Comparison switch
            {
                ComparisonKind.LessThan => value < Threshold,
                ComparisonKind.GreaterThan => value > Threshold,
                _ => throw new InvalidOperationException("Unsupported comparison type."),
            };
        }
    }

    /// <summary>
    ///   Enumeration for comparison types in threshold-based classifiers.
    /// </summary>
    public enum ComparisonKind
    {
        LessThan,
        GreaterThan
    }
}
