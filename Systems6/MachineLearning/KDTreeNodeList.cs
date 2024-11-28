using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   List of k-dimensional tree nodes for Unity.
    /// </summary>
    /// <typeparam name="T">The type of the value being stored.</typeparam>
    /// <remarks>
    ///   This class is used to store neighbor nodes when running one of the
    ///   search algorithms for k-dimensional trees.
    /// </remarks>
    [Serializable]
    public class KDTreeNodeList<T> : List<NodeDistanceV2<T>>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="KDTreeNodeList{T}"/> class that is empty.
        /// </summary>
        public KDTreeNodeList()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="KDTreeNodeList{T}"/> class with a specified capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the list.</param>
        public KDTreeNodeList(int capacity) : base(capacity)
        {
        }
    }

    /// <summary>
    ///   Represents a node-distance pair for k-dimensional trees.
    /// </summary>
    /// <typeparam name="T">The type of the value being stored in the node.</typeparam>
    [Serializable]
    public class NodeDistanceV2<T>
    {
        /// <summary>
        ///   The node associated with this distance.
        /// </summary>
        public T Node { get; }

        /// <summary>
        ///   The distance to the query point.
        /// </summary>
        public double Distance { get; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="NodeDistanceV2{T}"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="distance">The distance from the query point to the node.</param>
        public NodeDistanceV2(T node, double distance)
        {
            Node = node;
            Distance = distance;
        }

        /// <summary>
        ///   Returns a string representation of the node-distance pair.
        /// </summary>
        public override string ToString()
        {
            return $"{Node} at distance {Distance}";
        }
    }
}
