using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Decision Tree for Unity-based projects.
    /// </summary>
    [Serializable]
    public class DecisionTree : IEnumerable<DecisionNode>
    {
        [SerializeField] private DecisionNode root;
        [SerializeField] private List<DecisionVariable> attributes;

        // Properties for DecisionTree
        public int NumberOfInputs { get; private set; }
        public int NumberOfOutputs { get; private set; }
        public int NumberOfClasses { get; private set; }

        /// <summary>
        ///   Gets or sets the root node for this tree.
        /// </summary>
        public DecisionNode Root
        {
            get => root;
            set => root = value;
        }

        /// <summary>
        ///   Gets the collection of attributes processed by this tree.
        /// </summary>
        public List<DecisionVariable> Attributes => attributes;

        /// <summary>
        ///   Initializes a new DecisionTree with the given attributes.
        /// </summary>
        /// <param name="attributes">List of attributes for decision-making.</param>
        /// <param name="numberOfClasses">Number of output classes for this tree.</param>
        public DecisionTree(List<DecisionVariable> attributes, int numberOfClasses = 2)
        {
            if (attributes == null || attributes.Count == 0)
                throw new ArgumentException("Attributes cannot be null or empty.", nameof(attributes));

            this.attributes = attributes;

            // Initialize properties based on the input
            NumberOfInputs = attributes.Count;
            NumberOfOutputs = 1; // Assuming a single output for classification
            NumberOfClasses = numberOfClasses;
        }

        /// <summary>
        ///   Gets the name of the attribute by index.
        /// </summary>
        public string GetAttributeName(int index)
        {
            if (index < 0 || index >= attributes.Count)
                throw new IndexOutOfRangeException($"Attribute index {index} is out of range.");

            return attributes[index].Name;
        }

        /// <summary>
        ///   Predicts the class for a given input using the tree.
        /// </summary>
        public int Decide(double[] input)
        {
            if (Root == null)
                throw new InvalidOperationException("The decision tree has no root node.");

            return Decide(input, Root);
        }

        /// <summary>
        ///   Predicts the class for a given input starting from a specific subtree.
        /// </summary>
        public int Decide(double[] input, DecisionNode subtree)
        {
            DecisionNode current = subtree;

            while (current != null)
            {
                if (current.IsLeaf)
                    return current.Output.HasValue ? current.Output.Value : -1;

                int attributeIndex = current.AttributeIndex;
                double value = input[attributeIndex];

                current = current.Branches.FirstOrDefault(branch => branch.Compute(value));
            }

            throw new InvalidOperationException("The tree structure is invalid.");
        }

        /// <summary>
        ///   Predicts the classes for multiple inputs.
        /// </summary>
        public int[] Decide(double[][] inputs)
        {
            if (inputs == null || inputs.Length == 0)
                throw new ArgumentException("Inputs cannot be null or empty.", nameof(inputs));

            return inputs.Select(Decide).ToArray();
        }

        /// <summary>
        ///   Traverses the tree using pre-order traversal.
        /// </summary>
        public IEnumerator<DecisionNode> GetEnumerator()
        {
            if (Root == null)
                yield break;

            var stack = new Stack<DecisionNode>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;

                if (current.Branches != null)
                {
                    for (int i = current.Branches.Count - 1; i >= 0; i--)
                        stack.Push(current.Branches[i]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Range
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public Range(double min, double max)
        {
            Min = min;
            Max = max;
        }
    }
}
