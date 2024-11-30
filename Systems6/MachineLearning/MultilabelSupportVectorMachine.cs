using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public enum MultilabelProbabilityMethod
    {
        PerClass,
        SumsToOne,
        SumsToOneWithEmphasisOnWinner
    }

    public class MultilabelSupportVectorMachine<TModel, TKernel, TInput> : IDisposable
        where TKernel : class
        where TModel : class
    {
        private List<TModel> models;
        private MultilabelProbabilityMethod method = MultilabelProbabilityMethod.PerClass;

        private int numberOfClasses;
        private Dictionary<string, double[]> cache;

        public MultilabelProbabilityMethod Method
        {
            get => method;
            set => method = value;
        }

        public MultilabelSupportVectorMachine(int classes, Func<TModel> modelInitializer)
        {
            numberOfClasses = classes;
            models = new List<TModel>();
            cache = new Dictionary<string, double[]>();

            // Initialize models
            for (int i = 0; i < classes; i++)
            {
                models.Add(modelInitializer());
            }
        }

        public bool[] Decide(TInput input, Func<TInput, int, double> scoringFunction)
        {
            bool[] decisions = new bool[numberOfClasses];
            for (int i = 0; i < numberOfClasses; i++)
            {
                string cacheKey = $"Class-{i}";
                double score;

                if (cache.TryGetValue(cacheKey, out var cachedValues))
                {
                    score = cachedValues[0];
                }
                else
                {
                    score = scoringFunction(input, i);
                    cache[cacheKey] = new[] { score };
                }

                decisions[i] = score > 0;
            }
            return decisions;
        }

        public double[] Probabilities(TInput input, Func<TInput, int, double> scoringFunction)
        {
            double[] probabilities = new double[numberOfClasses];
            bool[] decisions = Decide(input, scoringFunction);

            for (int i = 0; i < numberOfClasses; i++)
            {
                double score = scoringFunction(input, i);

                if (method == MultilabelProbabilityMethod.PerClass)
                {
                    probabilities[i] = Math.Exp(score);
                }
                else if (method == MultilabelProbabilityMethod.SumsToOne)
                {
                    probabilities[i] = Math.Exp(score);
                }
                else if (method == MultilabelProbabilityMethod.SumsToOneWithEmphasisOnWinner)
                {
                    double rest = 1.0 - Math.Exp(score);
                    probabilities[i] = (rest / (numberOfClasses - 1));
                }
            }

            if (method != MultilabelProbabilityMethod.PerClass)
            {
                double sum = 0;
                for (int i = 0; i < probabilities.Length; i++)
                {
                    sum += probabilities[i];
                }
                for (int i = 0; i < probabilities.Length; i++)
                {
                    probabilities[i] /= sum;
                }
            }

            return probabilities;
        }

        public void Reset()
        {
            cache.Clear();
            Debug.Log("Machine cache has been reset.");
        }

        public void Dispose()
        {
            Reset();
            Debug.Log("Machine disposed.");
        }
    }
}
