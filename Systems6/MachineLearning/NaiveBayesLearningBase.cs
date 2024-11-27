using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public abstract class NaiveBayesLearningBase<TModel, TDistribution, TInput, TOptions>
        where TDistribution : class
        where TOptions : class, new()
        where TModel : class
    {
        private bool optimized = false;

        public TModel Model { get; set; }
        public bool Empirical { get; set; }
        public TOptions Options { get; set; }
        public Func<int, int, TDistribution> Distribution { get; set; }
        public CancellationToken Token { get; set; }

        protected NaiveBayesLearningBase()
        {
            this.Empirical = true;
            this.Options = new TOptions();
            this.Distribution = (classIndex, variableIndex) =>
            {
                try
                {
                    return Activator.CreateInstance(typeof(TDistribution)) as TDistribution;
                }
                catch
                {
                    throw new InvalidOperationException("Please specify how the initial distributions should be created.");
                }
            };
        }

        protected abstract TModel Create(TInput[][] x, int y);

        public virtual TModel Learn(TInput[][] x, int[] y, double[] weight = null)
        {
            ValidateArguments(x, y, weight);

            if (Model == null)
                Model = Create(x, y.Max() + 1);

            Parallel.For(0, GetOutputCount(), new ParallelOptions { CancellationToken = Token }, i =>
            {
                InnerLearn(x, y, weight, i);
            });

            return Model;
        }

        private void InnerLearn(TInput[][] x, int[] y, double[] weight, int classIndex)
        {
            int[] sampleIndicesInClass = Enumerable.Range(0, y.Length).Where(idx => y[idx] == classIndex).ToArray();
            var samplesInClass = sampleIndicesInClass.Select(idx => x[idx]).ToArray();

            if (Empirical)
            {
                SetModelPrior(classIndex, (double)sampleIndicesInClass.Length / x.Length);
            }

            Fit(classIndex, samplesInClass, weight, transposed: true);
        }

        protected virtual void Fit(int i, TInput[][] values, double[] weights, bool transposed)
        {
            var options = this.Options;

            var fitMethod = typeof(TDistribution).GetMethod("Fit", BindingFlags.Public | BindingFlags.Instance);
            if (fitMethod != null)
            {
                fitMethod.Invoke(Model, new object[] { values, weights, options });
                this.optimized = true;
            }
            else
            {
                throw new InvalidOperationException("The provided distribution does not support fitting.");
            }
        }

        private void ValidateArguments(TInput[][] x, int[] y, double[] weight)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Input data or labels cannot be null.");

            if (weight != null && weight.Length != x.Length)
                throw new ArgumentException("Weights must match the number of samples.");
        }

        private int GetOutputCount()
        {
            var outputCountProperty = typeof(TModel).GetProperty("NumberOfOutputs", BindingFlags.Public | BindingFlags.Instance);
            if (outputCountProperty == null)
                throw new InvalidOperationException("Model must define NumberOfOutputs.");

            return (int)outputCountProperty.GetValue(Model);
        }

        private void SetModelPrior(int classIndex, double value)
        {
            var priorsProperty = typeof(TModel).GetProperty("Priors", BindingFlags.Public | BindingFlags.Instance);
            if (priorsProperty == null)
                throw new InvalidOperationException("Model must define Priors.");

            var priors = priorsProperty.GetValue(Model) as IList<double>;
            if (priors == null || priors.Count <= classIndex)
                throw new InvalidOperationException("Invalid Priors property configuration.");

            priors[classIndex] = value;
        }
    }
}
