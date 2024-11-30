using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
namespace EdgeLoreMachineLearning
{
    public static class SupportVectorLearningHelper
    {
        /// <summary>
        /// Creates a new instance of a support vector machine (SVM).
        /// </summary>
        public static TModel Create<TModel, TInput, TKernel>(int inputs, TKernel kernel)
            where TModel : class, ISupportVectorMachine2<TInput>
            where TKernel : IKernel2<TInput>
        {
            TModel result = null;
            Type type = typeof(TModel);

            Debug.Log($"Creating model of type: {type.Name}");

            // Check for supported types
            if (type == typeof(SupportVectorMachine2))
                result = new SupportVectorMachine2(inputs) as TModel;
            else if (type == typeof(GenericSupportVectorMachine<TKernel, TInput>))
                result = new GenericSupportVectorMachine<TKernel, TInput>(inputs, kernel) as TModel;

            if (result == null)
                throw new NotSupportedException("To implement your own SVM type, override the Create method in your learning algorithm.");

            Debug.Log($"Model of type {type.Name} successfully created.");
            return result;
        }

        /// <summary>
        /// Overload to create an SVM for specific kernel and input types.
        /// </summary>
        public static TModel Create<TModel, TKernel>(int inputs, TKernel kernel)
            where TModel : class, ISupportVectorMachine2<double[]>
            where TKernel : IKernel2<double[]>
        {
            return Create<TModel, double[], TKernel>(inputs, kernel);
        }

        /// <summary>
        /// Determines the number of inputs for the given dataset and kernel.
        /// </summary>
        public static int GetNumberOfInputs<TKernel, TInput>(TKernel kernel, TInput[] x)
        {
            if (x == null || x.Length == 0)
                throw new ArgumentException("Cannot determine the number of inputs because the dataset is empty.");

            if (kernel is ILinear2<TInput> linearKernel)
            {
                Debug.Log("Kernel supports linear operation. Retrieving input length.");
                return linearKernel.GetLength(x);
            }

            Debug.LogWarning("Kernel does not support linear operation. Falling back to array-based estimation.");
            return x[0] is IList list ? list.Count : throw new InvalidOperationException("Input type is not compatible.");
        }

        /// <summary>
        /// Validates the output of a support vector machine.
        /// </summary>
        public static void CheckOutput<TInput>(ISupportVectorMachine2<TInput> model)
        {
            Debug.Log("Checking SVM output consistency...");

            if (model.SupportVectors == null || model.Weights == null)
                throw new InvalidOperationException("Model outputs are null.");
            if (model.SupportVectors.Length != model.Weights.Length)
                throw new InvalidOperationException("Mismatch between support vectors and weights.");

            Debug.Log("SVM output is consistent.");
        }

        /// <summary>
        /// Estimates kernel parameters if supported by the kernel type.
        /// </summary>
        public static TKernel EstimateKernel<TKernel, TInput>(TKernel kernel, TInput[] x)
            where TKernel : IKernel2<TInput>
        {
            if (kernel is IEstimable<TInput> estimable)
            {
                Debug.Log("Estimating kernel parameters...");
                estimable.Estimate(x);
                return kernel;
            }

            throw new InvalidOperationException("Kernel type does not support estimation.");
        }

        /// <summary>
        /// Creates a kernel instance dynamically.
        /// </summary>
        public static TKernel CreateKernel<TKernel, TInput>(TInput[] x)
            where TKernel : IKernel2<TInput>
        {
            Debug.Log("Creating kernel instance dynamically.");

            // Ensure the kernel type has a default constructor
            if (!typeof(TKernel).GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any(c => c.GetParameters().Length == 0))
                throw new InvalidOperationException("Kernel type does not have a default constructor.");

            var kernel = Activator.CreateInstance<TKernel>();
            if (kernel is IEstimable<TInput> estimable)
            {
                Debug.Log("Kernel supports estimation. Estimating parameters.");
                estimable.Estimate(x);
            }

            return kernel;
        }
    }

    /// <summary>
    /// Example interface for a Support Vector Machine.
    /// </summary>
    public interface ISupportVectorMachine2<TInput>
    {
        TInput[] SupportVectors { get; set; }
        double[] Weights { get; set; }
    }

    /// <summary>
    /// Example kernel interface.
    /// </summary>
    public interface IKernel2<TInput>
    {
        double Compute(TInput a, TInput b);
    }

    /// <summary>
    /// Example estimable kernel interface.
    /// </summary>
    public interface IEstimable<TInput> : IKernel2<TInput>
    {
        void Estimate(TInput[] data);
    }

    /// <summary>
    /// Example linear kernel interface.
    /// </summary>
    public interface ILinear2<TInput> : IKernel2<TInput>
    {
        int GetLength(TInput[] data);
    }

    /// <summary>
    /// Example Support Vector Machine class.
    /// </summary>
    public class SupportVectorMachine2 : ISupportVectorMachine2<double[]>
    {
        public double[][] SupportVectors { get; set; }
        public double[] Weights { get; set; }

        public SupportVectorMachine2(int inputs)
        {
            SupportVectors = new double[inputs][];
            Weights = new double[inputs];
        }
    }

    /// <summary>
    /// Generic Support Vector Machine class.
    /// </summary>
    public class GenericSupportVectorMachine<TKernel, TInput> : ISupportVectorMachine2<TInput>
        where TKernel : IKernel2<TInput>
    {
        public TInput[] SupportVectors { get; set; }
        public double[] Weights { get; set; }
        public TKernel Kernel { get; private set; }

        public GenericSupportVectorMachine(int inputs, TKernel kernel)
        {
            SupportVectors = new TInput[inputs];
            Weights = new double[inputs];
            Kernel = kernel;

            // Initialize your SVM with the kernel and inputs
            Debug.Log($"Generic SVM created with {inputs} inputs and kernel of type {typeof(TKernel).Name}");
        }
    }
}
