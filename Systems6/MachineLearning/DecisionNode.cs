using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Decision Node for Decision Trees in Unity.
    /// </summary>
    [Serializable]
    public class DecisionNode : IEnumerable<DecisionNode>
    {
        private DecisionNode parent;

        /// <summary>
        ///   The index of the attribute this node tests.
        /// </summary>
        public int AttributeIndex { get; set; }

        public double? Value { get; set; }
        public ComparisonKind Comparison { get; set; }
        public int? Output { get; set; }
        public List<DecisionNode> Branches { get; private set; }
        public DecisionTree Owner { get; set; }
        public DecisionNode Parent
        {
            get => parent;
            set => parent = value;
        }

        public bool IsRoot => Parent == null;
        public bool IsLeaf => Branches == null || Branches.Count == 0;

        /// <summary>
        ///   Creates a new DecisionNode.
        /// </summary>
        /// <param name="owner">The tree that owns this node.</param>
        public DecisionNode(DecisionTree owner)
        {
            Owner = owner;
            Comparison = ComparisonKind.None;
            Branches = new List<DecisionNode>();
            AttributeIndex = -1; // Default to an invalid index.
        }

        /// <summary>
        ///   Determines if a value satisfies this node's condition.
        /// </summary>
        public bool Compute(double x)
        {
            switch (Comparison)
            {
                case ComparisonKind.Equal:
                    return x == Value;
                case ComparisonKind.GreaterThan:
                    return x > Value;
                case ComparisonKind.GreaterThanOrEqual:
                    return x >= Value;
                case ComparisonKind.LessThan:
                    return x < Value;
                case ComparisonKind.LessThanOrEqual:
                    return x <= Value;
                case ComparisonKind.NotEqual:
                    return x != Value;
                default:
                    throw new InvalidOperationException("Unsupported comparison type.");
            }
        }

        /// <summary>
        ///   Determines if an integer value satisfies this node's condition.
        /// </summary>
        public bool Compute(int x)
        {
            return Compute((double)x);
        }

        /// <summary>
        ///   Clears all branches of the current node.
        /// </summary>
        public void ClearBranches()
        {
            Branches?.Clear();
        }

        /// <summary>
        ///   Computes the height of the node from the root.
        /// </summary>
        public int GetHeight()
        {
            int height = 0;
            DecisionNode current = Parent;

            while (current != null)
            {
                height++;
                current = current.Parent;
            }

            return height;
        }

        /// <summary>
        ///   Returns a string representation of this node.
        /// </summary>
        public override string ToString()
        {
            if (IsRoot)
                return "Root";

            var attributeName = Owner?.GetAttributeName(AttributeIndex) ?? "Attribute";
            var op = Comparison.ToString();
            var value = Value.HasValue ? Value.ToString() : "null";

            return $"{attributeName} {op} {value}";
        }

        /// <summary>
        ///   Traverses the subtree of this node.
        /// </summary>
        public IEnumerator<DecisionNode> GetEnumerator()
        {
            var stack = new Stack<DecisionNode>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;

                if (current.Branches != null)
                {
                    foreach (var child in current.Branches)
                        stack.Push(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///   Dynamically sets a property of this node using Reflection.
        /// </summary>
        public void SetProperty(string propertyName, object value)
        {
            var property = GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(this, value);
            }
            else
            {
                Debug.LogError($"Property {propertyName} not found or is read-only.");
            }
        }

        /// <summary>
        ///   Dynamically gets a property of this node using Reflection.
        /// </summary>
        public object GetProperty(string propertyName)
        {
            var property = GetType().GetProperty(propertyName);
            if (property != null && property.CanRead)
            {
                return property.GetValue(this);
            }
            else
            {
                Debug.LogError($"Property {propertyName} not found or is write-only.");
                return null;
            }
        }
    }
}
