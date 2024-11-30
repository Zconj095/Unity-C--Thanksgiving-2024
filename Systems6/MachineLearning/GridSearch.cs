using System;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Grid search procedure for automatic parameter tuning.
    /// </summary>
    public static class GridSearch
    {
        /// <summary>
        /// Creates a grid search range of parameter values.
        /// </summary>
        /// <typeparam name="T">The type of parameter values.</typeparam>
        /// <param name="values">The parameter values to include in the grid search.</param>
        /// <returns>A grid search range containing the specified values.</returns>
        public static GridSearchRange<T> Values<T>(params T[] values)
        {
            return new GridSearchRange<T> { Values = values };
        }

        /// <summary>
        /// Executes a grid search over a range of parameters to find the best model.
        /// </summary>
        /// <typeparam name="TModel">The type of the machine learning model.</typeparam>
        /// <typeparam name="TInput">The type of the input data.</typeparam>
        /// <typeparam name="TOutput">The type of the output data.</typeparam>
        /// <param name="ranges">The parameter ranges for the grid search.</param>
        /// <param name="createModel">Function to create a model given a parameter configuration.</param>
        /// <param name="evaluateModel">Function to evaluate a model and return its performance.</param>
        /// <returns>The best model and its performance.</returns>
        public static (TModel BestModel, double BestPerformance) PerformSearch<TModel, TInput, TOutput>(
            Dictionary<string, GridSearchRange> ranges,
            Func<Dictionary<string, object>, TModel> createModel,
            Func<TModel, TInput[], TOutput[], double> evaluateModel,
            TInput[] inputs,
            TOutput[] outputs)
        {
            double bestPerformance = double.MaxValue;
            TModel bestModel = default;

            var parameterCombinations = GenerateParameterCombinations(ranges);

            foreach (var parameters in parameterCombinations)
            {
                TModel model = createModel(parameters);
                double performance = evaluateModel(model, inputs, outputs);

                if (performance < bestPerformance)
                {
                    bestPerformance = performance;
                    bestModel = model;
                }
            }

            return (bestModel, bestPerformance);
        }

        /// <summary>
        /// Generates all possible combinations of parameters from the given ranges.
        /// </summary>
        /// <param name="ranges">The parameter ranges for the grid search.</param>
        /// <returns>A list of parameter combinations.</returns>
        private static List<Dictionary<string, object>> GenerateParameterCombinations(
            Dictionary<string, GridSearchRange> ranges)
        {
            var keys = new List<string>(ranges.Keys);
            var values = new List<object[]>(keys.Count);

            foreach (var key in keys)
            {
                values.Add(ranges[key].Values);
            }

            var combinations = new List<Dictionary<string, object>>();
            GenerateCombinationsRecursive(keys, values, new Dictionary<string, object>(), combinations, 0);

            return combinations;
        }

        /// <summary>
        /// Recursively generates all parameter combinations.
        /// </summary>
        private static void GenerateCombinationsRecursive(
            List<string> keys,
            List<object[]> values,
            Dictionary<string, object> current,
            List<Dictionary<string, object>> combinations,
            int depth)
        {
            if (depth == keys.Count)
            {
                combinations.Add(new Dictionary<string, object>(current));
                return;
            }

            string key = keys[depth];
            foreach (var value in values[depth])
            {
                current[key] = value;
                GenerateCombinationsRecursive(keys, values, current, combinations, depth + 1);
            }
        }
    }

    /// <summary>
    /// Represents a range of values for a parameter in the grid search.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    public class GridSearchRange<T>
    {
        /// <summary>
        /// The values included in the grid search range.
        /// </summary>
        public T[] Values { get; set; }
    }

    /// <summary>
    /// Represents a generic grid search range.
    /// </summary>
    public class GridSearchRange
    {
        /// <summary>
        /// The values included in the grid search range.
        /// </summary>
        public object[] Values { get; set; }
    }
}
