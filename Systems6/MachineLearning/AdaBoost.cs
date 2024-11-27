using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   AdaBoost learning algorithm for boosting weak classifiers into a strong classifier.
    /// </summary>
    /// <typeparam name="TModel">The type of the weak classifier model.</typeparam>
    public class AdaBoost<TModel>
        where TModel : class
    {
        private readonly List<(double Weight, TModel Model)> ensemble = new();
        private readonly double threshold = 0.5;
        private double tolerance = 1e-6;
        private int maxIterations = 100;

        /// <summary>
        ///   Gets or sets the function that creates and trains a model given a weighted dataset.
        /// </summary>
        public Func<double[][], int[], double[], TModel> Learner { get; set; }

        /// <summary>
        ///   Gets or sets the maximum number of iterations.
        /// </summary>
        public int MaxIterations
        {
            get => maxIterations;
            set => maxIterations = Math.Max(1, value);
        }

        /// <summary>
        ///   Gets or sets the relative tolerance for convergence.
        /// </summary>
        public double Tolerance
        {
            get => tolerance;
            set => tolerance = Math.Max(1e-9, value);
        }

        /// <summary>
        ///   Learns a model that can map the given inputs to the given outputs.
        /// </summary>
        /// <param name="inputs">The input samples.</param>
        /// <param name="outputs">The corresponding binary output labels.</param>
        /// <param name="weights">The sample weights.</param>
        /// <returns>A strong classifier composed of weighted weak classifiers.</returns>
        public TModel Learn(double[][] inputs, int[] outputs, double[] weights = null)
        {
            if (inputs == null || outputs == null)
                throw new ArgumentNullException("Inputs and outputs cannot be null.");
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("Inputs and outputs must have the same length.");

            int sampleCount = inputs.Length;
            weights ??= Enumerable.Repeat(1.0 / sampleCount, sampleCount).ToArray();

            double totalWeight = 0;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                // Train a new weak classifier
                var model = Learner(inputs, outputs, weights);
                if (model == null)
                    break;

                // Calculate classification error
                double error = 0;
                for (int i = 0; i < sampleCount; i++)
                {
                    bool isCorrect = Decide(model, inputs[i]) == outputs[i];
                    if (!isCorrect)
                        error += weights[i];
                }

                if (error >= threshold || error <= tolerance)
                    break;

                // Compute the model's weight
                double modelWeight = 0.5 * Math.Log((1.0 - error) / Math.Max(error, 1e-9));

                // Update sample weights
                double weightSum = 0;
                for (int i = 0; i < sampleCount; i++)
                {
                    bool isCorrect = Decide(model, inputs[i]) == outputs[i];
                    weights[i] *= Math.Exp(isCorrect ? -modelWeight : modelWeight);
                    weightSum += weights[i];
                }

                // Normalize weights
                for (int i = 0; i < sampleCount; i++)
                    weights[i] /= weightSum;

                ensemble.Add((modelWeight, model));
                totalWeight += modelWeight;

                // Check for convergence
                if (error <= tolerance)
                    break;
            }

            // Normalize the weights of the classifiers in the ensemble
            for (int i = 0; i < ensemble.Count; i++)
            {
                var (weight, model) = ensemble[i];
                ensemble[i] = (weight / totalWeight, model);
            }

            // Return the strong classifier (this could return the ensemble itself, but here we simply return the last model for compatibility)
            return ensemble.LastOrDefault().Model;
        }

        /// <summary>
        ///   Makes a decision using the ensemble of weak classifiers.
        /// </summary>
        /// <param name="input">The input sample.</param>
        /// <returns>The ensemble's decision.</returns>
        public int Decide(double[] input)
        {
            double score = 0;
            foreach (var (weight, model) in ensemble)
            {
                score += weight * (Decide(model, input) == 1 ? 1 : -1);
            }
            return score >= 0 ? 1 : 0;
        }

        /// <summary>
        ///   Makes a decision using a single model.
        /// </summary>
        /// <param name="model">The weak classifier model.</param>
        /// <param name="input">The input sample.</param>
        /// <returns>The model's decision.</returns>
        private int Decide(TModel model, double[] input)
        {
            var method = typeof(TModel).GetMethod("Decide", new[] { typeof(double[]) });
            if (method == null)
                throw new InvalidOperationException("The model must implement a Decide method.");

            return (int)method.Invoke(model, new object[] { input });
        }
    }
}
