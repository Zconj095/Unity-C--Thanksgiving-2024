using System;
using System.Collections.Generic;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Base class for performing grid search over hyperparameters.
    /// </summary>
    public abstract class BaseGridSearch<TResult, TModel, TRange, TParam, TLearner, TInput, TOutput>
        where TModel : class
        where TResult : GridSearchResult<TModel, TParam>, new()
    {
        public TRange ParameterRanges { get; set; }
        public Func<TParam, TLearner> Learner { get; set; }
        public Func<TLearner, TInput[], TOutput[], double[], TModel> Fit { get; set; }
        public Func<TOutput[], TOutput[], TModel, double> Loss { get; set; }

        /// <summary>
        /// Performs grid search to find the best model.
        /// </summary>
        /// <param name="inputs">The input data.</param>
        /// <param name="outputs">The expected outputs.</param>
        /// <param name="weights">Optional weights for training data.</param>
        /// <returns>The result of the grid search.</returns>
        public TResult Learn(TInput[] inputs, TOutput[] outputs, double[] weights = null)
        {
            if (ParameterRanges == null || Learner == null || Fit == null || Loss == null)
                throw new InvalidOperationException("All required properties (ParameterRanges, Learner, Fit, Loss) must be set.");

            var parameterCombinations = GenerateParameterCombinations();
            var results = new List<GridSearchResultEntry<TModel, TParam>>();

            foreach (var parameters in parameterCombinations)
            {
                var learner = Learner(parameters);
                TModel model = null;
                double error;

                try
                {
                    model = Fit(learner, inputs, outputs, weights);
                    var predictions = Predict(model, inputs);
                    error = Loss(outputs, predictions, model);
                }
                catch (Exception ex)
                {
                    error = double.PositiveInfinity; // Assign a high error value for failed attempts
                }

                results.Add(new GridSearchResultEntry<TModel, TParam>(parameters, model, error));
            }

            var bestResult = results.OrderBy(r => r.Error).First();
            return new TResult
            {
                BestModel = bestResult.Model,
                BestParameters = bestResult.Parameters,
                Results = results
            };
        }

        /// <summary>
        /// Predict outputs using the trained model.
        /// </summary>
        /// <param name="model">The trained model.</param>
        /// <param name="inputs">The input data.</param>
        /// <returns>Predicted outputs.</returns>
        protected abstract TOutput[] Predict(TModel model, TInput[] inputs);

        /// <summary>
        /// Generates all possible parameter combinations from the parameter ranges.
        /// </summary>
        /// <returns>A list of parameter combinations.</returns>
        private List<TParam> GenerateParameterCombinations()
        {
            var parameterCounts = GetLengths();
            var indices = CartesianProduct(parameterCounts);

            return indices.Select(GetParameters).ToList();
        }

        /// <summary>
        /// Generates Cartesian product of parameter indices.
        /// </summary>
        /// <param name="counts">The number of possible values for each parameter.</param>
        /// <returns>All combinations of parameter indices.</returns>
        private IEnumerable<int[]> CartesianProduct(int[] counts)
        {
            var results = new List<int[]> { new int[0] };

            foreach (var count in counts)
            {
                var temp = new List<int[]>();
                foreach (var result in results)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var newCombination = result.Concat(new[] { i }).ToArray();
                        temp.Add(newCombination);
                    }
                }

                results = temp;
            }

            return results;
        }

        /// <summary>
        /// Converts a set of indices to parameter values.
        /// </summary>
        /// <param name="indices">The indices for the parameters.</param>
        /// <returns>The parameter combination.</returns>
        protected abstract TParam GetParameters(int[] indices);

        /// <summary>
        /// Gets the number of possible values for each parameter in the range.
        /// </summary>
        /// <returns>An array containing the number of possible values for each parameter.</returns>
        protected abstract int[] GetLengths();
    }

    /// <summary>
    /// Represents the result of a grid search.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TParam">The type of the parameters.</typeparam>
    public class GridSearchResult<TModel, TParam>
    {
        public TModel BestModel { get; set; }
        public TParam BestParameters { get; set; }
        public List<GridSearchResultEntry<TModel, TParam>> Results { get; set; }
    }

    /// <summary>
    /// Represents an entry in the grid search results.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TParam">The type of the parameters.</typeparam>
    public class GridSearchResultEntry<TModel, TParam>
    {
        public TParam Parameters { get; }
        public TModel Model { get; }
        public double Error { get; }

        public GridSearchResultEntry(TParam parameters, TModel model, double error)
        {
            Parameters = parameters;
            Model = model;
            Error = error;
        }
    }
}
