using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   ID3 Learning Algorithm for Decision Trees in Unity.
    /// </summary>
    public class ID3Learning
    {
        private DecisionTreeV2 tree;
        private int[] attributeUsageCount;

        public bool Rejection { get; set; } = true;

        /// <summary>
        ///   Initializes a new instance of the ID3 learning algorithm.
        /// </summary>
        /// <param name="tree">The decision tree to be trained.</param>
        public ID3Learning(DecisionTreeV2 tree)
        {
            this.tree = tree ?? throw new ArgumentNullException(nameof(tree));
            InitializeTreeAttributes(tree);
        }

        private void InitializeTreeAttributes(DecisionTreeV2 tree)
        {
            attributeUsageCount = new int[tree.NumberOfInputs];
        }

        /// <summary>
        ///   Trains a decision tree using the ID3 algorithm.
        /// </summary>
        /// <param name="inputs">The input dataset.</param>
        /// <param name="outputs">The output labels.</param>
        public DecisionTreeV2 Learn(int[][] inputs, int[] outputs)
        {
            if (tree == null)
                throw new InvalidOperationException("The decision tree must be initialized before learning.");

            ResetAttributeUsage();
            SplitNode(tree.Root, inputs, outputs, 0);
            return tree;
        }

        private void ResetAttributeUsage()
        {
            for (int i = 0; i < attributeUsageCount.Length; i++)
                attributeUsageCount[i] = 0;
        }

        private void SplitNode(DecisionNodeV2 node, int[][] inputs, int[] outputs, int depth)
        {
            if (outputs.Distinct().Count() == 1)
            {
                node.Output = outputs[0];
                return;
            }

            if (inputs[0].Length == 0 || depth == tree.MaxDepth)
            {
                node.Output = GetMostCommonLabel(outputs);
                return;
            }

            int bestAttribute = SelectBestAttribute(inputs, outputs);
            if (bestAttribute == -1)
            {
                node.Output = GetMostCommonLabel(outputs);
                return;
            }

            node.SplitAttribute = bestAttribute;

            var partitions = PartitionData(inputs, outputs, bestAttribute);
            foreach (var partition in partitions)
            {
                var child = new DecisionNodeV2(tree)
                {
                    Parent = node,
                    ComparisonValue = partition.Key
                };

                SplitNode(child, partition.Value.Inputs, partition.Value.Outputs, depth + 1);
                node.Children.Add(child);
            }
        }

        private int SelectBestAttribute(int[][] inputs, int[] outputs)
        {
            double maxGain = double.NegativeInfinity;
            int bestAttribute = -1;

            for (int i = 0; i < inputs[0].Length; i++)
            {
                if (attributeUsageCount[i] > 0)
                    continue;

                double gain = CalculateInformationGain(inputs, outputs, i);
                if (gain > maxGain)
                {
                    maxGain = gain;
                    bestAttribute = i;
                }
            }

            if (bestAttribute != -1)
                attributeUsageCount[bestAttribute]++;

            return bestAttribute;
        }

        private double CalculateInformationGain(int[][] inputs, int[] outputs, int attributeIndex)
        {
            double entropyBefore = CalculateEntropy(outputs);
            var partitions = PartitionData(inputs, outputs, attributeIndex);

            double entropyAfter = 0;
            foreach (var partition in partitions.Values)
            {
                double proportion = (double)partition.Outputs.Length / outputs.Length;
                entropyAfter += proportion * CalculateEntropy(partition.Outputs);
            }

            return entropyBefore - entropyAfter;
        }

        private double CalculateEntropy(int[] outputs)
        {
            var probabilities = outputs
                .GroupBy(x => x)
                .Select(g => (double)g.Count() / outputs.Length);

            return -probabilities.Sum(p => p * Math.Log(p, 2));
        }

        private Dictionary<int, (int[][] Inputs, int[] Outputs)> PartitionData(int[][] inputs, int[] outputs, int attributeIndex)
        {
            var partitions = new Dictionary<int, List<int>>();
            for (int i = 0; i < inputs.Length; i++)
            {
                int key = inputs[i][attributeIndex];
                if (!partitions.ContainsKey(key))
                    partitions[key] = new List<int>();

                partitions[key].Add(i);
            }

            var result = new Dictionary<int, (int[][], int[])>();
            foreach (var partition in partitions)
            {
                int[][] inputSubset = partition.Value.Select(idx => inputs[idx]).ToArray();
                int[] outputSubset = partition.Value.Select(idx => outputs[idx]).ToArray();
                result[partition.Key] = (inputSubset, outputSubset);
            }

            return result;
        }

        private int GetMostCommonLabel(int[] outputs)
        {
            return outputs
                .GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }
    }

    /// <summary>
    ///   Represents a Decision Tree Node.
    /// </summary>
    public class DecisionNodeV2
    {
        public DecisionNodeV2 Parent { get; set; }
        public int? SplitAttribute { get; set; }
        public int? Output { get; set; }
        public int? ComparisonValue { get; set; }
        public List<DecisionNodeV2> Children { get; set; } = new List<DecisionNodeV2>();

        public DecisionNodeV2(DecisionTreeV2 tree) { }
    }

    /// <summary>
    ///   Represents a Decision Tree.
    /// </summary>
    public class DecisionTreeV2
    {
        public DecisionNodeV2 Root { get; set; }
        public int NumberOfInputs { get; set; }
        public int MaxDepth { get; set; }

        public DecisionTreeV2(int numberOfInputs, int maxDepth)
        {
            NumberOfInputs = numberOfInputs;
            MaxDepth = maxDepth;
            Root = new DecisionNodeV2(this);
        }
    }
}
