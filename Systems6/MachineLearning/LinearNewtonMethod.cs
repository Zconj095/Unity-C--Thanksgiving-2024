using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class LinearNewtonMethod<TKernel, TInput>
        where TKernel : struct, ILinear<TInput>
        where TInput : IList<float>
    {
        private TKernel kernel;
        private double[] weights;
        private double bias;
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

        public void Train(TInput[] inputs, int[] outputs, double[] regularization)
        {
            int samples = inputs.Length;
            int dimensions = inputs[0].Count + 1; // Including bias

            weights = new double[dimensions];
            double[] z = new double[samples];
            double[] gradient = new double[dimensions];
            double[] hessian = new double[dimensions];

            for (int iter = 0; iter < maxIterations; iter++)
            {
                // Step 1: Compute the decision values z
                for (int i = 0; i < samples; i++)
                {
                    z[i] = ComputeDecision(inputs[i]); // Scalar decision value for each input
                }

                // Step 2: Compute the objective function
                double objectiveValue = ComputeObjective(z, outputs, regularization);

                // Step 3: Compute gradient and Hessian
                ComputeGradient(inputs, outputs, z, regularization, gradient);
                ComputeHessian(inputs, outputs, z, regularization, hessian);

                // Step 4: Update weights using Newton-Raphson
                UpdateWeights(gradient, hessian);

                Debug.Log($"Iteration {iter + 1}, Objective: {objectiveValue}");

                if (objectiveValue < tolerance) // Check if the objective is below the tolerance
                {
                    Debug.Log("Converged!");
                    break;
                }
            }
        }

        private double ComputeDecision(TInput input)
        {
            double decision = bias;
            for (int i = 0; i < input.Count; i++)
            {
                decision += weights[i] * input[i];
            }
            return decision;
        }

        private double ComputeObjective(double[] z, int[] outputs, double[] regularization)
        {
            double objective = 0.0;
            for (int i = 0; i < outputs.Length; i++)
            {
                double loss = Math.Max(0, 1 - outputs[i] * z[i]); // Correct comparison
                objective += 0.5 * weights[i] * weights[i] + regularization[i] * loss * loss;
            }
            return objective;
        }

        private void ComputeGradient(TInput[] inputs, int[] outputs, double[] z, double[] regularization, double[] gradient)
        {
            Array.Clear(gradient, 0, gradient.Length);

            for (int i = 0; i < inputs.Length; i++)
            {
                if (z[i] < 1) // Valid scalar comparison
                {
                    for (int j = 0; j < inputs[i].Count; j++)
                    {
                        gradient[j] += -outputs[i] * inputs[i][j] * regularization[i] * (1 - z[i]);
                    }
                }
            }

            for (int j = 0; j < weights.Length; j++)
            {
                gradient[j] += weights[j];
            }
        }

        private void ComputeHessian(TInput[] inputs, int[] outputs, double[] z, double[] regularization, double[] hessian)
        {
            Array.Clear(hessian, 0, hessian.Length);

            for (int i = 0; i < inputs.Length; i++)
            {
                if (z[i] < 1) // Valid scalar comparison
                {
                    for (int j = 0; j < inputs[i].Count; j++)
                    {
                        hessian[j] += inputs[i][j] * inputs[i][j] * regularization[i];
                    }
                }
            }

            for (int j = 0; j < weights.Length; j++)
            {
                hessian[j] += 1;
            }
        }

        private void UpdateWeights(double[] gradient, double[] hessian)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] -= gradient[i] / hessian[i];
            }
        }

        public int Predict(TInput input)
        {
            double decision = ComputeDecision(input);
            return decision > 0 ? 1 : -1;
        }
    }

    public interface ILinear<TInput>
    {
        double Function(double[] weights, TInput input);
    }
}
