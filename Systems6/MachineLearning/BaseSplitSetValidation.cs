using System;
using System.Linq;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Base class for performance measurement methods based on splitting the data into multiple sets.
    /// </summary>
    /// <typeparam name="TResult">The type of the result learned by the validation method.</typeparam>
    /// <typeparam name="TModel">The type of the machine learning model.</typeparam>
    /// <typeparam name="TLearner">The type of the learning algorithm used to learn <typeparamref name="TModel"/>.</typeparam>
    /// <typeparam name="TInput">The type of the input data.</typeparam>
    /// <typeparam name="TOutput">The type of the output data or labels.</typeparam>
    public abstract class BaseSplitSetValidation<TResult, TModel, TLearner, TInput, TOutput>
        where TModel : class, ITransform<TInput, TOutput>
        where TResult : ITransform<TInput, TOutput>
    {
        public double? DefaultValue { get; set; }
        public Func<DataSubset<TInput, TOutput>, TLearner> Learner { get; set; }
        public Func<TLearner, TInput[], TOutput[], double[], TModel> Fit { get; set; }
        public Func<TOutput[], TOutput[], SetResult<TModel>, double> Loss { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSplitSetValidation"/> class.
        /// </summary>
        protected BaseSplitSetValidation() { }

        /// <summary>
        /// Learns a model that can map the given inputs to the given outputs.
        /// </summary>
        /// <param name="x">The model inputs.</param>
        /// <param name="y">The desired outputs associated with each input.</param>
        /// <param name="weights">The weight of importance for each input-output pair.</param>
        /// <returns>A model that has learned how to produce outputs from inputs.</returns>
        public abstract TResult Learn(TInput[] x, TOutput[] y, double[] weights = null);

        /// <summary>
        /// Learns and evaluates a model in a single subset of the data.
        /// </summary>
        /// <param name="subset">The subset of the data containing the training and testing subsets.</param>
        /// <param name="index">The index of this subset, if applicable.</param>
        /// <returns>A result object containing the created model and its performance.</returns>
        protected SplitResult<TModel, TInput, TOutput> LearnSubset(TrainValDataSplit<TInput, TOutput> subset, int index = 0)
        {
            // Ensure Training and Validation are of the correct type
            var trainingData = subset.Training as DataSubset<TInput, TOutput>;
            var validationData = subset.Validation as DataSubset<TInput, TOutput>;

            if (trainingData == null || validationData == null)
            {
                throw new InvalidOperationException("Training and Validation subsets must be of type DataSubset<TInput, TOutput>.");
            }

            // Create the learning algorithm
            TLearner teacher = Learner(trainingData);

            var trainInputs = trainingData.Inputs;
            var trainOutputs = trainingData.Outputs;
            var trainWeights = trainingData.Weights;

            try
            {
                // Train the model
                TModel model = Fit(teacher, trainInputs, trainOutputs, trainWeights);

                // Evaluate the model
                var valInputs = validationData.Inputs;
                var valOutputs = validationData.Outputs;
                var valWeights = validationData.Weights;

                var trainResult = new SetResult<TModel>(model, trainingData.Indices, "Training", trainingData.Proportion)
                {
                    Value = Loss(trainOutputs, model.Transform(trainInputs), null)
                };

                var valResult = new SetResult<TModel>(model, validationData.Indices, "Validation", validationData.Proportion)
                {
                    Value = Loss(valOutputs, model.Transform(valInputs), null)
                };

                return new SplitResult<TModel, TInput, TOutput>(model, index)
                {
                    Training = trainResult,
                    Validation = valResult
                };
            }
            catch
            {
                if (!DefaultValue.HasValue)
                    throw;

                return new SplitResult<TModel, TInput, TOutput>(null, index)
                {
                    Training = new SetResult<TModel>(null, trainingData.Indices, "Training", trainingData.Proportion)
                    {
                        Value = DefaultValue.Value
                    },
                    Validation = new SetResult<TModel>(null, validationData.Indices, "Validation", validationData.Proportion)
                    {
                        Value = DefaultValue.Value
                    }
                };
            }
        }
    }
}
