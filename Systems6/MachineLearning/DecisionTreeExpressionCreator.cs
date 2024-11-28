using System;
using System.Collections.Generic;
using System.Reflection;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Unity-compatible Decision Tree Expression Creator.
    ///   This class uses reflection to evaluate decisions
    ///   made by a decision tree.
    /// </summary>
    internal class DecisionTreeExpressionCreator
    {
        private readonly DecisionTree tree;

        /// <summary>
        ///   Initializes a new instance of the <see cref="DecisionTreeExpressionCreator"/> class.
        /// </summary>
        /// <param name="tree">The decision tree.</param>
        internal DecisionTreeExpressionCreator(DecisionTree tree)
        {
            this.tree = tree ?? throw new ArgumentNullException(nameof(tree));
        }

        /// <summary>
        ///   Creates a delegate for the tree using reflection.
        /// </summary>
        /// <returns>A delegate that evaluates the decision tree.</returns>
        public Func<double[], int> Create()
        {
            return input =>
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Input cannot be null.");

                var currentNode = tree.Root;

                while (currentNode != null)
                {
                    // If this node is a leaf, return its output
                    if (currentNode.IsLeaf)
                    {
                        return currentNode.Output ?? -1; // Return -1 if no output is defined
                    }

                    // Fetch the attribute index from the current node
                    var attributeIndex = currentNode.AttributeIndex;

                    if (attributeIndex < 0 || attributeIndex >= input.Length)
                        throw new ArgumentException("Input contains a value outside of expected ranges.", nameof(input));

                    // Get the current attribute value
                    var attributeValue = input[attributeIndex];
                    DecisionNode nextNode = null;

                    // Evaluate conditions on all branches
                    foreach (var branch in currentNode.Branches)
                    {
                        if (EvaluateCondition(attributeValue, branch.Comparison, branch.Value))
                        {
                            nextNode = branch;
                            break;
                        }
                    }

                    // If no branch matches, throw an exception
                    if (nextNode == null)
                        throw new ArgumentException("Input contains a value outside of expected ranges.", nameof(input));

                    // Move to the next node
                    currentNode = nextNode;
                }

                // If traversal ends unexpectedly, return default
                return -1;
            };
        }


        /// <summary>
        ///   Evaluates a condition for a node's attribute value.
        /// </summary>
        /// <param name="value">The attribute value.</param>
        /// <param name="comparison">The comparison kind.</param>
        /// <param name="expected">The expected value for the comparison.</param>
        /// <returns>True if the condition is met; otherwise false.</returns>
        private bool EvaluateCondition(double value, ComparisonKind comparison, double? expected)
        {
            if (!expected.HasValue)
                throw new InvalidOperationException("Comparison value cannot be null.");

            switch (comparison)
            {
                case ComparisonKind.Equal:
                    return value == expected.Value;
                case ComparisonKind.GreaterThan:
                    return value > expected.Value;
                case ComparisonKind.GreaterThanOrEqual:
                    return value >= expected.Value;
                case ComparisonKind.LessThan:
                    return value < expected.Value;
                case ComparisonKind.LessThanOrEqual:
                    return value <= expected.Value;
                case ComparisonKind.NotEqual:
                    return value != expected.Value;
                default:
                    throw new InvalidOperationException("Unexpected node comparison type.");
            }
        }
    }
}
