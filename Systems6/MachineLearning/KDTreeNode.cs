using System;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// K-dimensional tree node for Unity.
    /// </summary>
    [Serializable]
    public class KDTreeNodeV2 : KDTreeNodeBase<KDTreeNodeV2>
    {
    }

    /// <summary>
    /// K-dimensional tree node for Unity with generic value support.
    /// </summary>
    /// <typeparam name="T">The type of the value being stored.</typeparam>
    [Serializable]
    public class KDTreeNodeV2<T> : KDTreeNodeBase<KDTreeNodeV2<T>>
    {
        /// <summary>
        /// The value stored in this node.
        /// </summary>
        public T Value { get; set; }
    }

    /// <summary>
    /// Base class for K-dimensional tree nodes in Unity.
    /// </summary>
    /// <typeparam name="TNode">The class type for the nodes of the tree.</typeparam>
    [Serializable]
    public class KDTreeNodeBaseV2<TNode> where TNode : KDTreeNodeBase<TNode>
    {
        /// <summary>
        /// The position of the node in spatial coordinates.
        /// </summary>
        public double[] Position { get; set; }

        /// <summary>
        /// The dimension index of the split. This value is an index of the Position vector.
        /// </summary>
        public int Axis { get; set; }

        /// <summary>
        /// Left child of this node.
        /// </summary>
        public TNode Left { get; set; }

        /// <summary>
        /// Right child of this node.
        /// </summary>
        public TNode Right { get; set; }

        /// <summary>
        /// Converts the node to a string representation for Unity debugging.
        /// </summary>
        public override string ToString()
        {
            if (Position == null)
                return "(null)";

            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            for (int i = 0; i < Position.Length; i++)
            {
                sb.Append(Position[i]);
                if (i < Position.Length - 1)
                    sb.Append(", ");
            }
            sb.Append(")");

            return sb.ToString();
        }

        /// <summary>
        /// Compares the current node with another node using Reflection.
        /// </summary>
        public int CompareTo(TNode other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            PropertyInfo axisProperty = typeof(TNode).GetProperty(nameof(Axis));
            PropertyInfo positionProperty = typeof(TNode).GetProperty(nameof(Position));

            if (axisProperty == null || positionProperty == null)
                throw new InvalidOperationException("Required properties not found in TNode.");

            int axis = (int)axisProperty.GetValue(this);
            double[] position = (double[])positionProperty.GetValue(this);
            double[] otherPosition = (double[])positionProperty.GetValue(other);

            return position[axis].CompareTo(otherPosition[axis]);
        }

        /// <summary>
        /// Determines equality of two nodes using Unity-compatible methods.
        /// </summary>
        public bool Equals(TNode other)
        {
            if (other == null)
                return false;

            // Check if the references match.
            if (ReferenceEquals(this, other))
                return true;

            // Use reflection to compare positions.
            PropertyInfo positionProperty = typeof(TNode).GetProperty(nameof(Position));
            double[] thisPosition = (double[])positionProperty.GetValue(this);
            double[] otherPosition = (double[])positionProperty.GetValue(other);

            if (thisPosition.Length != otherPosition.Length)
                return false;

            for (int i = 0; i < thisPosition.Length; i++)
            {
                if (!Mathf.Approximately((float)thisPosition[i], (float)otherPosition[i]))
                    return false;
            }

            return true;
        }
    }
}
