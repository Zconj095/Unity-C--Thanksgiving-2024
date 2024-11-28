using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Reduced Error Pruning for Decision Trees in Unity.
    /// </summary>
    public class ReducedErrorPruning
    {
        private readonly DecisionTree tree;
        private readonly double[][] inputs;
        private readonly int[] outputs;
        private readonly int[] actual;
        private readonly Dictionary<DecisionNode, NodeInfo> info;

        private class NodeInfo
        {
            public List<int> Subset { get; set; }
            public double Error { get; set; }
            public double Gain { get; set; }

            public NodeInfo()
            {
                Subset = new List<int>();
            }
        }

        /// <summary>
        ///   Initializes a new instance of the ReducedErrorPruning class.
        /// </summary>
        /// <param name="tree">The decision tree to prune.</param>
        /// <param name="inputs">The pruning set inputs.</param>
        /// <param name="outputs">The pruning set outputs.</param>
        public ReducedErrorPruning(DecisionTree tree, double[][] inputs, int[] outputs)
        {
            this.tree = tree;
            this.inputs = inputs;
            this.outputs = outputs;
            this.actual = new int[outputs.Length];
            this.info = new Dictionary<DecisionNode, NodeInfo>();

            InitializeNodeInfo();
            TrackDecisions(tree.Root);
        }

        /// <summary>
        ///   Performs one pass of the pruning algorithm.
        /// </summary>
        /// <returns>The overall error after pruning.</returns>
        public double Run()
        {
            ComputeErrors();
            ComputeGains();

            var (maxNode, maxGain) = GetMaxGainNode();

            if (maxGain >= 0 && maxNode != null)
            {
                PruneNode(maxNode);
            }

            return ComputeOverallError();
        }

        private void InitializeNodeInfo()
        {
            foreach (var node in tree.TraverseAll())
            {
                info[node] = new NodeInfo();
            }
        }

        private void TrackDecisions(DecisionNode root)
        {
            for (var i = 0; i < inputs.Length; i++)
            {
                TrackDecisionPath(root, inputs[i], i);
            }
        }

        private void TrackDecisionPath(DecisionNode node, double[] input, int index)
        {
            while (node != null)
            {
                info[node].Subset.Add(index);

                if (node.IsLeaf)
                {
                    actual[index] = node.Output.HasValue ? node.Output.Value : -1;
                    return;
                }

                var nextNode = GetNextNode(node, input);

                node = nextNode;
            }

            throw new InvalidOperationException("The tree is degenerated.");
        }

        private DecisionNode GetNextNode(DecisionNode node, double[] input)
        {
            var attributeIndex = node.AttributeIndex; // Fixed: Access AttributeIndex from the node itself.

            if (attributeIndex < 0 || attributeIndex >= input.Length)
                throw new IndexOutOfRangeException("Invalid attribute index in the decision node.");

            return node.Branches.FirstOrDefault(branch => branch.Compute(input[attributeIndex]));
        }

        private void ComputeErrors()
        {
            foreach (var node in tree.TraverseAll())
            {
                info[node].Error = ComputeNodeError(node);
            }
        }

        private double ComputeNodeError(DecisionNode node)
        {
            var indices = info[node].Subset;
            if (indices.Count == 0) return 0;

            return indices.Count(i => outputs[i] != actual[i]) / (double)indices.Count;
        }

        private void ComputeGains()
        {
            foreach (var node in tree.TraverseAll())
            {
                info[node].Gain = ComputeNodeGain(node);
            }
        }

        private double ComputeNodeGain(DecisionNode node)
        {
            if (node.IsLeaf) return double.NegativeInfinity;

            var childErrorSum = node.Branches.Sum(child => info[child].Error);
            var nodeError = info[node].Error;

            return nodeError - childErrorSum;
        }

        private (DecisionNode maxNode, double maxGain) GetMaxGainNode()
        {
            DecisionNode maxNode = null;
            var maxGain = double.NegativeInfinity;

            foreach (var node in tree.TraverseAll())
            {
                var gain = info[node].Gain;

                if (gain > maxGain)
                {
                    maxGain = gain;
                    maxNode = node;
                }
            }

            return (maxNode, maxGain);
        }

        private void PruneNode(DecisionNode node)
        {
            var subset = info[node].Subset.ToArray();
            var commonOutput = outputs.Subset(subset).GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;

            node.ClearBranches();
            node.Output = commonOutput;
        }

        private double ComputeOverallError()
        {
            return inputs.Select((input, i) => tree.Decide(input) != outputs[i] ? 1 : 0).Sum() / (double)inputs.Length;
        }
    }

    public static class DecisionTreeExtensions
    {
        public static IEnumerable<DecisionNode> TraverseAll(this DecisionTree tree)
        {
            return TraverseAll(tree.Root);
        }

        public static IEnumerable<DecisionNode> TraverseAll(DecisionNode node)
        {
            yield return node;

            foreach (var child in node.Branches ?? Enumerable.Empty<DecisionNode>())
            {
                foreach (var descendant in TraverseAll(child))
                {
                    yield return descendant;
                }
            }
        }

        public static int[] Subset(this int[] array, int[] indices)
        {
            return indices.Select(i => array[i]).ToArray();
        }
    }
}
