using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class NaiveBayesLearning : NaiveBayesLearningBase<object, object, int, object>
    {
        // Constructor for NaiveBayesLearning
        public NaiveBayesLearning()
        {
            this.Options = new object(); // Replace with appropriate options class if needed.
        }

        /// <summary>
        /// Creates an instance of the NaiveBayes model to be learned.
        /// </summary>
        protected override object Create(int[][] x, int y)
        {
            int[] inputs = x.Select(row => row.Max()).ToArray();
            inputs = inputs.Select(i => i + 1).ToArray();
            Debug.Assert(inputs.Length == x[0].Length);

            var naiveBayesType = Type.GetType("NaiveBayes");
            if (naiveBayesType == null)
                throw new InvalidOperationException("NaiveBayes class not found.");

            var constructor = naiveBayesType.GetConstructor(new[] { typeof(int), typeof(int[]) });
            if (constructor == null)
                throw new InvalidOperationException("NaiveBayes constructor not found.");

            return constructor.Invoke(new object[] { y, inputs });
        }

        /// <summary>
        /// Learns a model that maps inputs to outputs.
        /// </summary>
        public override object Learn(int[][] x, int[] y, double[] weight = null)
        {
            ValidateArguments(x, y, weight);

            if (Model == null)
                Model = Create(x, y.Distinct().Count());

            var outputCount = GetOutputCount();
            for (int i = 0; i < outputCount; i++)
            {
                InnerLearn(x, y, i);
            }

            return Model;
        }

        private void InnerLearn(int[][] x, int[] y, int classIndex)
        {
            var sampleIndices = Enumerable.Range(0, y.Length).Where(idx => y[idx] == classIndex).ToArray();
            var samplesInClass = sampleIndices.Select(idx => x[idx]).ToArray();

            if (Empirical)
            {
                SetModelPrior(classIndex, (double)sampleIndices.Length / x.Length);
            }

            for (int inputIndex = 0; inputIndex < x[0].Length; inputIndex++)
            {
                InnerEstimate(classIndex, inputIndex, samplesInClass);
            }
        }

        private void InnerEstimate(int classIndex, int inputIndex, int[][] values)
        {
            var symbolCount = GetSymbolCount(inputIndex);
            if (symbolCount > 1)
            {
                var frequencies = new double[symbolCount];
                foreach (var row in values)
                {
                    frequencies[row[inputIndex]]++;
                }

                var distribution = GetDistribution(classIndex, inputIndex);
                TransformToProbabilities(frequencies, distribution);
            }
        }

        private void TransformToProbabilities(double[] frequencies, object distribution)
        {
            var sum = frequencies.Sum();
            var probabilities = frequencies.Select(f => f / sum).ToArray();

            var setProbabilitiesMethod = distribution.GetType().GetMethod("SetProbabilities");
            if (setProbabilitiesMethod == null)
                throw new InvalidOperationException("SetProbabilities method not found.");

            setProbabilitiesMethod.Invoke(distribution, new object[] { probabilities });
        }

        private void ValidateArguments(int[][] x, int[] y, double[] weight)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Input data or labels cannot be null.");

            if (weight != null && weight.Length != x.Length)
                throw new ArgumentException("Weights must match the number of samples.");
        }

        private int GetOutputCount()
        {
            var outputCountProperty = Model.GetType().GetProperty("NumberOfOutputs");
            if (outputCountProperty == null)
                throw new InvalidOperationException("Model must define NumberOfOutputs.");

            return (int)outputCountProperty.GetValue(Model);
        }

        private void SetModelPrior(int classIndex, double value)
        {
            var priorsProperty = Model.GetType().GetProperty("Priors");
            if (priorsProperty == null)
                throw new InvalidOperationException("Model must define Priors.");

            var priors = priorsProperty.GetValue(Model) as IList<double>;
            priors[classIndex] = value;
        }

        private int GetSymbolCount(int inputIndex)
        {
            var symbolCountProperty = Model.GetType().GetProperty("NumberOfSymbols");
            if (symbolCountProperty == null)
                throw new InvalidOperationException("Model must define NumberOfSymbols.");

            var symbolCounts = symbolCountProperty.GetValue(Model) as int[];
            return symbolCounts[inputIndex];
        }

        private object GetDistribution(int classIndex, int inputIndex)
        {
            var distributionsProperty = Model.GetType().GetProperty("Distributions");
            if (distributionsProperty == null)
                throw new InvalidOperationException("Model must define Distributions.");

            var distributions = distributionsProperty.GetValue(Model) as object[,];
            return distributions[classIndex, inputIndex];
        }
    }
}
