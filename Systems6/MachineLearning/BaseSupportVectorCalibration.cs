using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    // Define the interface for the support vector machine
    public interface IBaseSystemSupportVectorMachine<TInput>
    {
        /// <summary>
        /// Predicts the class label for a given input.
        /// </summary>
        int Predict(TInput input);

        /// <summary>
        /// Gets or sets the support vectors for the SVM.
        /// </summary>
        TInput[] SupportVectors { get; set; }

        /// <summary>
        /// Gets or sets the weights for the support vectors.
        /// </summary>
        double[] Weights { get; set; }

        /// <summary>
        /// Gets or sets the threshold for classification.
        /// </summary>
        double Threshold { get; set; }

        /// <summary>
        /// Gets or sets the kernel used for the SVM.
        /// </summary>
        BaseIKernel<TInput> Kernel { get; set; }
    }

    // Define the interface for a kernel function
    public interface BaseIKernel<TInput>
    {
        double Compute(TInput x, TInput y);
    }

    // Optional interface for linear kernels
    public interface BaseILinear<TInput> : BaseIKernel<TInput>
    {
        // Define an optional method to get input length if required
        int GetInputLength(TInput[] inputs);
    }

    public abstract class BaseSupportVectorCalibration<TModel, TKernel, TInput>
        where TKernel : BaseIKernel<TInput> // Changed to enforce the correct constraint
        where TModel : IBaseSystemSupportVectorMachine<TInput>
    {
        private TModel model;
        private TKernel kernel;

        /// <summary>
        /// Input vectors for training.
        /// </summary>
        protected TInput[] Input { get; set; }

        /// <summary>
        /// Output labels for training.
        /// </summary>
        protected bool[] Output { get; set; }

        /// <summary>
        /// Initializes the calibration class with the given machine.
        /// </summary>
        protected BaseSupportVectorCalibration(TModel machine)
        {
            model = machine ?? throw new ArgumentNullException(nameof(machine));
            kernel = (TKernel)machine.Kernel ?? throw new InvalidOperationException("Kernel must be of type TKernel.");
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected BaseSupportVectorCalibration() { }

        /// <summary>
        /// Checks if the model is linear.
        /// </summary>
        public bool IsLinear => kernel is BaseILinear<TInput>;

        /// <summary>
        /// The machine being calibrated.
        /// </summary>
        public TModel Model
        {
            get => model;
            set => model = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// The kernel of the machine.
        /// </summary>
        protected TKernel Kernel => kernel;

        /// <summary>
        /// Abstract method to implement the learning algorithm.
        /// </summary>
        protected abstract void InnerRun();

        /// <summary>
        /// Trains the model to map inputs to outputs.
        /// </summary>
        public TModel Learn(TInput[] x, bool[] y, double[] weights = null)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Inputs and outputs cannot be null.");

            Debug.Log($"Learning started with {x.Length} samples.");

            if (model == null)
            {
                int numInputs = GetNumberOfInputs(x);
                model = Create(numInputs, Kernel);
                Debug.Log($"Created model with {numInputs} inputs.");
            }

            Input = x;
            Output = y;

            Debug.Log("Running the internal learning algorithm...");
            InnerRun();

            Debug.Log("Learning completed.");
            return model;
        }

        /// <summary>
        /// Creates a new machine model for learning.
        /// </summary>
        protected virtual TModel Create(int inputs, TKernel kernel)
        {
            Debug.Log("Creating a new model...");
            if (typeof(TModel) == typeof(BaseSystemSupportVectorMachine<TKernel, TInput>))
                return (TModel)(object)new BaseSystemSupportVectorMachine<TKernel, TInput>(inputs, kernel);

            throw new NotSupportedException("The provided model type is not supported. Extend this method to support new types.");
        }

        /// <summary>
        /// Gets the number of inputs in the dataset.
        /// </summary>
        private int GetNumberOfInputs(TInput[] inputs)
        {
            if (inputs.Length == 0)
                throw new ArgumentException("The input dataset is empty.");

            if (kernel is BaseILinear<TInput> linearKernel)
                return linearKernel.GetInputLength(inputs);

            if (inputs[0] is Array array)
                return array.Length;

            throw new InvalidOperationException("Unable to determine input dimensions.");
        }
    }

    // Example SVM implementation for testing
    public class BaseSystemSupportVectorMachine<TKernel, TInput> : IBaseSystemSupportVectorMachine<TInput>
        where TKernel : BaseIKernel<TInput>
    {
        public TInput[] SupportVectors { get; set; }
        public double[] Weights { get; set; }
        public double Threshold { get; set; }
        public BaseIKernel<TInput> Kernel { get; set; }

        public int Predict(TInput input)
        {
            // Example prediction logic
            return 0;
        }

        public BaseSystemSupportVectorMachine(int inputs, TKernel kernel)
        {
            Kernel = kernel;
            // Initialize other properties as needed
        }
    }
}
