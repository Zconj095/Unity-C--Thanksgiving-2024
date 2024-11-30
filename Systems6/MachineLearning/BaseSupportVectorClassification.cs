using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    // Define the interface for the support vector machine
    public interface ISupportVectorMachine<TInput>
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
    }

    public abstract class BaseSupportVectorClassification<TModel, TKernel, TInput>
        where TKernel : IKernel<TInput>
        where TModel : ISupportVectorMachine<TInput>
    {
        private bool useKernelEstimation = false;
        private bool useComplexityHeuristic = true;
        private bool useClassLabelProportion;
        private bool hasKernelBeenSet;

        private double complexity = 1;
        private double positiveWeight = 1;
        private double negativeWeight = 1;

        private double Cpositive;
        private double Cnegative;

        private TKernel kernel;
        private TModel model;

        /// <summary>
        /// Training inputs.
        /// </summary>
        protected TInput[] Inputs { get; set; }

        /// <summary>
        /// Training outputs (class labels).
        /// </summary>
        protected int[] Outputs { get; set; }

        /// <summary>
        /// Cost values for each input vector.
        /// </summary>
        protected double[] C { get; set; }

        /// <summary>
        /// Complexity parameter C.
        /// </summary>
        public double Complexity
        {
            get => complexity;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                complexity = value;
                useComplexityHeuristic = false;
            }
        }

        /// <summary>
        /// Positive class weight.
        /// </summary>
        public double PositiveWeight
        {
            get => positiveWeight;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                positiveWeight = value;
            }
        }

        /// <summary>
        /// Negative class weight.
        /// </summary>
        public double NegativeWeight
        {
            get => negativeWeight;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                negativeWeight = value;
            }
        }

        /// <summary>
        /// Kernel function used for learning.
        /// </summary>
        public TKernel Kernel
        {
            get => kernel;
            set
            {
                kernel = value;
                useKernelEstimation = false;
                hasKernelBeenSet = true;
            }
        }

        /// <summary>
        /// The SVM model being learned.
        /// </summary>
        public TModel Model
        {
            get => model;
            set => model = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Learns a model from the provided input and output data.
        /// </summary>
        public TModel Learn(TInput[] x, bool[] y, double[] weights = null)
        {
            Debug.Log("Starting SVM training...");
            ValidateInputs(x, y);

            if (kernel == null)
            {
                kernel = CreateDefaultKernel();
                Debug.Log("Default kernel created.");
            }

            if (Model == null)
            {
                int numInputs = GetNumberOfInputs(x);
                Model = Create(numInputs, kernel);
                Debug.Log($"Created SVM model with {numInputs} inputs.");
            }

            Inputs = x;
            Outputs = ConvertLabelsToBinary(y);

            InitializeComplexityWeights(y, weights);

            Debug.Log("Running internal learning algorithm...");
            InnerRun();

            Debug.Log("Training complete.");
            return Model;
        }

        /// <summary>
        /// Runs the main learning algorithm.
        /// </summary>
        protected abstract void InnerRun();

        /// <summary>
        /// Creates a new instance of the SVM model.
        /// </summary>
        protected abstract TModel Create(int inputs, TKernel kernel);

        /// <summary>
        /// Creates a default kernel if none is provided.
        /// </summary>
        protected virtual TKernel CreateDefaultKernel()
        {
            throw new InvalidOperationException("A kernel must be provided or CreateDefaultKernel must be overridden.");
        }

        /// <summary>
        /// Gets the number of inputs in the dataset.
        /// </summary>
        private int GetNumberOfInputs(TInput[] x)
        {
            if (x.Length == 0)
                throw new ArgumentException("The input dataset is empty.");
            return x[0] is Array array ? array.Length : throw new InvalidOperationException("Unable to determine input dimensions.");
        }

        /// <summary>
        /// Converts boolean class labels to binary integers.
        /// </summary>
        private int[] ConvertLabelsToBinary(bool[] y)
        {
            int[] binaryLabels = new int[y.Length];
            for (int i = 0; i < y.Length; i++)
                binaryLabels[i] = y[i] ? 1 : -1;
            return binaryLabels;
        }

        /// <summary>
        /// Initializes the complexity weights for training.
        /// </summary>
        private void InitializeComplexityWeights(bool[] y, double[] weights)
        {
            int positives = CountTrue(y);
            int negatives = y.Length - positives;

            if (positives == 0 || negatives == 0)
                throw new InvalidOperationException("Data must contain both positive and negative examples.");

            if (useComplexityHeuristic)
                complexity = EstimateComplexity();

            if (useClassLabelProportion)
                AdjustClassWeights(positives, negatives);

            Cpositive = complexity * positiveWeight;
            Cnegative = complexity * negativeWeight;

            C = new double[y.Length];
            for (int i = 0; i < y.Length; i++)
                C[i] = y[i] ? Cpositive : Cnegative;

            if (weights != null)
            {
                for (int i = 0; i < C.Length; i++)
                    C[i] *= weights[i];
            }
        }

        /// <summary>
        /// Counts the number of true values in a boolean array.
        /// </summary>
        private int CountTrue(bool[] array)
        {
            int count = 0;
            foreach (bool value in array)
                if (value) count++;
            return count;
        }

        /// <summary>
        /// Adjusts class weights based on the ratio of positive to negative examples.
        /// </summary>
        private void AdjustClassWeights(int positives, int negatives)
        {
            double ratio = positives / (double)negatives;
            if (ratio > 1)
            {
                positiveWeight = 1;
                negativeWeight = 1 / ratio;
            }
            else
            {
                negativeWeight = 1;
                positiveWeight = ratio;
            }
        }

        /// <summary>
        /// Estimates the complexity parameter.
        /// </summary>
        private double EstimateComplexity()
        {
            Debug.Log("Estimating complexity parameter...");
            return 1.0; // Default value
        }

        /// <summary>
        /// Validates input data.
        /// </summary>
        private void ValidateInputs(TInput[] x, bool[] y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Inputs and outputs cannot be null.");
            if (x.Length != y.Length)
                throw new ArgumentException("Input and output arrays must have the same length.");
        }
    }
}
