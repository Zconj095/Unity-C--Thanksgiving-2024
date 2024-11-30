using System;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning.VectorMachines
{
    /// <summary>
    ///   Unity-Compatible Kernel Support Vector Machine.
    /// </summary>
    public class KernelSupportVectorMachine : ISupportVectorMachine<double[]>
    {
        public IKernel<double[]> Kernel { get; private set; }
        public int NumberOfInputs { get; private set; }
        public double[][] SupportVectors { get; private set; }
        public double[] Weights { get; private set; }
        public double Threshold { get; private set; }
        public bool IsProbabilistic { get; private set; }

        /// <summary>
        ///   Creates a new instance of KernelSupportVectorMachine with the specified kernel and inputs.
        /// </summary>
        /// <param name="kernel">The kernel function to use.</param>
        /// <param name="inputs">The number of inputs.</param>
        public KernelSupportVectorMachine(IKernel<double[]> kernel, int inputs)
        {
            Kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            NumberOfInputs = inputs > 0 ? inputs : throw new ArgumentException("Number of inputs must be positive.", nameof(inputs));
        }

        /// <summary>
        ///   Sets the support vectors for the machine.
        /// </summary>
        /// <param name="supportVectors">The support vectors.</param>
        /// <param name="weights">The weights for the support vectors.</param>
        /// <param name="threshold">The decision threshold.</param>
        public void SetSupportVectors(double[][] supportVectors, double[] weights, double threshold)
        {
            if (supportVectors == null || weights == null)
                throw new ArgumentNullException("Support vectors and weights cannot be null.");

            if (supportVectors.Length != weights.Length)
                throw new ArgumentException("The number of support vectors must match the number of weights.");

            SupportVectors = supportVectors;
            Weights = weights;
            Threshold = threshold;
        }

        /// <summary>
        ///   Computes the decision for the given input.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>The computed decision value.</returns>
        public double Compute(double[] input)
        {
            if (input.Length != NumberOfInputs)
                throw new ArgumentException("Input size must match the number of inputs.", nameof(input));

            double decision = Threshold;

            for (int i = 0; i < SupportVectors.Length; i++)
            {
                decision += Weights[i] * Kernel.Function(SupportVectors[i], input);
            }

            return decision;
        }

        /// <summary>
        ///   Clones the current instance.
        /// </summary>
        /// <returns>A new instance with the same parameters.</returns>
        public KernelSupportVectorMachine Clone()
        {
            var clone = new KernelSupportVectorMachine(Kernel, NumberOfInputs)
            {
                SupportVectors = (double[][])SupportVectors.Clone(),
                Weights = (double[])Weights.Clone(),
                Threshold = Threshold,
                IsProbabilistic = IsProbabilistic
            };
            return clone;
        }

        /// <summary>
        ///   Debugging helper to print the machine's parameters.
        /// </summary>
        public void DebugPrint()
        {
            Debug.Log($"KernelSupportVectorMachine Debug Info:\n" +
                      $"Inputs: {NumberOfInputs}\n" +
                      $"Threshold: {Threshold}\n" +
                      $"IsProbabilistic: {IsProbabilistic}\n" +
                      $"Support Vectors: {SupportVectors?.Length ?? 0}\n" +
                      $"Weights: {Weights?.Length ?? 0}");
        }
    }

    /// <summary>
    ///   Interface for Kernel Support Vector Machines.
    /// </summary>
    public interface ISupportVectorMachine<TInput>
    {
        double Compute(TInput input);
        KernelSupportVectorMachine Clone();
    }

    /// <summary>
    ///   Interface for Kernel Functions.
    /// </summary>
    public interface IKernel<TInput>
    {
        double Function(TInput x, TInput y);
    }
}
