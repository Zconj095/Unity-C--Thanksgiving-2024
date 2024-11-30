using UnityEngine;
using System;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    public class ProbabilisticDualCoordinateDescent : BaseProbabilisticDualCoordinateDescent<DUALSMV<DUALCOORDINATELINEAR, double[]>, DUALCOORDINATELINEAR, double[]>
    {
        public ProbabilisticDualCoordinateDescent() { }

        protected override DUALSMV<DUALCOORDINATELINEAR, double[]> Create(int inputs, DUALCOORDINATELINEAR kernel)
        {
            return new DUALSMV<DUALCOORDINATELINEAR, double[]>(inputs, kernel);
        }
    }

    public abstract class BaseProbabilisticDualCoordinateDescent<TModel, TKernel, TInput>
        where TModel : DUALSMV<TKernel, TInput>
        where TKernel : IDUALLINEAR<TInput>
        where TInput : class
    {
        private TInput weights;
        private int biasIndex;
        private double epsilon = 0.1;
        private int maxIterations = 1000;

        public double Tolerance
        {
            get => epsilon;
            set => epsilon = value;
        }

        public int MaximumIterations
        {
            get => maxIterations;
            set => maxIterations = value;
        }

        public TModel Model { get; private set; }
        public TKernel Kernel { get; set; }
        public TInput[] Inputs { get; set; }
        public int[] Outputs { get; set; }
        public double[] C { get; set; }

        protected abstract TModel Create(int inputs, TKernel kernel);

        /// <summary>
        /// Runs the learning algorithm.
        /// </summary>
        protected void RunAlgorithm()
        {
            Debug.Log("Starting Dual Coordinate Descent Optimization...");
            int iterations = 0;
            int samples = Inputs.Length;
            int parameters = Kernel.GetLength(Inputs[0]) + 1;

            weights = CreateEmptyTInput(parameters + 1);
            biasIndex = parameters;

            double[] xTx = new double[samples];
            double[] alpha = new double[2 * samples];
            double[] upperBounds = C;

            InitializeAlphaAndWeights(alpha, xTx, samples);

            while (iterations < maxIterations)
            {
                ShuffleIndices(samples);
                PerformOptimizationStep(alpha, xTx, samples);
                iterations++;

                if (ConvergenceReached())
                {
                    Debug.Log($"Convergence achieved after {iterations} iterations.");
                    break;
                }
            }

            FinalizeModel();
        }

        private void InitializeAlphaAndWeights(double[] alpha, double[] xTx, int samples)
        {
            for (int i = 0; i < samples; i++)
            {
                alpha[2 * i] = Math.Min(0.001 * C[i], 1e-8);
                alpha[2 * i + 1] = C[i] - alpha[2 * i];

                TInput xi = Inputs[i];
                xTx[i] = 1 + Kernel.Function(xi, xi);
                UpdateWeights(alpha, i);
            }
        }

        private void PerformOptimizationStep(double[] alpha, double[] xTx, int samples)
        {
            for (int i = 0; i < samples; i++)
            {
                int j = UnityEngine.Random.Range(i, samples);
                SwapIndices(i, j);

                int index = i;
                TInput input = Inputs[index];
                OptimizeDualProblem(alpha, xTx, input, index);
            }
        }

        private void OptimizeDualProblem(double[] alpha, double[] xTx, TInput input, int index)
        {
            double upperBound = C[index];
            double xiSquare = xTx[index];
            double ywTx = Outputs[index] * Kernel.Function(input, weights);

            double gradient = ywTx * alpha[2 * index] + Math.Log(alpha[2 * index] / (upperBound - alpha[2 * index]));
            double step = -gradient / (xiSquare + upperBound);

            UpdateAlphaAndWeights(alpha, index, step, upperBound);
        }

        private void UpdateAlphaAndWeights(double[] alpha, int index, double step, double upperBound)
        {
            double newAlpha = Math.Max(0, Math.Min(upperBound, alpha[2 * index] + step));
            double delta = newAlpha - alpha[2 * index];

            alpha[2 * index] = newAlpha;
            UpdateWeights(alpha, index, delta);
        }

        private void UpdateWeights(double[] alpha, int index, double delta = 0)
        {
            TInput xi = Inputs[index];
            Kernel.Product(delta, xi, weights);
        }

        private bool ConvergenceReached()
        {
            return Kernel.Function(weights, weights) < epsilon;
        }

        private void FinalizeModel()
        {
            Model = Create(Kernel.GetLength(weights), Kernel);
            Model.Weights = Kernel.ExtractWeights(weights);
            Model.Threshold = Kernel.GetBias(weights);
            Debug.Log("Optimization completed. Final model is ready.");
        }

        private void SwapIndices(int i, int j)
        {
            var temp = Inputs[i];
            Inputs[i] = Inputs[j];
            Inputs[j] = temp;
        }

        private void ShuffleIndices(int samples)
        {
            for (int i = samples - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                SwapIndices(i, j);
            }
        }

        private TInput CreateEmptyTInput(int size)
        {
            return (TInput)Activator.CreateInstance(typeof(TInput), size);
        }
    }

    public class DUALSMV<TKernel, TInput>
        where TKernel : DUALIKERNAL<TInput>
    {
        public TKernel Kernel { get; set; }
        public double[] Weights { get; set; }
        public double Threshold { get; set; }

        public DUALSMV(int inputs, TKernel kernel)
        {
            Kernel = kernel;
            Weights = new double[inputs];
        }
    }

    public interface DUALIKERNAL<TInput>
    {
        double Function(TInput input1, TInput input2);
        double[] ExtractWeights(TInput weights);
        double GetBias(TInput weights);
    }

    public interface IDUALLINEAR<TInput> : DUALIKERNAL<TInput>
    {
        int GetLength(TInput input);
        void Product(double scalar, TInput input, TInput output);
    }

    public struct DUALCOORDINATELINEAR : IDUALLINEAR<double[]>
    {
        public double Function(double[] input1, double[] input2)
        {
            double sum = 0;
            for (int i = 0; i < input1.Length; i++)
                sum += input1[i] * input2[i];
            return sum;
        }

        public int GetLength(double[] input)
        {
            return input.Length;
        }

        public void Product(double scalar, double[] input, double[] output)
        {
            for (int i = 0; i < input.Length; i++)
                output[i] += scalar * input[i];
        }

        public double[] ExtractWeights(double[] weights)
        {
            return weights.Take(weights.Length - 1).ToArray();
        }

        public double GetBias(double[] weights)
        {
            return weights.Last();
        }
    }
}
