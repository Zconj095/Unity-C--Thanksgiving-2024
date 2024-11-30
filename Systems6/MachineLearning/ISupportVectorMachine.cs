using System;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Common interface for binary support vector machines.
    /// </summary>
    /// 
    /// <typeparam name="TInput">The type of the input data handled by the machine.</typeparam>
    public interface IISVMSupportVectorMachine<TInput>
    {
        /// <summary>
        ///   Gets or sets the collection of weights used by this machine.
        /// </summary>
        double[] Weights { get; set; }

        /// <summary>
        ///   Gets or sets the collection of support vectors used by this machine.
        /// </summary>
        TInput[] SupportVectors { get; set; }

        /// <summary>
        ///   Gets or sets the threshold (bias) term for this machine.
        /// </summary>
        double Threshold { get; set; }

        /// <summary>
        ///   Gets or sets the kernel used by this machine.
        /// </summary>
        ISVMIKernel<TInput> Kernel { get; set; }

        /// <summary>
        ///   Gets whether this machine has been calibrated to
        ///   produce probabilistic outputs.
        /// </summary>
        bool IsProbabilistic { get; set; }

        /// <summary>
        ///   If this machine has a linear kernel, compresses all
        ///   support vectors into a single parameter vector.
        /// </summary>
        void Compress();
    }

    /// <summary>
    ///   Common interface for kernel functions.
    /// </summary>
    /// 
    /// <typeparam name="TInput">The type of the input data handled by the kernel.</typeparam>
    public interface ISVMIKernel<TInput>
    {
        /// <summary>
        ///   Computes the kernel function for two input vectors.
        /// </summary>
        /// 
        /// <param name="a">The first input vector.</param>
        /// <param name="b">The second input vector.</param>
        /// <returns>The result of the kernel function.</returns>
        double Compute(TInput a, TInput b);
    }

    /// <summary>
    ///   Implementation of a linear kernel.
    /// </summary>
    public class ISVMLinearKernel : ISVMIKernel<double[]>
    {
        /// <summary>
        ///   Computes the dot product of two vectors.
        /// </summary>
        public double Compute(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must have the same length.");

            double result = 0;
            for (int i = 0; i < a.Length; i++)
                result += a[i] * b[i];

            return result;
        }
    }

}
