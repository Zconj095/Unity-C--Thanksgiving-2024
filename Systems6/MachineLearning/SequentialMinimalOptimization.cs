using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace EdgeLoreMachineLearning
{
    public enum SelectionStrategy
    {
        Sequential,
        WorstPair,
        SecondOrder
    }

    public class SequentialMinimalOptimization : MonoBehaviour
    {
        // Optimization parameters
        private double tolerance = 1e-2;
        private double epsilon = 1e-6;
        private bool shrinking;

        // Support Vector Machine parameters
        private double[] alpha;
        private HashSet<int> activeExamples;
        private HashSet<int> nonBoundExamples;
        private HashSet<int> atBoundsExamples;
        private int i_lower;
        private int i_upper;
        private double b_upper;
        private double b_lower;
        private double[] errors;

        private SelectionStrategy strategy = SelectionStrategy.WorstPair;
        private int maxChecks = 100;

        // Kernel function simulation
        private Func<double[], double[], double> kernelFunction;

        // Reflection Fields
        private FieldInfo[] fields;

        void Start()
        {
            Init();
        }

        private void Init()
        {
            activeExamples = new HashSet<int>();
            nonBoundExamples = new HashSet<int>();
            atBoundsExamples = new HashSet<int>();
            kernelFunction = (x, y) => DotProduct(x, y);

            // Using Unity Reflection to inspect runtime fields
            fields = typeof(SequentialMinimalOptimization).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Log("Fields available via Reflection:");
            foreach (var field in fields)
            {
                Debug.Log($"Field Name: {field.Name}, Field Type: {field.FieldType}");
            }
        }

        public double Epsilon
        {
            get { return epsilon; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                epsilon = value;
            }
        }

        public double Tolerance
        {
            get { return tolerance; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                tolerance = value;
            }
        }

        public SelectionStrategy Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        public void Learn(double[][] inputs, int[] outputs, double[] weights = null)
        {
            if (inputs == null || outputs == null) throw new ArgumentNullException("Inputs or outputs cannot be null.");

            // Example using Unity reflection to modify runtime values
            Debug.Log("Modifying alpha array size via reflection...");
            var alphaField = Array.Find(fields, f => f.Name == "alpha");
            if (alphaField != null)
            {
                alpha = new double[inputs.Length];
                alphaField.SetValue(this, alpha);
            }

            Debug.Log($"Learning initiated with {inputs.Length} samples.");
            RunLearning(inputs, outputs);
        }

        private void RunLearning(double[][] inputs, int[] outputs)
        {
            int samples = inputs.Length;
            alpha = new double[samples];
            errors = new double[samples];
            Debug.Log($"Learning initiated for {samples} samples.");

            for (int i = 0; i < samples; i++)
            {
                Debug.Log($"Processing sample {i + 1}/{samples}");
                ComputeExample(i, inputs, outputs);
            }

            Debug.Log("Learning completed.");
        }

        private void ComputeExample(int index, double[][] inputs, int[] outputs)
        {
            Debug.Log($"Computing SVM optimization for sample index {index}.");

            double[] sample = inputs[index];
            double label = outputs[index];

            // Simulating a kernel function evaluation
            double sum = 0;
            foreach (var otherIndex in activeExamples)
            {
                sum += alpha[otherIndex] * outputs[otherIndex] * kernelFunction(inputs[otherIndex], sample);
            }

            double error = sum - label;
            errors[index] = error;

            Debug.Log($"Error for sample index {index}: {error}");
        }

        private double DotProduct(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }
            return sum;
        }

        // Add reflection-based utilities for debugging
        public void PrintFieldValues()
        {
            Debug.Log("Field values at runtime:");
            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                Debug.Log($"Field: {field.Name}, Value: {value}");
            }
        }
    }
}
