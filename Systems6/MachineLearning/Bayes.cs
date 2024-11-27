using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class Bayes<TDistribution, TInput> where TDistribution : class
    {
        private TDistribution[] distributions;
        private double[] priors;

        public TDistribution[] Distributions
        {
            get => distributions;
            set
            {
                if (value == null || value.Length != distributions.Length)
                    throw new ArgumentException("Dimension mismatch for distributions.");
                distributions = value;
            }
        }

        public double[] Priors
        {
            get => priors;
            set
            {
                if (value == null || value.Length != priors.Length)
                    throw new ArgumentException("Dimension mismatch for priors.");
                priors = value;
            }
        }

        public int NumberOfClasses { get; private set; }
        public int NumberOfInputs { get; private set; }

        public Bayes(int classes, int inputs, Func<TDistribution> initializer)
        {
            Initialize(classes, inputs);
            for (int i = 0; i < distributions.Length; i++)
            {
                distributions[i] = initializer();
            }
        }

        public Bayes(int classes, int inputs, Func<int, TDistribution> initializer)
        {
            Initialize(classes, inputs);
            for (int i = 0; i < distributions.Length; i++)
            {
                distributions[i] = initializer(i);
            }
        }

        private void Initialize(int classes, int inputs)
        {
            if (classes < 2)
                throw new ArgumentOutOfRangeException(nameof(classes), "Number of classes must be greater than 1.");

            if (inputs <= 0)
                throw new ArgumentOutOfRangeException(nameof(inputs), "Number of inputs must be greater than 0.");

            NumberOfClasses = classes;
            NumberOfInputs = inputs;

            distributions = new TDistribution[classes];
            priors = Enumerable.Repeat(1.0 / classes, classes).ToArray();
        }

        public double LogLikelihood(TInput input, int classIndex)
        {
            if (classIndex < 0 || classIndex >= distributions.Length)
                throw new ArgumentOutOfRangeException(nameof(classIndex), "Invalid class index.");

            var distribution = distributions[classIndex];
            if (distribution == null)
                throw new InvalidOperationException($"No distribution defined for class {classIndex}.");

            var logProbabilityFunction = distribution.GetType().GetMethod("LogProbabilityFunction", BindingFlags.Public | BindingFlags.Instance);
            if (logProbabilityFunction == null)
                throw new InvalidOperationException("The distribution does not implement a LogProbabilityFunction method.");

            var logProbability = (double)logProbabilityFunction.Invoke(distribution, new object[] { input });
            return Math.Log(priors[classIndex]) + logProbability;
        }
    }
}
