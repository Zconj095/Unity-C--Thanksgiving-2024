using System;
using UnityEngine;

namespace EdgeLoreMachineLearning.VectorMachines
{
    // Interface for kernel function
    public interface SVMIKernel<TInput>
    {
        double Function(TInput x, TInput y);
    }

    // Interface for linear kernel, extending SVMIKernel
    public interface ILinearKernel<TInput> : SVMIKernel<TInput>
    {
        double[] Compress(double[] weights, TInput[] supportVectors, out double bias);
    }

    public class SupportVectorMachine<TKernel, TInput> : ICloneable
        where TKernel : SVMIKernel<TInput>
    {
        public TKernel Kernel { get; set; }
        public TInput[] SupportVectors { get; private set; }
        public double[] Weights { get; private set; }
        public double Threshold { get; set; }
        public bool IsProbabilistic { get; set; }
        public int NumberOfInputs { get; private set; }
        public int NumberOfClasses { get; private set; } = 2;

        /// <summary>
        /// Initializes a new instance of the SupportVectorMachine class.
        /// </summary>
        public SupportVectorMachine(int inputs, TKernel kernel)
        {
            NumberOfInputs = inputs;
            Kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        /// <summary>
        /// Sets the support vectors and weights for this machine.
        /// </summary>
        public void SetSupportVectors(TInput[] supportVectors, double[] weights, double threshold)
        {
            if (supportVectors == null || weights == null)
                throw new ArgumentNullException("SupportVectors and Weights cannot be null.");

            if (supportVectors.Length != weights.Length)
                throw new ArgumentException("SupportVectors and Weights must have the same length.");

            SupportVectors = supportVectors;
            Weights = weights;
            Threshold = threshold;
        }

        /// <summary>
        /// Computes a decision for the given input vector.
        /// </summary>
        public bool Decide(TInput input)
        {
            double sum = Threshold;
            for (int i = 0; i < SupportVectors.Length; i++)
            {
                sum += Weights[i] * Kernel.Function(SupportVectors[i], input);
            }
            return sum > 0;
        }

        /// <summary>
        /// Computes scores for a batch of inputs.
        /// </summary>
        public double[] ComputeScores(TInput[] inputs)
        {
            double[] scores = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                double sum = Threshold;
                for (int j = 0; j < SupportVectors.Length; j++)
                {
                    sum += Weights[j] * Kernel.Function(SupportVectors[j], inputs[i]);
                }
                scores[i] = sum;
            }
            return scores;
        }

        /// <summary>
        /// Compresses the machine for linear kernels.
        /// </summary>
        public void Compress()
        {
            if (!(Kernel is ILinearKernel<TInput> linearKernel))
                throw new InvalidOperationException("Compression is only valid for linear kernels.");

            double bias;
            var compressedWeights = linearKernel.Compress(Weights, SupportVectors, out bias);

            SupportVectors = new TInput[1]; // Example placeholder, update with appropriate logic if needed
            Weights = compressedWeights;
            Threshold += bias;
        }

        /// <summary>
        /// Converts the machine into an array of weights for linear kernels.
        /// </summary>
        public double[] ToWeights()
        {
            if (!(Kernel is ILinearKernel<TInput> linearKernel))
                throw new InvalidOperationException("Conversion is only valid for linear kernels.");

            double bias;
            double[] weights = linearKernel.Compress(Weights, SupportVectors, out bias);

            var result = new double[weights.Length + 1];
            result[0] = Threshold + bias;
            Array.Copy(weights, 0, result, 1, weights.Length);
            return result;
        }

        /// <summary>
        /// Clones this instance of the machine.
        /// </summary>
        public object Clone()
        {
            var clone = new SupportVectorMachine<TKernel, TInput>(NumberOfInputs, Kernel)
            {
                SupportVectors = (TInput[])SupportVectors.Clone(),
                Weights = (double[])Weights.Clone(),
                Threshold = Threshold,
                IsProbabilistic = IsProbabilistic
            };
            return clone;
        }

        /// <summary>
        /// Debug utility to print details about the machine.
        /// </summary>
        public void DebugPrint()
        {
            Debug.Log($"Support Vector Machine:\n" +
                      $"Number of Inputs: {NumberOfInputs}\n" +
                      $"Threshold: {Threshold}\n" +
                      $"Support Vector Count: {SupportVectors?.Length ?? 0}\n" +
                      $"Weights Count: {Weights?.Length ?? 0}\n" +
                      $"Is Probabilistic: {IsProbabilistic}");
        }
    }
}
