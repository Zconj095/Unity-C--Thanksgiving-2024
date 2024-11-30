using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class AveragedStochasticGradientDescent<TModel, TKernel, TInput, TLoss>
        where TKernel : struct
        where TInput : IList<float>
        where TLoss : struct
    {
        private TKernel kernel;
        private double lambda = 1e-3;
        private double eta0 = 0.01;
        private double mu0 = 1.0;
        private double tstart;
        private double[] weights;
        private double[] averagedWeights;
        private double weightDivisor = 1.0;
        private double weightBias;
        private double averagedWeightBias;

        public TKernel Kernel
        {
            get => kernel;
            set => kernel = value;
        }

        public double LearningRate
        {
            get => eta0;
            set => eta0 = value;
        }

        public double Lambda
        {
            get => lambda;
            set => lambda = value;
        }

        public void Train(IEnumerable<TInput> inputs, IEnumerable<bool> outputs)
        {
            Type modelType = typeof(TModel);
            MethodInfo scoreMethod = modelType.GetMethod("Score");
            if (scoreMethod == null)
            {
                Debug.LogError($"Method 'Score' not found in {modelType.Name}");
                return;
            }

            foreach (var (input, output) in PairInputsOutputs(inputs, outputs))
            {
                double eta = eta0 / (1 + lambda * eta0);
                TrainOne(input, output, eta, 1.0);
            }
        }

        private void TrainOne(TInput input, bool output, double eta, double mu)
        {
            // Ensure weights are initialized
            if (weights == null)
                weights = new double[input.Count];

            // Weight adjustment
            for (int i = 0; i < weights.Length; i++)
                weights[i] *= (1 - eta * lambda);

            // Update weights using the input and output
            for (int i = 0; i < input.Count; i++)
            {
                double gradient = (output ? 1 : -1) * input[i];
                weights[i] += eta * gradient;
            }

            // Update averaged weights
            if (averagedWeights == null)
                averagedWeights = new double[weights.Length];

            for (int i = 0; i < weights.Length; i++)
            {
                averagedWeights[i] = (averagedWeights[i] + weights[i]) / 2.0;
            }

            // Update biases
            weightBias *= (1 - eta * lambda);
            weightBias += eta * (output ? 1 : -1);
            averagedWeightBias += mu * (weightBias - averagedWeightBias);
        }

        private double Predict(TInput input)
        {
            if (weights == null)
                throw new InvalidOperationException("Model has not been trained yet.");

            double sum = 0.0;
            for (int i = 0; i < input.Count; i++)
            {
                sum += weights[i] * input[i];
            }

            return sum + weightBias;
        }

        private double EvaluateLoss(IEnumerable<TInput> inputs, IEnumerable<bool> outputs)
        {
            var inputList = inputs.ToList();
            var outputList = outputs.ToList();
            if (inputList.Count != outputList.Count)
                throw new ArgumentException("Input and output count mismatch.");

            double totalLoss = 0.0;
            for (int i = 0; i < inputList.Count; i++)
            {
                double prediction = Predict(inputList[i]);
                double actual = outputList[i] ? 1.0 : -1.0;
                totalLoss += 0.5 * Math.Pow(prediction - actual, 2);
            }

            return totalLoss / inputList.Count;
        }

        private IEnumerable<(TInput, bool)> PairInputsOutputs(IEnumerable<TInput> inputs, IEnumerable<bool> outputs)
        {
            var inputEnumerator = inputs.GetEnumerator();
            var outputEnumerator = outputs.GetEnumerator();

            while (inputEnumerator.MoveNext() && outputEnumerator.MoveNext())
            {
                yield return (inputEnumerator.Current, outputEnumerator.Current);
            }
        }

        [Serializable]
        private class ModelData
        {
            // Consider serializing kernel if necessary
            // public TKernel Kernel;
            public double[] Weights;
            public double[] AveragedWeights;
            public double Bias;
            public double AveragedBias;
        }

        public void SaveModel(string path)
        {
            var modelData = new ModelData
            {
                // Kernel = kernel,
                Weights = weights,
                AveragedWeights = averagedWeights,
                Bias = weightBias,
                AveragedBias = averagedWeightBias
            };

            string json = JsonUtility.ToJson(modelData);
            System.IO.File.WriteAllText(path, json);
            Debug.Log($"Model saved to {path}");
        }

        public void LoadModel(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                Debug.LogError($"Model file not found at {path}");
                return;
            }

            string json = System.IO.File.ReadAllText(path);
            var modelData = JsonUtility.FromJson<ModelData>(json);

            // kernel = modelData.Kernel;
            weights = modelData.Weights;
            averagedWeights = modelData.AveragedWeights;
            weightBias = modelData.Bias;
            averagedWeightBias = modelData.AveragedBias;

            Debug.Log($"Model loaded from {path}");
        }
    }
}
