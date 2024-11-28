using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   K-dimensional tree for Unity.
    /// </summary>
    /// <typeparam name="T">The type of the value being stored.</typeparam>
    public class KDTree<T>
    {
        private readonly int dimensions;

        public KDTreeNode<T> Root { get; private set; }

        public KDTree(int dimensions)
        {
            if (dimensions <= 0)
                throw new ArgumentException("Dimensions must be greater than zero.", nameof(dimensions));

            this.dimensions = dimensions;
        }

        public KDTree(int dimensions, KDTreeNode<T> root)
            : this(dimensions)
        {
            Root = root;
        }

        public void Add(double[] position, T value)
        {
            if (position.Length != dimensions)
                throw new ArgumentException("Point dimensions do not match the tree dimensions.", nameof(position));

            Root = AddNode(Root, position, value, 0);
        }

        private KDTreeNode<T> AddNode(KDTreeNode<T> node, double[] position, T value, int depth)
        {
            if (node == null)
                return new KDTreeNode<T> { Position = position, Value = value, Axis = depth % dimensions };

            int axis = depth % dimensions;
            if (position[axis] < node.Position[axis])
                node.Left = AddNode(node.Left, position, value, depth + 1);
            else
                node.Right = AddNode(node.Right, position, value, depth + 1);

            return node;
        }

        public List<KDTreeNode<T>> Nearest(double[] position, double radius)
        {
            if (position.Length != dimensions)
                throw new ArgumentException("Point dimensions do not match the tree dimensions.", nameof(position));

            var results = new List<KDTreeNode<T>>();
            SearchNearestRadius(Root, position, radius * radius, results);
            return results;
        }

        private void SearchNearestRadius(KDTreeNode<T> node, double[] target, double radiusSquared, List<KDTreeNode<T>> results)
        {
            if (node == null)
                return;

            double distanceSquared = EuclideanDistanceSquared(node.Position, target);
            if (distanceSquared <= radiusSquared)
                results.Add(node);

            int axis = node.Axis;
            double diff = target[axis] - node.Position[axis];
            double diffSquared = diff * diff;

            if (diff <= 0)
            {
                SearchNearestRadius(node.Left, target, radiusSquared, results);
                if (diffSquared <= radiusSquared)
                    SearchNearestRadius(node.Right, target, radiusSquared, results);
            }
            else
            {
                SearchNearestRadius(node.Right, target, radiusSquared, results);
                if (diffSquared <= radiusSquared)
                    SearchNearestRadius(node.Left, target, radiusSquared, results);
            }
        }

        public List<KDTreeNode<T>> Nearest(double[] position, int neighbors)
        {
            if (position.Length != dimensions)
                throw new ArgumentException("Point dimensions do not match the tree dimensions.", nameof(position));

            var results = new PriorityQueue2<KDTreeNode<T>>(neighbors, Comparer<KDTreeNode<T>>.Create((a, b) =>
                EuclideanDistanceSquared(b.Position, position).CompareTo(EuclideanDistanceSquared(a.Position, position))));

            SearchNearestNeighbors(Root, position, results, neighbors);
            return new List<KDTreeNode<T>>(results);
        }

        private void SearchNearestNeighbors(KDTreeNode<T> node, double[] target, PriorityQueue2<KDTreeNode<T>> results, int neighbors)
        {
            if (node == null)
                return;

            double distanceSquared = EuclideanDistanceSquared(node.Position, target);
            results.Enqueue(node);

            if (results.Count > neighbors)
                results.Dequeue();

            int axis = node.Axis;
            double diff = target[axis] - node.Position[axis];
            double diffSquared = diff * diff;

            if (diff <= 0)
            {
                SearchNearestNeighbors(node.Left, target, results, neighbors);
                if (results.Count < neighbors || diffSquared <= EuclideanDistanceSquared(results.Min.Position, target))
                    SearchNearestNeighbors(node.Right, target, results, neighbors);
            }
            else
            {
                SearchNearestNeighbors(node.Right, target, results, neighbors);
                if (results.Count < neighbors || diffSquared <= EuclideanDistanceSquared(results.Min.Position, target))
                    SearchNearestNeighbors(node.Left, target, results, neighbors);
            }
        }

        private double EuclideanDistanceSquared(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }
            return sum;
        }
    }

    public class KDTreeNode<T>
    {
        public double[] Position { get; set; }
        public T Value { get; set; }
        public int Axis { get; set; }
        public KDTreeNode<T> Left { get; set; }
        public KDTreeNode<T> Right { get; set; }
    }

    public class PriorityQueue2<T> : SortedSet<T>
    {
        private readonly int maxSize;

        public PriorityQueue2(int maxSize, IComparer<T> comparer) : base(comparer)
        {
            this.maxSize = maxSize;
        }

        public new void Enqueue(T item)
        {
            Add(item);
            if (Count > maxSize)
                Remove(Max);
        }

        public T Dequeue()
        {
            var first = Min;
            Remove(first);
            return first;
        }
    }
}
