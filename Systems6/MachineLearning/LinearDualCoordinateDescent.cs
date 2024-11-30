using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class LinearDualCoordinateDescent<TKernel, TInput>
        where TKernel : struct, ILinear<TInput>
        where TInput : IList<float>
    {
        private TKernel kernel;
        private double[] alpha;
        private double[] weights;
        private double bias;
        private double tolerance = 0.1;
        private int maxIterations = 1000;
        private Loss loss = Loss.L2;

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

        public Loss LossFunction
        {
            get => loss;
            set => loss = value;
        }

        public void Train(TInput[] inputs, int[] outputs, double[] regularization)
        {
            int samples = inputs.Length;
            int dimensions = inputs[0].Count;

            // Initialize weights and multipliers
            weights = new double[dimensions];
            alpha = new double[samples];
            bias = 0;

            double[] diagonal = new double[samples];
            double[] QD = new double[samples];

            for (int i = 0; i < samples; i++)
            {
                diagonal[i] = (loss == Loss.L2) ? 0.5 / regularization[i] : 0;
                QD[i] = ComputeKernel(inputs[i], inputs[i]) + diagonal[i];
            }

            for (int iter = 0; iter < maxIterations; iter++)
            {
                double PGmaxOld = double.PositiveInfinity;
                double PGminOld = double.NegativeInfinity;

                for (int i = 0; i < samples; i++)
                {
                    int yi = outputs[i];
                    double G = ComputeDecisionFunction(inputs[i]) - 1.0 * yi;

                    G += alpha[i] * diagonal[i];
                    double PG = 0;

                    if (alpha[i] == 0)
                    {
                        if (G > PGmaxOld) continue;
                        else if (G < 0) PG = G;
                    }
                    else if (alpha[i] == regularization[i])
                    {
                        if (G < PGminOld) continue;
                        else if (G > 0) PG = G;
                    }
                    else
                    {
                        PG = G;
                    }

                    if (Math.Abs(PG) > 1e-12)
                    {
                        double alphaOld = alpha[i];
                        alpha[i] = Math.Min(Math.Max(alpha[i] - G / QD[i], 0.0), regularization[i]);
                        double d = (alpha[i] - alphaOld) * yi;

                        // Update weights and bias
                        UpdateWeights(inputs[i], d);
                        bias += d;
                    }
                }

                if (iter % 10 == 0)
                {
                    Debug.Log($"Iteration {iter}: Tolerance = {PGmaxOld - PGminOld}");
                }

                if (PGmaxOld - PGminOld <= tolerance) break;
            }

            Debug.Log("Training completed.");
        }

        private void UpdateWeights(TInput input, double d)
        {
            for (int i = 0; i < input.Count; i++)
            {
                weights[i] += d * input[i];
            }
        }

        private double ComputeKernel(TInput x, TInput y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += x[i] * y[i];
            }
            return sum;
        }

        private double ComputeDecisionFunction(TInput input)
        {
            double decision = bias;
            for (int i = 0; i < weights.Length; i++)
            {
                decision += weights[i] * input[i];
            }
            return decision;
        }

        public int Predict(TInput input)
        {
            double decision = ComputeDecisionFunction(input);
            return decision > 0 ? 1 : -1;
        }
    }

    public enum Loss
    {
        L1,
        L2
    }
}
