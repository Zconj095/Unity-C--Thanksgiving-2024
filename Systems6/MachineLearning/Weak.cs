using System;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Adapter for models that do not implement a .Decide function.
    /// </summary>
    /// <typeparam name="TModel">The type for the weak classifier model.</typeparam>
    public class Weak<TModel>
    {
        /// <summary>
        ///   Gets or sets the weak decision model.
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        ///   Gets or sets the decision function used by the <see cref="Model"/>.
        /// </summary>
        public MethodInfo DecisionFunction { get; private set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Weak{TModel}"/> class.
        /// </summary>
        /// <param name="model">The classifier model.</param>
        /// <param name="methodName">The name of the decision function method in the model.</param>
        public Weak(TModel model, string methodName)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));

            // Use reflection to locate the decision function
            DecisionFunction = typeof(TModel).GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            if (DecisionFunction == null)
            {
                throw new ArgumentException($"The method '{methodName}' was not found in the model type '{typeof(TModel).Name}'.");
            }
        }

        /// <summary>
        ///   Computes the classifier decision for a given input using reflection.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>The model's decision label.</returns>
        public int Compute(double[] input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var result = DecisionFunction.Invoke(Model, new object[] { input });
            if (result is int intResult)
                return intResult;

            throw new InvalidOperationException("The decision function must return an integer label.");
        }

        /// <summary>
        ///   Computes a binary decision (true/false) based on the model's output.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>True if the decision label corresponds to the positive class, false otherwise.</returns>
        public bool Decide(double[] input)
        {
            int result = Compute(input);
            return result > 0;
        }
    }

    /// <summary>
    ///   A sample weak model for testing purposes.
    /// </summary>
    public class ExampleWeakModel
    {
        /// <summary>
        ///   Sample decision function that classifies input based on the sum of its values.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>An integer label (+1 or -1).</returns>
        public int DecisionFunction(double[] input)
        {
            double sum = 0;
            foreach (var value in input)
                sum += value;

            return sum > 0.5 * input.Length ? 1 : -1;
        }
    }
}
