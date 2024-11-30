using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace EdgeLoreMachineLearning
{
    public enum LossFunctionType
    {
        LogisticLoss
    }

    public class StochasticGradientDescent : MonoBehaviour
    {
        // Parameters
        private double learningRate = 0.01;
        private double regularizationTerm = 1e-5;
        private double tolerance = 1e-5;
        private int maxIterations = 100;
        private bool useBias = true;

        private double[] weights;
        private double bias;
        private int iterationCount;

        // Reflection utilities
        private FieldInfo[] fields;

        void Start()
        {
            Init();
        }

        private void Init()
        {
            weights = null;
            bias = 0;
            iterationCount = 0;

            // Use reflection to inspect internal state
            fields = typeof(StochasticGradientDescent).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Log("Reflection-enabled fields for debugging:");
            foreach (var field in fields)
            {
                Debug.Log($"Field: {field.Name}, Type: {field.FieldType}");
            }
        }

        public void Train(double[][] inputs, bool[] outputs, LossFunctionType lossType = LossFunctionType.LogisticLoss)
        {
            int numSamples = inputs.Length;
            int numFeatures = inputs[0].Length;

            // Initialize weights and bias
            if (weights == null)
            {
                weights = new double[numFeatures];
                for (int i = 0; i < numFeatures; i++) weights[i] = UnityEngine.Random.value;
            }

            for (iterationCount = 0; iterationCount < maxIterations; iterationCount++)
            {
                double totalLoss = 0;

                for (int i = 0; i < numSamples; i++)
                {
                    double predicted = ComputeScore(inputs[i]);
                    bool actual = outputs[i];

                    // Loss function derivative (logistic loss for this example)
                    double error = ComputeError(predicted, actual, lossType);

                    // Update weights
                    for (int j = 0; j < numFeatures; j++)
                    {
                        weights[j] -= learningRate * (error * inputs[i][j] + regularizationTerm * weights[j]);
                    }

                    // Update bias
                    if (useBias)
                    {
                        bias -= learningRate * error;
                    }

                    // Accumulate total loss
                    totalLoss += Math.Abs(error);
                }

                Debug.Log($"Iteration {iterationCount + 1}, Loss: {totalLoss}");

                if (totalLoss < tolerance)
                {
                    Debug.Log("Convergence achieved.");
                    break;
                }
            }

            Debug.Log("Training completed.");
        }

        private double ComputeScore(double[] input)
        {
            double score = 0;
            for (int i = 0; i < input.Length; i++)
            {
                score += weights[i] * input[i];
            }

            return score + (useBias ? bias : 0);
        }

        private double ComputeError(double predicted, bool actual, LossFunctionType lossType)
        {
            switch (lossType)
            {
                case LossFunctionType.LogisticLoss:
                    double target = actual ? 1.0 : 0.0;
                    return predicted - target;

                default:
                    throw new NotImplementedException("Loss function not implemented.");
            }
        }

        public void PrintWeightsAndBias()
        {
            Debug.Log("Current weights and bias:");
            for (int i = 0; i < weights.Length; i++)
            {
                Debug.Log($"Weight[{i}]: {weights[i]}");
            }
            Debug.Log($"Bias: {bias}");
        }

        public void Reset()
        {
            Init();
        }
    }
}
