using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Helper class for creating and validating Decision Trees in Unity.
    /// </summary>
    public static class DecisionTreeHelper
    {
        /// <summary>
        ///   Checks the validity of input arguments for training a decision tree.
        /// </summary>
        public static void CheckArgs(DecisionTree tree, int[][] inputs, int[] outputs, double[] weights = null)
        {
            ValidateArguments(tree, inputs, outputs, weights);

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (tree.Attributes[j].Nature != DecisionVariableKind.Discrete)
                        Debug.LogError("Tree expects real-valued inputs.");

                    double min = tree.Attributes[j].Range.Min;
                    double max = tree.Attributes[j].Range.Max;

                    if (inputs[i][j] < min || inputs[i][j] > max)
                    {
                        throw new ArgumentOutOfRangeException(
                            "inputs",
                            $"Input vector at index {i} contains an invalid entry ({inputs[i][j]}) at column {j}. " +
                            $"Expected value between [{min}, {max}]."
                        );
                    }
                }
            }
        }

        public static void CheckArgs(DecisionTree tree, double[][] inputs, int[] outputs, double[] weights = null)
        {
            ValidateArguments(tree, inputs, outputs, weights);

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (tree.Attributes[j].Nature != DecisionVariableKind.Discrete)
                        continue;

                    double min = tree.Attributes[j].Range.Min;
                    double max = tree.Attributes[j].Range.Max;

                    if (inputs[i][j] < min || inputs[i][j] > max)
                    {
                        throw new ArgumentOutOfRangeException(
                            "inputs",
                            $"Input vector at index {i} contains an invalid entry ({inputs[i][j]}) at column {j}. " +
                            $"Expected value between [{min}, {max}]."
                        );
                    }
                }
            }
        }

        private static void ValidateArguments(DecisionTree tree, Array[] inputs, int[] outputs, double[] weights = null)
        {
            if (inputs == null) throw new ArgumentNullException(nameof(inputs), "Inputs cannot be null.");
            if (outputs == null) throw new ArgumentNullException(nameof(outputs), "Outputs cannot be null.");
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("The number of input vectors and output labels must match.");
            if (inputs.Length == 0)
                throw new ArgumentException("Training requires at least one input vector.");
            if (weights != null && inputs.Length != weights.Length)
                throw new ArgumentException("The number of weights must match the number of inputs.");

            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] == null)
                    throw new ArgumentNullException($"Input vector at index {i} is null.");
                if (inputs[i].Length != tree.NumberOfInputs)
                    throw new ArgumentException(
                        $"Input vector at index {i} has {inputs[i].Length} dimensions, but the tree expects {tree.NumberOfInputs}."
                    );
            }

            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i] < 0 || outputs[i] >= tree.NumberOfOutputs)
                    throw new ArgumentOutOfRangeException(
                        nameof(outputs),
                        $"Output label at index {i} should be >= 0 and < {tree.NumberOfOutputs}."
                    );
            }
        }

        public static DecisionTree Create(double[][] inputs, int[] outputs, IList<DecisionVariable> attributes)
        {
            attributes ??= DecisionVariable.FromData(inputs);
            return CreateTree(outputs, attributes);
        }

        public static DecisionTree Create(int[][] inputs, int[] outputs, IList<DecisionVariable> attributes)
        {
            // Convert int[][] to double[][] for compatibility
            var convertedInputs = inputs.Select(row => row.Select(x => (double)x).ToArray()).ToArray();
            attributes ??= DecisionVariable.FromData(convertedInputs);
            return CreateTree(outputs, attributes);
        }

        public static DecisionTree Create(int?[][] inputs, int[] outputs, IList<DecisionVariable> attributes)
        {
            // Convert int?[][] to double[][], replacing nulls with double.NaN
            var convertedInputs = inputs
                .Select(row => row.Select(x => x.HasValue ? (double)x.Value : double.NaN).ToArray())
                .ToArray();

            attributes ??= DecisionVariable.FromData(convertedInputs);
            return CreateTree(outputs, attributes);
        }

        private static DecisionTree CreateTree(int[] outputs, IList<DecisionVariable> attributes)
        {
            int classes = outputs.Max() + 1;
            var tree = new DecisionTree(attributes.ToList(), classes);
            Debug.Log($"Decision Tree created with {classes} classes and {attributes.Count} attributes.");
            return tree;
        }
    }
}
