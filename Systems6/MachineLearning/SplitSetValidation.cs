using System;
using System.Collections.Generic;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Split-set validation for machine learning models.
    /// </summary>
    /// <typeparam name="TModel">The type of the model being validated.</typeparam>
    public class SplitSetValidation2<TModel> where TModel : class
    {
        /// <summary>
        /// The indices of all available samples.
        /// </summary>
        public int[] Indices { get; private set; }

        /// <summary>
        /// The proportion of training samples compared to the validation samples.
        /// </summary>
        public double Proportion { get; private set; }

        /// <summary>
        /// Indicates if the split is stratified.
        /// </summary>
        public bool IsStratified { get; private set; }

        /// <summary>
        /// The indices of the validation set.
        /// </summary>
        public int[] ValidationSet { get; private set; }

        /// <summary>
        /// The indices of the training set.
        /// </summary>
        public int[] TrainingSet { get; private set; }

        /// <summary>
        /// The function for fitting the model on the training set.
        /// </summary>
        public Func<int[], SplitSetStatistics<TModel>> Fitting { get; set; }

        /// <summary>
        /// The function for evaluating the model on the validation set.
        /// </summary>
        public Func<int[], TModel, SplitSetStatistics<TModel>> Evaluation { get; set; }

        /// <summary>
        /// Creates a new split-set validation instance.
        /// </summary>
        /// <param name="size">The total number of available samples.</param>
        /// <param name="proportion">The proportion of training samples.</param>
        public SplitSetValidation2(int size, double proportion)
        {
            Proportion = proportion;
            IsStratified = false;
            Indices = GenerateRandomIndices(size, proportion);

            ValidationSet = Indices.Where(x => x == 0).ToArray();
            TrainingSet = Indices.Where(x => x == 1).ToArray();
        }

        /// <summary>
        /// Creates a new stratified split-set validation instance.
        /// </summary>
        /// <param name="size">The total number of available samples.</param>
        /// <param name="proportion">The proportion of training samples.</param>
        /// <param name="outputs">The output labels for stratification.</param>
        public SplitSetValidation2(int size, double proportion, int[] outputs)
        {
            if (outputs.Length != size)
                throw new ArgumentException("The number of outputs must match the number of samples.");

            Proportion = proportion;
            IsStratified = true;

            var negativeIndices = outputs
                .Select((value, index) => new { value, index })
                .Where(x => x.value == outputs.Min())
                .Select(x => x.index)
                .ToList();

            var positiveIndices = outputs
                .Select((value, index) => new { value, index })
                .Where(x => x.value == outputs.Max())
                .Select(x => x.index)
                .ToList();

            TrainingSet = CreateStratifiedSplit(negativeIndices, positiveIndices, proportion);
            ValidationSet = negativeIndices.Concat(positiveIndices).ToArray();
        }

        /// <summary>
        /// Runs the split-set validation process.
        /// </summary>
        /// <returns>The result of the validation.</returns>
        public class SplitSetResult<TModel> where TModel : class
        {
            public SplitSetValidation<TModel> Validation { get; private set; }
            public SplitSetStatistics<TModel> TrainingStatistics { get; private set; }
            public SplitSetStatistics<TModel> ValidationStatistics { get; private set; }

            public SplitSetResult(
                SplitSetValidation<TModel> validation,
                SplitSetStatistics<TModel> trainingStatistics,
                SplitSetStatistics<TModel> validationStatistics)
            {
                Validation = validation;
                TrainingStatistics = trainingStatistics;
                ValidationStatistics = validationStatistics;
            }
        }





        private static int[] GenerateRandomIndices(int size, double proportion)
        {
            var random = new Random();
            var indices = new int[size];
            int trainingCount = (int)(size * proportion);

            for (int i = 0; i < trainingCount; i++)
                indices[i] = 1;

            return indices.OrderBy(_ => random.Next()).ToArray();
        }

        private static int[] CreateStratifiedSplit(
            List<int> negativeIndices, List<int> positiveIndices, double proportion)
        {
            int negTrainCount = (int)(negativeIndices.Count * proportion);
            int posTrainCount = (int)(positiveIndices.Count * proportion);

            var trainingSet = new List<int>();
            trainingSet.AddRange(negativeIndices.Take(negTrainCount));
            trainingSet.AddRange(positiveIndices.Take(posTrainCount));

            negativeIndices.RemoveRange(0, negTrainCount);
            positiveIndices.RemoveRange(0, posTrainCount);

            return trainingSet.ToArray();
        }
    }

}
