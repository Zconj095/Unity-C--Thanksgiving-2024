using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Collection of k-dimensional tree nodes for Unity.
    /// </summary>
    /// <typeparam name="TNode">The class type for the nodes of the tree.</typeparam>
    [Serializable]
    public class KDTreeNodeCollection<TNode> : ICollection<NodeDistance<TNode>>
        where TNode : KDTreeNodeBase<TNode>
    {
        private readonly double[] distances;
        private readonly TNode[] nodes;
        private int count;

        /// <summary>
        ///   Maximum number of elements in this collection.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        ///   Gets the number of elements in this collection.
        /// </summary>
        public int Count => count;

        /// <summary>
        ///   Gets a value indicating whether the collection is read-only (always false for this implementation).
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        ///   Initializes a new instance of the <see cref="KDTreeNodeCollection{TNode}"/> class with a specified capacity.
        /// </summary>
        /// <param name="capacity">Maximum number of elements allowed in the collection.</param>
        public KDTreeNodeCollection(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

            Capacity = capacity;
            distances = new double[capacity];
            nodes = new TNode[capacity];
        }

        /// <summary>
        ///   Adds a new node and its distance to the collection.
        ///   The collection will automatically sort and maintain its size.
        /// </summary>
        /// <param name="node">The node to add.</param>
        /// <param name="distance">The distance of the node from the query point.</param>
        /// <returns>True if the node was added; false otherwise.</returns>
        public bool Add(TNode node, double distance)
        {
            if (count < Capacity)
            {
                AddNode(distance, node);
                return true;
            }

            if (distance < MaximumDistance())
            {
                RemoveFarthest();
                AddNode(distance, node);
                return true;
            }

            return false;
        }

        /// <summary>
        ///   Adds the specified item to the collection.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(NodeDistance<TNode> item)
        {
            Add(item.Node, item.Distance);
        }

        /// <summary>
        ///   Clears all nodes from the collection.
        /// </summary>
        public void Clear()
        {
            Array.Clear(nodes, 0, nodes.Length);
            Array.Clear(distances, 0, distances.Length);
            count = 0;
        }

        /// <summary>
        ///   Determines whether the collection contains a specific value.
        /// </summary>
        /// <param name="item">The item to locate in the collection.</param>
        /// <returns>True if the item is found; otherwise, false.</returns>
        public bool Contains(NodeDistance<TNode> item)
        {
            for (int i = 0; i < count; i++)
            {
                if (nodes[i].Equals(item.Node) && Mathf.Approximately((float)distances[i], (float)item.Distance))
                    return true;
            }
            return false;
        }

        /// <summary>
        ///   Copies the elements of the collection to an array, starting at a specified index.
        /// </summary>
        /// <param name="array">The array to copy the elements to.</param>
        /// <param name="arrayIndex">The starting index in the array.</param>
        public void CopyTo(NodeDistance<TNode>[] array, int arrayIndex)
        {
            for (int i = 0; i < count; i++)
                array[arrayIndex + i] = new NodeDistance<TNode>(nodes[i], distances[i]);
        }

        /// <summary>
        ///   Removes the specified item from the collection. Not supported.
        /// </summary>
        public bool Remove(NodeDistance<TNode> item)
        {
            throw new NotSupportedException("Remove operation is not supported.");
        }

        /// <summary>
        ///   Gets an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<NodeDistance<TNode>> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
                yield return new NodeDistance<TNode>(nodes[i], distances[i]);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///   Gets the node with the smallest distance.
        /// </summary>
        public TNode Nearest => count > 0 ? nodes[0] : null;

        /// <summary>
        ///   Gets the node with the largest distance.
        /// </summary>
        public TNode Farthest => count > 0 ? nodes[count - 1] : null;

        /// <summary>
        ///   Gets the smallest distance in the collection.
        /// </summary>
        public double MinimumDistance()
        {
            if (count == 0)
                throw new InvalidOperationException("The collection is empty.");
            return distances[0];
        }

        /// <summary>
        ///   Gets the largest distance in the collection.
        /// </summary>
        public double MaximumDistance()
        {
            if (count == 0)
                throw new InvalidOperationException("The collection is empty.");
            return distances[count - 1];
        }

        /// <summary>
        ///   Adds a node and distance to the collection and ensures proper sorting.
        /// </summary>
        private void AddNode(double distance, TNode node)
        {
            int i = count++;
            distances[i] = distance;
            nodes[i] = node;

            while (i > 0 && distances[i] < distances[i - 1])
            {
                Swap(i, i - 1);
                i--;
            }
        }

        /// <summary>
        ///   Removes the farthest node from the collection.
        /// </summary>
        private void RemoveFarthest()
        {
            if (count > 0)
                count--;
        }

        /// <summary>
        ///   Swaps two elements in the collection.
        /// </summary>
        private void Swap(int indexA, int indexB)
        {
            (distances[indexA], distances[indexB]) = (distances[indexB], distances[indexA]);
            (nodes[indexA], nodes[indexB]) = (nodes[indexB], nodes[indexA]);
        }
    }
}
