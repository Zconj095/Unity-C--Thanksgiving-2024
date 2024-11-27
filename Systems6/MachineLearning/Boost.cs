using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning.Boosting
{
    /// <summary>
    ///   Weighted weak classifier.
    /// </summary>
    /// <typeparam name="TModel">The type of the weak classifier.</typeparam>
    public class Weighted<TModel>
    {
        /// <summary>
        ///   Gets or sets the weight associated with the weak classifier.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        ///   Gets or sets the weak classifier model.
        /// </summary>
        public TModel Model { get; set; }
    }

    /// <summary>
    ///   Boosted classification model.
    /// </summary>
    /// <typeparam name="TModel">The type of the weak classifier.</typeparam>
    /// <typeparam name="TInput">The type of input data the classifiers accept.</typeparam>
    public class BoostBase<TModel, TInput> : IEnumerable<Weighted<TModel>>
    {
        /// <summary>
        ///   Gets the list of weighted classifiers in this boosted classifier.
        /// </summary>
        public List<Weighted<TModel>> Models { get; } = new();

        /// <summary>
        ///   Computes the decision result for a given input using the boosted ensemble.
        /// </summary>
        /// <param name="input">The input sample.</param>
        /// <returns>The binary classification result (true/false).</returns>
        public bool Decide(TInput input)
        {
            double score = 0.0;

            foreach (var weightedModel in Models)
            {
                int decision = InvokeDecideMethod(weightedModel.Model, input);
                score += decision > 0 ? weightedModel.Weight : -weightedModel.Weight;
            }

            return score > 0;
        }

        /// <summary>
        ///   Adds a new weak classifier with its corresponding weight to the boosted classifier.
        /// </summary>
        /// <param name="weight">The weight of the weak classifier.</param>
        /// <param name="model">The weak classifier model.</param>
        public void Add(double weight, TModel model)
        {
            Models.Add(new Weighted<TModel> { Weight = weight, Model = model });
        }

        /// <summary>
        ///   Dynamically invokes the "Decide" method of the weak classifier model using reflection.
        /// </summary>
        /// <param name="model">The weak classifier model.</param>
        /// <param name="input">The input sample.</param>
        /// <returns>The binary decision result from the model (1 for true, 0 for false).</returns>
        private int InvokeDecideMethod(TModel model, TInput input)
        {
            var decideMethod = typeof(TModel).GetMethod("Decide", new[] { typeof(TInput) });
            if (decideMethod == null)
                throw new InvalidOperationException($"The model of type {typeof(TModel)} must implement a 'Decide' method.");

            return (int)decideMethod.Invoke(model, new object[] { input });
        }

        /// <summary>
        ///   Returns an enumerator that iterates through the weighted classifiers in the ensemble.
        /// </summary>
        public IEnumerator<Weighted<TModel>> GetEnumerator() => Models.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    ///   Boosted classification model specialized for weak classifiers working on `double[]` input.
    /// </summary>
    /// <typeparam name="TModel">The type of the weak classifier.</typeparam>
    public class Boost<TModel> : BoostBase<TModel, double[]>
    {
        /// <summary>
        ///   Computes the class label for the given input.
        /// </summary>
        /// <param name="input">The input sample.</param>
        /// <returns>The class label (1 for true, -1 for false).</returns>
        public int Compute(double[] input) => Decide(input) ? 1 : -1;
    }
}
