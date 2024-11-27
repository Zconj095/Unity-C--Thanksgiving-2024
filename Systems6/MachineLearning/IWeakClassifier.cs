using System;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Common interface for Weak classifiers used in Boosting mechanisms.
    /// </summary>
    public interface IWeakClassifier
    {
        /// <summary>
        ///   Computes the output class label for a given input.
        /// </summary>
        /// <param name="inputs">The input vector.</param>
        /// <returns>The most likely class label for the given input.</returns>
        int Compute(double[] inputs);
    }

    /// <summary>
    ///   A sample implementation of a weak classifier using Unity reflection.
    /// </summary>
    public class WeakClassifier : IWeakClassifier
    {
        private readonly object model;
        private readonly MethodInfo decideMethod;

        /// <summary>
        ///   Initializes a new instance of the <see cref="WeakClassifier"/> class.
        /// </summary>
        /// <param name="model">The underlying model object.</param>
        /// <param name="decideMethodName">The name of the method used for decision-making.</param>
        public WeakClassifier(object model, string decideMethodName)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Model cannot be null.");

            this.model = model;

            // Use reflection to find the method
            decideMethod = model.GetType().GetMethod(decideMethodName, BindingFlags.Public | BindingFlags.Instance);
            if (decideMethod == null)
                throw new ArgumentException($"The method '{decideMethodName}' was not found in the provided model.");
        }

        /// <summary>
        ///   Computes the output class label for a given input using the model.
        /// </summary>
        /// <param name="inputs">The input vector.</param>
        /// <returns>The most likely class label for the given input.</returns>
        public int Compute(double[] inputs)
        {
            if (inputs == null)
                throw new ArgumentNullException(nameof(inputs), "Input vector cannot be null.");

            // Use reflection to invoke the decision method on the model
            var result = decideMethod.Invoke(model, new object[] { inputs });

            if (result is bool boolResult)
                return boolResult ? 1 : 0;
            if (result is int intResult)
                return intResult;

            throw new InvalidOperationException("The decision method returned an unsupported type.");
        }
    }

    /// <summary>
    ///   A sample weak classifier model for testing.
    /// </summary>
    public class ExampleModel
    {
        public bool Decide(double[] inputs)
        {
            // Example decision logic
            double sum = 0;
            foreach (var value in inputs)
            {
                sum += value;
            }
            return sum > 0.5 * inputs.Length; // Decision based on average
        }
    }
}
