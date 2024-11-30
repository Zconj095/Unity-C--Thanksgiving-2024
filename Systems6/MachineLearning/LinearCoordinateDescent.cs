using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class LinearCoordinateDescent<TKernel>
        where TKernel : struct
    {
        private TKernel kernel;
        private double[] weights;
        private double threshold;
        private double tolerance = 0.1;
        private int maxIterations = 1000;

        public double Tolerance
        {
            get => tolerance;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Tolerance must be greater than zero.");
                tolerance = value;
            }
        }

        public int MaxIterations
        {
            get => maxIterations;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "MaxIterations must be greater than zero.");
                maxIterations = value;
            }
        }

        public void Train(double[][] inputs, int[] outputs, double[] regularization)
        {
            int samples = inputs.Length;
            int features = inputs[0].Length;

            // Initialize weights and thresholds
            weights = new double[features];
            threshold = 0;

            double[] alpha = new double[samples];
            double[] gradients = new double[features];
            double[] biases = new double[samples];

            Array.Fill(alpha, 0);
            Array.Fill(weights, 0);
            Array.Fill(biases, 1);

            for (int iter = 0; iter < maxIterations; iter++)
            {
                double maxGradient = 0;

                // Iterate through each feature
                for (int j = 0; j < features; j++)
                {
                    double gradient = ComputeGradient(inputs, outputs, regularization, weights, biases, j);
                    maxGradient = Math.Max(maxGradient, Math.Abs(gradient));

                    // Update the weight using gradient descent
                    double step = gradient / (ComputeHessian(inputs, regularization, j) + 1e-12);
                    weights[j] -= step;

                    // Update biases
                    for (int i = 0; i < samples; i++)
                    {
                        biases[i] -= step * inputs[i][j] * outputs[i];
                    }
                }

                Debug.Log($"Iteration {iter}: Max Gradient = {maxGradient}");

                if (maxGradient < tolerance)
                {
                    Debug.Log("Converged!");
                    break;
                }
            }

            threshold = ComputeThreshold(inputs, outputs);
            Debug.Log("Training completed.");
        }

        private double ComputeGradient(double[][] inputs, int[] outputs, double[] regularization, double[] weights, double[] biases, int featureIndex)
        {
            double gradient = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                double margin = biases[i] - DotProduct(weights, inputs[i]) * outputs[i];
                if (margin > 0)
                {
                    gradient -= regularization[i] * inputs[i][featureIndex] * outputs[i];
                }
            }

            return gradient + Math.Sign(weights[featureIndex]);
        }

        private double ComputeHessian(double[][] inputs, double[] regularization, int featureIndex)
        {
            double hessian = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                hessian += regularization[i] * inputs[i][featureIndex] * inputs[i][featureIndex];
            }

            return hessian;
        }

        private double ComputeThreshold(double[][] inputs, int[] outputs)
        {
            double sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += outputs[i] - DotProduct(weights, inputs[i]);
            }

            return sum / inputs.Length;
        }

        private double DotProduct(double[] vectorA, double[] vectorB)
        {
            double sum = 0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                sum += vectorA[i] * vectorB[i];
            }

            return sum;
        }

        public int Predict(double[] input)
        {
            double score = DotProduct(weights, input) + threshold;
            return score > 0 ? 1 : -1;
        }
    }
}
