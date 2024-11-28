using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Represents a node-distance pair for k-dimensional trees in Unity.
    /// </summary>
    /// <typeparam name="TNode">The class type for the nodes of the tree.</typeparam>
    [Serializable]
    public struct NodeDistanceV3<TNode> : IComparable<NodeDistanceV3<TNode>>, IEquatable<NodeDistanceV3<TNode>>
        where TNode : IEquatable<TNode>
    {
        /// <summary>
        ///   The node in this pair.
        /// </summary>
        public TNode Node { get; }

        /// <summary>
        ///   The distance of the node from the query point.
        /// </summary>
        public double Distance { get; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="NodeDistanceV3{TNode}"/> struct.
        /// </summary>
        /// <param name="node">The node value.</param>
        /// <param name="distance">The distance value.</param>
        public NodeDistanceV3(TNode node, double distance)
        {
            Node = node;
            Distance = distance;
        }

        /// <summary>
        ///   Checks if the specified object is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>True if the object is equal to this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is NodeDistanceV3<TNode> other && Equals(other);
        }

        /// <summary>
        ///   Checks if the specified <see cref="NodeDistanceV3{TNode}"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The other node-distance pair to compare.</param>
        /// <returns>True if the other node-distance pair is equal to this instance; otherwise, false.</returns>
        public bool Equals(NodeDistanceV3<TNode> other)
        {
            return Mathf.Approximately((float)Distance, (float)other.Distance) && Node.Equals(other.Node);
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Node.GetHashCode();
            hash = hash * 23 + Distance.GetHashCode();
            return hash;
        }

        /// <summary>
        ///   Compares this instance to another <see cref="NodeDistanceV3{TNode}"/> instance.
        /// </summary>
        /// <param name="other">The other node-distance pair to compare.</param>
        /// <returns>
        ///   An integer indicating whether this instance is less than, equal to, or greater than the other instance.
        /// </returns>
        public int CompareTo(NodeDistanceV3<TNode> other)
        {
            return Distance.CompareTo(other.Distance);
        }

        /// <summary>
        ///   Checks if two <see cref="NodeDistanceV3{TNode}"/> instances are equal.
        /// </summary>
        public static bool operator ==(NodeDistanceV3<TNode> a, NodeDistanceV3<TNode> b)
        {
            return a.Equals(b);
        }

        /// <summary>
        ///   Checks if two <see cref="NodeDistanceV3{TNode}"/> instances are not equal.
        /// </summary>
        public static bool operator !=(NodeDistanceV3<TNode> a, NodeDistanceV3<TNode> b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        ///   Checks if the distance of one <see cref="NodeDistanceV3{TNode}"/> is less than another.
        /// </summary>
        public static bool operator <(NodeDistanceV3<TNode> a, NodeDistanceV3<TNode> b)
        {
            return a.Distance < b.Distance;
        }

        /// <summary>
        ///   Checks if the distance of one <see cref="NodeDistanceV3{TNode}"/> is greater than another.
        /// </summary>
        public static bool operator >(NodeDistanceV3<TNode> a, NodeDistanceV3<TNode> b)
        {
            return a.Distance > b.Distance;
        }

        /// <summary>
        ///   Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string that represents this instance.</returns>
        public override string ToString()
        {
            return $"<{Node}, {Distance}>";
        }
    }
}
